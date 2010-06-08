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

            UserProcess upView = new UserProcess(usrDst);
            UsrAuthProcess uapView = new UsrAuthProcess(usrAuDst);

            upView.View();
            uapView.View();

            DataTable usrTable = upView.MyDst.Tables["view_usr_info"];
            DataTable usrAuTable = uapView.MyDst.Tables["tbl_usr_authority"];

            //taskTable.DefaultView.RowFilter =
            //    "isDel = " + bool.FalseString.ToString().Trim();
            Session["UsrAuthProcess"] = uapView;
            Session["usrDtSources"] = usrTable;
            Session["usrAuDtSources"] = usrAuTable;
            Session["selUsrId"] = -1;

            List<string> strAuth = new List<string>();
            strAuth.Clear();
            Session["strAuth"] = strAuth;
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
        cblAuth.Visible = true;
    }
    protected void usrGV_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void usrGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //taskTable.DefaultView.RowFilter =
        //    "isDel = " + bool.FalseString.ToString().Trim();
    }
    protected void usrGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {
        int index = e.NewSelectedIndex;

        int diIndex = usrGV.Rows[index].DataItemIndex;

        DataTable dt = Session["usrDtSources"] as DataTable;
        int usrId = int.Parse(dt.Rows[diIndex]["usrId"].ToString().Trim());

        cblAuth_DataInit(usrId);

        usrGV.Enabled = false;
    }
    protected void cblAuth_DataInit(int usrId)
    {
        DataTable dt = Session["usrAuDtSources"] as DataTable;
        dt.DefaultView.RowFilter = "usrId = " + usrId.ToString().Trim();

        Session["selUsrId"] = usrId;
        cblAuth.DataBind();

        cblAuth.Enabled = true;
        //cblAuth.Visible = true;
    }
    protected void cblAuth_DataBinding(object sender, EventArgs e)
    {
        string strIndex = string.Empty;
        DataView dv = (Session["usrAuDtSources"] as DataTable).DefaultView;
        List<string> ls = Session["strAuth"] as List<string>;

        foreach (ListItem li in cblAuth.Items)
        {
            li.Selected = false;
        }

        foreach (DataRowView drv in dv)
        {
            strIndex = drv.Row["authority"].ToString().Trim();
            cblAuth.Items.FindByValue(strIndex).Selected = true;
            ls.Add(strIndex);
        }        

    }

    
    protected void btnOk_Click(object sender, EventArgs e)
    {
        DataTable dt = Session["usrAuDtSources"] as DataTable;
        List<string> ls = Session["strAuth"] as List<string>;

        int usrId = int.Parse(Session["selUsrId"].ToString().Trim());
        
        foreach (ListItem li in cblAuth.Items)
        {
            if (li.Selected)
            {
                if (!ls.Contains(li.Value))
                {
                    DataRow dr = dt.NewRow();
                    dr["authority"] = int.Parse(li.Value);
                    dr["usrId"] = usrId;

                    dt.Rows.Add(dr);
                }
                else
                { 
                }
            }
        }

        UsrAuthProcess uap = Session["UsrAuthProcess"] as UsrAuthProcess;

        uap.commit();
        uap.View();

        Session["usrAuDtSources"] = uap.MyDst.Tables["tbl_usr_authority"] as DataTable;
        
        ls.Clear();

        usrGV.Enabled = true;

        cblAuth.Enabled = false;
    }
    protected void cblAuth_TextChanged(object sender, EventArgs e)
    {
        btnOk.Visible = true;
    }
}