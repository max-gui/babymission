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
    string oldDepName = string.Empty;
            
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

        
        if (SelfDepartGV.EditIndex == -1)
        {
            e.Cancel = false;
            SelfDepartGV.EditIndex = index;
            
            SelfDepartGV.Columns[0].Visible = false;

            SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
            SelfDepartGV.DataBind();
            
            btnAdd.Enabled = false;
        }
        else
        {
            e.Cancel = true;
        }

        
    }
    
    protected void SelfDepartGV_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if ( "Add" == e.CommandName)
        {
            DataTable dt = Session["dtSources"] as DataTable;
            DataRow dr = dt.NewRow();
            dr["startTime"] = DateTime.Now;
            dr["departmentName"] = " ";
            dr["endTime"] = DateTime.Parse(strForever);
            dt.Rows.Add(dr);
            
            SelfDepartGV.Columns[0].Visible = false;

            Session["dtSources"] = dt;
            SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
            SelfDepartGV.DataBind();
            SelfDepartGV.DataBind();
            btnAdd.Enabled = false;
        }
    }
    protected void SelfDepartGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (SelfDepartGV.EditIndex == -1)
        {
            DataTable dt = (DataTable)Session["dtSources"];

            //Update the values.
            GridViewRow row = SelfDepartGV.Rows[e.RowIndex];
            dt.DefaultView[row.DataItemIndex].Row["endTime"] =
                DateTime.Now.ToShortDateString();

            btnAdd.Enabled = true;
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
            btnAdd.Enabled = true;
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

    //protected void btnOk_Click(object sender, EventArgs e)
    //{

    //    SelfDepartProcess sdp = Session["SelfDepartProcess"] as SelfDepartProcess;
        
    //    sdp.commit();
    //    sdp.SelDepView();

    //    DataTable taskTable = sdp.MyDst.Tables["tbl_department"];
    //    //taskTable.DefaultView.RowFilter =
    //    //        "isDel = " + bool.FalseString.ToString().Trim() + " and departmentName <> '无' ";
    //    Session["dtSources"] = sdp.MyDst.Tables["tbl_department"] as DataTable;
    //    SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
        
    //    SelfDepartGV.DataBind();
    //    btnAdd.Enabled = false;
    //}

    protected string input_check(string depName)
    {
        DataTable dt = (DataTable)Session["dtSources"];
        string strRtn = string.Empty;

        if (string.IsNullOrWhiteSpace(depName))
        {
            strRtn = "部门名称不能为空！";
        }
        else if (depName.Length > 25)
        {
            strRtn = "部门名称不能超过25个字！";
        }
        else if (depName.Equals("部门名称不能为空！"))
        {
            strRtn = "部门名称不能为空！  ";
        }
        else if (depName.Equals("部门名称不能超过25个字！"))
        {
            strRtn = "部门名称不能超过25个字！  ";
        }
        else
        {
            strRtn = depName;
        }

        return strRtn;
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        int index = SelfDepartGV.SelectedIndex;

        int dataIndex = SelfDepartGV.Rows[index].DataItemIndex;
        DataTable dt = (DataTable)Session["dtSources"];
        int depId = int.Parse(dt.DefaultView[dataIndex].Row["departmentId"].ToString());

        Button btn = null;
        btn = (SelfDepartGV.Rows[index].FindControl("btnUpdate") as Button);
        btn.Visible = false;

        SelfDepartProcess sdp = Session["SelfDepartProcess"] as SelfDepartProcess;

        sdp.SelfDepDel(depId);

        sdp.SelDepView();

        DataTable taskTable = sdp.MyDst.Tables["tbl_department"];
        //taskTable.DefaultView.RowFilter =
        //        "isDel = " + bool.FalseString.ToString().Trim() + " and departmentName <> '无' ";
        Session["dtSources"] = sdp.MyDst.Tables["tbl_department"] as DataTable;
        SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

        SelfDepartGV.SelectedIndex = -1;
        SelfDepartGV.EditIndex = -1;
        SelfDepartGV.DataBind();

        btn = sender as Button;
        btn.Visible = false;
        btnAdd.Enabled = true;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int index = SelfDepartGV.SelectedIndex;

        int dataIndex = SelfDepartGV.Rows[index].DataItemIndex;
        DataTable dt = (DataTable)Session["dtSources"];

        GridViewRow row = SelfDepartGV.Rows[index];
        TextBox tbDepName = row.Cells[1].Controls[0] as TextBox;
        string newDepName = tbDepName.Text.ToString().Trim();

        string strCheck = newDepName;
        newDepName = input_check(strCheck.Trim());

        if (newDepName.Equals(strCheck))
        {
            if (!oldDepName.Equals(newDepName))
            {

                int depId = int.Parse(dt.DefaultView[dataIndex].Row["departmentId"].ToString());

                SelfDepartProcess sdp = Session["SelfDepartProcess"] as SelfDepartProcess;

                sdp.SelfDepUpdate(depId, newDepName);

                sdp.SelDepView();

                DataTable taskTable = sdp.MyDst.Tables["tbl_department"];
                //taskTable.DefaultView.RowFilter =
                //        "isDel = " + bool.FalseString.ToString().Trim() + " and departmentName <> '无' ";
                Session["dtSources"] = sdp.MyDst.Tables["tbl_department"] as DataTable;
            }

            Button btn = null;
            btn = (SelfDepartGV.Rows[index].FindControl("btnDel") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;

            SelfDepartGV.SelectedIndex = -1;
            SelfDepartGV.EditIndex = -1;

            SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;  
            SelfDepartGV.DataBind();

            btnAdd.Enabled = true;
        }
        else
        {
            tbDepName.Text = newDepName;
            SelfDepartGV.SelectedIndex = index;
            SelfDepartGV.EditIndex = index;

            //Button btn = null;
            //btn = (SelfDepartGV.Rows[index].FindControl("btnDel") as Button);
            //btn.Visible = true;
            //btn = sender as Button;
            //btn.Visible = true;
        }
    }
    protected void SelfDepartGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (SelfDepartGV.SelectedIndex == -1)
        {
            e.Cancel = false;
            int index = e.NewSelectedIndex;
            
            int dataIndex = SelfDepartGV.Rows[index].DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];
            oldDepName = dt.DefaultView[dataIndex].Row["departmentName"].ToString();

            SelfDepartGV.EditIndex = index;
            SelfDepartGV.DataSource = Session["dtSources"];
            SelfDepartGV.DataBind();

            Button btn = null;
            btn = (SelfDepartGV.Rows[index].FindControl("btnDel") as Button);
            btn.Visible = true;
            btn = (SelfDepartGV.Rows[index].FindControl("btnUpdate") as Button);
            btn.Visible = true;
        }
        else
        {
            e.Cancel = true;
        }

        btnAdd.Enabled = false;
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        lblDepName.Visible = true;
        txtDepName.Visible = true;
        btnAccept.Visible = true;
        btnNo.Visible = true;

        btnAdd.Enabled = false;
    }
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        lblDepName.Visible = false;
        txtDepName.Visible = false;
        btnAccept.Visible = false;
        btnNo.Visible = false;

        btnAdd.Enabled = true;
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        lblDepName.Visible = false;
        txtDepName.Text = string.Empty;
        txtDepName.Visible = false;
        btnAccept.Visible = false;
        btnNo.Visible = false;

        btnAdd.Enabled = true;
    }
}