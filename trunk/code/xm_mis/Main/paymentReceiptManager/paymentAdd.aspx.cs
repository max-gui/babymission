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
    public partial class paymentAdd : System.Web.UI.Page
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
                    DataColumn colHasPayPercent = new DataColumn("selfHasPay", System.Type.GetType("System.String"));
                    DataColumn colPayExplication = new DataColumn("paymentExplication", System.Type.GetType("System.String"));

                    paymentApplyTable = new DataTable("tbl_paymentApply");

                    paymentApplyTable.Columns.Add(colSubContractId);
                    paymentApplyTable.Columns.Add(colCustMaxPay);
                    paymentApplyTable.Columns.Add(colPayPercent);
                    paymentApplyTable.Columns.Add(colHasPayPercent);
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

                #region lblContractPay
                PaymentApplyProcess payApplyView = new PaymentApplyProcess(MyDst);

                payApplyView.MainContractPayMax(mainContractId);

                string maxPay = payApplyView.StrRtn;
                //float m = float.Parse(maxPay);
                //paymentApplyTable.Rows[0]["custMaxPay"] = maxPay;
                //lblMainContractPay.Text = maxPay + "%";
                //lblSubContractPay.Text = selfReceiving + "%";
                //#endregion

                //#region ddlSelfReceipt
                //ListItemCollection licNomal = new ListItemCollection();

                //int num = 0;
                //int max = int.Parse(maxPay) - int.Parse(selfReceiving) + 5;
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
                //for (int i = max; i < max; i = i + 5)
                //{
                //    strValue = i.ToString();

                //    strText = strValue + strPercent;

                //    licSpecial.Add(strText);
                //}

                //Session["licNomal"] = licNomal;
                //Session["licSpecial"] = licSpecial;
                //#endregion

                //ddlSelfPay.DataSource = Session["licNomal"];
                //ddlSelfPay.DataBind();
                //ddlSelfPay.SelectedIndex = 0;

                //Session["PaymentApplyProcess"] = payApplyView;

                float m = float.Parse(maxPay);
                float s = float.Parse(selfReceiving);
                paymentApplyTable.Rows[0]["custMaxPay"] = maxPay;
                lblMainContractPay.Text = m.ToString("p");
                lblSubContractPay.Text = s.ToString("p");
                #endregion

                #region ddlSelfReceipt
                DataTable dtNomal = new DataTable();
                dtNomal.Columns.Add("DataTextField",Type.GetType("System.String"));
                dtNomal.Columns.Add("DataValueField",Type.GetType("System.Single"));

                float num = 0;
                float max = m - s + 0.05f;
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
                dtSpecial.Columns.Add("DataTextField",Type.GetType("System.String"));
                dtSpecial.Columns.Add("DataValueField",Type.GetType("System.Single"));
                
                num = 0;
                max = 1.05f - s;
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

                ddlSelfPay.DataSource = Session["dtNomal"];
                ddlSelfPay.DataValueField = "DataValueField";
                ddlSelfPay.DataTextField = "DataTextField";
                
                ddlSelfPay.DataBind();
                ddlSelfPay.SelectedIndex = 0;

                Session["PaymentApplyProcess"] = payApplyView;
            }
        }

        protected DataTable getInput()
        {
            DataRow sessionDr = Session["seldSubContract"] as DataRow;
            string selfReceiving = sessionDr["receivingPercent"].ToString();
            float selfNewPay = float.Parse(ddlSelfPay.SelectedValue);
            float selfHasPay = float.Parse(selfReceiving);
            float selfToPay = selfNewPay + selfHasPay;
            string receiptExplication = txtPayExplication.Text.ToString().Trim();

            DataTable payApplyTable = Session["payApplyTable"] as DataTable;

            payApplyTable.Rows[0]["payPercent"] = selfToPay;
            payApplyTable.Rows[0]["selfHasPay"] = selfHasPay;
            payApplyTable.Rows[0]["paymentExplication"] = receiptExplication;

            return payApplyTable;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                DataTable payApplyTable = getInput();

                //PaymentApplyProcess pap = Session["PaymentApplyProcess"] as PaymentApplyProcess;

                //pap.MyDst.Tables.Add(payApplyTable);
                //pap.Add();

                System.Nullable<int> subContractId = int.Parse(payApplyTable.Rows[0]["subContractId"].ToString());
                System.Nullable<float> custMaxPay = float.Parse(payApplyTable.Rows[0]["custMaxPay"].ToString());
                System.Nullable<float> selfToPay = float.Parse(payApplyTable.Rows[0]["payPercent"].ToString());
                System.Nullable<float> hasPayPercent = float.Parse(payApplyTable.Rows[0]["selfHasPay"].ToString());
                string paymentExplication = payApplyTable.Rows[0]["paymentExplication"].ToString();
                System.Nullable<int> intRef = 0;

                string num = float.Parse(payApplyTable.Rows[0]["payPercent"].ToString()).ToString("p");

                Xm_db xmDataCont = Xm_db.GetInstance();

                try
                {
                    xmDataCont.Tbl_paymentApply_Insert(subContractId, custMaxPay, selfToPay, hasPayPercent, paymentExplication, ref intRef);

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

                    xmDataCont.Tbl_paymentApply_Insert(subContractId, custMaxPay, selfToPay, hasPayPercent, paymentExplication, ref intRef);

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
                    "申请付款" + num + System.Environment.NewLine + Request.Url.toNewUrlForMail("/Main/infoViewManager/paymentInfo/paymentView.aspx"));
            }

            Response.Redirect("~/Main/paymentReceiptManager/paymentApply.aspx");
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/paymentReceiptManager/paymentApply.aspx");
        }

        protected void btnSpecial_Click(object sender, EventArgs e)
        {
            string dtSourcesStr = "dtSpecial";
            ddlSelfPayInit(dtSourcesStr, btnNomal, btnSpecial);            
        }

        protected void btnNomal_Click(object sender, EventArgs e)
        {
            string dtSourcesStr = "dtNomal";
            ddlSelfPayInit(dtSourcesStr, btnSpecial, btnNomal);
        }

        private void ddlSelfPayInit(string dtSourcesStr, Button seeBtn, Button unSeeBtn)
        {

            ddlSelfPay.DataSource = Session[dtSourcesStr];
            ddlSelfPay.DataValueField = "DataValueField";
            ddlSelfPay.DataTextField = "DataTextField";
            ddlSelfPay.DataBind();
            ddlSelfPay.SelectedIndex = 0;
            
            seeBtn.Visible = true;
            unSeeBtn.Visible = false;
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

        protected bool ddlSelfPay_Check()
        {
            bool flag = true;

            float f0 = 0;
            if (ddlSelfPay.SelectedValue.Equals(f0.ToString()))
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