using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.App_Code.logic;
namespace xm_mis.Main.custInfoManager.custCompManager
{
    public partial class custCompEdit : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                DataSet MyDst = new DataSet();
                custCompProcess myView = new custCompProcess(MyDst);

                myView.RealCompView();
                DataTable taskTable = myView.MyDst.Tables["tbl_customer_company"];

                Session["custCompProcess"] = myView;
                Session["dtSources"] = taskTable;


                custCompGV.DataSource = Session["dtSources"];
                custCompGV.DataBind();
            }
        }

        protected void btnCustManEdit_Click(object sender, EventArgs e)
        {
            int numIndex = custCompGV.SelectedRow.DataItemIndex;

            DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            Session["selCustCompDt"] = dt;

            Response.Redirect("~/Main/custInfoManager/custCompManager/custCompEditing.aspx");
        }

        protected void custCompGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            custCompGV.PageIndex = e.NewPageIndex;

            custCompGV.DataSource = Session["dtSources"];
            custCompGV.DataBind();
        }

        protected void custCompGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (custCompGV.SelectedIndex == -1)
            {
                e.Cancel = false;
                int index = e.NewSelectedIndex;

                custCompGV.EditIndex = index;
                custCompGV.DataSource = Session["dtSources"];
                custCompGV.DataBind();

                Button btn = null;
                btn = (custCompGV.Rows[index].FindControl("btnDel") as Button);
                btn.Visible = true;
                btn = (custCompGV.Rows[index].FindControl("btnUpdate") as Button);
                btn.Visible = true;
                btn = (custCompGV.Rows[index].FindControl("btnCancle") as Button);
                btn.Visible = true;
                btn = (custCompGV.Rows[index].FindControl("btnCustManEdit") as Button);
                btn.Visible = true;
            }
            else
            {
                e.Cancel = true;
            }

            btnAdd.Enabled = false;
        }

        protected void custCompGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            int dataIndex = custCompGV.SelectedRow.DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];
            int depId = int.Parse(dt.DefaultView[dataIndex].Row["custCompyId"].ToString());

            Button btn = null;
            btn = (custCompGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = (custCompGV.SelectedRow.FindControl("btnCancle") as Button);
            btn.Visible = false;
            btn = (custCompGV.SelectedRow.FindControl("btnCustManEdit") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;

            custCompProcess ccp = Session["custCompProcess"] as custCompProcess;

            ccp.SelfDepDel(depId);

            ccp.SelDepView();

            DataTable taskTable = ccp.MyDst.Tables["tbl_department"];

            Session["dtSources"] = ccp.MyDst.Tables["tbl_department"] as DataTable;
            custCompGV.DataSource = Session["dtSources"];

            custCompGV.SelectedIndex = -1;
            custCompGV.EditIndex = -1;
            custCompGV.DataBind();

            btnAdd.Enabled = true;
        }
    }
}