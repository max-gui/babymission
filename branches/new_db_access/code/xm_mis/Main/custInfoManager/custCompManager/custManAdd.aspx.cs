using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.custInfoManager.custCompManager
{
    public partial class custManAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                int usrAuth = 0;
                string strUsrAuth = Session["totleAuthority"].ToString();
                usrAuth = int.Parse(strUsrAuth);
                int flag = 0x3 << 4;

                if ((usrAuth & flag) == 0)
                    Response.Redirect("~/Main/NoAuthority.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                DataSet custCompDst = new DataSet();

                custCompProcess custCompView = new custCompProcess(custCompDst);

                custCompView.RealCompView();

                DataTable custCompTable = custCompView.MyDst.Tables["tbl_customer_company"];

                ddlCustComp.DataSource = custCompTable;
                ddlCustComp.DataTextField = "custCompName";
                ddlCustComp.DataValueField = "custCompyId";

                ddlCustComp.DataBind();
            }            
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                string cmn = txtName.Text.ToString().Trim();
                string cmd = txtDep.Text.ToString().Trim();
                string cmt = txtTitle.Text.ToString().Trim();
                string cmc = txtContact.Text.ToString().Trim();
                string cme = txtEmail.Text.ToString().Trim();
                string ccnId = ddlCustComp.SelectedValue.ToString().Trim();

                #region dataset
                DataSet dataSet = new DataSet();
                DataRow custManRow = null;

                DataColumn custManName = new DataColumn("custManName", System.Type.GetType("System.String"));
                DataColumn custManDep = new DataColumn("custManDepart", System.Type.GetType("System.String"));
                DataColumn custManTitle = new DataColumn("custManTitle", System.Type.GetType("System.String"));
                DataColumn custManContact = new DataColumn("custManContact", System.Type.GetType("System.String"));
                DataColumn custManEmail = new DataColumn("custManEmail", System.Type.GetType("System.String"));
                DataColumn custCompId = new DataColumn("custCompyId", System.Type.GetType("System.String"));

                DataTable compTable = new DataTable("tbl_customer_manager");

                compTable.Columns.Add(custManName);
                compTable.Columns.Add(custManDep);
                compTable.Columns.Add(custManTitle);
                compTable.Columns.Add(custManContact);
                compTable.Columns.Add(custManEmail);
                compTable.Columns.Add(custCompId);

                custManRow = compTable.NewRow();
                custManRow["custManName"] = cmn;
                custManRow["custManDepart"] = cmd;
                custManRow["custManTitle"] = cmt;
                custManRow["custManContact"] = cmc;
                custManRow["custManEmail"] = cme;
                custManRow["custCompyId"] = ccnId;
                compTable.Rows.Add(custManRow);

                dataSet.Tables.Add(compTable);
                #endregion

                DataSet dsCheck = new DataSet();
                custManProcess cmp = new custManProcess(dataSet);

                cmp.Add();
                int rowRtn = -1;

                rowRtn = cmp.IntRtn;
                if (0 == rowRtn)
                {
                    rowRtn = cmp.IntRtn;

                    if (0 == rowRtn)
                    {
                        cmp.MyDst = dataSet;

                        cmp.Add();

                        string newCompId = cmp.StrRtn;

                        string continueUrl = "~/Main/DefaultMainSite.aspx";//Request.QueryString["ReturnUrl"];

                        Response.Redirect(continueUrl);
                    }
                    else
                    {
                        lblTitle.Text = "公司简称已存在!";
                    }
                }
                else
                {
                    lblName.Text = "公司名已存在!";
                }
            }
        }

        protected bool txtName_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtName.Text.ToString().Trim()))
            {
                lblName.Text = "*必填项!";
                flag = false;
            }
            else if (txtName.Text.ToString().Trim().Length > 20)
            {
                lblName.Text = "名字太长!";
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblName.Text = string.Empty;
                //Session["flagUsrName"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }
        protected bool txtDep_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtDep.Text.ToString().Trim()))
            {
                lblDep.Text = "*必填项!";
                flag = false;
            }
            else if (txtDep.Text.ToString().Trim().Length > 20)
            {
                lblDep.Text = "部门名称太长!";
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblDep.Text = string.Empty;
                //Session["flagUsrName"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }
        protected bool txtTitle_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtTitle.Text.ToString().Trim()))
            {
                lblTitle.Text = "*必填项!";
                flag = false;
            }
            else if (txtTitle.Text.ToString().Trim().Length > 10)
            {
                lblTitle.Text = "职位名称太长!";
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblTitle.Text = string.Empty;
                //Session["flagPassWord"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }
        protected bool txtContact_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtContact.Text.ToString().Trim()))
            {
                lblContact.Text = "*必填项!";
                //Session["flagContact"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else if (txtContact.Text.ToString().Trim().Length != 11)
            {
                lblContact.Text = "手机号码应为11位!";
                //Session["flagContact"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                try
                {
                    long sc = long.Parse(txtContact.Text.ToString().Trim());
                    lblContact.Text = string.Empty;
                    //Session["flagContact"] = bool.TrueString.ToString().Trim();
                }
                catch (FormatException e)
                {
                    lblContact.Text = "手机号码只能包含数字!";
                    //Session["flagContact"] = bool.FalseString.ToString().Trim();
                    Console.WriteLine("{0} Exception caught.", e);
                    flag = false;
                }
            } 
            
            return flag;
        }
        protected bool txtEmail_TextCheck()
        {
            string strLblEmail = txtEmail.Text.ToString().Trim();

            bool flag = true;
            if (string.IsNullOrWhiteSpace(strLblEmail))
            {
                lblEmail.Text = "*必填项!";
                flag = false;
            }
            else if (!strLblEmail.Contains("@") || strLblEmail.StartsWith("@") || strLblEmail.EndsWith("@"))
            {
                lblEmail.Text = "邮件格式不对!";
                flag = false;
            }
            else
            {
                lblEmail.Text = string.Empty;
            }

            return flag;
        }

        protected bool inputCheck()
        {
            bool flag = true;
            if (!txtName_TextCheck())
            {
                flag = false;
            }
            else if (!txtDep_TextCheck())
            {
                flag = false;
            }
            else if (!txtTitle_TextCheck())
            {
                flag = false;
            }

            return flag;
        }

        protected void ddlCustComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCustComp.Text = string.Empty;
        }
    }
}