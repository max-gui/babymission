using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
using xm_mis.Main;
namespace xm_mis.Main.projectTagInfoManager
{
    public partial class projectStepSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.projectStepView);
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
                List<string> lstCtlName = new List<string>(4);

                lstCtlName.Add("supplierOk");
                lstCtlName.Add("receiptOk");
                lstCtlName.Add("receivingOk");
                lstCtlName.Add("productOk");

                Session["lstCtlName"] = lstCtlName;

                Xm_db xmDataCont = Xm_db.GetInstance();

                var projectStepEdit =
                    from projectStep in xmDataCont.View_project_step
                    where projectStep.ProjectEd > DateTime.Now && 
                          projectStep.MainContractEd > DateTime.Now &&
                          projectStep.ProjectDetail.Equals("sell")
                    select new { projectStep.ProjectTagId, projectStep.ProjectTag, projectStep.SupplierOk, projectStep.ReceivingOk, projectStep.ReceiptOk, projectStep.ProductOk };

                DataTable dtSources = projectStepEdit.Distinct().ToDataTable();

                Session["dtSources"] = dtSources;

                this.projectInfoGV.DataSource = dtSources;
                projectInfoGV.DataBind();

                //MailManager p = MailManager.getMM();

                //Class1.connectMM(p);

                //p.SimulateNewMail("vampiler@163.com", "这是测试邮件", "邮件内容");
                //BeckSendMail.getMM().NewMail("vampiler@163.com", "这是测试邮件", "邮件内容");
            }
        }

        protected void btnMoreInfo_Click(object sender, EventArgs e)
        {
            GridViewRow dvr = (sender as LinkButton).Parent.Parent as GridViewRow;

            int index = dvr.DataItemIndex;

            DataTable dtSources = Session["dtSources"] as DataTable;

            string projectTagId = dtSources.Rows[index]["projectTagId"].ToString();

            Session["projectTagId"] = projectTagId;

            Response.Redirect("~/Main/projectTagInfoManager/projectStepDetail.aspx");
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

        protected void projectInfoGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtSources = Session["dtSources"] as DataTable;

                int index = e.Row.DataItemIndex;
                DataRow dr = dtSources.Rows[index];

                List<string> lstCtlName = Session["lstCtlName"] as List<string>;

                TableCell tcl = null;
                foreach (string ctlName in lstCtlName)
                {
                    tcl = e.Row.FindControl(ctlName).Parent as TableCell;
                    if (dr[ctlName].ToString().Equals(bool.TrueString))
                    {
                        tcl.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Green);
                    }
                    else if (dr[ctlName].ToString().Equals(bool.FalseString))
                    {
                        tcl.BackColor = System.Drawing.Color.FromKnownColor(System.Drawing.KnownColor.Red);
                    }
                    else
                    { 
                        
                    }
                }
            }
        }
    }
}