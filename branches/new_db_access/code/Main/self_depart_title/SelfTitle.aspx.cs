using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class SelfTitle : System.Web.UI.Page
{
    //string strForever = "9999-12-31";
            
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

        if (!IsPostBack)
        {   
            DataSet MyDst = new DataSet();
            SelfTitleProcess myView = new SelfTitleProcess(MyDst);

            myView.SelfTitleView();
            DataTable taskTable = myView.MyDst.Tables["tbl_title"];
            //taskTable.DefaultView.RowFilter =
            //    "isDel = " + bool.FalseString.ToString().Trim() + " and titleName <> '无' ";
            Session["SelfTitleProcess"] = myView;
            Session["dtSources"] = taskTable;
            //Session["error"] = bool.FalseString.ToString().Trim();
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

    //protected void SelfTitleGV_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    int index = Convert.ToInt32(e.NewEditIndex);

    //    DataTable dt = (DataTable)Session["dtSources"];

    //    //if (SelfTitleGV.EditIndex != -1)
    //    //{
    //        //int indexRow = SelfTitleGV.Rows[SelfTitleGV.EditIndex].DataItemIndex;
    //        //if ((dt.DefaultView[indexRow].Row.RowState == DataRowState.Added) &&
    //        //    (string.IsNullOrWhiteSpace(dt.DefaultView[indexRow].Row["titleName"].ToString().Trim())))
    //        //{
    //        //    dt.DefaultView[indexRow].Row.Delete();
    //        //}
    //    //}

    //    if (SelfTitleGV.EditIndex == -1)
    //    {
    //        e.Cancel = false;
    //        SelfTitleGV.EditIndex = index;
    //    }
    //    else
    //    {
    //        e.Cancel = true;
    //    }

    //    //if (indexNow.Equals(-1))
    //    //{
    //    //    SelfTitleGV.EditIndex = index;
    //    //}
    //    //else
    //    //{
    //    //    Session["error"] = bool.TrueString.ToString().Trim();
    //    //    dt.Rows[indexNow]["titleName"] = "职位名称不能为空！";
    //    //    //SelfTitleGV.EditIndex = indexNow;
    //    //}//Bind data to the GridView control.
    //    //BindData();
    //    SelfTitleGV.Columns[0].Visible = false;



    //    SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
    //    SelfTitleGV.DataBind();
    //    SelfTitleGV.DataBind();
    //    //SelfTitleGV.DataBind();

    //    btnOk.Enabled = false;
    //}

    //protected void SelfTitleGV_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if ("Add" == e.CommandName)
    //    {
    //        //int index = Convert.ToInt32(e.CommandArgument);
    //        //index++;

    //        // Retrieve the row that contains the button clicked 
    //        // by the user from the Rows collection.
    //        //GridViewRow row = new GridViewRow(index, index, DataControlRowType.EmptyDataRow, DataControlRowState.Edit);
    //        DataTable dt = Session["dtSources"] as DataTable;
    //        DataRow dr = dt.NewRow();
    //        dr["startTime"] = DateTime.Now;
    //        dr["titleName"] = " ";
    //        dr["endTime"] = DateTime.Parse(strForever);
    //        dt.Rows.Add(dr);

    //        //SelfTitleGV.EditIndex = index;
    //        SelfTitleGV.Columns[0].Visible = false;

    //        Session["dtSources"] = dt;
    //        SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
    //        SelfTitleGV.DataBind();
    //        SelfTitleGV.DataBind();

    //        btnOk.Enabled = false;
    //    }
    //}
    //protected void SelfTitleGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    if (SelfTitleGV.EditIndex == -1)
    //    {
    //        DataTable dt = (DataTable)Session["dtSources"];

    //        // DataView dv = Session["dtSources"] as DataView;

    //        //Update the values.
    //        GridViewRow row = SelfTitleGV.Rows[e.RowIndex];
    //        //dt.Rows[row.DataItemIndex].Delete();
    //        dt.DefaultView[row.DataItemIndex].Row["endTime"] =
    //            DateTime.Now.ToShortDateString();

    //        btnOk.Enabled = true;
    //    }
        
    //    SelfTitleGV.DataSource = Session["dtSources"] as DataTable;
    //    SelfTitleGV.DataBind();

    //    //数据库更新；
    //}
    //protected void SelfTitleGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    //Retrieve the table from the session object.
    //    DataTable dt = (DataTable)Session["dtSources"];
    //    int index = e.RowIndex;

    //    //Update the values.
    //    GridViewRow row = SelfTitleGV.Rows[index];
    //    string strTxt = ((TextBox)(row.Cells[2].Controls[0])).Text.ToString().Trim();

    //    if (string.IsNullOrWhiteSpace(strTxt))
    //    {
    //        Session["error"] = bool.TrueString.ToString().Trim();
    //        dt.DefaultView[row.DataItemIndex].Row["titleName"] = "职位名称不能为空！";
    //        SelfTitleGV.EditIndex = index;
    //    }
    //    else if (strTxt.Length > 25)
    //    {
    //        Session["error"] = bool.TrueString.ToString().Trim();
    //        dt.DefaultView[row.DataItemIndex].Row["titleName"] = "职位名称不能超过25个字！";
    //        SelfTitleGV.EditIndex = index;
    //    }
    //    else
    //    {
    //        Session["error"] = bool.FalseString.ToString().Trim();
    //        dt.DefaultView[row.DataItemIndex].Row["titleName"] = strTxt;
    //        SelfTitleGV.EditIndex = -1;
    //        SelfTitleGV.Columns[0].Visible = true;

    //        btnOk.Enabled = true;
    //    }

    //    SelfTitleGV.DataSource = Session["dtSources"] as DataTable;
    //    SelfTitleGV.DataBind();
    //}
    //protected void SelfTitleGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //    DataTable dt = (DataTable)Session["dtSources"];

    //    GridViewRow row = SelfTitleGV.Rows[e.RowIndex];
    //    string str = dt.DefaultView[row.DataItemIndex].Row["titleName"].ToString().Trim();

    //    if (string.IsNullOrWhiteSpace(str) ||
    //        bool.Parse(Session["error"].ToString().Trim()))
    //    {
    //        dt.DefaultView[row.DataItemIndex].Row.Delete();
    //        Session["error"] = bool.FalseString.ToString().Trim();
    //    }

    //    SelfTitleGV.EditIndex = -1;
    //    SelfTitleGV.Columns[0].Visible = true;

    //    //Bind data to the GridView control.
    //    SelfTitleGV.DataSource = Session["dtSources"] as DataTable;
    //    SelfTitleGV.DataBind();
    //}
    //protected void SelfTitleGV_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        System.Data.DataRowView drv = (System.Data.DataRowView)e.Row.DataItem;
    //        if (string.IsNullOrWhiteSpace(drv["titleName"].ToString().Trim()))
    //        {
    //            SelfTitleGV.EditIndex = e.Row.RowIndex;
    //            //rbl.Items.FindByValue("del").Enabled = false;
    //        }
    //        //gridview嵌套控件
    //        //System.Data.DataRowView drv = (System.Data.DataRowView)e.Row.DataItem;
    //        //RadioButtonList rbl = (RadioButtonList)e.Row.FindControl("RblIsDel");
    //        //if (rbl != null)
    //        //{
    //        //    if (bool.Parse(drv["isDel"].ToString().Trim()))
    //        //    {
    //        //        rbl.Items.FindByValue("del").Selected = true;
    //        //        //rbl.Items.FindByValue("del").Enabled = false;
    //        //    }
    //        //    else
    //        //    {
    //        //        rbl.Items.FindByValue("unDel").Selected = true;
    //        //        //rbl.Items.FindByValue("unDel").Enabled = false;
    //        //    }
    //        //    rbl.Items.FindByValue("del").Enabled = false;
    //        //    rbl.Items.FindByValue("unDel").Enabled = false;
    //        //}

    //        //TextBox ttb = (TextBox)e.Row.FindControl("depNameTxt");
    //        //if (ttb != null)
    //        //{
    //        //    ttb.Text = drv["titleName"].ToString().Trim();
    //        //    ttb.Enabled = false;
    //        //}

    //    }

    //}

    //protected void btnOk_Click(object sender, EventArgs e)
    //{
    //    SelfTitleProcess stp = Session["SelfTitleProcess"] as SelfTitleProcess;

    //    stp.commit();
    //    stp.SelTitleView();

    //    DataTable taskTable = stp.MyDst.Tables["tbl_title"];
    //    //taskTable.DefaultView.RowFilter =
    //    //    "isDel = " + bool.FalseString.ToString().Trim() + " and titleName <> '无' ";
    //    Session["dtSources"] = stp.MyDst.Tables["tbl_title"] as DataTable;
    //    SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

    //    SelfTitleGV.DataBind();

    //    btnOk.Enabled = false;
    //}

    protected string input_check(string titleName)
    {
        DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();
        DataColumn[] key = new DataColumn[1];
        key[0] = dt.Columns["titleName"];

        dt.PrimaryKey = key;

        dt.Rows.Contains(titleName);

        string strRtn = string.Empty;

        if (string.IsNullOrWhiteSpace(titleName))
        {
            strRtn = "职位名称不能为空！";
        }
        else if (titleName.Length > 25)
        {
            strRtn = "职位名称不能超过25个字！";
        }
        else if (dt.Rows.Contains(titleName))
        {
            strRtn = "职位名称不能重复！";
        }
        else if (titleName.Equals("职位名称不能为空！"))
        {
            strRtn = "职位名称不能为空！  ";
        }
        else if (titleName.Equals("职位名称不能超过25个字！"))
        {
            strRtn = "职位名称不能超过25个字！  ";
        }
        else if (titleName.Equals("职位名称不能重复！"))
        {
            strRtn = "职位名称不能重复！  ";
        }
        else
        {
            strRtn = titleName;
        }

        return strRtn;
    }

    protected void btnDel_Click(object sender, EventArgs e)
    {
        int index = SelfTitleGV.SelectedIndex;

        int dataIndex = SelfTitleGV.Rows[index].DataItemIndex;
        DataTable dt = (DataTable)Session["dtSources"];
        int titleId = int.Parse(dt.DefaultView[dataIndex].Row["titleId"].ToString());

        Button btn = null;
        btn = (SelfTitleGV.Rows[index].FindControl("btnUpdate") as Button);
        btn.Visible = false;
        btn = (SelfTitleGV.Rows[index].FindControl("btnCancle") as Button);
        btn.Visible = false;
        btn = sender as Button;
        btn.Visible = false;

        SelfTitleProcess stp = Session["SelfTitleProcess"] as SelfTitleProcess;

        stp.SelfTitleDel(titleId);

        stp.SelfTitleView();

        DataTable taskTable = stp.MyDst.Tables["tbl_title"];
        //taskTable.DefaultView.RowFilter =
        //        "isDel = " + bool.FalseString.ToString().Trim() + " and departmentName <> '无' ";
        Session["dtSources"] = stp.MyDst.Tables["tbl_title"] as DataTable;
        SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

        SelfTitleGV.SelectedIndex = -1;
        SelfTitleGV.EditIndex = -1;
        SelfTitleGV.DataBind();

        btnAdd.Enabled = true;
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int index = SelfTitleGV.SelectedIndex;

        int dataIndex = SelfTitleGV.Rows[index].DataItemIndex;
        DataTable dt = (DataTable)Session["dtSources"];

        GridViewRow row = SelfTitleGV.Rows[index];
        TextBox tbTitleName = row.Cells[1].Controls[0] as TextBox;
        string newTitleName = tbTitleName.Text.ToString().Trim();

        string strCheck = newTitleName;
        newTitleName = input_check(strCheck.Trim());

        if (newTitleName.Equals(strCheck))
        {
            //string oldTitleName = Session["oldTitleName"].ToString();
            //if (!oldTitleName.Equals(newTitleName))
            //{

            int titleId = int.Parse(dt.DefaultView[dataIndex].Row["titleId"].ToString());

            SelfTitleProcess stp = Session["SelfTitleProcess"] as SelfTitleProcess;

            stp.SelfTitleUpdate(titleId, newTitleName);

            stp.SelfTitleView();

            DataTable taskTable = stp.MyDst.Tables["tbl_title"];
            //taskTable.DefaultView.RowFilter =
            //        "isDel = " + bool.FalseString.ToString().Trim() + " and departmentName <> '无' ";
            Session["dtSources"] = stp.MyDst.Tables["tbl_title"] as DataTable;
            //}

            Button btn = null;
            btn = (SelfTitleGV.Rows[index].FindControl("btnDel") as Button);
            btn.Visible = false;
            btn = (SelfTitleGV.Rows[index].FindControl("btnCancle") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;

            SelfTitleGV.SelectedIndex = -1;
            SelfTitleGV.EditIndex = -1;

            SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;  
            SelfTitleGV.DataBind();

            btnAdd.Enabled = true;
        }
        else
        {
            tbTitleName.Text = newTitleName;
            SelfTitleGV.SelectedIndex = index;
            SelfTitleGV.EditIndex = index;
        }
    }
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        int index = SelfTitleGV.SelectedIndex;

        Button btn = null;
        btn = (SelfTitleGV.Rows[index].FindControl("btnUpdate") as Button);
        btn.Visible = false;
        btn = (SelfTitleGV.Rows[index].FindControl("btnCancle") as Button);
        btn.Visible = false;
        btn = sender as Button;
        btn.Visible = false;

        SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

        SelfTitleGV.SelectedIndex = -1;
        SelfTitleGV.EditIndex = -1;
        SelfTitleGV.DataBind();

        btnAdd.Enabled = true;
    }
    
    protected void SelfTitleGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (SelfTitleGV.SelectedIndex == -1)
        {
            e.Cancel = false;
            int index = e.NewSelectedIndex;

            //int dataIndex = SelfTitleGV.Rows[index].DataItemIndex;
            //DataTable dt = (DataTable)Session["dtSources"];
            //Session["oldTitleName"] = dt.DefaultView[dataIndex].Row["titleName"].ToString();

            SelfTitleGV.EditIndex = index;
            SelfTitleGV.DataSource = Session["dtSources"];
            SelfTitleGV.DataBind();

            Button btn = null;
            btn = (SelfTitleGV.Rows[index].FindControl("btnDel") as Button);
            btn.Visible = true;
            btn = (SelfTitleGV.Rows[index].FindControl("btnUpdate") as Button);
            btn.Visible = true;
            btn = (SelfTitleGV.Rows[index].FindControl("btnCancle") as Button);
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
        SelfTitleGV.Enabled = false;
        lblTitleName.Visible = true;
        txtTitleName.Visible = true;
        btnAccept.Visible = true;
        btnNo.Visible = true;

        btnAdd.Enabled = false;
    }
    protected void btnAccept_Click(object sender, EventArgs e)
    {
        SelfTitleProcess stp = Session["SelfTitleProcess"] as SelfTitleProcess;

        string newTitleName = txtTitleName.Text.ToString().Trim();

        string strCheck = newTitleName;

        newTitleName = input_check(strCheck.Trim());
        if (newTitleName.Equals(strCheck))
        {
            stp.SelfTitleAdd(newTitleName);

            stp.SelfTitleView();

            DataTable taskTable = stp.MyDst.Tables["tbl_title"];
            //taskTable.DefaultView.RowFilter =
            //        "isDel = " + bool.FalseString.ToString().Trim() + " and departmentName <> '无' ";
            Session["dtSources"] = stp.MyDst.Tables["tbl_title"] as DataTable;

            SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;  
            SelfTitleGV.DataBind();

            SelfTitleGV.Enabled = true;
            lblTitleName.Visible = false;
            txtTitleName.Text = string.Empty;
            txtTitleName.Visible = false;
            btnAccept.Visible = false;
            btnNo.Visible = false;

            btnAdd.Enabled = true;
        }
        else
        {
            txtTitleName.Text = newTitleName;
        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        SelfTitleGV.Enabled = true;
        lblTitleName.Visible = false;
        txtTitleName.Text = string.Empty;
        txtTitleName.Visible = false;
        btnAccept.Visible = false;
        btnNo.Visible = false;

        btnAdd.Enabled = true;
    }

    protected void SelfTitleGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        SelfTitleGV.PageIndex = e.NewPageIndex;

        SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;  
        SelfTitleGV.DataBind();
    }    
}