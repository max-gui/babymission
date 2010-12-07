using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.projectTagInfoManager.returned
{
    public partial class returnOk : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.bor_retExamine);
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
                string usrId = Session["usrId"] as string;

                //DataSet MyDst = new DataSet();
                //ProjectTagProcess myView = new ProjectTagProcess(MyDst);


                //string projectDetail = "borrow";

                //myView.RealProjTagList(projectDetail);
                //DataTable taskTable = myView.MyDst.Tables["view_project_tag"].DefaultView.ToTable();

                //dt_modify(taskTable, string.Empty);

                //Session["ProjectTagProcess"] = myView;
                //Session["dtSources"] = taskTable;


                //projectInfoGV.DataSource = Session["dtSources"];
                //projectInfoGV.DataBind();

                Xm_db xmDataContext = Xm_db.GetInstance();
                var projectBorrow =
                    from project in xmDataContext.View_project_tag
                    where project.DoneTime > DateTime.Now &&
                            project.EndTime > DateTime.Now &&
                            project.ProjectDetail == "return" &&
                            project.ApproveUsrId == int.Parse(usrId) &&
                            project.Approve.Equals("unDo")
                    select project;
                //List<view_project_tag> temp = query1.ToList<view_project_tag>();
                //DataTable dt = new DataTable();
                //temp[0].GetType().GetProperties()[0].Name
                //foreach (var t in temp)
                //{
                //    if (string.IsNullOrWhiteSpace(t.approveUsrName))
                //    {
                //        t.approveUsrName = t.custCompName;
                //    }
                //}

                //dt.BeginLoadData();
                //dt.LoadDataRow(temp.ToArray<view_project_tag>(), true);
                //dt.EndLoadData();

                DataTable dt = projectBorrow.ToDataTable();

                Session["dtSources"] = dt;

                projectInfoGV.DataSource = dt;
                projectInfoGV.DataBind();
            }
        }

        protected void btnMoreInfo_Click(object sender, EventArgs e)
        {
            GridViewRow dvr = (sender as LinkButton).Parent.Parent as GridViewRow;
            //int index = dvr.RowIndex;
            //int numIndex = projectInfoGV.Rows[index].DataItemIndex;
            int numIndex = dvr.DataItemIndex;
            DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            DataRow dr = dt.Rows[numIndex];

            Session["seldProject"] = dr;

            Response.Redirect("~/Main/projectTagInfoManager/returned/returnDetail.aspx");
        }

        protected void projectInfoGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            projectInfoGV.PageIndex = e.NewPageIndex;

            projectInfoGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;  
            projectInfoGV.DataBind();
        }

        protected void projectInfoGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}