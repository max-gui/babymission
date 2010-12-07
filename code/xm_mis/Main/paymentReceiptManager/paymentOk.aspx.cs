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
    public partial class paymentOk : System.Web.UI.Page
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

                #region selfPaymentGV
                PaymentApplyProcess paymentApplyView = new PaymentApplyProcess(MyDst);

                paymentApplyView.RealSelfPaymentView();
                DataTable taskTable = paymentApplyView.MyDst.Tables["view_subPayment"].DefaultView.ToTable();

                string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                string strFilter =
                    " isAccept = " + "'" + bool.TrueString + "'" +
                    " and doneTime > " + "'" + end + "'";

                dt_modify(taskTable, strFilter);

                //dt.DefaultView.RowFilter = strFilter;
                //DataTable taskTable = dt.DefaultView.ToTable();
                //dt.Clear();                

                Session["PaymentApplyProcess"] = paymentApplyView;
                Session["dtSources"] = taskTable.DefaultView.ToTable();

                taskTable.Clear();

                selfPaymentGV.DataSource = Session["dtSources"];
                selfPaymentGV.DataBind();
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

        protected void btnOk_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = selfPaymentGV.SelectedRow;

            int index = gvr.DataItemIndex;

            DataTable dt = Session["dtSources"] as DataTable;

            string strPaymentId = dt.Rows[index]["paymentId"].ToString();
            int usrId = int.Parse(dt.Rows[index]["usrId"].ToString());
            string strProjetTag = dt.Rows[index]["projectTag"].ToString();
            //string payNum = dt.Rows[index]["payPercent"].ToString();
            string strPayPercent = dt.Rows[index]["payPercent"].ToString();
            string strSubContractId = dt.Rows[index]["subContractId"].ToString();
            string strProjetTagId = dt.Rows[index]["projectTagId"].ToString();

            //string strFilter =
            //        " paymentId = " + "'" + strPaymentId + "'";
            //dt.DefaultView.RowFilter = strFilter;

            //DataTable doneTable = dt.DefaultView.ToTable("addTable");
            //dt.Clear();

            PaymentApplyProcess pap = Session["PaymentApplyProcess"] as PaymentApplyProcess;

            //pap.MyDst.Tables.Add(doneTable);
            //pap.SelfPaymentDone();

            Xm_db xmDataCont = Xm_db.GetInstance();

            int paymentId = int.Parse(strPaymentId);
            float payPercent = float.Parse(strPayPercent);
            int subContractId = int.Parse(strSubContractId);
            int projectTagId = int.Parse(strProjetTagId);
            string payNum = payPercent.ToString("p");
            xmDataCont.SelfPayment_done(paymentId, payPercent, subContractId, projectTagId);            

            try
            {
                xmDataCont.SelfPayment_done(paymentId, payPercent, subContractId, projectTagId);

                xmDataCont.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);

                mailDetail(usrId, strProjetTag, payNum, xmDataCont);
            }
            catch (System.Data.Linq.ChangeConflictException cce)
            {
                string strEx = cce.Message;
                foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataCont.ChangeConflicts)
                {
                    occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                }

                xmDataCont.SelfPayment_done(paymentId, payPercent, subContractId, projectTagId);

                xmDataCont.SubmitChanges();

                mailDetail(usrId, strProjetTag, payNum, xmDataCont);
            }

            var viewSubPayment =
                from subPayment in xmDataCont.View_subPayment
                where subPayment.EndTime > DateTime.Now &&
                    subPayment.IsAccept.Equals(bool.TrueString) &&
                    subPayment.DoneTime > DateTime.Now
                select subPayment;

            DataTable taskTable = viewSubPayment.ToDataTable();
            //pap.RealSelfPaymentView();
            //DataTable taskTable = pap.MyDst.Tables["view_subPayment"].DefaultView.ToTable();

            //string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
            string strFilter = string.Empty;
            //    " isAccept = " + "'" + bool.TrueString + "'" +
            //    " and doneTime > " + "'" + end + "'";

            dt_modify(taskTable, strFilter);

            Session["dtSources"] = taskTable;

            selfPaymentGV.SelectedIndex = -1;
            selfPaymentGV.Enabled = true;
            selfPaymentGV.Columns[12].Visible = true;

            selfPaymentGV.DataSource = Session["dtSources"];
            selfPaymentGV.DataBind();

            btnOk.Visible = false;
            btnNo.Visible = false;
        }

        private static void mailDetail(int usrId, string strProjetTag, string payNum, Xm_db xmDataCont)
        {
            //var usr_autority =
            //from usr in xmDataCont.Tbl_usr
            //join auth in xmDataCont.View_usr_autority on usr.UsrId equals auth.UsrId
            //where (auth.UsrId == usrId ||
            //      auth.AuthorityId == 25) &&
            //        auth.UsrAuEnd > DateTime.Now
            //select usr;

            //int flag = 0x2000;
            var usr_autority =
                from usr in xmDataCont.Tbl_usr
                //join auth in xmDataCont.View_usr_autority on usr.UsrId equals auth.UsrId
                where (usr.UsrId == usrId ||
                      ((usr.TotleAuthority & (UInt32)AuthAttributes.pay_receiptExamine) != 0)) &&
                      usr.EndTime > DateTime.Now
                //where (usr.UsrId == usrId ||
                //      usr.TotleAuthority.ToAuthAttr().HasOneFlag(AuthAttributes.pay_receiptExamine)) &&
                //      usr.EndTime > DateTime.Now
                select usr;

            foreach (var usr in usr_autority)
            {
                BeckSendMail.getMM().NewMail(usr.UsrEmail, "mis系统票务通知", strProjetTag + "的付款申请已完成" + payNum + "，请尽快完成后续工作");
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            selfPaymentGV.SelectedIndex = -1;
            selfPaymentGV.Enabled = true;
            selfPaymentGV.Columns[14].Visible = true;

            selfPaymentGV.DataSource = Session["dtSources"];
            selfPaymentGV.DataBind();

            btnOk.Visible = false;
            btnNo.Visible = false;
        }

        protected void paymentDone_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (sender as LinkButton).Parent.Parent as GridViewRow;

            int index = gvr.RowIndex;

            selfPaymentGV.SelectedIndex = index;
            selfPaymentGV.Enabled = false;
            selfPaymentGV.Columns[14].Visible = false;

            selfPaymentGV.DataSource = Session["dtSources"];
            selfPaymentGV.DataBind();

            btnOk.Visible = true;
            btnNo.Visible = true;
        }

        protected void selfPaymentGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            selfPaymentGV.PageIndex = e.NewPageIndex;

            selfPaymentGV.DataSource = Session["dtSources"];
            selfPaymentGV.DataBind();
        }

        protected void selfPaymentGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}