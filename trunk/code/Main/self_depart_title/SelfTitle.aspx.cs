using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class SelfTitle : System.Web.UI.Page
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
        //SelfTitleProcess myView = new SelfTitleProcess(MyDst);


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
            SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
            //string[] strKeyNames = new string[1];
            //strKeyNames[0] = "titleName";
            //SelfTitleGV.DataKeyNames = strKeyNames;
            SelfTitleGV.DataBind();
        }
    }

    protected void SelfTitleGV_Sorting(object sender, GridViewSortEventArgs e)
    {

    }

    protected void SelfTitleGV_RowEditing(object sender, GridViewEditEventArgs e)
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

        if (SelfTitleGV.EditIndex == -1)
        {
            e.Cancel = false;
            SelfTitleGV.EditIndex = index;
        }
        else
        {
            e.Cancel = true;
        }

        //if (indexNow.Equals(-1))
        //{
        //    SelfTitleGV.EditIndex = index;
        //}
        //else
        //{
        //    Session["error"] = bool.TrueString.ToString().Trim();
        //    dt.Rows[indexNow]["titleName"] = "职位名称不能为空！";
        //    //SelfTitleGV.EditIndex = indexNow;
        //}//Bind data to the GridView control.
        //BindData();
        SelfTitleGV.Columns[0].Visible = false;



        SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
        SelfTitleGV.DataBind();
        SelfTitleGV.DataBind();
        //SelfTitleGV.DataBind();

    }

    protected void SelfTitleGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if ("Add" == e.CommandName)
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

            //SelfTitleGV.EditIndex = index;
            SelfTitleGV.Columns[0].Visible = false;

            Session["dtSources"] = dt;
            SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
            SelfTitleGV.DataBind();
            SelfTitleGV.DataBind();
        }
    }
    protected void SelfTitleGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (SelfTitleGV.EditIndex == -1)
        {
            DataTable dt = (DataTable)Session["dtSources"];

            // DataView dv = Session["dtSources"] as DataView;

            //Update the values.
            GridViewRow row = SelfTitleGV.Rows[e.RowIndex];
            //dt.Rows[row.DataItemIndex].Delete();
            //dt.Rows[row.DataItemIndex]["isDel"] = bool.TrueString.ToString().Trim();
            dt.DefaultView[row.DataItemIndex].Row["isDel"] = bool.TrueString.ToString().Trim();


            //SelfTitleGV.DataSource = Session["dtSources"] as DataTable;
        }
        
        SelfTitleGV.DataSource = Session["dtSources"] as DataTable;
        SelfTitleGV.DataBind();

        //数据库更新；
    }
    protected void SelfTitleGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //Retrieve the table from the session object.
        DataTable dt = (DataTable)Session["dtSources"];
        int index = e.RowIndex;

        //Update the values.
        GridViewRow row = SelfTitleGV.Rows[index];
        string strTxt = ((TextBox)(row.Cells[2].Controls[0])).Text.ToString().Trim();

        if (string.IsNullOrWhiteSpace(strTxt))
        {
            Session["error"] = bool.TrueString.ToString().Trim();
            dt.DefaultView[row.DataItemIndex].Row["titleName"] = "职位名称不能为空！";
            SelfTitleGV.EditIndex = index;
        }
        else if (strTxt.Length > 25)
        {
            Session["error"] = bool.TrueString.ToString().Trim();
            dt.DefaultView[row.DataItemIndex].Row["titleName"] = "职位名称不能超过25个字！";
            SelfTitleGV.EditIndex = index;
        }
        else
        {
            Session["error"] = bool.FalseString.ToString().Trim();
            dt.DefaultView[row.DataItemIndex].Row["titleName"] = strTxt;
            SelfTitleGV.EditIndex = -1;
            SelfTitleGV.Columns[0].Visible = true;

        }

        SelfTitleGV.DataSource = Session["dtSources"] as DataTable;
        SelfTitleGV.DataBind();
    }
    protected void SelfTitleGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        DataTable dt = (DataTable)Session["dtSources"];

        GridViewRow row = SelfTitleGV.Rows[e.RowIndex];
        string str = dt.DefaultView[row.DataItemIndex].Row["titleName"].ToString().Trim();

        if (string.IsNullOrWhiteSpace(str) ||
            bool.Parse(Session["error"].ToString().Trim()))
        {
            dt.DefaultView[row.DataItemIndex].Row.Delete();
            Session["error"] = bool.FalseString.ToString().Trim();
        }

        SelfTitleGV.EditIndex = -1;
        SelfTitleGV.Columns[0].Visible = true;

        //Bind data to the GridView control.
        SelfTitleGV.DataSource = Session["dtSources"] as DataTable;
        SelfTitleGV.DataBind();
    }
    protected void SelfTitleGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Data.DataRowView drv = (System.Data.DataRowView)e.Row.DataItem;
            if (string.IsNullOrWhiteSpace(drv["titleName"].ToString().Trim()))
            {
                SelfTitleGV.EditIndex = e.Row.RowIndex;
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
            //    ttb.Text = drv["titleName"].ToString().Trim();
            //    ttb.Enabled = false;
            //}

        }

    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        SelfTitleProcess stp = Session["SelfTitleProcess"] as SelfTitleProcess;

        stp.commit();
        stp.View();

        Session["dtSources"] = stp.MyDst.Tables["tbl_title"] as DataTable;
        SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

        SelfTitleGV.DataBind();
    }
}