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
    public partial class paymentApply : System.Web.UI.Page
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

                #region selfPaymentGV
                PaymentApplyProcess payApplyView = new PaymentApplyProcess(MyDst);

                payApplyView.RealSelfPaymentView();
                DataTable taskTable = payApplyView.MyDst.Tables["tbl_paymentApply"];

                DataColumn colCustMaxPay = new DataColumn("custMaxPayPercent", System.Type.GetType("System.String"));
                DataColumn colSelfPay = new DataColumn("selfPayPercent", System.Type.GetType("System.String"));
                DataColumn colAcceptOrNot = new DataColumn("acceptOrNot", System.Type.GetType("System.String"));
                DataColumn colDone = new DataColumn("Done", System.Type.GetType("System.String"));
                taskTable.Columns.Add(colCustMaxPay);
                taskTable.Columns.Add(colSelfPay);
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
                    dr["custMaxPayPercent"] = dr["custMaxPay"].ToString() + strPercent;
                    dr["selfPayPercent"] = dr["payPercent"].ToString() + strPercent;
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

                Session["PaymentApplyProcess"] = payApplyView;
                Session["dtSources"] = taskTable;

                selfPaymentGV.DataSource = Session["dtSources"];
                selfPaymentGV.DataBind();
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

        protected void selfPaymentGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void selfPaymentGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void selfPaymentGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}