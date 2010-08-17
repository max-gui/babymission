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
    public partial class mainContractReceiptView : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                string usrId = Session["usrId"] as string;

                DataSet MyDst = new DataSet();

                #region mainContractGV
                MainContractProcess mainContractView = new MainContractProcess(MyDst);
                mainContractView.UsrId = usrId;

                mainContractView.RealmainContractProjectUsrView();
                DataTable taskTable = mainContractView.MyDst.Tables["view_mainContract_project_usr"];

                Session["MainContractProcess"] = mainContractView;
                Session["dtSources"] = taskTable;


                mainContractGV.DataSource = Session["dtSources"];
                mainContractGV.DataBind();
                #endregion
            }
        }

        protected void mainContractGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mainContractGV.PageIndex = e.NewPageIndex;

            mainContractGV.DataSource = Session["dtSources"];
            mainContractGV.DataBind();
        }

        protected void mainContractGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void mainContractGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;

                Button btn = e.Row.FindControl("btnRecieptApply") as Button;

                if (null != btn)
                {
                    btn.CommandArgument = index.ToString();
                }

                DataTable dt = Session["dtSources"] as DataTable;

                int num = int.Parse(dt.DefaultView[index]["selfReceivingPercent"].ToString());

                DropDownList ddl = e.Row.FindControl("ddlPay") as DropDownList;

                if (null != ddl)
                {
                    //ListItemCollection lic = new ListItemCollection();
                    string strValue = string.Empty;
                    string strPercent = "%";
                    string strText = string.Empty;
                    for (int i = num;i <= 100;i = i + 10)
                    {
                        strValue = i.ToString();

                        strText = strValue + strPercent;
                        ddl.Items.Add(strText);
                        ddl.Items.FindByText(strText).Value = strValue;
                        //lic.Add(strText);
                        //lic.FindByText(strText).Value = strValue;
                    }

                    //ddl.DataSource = lic;
                    //ddl.DataBind();
                    ddl.SelectedValue = num.ToString();
                }
            }
        }        

        protected void mainContractGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strCNM = e.CommandName;

            if (strCNM.Equals("payEdit"))
            {
                GridViewRow gvr = (e.CommandSource as LinkButton).Parent.Parent as GridViewRow;

                int index = gvr.RowIndex;
                string strIndex = index.ToString();
                DropDownList ddl = mainContractGV.Rows[index].FindControl("ddlPay") as DropDownList;
                if (null != ddl)
                {
                    ddl.Enabled = true;
                    
                    //ddl.Enabled = false;

                    mainContractGV.Columns[8].Visible = false;
                    mainContractGV.Columns[9].Visible = false;
                    mainContractGV.Columns[10].Visible = false;

                    btnOk.Visible = true;
                    btnOk.CommandArgument = strIndex;
                    btnNo.Visible = true;
                    btnNo.CommandArgument = strIndex;
                }
            }
        }

        protected void btnRecieptApply_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int index = int.Parse(btn.CommandArgument);
            int numIndex = mainContractGV.Rows[index].DataItemIndex;

            DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            DataRow dr = dt.Rows[numIndex];

            Session["seldMainContract"] = dr;

            Response.Redirect("~/Main/paymentReceiptManager/receiptApply.aspx");
        }

        protected void btnSubContractEdit_Click(object sender, EventArgs e)
        {
            GridViewRow dvr = (sender as Button).Parent.Parent as GridViewRow;
            int index = dvr.RowIndex;
            int numIndex = mainContractGV.Rows[index].DataItemIndex;

            DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            DataRow dr = dt.Rows[numIndex];

            Session["seldMainContract"] = dr;

            Response.Redirect("~/Main/paymentReceiptManager/subContractPaymentView.aspx");
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            int index = int.Parse(btnOk.CommandArgument);            

            int dataIndex = mainContractGV.Rows[index].DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];

            GridViewRow row = mainContractGV.Rows[index];
            DropDownList ddlPay = row.FindControl("ddlPay") as DropDownList;
            string payPercent = ddlPay.SelectedValue.ToString();

            int mainContractId = int.Parse(dt.DefaultView[dataIndex].Row["mainContractId"].ToString());

            MainContractProcess mcp = Session["MainContractProcess"] as MainContractProcess;

            mcp.MainContractPayPercentUpdate(mainContractId, payPercent);

            mcp.RealmainContractProjectUsrView();

            DataTable taskTable = mcp.MyDst.Tables["view_mainContract_project_usr"];
            Session["dtSources"] = taskTable;

            mainContractGV.DataSource = Session["dtSources"];
            mainContractGV.DataBind();

            Button btn = null;
            btn = sender as Button;
            btn.Visible = false;
            btn = btnNo;
            btn.Visible = false;
            mainContractGV.Columns[8].Visible = true;
            mainContractGV.Columns[9].Visible = true;
            mainContractGV.Columns[10].Visible = true;
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            int index = int.Parse(btnNo.CommandArgument);

            DropDownList ddl = mainContractGV.Rows[index].FindControl("ddlPay") as DropDownList;
            ddl.Enabled = false;

            mainContractGV.Columns[8].Visible = true;
            mainContractGV.Columns[9].Visible = true;
            mainContractGV.Columns[10].Visible = true;

            btnOk.Visible = false;
            btnNo.Visible = false;
        }
    }
}