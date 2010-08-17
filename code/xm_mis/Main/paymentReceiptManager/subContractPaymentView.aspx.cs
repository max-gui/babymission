using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.paymentReceiptManager
{
    public partial class subContractPaymentView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                int usrAuth = 0;
                string strUsrAuth = Session["totleAuthority"] as string;
                usrAuth = int.Parse(strUsrAuth);
                int flag = 0x1 << 7;

                if ((usrAuth & flag) == 0)
                    Response.Redirect("~/Main/NoAuthority.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (null == Session["seldMainContract"])
            {
                Response.Redirect("~/Main/paymentReceiptManager/mainContractReceiptView.aspx");
            }

            if (!IsPostBack)
            {
                DataRow sessionDr = Session["seldMainContract"] as DataRow; 
                
                string mainContractId = sessionDr["mainContractId"].ToString();
                string strFilter =
                    " mainContractId = " + "'" + mainContractId + "'";
                
                #region selMainContract
                
                lblProjectTag.Text = sessionDr["projectTag"].ToString();
                lblMainContractTag.Text = sessionDr["mainContractTag"].ToString();
                lblCust.Text = sessionDr["contractCompName"].ToString();
                lblMainContractMoney.Text = sessionDr["cash"].ToString();
                lblMainContractDateLine.Text = sessionDr["dateLine"].ToString();
                lblMainContractPayment.Text = sessionDr["paymentMode"].ToString();

                #endregion
                

                #region subContractGV
                DataSet vscsDst = new DataSet();
                subContractProcess vscsView = new subContractProcess(vscsDst);

                vscsView.RealSubContractSupplierView();
                DataTable taskTable = vscsView.MyDst.Tables["view_subContract_supplier"].DefaultView.ToTable();                

                dt_modify(taskTable, strFilter);

                Session["subContractProcess"] = vscsView;
                Session["dtSources"] = taskTable;

                subContractGV.DataSource = Session["dtSources"];
                subContractGV.DataBind();
                #endregion
            }
        }

        protected void dt_modify(DataTable dt, string strFilter)
        {
            DataColumn colSelfPay = new DataColumn("selfPay", System.Type.GetType("System.String"));
            dt.Columns.Add(colSelfPay);

            dt.DefaultView.RowFilter = strFilter;

            string strPercent = "%".ToString();
            foreach (DataRow dr in dt.Rows)
            {
                dr["selfPay"] = dr["receivingPercent"].ToString() + strPercent;
            }
        }
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

                subContractGV.Columns[7].Visible = false;
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
            string receiptPercent = ddlReceipt.SelectedValue.ToString();

            int subContractId = int.Parse(dt.DefaultView[dataIndex].Row["subContractId"].ToString());
            string mainContractId = dt.DefaultView[dataIndex].Row["mainContractId"].ToString();
            string strFilter =
                " mainContractId = " + "'" + mainContractId + "'";

            subContractProcess scp = Session["subContractProcess"] as subContractProcess;

            scp.SubContractReceiptPercentUpdate(subContractId, receiptPercent);

            scp.RealSubContractSupplierView();

            
            DataTable taskTable = scp.MyDst.Tables["view_subContract_supplier"];
            dt_modify(taskTable, strFilter);

            Session["dtSources"] = taskTable;

            subContractGV.DataSource = Session["dtSources"];
            subContractGV.DataBind();

            Button btn = null;
            btn = sender as Button;
            btn.Visible = false;
            btn = btnNo;
            btn.Visible = false;
            subContractGV.Columns[7].Visible = true;
            subContractGV.Columns[8].Visible = true;

            btnRtn.Visible = true;
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            int index = int.Parse(btnNo.CommandArgument);

            DropDownList ddl = subContractGV.Rows[index].FindControl("ddlReceipt") as DropDownList;
            ddl.Enabled = false;

            subContractGV.Columns[7].Visible = true;
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

                int num = int.Parse(dt.DefaultView[index]["receiptPercent"].ToString());

                DropDownList ddl = e.Row.FindControl("ddlReceipt") as DropDownList;

                if (null != ddl)
                {
                    string strValue = string.Empty;
                    string strPercent = "%";
                    string strText = string.Empty;
                    for (int i = num; i <= 100; i = i + 10)
                    {
                        strValue = i.ToString();

                        strText = strValue + strPercent;
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