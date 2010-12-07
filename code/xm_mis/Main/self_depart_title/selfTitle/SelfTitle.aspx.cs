using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.self_depart_title.selfTitle
{
    public partial class SelfTitle : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.selfCompany);
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

            if (!IsPostBack)
            {
                DataSet MyDst = new DataSet();
                SelfTitleProcess myView = new SelfTitleProcess(MyDst);

                myView.SelfTitleView();
                DataTable taskTable = myView.MyDst.Tables["tbl_title"];

                Session["SelfTitleProcess"] = myView;
                Session["dtSources"] = taskTable;

                SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
                SelfTitleGV.DataBind();
            }
        }

        protected void SelfTitleGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

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
            else if (titleName.Length > 50)
            {
                strRtn = "职位名称不能超过50个字！";
            }
            else if (dt.Rows.Contains(titleName))
            {
                strRtn = "职位名称不能重复！";
            }
            else if (titleName.Equals("职位名称不能为空！"))
            {
                strRtn = "职位名称不能为空！  ";
            }
            else if (titleName.Equals("职位名称不能超过50个字！"))
            {
                strRtn = "职位名称不能超过50个字！  ";
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
                int titleId = int.Parse(dt.DefaultView[dataIndex].Row["titleId"].ToString());

                SelfTitleProcess stp = Session["SelfTitleProcess"] as SelfTitleProcess;

                stp.SelfTitleUpdate(titleId, newTitleName);

                stp.SelfTitleView();

                DataTable taskTable = stp.MyDst.Tables["tbl_title"];

                Session["dtSources"] = stp.MyDst.Tables["tbl_title"] as DataTable;

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
            Response.Redirect("~/Main/self_depart_title/selfTitle/SelfTitleAdd.aspx");
        }

        protected void SelfTitleGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SelfTitleGV.PageIndex = e.NewPageIndex;

            SelfTitleGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;  
            SelfTitleGV.DataBind();
        }
    }
}