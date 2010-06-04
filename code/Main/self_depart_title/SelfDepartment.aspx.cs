using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class SelfDepartment : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int usrAuth = 0;
        string strUsrAuth = Session["totleAuthority"].ToString().Trim();
        if (String.IsNullOrEmpty(strUsrAuth))
        {
            Response.Redirect("~/Login.aspx");
        }
        else
        {
            usrAuth = int.Parse(strUsrAuth);
            int flag = 0x1 << 3;
            
            if ((usrAuth & flag) == 0)
                Response.Redirect("~/Login.aspx");
        }

        //DataSet MyDst = new DataSet();
        //SelfDepartProcess myView = new SelfDepartProcess(MyDst);

        
        if (!IsPostBack)
        {
            DataSet MyDst = new DataSet();
            SelfDepartProcess myView = new SelfDepartProcess(MyDst);

            myView.View();
            DataTable taskTable = myView.MyDst.Tables["tbl_department"];

            Session["SelfDepartProcess"] = myView;
            Session["dtSources"] = myView.MyDst.Tables["tbl_department"];
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
        SelfDepartGV.EditIndex = index;
        //Bind data to the GridView control.
        //BindData();
        SelfDepartGV.Columns[0].Visible = false;

        

        SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
        SelfDepartGV.DataBind();
        //SelfDepartGV.DataBind();
        
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
            dr["isDel"] = bool.FalseString.ToString().Trim();
            dt.Rows.Add(dr);
            
            //SelfDepartGV.EditIndex = index;
            SelfDepartGV.Columns[0].Visible = false;

            Session["dtSources"] = dt;
            SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
            SelfDepartGV.DataBind();
            SelfDepartGV.DataBind();
        }
    }
    protected void SelfDepartGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtSources"];

        //Update the values.
        GridViewRow row = SelfDepartGV.Rows[e.RowIndex];
        dt.Rows[row.DataItemIndex].Delete();
        //dt.Rows[row.DataItemIndex]["isDel"] = bool.TrueString.ToString().Trim();
        

        SelfDepartGV.DataSource = Session["dtSources"] as DataTable;
        SelfDepartGV.DataBind();

        //数据库更新；
    }
    protected void SelfDepartGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //Retrieve the table from the session object.
        DataTable dt = (DataTable)Session["dtSources"];

        //Update the values.
        GridViewRow row = SelfDepartGV.Rows[e.RowIndex];
        dt.Rows[row.DataItemIndex]["departmentName"] = ((TextBox)(row.Cells[2].Controls[0])).Text;

        //Reset the edit index.
        SelfDepartGV.EditIndex = -1;
        SelfDepartGV.Columns[0].Visible = true;

        SelfDepartGV.DataSource = Session["dtSources"] as DataTable;
        SelfDepartGV.DataBind();
    }
    protected void SelfDepartGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtSources"];

        GridViewRow row = SelfDepartGV.Rows[e.RowIndex];
        string str = dt.Rows[row.DataItemIndex]["departmentName"].ToString().Trim();

        if (string.IsNullOrWhiteSpace(str))
        {
            dt.Rows[row.DataItemIndex].Delete();
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

        DataTable dt = Session["dtSources"] as DataTable;

        int count = dt.Rows.Count;
        for (int i = 0; i < count; i ++ )
        {
            if (dt.Rows[i].RowState == DataRowState.Deleted)
            {
                dt.Rows[i].RejectChanges();
                dt.Rows[i]["isDel"] = bool.TrueString.ToString().Trim();
            }
        }
        
        SelfDepartProcess sdp = Session["SelfDepartProcess"] as SelfDepartProcess;
        
        sdp.commit();
        sdp.View();

        Session["dtSources"] = sdp.MyDst.Tables["tbl_department"] as DataTable;
        SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
        
        SelfDepartGV.DataBind();
    }
}