using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.projectTagInfoManager.borrowed
{
    public partial class borrowDetail : System.Web.UI.Page
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
                DataRow dr = Session["seldProject"] as DataRow;

                lblProjectTag.Text = dr["projectTag"].ToString();
                lblProjectSynopsis.Text = dr["projectSynopsis"].ToString();
                lblCustCompName.Text = dr["custCompName"].ToString();
                lblCustManName.Text = dr["custManName"].ToString();
                lblProductName.Text = dr["productName"].ToString();
                lblProductTag.Text = dr["productTag"].ToString();
                lblApplymentUsrName.Text = dr["applymentUsrName"].ToString();
                lblStartTime.Text = dr["startTime"].ToString();
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            DataRow dr = Session["seldProject"] as DataRow;

            int projectApproveId = int.Parse(dr["projectApproveId"].ToString());

            Xm_db xmDataContext = Xm_db.GetInstance();
            var projectAppoveEdit =
                (from projectApprove in xmDataContext.Tbl_project_approve
                where projectApprove.ProjectApproveId == projectApproveId
                select projectApprove).First();

            projectAppoveEdit.Approve = bool.TrueString;
            projectAppoveEdit.ApproveResult = "通过";

            try
            {
                //xmDataCont.Refresh(System.Data.Linq.RefreshMode.KeepChanges, xmDataCont.tbl_mainContract);
                //xmDataCont.Refresh(System.Data.Linq.RefreshMode.KeepChanges, xmDataCont.tbl_projectTagInfo);
                xmDataContext.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
            }
            catch (System.Data.Linq.ChangeConflictException cce)
            {
                string strEx = cce.Message;
                foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataContext.ChangeConflicts)
                {
                    //No database values are merged into current.
                    occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                }

                xmDataContext.SubmitChanges();
            }

            Response.Redirect("~/Main/projectTagInfoManager/borrowed/borrowOk.aspx");
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            DataRow dr = Session["seldProject"] as DataRow;

            int projectApproveId = int.Parse(dr["projectApproveId"].ToString());

            Xm_db xmDataContext = Xm_db.GetInstance();
            var projectAppoveEdit =
                (from projectApprove in xmDataContext.Tbl_project_approve
                 where projectApprove.ProjectApproveId == projectApproveId
                 select projectApprove).First();

            projectAppoveEdit.Approve = bool.FalseString;
            projectAppoveEdit.ApproveResult = "未通过";
            int projectId = projectAppoveEdit.ProjectTagId;
            DateTime projectSt = projectAppoveEdit.StartTime;

            var businessProductEdit =
                (from businessProduct in xmDataContext.Tbl_businessProduct
                 where businessProduct.ProjectTagId == projectId
                 select businessProduct).First();

            businessProductEdit.EndTime = DateTime.Now;
            int productStockId = businessProductEdit.ProductStockId;

            var productStockEdit =
                (from productStock in xmDataContext.Tbl_productStock
                 where productStock.ProductStockId == productStockId
                 select productStock).First();

            productStockEdit.ToOut = bool.FalseString;

            var productPurposeRelationEdit =
                (from productPurpose_relation in xmDataContext.Tbl_productPurpose_relation
                 where productPurpose_relation.ProductStockId == productStockId &&
                       productPurpose_relation.EndTime.Equals(projectSt)
                 orderby productPurpose_relation.EndTime descending
                 select productPurpose_relation).First();

            productPurposeRelationEdit.EndTime = DateTime.MaxValue;

            try
            {
                //xmDataCont.Refresh(System.Data.Linq.RefreshMode.KeepChanges, xmDataCont.tbl_mainContract);
                //xmDataCont.Refresh(System.Data.Linq.RefreshMode.KeepChanges, xmDataCont.tbl_projectTagInfo);
                xmDataContext.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
            }
            catch (System.Data.Linq.ChangeConflictException cce)
            {
                string strEx = cce.Message;
                foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataContext.ChangeConflicts)
                {
                    //No database values are merged into current.
                    occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                }

                xmDataContext.SubmitChanges();
            }

            Response.Redirect("~/Main/projectTagInfoManager/borrowed/borrowOk.aspx");
        }

        protected void btnRtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/projectTagInfoManager/borrowed/borrowOk.aspx");
        }
    }
}