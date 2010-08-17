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
    public partial class receiptApply : System.Web.UI.Page
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
                DataSet MyDst = new DataSet();

                #region selfReceiptGV
                ReceiptApplyProcess receiptApplyView = new ReceiptApplyProcess(MyDst);

                receiptApplyView.RealSelfReceiptView();
                DataTable taskTable = receiptApplyView.MyDst.Tables["tbl_receiptApply"];

                DataColumn colCustMaxReceipt = new DataColumn("custMaxReceiptPercent", System.Type.GetType("System.String"));
                DataColumn colSelfReceipt = new DataColumn("selfReceiptPercent", System.Type.GetType("System.String"));
                DataColumn colAcceptOrNot = new DataColumn("acceptOrNot", System.Type.GetType("System.String"));
                DataColumn colDone = new DataColumn("Done", System.Type.GetType("System.String"));
                taskTable.Columns.Add(colCustMaxReceipt);
                taskTable.Columns.Add(colSelfReceipt);
                taskTable.Columns.Add(colAcceptOrNot);
                taskTable.Columns.Add(colDone);

                string strPercent = "%".ToString();
                string strAccept = "已批准";
                string strNotAccept = "已驳回";
                string strUnExamine = "未审批";
                string strNotDone = "未完成";
                string strNotDoneTime = "9999/12/31 0:00:00";
                string strDoneTime = string.Empty;
                foreach (DataRow dr in taskTable.Rows)
                {
                    dr["custMaxReceiptPercent"] = dr["custMaxReceipt"].ToString() + strPercent;
                    dr["selfReceiptPercent"] = dr["receiptPercent"].ToString() + strPercent;
                    strDoneTime = dr["doneTime"].ToString();
                    if (strDoneTime.Equals(strNotDoneTime))
                    {
                        dr["Done"] = strNotDone;
                    }
                    else
                    {
                        dr["Done"] = strDoneTime;
                    }
                    if (dr["isAccept"].ToString().Equals("unExamine"))
                    {
                        dr["acceptOrNot"] = strUnExamine;
                    }
                    else if (dr["isAccept"].ToString().Equals(bool.FalseString))
                    {
                        dr["acceptOrNot"] = strNotAccept;
                    }
                    else
                    {
                        dr["acceptOrNot"] = strAccept;
                    }
                }

                Session["ReceiptApplyProcess"] = receiptApplyView;
                Session["dtSources"] = taskTable;
                
                selfReceiptGV.DataSource = Session["dtSources"];
                selfReceiptGV.DataBind();
                #endregion

                DataTable dt = taskTable.DefaultView.ToTable();
                string strFilter =
                    "doneTime = " + "'" + "9999-12-31" + "'";
                dt.DefaultView.RowFilter = strFilter;

                if (dt.DefaultView.Count > 0)
                {
                    btnAdd.Visible = false;
                }
            }
        }

        protected void selfReceiptGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            selfReceiptGV.PageIndex = e.NewPageIndex;

            selfReceiptGV.DataSource = Session["dtSources"];
            selfReceiptGV.DataBind();
        }

        protected void selfReceiptGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void selfReceiptGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void selfReceiptGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //int index = e.Row.RowIndex;

                //Button btn = e.Row.FindControl("btnRecieptApply") as Button;

                //if (null != btn)
                //{
                //    btn.CommandArgument = index.ToString();
                //}

                //DataTable dt = Session["dtSources"] as DataTable;

                //int num = int.Parse(dt.DefaultView[index]["selfReceivingPercent"].ToString());

                //DropDownList ddl = e.Row.FindControl("ddlPay") as DropDownList;

                //if (null != ddl)
                //{
                //    //ListItemCollection lic = new ListItemCollection();
                //    string strValue = string.Empty;
                //    string strPercent = "%";
                //    string strText = string.Empty;
                //    for (int i = num; i <= 100; i = i + 10)
                //    {
                //        strValue = i.ToString();

                //        strText = strValue + strPercent;
                //        ddl.Items.Add(strText);
                //        ddl.Items.FindByText(strText).Value = strValue;
                //        //lic.Add(strText);
                //        //lic.FindByText(strText).Value = strValue;
                //    }

                //    //ddl.DataSource = lic;
                //    //ddl.DataBind();
                //    ddl.SelectedValue = num.ToString();
            //    }
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
    }
}