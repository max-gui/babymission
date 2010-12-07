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
    public partial class subContractPaymentView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.pay_receiptApply | AuthAttributes.pay_receiptOk);
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

            if (null == Session["seldMainContract"])
            {
                Response.Redirect("~/Main/paymentReceiptManager/mainContractReceiptView.aspx");
            }

            if (!IsPostBack)
            {
                DataRow sessionDr = Session["seldMainContract"] as DataRow; 
                
                string strMainContractId = sessionDr["mainContractId"].ToString();
                string strFilter =
                    " mainContractId = " + "'" + strMainContractId + "'";
                
                #region selMainContract
                
                lblProjectTag.Text = sessionDr["projectTag"].ToString();
                lblMainContractTag.Text = sessionDr["mainContractTag"].ToString();
                lblCust.Text = sessionDr["custCompName"].ToString();
                lblMainContractMoney.Text = sessionDr["cash"].ToString();
                lblMainContractDateLine.Text = sessionDr["dateLine"].ToString();
                lblMainContractPayment.Text = sessionDr["paymentMode"].ToString();

                #endregion
                

                #region subContractGV
                //DataSet vscsDst = new DataSet();
                //subContractProcess vscsView = new subContractProcess(vscsDst);

                //vscsView.RealSubContractSupplierView();
                //DataTable taskTable = vscsView.MyDst.Tables["view_subContract_supplier"].DefaultView.ToTable();
                Xm_db xmDataCont = Xm_db.GetInstance();
                int mainContractId = int.Parse(strMainContractId);

                reflash(mainContractId, xmDataCont);

                //taskTable.DefaultView.RowFilter = strFilter;
                //dt_modify(taskTable, strFilter);

                //Session["subContractProcess"] = vscsView;
                //Session["dtSources"] = taskTable;

                subContractGV.DataSource = Session["dtSources"];
                subContractGV.DataBind();
                #endregion

                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                //bool flag = usrAuthAttr.hasOneFlag(AuthAttributes.pay_receiptApply | AuthAttributes.projectTagApply);

                if (usrAuthAttr.HasOneFlag(AuthAttributes.pay_receiptApply))
                {
                    subContractGV.Columns[6].Visible = false; 
                }
                else if (usrAuthAttr.HasOneFlag(AuthAttributes.pay_receiptOk))
                {
                    subContractGV.Columns[8].Visible = false;
                }
            }
        }

        //protected void dt_modify(DataTable dt, string strFilter)
        //{
        //    //DataColumn colSelfPay = new DataColumn("selfPay", System.Type.GetType("System.String"));
        //    //dt.Columns.Add(colSelfPay);

        //    dt.DefaultView.RowFilter = strFilter;

        //    //string strPercent = "%".ToString();
        //    //foreach (DataRow dr in dt.Rows)
        //    //{
        //    //    dr["selfPay"] = dr["receivingPercent"].ToString() + strPercent;
        //    //}
        //}
        protected void receiptEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (sender as LinkButton).Parent.Parent as GridViewRow;

            int index = gvr.RowIndex;
            string strIndex = index.ToString();
            DropDownList ddl = subContractGV.Rows[index].FindControl("ddlReceipt") as DropDownList;

            if (null != ddl)
            {
                ddl.Enabled = true;

                //ddl.Enabled = false;

                subContractGV.Columns[6].Visible = false;
                subContractGV.Columns[8].Visible = false;

                btnOk.Visible = true;
                btnOk.CommandArgument = strIndex;
                btnNo.Visible = true;
                btnNo.CommandArgument = strIndex;

                btnRtn.Visible = false;
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            int index = int.Parse(btnOk.CommandArgument);

            int dataIndex = subContractGV.Rows[index].DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];

            GridViewRow row = subContractGV.Rows[index];
            DropDownList ddlReceipt = row.FindControl("ddlReceipt") as DropDownList;
            float receiptPercent = float.Parse(ddlReceipt.SelectedValue.ToString());

            int subContractId = int.Parse(dt.DefaultView[dataIndex].Row["subContractId"].ToString());
            string strMainContractId = dt.DefaultView[dataIndex].Row["mainContractId"].ToString();
            string strFilter =
                " mainContractId = " + "'" + strMainContractId + "'";

            //subContractProcess scp = Session["subContractProcess"] as subContractProcess;

            //scp.SubContractReceiptPercentUpdate(subContractId, receiptPercent);


            //string strUsrId = Session["usrId"] as string;

            //int usrId = int.Parse(strUsrId);
            int mainContractId = int.Parse(strMainContractId);
            string num = receiptPercent.ToString("p");

            Xm_db xmDataCont = Xm_db.GetInstance();

            try
            {
                xmDataCont.SubContract_receiptPercent_update(subContractId, receiptPercent);

                xmDataCont.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);

                reflash(mainContractId, xmDataCont);

                sendMail(mainContractId, xmDataCont, num);
            }
            catch (System.Data.Linq.ChangeConflictException cce)
            {
                string strEx = cce.Message;
                foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataCont.ChangeConflicts)
                {
                    occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                }

                xmDataCont.SubContract_receiptPercent_update(subContractId, receiptPercent);

                xmDataCont.SubmitChanges();

                reflash(mainContractId, xmDataCont);

                sendMail(mainContractId, xmDataCont, num);
            }

            //xmDataCont.SubContract_receiptPercent_update(subContractId, receiptPercent);

            //var ViewsSubContract_supplier =
            //    from subContract_supplier in xmDataCont.View_subContract_supplier
            //    where subContract_supplier.MainContractId == mainContractId &&
            //          subContract_supplier.EndTime > DateTime.Now
            //    select subContract_supplier;

            //var emailDetail =
            //    from mainContract in xmDataCont.Tbl_mainContract
            //    join project in xmDataCont.Tbl_projectTagInfo on mainContract.ProjectTagId equals project.ProjectTagId
            //    join applyment_user in xmDataCont.Tbl_applyment_user on project.ProjectTagId equals applyment_user.ProjectTagId
            //    join user in xmDataCont.Tbl_usr on applyment_user.UsrId equals user.UsrId
            //    where mainContract.MainContractId == mainContractId
            //    select new { user.UsrEmail };

            //string usrEmail = emailDetail.First().UsrEmail;
            //string num = ViewsSubContract_supplier.First(elm => elm.SubContractId == subContractId).ReceiptPercent.ToString("p");

            //DataTable dtSources = projectStepEdit.Distinct().ToDataTable();

            //BeckSendMail.getMM().NewMail(usrEmail, "mis系统票务通知", "总共收到客户票款额" + num);

            //DataTable taskTable = ViewsSubContract_supplier.ToDataTable();

            //taskTable.DefaultView.RowFilter = strFilter;
            //scp.RealSubContractSupplierView();

            
            //DataTable taskTable = scp.MyDst.Tables["view_subContract_supplier"];
            //dt_modify(taskTable, strFilter);

            //Session["dtSources"] = taskTable;

            subContractGV.DataSource = Session["dtSources"];
            subContractGV.DataBind();

            Button btn = null;
            btn = sender as Button;
            btn.Visible = false;
            btn = btnNo;
            btn.Visible = false;
            subContractGV.Columns[6].Visible = true;
            subContractGV.Columns[8].Visible = true;

            btnRtn.Visible = true;
        }

        private void reflash(int mainContractId, Xm_db xmDataCont)
        {
            var ViewsSubContract_supplier =
                from subContract_supplier in xmDataCont.View_subContract_supplier
                where subContract_supplier.MainContractId == mainContractId &&
                      subContract_supplier.EndTime > DateTime.Now
                select subContract_supplier;

            DataTable taskTable = ViewsSubContract_supplier.ToDataTable();

            Session["dtSources"] = taskTable;
        }

        private static void sendMail(System.Nullable<int> mainContractId, Xm_db xmDataCont, string num)
        {
            var emailDetail =
                from mainContract in xmDataCont.Tbl_mainContract
                join project in xmDataCont.Tbl_projectTagInfo on mainContract.ProjectTagId equals project.ProjectTagId
                join applyment_user in xmDataCont.Tbl_applyment_user on project.ProjectTagId equals applyment_user.ProjectTagId
                join user in xmDataCont.Tbl_usr on applyment_user.UsrId equals user.UsrId
                where mainContract.MainContractId == mainContractId
                select new { user.UsrEmail, project.ProjectTag };

            string usrEmail = emailDetail.First().UsrEmail;
            string projectTag = emailDetail.First().ProjectTag;
            //string num = emailDetail.SelfReceivingPercent.ToString("p");
            //string num = ViewsSubContract_supplier.First(elm => elm.SubContractId == subContractId).ReceiptPercent.ToString("p");
            //DataTable dtSources = projectStepEdit.Distinct().ToDataTable();

            BeckSendMail.getMM().NewMail(usrEmail, "mis系统票务通知", projectTag + "项目总共收到客户付款额" + num);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            int index = int.Parse(btnNo.CommandArgument);

            DropDownList ddl = subContractGV.Rows[index].FindControl("ddlReceipt") as DropDownList;
            ddl.Enabled = false;

            subContractGV.Columns[6].Visible = true;
            subContractGV.Columns[8].Visible = true;

            btnOk.Visible = false;
            btnNo.Visible = false;

            btnRtn.Visible = true;
        }

        protected void btnPayApply_Click(object sender, EventArgs e)
        {
            GridViewRow dvr = (sender as Button).Parent.Parent as GridViewRow;
            int index = dvr.RowIndex;
            int numIndex = subContractGV.Rows[index].DataItemIndex;

            DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            DataRow dr = dt.Rows[numIndex];

            Session["seldSubContract"] = dr;

            Response.Redirect("~/Main/paymentReceiptManager/paymentApply.aspx");
        }

        protected void subContractGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            subContractGV.PageIndex = e.NewPageIndex;

            subContractGV.DataSource = Session["dtSources"];
            subContractGV.DataBind();
        }

        protected void subContractGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void subContractGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;

                DataTable dt = Session["dtSources"] as DataTable;

                //int num = int.Parse(dt.DefaultView[index]["receiptPercent"].ToString());
                float num = float.Parse(dt.DefaultView[index]["receiptPercent"].ToString());

                DropDownList ddl = e.Row.FindControl("ddlReceipt") as DropDownList;

                if (null != ddl)
                {
                    string strValue = string.Empty;
                    //string strPercent = "%";
                    string strText = string.Empty;
                    for (float i = num; i < 1.05f; i = i + 0.05f)
                    {
                        strValue = i.ToString();

                        strText = i.ToString("p");
                        ddl.Items.Add(strText);
                        ddl.Items.FindByText(strText).Value = strValue;
                    }

                    ddl.SelectedValue = num.ToString();
                }
            }
        }

        protected void subContractGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
        }

        protected void btnRtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/paymentReceiptManager/mainContractReceiptView.aspx");
        }
    }
}