using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using System.Net.Mail;
using xm_mis.db;
namespace xm_mis.Main.usrSelfModify
{
    public partial class userSelfEmailModify : System.Web.UI.Page
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

                Xm_db xmDataCont = Xm_db.GetInstance();

                var usrModify =
                    (from usr in xmDataCont.Tbl_usr
                     where usr.UsrId == usrId &&
                           usr.EndTime > DateTime.Now
                     select usr).First();

                usrModify.UsrEmail = sc;

                try
                {
                    xmDataCont.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
                }
                catch (System.Data.Linq.ChangeConflictException cce)
                {
                    string strEx = cce.Message;
                    foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataCont.ChangeConflicts)
                    {
                        occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                    }

                    xmDataCont.SubmitChanges();
                }

                string continueUrl = "~/Main/DefaultMainSite.aspx";//Request.QueryString["ReturnUrl"];

                Response.Redirect(continueUrl);
            }
        }

        protected bool txtContact_TextCheck()
        {
            bool flag = true;

            string strTxt = txtContact.Text.ToString().Trim();
            if (string.IsNullOrWhiteSpace(strTxt))
            {
                txtContact.Text = "不能为空！";
                flag = false;
            }
            else if (strTxt.Length > 50)
            {
                txtContact.Text = "不能超过50个字！";
                flag = false;
            }
            else if (strTxt.Equals("不能为空！"))
            {
                txtContact.Text = "不能为空！  ";
                flag = false;
            }
            else if (strTxt.Equals("不能超过50个字！"))
            {
                txtContact.Text = "不能超过50个字！  ";
                flag = false;
            }
            else
            {
                try
                {
                    MailAddress mAddr = new MailAddress(strTxt);
                }
                catch (Exception ex)
                {
                    txtContact.Text = ex.Message;
                    flag = false;
                }
            }

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

