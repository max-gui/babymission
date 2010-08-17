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
    public partial class paymentExamine : System.Web.UI.Page
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
                DataRow sessionDr = Session["seldSelfPayment"] as DataRow;

                string strSubContractId = sessionDr["subContractId"].ToString();

                #region dr
                DataSet MyDst = new DataSet();
                subContractProcess subContractView = new subContractProcess(MyDst);

                subContractView.FullSubContractInfo();
                DataTable taskTable = subContractView.MyDst.Tables["view_full_subContractInfo"].DefaultView.ToTable();

                string strFilter =
                    " subContractId = " + "'" + strSubContractId + "'";
                taskTable.DefaultView.RowFilter = strFilter;

                DataRow dr = taskTable.DefaultView.ToTable().Rows[0];
                #endregion

                #region context_input
                lblProjectTag.Text = dr["projectTag"].ToString();
                lblSubContractTag.Text = dr["subContractTag"].ToString();
                lblSupplier.Text = dr["supplierName"].ToString();
                lblSubContractMoney.Text = dr["cash"].ToString();
                lblSubContractDateLine.Text = dr["dateLine"].ToString();
                lblSubContractPayment.Text = dr["paymentMode"].ToString();

                lblCustPayMax.Text = sessionDr["custMaxPayPercent"].ToString();
                lblSelfPay.Text = sessionDr["selfPayPercent"].ToString();
                txtPayExplication.Text = sessionDr["paymentExplication"].ToString();
                string strDone = sessionDr["Done"].ToString();
                lblDone.Text = strDone;
                txtPayComment.Text = sessionDr["paymentComment"].ToString();

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
                DataRow sessionDr = Session["seldSelfPayment"] as DataRow;

                string strPayId = sessionDr["paymentId"].ToString();
                string strOk = bool.TrueString;
                string strPayComment = txtPayComment.Text.Trim();

                DataSet dst = new DataSet();
                PaymentApplyProcess pap = new PaymentApplyProcess(dst);

                pap.SelfPaymentExamine(strPayId, strOk, strPayComment);

                Response.Redirect("~/Main/infoViewManager/paymentInfo/paymentView.aspx");
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                DataRow sessionDr = Session["seldSelfPayment"] as DataRow;

                string strPayId = sessionDr["paymentId"].ToString();
                string strOk = bool.FalseString;
                string strPayComment = txtPayComment.Text.Trim();

                DataSet dst = new DataSet();
                PaymentApplyProcess pap = new PaymentApplyProcess(dst);

                pap.SelfPaymentExamine(strPayId, strOk, strPayComment);

                Response.Redirect("~/Main/infoViewManager/paymentInfo/paymentView.aspx");
            }
        }

        protected void btnRtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/infoViewManager/paymentInfo/paymentView.aspx");
        }

        protected void txtPayComment_TextChanged(object sender, EventArgs e)
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

            flag = txtNullOrLenth_Check(txtPayComment);

            return flag;
        }
    }
}