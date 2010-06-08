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
                Response.Redirect("~/Main/self_depart_title/SelfDepartment.aspx");
                break;
            case ("titleEdit"):
                Response.Redirect("~/Main/self_depart_title/SelfTitle.aspx");
                break;
            case ("usrAuth"):
                Response.Redirect("~/Main/usrManagerment/usrAuthManagerment.aspx");
                break;
            case ("usrDepartTitle"):
            Response.Redirect("~/Main/usrManagerment/usrAuthManagerment.aspx");
            break;
        
        
        
        }
    }
}
