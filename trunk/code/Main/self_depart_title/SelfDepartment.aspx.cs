using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class SelfDepartment : System.Web.UI.Page
{
    string strForever = "9999-12-31";
            
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
            DataSet MyDst = new DataSet();
            SelfDepartProcess myView = new SelfDepartProcess(MyDst);

            myView.SelDepView();
            DataTable taskTable = myView.MyDst.Tables["tbl_department"];
            //taskTable.DefaultView.RowFilter =
            //    "isDel = " + bool.FalseString.ToString().Trim() + " and departmentName <> '无' ";
            Session["SelfDepartProcess"] = myView;
            Session["dtSources"] = taskTable;
            Session["error"] = bool.FalseString.ToString().Trim();
            //if (Session["PAGESIZE"] != null)
            //{
            //    SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
            //    SelfDepartGV.DataBind();
            //}
            //else
            //{
            //}
            SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
            //string[] strKeyNames = new string[1];
            //strKeyNames[0] = "departmentName";
            //SelfDepartGV.DataKeyNames = strKeyNames;
            SelfDepartGV.DataBind();
        }

    }

    protected void SelfDepartGV_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    protected void SelfDepartGV_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int index = Convert.ToInt32(e.NewEditIndex);

        DataTable dt = (DataTable)Session["dtSources"];

        //if (SelfTitleGV.EditIndex != -1)
        //{
            //int indexRow = SelfTitleGV.Rows[SelfTitleGV.EditIndex].DataItemIndex;
            //if ((dt.DefaultView[indexRow].Row.RowState == DataRowState.Added) &&
            //    (string.IsNullOrWhiteSpace(dt.DefaultView[indexRow].Row["titleName"].ToString().Trim())))
            //{
            //    dt.DefaultView[indexRow].Row.Delete();
            //}
        //}

        if (SelfDepartGV.EditIndex == -1)
        {
            e.Cancel = false;
            SelfDepartGV.EditIndex = index;
        }
        else
        {
            e.Cancel = true;
        }

        //SelfDepartGV.EditIndex = index;
        //Bind data to the GridView control.
        //BindData();
        SelfDepartGV.Columns[0].Visible = false;

        //SelfDepartGV.Enabled = false;
        //SelfDepartGV.Rows[index].Enabled = true;

        SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
        SelfDepartGV.DataBind();
        SelfDepartGV.DataBind();
        //SelfDepartGV.Rows[index].Enabled = true;
        btnOk.Enabled = false;
    }
    
    protected void SelfDepartGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if ( "Add" == e.CommandName)
        {
            //int index = Convert.ToInt32(e.CommandArgument);
            //index++;

            // Retrieve the row that contains the button clicked 
            // by the user from the Rows collection.
            //GridViewRow row = new GridViewRow(index, index, DataControlRowType.EmptyDataRow, DataControlRowState.Edit);
            DataTable dt = Session["dtSources"] as DataTable;
            DataRow dr = dt.NewRow();
            dr["startTime"] = DateTime.Now;
            dr["departmentName"] = " ";
            dr["endTime"] = DateTime.Parse(strForever);
            dt.Rows.Add(dr);
            
            //SelfDepartGV.EditIndex = index;
            SelfDepartGV.Columns[0].Visible = false;

            Session["dtSources"] = dt;
            SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
            SelfDepartGV.DataBind();
            SelfDepartGV.DataBind();
            btnOk.Enabled = false;
        }
    }
    protected void SelfDepartGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (SelfDepartGV.EditIndex == -1)
        {
            DataTable dt = (DataTable)Session["dtSources"];

            //Update the values.
            GridViewRow row = SelfDepartGV.Rows[e.RowIndex];
            //dt.Rows[row.DataItemIndex].Delete();
            dt.DefaultView[row.DataItemIndex].Row["endTime"] =
                DateTime.Now.ToShortDateString();

            btnOk.Enabled = true;
        }
        

        SelfDepartGV.DataSource = Session["dtSources"] as DataTable;
        SelfDepartGV.DataBind();

        //数据库更新；
    }
    protected void SelfDepartGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //Retrieve the table from the session object.
        DataTable dt = (DataTable)Session["dtSources"];
        int index = e.RowIndex;

        //Update the values.
        GridViewRow row = SelfDepartGV.Rows[index];
        string strTxt = ((TextBox)(row.Cells[2].Controls[0])).Text.ToString().Trim();

        if (string.IsNullOrWhiteSpace(strTxt)) 
        {
            Session["error"] = bool.TrueString.ToString().Trim();
            dt.DefaultView[row.DataItemIndex].Row["departmentName"] = "部门名称不能为空！";
            SelfDepartGV.EditIndex = index;
        }
        else if (strTxt.Length > 25)
        {
            Session["error"] = bool.TrueString.ToString().Trim();
            dt.DefaultView[row.DataItemIndex].Row["departmentName"] = "部门名称不能超过25个字！";
            SelfDepartGV.EditIndex = index;
        }
        else
        {
            Session["error"] = bool.FalseString.ToString().Trim();
            dt.DefaultView[row.DataItemIndex].Row["departmentName"] = strTxt;
            SelfDepartGV.EditIndex = -1;
            SelfDepartGV.Columns[0].Visible = true;
            btnOk.Enabled = true;
        }
        
        SelfDepartGV.DataSource = Session["dtSources"] as DataTable;
        SelfDepartGV.DataBind();
    }
    protected void SelfDepartGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtSources"];

        GridViewRow row = SelfDepartGV.Rows[e.RowIndex];
        string str = dt.DefaultView[row.DataItemIndex].Row["departmentName"].ToString().Trim();

        if (string.IsNullOrWhiteSpace(str) ||
            bool.Parse(Session["error"].ToString().Trim()))
        {
            dt.DefaultView[row.DataItemIndex].Row.Delete();
            Session["error"] = bool.FalseString.ToString().Trim();
        }

        SelfDepartGV.EditIndex = -1;
        SelfDepartGV.Columns[0].Visible = true;

        //Bind data to the GridView control.
        SelfDepartGV.DataSource = Session["dtSources"] as DataTable;
        SelfDepartGV.DataBind();
    }
    protected void SelfDepartGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Data.DataRowView drv = (System.Data.DataRowView)e.Row.DataItem;
            if (string.IsNullOrWhiteSpace(drv["departmentName"].ToString().Trim()))
            {
                SelfDepartGV.EditIndex = e.Row.RowIndex;
                //rbl.Items.FindByValue("del").Enabled = false;
            }
            //gridview嵌套控件
            //System.Data.DataRowView drv = (System.Data.DataRowView)e.Row.DataItem;
            //RadioButtonList rbl = (RadioButtonList)e.Row.FindControl("RblIsDel");
            //if (rbl != null)
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

        SelfDepartProcess sdp = Session["SelfDepartProcess"] as SelfDepartProcess;
        
        sdp.commit();
        sdp.SelDepView();

        DataTable taskTable = sdp.MyDst.Tables["tbl_department"];
        //taskTable.DefaultView.RowFilter =
        //        "isDel = " + bool.FalseString.ToString().Trim() + " and departmentName <> '无' ";
        Session["dtSources"] = sdp.MyDst.Tables["tbl_department"] as DataTable;
        SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
        
        SelfDepartGV.DataBind();
        btnOk.Enabled = false;
    }
}