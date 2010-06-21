using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void MainMenu_MenuItemClick(object sender, MenuEventArgs e)
    {
        string s = e.Item.Value.ToString().Trim();
        switch (s)
        { 
            case ("departEdit"):
                Response.Redirect("~/Main/self_depart_title/selfDep/SelfDepartment.aspx");
                break;
            case ("titleEdit"):
                Response.Redirect("~/Main/self_depart_title/selfTitle/SelfTitle.aspx");
                break;
            case ("usrAuth"):
                Response.Redirect("~/Main/usrManagerment/usrAuthManagerment.aspx");
                break;
            case ("usrDepartTitle"):
                Response.Redirect("~/Main/usrManagerment/usrDepartTitleManagerment.aspx");
                break;
            case ("usrDel"):
                Response.Redirect("~/Main/usrManagerment/usrInfoDel.aspx");
                break;
            case ("selfPwdModify"):
                Response.Redirect("~/Main/usrSelfModify/usrSelfPwdModify.aspx");
                break;
            case ("selfContectModify"):
                Response.Redirect("~/Main/usrSelfModify/usrSelfContactModify.aspx");
            break;
            case ("custCompAdd"):
            Response.Redirect("~/Main/custInfoManager/custCompManager/custCompAdd.aspx");
            break;
                
        }
    }
}
