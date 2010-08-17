using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.self_depart_title.selfDep
{
    public partial class SelfDepartment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                int usrAuth = 0;
                string strUsrAuth = Session["totleAuthority"] as string;
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
                SelfDepartProcess myView = new SelfDepartProcess(MyDst);

                myView.SelDepView();
                DataTable taskTable = myView.MyDst.Tables["tbl_department"];

                Session["SelfDepartProcess"] = myView;
                Session["dtSources"] = taskTable;


                SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
                SelfDepartGV.DataBind();
            }
        }

        protected void SelfDepartGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected string input_check(string depName)
        {
            DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();
            DataColumn[] key = new DataColumn[1];
            key[0] = dt.Columns["departmentName"];

            dt.PrimaryKey = key;

            dt.Rows.Contains(depName);

            string strRtn = string.Empty;

            if (string.IsNullOrWhiteSpace(depName))
            {
                strRtn = "部门名称不能为空！";
            }
            else if (depName.Length > 25)
            {
                strRtn = "部门名称不能超过25个字！";
            }
            else if (dt.Rows.Contains(depName))
            {
                strRtn = "部门名称不能重复！";
            }
            else if (depName.Equals("部门名称不能为空！"))
            {
                strRtn = "部门名称不能为空！  ";
            }
            else if (depName.Equals("部门名称不能超过25个字！"))
            {
                strRtn = "部门名称不能超过25个字！  ";
            }
            else if (depName.Equals("部门名称不能重复！"))
            {
                strRtn = "部门名称不能重复！  ";
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
            btn = (SelfDepartGV.Rows[index].FindControl("btnCancle") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;

            SelfDepartProcess sdp = Session["SelfDepartProcess"] as SelfDepartProcess;

            sdp.SelfDepDel(depId);

            sdp.SelDepView();

            DataTable taskTable = sdp.MyDst.Tables["tbl_department"];

            Session["dtSources"] = sdp.MyDst.Tables["tbl_department"] as DataTable;
            SelfDepartGV.DataSource = Session["dtSources"];

            SelfDepartGV.SelectedIndex = -1;
            SelfDepartGV.EditIndex = -1;
            SelfDepartGV.DataBind();

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
                int depId = int.Parse(dt.DefaultView[dataIndex].Row["departmentId"].ToString());

                SelfDepartProcess sdp = Session["SelfDepartProcess"] as SelfDepartProcess;

                sdp.SelfDepUpdate(depId, newDepName);

                sdp.SelDepView();

                DataTable taskTable = sdp.MyDst.Tables["tbl_department"];
                Session["dtSources"] = sdp.MyDst.Tables["tbl_department"] as DataTable;

                Button btn = null;
                btn = (SelfDepartGV.Rows[index].FindControl("btnDel") as Button);
                btn.Visible = false;
                btn = (SelfDepartGV.Rows[index].FindControl("btnCancle") as Button);
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
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            int index = SelfDepartGV.SelectedIndex;

            Button btn = null;
            btn = (SelfDepartGV.Rows[index].FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = (SelfDepartGV.Rows[index].FindControl("btnCancle") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;

            SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

            SelfDepartGV.SelectedIndex = -1;
            SelfDepartGV.EditIndex = -1;
            SelfDepartGV.DataBind();

            btnAdd.Enabled = true;
        }

        protected void SelfDepartGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (SelfDepartGV.SelectedIndex == -1)
            {
                e.Cancel = false;
                int index = e.NewSelectedIndex;

                SelfDepartGV.EditIndex = index;
                SelfDepartGV.DataSource = Session["dtSources"];
                SelfDepartGV.DataBind();

                Button btn = null;
                btn = (SelfDepartGV.Rows[index].FindControl("btnDel") as Button);
                btn.Visible = true;
                btn = (SelfDepartGV.Rows[index].FindControl("btnUpdate") as Button);
                btn.Visible = true;
                btn = (SelfDepartGV.Rows[index].FindControl("btnCancle") as Button);
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
            Response.Redirect("~/Main/self_depart_title/selfDep/SelfDepAdd.aspx");
        }

        protected void SelfDepartGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SelfDepartGV.PageIndex = e.NewPageIndex;

            SelfDepartGV.DataSource = Session["dtSources"];
            SelfDepartGV.DataBind();
        }
    }
}