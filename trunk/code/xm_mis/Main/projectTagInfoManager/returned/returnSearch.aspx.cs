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
    public partial class returnSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.bor_retApply);
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
                
                string projectDetail = "return";

                Xm_db xmDataCont = Xm_db.GetInstance();

                var projectEdit =
                    from project in xmDataCont.View_project_tag
                    where project.EndTime > DateTime.Now &&
                          project.ProjectDetail == projectDetail
                    select new
                    {
                        project.ProjectTag,
                        project.ProjectSynopsis,
                        project.ProductName,
                        project.ProductTag,
                        project.CustCompName,
                        project.ApplymentUsrName,
                        project.ApproveUsrName,
                        project.Approve,
                        project.ApproveResult,
                        project.StartTime,
                        project.DoneTime
                    };

                DataTable taskTable = projectEdit.Distinct().ToDataTable();
                dt_modify(taskTable, string.Empty);

                //Session["ProjectTagProcess"] = taskTable;
                Session["dtSources"] = taskTable;


                projectInfoGV.DataSource = Session["dtSources"];
                projectInfoGV.DataBind();
            }
        }

        protected void dt_modify(DataTable dt, string strFilter)
        {
            DataColumn colCust = new DataColumn("cust", System.Type.GetType("System.String"));
            dt.Columns.Add(colCust);
            DataColumn colDoneTime = new DataColumn("done", System.Type.GetType("System.String"));
            dt.Columns.Add(colDoneTime);

            dt.DefaultView.RowFilter = strFilter;

            string strCustName = string.Empty;
            string strDoneTime = string.Empty;
            string strNotDone = "未完成";
            DateTime dateTemp;
            foreach (DataRow dr in dt.Rows)
            {                
                strCustName = dr["custCompName"].ToString();
                dateTemp = (DateTime)dr["doneTime"];
                if (string.IsNullOrWhiteSpace(strCustName))
                {
                    dr["cust"] = dr["applymentUsrName"].ToString();
                }
                else
                {
                    dr["cust"] = strCustName;
                }

                if (dateTemp > DateTime.Now)
                {
                    dr["done"] = strNotDone;
                }
                else
                {
                    dr["done"] = dateTemp.ToString();
                }
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
            Response.Redirect("~/Main/projectTagInfoManager/returned/newReturn.aspx");
        }
    }
}