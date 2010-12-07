using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.paymentReceiptManager
{
    public partial class receiptOk : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.pay_receiptOk);
                if (!flag)
                {
                    Response.Redirect("~/Main/NoAuthority.aspx");
                }
            }
            else
            {
                string url = Request.FilePath;
                Session["backUrl"] = url;
                Response.Redirect("~/Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                DataSet MyDst = new DataSet();

                #region selfReceiptGV
                ReceiptApplyProcess receiptApplyView = new ReceiptApplyProcess(MyDst);

                receiptApplyView.RealSelfReceiptView();
                DataTable taskTable = receiptApplyView.MyDst.Tables["view_mainReceipt"].DefaultView.ToTable();

                string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                string strFilter =
                    " isAccept = " + "'" + bool.TrueString + "'" +
                    " and doneTime > " + "'" + end + "'";

                dt_modify(taskTable, strFilter);

                //dt.DefaultView.RowFilter = strFilter;
                //DataTable taskTable = dt.DefaultView.ToTable();
                //dt.Clear();                

                Session["ReceiptApplyProcess"] = receiptApplyView;
                Session["dtSources"] = taskTable.DefaultView.ToTable();

                taskTable.Clear();

                selfReceiptGV.DataSource = Session["dtSources"];
                selfReceiptGV.DataBind();
                #endregion
            }
        }

        protected void dt_modify(DataTable dt, string strFilter)
        {
            dt.DefaultView.RowFilter = strFilter;

            DataColumn colDone = new DataColumn("Done", System.Type.GetType("System.String"));
            dt.Columns.Add(colDone);

            string strNotDone = "未完成";
            DateTime doneTime = DateTime.Now;
            foreach (DataRow dr in dt.Rows)
            {
                doneTime = (DateTime)dr["doneTime"];
                if (doneTime > DateTime.Now)
                {
                    dr["Done"] = strNotDone;
                }
                else
                {
                    dr["Done"] = doneTime.ToString();
                }
            }
        }

        protected void receiptDone_Click(object sender, EventArgs e)
        {

            GridViewRow gvr = (sender as LinkButton).Parent.Parent as GridViewRow;

            int index = gvr.RowIndex;

            selfReceiptGV.SelectedIndex = index;
            selfReceiptGV.Enabled = false;
            selfReceiptGV.Columns[12].Visible = false;

            selfReceiptGV.DataSource = Session["dtSources"];
            selfReceiptGV.DataBind();

            btnOk.Visible = true;
            btnNo.Visible = true;
        }

        protected void selfReceiptGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            selfReceiptGV.PageIndex = e.NewPageIndex;

            selfReceiptGV.DataSource = Session["dtSources"];
            selfReceiptGV.DataBind();
        }

        protected void selfReceiptGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = selfReceiptGV.SelectedRow;

            int index = gvr.DataItemIndex;

            DataTable dt = Session["dtSources"] as DataTable;

            string strReceiptId = dt.Rows[index]["receiptId"].ToString();
            int usrId = int.Parse(dt.Rows[index]["usrId"].ToString());
            string projetTag = dt.Rows[index]["projectTag"].ToString();
            //string receiptNum = dt.Rows[index]["receiptPercent"].ToString();
            string strReceiptPercent = dt.Rows[index]["receiptPercent"].ToString();
            string strMainContractId = dt.Rows[index]["mainContractId"].ToString();
            string strProjetTagId = dt.Rows[index]["projectTagId"].ToString();

            //string strFilter =
            //        " receiptId = " + "'" + strReceiptId + "'";
            //dt.DefaultView.RowFilter = strFilter;

            //DataTable doneTable = dt.DefaultView.ToTable("addTable");
            //dt.Clear();

            //ReceiptApplyProcess rap = Session["ReceiptApplyProcess"] as ReceiptApplyProcess;

            //rap.MyDst.Tables.Add(doneTable);
            //rap.SelfReceiptDone();

            Xm_db xmDataCont = Xm_db.GetInstance();

            int receiptId = int.Parse(strReceiptId);
            float receiptPercent = float.Parse(strReceiptPercent);
            int mainContractId = int.Parse(strMainContractId);
            int projectTagId = int.Parse(strProjetTagId);
            string receiptNum = receiptPercent.ToString("p");

            xmDataCont.SelfReceipt_done(receiptId, receiptPercent, mainContractId, projectTagId);


            //var usr_autority =
            //    from usr in xmDataCont.Tbl_usr
            //    join auth in xmDataCont.View_usr_autority on usr.UsrId equals auth.UsrId
            //    where (auth.UsrId == usrId ||
            //          auth.AuthorityId == 25) &&
            //            auth.UsrAuEnd > DateTime.Now
            //    select usr;

            //int flag = 0x2000;
            var usr_autority =
                from usr in xmDataCont.Tbl_usr
                //join auth in xmDataCont.View_usr_autority on usr.UsrId equals auth.UsrId
                where (usr.UsrId == usrId ||
                      ((usr.TotleAuthority & (UInt32)AuthAttributes.pay_receiptExamine) != 0)) &&
                      usr.EndTime > DateTime.Now
                select usr;
                //where (usr.UsrId == usrId ||
                //      usr.TotleAuthority.ToAuthAttr().HasOneFlag(AuthAttributes.pay_receiptExamine)) &&
                //      usr.EndTime > DateTime.Now
                //select usr;

            foreach (var usr in usr_autority)
            {
                BeckSendMail.getMM().NewMail(usr.UsrEmail, "mis系统票务通知", projetTag + "的开票申请已完成" + receiptNum + "，请尽快完成后续工作");
            }

            var viewMainReceipt =
                from mainReceipt in xmDataCont.View_mainReceipt
                where mainReceipt.EndTime > DateTime.Now &&
                    mainReceipt.IsAccept.Equals(bool.TrueString) &&
                    mainReceipt.DoneTime > DateTime.Now
                select mainReceipt;

            DataTable taskTable = viewMainReceipt.ToDataTable();
            //rap.RealSelfReceiptView();
            //DataTable taskTable = rap.MyDst.Tables["view_mainReceipt"].DefaultView.ToTable();

            //string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
            //strFilter =
            //    " isAccept = " + "'" + bool.TrueString + "'" +
            //    " and doneTime > " + "'" + end + "'";

            string strFilter = string.Empty;

            dt_modify(taskTable, strFilter);

            Session["dtSources"] = taskTable.DefaultView.ToTable();

            selfReceiptGV.SelectedIndex = -1;
            selfReceiptGV.Enabled = true;
            selfReceiptGV.Columns[12].Visible = true;

            selfReceiptGV.DataSource = Session["dtSources"];
            selfReceiptGV.DataBind();

            btnOk.Visible = false;
            btnNo.Visible = false;
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            selfReceiptGV.SelectedIndex = -1;
            selfReceiptGV.Enabled = true;
            selfReceiptGV.Columns[12].Visible = true;

            selfReceiptGV.DataSource = Session["dtSources"];
            selfReceiptGV.DataBind();

            btnOk.Visible = false;
            btnNo.Visible = false;
        }
    }
}