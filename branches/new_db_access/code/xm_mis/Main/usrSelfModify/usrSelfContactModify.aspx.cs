using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.usrSelfModify
{
    public partial class usrSelfContactModify : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                string sc = txtContact.Text.ToString().Trim();
                int usrId = int.Parse(Session["usrId"].ToString());

                DataSet ds = null;
                UserProcess up = new UserProcess(ds);

                up.usrContactModify(usrId, sc);
                int rowRtn = up.IntRtn;

                string continueUrl = "~/Main/DefaultMainSite.aspx";//Request.QueryString["ReturnUrl"];

                Response.Redirect(continueUrl);
            }
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

            //btnOk();

            return flag;
        }

        protected bool inputCheck()
        {
            bool flag = true;
            if (!txtContact_TextCheck())
            {
                flag = false;
            }

            return flag;
        }
    }
}