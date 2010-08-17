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
    public partial class receiptExamine : System.Web.UI.Page
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
                DataRow sessionDr = Session["seldSelfReceipt"] as DataRow;

                string strMainContractId = sessionDr["mainContractId"].ToString();

                #region dr
                DataSet MyDst = new DataSet();
                MainContractProcess mainContractView = new MainContractProcess(MyDst);

                mainContractView.RealmainContractProjectView();
                DataTable taskTable = mainContractView.MyDst.Tables["view_mainContract_project_usr"].DefaultView.ToTable();

                string strFilter =
                    " mainContractId = " + "'" + strMainContractId + "'";
                taskTable.DefaultView.RowFilter = strFilter;

                DataRow dr = taskTable.DefaultView.ToTable().Rows[0];
                #endregion

                #region context_input
                lblProjectTag.Text = dr["projectTag"].ToString();
                lblMainContractTag.Text = dr["mainContractTag"].ToString();
                lblCust.Text = dr["contractCompName"].ToString();
                lblMainContractMoney.Text = dr["cash"].ToString();
                lblMainContractDateLine.Text = dr["dateLine"].ToString();
                lblMainContractPayment.Text = dr["paymentMode"].ToString();

                lblCustReceiptMax.Text = sessionDr["custMaxReceiptPercent"].ToString();
                lblSelfReceipt.Text = sessionDr["selfReceiptPercent"].ToString();
                txtReceiptExplication.Text = sessionDr["receiptExplication"].ToString();
                string strDone = sessionDr["Done"].ToString();
                lblDone.Text = strDone;
                txtReceiptComment.Text = sessionDr["receiptComment"].ToString();

                if (sessionDr["isAccept"].ToString().Equals("unExamine"))
                {
                    btnOK.Visible = true;
                    btnNo.Visible = true;
                }
                #endregion
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                DataRow sessionDr = Session["seldSelfReceipt"] as DataRow;

                string strReceiptId = sessionDr["receiptId"].ToString();
                string strOk = bool.TrueString;
                string strReceiptComment = txtReceiptComment.Text.Trim();

                DataSet dst = new DataSet();
                ReceiptApplyProcess rap = new ReceiptApplyProcess(dst);

                rap.SelfReceiptExamine(strReceiptId, strOk, strReceiptComment);

                Response.Redirect("~/Main/infoViewManager/receiptView.aspx");
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                DataRow sessionDr = Session["seldSelfReceipt"] as DataRow;

                string strReceiptId = sessionDr["receiptId"].ToString();
                string strOk = bool.FalseString;
                string strReceiptComment = txtReceiptComment.Text.Trim();

                DataSet dst = new DataSet();
                ReceiptApplyProcess rap = new ReceiptApplyProcess(dst);

                rap.SelfReceiptExamine(strReceiptId, strOk, strReceiptComment);

                Response.Redirect("~/Main/infoViewManager/receiptView.aspx");
            }
        }

        protected void btnRtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/infoViewManager/receiptView.aspx");
        }

        protected void txtReceiptComment_TextChanged(object sender, EventArgs e)
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

        protected bool inputCheck()
        {
            bool flag = true;

            flag = txtNullOrLenth_Check(txtReceiptComment);

            return flag;
        }
    }
}