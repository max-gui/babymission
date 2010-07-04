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
    public partial class usrDepartTitleManagerment : System.Web.UI.Page
    {
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

                upView.UsrSelfDepartTitleView();
                sdView.RealDepView();
                stView.RealTitleView();
                DataTable upTable = upView.MyDst.Tables["view_usr_department_title"];
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

                Button btn = null;
                btn = (usrGV.Rows[index].FindControl("btnOk") as Button);
                btn.Visible = true;
                btn = (usrGV.Rows[index].FindControl("btnCancle") as Button);
                btn.Visible = true;
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
                DataTable dt_udt = (Session["upDtSources"] as DataTable).DefaultView.ToTable();
                DataTable dtD = Session["sdDtSources"] as DataTable;
                DataTable dtT = Session["stDtSources"] as DataTable;

                int index = e.Row.DataItemIndex;
                string depId = dt_udt.Rows[index]["departmentId"].ToString();
                string titleId = dt_udt.Rows[index]["titleId"].ToString();

                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlDep");
                if (ddl != null)
                {
                    ddl.DataSource = dtD;
                    ddl.DataTextField = "departmentName".ToString();
                    ddl.DataValueField = "departmentId".ToString();
                    ddl.DataBind();
                    //ddl.Items[0].
                    //if (depTime.CompareTo(DateTime.Now) > 0)
                    //{
                    //    ddl.SelectedValue = depName;
                    //}
                    //else
                    //{
                    //    ddl.SelectedValue = sdNullId;
                    //}
                    ddl.SelectedValue = depId;
                }

                ddl = (DropDownList)e.Row.FindControl("ddlTitle");
                if (ddl != null)
                {
                    ddl.DataSource = dtT;
                    ddl.DataTextField = "titleName".ToString();
                    ddl.DataValueField = "titleId".ToString();
                    ddl.DataBind();

                    ddl.SelectedValue = titleId;
                }
            }
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            int index = usrGV.SelectedIndex;

            DropDownList ddl = null;
            ddl = (usrGV.Rows[index].FindControl("ddlDep") as DropDownList);
            string strDepId = ddl.SelectedValue.ToString();
            ddl.Enabled = false;
            ddl = (usrGV.Rows[index].FindControl("ddlTitle") as DropDownList);
            string strTitleId = ddl.SelectedValue.ToString();
            ddl.Enabled = false;

            Button btn = null;
            btn = (usrGV.Rows[index].FindControl("btnCancle") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;

            UserProcess up = Session["UserProcess"] as UserProcess;

            DataTable dt = (Session["upDtSources"] as DataTable).DefaultView.ToTable();

            int itemIndex = usrGV.Rows[index].DataItemIndex;
            string oldDepId = dt.Rows[itemIndex]["departmentId"].ToString();
            string oldTitleId = dt.Rows[itemIndex]["titleId"].ToString();

            if (!strDepId.Equals(oldDepId))
            {
                int usrDepId = int.Parse(dt.Rows[index]["usrDepId"].ToString());
                int usrId = int.Parse(dt.Rows[index]["usrId"].ToString());
                int depId = int.Parse(strDepId);

                up.SelfUsrDepartUpdate(usrDepId, usrId, depId);
            }
            if (!strTitleId.Equals(oldTitleId))
            {
                int usrTitleId = int.Parse(dt.Rows[index]["usrTitleId"].ToString());
                int usrId = int.Parse(dt.Rows[index]["usrId"].ToString());
                int titleId = int.Parse(strTitleId);

                up.SelfUsrTitleUpdate(usrTitleId, usrId, titleId);
            }

            up.UsrSelfDepartTitleView();
            DataTable upTable = up.MyDst.Tables["view_usr_department_title"];

            Session["upDtSources"] = upTable;
            usrGV.DataSource = Session["upDtSources"];
            usrGV.SelectedIndex = -1;
            usrGV.DataBind();
        }
        protected void usrGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            usrGV.PageIndex = e.NewPageIndex;

            usrGV.DataSource = Session["upDtSources"];//["dtSources"] as DataTable;  
            usrGV.DataBind();
        }
        protected void btnCancle_Click(object sender, EventArgs e)
        {
            int index = usrGV.SelectedIndex;

            DropDownList ddl = null;
            ddl = (usrGV.Rows[index].FindControl("ddlDep") as DropDownList);
            ddl.Enabled = false;
            ddl = (usrGV.Rows[index].FindControl("ddlTitle") as DropDownList);
            ddl.Enabled = false;

            Button btn = null;
            btn = (usrGV.Rows[index].FindControl("btnOk") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;

            usrGV.DataSource = Session["upDtSources"];
            usrGV.SelectedIndex = -1;
            usrGV.DataBind();
        }
    }
}