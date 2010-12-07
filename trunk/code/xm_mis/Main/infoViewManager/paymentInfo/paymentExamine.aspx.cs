using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.infoViewManager.paymentInfo
{
    public partial class paymentExamine : System.Web.UI.Page
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

                lblCustPayMax.Text = float.Parse(sessionDr["custMaxPay"].ToString()).ToString("p");
                lblSelfToPay.Text = float.Parse(sessionDr["toPayCash"].ToString()).ToString("c");
                lblSelfHasPay.Text = float.Parse(sessionDr["hasPayPercent"].ToString()).ToString("p");
                lblSelfTotlePay.Text = float.Parse(sessionDr["payPercent"].ToString()).ToString("p");
                txtPayExplication.Text = sessionDr["paymentExplication"].ToString();
                string strDone = sessionDr["Done"].ToString();
                lblDone.Text = strDone;
                txtPayComment.Text = sessionDr["paymentComment"].ToString();

                DateTime doneTime = (DateTime)dr["doneTime"];
                if (doneTime > DateTime.Now)
                {
                    btnOK.Visible = true;
                    btnNo.Visible = true;
                }
                #endregion
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            string strOk = bool.TrueString;
            examine(strOk);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            string strNot = bool.FalseString;
            examine(strNot);
        }

        private void examine(string okOrNot)
        {
            if (inputCheck())
            {
                DataRow sessionDr = Session["seldSelfPayment"] as DataRow;

                string strPayId = sessionDr["paymentId"].ToString();
                string strPayComment = txtPayComment.Text.Trim();

                DataSet dst = new DataSet();
                PaymentApplyProcess pap = new PaymentApplyProcess(dst);

                pap.SelfPaymentExamine(strPayId, okOrNot, strPayComment);

                Xm_db xmDataCont = Xm_db.GetInstance();

                int usrId = int.Parse(sessionDr["usrId"].ToString());
                var usrInfo =
                    from usr in xmDataCont.Tbl_usr
                    where usr.UsrId == usrId &&
                          usr.EndTime > DateTime.Now
                    select usr;

                string projetTag = sessionDr["projectTag"].ToString();

                if (okOrNot.Equals(bool.TrueString))
                {
                    //int flag = 0x80;
                    var usr_autority =
                        from usr in xmDataCont.Tbl_usr
                        //join auth in xmDataCont.View_usr_autority on usr.UsrId equals auth.UsrId
                        where (usr.TotleAuthority & (UInt32)AuthAttributes.pay_receiptOk) != 0 &&
                              usr.EndTime > DateTime.Now
                        select usr;
                        //where usr.TotleAuthority.ToAuthAttr().HasOneFlag(AuthAttributes.pay_receiptOk) &&
                        //      usr.EndTime > DateTime.Now
                        //select usr;

                    foreach (var usr in usr_autority)
                    {
                        BeckSendMail.getMM().NewMail(usr.UsrEmail, 
                            "mis系统票务通知", 
                            "付款申请已通过审批，请尽快完成后续工作" + System.Environment.NewLine + Request.Url.toNewUrlForMail("/Main/paymentReceiptManager/paymentOk.aspx"));
                    }

                    BeckSendMail.getMM().NewMail(usrInfo.First().UsrEmail, 
                        "mis系统票务通知", 
                        projetTag + "的付款申请已通过审批，请尽快完成后续工作");
                }
                else
                {
                    BeckSendMail.getMM().NewMail(usrInfo.First().UsrEmail, "mis系统票务通知", projetTag + "的付款申请暂缓，请尽快完成后续工作");
                }

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
            else if (strTxt.Length > 50)
            {
                txtBx.Text = "不能超过50个字！";
                flag = false;
            }
            else if (strTxt.Equals("不能为空！"))
            {
                txtBx.Text = "不能为空！  ";
                flag = false;
            }
            else if (strTxt.Equals("不能超过50个字！"))
            {
                txtBx.Text = "不能超过50个字！  ";
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