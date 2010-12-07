using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.paymentReceiptManager
{
    public partial class receiptEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.pay_receiptApply);
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

            if (null == Session["seldMainContract"])
            {
                Response.Redirect("~/Main/paymentReceiptManager/mainContractReceiptView.aspx");
            }

            if (!IsPostBack)
            {
                //DataRow sessionDr = Session["seldMainContract"] as DataRow;

                //System.Nullable<int> mainContractId = int.Parse(sessionDr["mainContractId"].ToString());

                //string selfReceipt = sessionDr["selfReceiptPercent"].ToString();

                int receiptId = int.Parse(Session["receiptId"] as string);

                Xm_db xmDataCont = Xm_db.GetInstance();

                var receiptApplyEdit =
                    (from receiptApply in xmDataCont.Tbl_receiptApply
                     where receiptApply.ReceiptId == receiptId
                     select receiptApply).First();

                float selfReceipt = receiptApplyEdit.HasReceiptPercent;
                System.Nullable<int> mainContractId = receiptApplyEdit.MainContractId;
                System.Nullable<float> maxRtn = 0;
                int rtn = xmDataCont.SubContract_MaxReceipt(mainContractId, ref maxRtn);

                lblSubContractReceipt.Text = maxRtn.Value.ToString("p");

                lblMainContractReceipt.Text = selfReceipt.ToString("p");

                //#region ddlSelfReceipt
                //ListItemCollection licNomal = new ListItemCollection();
                //int num = 0;
                //int max = int.Parse(maxReceipt) - int.Parse(selfReceipt) + 5;
                //string strValue = string.Empty;
                //string strPercent = "%";
                //string strText = string.Empty;
                //for (int i = num; i < max; i = i + 5)
                //{
                //    strValue = i.ToString();

                //    strText = strValue + strPercent;

                //    licNomal.Add(strText);
                //}

                //ListItemCollection licSpecial = new ListItemCollection();
                //num = max;
                //max = 110 - max;
                //strValue = string.Empty;
                //strText = string.Empty;
                //for (int i = num; i < max; i = i + 5)
                //{
                //    strValue = i.ToString();

                //    strText = strValue + strPercent;

                //    licSpecial.Add(strText);
                //}

                //Session["licNomal"] = licNomal;
                //Session["licSpecial"] = licSpecial;
                //#endregion

                Session["maxReceipt"] = maxRtn.Value;

                #region ddlSelfReceipt
                DataTable dtNomal = new DataTable();
                dtNomal.Columns.Add("DataTextField", Type.GetType("System.String"));
                dtNomal.Columns.Add("DataValueField", Type.GetType("System.Single"));

                float num = 0;
                float max = maxRtn.Value - selfReceipt + 0.05f;
                max = max <= 0.05f ? 0.05f : max;
                DataRow dr = null;
                for (float i = num; i < max; i = i + 0.05f)
                {
                    dr = dtNomal.NewRow();
                    dr["DataTextField"] = i.ToString("p");
                    dr["DataValueField"] = i;
                    dtNomal.Rows.Add(dr);
                }

                DataTable dtSpecial = new DataTable();
                dtSpecial.Columns.Add("DataTextField", Type.GetType("System.String"));
                dtSpecial.Columns.Add("DataValueField", Type.GetType("System.Single"));

                num = 0;
                max = 1.05f - selfReceipt;
                dr = null;
                for (float i = num; i < max; i = i + 0.05f)
                {
                    dr = dtSpecial.NewRow();
                    dr["DataTextField"] = i.ToString("p");
                    dr["DataValueField"] = i;
                    dtSpecial.Rows.Add(dr);
                    //licNomal.Add(new ListItem(i.ToString("p"), i.ToString()));
                }

                Session["dtNomal"] = dtNomal;
                Session["dtSpecial"] = dtSpecial;
                #endregion

                ddlSelfReceipt.DataSource = Session["dtNomal"];
                ddlSelfReceipt.DataValueField = "DataValueField";
                ddlSelfReceipt.DataTextField = "DataTextField";

                ddlSelfReceipt.DataBind();
                ddlSelfReceipt.SelectedIndex = 0;

                txtReceiptExplication.Text = receiptApplyEdit.ReceiptExplication;
            }
        }

        protected void btnSpecial_Click(object sender, EventArgs e)
        {
            ddlSelfReceiptInit("dtSpecial", btnNomal, btnSpecial);
        }

        protected void btnNomal_Click(object sender, EventArgs e)
        {
            ddlSelfReceiptInit("dtNomal", btnSpecial, btnNomal);
        }

        private void ddlSelfReceiptInit(string dtSourcesStr, Button seeBtn, Button unSeeBtn)
        {

            ddlSelfReceipt.DataSource = Session["dtSourcesStr"];
            ddlSelfReceipt.DataValueField = "DataValueField";
            ddlSelfReceipt.DataTextField = "DataTextField";
            ddlSelfReceipt.DataBind();
            ddlSelfReceipt.SelectedIndex = 0;

            seeBtn.Visible = true;
            unSeeBtn.Visible = false;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {                
                int receiptId = int.Parse(Session["receiptId"] as string);

                Xm_db xmDataCont = Xm_db.GetInstance();

                var receiptApplyEdit =
                    (from receiptApply in xmDataCont.Tbl_receiptApply
                     where receiptApply.ReceiptId == receiptId
                     select receiptApply).First();

                string seldReceipt = ddlSelfReceipt.SelectedValue;
                float selfNewReceipt = float.Parse(seldReceipt);
                float selfHasReceipt = receiptApplyEdit.HasReceiptPercent;
                float receiptPercent = selfNewReceipt + selfHasReceipt;

                //paymentApplyEdit.CustMaxPay = float.Parse(Session["maxPay"].ToString());
                //paymentApplyEdit.PaymentExplication = txtPayExplication.Text.ToString().Trim();
                //paymentApplyEdit.PayPercent = payPercent;

                string num = receiptPercent.ToString("p");
                //char[] charsToTrim = { '%' };
                //int selfNewReceipt = int.Parse(ddlSelfReceipt.SelectedValue.TrimEnd(charsToTrim));
                //int selfHasReceipt = int.Parse(lblMainContractReceipt.Text.TrimEnd(charsToTrim));

                receiptApplyEdit.CustMaxReceipt = float.Parse(Session["maxReceipt"].ToString());
                receiptApplyEdit.ReceiptExplication = txtReceiptExplication.Text.ToString().Trim();
                receiptApplyEdit.ReceiptPercent = receiptPercent;

                try
                {
                    xmDataCont.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);

                    applyOk(num, xmDataCont);
                }
                catch (System.Data.Linq.ChangeConflictException cce)
                {
                    string strEx = cce.Message;
                    foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataCont.ChangeConflicts)
                    {
                        occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                    }

                    xmDataCont.SubmitChanges();

                    applyOk(num, xmDataCont);
                }
            }
        }

        private void applyOk(string num, Xm_db xmDataCont)
        {
            //int flag = 0x2000;
            var usr_autority =
                from usr in xmDataCont.Tbl_usr
                //join auth in xmDataCont.View_usr_autority on usr.UsrId equals auth.UsrId
                where (usr.TotleAuthority & (UInt32)AuthAttributes.pay_receiptExamine) != 0 &&
                      usr.EndTime > DateTime.Now
                select usr;
                //where usr.TotleAuthority.ToAuthAttr().HasOneFlag(AuthAttributes.pay_receiptExamine) &&
                //      usr.EndTime > DateTime.Now
                //select usr;

            foreach (var usr in usr_autority)
            {
                BeckSendMail.getMM().NewMail(usr.UsrEmail, 
                    "mis系统票务审批通知",
                    "申请开票" + num + System.Environment.NewLine + Request.Url.toNewUrlForMail("/Main/infoViewManager/receiptView.aspx"));
            }

            Response.Redirect("~/Main/paymentReceiptManager/receiptApply.aspx");
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

        protected bool ddlSelfReceipt_Check()
        {
            bool flag = true;

            float f0 = 0;
            if (ddlSelfReceipt.SelectedValue.Equals(f0.ToString()))
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