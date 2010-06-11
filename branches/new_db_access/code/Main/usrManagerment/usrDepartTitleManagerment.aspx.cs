using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class Main_usrManagerment_usrDepartTitleManagerment : System.Web.UI.Page
{
    string strForever = "9999-12-31";

    string sdNullId = "无";
    string stNullId = "无";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(null == Session["totleAuthority"]))
        {
            int usrAuth = 0;
            string strUsrAuth = Session["totleAuthority"].ToString();
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
            sdView.RealDepView();
            stView.RealTitleView();
            DataTable upTable = upView.MyDst.Tables["view_usr_departTitle"];
            DataTable sdTable = sdView.MyDst.Tables["tbl_department"];
            DataTable stTable = stView.MyDst.Tables["tbl_title"];

            //DataColumn[] sdKey = new DataColumn[1];
            //DataColumn[] stKey = new DataColumn[1];
            //sdKey[0] = sdTable.Columns[1];
            //stKey[0] = stTable.Columns[1];

            //sdTable.PrimaryKey = sdKey;
            //stTable.PrimaryKey = stKey;

            //object findVals = new object();
            //findVals = "无";
            
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
            string depName = dt_udt.Rows[index]["departmentName"].ToString();
            DateTime depTime = DateTime.Parse(dt_udt.Rows[index]["depEnd"].ToString());
            string titleName = dt_udt.Rows[index]["titleName"].ToString();
            DateTime titleTime = DateTime.Parse(dt_udt.Rows[index]["titleEnd"].ToString());
            DropDownList ddl = (DropDownList)e.Row.FindControl("ddlDep");
            if (ddl != null)
            {
                ddl.DataSource = dtD;
                ddl.DataTextField = "departmentName".ToString();
                ddl.DataValueField = "departmentName".ToString();
                ddl.DataBind();
                //ddl.Items[0].
                if (depTime.CompareTo(DateTime.Now) > 0)
                {
                    ddl.SelectedValue = depName;
                }
                else
                {
                    ddl.SelectedValue = sdNullId;
                }
                
            }

            ddl = (DropDownList)e.Row.FindControl("ddlTitle");
            if (ddl != null)
            {
                ddl.DataSource = dtT;
                ddl.DataTextField = "titleName".ToString();
                ddl.DataValueField = "titleName".ToString();
                ddl.DataBind();

                if (titleTime.CompareTo(DateTime.Now) > 0)
                {
                    ddl.SelectedValue = titleName; 
                }
                else
                {
                    ddl.SelectedValue = stNullId;
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
        string depName = ddl.SelectedValue.ToString();
        ddl.Enabled = false;
        ddl = (usrGV.Rows[index].FindControl("ddlTitle") as DropDownList);
        string titleName = ddl.SelectedValue.ToString();
        ddl.Enabled = false;

        UserProcess up = Session["UserProcess"] as UserProcess;

        DataTable dt = Session["upDtSources"] as DataTable;
        string usrName = dt.Rows[index]["usrName"].ToString();
        DateTime depSt = DateTime.Parse(dt.Rows[index]["usrDepSt"].ToString());
        DateTime TitleSt = DateTime.Parse(dt.Rows[index]["usrTitleSt"].ToString());
        string depOldNm = dt.Rows[index]["departmentName"].ToString();
        string TitleOldNm = dt.Rows[index]["titleName"].ToString();

        up.usrDepModify(usrName , depName, depSt , depOldNm );
        up.usrTitleModify(usrName, titleName, TitleSt , TitleOldNm);

        up.usrDepartTitleView();
        DataTable upTable = up.MyDst.Tables["view_usr_departTitle"];
        
        Session["upDtSources"] = upTable;
        
        Button btnOk = sender as Button;
        btnOk.Visible = false;
        usrGV.SelectedIndex = -1;
    }
}