using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.infoViewManager
{
    public partial class receiptView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                int usrAuth = 0;
                string strUsrAuth = Session["totleAuthority"] as string;
                usrAuth = int.Parse(strUsrAuth);
                int flag = 0x1 << 6;

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
            string strCNM = e.CommandName;

            if (strCNM.Equals("detailView"))
            {
                GridViewRow gvr = (e.CommandSource as LinkButton).Parent.Parent as GridViewRow;

                int itemIndex = gvr.DataItemIndex;

                DataTable dt = Session["dtSources"] as DataTable;

                DataRow dr = dt.Rows[itemIndex];

                Session["seldSelfReceipt"] = dr;

                Response.Redirect("~/Main/infoViewManager/receiptExamine.aspx");
            }
        }

        protected void selfReceiptGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void selfReceiptGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    int index = e.Row.RowIndex;

            //    LinkButton btn = e.Row.FindControl("detailView") as LinkButton;

            //    if (null != btn)
            //    {
            //        btn.CommandArgument = index.ToString();
            //    }
            //}
        }
    }
}