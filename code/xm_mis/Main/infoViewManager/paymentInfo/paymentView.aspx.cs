using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.infoViewManager.paymentInfo
{
    public partial class paymentView : System.Web.UI.Page
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

                #region selfPaymentGV
                PaymentApplyProcess payApplyView = new PaymentApplyProcess(MyDst);

                payApplyView.RealSelfPaymentView();
                DataTable taskTable = payApplyView.MyDst.Tables["view_subPayment"].DefaultView.ToTable();

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

                selfPaymentGV.DataSource = Session["dtSources"];
                selfPaymentGV.DataBind();
                #endregion
            }
        }

        protected void selfPaymentGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            selfPaymentGV.PageIndex = e.NewPageIndex;

            selfPaymentGV.DataSource = Session["dtSources"];
            selfPaymentGV.DataBind();
        }

        protected void detailView_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (sender as LinkButton).Parent.Parent as GridViewRow;

            int itemIndex = gvr.DataItemIndex;

            DataTable dt = Session["dtSources"] as DataTable;

            DataRow dr = dt.Rows[itemIndex];

            Session["seldSelfPayment"] = dr;

            Response.Redirect("~/Main/infoViewManager/paymentInfo/paymentExamine.aspx");
        }

        protected void selfPaymentGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void selfPaymentGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }
    }
}