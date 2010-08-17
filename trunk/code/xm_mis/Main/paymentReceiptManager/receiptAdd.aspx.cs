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
    public partial class receiptAdd : System.Web.UI.Page
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

                DataTable receiptApplyTable = null;
                if (null == Session["receiptApplyTable"])
                {
                    DataRow receiptApplyRow = null;

                    DataColumn colMainContractId = new DataColumn("mainContractId", System.Type.GetType("System.String"));
                    DataColumn colCustMaxReceipt = new DataColumn("custMaxReceipt", System.Type.GetType("System.String"));
                    DataColumn colReceiptPercent = new DataColumn("receiptPercent", System.Type.GetType("System.String"));
                    DataColumn colReceiptExplication = new DataColumn("receiptExplication", System.Type.GetType("System.String"));

                    receiptApplyTable = new DataTable("tbl_receiptApply");

                    receiptApplyTable.Columns.Add(colMainContractId);
                    receiptApplyTable.Columns.Add(colCustMaxReceipt);
                    receiptApplyTable.Columns.Add(colReceiptPercent);
                    receiptApplyTable.Columns.Add(colReceiptExplication);

                    receiptApplyRow = receiptApplyTable.NewRow();
                    receiptApplyRow["mainContractId"] = mainContractId;
                    receiptApplyRow["receiptPercent"] = string.Empty;
                    receiptApplyRow["receiptExplication"] = string.Empty;
                    receiptApplyTable.Rows.Add(receiptApplyRow);

                    Session["receiptApplyTable"] = receiptApplyTable;
                }
                else
                {
                    receiptApplyTable = Session["receiptApplyTable"] as DataTable;
                }

                string selfReceipt = sessionDr["selfReceiptPercent"].ToString();

                DataSet MyDst = new DataSet();

                #region lblSubContractReceipt
                ReceiptApplyProcess receiptApplyView = new ReceiptApplyProcess(MyDst);

                receiptApplyView.SubContractReceiptMax(mainContractId);

                string maxReceipt = receiptApplyView.StrRtn;

                receiptApplyTable.Rows[0]["custMaxReceipt"] = maxReceipt;
                lblSubContractReceipt.Text = maxReceipt + "%";
                #endregion

                #region ddlSelfReceipt
                ListItemCollection licNomal = new ListItemCollection();

                int num = int.Parse(selfReceipt);
                int max = int.Parse(maxReceipt) + 10;
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

                ddlSelfReceipt.DataSource = Session["licNomal"];
                ddlSelfReceipt.DataBind();
                ddlSelfReceipt.SelectedIndex = 0;

                Session["ReceiptApplyProcess"] = receiptApplyView;
            }
        }

        protected DataTable getInput()
        {
            char[] charsToTrim = { '%'};
            string selfNewReceipt = ddlSelfReceipt.SelectedValue.TrimEnd(charsToTrim);
            string receiptExplication = txtReceiptExplication.Text.ToString().Trim();

            DataTable receiptApplyTable = Session["receiptApplyTable"] as DataTable;

            receiptApplyTable.Rows[0]["receiptPercent"] = selfNewReceipt;
            receiptApplyTable.Rows[0]["receiptExplication"] = receiptExplication;

            return receiptApplyTable;
        }

        protected void btnSpecial_Click(object sender, EventArgs e)
        {
            ddlSelfReceipt.DataSource = Session["licSpecial"];
            ddlSelfReceipt.DataBind();
            ddlSelfReceipt.SelectedIndex = 0;

            btnSpecial.Visible = false;
            btnNomal.Visible = true;
        }

        protected void btnNomal_Click(object sender, EventArgs e)
        {
            ddlSelfReceipt.DataSource = Session["licNomal"];
            ddlSelfReceipt.DataBind();
            ddlSelfReceipt.SelectedIndex = 0;

            btnSpecial.Visible = true;
            btnNomal.Visible = false;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                DataTable receiptApplyTable = getInput();

                ReceiptApplyProcess rap = Session["ReceiptApplyProcess"] as ReceiptApplyProcess;

                rap.MyDst.Tables.Add(receiptApplyTable);
                rap.Add();

                Response.Redirect("~/Main/paymentReceiptManager/receiptApply.aspx");
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/paymentReceiptManager/receiptApply.aspx");
        }

        protected void txtReceiptExplication_TextChanged(object sender, EventArgs e)
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

        protected bool ddlSelfReceipt_Check()
        {
            bool flag = true;

            if (ddlSelfReceipt.SelectedValue.Equals("0%"))
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
                        
            flag = txtNullOrLenth_Check(txtReceiptExplication)
                && ddlSelfReceipt_Check();

            return flag;
        }
    }
}