using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class Main_usrManagerment_usrDepartTitleManagerment : System.Web.UI.Page
{
    string sdNullId = string.Empty;
    string stNullId = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(null == Session["totleAuthority"]))
        {
            int usrAuth = 0;
            string strUsrAuth = Session["totleAuthority"].ToString().Trim();
            usrAuth = int.Parse(strUsrAuth);
            int flag = 0x1 << 3;

            if ((usrAuth & flag) == 0)
                Response.Redirect("~/Main/NoAuthority.aspx");
        }
        else
        {
            Response.Redirect("~/Account/Login.aspx");
        }

        //DataSet MyDst = new DataSet();
        //SelfDepartProcess myView = new SelfDepartProcess(MyDst);


        if (!IsPostBack)
        {
            DataSet upDst = new DataSet();
            DataSet sdDst = new DataSet();
            DataSet stDst = new DataSet();
            UserProcess upView = new UserProcess(upDst);
            SelfDepartProcess sdView = new SelfDepartProcess(sdDst);
            SelfTitleProcess stView = new SelfTitleProcess(stDst);

            upView.usrDepartTitleView();
            sdView.View();
            stView.View();
            DataTable upTable = upView.MyDst.Tables["view_usr_departTitle"];
            DataTable sdTable = sdView.MyDst.Tables["tbl_department"];
            DataTable stTable = stView.MyDst.Tables["tbl_title"];

            DataColumn[] sdKey = new DataColumn[1];
            DataColumn[] stKey = new DataColumn[1];
            sdKey[0] = sdTable.Columns[1];
            stKey[0] = stTable.Columns[1];

            sdTable.PrimaryKey = sdKey;
            stTable.PrimaryKey = stKey;

            object findVals = new object();
            findVals = "无";

            sdNullId = sdTable.Rows.Find(findVals)["departmentId"].ToString().Trim();
            stNullId = stTable.Rows.Find(findVals)["titleId"].ToString().Trim();

            sdTable.DefaultView.RowFilter =
                "isDel = " + bool.FalseString.ToString().Trim();
            stTable.DefaultView.RowFilter =
                "isDel = " + bool.FalseString.ToString().Trim();

            Session["UserProcess"] = upView;
            Session["upDtSources"] = upTable;
            Session["sdDtSources"] = sdTable;
            Session["stDtSources"] = stTable;
            
            //if (Session["PAGESIZE"] != null)
            //{
            //    SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
            //    SelfDepartGV.DataBind();
            //}
            //else
            //{
            //}
            usrGV.DataSource = Session["upDtSources"];//["dtSources"] as DataTable;
            //string[] strKeyNames = new string[1];
            //strKeyNames[0] = "departmentName";
            //SelfDepartGV.DataKeyNames = strKeyNames;
            usrGV.DataBind();
        }
    }
    protected void usrGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void usrGV_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void usrGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (usrGV.SelectedIndex == -1)
        {
            e.Cancel = false;
            int index = e.NewSelectedIndex;

            DropDownList ddl = null;
            ddl = (usrGV.Rows[index].FindControl("ddlDep") as DropDownList);
            ddl.Enabled = true;
            ddl = (usrGV.Rows[index].FindControl("ddlTitle") as DropDownList);
            ddl.Enabled = true;

            Button btnOk = (usrGV.Rows[index].FindControl("btnOk") as Button);
            btnOk.Visible = true;            
        }
        else
        {
            e.Cancel = true;
        }
    }
    protected void usrGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //
            DataTable dt_udt = Session["upDtSources"] as DataTable;
            DataTable dtD = Session["sdDtSources"] as DataTable;
            DataTable dtT = Session["stDtSources"] as DataTable;

            int index = e.Row.DataItemIndex;
            string strDepId = dt_udt.Rows[index]["departmentId"].ToString().Trim();
            string strTitleId = dt_udt.Rows[index]["titleId"].ToString().Trim();
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlDep");
            if (ddl != null)
            {
                ddl.DataSource = dtD;
                ddl.DataTextField = "departmentName".ToString().Trim();
                ddl.DataValueField = "departmentId".ToString().Trim();
                ddl.DataBind();
                //ddl.Items[0].
                if (bool.Parse(dt_udt.Rows[index]["depIsDel"].ToString().Trim()))
                {
                    ddl.SelectedValue = sdNullId;
                }
                else
                {
                    ddl.SelectedValue = strDepId;
                }
                
            }

            ddl = (DropDownList)e.Row.FindControl("ddlTitle");
            if (ddl != null)
            {
                ddl.DataSource = dtT;
                ddl.DataTextField = "titleName".ToString().Trim();
                ddl.DataValueField = "titleId".ToString().Trim();
                ddl.DataBind();

                if (bool.Parse(dt_udt.Rows[index]["titleIsDel"].ToString().Trim()))
                {
                    ddl.SelectedValue = stNullId;
                }
                else
                {
                    ddl.SelectedValue = strTitleId;
                }
            }


            //{
            //    if (bool.Parse(drv["isDel"].ToString().Trim()))
            //    {
            //        rbl.Items.FindByValue("del").Selected = true;
            //        //rbl.Items.FindByValue("del").Enabled = false;
            //    }
            //    else
            //    {
            //        rbl.Items.FindByValue("unDel").Selected = true;
            //        //rbl.Items.FindByValue("unDel").Enabled = false;
            //    }
            //    rbl.Items.FindByValue("del").Enabled = false;
            //    rbl.Items.FindByValue("unDel").Enabled = false;
            //}

            //TextBox ttb = (TextBox)e.Row.FindControl("depNameTxt");
            //if (ttb != null)
            //{
            //    ttb.Text = drv["departmentName"].ToString().Trim();
            //    ttb.Enabled = false;
            //}

        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        int index = usrGV.SelectedIndex;

        DropDownList ddl = null;
        ddl = (usrGV.Rows[index].FindControl("ddlDep") as DropDownList);
        int depId = int.Parse(ddl.SelectedValue.ToString().Trim());
        ddl.Enabled = false;
        ddl = (usrGV.Rows[index].FindControl("ddlTitle") as DropDownList);
        int titleId = int.Parse(ddl.SelectedValue.ToString().Trim());
        ddl.Enabled = false;

        UserProcess up = Session["UserProcess"] as UserProcess;

        DataTable dt = Session["upDtSources"] as DataTable;
        int usrId = int.Parse(dt.Rows[index]["usrId"].ToString().Trim());
        up.usrDepModify(usrId, depId);
        up.usrTitleModify(usrId, titleId);

        up.usrDepartTitleView();
        DataTable upTable = up.MyDst.Tables["view_usr_departTitle"];
        
        Session["upDtSources"] = upTable;
        
        Button btnOk = sender as Button;
        btnOk.Visible = false;
        usrGV.SelectedIndex = -1;
    }
}