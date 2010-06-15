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
                Response.Redirect("~/Main/NoAuthority.aspx");
        }
        else
        {
            Response.Redirect("~/Account/Login.aspx");
        }

        if (!IsPostBack)
        {
            DataSet usrDst = new DataSet();
            DataSet usrAuDst = new DataSet();
            DataSet auDst = new DataSet();

            UserProcess upView = new UserProcess(usrDst);
            UsrAuthProcess uapView = new UsrAuthProcess(usrAuDst);
            AuthProcess auView = new AuthProcess(auDst);

            upView.UsrSelfDepartTitleView();
            uapView.View();
            auView.View();

            DataTable usrTable = upView.MyDst.Tables["view_usr_department_title"];
            DataTable usrAuTable = uapView.MyDst.Tables["view_usr_autority"];
            DataTable auTable = auView.MyDst.Tables["tbl_authority"];

            //DataColumn[] keys = new DataColumn[2];
            //keys[0] = usrAuTable.Columns[0];
            //keys[1] = usrAuTable.Columns[1];

            //usrAuTable.PrimaryKey = keys;
            //taskTable.DefaultView.RowFilter =
            //    "isDel = " + bool.FalseString.ToString().Trim();
            Session["UsrAuthProcess"] = uapView;
            Session["usrDtSources"] = usrTable;
            Session["usrAuDtSources"] = usrAuTable;
            Session["auTable"] = auTable;
            //Session["selUsrId"] = -1;

            //List<string> strAuth = new List<string>();
            //strAuth.Clear();
            //Session["strAuth"] = strAuth;
            //Session["error"] = bool.FalseString.ToString().Trim();
            //if (Session["PAGESIZE"] != null)
            //{
            //    SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
            //    SelfTitleGV.DataBind();
            //}
            //else
            //{
            //}
            usrGV.DataSource = Session["usrDtSources"];//["dtSources"] as DataTable;
            //cblAuth.DataSource = Session["usrAuDtSources"];
            //string[] strKeyNames = new string[1];
            //strKeyNames[0] = "titleName";
            //SelfTitleGV.DataKeyNames = strKeyNames;
            usrGV.DataBind();

        }
    }
    protected void usrGV_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    
    protected void usrGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        if (-1 == usrGV.SelectedIndex)
        {
            e.Cancel = false;
            int index = e.NewSelectedIndex;
            usrGV.SelectedIndex = index;
            //int itemIndex = usrGV.Rows[index].DataItemIndex;

            //DataTable dt = (Session["usrDtSources"] as DataTable).DefaultView.ToTable();
            //string usrId = dt.Rows[itemIndex]["usrId"].ToString();
            DataTable dtAu = (Session["auTable"] as DataTable).DefaultView.ToTable();

            CheckBoxList cbl = usrGV.Rows[index].FindControl("cblUsrAuth") as CheckBoxList;
            cbl.DataBind();
            //if (cbl != null)
            //{
            //    cbl.DataSource = dtAu;
            //    cbl.DataTextField = "authorityName".ToString();
            //    cbl.DataValueField = "authorityId".ToString();
            //    cbl.DataBind();
            //}
            cbl.Visible = true;

            Button btn = null;
            btn = usrGV.Rows[index].FindControl("btnOk") as Button;
            btn.Visible = true;
            btn = usrGV.Rows[index].FindControl("btnCancle") as Button;
            btn.Visible = true;
            //cblAuth_DataInit(usrId);

            //usrGV.Enabled = false;

            //btnOk.Visible = true;
        }
        else
        {
            e.Cancel = true;
        }
    }
    //protected void cblAuth_DataInit(int usrId)
    //{
    //    DataTable dt = Session["usrAuDtSources"] as DataTable;
    //    dt.DefaultView.RowFilter = "usrId = " + usrId.ToString().Trim();

    //    Session["selUsrId"] = usrId;

    //    cblAuth.DataSource = 
    //    cblAuth.DataTextField = "authorityName";
    //    cblAuth.DataValueField = "authorityId";
    //    cblAuth.DataBind();

    //    cblAuth.Enabled = true;
    //}
    //protected void cblAuth_DataBinding(object sender, EventArgs e)
    //{
    //    string strIndex = string.Empty;
    //    DataView dv = (Session["usrAuDtSources"] as DataTable).DefaultView;
    //    List<string> ls = Session["strAuth"] as List<string>;
    //    ls.Clear();
        
    //    foreach (ListItem li in cblAuth.Items)
    //    {
    //        li.Selected = false;
    //    }

    //    foreach (DataRowView drv in dv)
    //    {
    //        strIndex = drv.Row["authority"].ToString().Trim();
    //        cblAuth.Items.FindByValue(strIndex).Selected = true;
    //        ls.Add(strIndex);
    //    }        

    //}

    //protected void cblAuth_TextChanged(object sender, EventArgs e)
    //{
    //    //btnOk.Visible = true;
    //}
    protected void usrGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataTable dtAu = (Session["auTable"] as DataTable).DefaultView.ToTable();

            CheckBoxList cbl = e.Row.FindControl("cblUsrAuth") as CheckBoxList;
            if (cbl != null)
            {
                cbl.DataSource = dtAu;
                cbl.DataTextField = "authorityName".ToString();
                cbl.DataValueField = "authorityId".ToString();
                cbl.DataBind();
            }
        }
    }
    protected void cblUsrAuth_DataBound(object sender, EventArgs e)
    {
        int index = usrGV.SelectedIndex;

        int minIdex = -1;
        if (index > minIdex)
        {
            int itemIndex = usrGV.Rows[index].DataItemIndex;

            DataTable dt = (Session["usrDtSources"] as DataTable).DefaultView.ToTable();
            string usrId = dt.Rows[itemIndex]["usrId"].ToString();

            DataTable dtUsrAu = (Session["usrAuDtSources"] as DataTable).DefaultView.ToTable();
            dtUsrAu.DefaultView.RowFilter = "usrId = " + usrId;
            DataView dv = dtUsrAu.DefaultView;

            CheckBoxList cbl = sender as CheckBoxList;

            string authId = string.Empty;
            foreach (DataRowView drv in dv)
            {
                authId = drv.Row["authorityId"].ToString();
                cbl.Items.FindByValue(authId).Selected = true;
            }
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        UsrAuthProcess uap = Session["UsrAuthProcess"] as UsrAuthProcess;

        int index = usrGV.SelectedIndex;
        int itemIndex = usrGV.Rows[index].DataItemIndex;

        DataTable dt = (Session["usrDtSources"] as DataTable).DefaultView.ToTable();
        string usrId = dt.Rows[itemIndex]["usrId"].ToString();

        DataTable dtUsrAu = (Session["usrAuDtSources"] as DataTable).DefaultView.ToTable();
        
        DataColumn[] keys = new DataColumn[2];
        keys[0] = dtUsrAu.Columns["usrId"];
        keys[1] = dtUsrAu.Columns["authorityId"];

        dtUsrAu.PrimaryKey = keys;

        CheckBoxList cbl = usrGV.Rows[index].FindControl("cblUsrAuth") as CheckBoxList;

        object[] findKeys = new object[2];
        findKeys[0] = usrId;
        DataRow dr = null;
        int usrAuhId = -1;
        string authId = string.Empty;
        foreach (ListItem li in cbl.Items)
        {
            findKeys[1] = li.Value;
            dr = dtUsrAu.Rows.Find(findKeys);
            if (li.Selected)
            {
                if (null == dr)
                {
                    //add
                    uap.UsrAuthAdd(int.Parse(usrId), int.Parse(li.Value));
                    //DataRow dr = dt.NewRow();
                    //dr["authority"] = int.Parse(li.Value);
                    //dr["usrId"] = usrId;

                    //dt.Rows.Add(dr);
                }
                else
                {
                }
            }
            else
            {
                if (null != dr)
                {
                    //del
                    usrAuhId = int.Parse(dr["usrAuhId"].ToString());
                    uap.UsrAuthDel(usrAuhId);
                    //DataRow dr = dt.NewRow();
                    //dr["authority"] = int.Parse(li.Value);
                    ////dr["usrId"] = usrId;

                    //object[] findVals = new object[2];
                    //findVals[0] = usrId;
                    //findVals[1] = int.Parse(li.Value);

                    //dr = dt.Rows.Find(findVals);
                    //dr.Delete();
                }
                else
                {
                }
            }
        }

        uap.View();
        
        DataTable usrAuTable = uap.MyDst.Tables["view_usr_autority"];

        Session["usrAuDtSources"] = usrAuTable;

        //ls.Clear();
        usrGV.SelectedIndex = -1;

        cbl.Visible = false;

        Button btn = null;
        btn = btn = usrGV.Rows[index].FindControl("btnCancle") as Button;
        btn.Visible = false;
        btn = sender as Button;
        btn.Visible = false;
    }
    protected void btnCancle_Click(object sender, EventArgs e)
    {
        int index = usrGV.SelectedIndex;


        CheckBoxList cbl = usrGV.Rows[index].FindControl("cblUsrAuth") as CheckBoxList;
        cbl.Visible = false;

        Button btn = null;
        btn = usrGV.Rows[index].FindControl("btnOk") as Button;
        btn.Visible = false;
        btn = usrGV.Rows[index].FindControl("btnCancle") as Button;
        btn.Visible = false;

        //usrGV.DataSource = Session["upDtSources"];
        usrGV.SelectedIndex = -1;
        //usrGV.DataBind();
    }
    protected void usrGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        usrGV.PageIndex = e.NewPageIndex;

        usrGV.DataSource = Session["usrDtSources"];//["dtSources"] as DataTable;  
        usrGV.DataBind();
    }
}