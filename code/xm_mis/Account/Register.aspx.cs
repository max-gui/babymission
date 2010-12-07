using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using System.Net.Mail;
namespace xm_mis.Account
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //        txtName.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.projectTagApply);
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
                DataSet dst = new DataSet();
                DataRow dr = null;

                #region ddlSelfDepartView

                SelfDepartProcess ddlSelfDepartView = new SelfDepartProcess(dst);

                ddlSelfDepartView.SelDepView();
                DataTable ddlSelfDepartTable = ddlSelfDepartView.MyDst.Tables["tbl_department"].DefaultView.ToTable();

                dr = ddlSelfDepartTable.NewRow();
                dr["departmentId"] = -1;
                dr["departmentName"] = string.Empty;
                dr["endTime"] = "9999-12-31";
                ddlSelfDepartTable.Rows.Add(dr);

                ddlDepart.DataValueField = "departmentId";
                ddlDepart.DataTextField = "departmentName";
                ddlDepart.DataSource = ddlSelfDepartTable;
                ddlDepart.DataBind();
                ddlDepart.SelectedValue = "-1";

                #endregion

                #region ddlSelfTitleView

                SelfTitleProcess ddlSelfTitleView = new SelfTitleProcess(dst);

                ddlSelfTitleView.SelfTitleView();
                DataTable ddlSelfTitleTable = ddlSelfTitleView.MyDst.Tables["tbl_title"].DefaultView.ToTable();

                dr = ddlSelfTitleTable.NewRow();
                dr["titleId"] = -1;
                dr["titleName"] = string.Empty;
                dr["endTime"] = "9999-12-31";
                ddlSelfTitleTable.Rows.Add(dr);

                ddlTitle.DataValueField = "titleId";
                ddlTitle.DataTextField = "titleName";
                ddlTitle.DataSource = ddlSelfTitleTable;
                ddlTitle.DataBind();
                ddlTitle.SelectedValue = "-1";

                #endregion
            }
        }

        /*
        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(this.txtUsrName.Text.ToString().Trim(), false);

            string continueUrl = Request.QueryString["ReturnUrl"];
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }
        */
        protected void btnReg_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                string usrName = txtUsrName.Text.ToString().Trim();
                string realName = txtRealName.Text.ToString().Trim();
                string usrMobile = txtMobile.Text.ToString().Trim();
                string usrEmail = txtEmail.Text.ToString().Trim();
                string departmentId = ddlDepart.SelectedValue.ToString().Trim();
                string titleId = ddlTitle.SelectedValue.ToString().Trim();
                //int sc = int.Parse(txtContact.Text.ToString().Trim());

                #region dataset
                DataSet dataSet = new DataSet();
                DataRow userRow = null;

                DataColumn colName = new DataColumn("realName", System.Type.GetType("System.String"));
                DataColumn colUsrName = new DataColumn("usrName", System.Type.GetType("System.String"));
                DataColumn colUsrMobile = new DataColumn("usrMobile", System.Type.GetType("System.String"));
                DataColumn colUsrEmail = new DataColumn("usrEmail", System.Type.GetType("System.String"));
                DataColumn colDepartId = new DataColumn("departmentId", System.Type.GetType("System.String"));
                DataColumn colTitleId = new DataColumn("titleId", System.Type.GetType("System.String"));

                DataTable userTable = new DataTable("addTable");

                userTable.Columns.Add(colName);
                userTable.Columns.Add(colUsrName);
                userTable.Columns.Add(colUsrMobile);
                userTable.Columns.Add(colUsrEmail);
                userTable.Columns.Add(colDepartId);
                userTable.Columns.Add(colTitleId);

                userRow = userTable.NewRow();
                userRow["realName"] = realName;
                userRow["usrName"] = usrName;
                userRow["usrMobile"] = usrMobile;
                userRow["usrEmail"] = usrEmail;
                userRow["departmentId"] = departmentId;
                userRow["titleId"] = titleId;
                userTable.Rows.Add(userRow);

                dataSet.Tables.Add(userTable);
                #endregion

                UserProcess up = new UserProcess(dataSet);

                string error = up.Add_includeError();

                if (string.IsNullOrEmpty(error))
                {
                    Response.Redirect("~/Main/usrManagerment/usrInfoManagerment.aspx");
                }
                else
                {
                    lblName.Text = error;
                }
            }
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

        protected bool txtMobileNumber_Check(TextBox txtBx)
        {
            bool flag = true;

            string strBx = string.Empty;
            long sc = 0;
            try
            {
                strBx = txtBx.Text.ToString().Trim();
                sc = long.Parse(strBx);
                txtBx.Text = strBx;

                if (strBx.Length != 11)
                {
                    txtBx.Text = "手机号码应为11位!";
                    //Session["flagContact"] = bool.FalseString.ToString().Trim();
                    flag = false;
                }
            }
            catch (ArgumentNullException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }
            catch (FormatException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }
            catch (OverflowException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }

            return flag;
        }

        protected bool ddlUnSelect_Check(DropDownList ddl)
        {
            bool flag = true;

            if (ddl.SelectedValue.Equals("-1"))
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

            flag = txtNullOrLenth_Check(txtUsrName)
                && txtNullOrLenth_Check(txtRealName)
                && txtMobileNumber_Check(txtMobile)
                && txtNullOrLenth_Check(txtEmail)
                && ddlUnSelect_Check(ddlDepart)
                && ddlUnSelect_Check(ddlTitle);

            try
            {
                MailAddress mAddr = new MailAddress(txtEmail.Text.Trim());
            }
            catch (Exception ex)
            {
                txtEmail.Text = ex.Message;
                flag = false;
            }

            return flag;
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/usrManagerment/usrInfoManagerment.aspx");
        }

    }
}
