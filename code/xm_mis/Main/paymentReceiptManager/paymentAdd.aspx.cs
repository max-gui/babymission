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
    public partial class paymentAdd : System.Web.UI.Page
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

            if (null == Session["seldSubContract"])
            {
                Response.Redirect("~/Main/paymentReceiptManager/subContractPaymentView.aspx");
            }

            if (!IsPostBack)
            {
                DataRow sessionDr = Session["seldSubContract"] as DataRow;

                string mainContractId = sessionDr["mainContractId"].ToString();
                string subContractId = sessionDr["subContractId"].ToString();

                DataTable paymentApplyTable = null;
                if (null == Session["payApplyTable"])
                {
                    DataRow receiptApplyRow = null;

                    DataColumn colSubContractId = new DataColumn("subContractId", System.Type.GetType("System.String"));
                    DataColumn colCustMaxPay = new DataColumn("custMaxPay", System.Type.GetType("System.String"));
                    DataColumn colPayPercent = new DataColumn("payPercent", System.Type.GetType("System.String"));
                    DataColumn colPayExplication = new DataColumn("paymentExplication", System.Type.GetType("System.String"));

                    paymentApplyTable = new DataTable("tbl_paymentApply");

                    paymentApplyTable.Columns.Add(colSubContractId);
                    paymentApplyTable.Columns.Add(colCustMaxPay);
                    paymentApplyTable.Columns.Add(colPayPercent);
                    paymentApplyTable.Columns.Add(colPayExplication);

                    receiptApplyRow = paymentApplyTable.NewRow();
                    receiptApplyRow["subContractId"] = subContractId;
                    receiptApplyRow["payPercent"] = string.Empty;
                    receiptApplyRow["paymentExplication"] = string.Empty;
                    paymentApplyTable.Rows.Add(receiptApplyRow);

                    Session["payApplyTable"] = paymentApplyTable;
                }
                else
                {
                    paymentApplyTable = Session["payApplyTable"] as DataTable;
                }

                string selfReceiving = sessionDr["receivingPercent"].ToString();

                DataSet MyDst = new DataSet();

                #region lblSubContractReceipt
                PaymentApplyProcess payApplyView = new PaymentApplyProcess(MyDst);

                payApplyView.MainContractPayMax(mainContractId);

                string maxPay = payApplyView.StrRtn;

                paymentApplyTable.Rows[0]["custMaxPay"] = maxPay;
                lblMainContractPay.Text = maxPay + "%";
                #endregion

                #region ddlSelfReceipt
                ListItemCollection licNomal = new ListItemCollection();

                int num = int.Parse(selfReceiving);
                int max = int.Parse(maxPay) + 10;
                string strValue = string.Empty;
                string strPercent = "%";
                string strText = string.Empty;
                for (int i = num; i < max; i = i + 10)
                {
                    strValue = i.ToString();

                    strText = strValue + strPercent;

                    licNomal.Add(strText);
                }

                ListItemCollection licSpecial = new ListItemCollection();
                strValue = string.Empty;
                strText = string.Empty;
                for (int i = max; i < 110; i = i + 10)
                {
                    strValue = i.ToString();

                    strText = strValue + strPercent;

                    licSpecial.Add(strText);
                }

                Session["licNomal"] = licNomal;
                Session["licSpecial"] = licSpecial;
                #endregion

                ddlSelfPay.DataSource = Session["licNomal"];
                ddlSelfPay.DataBind();
                ddlSelfPay.SelectedIndex = 0;

                Session["PaymentApplyProcess"] = payApplyView;
            }
        }

        protected DataTable getInput()
        {
            char[] charsToTrim = { '%' };
            string selfNewPay = ddlSelfPay.SelectedValue.TrimEnd(charsToTrim);
            string receiptExplication = txtPayExplication.Text.ToString().Trim();

            DataTable payApplyTable = Session["payApplyTable"] as DataTable;

            payApplyTable.Rows[0]["payPercent"] = selfNewPay;
            payApplyTable.Rows[0]["paymentExplication"] = receiptExplication;

            return payApplyTable;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                DataTable payApplyTable = getInput();

                PaymentApplyProcess pap = Session["PaymentApplyProcess"] as PaymentApplyProcess;

                pap.MyDst.Tables.Add(payApplyTable);
                pap.Add();

                Response.Redirect("~/Main/paymentReceiptManager/paymentApply.aspx");
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/paymentReceiptManager/paymentApply.aspx");
        }

        protected void btnSpecial_Click(object sender, EventArgs e)
        {
            ddlSelfPay.DataSource = Session["licSpecial"];
            ddlSelfPay.DataBind();
            ddlSelfPay.SelectedIndex = 0;

            btnSpecial.Visible = false;
            btnNomal.Visible = true;
        }

        protected void btnNomal_Click(object sender, EventArgs e)
        {
            ddlSelfPay.DataSource = Session["licNomal"];
            ddlSelfPay.DataBind();
            ddlSelfPay.SelectedIndex = 0;

            btnSpecial.Visible = true;
            btnNomal.Visible = false;
        }

        protected void txtPayExplication_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBx = sender as TextBox;
            txtNullOrLenth_Check(txtBx);
        }

        protected bool txtNullOrLenth_Check(TextBox txtBx)
        {
            bool flag = true;

            string strTxt = txtBx.Text.ToString().Trim();
            if (string.IsNullOrWhiteSpace(strTxt))
            {
                txtBx.Text = "不能为空！";
                flag = false;
            }
            else if (strTxt.Length > 25)
            {
                txtBx.Text = "不能超过25个字！";
                flag = false;
            }
            else if (strTxt.Equals("不能为空！"))
            {
                txtBx.Text = "不能为空！  ";
                flag = false;
            }
            else if (strTxt.Equals("不能超过25个字！"))
            {
                txtBx.Text = "不能超过25个字！  ";
                flag = false;
            }

            return flag;
        }

        protected bool ddlSelfPay_Check()
        {
            bool flag = true;

            if (ddlSelfPay.SelectedValue.Equals("0%"))
            {
                flag = false;
            }
            else
            {
            }

            return flag;
        }

        protected bool inputCheck()
        {
            bool flag = true;

            flag = txtNullOrLenth_Check(txtPayExplication)
                && ddlSelfPay_Check();

            return flag;
        }
    }
}