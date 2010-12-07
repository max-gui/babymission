using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.projectTagInfoManager
{
    public partial class projectSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                //int usrAuth = 0;
                //string strUsrAuth = Session["totleAuthority"] as string;
                //usrAuth = int.Parse(strUsrAuth);
                
                //int flag = 0x1 << 3;

                //if ((usrAuth & flag) == 0)
                //    Response.Redirect("~/Main/NoAuthority.aspx");

                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];
                
                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.projectTagApply);
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
                string projectDetail = "sell";
                
                Xm_db xmDataCont = Xm_db.GetInstance();

                var projectTagEdit =
                    from projectTag in xmDataCont.View_project_tag
                    where projectTag.EndTime > DateTime.Now &&
                          projectTag.ProjectDetail.Equals(projectDetail)
                    select new 
                    {   projectTag.ProjectTag, 
                        projectTag.ProjectSynopsis, 
                        projectTag.CustCompName, 
                        projectTag.CustCompAddress, 
                        projectTag.CustManName, 
                        projectTag.CustManContact, 
                        projectTag.CustManEmail, 
                        projectTag.ApplymentUsrName, 
                        projectTag.ApplymentUsrMobile, 
                        projectTag.StartTime 
                    };

                //DataSet MyDst = new DataSet();
                //ProjectTagProcess myView = new ProjectTagProcess(MyDst);

                //string projectDetail = "sell";

                //myView.RealProjTagList(projectDetail);
                //DataTable taskTable = myView.MyDst.Tables["view_project_tag"];

                DataTable taskTable = projectTagEdit.Distinct().ToDataTable();

                //Session["ProjectTagProcess"] = myView;
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/projectTagInfoManager/projectTagAdd.aspx");
        }
    }
}