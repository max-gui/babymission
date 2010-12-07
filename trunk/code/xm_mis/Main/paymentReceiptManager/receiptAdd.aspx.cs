using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
using System.Text;
namespace xm_mis.Main.paymentReceiptManager
{
    public partial class receiptAdd : System.Web.UI.Page
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
                DataRow sessionDr = Session["seldMainContract"] as DataRow;

                string mainContractId = sessionDr["mainContractId"].ToString();

                DataTable receiptApplyTable = null;
                if (null == Session["receiptApplyTable"])
                {
                    DataRow receiptApplyRow = null;

                    DataColumn colMainContractId = new DataColumn("mainContractId", System.Type.GetType("System.String"));
                    DataColumn colCustMaxReceipt = new DataColumn("custMaxReceipt", System.Type.GetType("System.String"));
                    DataColumn colReceiptPercent = new DataColumn("receiptPercent", System.Type.GetType("System.String"));
                    DataColumn colHasReceiptPercent = new DataColumn("selfHasReceipt", System.Type.GetType("System.String"));
                    DataColumn colReceiptExplication = new DataColumn("receiptExplication", System.Type.GetType("System.String"));

                    receiptApplyTable = new DataTable("tbl_receiptApply");

                    receiptApplyTable.Columns.Add(colMainContractId);
                    receiptApplyTable.Columns.Add(colCustMaxReceipt);
                    receiptApplyTable.Columns.Add(colReceiptPercent);
                    receiptApplyTable.Columns.Add(colHasReceiptPercent);
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

                #region lblContractReceipt
                ReceiptApplyProcess receiptApplyView = new ReceiptApplyProcess(MyDst);

                receiptApplyView.SubContractReceiptMax(mainContractId);

                string maxReceipt = receiptApplyView.StrRtn;

                //receiptApplyTable.Rows[0]["custMaxReceipt"] = maxReceipt;
                //lblSubContractReceipt.Text = maxReceipt + "%";

                //lblMainContractReceipt.Text = selfReceipt + "%";
                //#endregion

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

                //ddlSelfReceipt.DataSource = Session["licNomal"];
                //ddlSelfReceipt.DataBind();
                //ddlSelfReceipt.SelectedIndex = 0;

                float m = float.Parse(maxReceipt);
                float s = float.Parse(selfReceipt);
                receiptApplyTable.Rows[0]["custMaxReceipt"] = maxReceipt;
                lblSubContractReceipt.Text = m.ToString("p");
                lblMainContractReceipt.Text = s.ToString("p");
                #endregion

                #region ddlSelfReceipt
                DataTable dtNomal = new DataTable();
                dtNomal.Columns.Add("DataTextField", Type.GetType("System.String"));
                dtNomal.Columns.Add("DataValueField", Type.GetType("System.Single"));

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
                dtSpecial.Columns.Add("DataTextField", Type.GetType("System.String"));
                dtSpecial.Columns.Add("DataValueField", Type.GetType("System.Single"));

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

                ddlSelfReceipt.DataSource = Session["dtNomal"];
                ddlSelfReceipt.DataValueField = "DataValueField";
                ddlSelfReceipt.DataTextField = "DataTextField";

                ddlSelfReceipt.DataBind();
                ddlSelfReceipt.SelectedIndex = 0;
                
                Session["ReceiptApplyProcess"] = receiptApplyView;
            }
        }

        protected DataTable getInput()
        {
            DataRow sessionDr = Session["seldMainContract"] as DataRow;
            string selfReceipt = sessionDr["selfReceiptPercent"].ToString();
            float selfNewReceipt = float.Parse(ddlSelfReceipt.SelectedValue);
            float selfHasReceipt = float.Parse(selfReceipt);
            float selfToReceipt = selfNewReceipt + selfHasReceipt;
            string receiptExplication = txtReceiptExplication.Text.ToString().Trim();

            DataTable receiptApplyTable = Session["receiptApplyTable"] as DataTable;

            receiptApplyTable.Rows[0]["receiptPercent"] = selfToReceipt;
            receiptApplyTable.Rows[0]["selfHasReceipt"] = selfHasReceipt;
            receiptApplyTable.Rows[0]["receiptExplication"] = receiptExplication;

            return receiptApplyTable;
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

            ddlSelfReceipt.DataSource = Session[dtSourcesStr];
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
                DataTable receiptApplyTable = getInput();

                //PaymentApplyProcess pap = Session["PaymentApplyProcess"] as PaymentApplyProcess;

                //pap.MyDst.Tables.Add(payApplyTable);
                //pap.Add();

                System.Nullable<int> mainContractId = int.Parse(receiptApplyTable.Rows[0]["mainContractId"].ToString());
                System.Nullable<float> custMaxReceipt = float.Parse(receiptApplyTable.Rows[0]["custMaxReceipt"].ToString());
                System.Nullable<float> selfToReceipt = float.Parse(receiptApplyTable.Rows[0]["receiptPercent"].ToString());
                System.Nullable<float> hasReceiptPercent = float.Parse(receiptApplyTable.Rows[0]["selfHasReceipt"].ToString());
                string receiptExplication = receiptApplyTable.Rows[0]["receiptExplication"].ToString();
                System.Nullable<int> intRef = 0;

                string num = float.Parse(receiptApplyTable.Rows[0]["receiptPercent"].ToString()).ToString("p");

                Xm_db xmDataCont = Xm_db.GetInstance();

                try
                {
                    xmDataCont.Tbl_receiptApply_Insert(mainContractId, custMaxReceipt, selfToReceipt, hasReceiptPercent, receiptExplication, ref intRef);

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

                    xmDataCont.Tbl_receiptApply_Insert(mainContractId, custMaxReceipt, selfToReceipt, hasReceiptPercent, receiptExplication, ref intRef);

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