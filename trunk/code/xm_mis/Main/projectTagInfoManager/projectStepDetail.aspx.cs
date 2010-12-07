using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.db;
using xm_mis.logic;
namespace xm_mis.Main.projectTagInfoManager
{
    public partial class projectStepDetail : System.Web.UI.Page
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

            if (null == Session["projectTagId"])
            {
                Response.Redirect("~/Main/projectTagInfoManager/projectStepSearch.aspx");
            }

            if (!IsPostBack)
            {
                int projectTagId = int.Parse(Session["projectTagId"] as string);

                Xm_db xmDataCont = Xm_db.GetInstance();

                DateTime today = DateTime.Now;

                var projectStepEdit =
                    from projectStep in xmDataCont.View_project_step
                    where projectStep.ProjectTagId == projectTagId &&
                          (projectStep.MainContractEd.HasValue ? projectStep.MainContractEd > today : true) &&
                          (projectStep.SubContractEd.HasValue ? projectStep.SubContractEd > today : true) &&
                          (projectStep.ToOutProductEd.HasValue ? projectStep.ToOutProductEd > today : true)
                    select projectStep;

                //string strSplite = "%";
                var projectStepEditElement = projectStepEdit.First();
                lblProjectSt.Text = projectStepEditElement.ProjectSt.ToString();
                lblProjectTag.Text = projectStepEditElement.ProjectTag;
                lblProjectSynopsis.Text = projectStepEditElement.ProjectSynopsis;
                lblReceiptPercent.Text = projectStepEditElement.ReceiptPercent.HasValue ? projectStepEditElement.ReceiptPercent.Value.ToString("p") : string.Empty;
                lblSelfReceiptPercent.Text = projectStepEditElement.SelfReceiptPercent.HasValue ? projectStepEditElement.SelfReceiptPercent.Value.ToString("p") : string.Empty;
                lblReceivingPercent.Text = projectStepEditElement.ReceivingPercent.HasValue ? projectStepEditElement.ReceivingPercent.Value.ToString("p") : string.Empty;
                lblSelfReceivingPercent.Text = projectStepEditElement.SelfReceivingPercent.HasValue ? projectStepEditElement.SelfReceivingPercent.Value.ToString("p") : string.Empty;

                var projProdEdit =
                    from projProd in projectStepEdit
                    let oweProductNum = projProd.ProductNum - projProd.HasSupplied
                    let toOutProductNum = projectStepEdit.Count(elementTemp => elementTemp.ToOutProductId == elementTemp.ContractProductId)
                    select new { projProd.ContractProductName, projProd.HasSupplied, oweProductNum, toOutProductNum};

                Session["dtSources"] = projProdEdit.Distinct().ToDataTable();

                projProdInfoGV.DataSource = Session["dtSources"];
                projProdInfoGV.DataBind();
            } 
        }

        protected void projProdInfoGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            projProdInfoGV.PageIndex = e.NewPageIndex;
            projProdInfoGV.DataSource = Session["dtSources"];
            projProdInfoGV.DataBind();
        }

        protected void projProdInfoGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void brnRtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/projectTagInfoManager/projectStepSearch.aspx");
        }
    }
}