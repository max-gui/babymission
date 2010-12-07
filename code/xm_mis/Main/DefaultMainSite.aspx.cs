using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace xm_mis.Main
{
    public partial class DefaultMainSite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                //int usrAuth = 0;
                //string strUsrAuth = Session["totleAuthority"] as string;
                //usrAuth = int.Parse(strUsrAuth);
                //int flag = 0x1 << 15;

                //if ((usrAuth & flag) == 0)
                //    Response.Redirect("~/Main/NoAuthority.aspx");
            }
            else
            {
                //string url = Request.FilePath;
                //Session["backUrl"] = url;
                Response.Redirect("~/Account/Login.aspx");
            }
        }
    }
}