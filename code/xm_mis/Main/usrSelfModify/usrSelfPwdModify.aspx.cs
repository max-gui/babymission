using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
namespace xm_mis.Main.usrSelfModify
{
    public partial class usrSelfPwdModify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                string spw = txtPassWord.Text.ToString().Trim();
                int usrId = int.Parse(Session["usrId"].ToString());

                DataSet ds = null;
                UserProcess up = new UserProcess(ds);

                up.usrPwdModify(usrId, spw);
                int rowRtn = up.IntRtn;

                string continueUrl = "~/Main/DefaultMainSite.aspx";//Request.QueryString["ReturnUrl"];

                Response.Redirect(continueUrl);
            }
        }

        protected bool txtPassWord_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtPassWord.Text.ToString().Trim()))
            {
                lblPassWord.Text = "*必填项!";
                flag = false;
            }
            else if (txtPassWord.Text.ToString().Trim().Length > 10)
            {
                lblPassWord.Text = "密码太长!";
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblPassWord.Text = string.Empty;
                //Session["flagPassWord"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }
        protected bool txtRPassWord_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtRPassWord.Text.ToString().Trim()))
            {
                lblRPassWord.Text = "*必填项!";
                flag = false;
            }
            else if (!(txtRPassWord.Text.ToString().Equals(txtPassWord.Text.ToString())))
            {
                lblRPassWord.Text = "两次输入的密码不同!";
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblRPassWord.Text = string.Empty;
                //Session["flagPassWord"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }

        protected bool inputCheck()
        {
            bool flag = true;
            if (!txtPassWord_TextCheck())
            {
                flag = false;
            }
            else if (!txtRPassWord_TextCheck())
            {
                flag = false;
            }

            return flag;
        }
    }
}