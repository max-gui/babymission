using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.PublicInformation
{
    public partial class projectTagInfo : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                //int usrAuth = 0;
                //string strUsrAuth = Session["totleAuthority"] as string;
                //usrAuth = int.Parse(strUsrAuth);
                //int flag = 0x1 << 7;

                //if ((usrAuth & flag) == 0)
                //    Response.Redirect("~/Main/NoAuthority.aspx");
            }
            else
            {
                string url = Request.FilePath;
                Session["backUrl"] = url;
                Response.Redirect("~/Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                //string usrId = Session["usrId"] as string;

                DataSet MyDst = new DataSet();

                #region projectTagGV
                //ProjectTagProcess projectTagView = new ProjectTagProcess(MyDst);
                
                //string projectDetail = "sell";

                //projectTagView.RealProjTagList(projectDetail);
                //DataTable taskTable = projectTagView.MyDst.Tables["view_project_tag"].DefaultView.ToTable();

                //string strFilter =
                //    " projectDetail = " + "'" + projectDetail + "'";
                //taskTable.DefaultView.RowFilter = strFilter;

                Xm_db xmDataCont = Xm_db.GetInstance();

                var projectView =
                    from project in xmDataCont.View_project_tag
                    where project.EndTime > DateTime.Now &&
                          project.ProjectDetail.Equals("sell")
                    select project;

                DataTable taskTable = projectView.AsEnumerable<View_project_tag>().Distinct<View_project_tag>(new MainIdComparer<View_project_tag>("ProjectTagId")).ToDataTable();

                Session["dtSources"] = taskTable.DefaultView.ToTable();
                
                projectTagGV.DataSource = Session["dtSources"];
                projectTagGV.DataBind();
                #endregion
            }
        }

        protected void projectTagGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void projectTagGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}