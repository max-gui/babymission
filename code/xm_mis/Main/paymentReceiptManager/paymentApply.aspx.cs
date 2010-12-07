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
    public partial class paymentApply : System.Web.UI.Page
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

            if (Session["seldSubContract"] == null)
            {
                Response.Redirect("~/Main/paymentReceiptManager/subContractPaymentView.aspx");
            }

            if (!IsPostBack)
            {
                DataRow sessionDr = Session["seldSubContract"] as DataRow;
                string strSubContractId = sessionDr["subContractId"].ToString();

                //DataSet MyDst = new DataSet();

                #region selfPaymentGV
                //PaymentApplyProcess payApplyView = new PaymentApplyProcess(MyDst);

                //payApplyView.RealSelfPaymentView();
                //DataTable dt = payApplyView.MyDst.Tables["view_subPayment"].DefaultView.ToTable();

                //string strFilter =
                //    " subContractId = " + "'" + strSubContractId + "'";
                //dt.DefaultView.RowFilter = strFilter;
                //DataTable taskTable = dt.DefaultView.ToTable();

                Xm_db xmDataCont = Xm_db.GetInstance();

                int subContractId = int.Parse(strSubContractId);

                var subPaymentView =
                    from subPayment in xmDataCont.View_subPayment
                    where subPayment.EndTime > DateTime.Now &&
                          subPayment.SubContractId == subContractId
                    select subPayment;

                DataTable taskTable = subPaymentView.ToDataTable();
                dt_modify(taskTable, string.Empty);

                Session["dtSources"] = taskTable;

                btnVisible_Init(taskTable);

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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/paymentReceiptManager/paymentAdd.aspx");
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/paymentReceiptManager/subContractPaymentView.aspx");
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

        protected void toDel_Click(object sender, EventArgs e)
        {
            LinkButton lbt = sender as LinkButton;

            selfPaymentGV.SelectedIndex = (lbt.Parent.Parent as GridViewRow).DataItemIndex;
            selfPaymentGV.Enabled = false;
            selfPaymentGV.DataSource = Session["dtSources"];
            selfPaymentGV.DataBind();

            btnAccept.Visible = true;
            btnCancel.Visible = true;
            btnAdd.Visible = false;
            btnNo.Visible = false;
        }

        protected void toEdit_Click(object sender, EventArgs e)
        {
            int paymentId = int.Parse((sender as LinkButton).CommandArgument);
            Xm_db xmDataCont = Xm_db.GetInstance();

            var paymentApplyEdit =
                from paymentApply in xmDataCont.Tbl_paymentApply
                where paymentApply.PaymentId == paymentId &&
                      paymentApply.IsAccept.Equals(bool.TrueString)
                select paymentApply;

            if (paymentApplyEdit.Count() == 0)
            {
                Session["seldPaymentId"] = paymentId.ToString();

                Response.Redirect("~/Main/paymentReceiptManager/paymentEdit.aspx");
            }
            else
            {
                Page.ClientScript.ShowAlertWindow("无权修改", this.GetType());
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            int index = selfPaymentGV.SelectedIndex;
            LinkButton lkb = selfPaymentGV.Rows[index].FindControl("toDel") as LinkButton;
            
            int paymentId = int.Parse(lkb.CommandArgument);

            Xm_db xmDataCont = Xm_db.GetInstance();

            var paymentApplyEdit =
                from paymentApply in xmDataCont.Tbl_paymentApply
                where paymentApply.PaymentId == paymentId &&
                      paymentApply.IsAccept == "unDo"
                select paymentApply;

            if (paymentApplyEdit.Count() > 0)
            {
                paymentApplyEdit.First().EndTime = DateTime.Now;

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

                //GridViewRow gvr = selfPaymentGV.Rows[index];

                //Label lbl = gvr.FindControl("lblMessage") as Label;
                //lbl.Visible = false;

                lkb.Visible = true;

                DataRow sessionDr = Session["seldSubContract"] as DataRow;
                int subContractId = int.Parse(sessionDr["subContractId"].ToString());

                var subPaymentEdit =
                    from subPayment in xmDataCont.View_subPayment
                    where subPayment.EndTime > DateTime.Now &&
                          subPayment.SubContractId == subContractId
                    select subPayment;

                DataTable taskTable = subPaymentEdit.ToDataTable();
                dt_modify(taskTable, string.Empty);

                DataTable dtSource = taskTable;
                Session["dtSources"] = dtSource;

                btnVisible_Init(dtSource);

                selfPaymentGV.DataSource = Session["dtSources"];
                selfPaymentGV.DataBind();
            }
            else
            {
                //Label lbl = (lkb.Parent.Parent as GridViewRow).FindControl("lblMessage") as Label;
                //lbl.Text = "无权删除";
                //lbl.Visible = true;
                Page.ClientScript.ShowAlertWindow("无权修改", this.GetType());
            }

            selfPaymentGV.SelectedIndex = -1;
            selfPaymentGV.Enabled = true;

            btnAccept.Visible = false;
            btnCancel.Visible = false;

            btnAdd.Visible = true;
            btnNo.Visible = true;
                
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            DataTable dtSource = Session["dtSources"] as DataTable;

            selfPaymentGV.SelectedIndex = -1;
            selfPaymentGV.Enabled = true;
            //selfPaymentGV.DataSource = dtSource;
            //selfPaymentGV.DataBind();

            btnVisible_Init(dtSource);

            btnAccept.Visible = false;
            btnCancel.Visible = false;

            btnAdd.Visible = true;
            btnNo.Visible = true;

            //int index = selfPaymentGV.SelectedIndex;
            //GridViewRow gvr = selfPaymentGV.Rows[index];

            //Label lbl = gvr.FindControl("lblMessage") as Label;
            //lbl.Visible = false;
        }
    }
}