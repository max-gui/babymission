using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class Main_usrManagerment_usrAuthManagerment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(null == Session["totleAuthority"]))
        {
            int usrAuth = 0;
            string strUsrAuth = Session["totleAuthority"].ToString().Trim();
            usrAuth = int.Parse(strUsrAuth);
            int flag = 0x1 << 3;

            if ((usrAuth & flag) == 0)
                Response.Redirect("~/Main/NoAuthority");
        }
        else
        {
            Response.Redirect("~/Login.aspx");
        }

        if (!IsPostBack)
        {
            DataSet MyDst = new DataSet();
            SelfTitleProcess myView = new SelfTitleProcess(MyDst);

            myView.View();
            DataTable taskTable = myView.MyDst.Tables["tbl_title"];
            taskTable.DefaultView.RowFilter =
                "isDel = " + bool.FalseString.ToString().Trim();
            Session["SelfTitleProcess"] = myView;
            Session["dtSources"] = taskTable;
            Session["error"] = bool.FalseString.ToString().Trim();
            //if (Session["PAGESIZE"] != null)
            //{
            //    SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
            //    SelfTitleGV.DataBind();
            //}
            //else
            //{
            //}
            usrGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
            //string[] strKeyNames = new string[1];
            //strKeyNames[0] = "titleName";
            //SelfTitleGV.DataKeyNames = strKeyNames;
            usrGV.DataBind();
        }
    }
    protected void usrGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
}