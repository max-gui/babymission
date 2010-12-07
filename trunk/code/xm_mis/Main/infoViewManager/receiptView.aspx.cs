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
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.pay_receiptExamine);
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
                
                DataColumn colDone = new DataColumn("Done", System.Type.GetType("System.String"));
                taskTable.Columns.Add(colDone);

                string strNotDone = "未完成";
                DateTime doneTime = DateTime.Now;
                foreach (DataRow dr in taskTable.Rows)
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
    }
}