using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.stockInfoManager.productOutManager.productOutRelationManager
{
    public partial class sellProductStockOutView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.sellProductRelation);
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

                #region productStockRelationTable

                DataSet myDst = new DataSet();
                ProductPurposeRelationProcess pprpView = new ProductPurposeRelationProcess(myDst);

                Session["ProductPurposeRelationProcess"] = pprpView;
                pprpView.RealProductPurposeRelationView();

                DataTable productStockRelationTable = pprpView.MyDst.Tables["view_productStockRelation"].DefaultView.ToTable();

                string strFilter =
                    " productPurpose = " + "'" + "forSell".ToString() + "'";
                productStockRelationTable.DefaultView.RowFilter = strFilter;
                Session["dtSources"] = productStockRelationTable.DefaultView.ToTable();
                
                sellProductGV.DataSource = Session["dtSources"];
                sellProductGV.DataBind();

                #endregion
            }
        }

        protected void outProduct_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (sender as LinkButton).Parent.Parent as GridViewRow;

            int index = gvr.DataItemIndex;

            DataTable dt = Session["dtSources"] as DataTable;

            string strProductId = dt.Rows[index]["productId"].ToString();
            string strSupplierId = dt.Rows[index]["supplierId"].ToString();
            string productPurposeRelationId = dt.Rows[index]["productPurposeRelationId"].ToString();
            string productStockId = dt.Rows[index]["productStockId"].ToString();
            string productName = dt.Rows[index]["productName"].ToString();
            string productTag = dt.Rows[index]["productTag"].ToString();

            sellProductGV.Visible = false;

            #region projectNeedGV
            Xm_db xmDataCont = Xm_db.GetInstance();

            int productId = int.Parse(strProductId);
            int supplierId = int.Parse(strSupplierId);
            var project_need_productView =
                from project_need_product in xmDataCont.View_project_need_product
                where project_need_product.SubContractEd > DateTime.Now &&
                      project_need_product.SubContractProductEd > DateTime.Now &&
                      project_need_product.HasSupplied < project_need_product.ProductNum &&
                      project_need_product.ProductId == productId &&
                      project_need_product.SupplierId == supplierId
                select new 
                { 
                    project_need_product.ProjectTag, 
                    project_need_product.MainContractTag, 
                    project_need_product.CustCompName,
                    project_need_product.DateLine,
                    project_need_product.SubContractProductId,
                    project_need_product.ProjectTagId
                };

            //DataSet myDst = new DataSet();
            //MainContractProcess mcpView = new MainContractProcess(myDst);

            //mcpView.RealProjectNeedProductView();
            //DataTable taskTable = mcpView.MyDst.Tables["view_project_need_product"].DefaultView.ToTable();
            DataTable taskTable = project_need_productView.Distinct().ToDataTable();
            
            DataColumn colPprId = new DataColumn("productPurposeRelationId", System.Type.GetType("System.String"));
            DataColumn colPsId = new DataColumn("productStockId", System.Type.GetType("System.String"));
            DataColumn colPn = new DataColumn("productName", System.Type.GetType("System.String"));
            DataColumn colPt = new DataColumn("productTag", System.Type.GetType("System.String"));
            taskTable.Columns.Add(colPprId);
            taskTable.Columns.Add(colPsId);
            taskTable.Columns.Add(colPn);
            taskTable.Columns.Add(colPt);
            
            foreach (DataRow dr in taskTable.Rows)
            {
                dr["productPurposeRelationId"] = productPurposeRelationId;
                dr["productStockId"] = productStockId;
                dr["productName"] = productName;
                dr["productTag"] = productTag;
            }

            Session["view_project_need_product"] = taskTable;

            if (taskTable.DefaultView.Count <= 0)
            {
                lblNoOut.Visible = true;
            }
            else
            {
                projectNeedGV.DataSource = Session["view_project_need_product"];
                projectNeedGV.DataBind();

                projectNeedGV.Visible = true;
            }

            btnNullRtn.Visible = true;
            #endregion
        }

        protected void outView_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (sender as LinkButton).Parent.Parent as GridViewRow;

            int index = gvr.DataItemIndex;

            DataTable dt = Session["view_project_need_product"] as DataTable;

            string projectTagId = dt.Rows[index]["projectTagId"].ToString();
            string productPurposeRelationId = dt.Rows[index]["productPurposeRelationId"].ToString();
            string subContractProductId = dt.Rows[index]["subContractProductId"].ToString();
            string productStockId = dt.Rows[index]["productStockId"].ToString();
            string strProductName = dt.Rows[index]["productName"].ToString();
            string strProductTag = dt.Rows[index]["productTag"].ToString();

            DataSet ds = new DataSet();
            ds.Tables.Add(dt.DefaultView.ToTable("addTable"));
            SellProductProcess spp = new SellProductProcess(ds);

            spp.MyDst.Tables["addTable"].Clear();

            DataRow dr = spp.MyDst.Tables["addTable"].NewRow();
            dr["projectTagId"] = projectTagId;
            dr["productPurposeRelationId"] = productPurposeRelationId;
            dr["subContractProductId"] = subContractProductId;
            dr["productStockId"] = productStockId;

            spp.MyDst.Tables["addTable"].Rows.Add(dr);
            dt.Clear();

            spp.Add();

            Xm_db xmDataCont = Xm_db.GetInstance();

            //var usr_autority =
            //    from usr in xmDataCont.Tbl_usr
            //    join auth in xmDataCont.View_usr_autority on usr.UsrId equals auth.UsrId
            //    where auth.AuthorityId == 20 &&
            //          auth.UsrAuEnd > DateTime.Now
            //    select usr;

            //int flag = 0x100;
            var usr_autority =
                from usr in xmDataCont.Tbl_usr
                //join auth in xmDataCont.View_usr_autority on usr.UsrId equals auth.UsrId
                where (usr.TotleAuthority & (UInt32)AuthAttributes.stockManager) != 0 &&
                      usr.EndTime > DateTime.Now
                select usr;
                //where usr.TotleAuthority.ToAuthAttr().HasOneFlag(AuthAttributes.stockManager) &&
                //      usr.EndTime > DateTime.Now
                //select usr;

            foreach (var usr in usr_autority)
            {
                BeckSendMail.getMM().NewMail(usr.UsrEmail,
                    "mis系统出货准备通知", 
                    "编号为" + strProductTag + "的" + strProductName + ",等待您为其办理出货手续" +
                    System.Environment.NewLine + Request.Url.toNewUrlForMail("/Main/stockInfoManager/productStockView/productStockDetail.aspx"));
            }

            lblNoOut.Visible = false;
            btnNullRtn.Visible = false;

            sellProductGV.Visible = true;
            projectNeedGV.Visible = false;

            ProductPurposeRelationProcess pprpView = Session["ProductPurposeRelationProcess"] as ProductPurposeRelationProcess;

            pprpView.RealProductPurposeRelationView();

            DataTable productStockRelationTable = pprpView.MyDst.Tables["view_productStockRelation"].DefaultView.ToTable();

            string strFilter =
                " productPurpose = " + "'" + "forSell".ToString() + "'";
            productStockRelationTable.DefaultView.RowFilter = strFilter;
            Session["dtSources"] = productStockRelationTable.DefaultView.ToTable();

            sellProductGV.DataSource = Session["dtSources"];
            sellProductGV.DataBind();
        }

        protected void btnRtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/stockInfoManager/productOutManager/productOutRelationManager/sellProductStockOutList.aspx");
        }

        protected void projectNeedGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            projectNeedGV.PageIndex = e.NewPageIndex;

            projectNeedGV.DataSource = Session["dtSources"];
            projectNeedGV.DataBind();
        }

        protected void projectNeedGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void sellProductGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            sellProductGV.PageIndex = e.NewPageIndex;

            sellProductGV.DataSource = Session["dtSources"];
            sellProductGV.DataBind();
        }

        protected void sellProductGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnNullRtn_Click(object sender, EventArgs e)
        {
            lblNoOut.Visible = false;
            btnNullRtn.Visible = false;

            sellProductGV.Visible = true;
            projectNeedGV.Visible = false;
        }
    }
}