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
    public partial class receiptApply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.pay_receiptApply);
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

            if (Session["seldMainContract"] == null)
            {
                Response.Redirect("~/Main/paymentReceiptManager/mainContractReceiptView.aspx");
            }

            if (!IsPostBack)
            {
                DataRow sessionDr = Session["seldMainContract"] as DataRow;
                string strMainContractId = sessionDr["mainContractId"].ToString();

                //DataSet MyDst = new DataSet();

                #region selfReceiptGV
                //ReceiptApplyProcess receiptApplyView = new ReceiptApplyProcess(MyDst);

                //receiptApplyView.RealSelfReceiptView();
                //DataTable dt = receiptApplyView.MyDst.Tables["view_mainReceipt"];

                //string strFilter =
                //    " mainContractId = " + "'" + mainContractId + "'";
                //dt.DefaultView.RowFilter = strFilter;
                //DataTable taskTable = dt.DefaultView.ToTable();
                Xm_db xmDataCont = Xm_db.GetInstance();

                int mainContractId = int.Parse(strMainContractId);

                var mainReceiptView =
                    from mainReceipt in xmDataCont.View_mainReceipt
                    where mainReceipt.EndTime > DateTime.Now &&
                          mainReceipt.MainContractId == mainContractId
                    select mainReceipt;
                    
                DataTable taskTable = mainReceiptView.ToDataTable();
                
                dt_modify(taskTable, string.Empty);

                //Session["ReceiptApplyProcess"] = receiptApplyView;
                Session["dtSources"] = taskTable;

                btnVisible_Init(taskTable);
                
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

        void btnVisible_Init(DataTable dt)
        {
            DataTable testTable = dt.DefaultView.ToTable();

            if (testTable.Rows.Count <= 0)
            {
                testTable.Clear();
                return;
            }
 
            string strFilter =
                "doneTime > " + "'" + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + "'";
            testTable.DefaultView.RowFilter = strFilter;

            if (testTable.DefaultView.Count > 0)
            {
                btnAdd.Visible = false;
            }

            testTable.Clear();
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

        protected void toDel_Click(object sender, EventArgs e)
        {
            LinkButton lbt = sender as LinkButton;
            int businessProductId = int.Parse(lbt.CommandArgument);

            selfReceiptGV.SelectedIndex = (lbt.Parent.Parent as GridViewRow).DataItemIndex;
            selfReceiptGV.Enabled = false;
            selfReceiptGV.DataSource = Session["dtSources"];
            selfReceiptGV.DataBind();

            btnAccept.Visible = true;
            btnCancel.Visible = true;
            btnAdd.Visible = false;
            btnNo.Visible = false;
        }

        protected void toEdit_Click(object sender, EventArgs e)
        {
            int receiptId = int.Parse((sender as LinkButton).CommandArgument);
            Xm_db xmDataCont = Xm_db.GetInstance();

            var receiptApplyEdit =
                from receiptApply in xmDataCont.Tbl_receiptApply
                where receiptApply.ReceiptId == receiptId &&
                      receiptApply.IsAccept.Equals(bool.TrueString)
                select receiptApply;

            if (receiptApplyEdit.Count() == 0)
            {
                Session["receiptId"] = receiptId.ToString();

                Response.Redirect("~/Main/paymentReceiptManager/receiptEdit.aspx");
            }
            else
            {
                Page.ClientScript.ShowAlertWindow("无权修改", this.GetType());
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/paymentReceiptManager/receiptAdd.aspx");
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/paymentReceiptManager/mainContractReceiptView.aspx");
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            int index = selfReceiptGV.SelectedIndex;
            LinkButton lkb = selfReceiptGV.Rows[index].FindControl("toDel") as LinkButton;

            int receiptId = int.Parse(lkb.CommandArgument);

            Xm_db xmDataCont = Xm_db.GetInstance();

            var receiptApplyEdit =
                from receiptApply in xmDataCont.Tbl_receiptApply
                where receiptApply.ReceiptId == receiptId &&
                      receiptApply.IsAccept == "unDo"
                select receiptApply;

            if (receiptApplyEdit.Count() > 0)
            {
                receiptApplyEdit.First().EndTime = DateTime.Now;

                try
                {
                    xmDataCont.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
                }
                catch (System.Data.Linq.ChangeConflictException cce)
                {
                    string strEx = cce.Message;
                    foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataCont.ChangeConflicts)
                    {
                        occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                    }

                    xmDataCont.SubmitChanges();
                }

                DataRow sessionDr = Session["seldMainContract"] as DataRow;
                int mainContractId = int.Parse(sessionDr["mainContractId"].ToString());

                lkb.Visible = true;

                var subPaymentEdit =
                    from subPayment in xmDataCont.View_mainReceipt
                    where subPayment.EndTime > DateTime.Now &&
                          subPayment.MainContractId == mainContractId
                    select subPayment;

                DataTable taskTable = subPaymentEdit.ToDataTable();
                dt_modify(taskTable, string.Empty);

                DataTable dtSource = taskTable;
                Session["dtSources"] = dtSource;

                selfReceiptGV.DataSource = Session["dtSources"];
                selfReceiptGV.DataBind();

                btnVisible_Init(dtSource);
            }
            else
            {
                Page.ClientScript.ShowAlertWindow("无权修改", this.GetType());
            }

            selfReceiptGV.SelectedIndex = -1;
            selfReceiptGV.Enabled = true;

            btnAccept.Visible = false;
            btnCancel.Visible = false;

            btnAdd.Visible = true;
            btnNo.Visible = true;            
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            DataTable dtSource = Session["dtSources"] as DataTable;

            selfReceiptGV.SelectedIndex = -1;
            selfReceiptGV.Enabled = true;
            //selfReceiptGV.DataSource = dtSource;
            //selfReceiptGV.DataBind();

            btnVisible_Init(dtSource);

            btnAccept.Visible = false;
            btnCancel.Visible = false;

            btnAdd.Visible = true;
            btnNo.Visible = true;

            //int index = selfReceiptGV.SelectedIndex;
            //GridViewRow gvr = selfReceiptGV.Rows[index];

            //Label lbl = gvr.FindControl("lblMessage") as Label;
            //lbl.Visible = false;
        }
    }
}