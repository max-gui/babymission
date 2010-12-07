using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.infoViewManager
{
    public partial class receiptExamine : System.Web.UI.Page
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
                lblCust.Text = dr["custCompName"].ToString();
                lblMainContractMoney.Text = dr["cash"].ToString();
                lblMainContractDateLine.Text = dr["dateLine"].ToString();
                lblMainContractPayment.Text = dr["paymentMode"].ToString();

                lblCustReceiptMax.Text = float.Parse(sessionDr["custMaxReceipt"].ToString()).ToString("p");
                lblSelfToReceipt.Text = float.Parse(sessionDr["toReceiptCash"].ToString()).ToString("c");
                lblSelfHasReceipt.Text = float.Parse(sessionDr["hasReceiptPercent"].ToString()).ToString("p");
                lblSelfTotleReceipt.Text = float.Parse(sessionDr["receiptPercent"].ToString()).ToString("p");
                txtReceiptExplication.Text = sessionDr["receiptExplication"].ToString();
                string strDone = sessionDr["Done"].ToString();
                lblDone.Text = strDone;
                txtReceiptComment.Text = sessionDr["receiptComment"].ToString();
                
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
                DataRow sessionDr = Session["seldSelfReceipt"] as DataRow;

                string strReceiptId = sessionDr["receiptId"].ToString();
                string strReceiptComment = txtReceiptComment.Text.Trim();

                DataSet dst = new DataSet();
                ReceiptApplyProcess rap = new ReceiptApplyProcess(dst);

                rap.SelfReceiptExamine(strReceiptId, okOrNot, strReceiptComment);

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
                            "开票申请已通过审批，请尽快完成后续工作" + System.Environment.NewLine + Request.Url.toNewUrlForMail("/Main/paymentReceiptManager/receiptOk.aspx"));
                    }

                    BeckSendMail.getMM().NewMail(usrInfo.First().UsrEmail, 
                        "mis系统票务通知", 
                        projetTag + "的开票申请已通过审批，请尽快完成后续工作");
                }
                else
                {
                    BeckSendMail.getMM().NewMail(usrInfo.First().UsrEmail, 
                        "mis系统票务通知", 
                        projetTag + "的开票申请暂缓，请尽快完成后续工作");
                }
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

            flag = txtNullOrLenth_Check(txtReceiptComment);

            return flag;
        }
    }
}