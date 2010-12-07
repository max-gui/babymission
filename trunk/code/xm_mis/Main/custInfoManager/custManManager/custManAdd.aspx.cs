using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.custInfoManager.custManManager
{
    public partial class custManAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.custManager);
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

            if (null == Session["selCustCompDr"])
            {
                Response.Redirect("~/Main/custInfoManager/custCompManager/custCompEditing.aspx");
            }
            
            if (!IsPostBack)
            {
                DataRow sessionDr = Session["selCustCompDr"] as DataRow;

                lblCustComp.Text = sessionDr["custCompName"].ToString();
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                DataRow sessionDr = Session["selCustCompDr"] as DataRow;
                string compId = sessionDr["custCompyId"].ToString().Trim();

                string cmn = txtName.Text.ToString().Trim();
                string cmd = txtDep.Text.ToString().Trim();
                string cmt = txtTitle.Text.ToString().Trim();
                string cmc = txtContact.Text.ToString().Trim();
                string cme = txtEmail.Text.ToString().Trim();
                string ccId = compId;

                #region dataset
                DataSet dataSet = new DataSet();
                DataRow custManRow = null;
                
                DataColumn custManName = new DataColumn("custManName", System.Type.GetType("System.String"));
                DataColumn custManDep = new DataColumn("custManDepart", System.Type.GetType("System.String"));
                DataColumn custManTitle = new DataColumn("custManTitle", System.Type.GetType("System.String"));
                DataColumn custManContact = new DataColumn("custManContact", System.Type.GetType("System.String"));
                DataColumn custManEmail = new DataColumn("custManEmail", System.Type.GetType("System.String"));
                DataColumn custCompId = new DataColumn("custCompyId", System.Type.GetType("System.String"));

                DataTable custCompTable = new DataTable("tbl_customer_manager");

                custCompTable.Columns.Add(custManName);
                custCompTable.Columns.Add(custManDep);
                custCompTable.Columns.Add(custManTitle);
                custCompTable.Columns.Add(custManContact);
                custCompTable.Columns.Add(custManEmail);
                custCompTable.Columns.Add(custCompId);

                custManRow = custCompTable.NewRow();
                custManRow["custManName"] = cmn;
                custManRow["custManDepart"] = cmd;
                custManRow["custManTitle"] = cmt;
                custManRow["custManContact"] = cmc;
                custManRow["custManEmail"] = cme;
                custManRow["custCompyId"] = ccId;
                custCompTable.Rows.Add(custManRow);

                dataSet.Tables.Add(custCompTable);
                #endregion

                custManProcess cmp = new custManProcess(dataSet);

                cmp.Add();

                string continueUrl = "~/Main/custInfoManager/custCompManager/custCompEditing.aspx";//Request.QueryString["ReturnUrl"];

                Response.Redirect(continueUrl);                
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

        //protected bool ddlCustComp_TextCheck()
        //{
        //    bool flag = true;
        //    if (string.IsNullOrWhiteSpace(ddlCustComp.SelectedValue.ToString().Trim()))
        //    {
        //        lblCustComp.Text = "*必填项!";
        //        flag = false;
        //    }
        //    else
        //    {
        //        lblCustComp.Text = string.Empty;
        //        //Session["flagUsrName"] = bool.TrueString.ToString().Trim();
        //        //btnOk();
        //    }

        //    return flag;
        //}

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
        //protected bool txtContact_TextCheck()
        //{
        //    bool flag = true;
        //    if (string.IsNullOrWhiteSpace(txtContact.Text.ToString().Trim()))
        //    {
        //        lblContact.Text = "*必填项!";
        //        //Session["flagContact"] = bool.FalseString.ToString().Trim();
        //        flag = false;
        //    }
        //    else if (txtContact.Text.ToString().Trim().Length != 11)
        //    {
        //        lblContact.Text = "手机号码应为11位!";
        //        //Session["flagContact"] = bool.FalseString.ToString().Trim();
        //        flag = false;
        //    }
        //    else
        //    {
        //        long sc = 0;
        //        try
        //        {
        //            sc = long.Parse(txtContact.Text.ToString().Trim());
        //            lblContact.Text = string.Empty;
        //            //Session["flagContact"] = bool.TrueString.ToString().Trim();
        //        }
        //        catch (FormatException e)
        //        {
        //            lblContact.Text = "手机号码只能包含数字!";
        //            //Session["flagContact"] = bool.FalseString.ToString().Trim();
        //            Console.WriteLine("{0} Exception caught.", e);
        //            flag = false;
        //        }
        //    } 
            
        //    return flag;
        //}
        //protected bool txtEmail_TextCheck()
        //{
        //    string strLblEmail = txtEmail.Text.ToString().Trim();

        //    bool flag = true;
        //    if (string.IsNullOrWhiteSpace(strLblEmail))
        //    {
        //        lblEmail.Text = "*必填项!";
        //        flag = false;
        //    }
        //    else if (!strLblEmail.Contains("@") || strLblEmail.StartsWith("@") || strLblEmail.EndsWith("@"))
        //    {
        //        lblEmail.Text = "邮件格式不对!";
        //        flag = false;
        //    }
        //    else
        //    {
        //        lblEmail.Text = string.Empty;
        //    }

        //    return flag;
        //}

        protected bool inputCheck()
        {
            bool flag = true;
            if (!txtName_TextCheck())
            {
                flag = false;
            }
            //else if (!ddlCustComp_TextCheck())
            //{
            //    flag = false;
            //}
            else if (!txtDep_TextCheck())
            {
                flag = false;
            }
            else if (!txtTitle_TextCheck())
            {
                flag = false;
            }
            //else if (!txtContact_TextCheck())
            //{
            //    flag = false;
            //}
            //else if (!txtEmail_TextCheck())
            //{
            //    flag = false;
            //}

            return flag;
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/custInfoManager/custCompManager/custCompEditing.aspx");
        }

        //protected void ddlCustComp_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    lblCustComp.Text = string.Empty;
        //}
    }
}