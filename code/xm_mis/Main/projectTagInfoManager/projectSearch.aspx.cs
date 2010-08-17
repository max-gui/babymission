using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.projectTagInfoManager
{
    public partial class projectSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                int usrAuth = 0;
                string strUsrAuth = Session["totleAuthority"] as string;
                usrAuth = int.Parse(strUsrAuth);
                int flag = 0x1 << 4;

                if ((usrAuth & flag) == 0)
                    Response.Redirect("~/Main/NoAuthority.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                string usrId = Session["usrId"] as string;

                DataSet MyDst = new DataSet();
                ProjectTagProcess myView = new ProjectTagProcess(MyDst);

                myView.RealProjTagView(usrId);
                DataTable taskTable = myView.MyDst.Tables["view_project_tag"];

                Session["ProjectTagProcess"] = myView;
                Session["dtSources"] = taskTable;


                projectInfoGV.DataSource = Session["dtSources"];
                projectInfoGV.DataBind();
            }
        }

        protected void projectInfoGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            projectInfoGV.PageIndex = e.NewPageIndex;

            projectInfoGV.DataSource = Session["dtSources"];
            projectInfoGV.DataBind();
        }

        protected void projectInfoGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}