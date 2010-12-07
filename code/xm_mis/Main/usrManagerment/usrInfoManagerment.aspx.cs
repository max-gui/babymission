using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.usrManagerment
{
    public partial class usrInfoManagerment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.systemManager);
                if (!flag)
                {
                    Response.Redirect("~/Main/NoAuthority.aspx");
                }
            }
            else
            {
                string url = Request.FilePath;
                Session["backUrl"] = url;
                Response.Redirect("~/Account/Login.aspx");
            }

            //DataSet MyDst = new DataSet();
            //SelfDepartProcess myView = new SelfDepartProcess(MyDst);


            if (!IsPostBack)
            {
                DataSet upDst = new DataSet();
                UserProcess upView = new UserProcess(upDst);

                upView.UsrSelfDepartTitleView();
                DataTable upTable = upView.MyDst.Tables["view_usr_department_title"];

                Session["UserProcess"] = upView;
                Session["upDtSources"] = upTable;
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
        protected void usrGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            usrGV.SelectedIndex = e.RowIndex;
            usrGV.Enabled = false;

            Button btn = null;
            btn = btnDel;
            btn.Visible = true;
            btn = btnCancel;
            btn.Visible = true;
            btn = btnAdd;
            btn.Visible = false;
        }
        protected void usrGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void usrGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            usrGV.PageIndex = e.NewPageIndex;

            usrGV.DataSource = Session["upDtSources"];//["dtSources"] as DataTable;  
            usrGV.DataBind();
        }
        protected void usrGV_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Button btn = null;
            btn = btnDel;
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;
            btn = btnAdd;
            btn.Visible = true;

            usrGV.DataSource = Session["upDtSources"];//["dtSources"] as DataTable;

            usrGV.SelectedIndex = -1;
            usrGV.DataBind();
            usrGV.Enabled = true;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Account/Register.aspx");
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            int index = usrGV.SelectedIndex;
            int itemIdex = usrGV.Rows[index].DataItemIndex;

            DataTable dt = (Session["upDtSources"] as DataTable).DefaultView.ToTable();
            string usrId = dt.Rows[itemIdex]["usrId"].ToString();

            UserProcess up = Session["UserProcess"] as UserProcess;

            up.usrDel(usrId);

            up.UsrSelfDepartTitleView();
            DataTable upTable = up.MyDst.Tables["view_usr_department_title"];
            Session["upDtSources"] = upTable;

            usrGV.DataSource = Session["upDtSources"];
            usrGV.DataBind();

            Button btn = sender as Button;
            btn.Visible = false;
            btn = btnCancel;
            btn.Visible = false;
            btn = btnAdd;
            btn.Visible = true;

            usrGV.SelectedIndex = -1;
            usrGV.Enabled = true;
        }
    }
}