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
    public partial class returnProductStockOutView : System.Web.UI.Page
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
                    " productPurpose <> " + "'" + "forSell".ToString() + "'";
                productStockRelationTable.DefaultView.RowFilter = strFilter;
                Session["view_productStockRelation"] = productStockRelationTable.DefaultView.ToTable();

                returnProductGV.DataSource = Session["view_productStockRelation"];
                returnProductGV.DataBind();

                #endregion
            }
        }

        protected void outProduct_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (sender as LinkButton).Parent.Parent as GridViewRow;

            int index = gvr.DataItemIndex;

            DataTable dt = Session["view_productStockRelation"] as DataTable;

            string strProductId = dt.Rows[index]["productId"].ToString();
            string productTag = dt.Rows[index]["productTag"].ToString();
            string productPurposeRelationId = dt.Rows[index]["productPurposeRelationId"].ToString();
            string productPurpose = dt.Rows[index]["productPurpose"].ToString();
            string productStockId = dt.Rows[index]["productStockId"].ToString();

            //dt.Rows[index][]
            returnProductGV.Visible = false;

            #region projectNeedGV
            Xm_db xmDataCont = Xm_db.GetInstance();

            int productId = int.Parse(strProductId);
            DateTime dateLimit = DateTime.Now;

            var returned_need_productEdit =
                from returned_need_product in xmDataCont.View_returned_need_product
                where returned_need_product.ProductId == productId &&
                      returned_need_product.ProductTag == productTag &&
                      returned_need_product.EndTime > dateLimit &&
                      returned_need_product.DoneTime > dateLimit &&
                      returned_need_product.ToOut == bool.FalseString
                select returned_need_product;

            DataTable taskTable = returned_need_productEdit.ToDataTable();

            DataColumn colPprId = new DataColumn("productPurposeRelationId", System.Type.GetType("System.String"));
            DataColumn colPsId = new DataColumn("seldProductStockId", System.Type.GetType("System.String"));
            DataColumn colPp = new DataColumn("productPurpose", System.Type.GetType("System.String"));
            taskTable.Columns.Add(colPprId);
            taskTable.Columns.Add(colPsId);
            taskTable.Columns.Add(colPp);

            foreach (DataRow dr in taskTable.Rows)
            {
                dr["productPurposeRelationId"] = productPurposeRelationId;
                dr["seldProductStockId"] = productStockId;
                dr["productPurpose"] = productPurpose;
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

            string strProjectTagId = dt.Rows[index]["projectTagId"].ToString();
            string strProductPurposeRelationId = dt.Rows[index]["productPurposeRelationId"].ToString();
            string strProductStockId = dt.Rows[index]["seldProductStockId"].ToString();
            string productPurpose = dt.Rows[index]["productPurpose"].ToString();

            Xm_db xmDataCont = Xm_db.GetInstance();
            int projectTagId = int.Parse(strProjectTagId);                
                
            if (productPurpose.Equals("forReturned"))
            {
                Tbl_businessProduct busProd = new Tbl_businessProduct();

                int productStockId = int.Parse(strProductStockId);

                busProd.ProjectTagId = projectTagId;
                busProd.ProductStockId = productStockId;
                busProd.StartTime = DateTime.Now;
                busProd.EndTime = DateTime.MaxValue;

                xmDataCont.Tbl_businessProduct.InsertOnSubmit(busProd);

                int productPurposeRelationId = int.Parse(strProductPurposeRelationId);

                var productPurpose_relationEdit =
                    (from productPurpose_relation in xmDataCont.Tbl_productPurpose_relation
                     where productPurpose_relation.ProductPurposeRelationId == productPurposeRelationId
                     select productPurpose_relation).First();

                productPurpose_relationEdit.EndTime = DateTime.Now;

                var productStockEdit =
                    (from productStock in xmDataCont.Tbl_productStock
                     where productStock.ProductStockId == productStockId
                     select productStock).First();

                productStockEdit.ToOut = bool.TrueString;
            }
            else
            {
                int productPurposeRelationId = int.Parse(strProductPurposeRelationId);

                var projectEdit =
                    (from project in xmDataCont.Tbl_projectTagInfo
                     where project.ProjectTagId == projectTagId
                     select project).First();

                projectEdit.DoneTime = DateTime.Now;
            }

            try
            {
                //xmDataCont.Refresh(System.Data.Linq.RefreshMode.KeepChanges, xmDataCont.tbl_mainContract);
                //xmDataCont.Refresh(System.Data.Linq.RefreshMode.KeepChanges, xmDataCont.tbl_projectTagInfo);
                xmDataCont.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
            }
            catch (System.Data.Linq.ChangeConflictException cce)
            {
                string strEx = cce.Message;
                foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataCont.ChangeConflicts)
                {
                    //No database values are merged into current.
                    occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                }

                xmDataCont.SubmitChanges();
            }

            //DataSet ds = new DataSet();
            //ds.Tables.Add(dt.DefaultView.ToTable("addTable"));
            //SellProductProcess spp = new SellProductProcess(ds);

            //spp.MyDst.Tables["addTable"].Clear();

            //DataRow dr = spp.MyDst.Tables["addTable"].NewRow();
            //dr["projectTagId"] = strProjectTagId;
            //dr["productPurposeRelationId"] = strProductPurposeRelationId;
            //dr["productStockId"] = strProductStockId;

            //spp.MyDst.Tables["addTable"].Rows.Add(dr);
            //dt.Clear();

            //spp.Add();

            lblNoOut.Visible = false;
            btnNullRtn.Visible = false;

            returnProductGV.Visible = true;
            projectNeedGV.Visible = false;

            ProductPurposeRelationProcess pprpView = Session["ProductPurposeRelationProcess"] as ProductPurposeRelationProcess;

            pprpView.RealProductPurposeRelationView();

            DataTable productStockRelationTable = pprpView.MyDst.Tables["view_productStockRelation"].DefaultView.ToTable();

            string strFilter =
                " productPurpose = " + "'" + "forReturned".ToString() + "'";
            productStockRelationTable.DefaultView.RowFilter = strFilter;
            Session["view_productStockRelation"] = productStockRelationTable.DefaultView.ToTable();

            returnProductGV.DataSource = Session["view_productStockRelation"];
            returnProductGV.DataBind();
        }

        protected void btnRtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/stockInfoManager/productOutManager/productOutRelationManager/returnProductStockOutList.aspx");
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

        protected void returnProductGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            returnProductGV.PageIndex = e.NewPageIndex;

            returnProductGV.DataSource = Session["dtSources"];
            returnProductGV.DataBind();
        }

        protected void returnProductGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnNullRtn_Click(object sender, EventArgs e)
        {
            lblNoOut.Visible = false;
            btnNullRtn.Visible = false;

            returnProductGV.Visible = true;
            projectNeedGV.Visible = false;
        }
    }
}