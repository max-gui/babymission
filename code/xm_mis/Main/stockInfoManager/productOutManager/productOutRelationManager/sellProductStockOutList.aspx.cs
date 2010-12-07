using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.db;
using xm_mis.logic;
namespace xm_mis.Main.stockInfoManager.productOutManager.productOutRelationManager
{
    public partial class sellProductStockOutList : System.Web.UI.Page
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
                Xm_db xmDataCont = Xm_db.GetInstance();

                var businessProd =
                    from busiProd in xmDataCont.View_businessProduct
                    where busiProd.BusinessProductEd > DateTime.Now &&
                          busiProd.ProjectDetail == "sell"
                    select busiProd;

                DataTable dtSource = businessProd.ToDataTable();

                Session["dtSources"] = dtSource;

                businessProductGV.DataSource = Session["dtSources"];
                businessProductGV.DataBind();
            }
        }

        protected void businessProductGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            businessProductGV.PageIndex = e.NewPageIndex;
            businessProductGV.DataSource = Session["dtSources"];
            businessProductGV.DataBind();
        }

        protected void businessProductGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void toDel_Click(object sender, EventArgs e)
        {
            LinkButton lbt = sender as LinkButton;
            int businessProductId = int.Parse(lbt.CommandArgument);

            businessProductGV.SelectedIndex = (lbt.Parent.Parent as GridViewRow).DataItemIndex;
            businessProductGV.Enabled = false;
            businessProductGV.DataSource = Session["dtSources"];
            businessProductGV.DataBind();

            btnDelAccept.Visible = true;

            btnCancel.Visible = true;
            btnAdd.Visible = false;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/stockInfoManager/productOutManager/productOutRelationManager/sellProductStockOutView.aspx");
        }

        protected void btnDelAccept_Click(object sender, EventArgs e)
        {
            int index = businessProductGV.SelectedIndex;
            LinkButton lbt = businessProductGV.Rows[index].FindControl("toDel") as LinkButton;
            int businessProductId = int.Parse(lbt.CommandArgument);
            lbt.Visible = true;

            int usrId = int.Parse(Session["usrId"] as string);

            Xm_db xmDataCont = Xm_db.GetInstance();

            var businessProdEdit =
                from busiProd in xmDataCont.View_businessProduct
                where busiProd.BusinessProductId == businessProductId &&
                      busiProd.UsrId == usrId &&
                      busiProd.BusinessProductEd > DateTime.Now
                select busiProd;

            if (businessProdEdit.Count() > 0)
            {
                View_businessProduct businessProdTemp = businessProdEdit.First();
                xmDataCont.Sell_businessProduct_del(
                    businessProdTemp.ProjectTagId, businessProdTemp.ProductStockId, businessProdTemp.ProductId, businessProdTemp.BusinessProductId);

                btnCancel.Visible = false;
                btnAdd.Visible = true;
                btnDelAccept.Visible = false;

                //try
                //{
                //    xmDataCont.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
                //}
                //catch (System.Data.Linq.ChangeConflictException cce)
                //{
                //    string strEx = cce.Message;
                //    foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataCont.ChangeConflicts)
                //    {
                //        occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                //    }

                //    xmDataCont.SubmitChanges();
                //}

                var businessProd =
                    from busiProd in xmDataCont.View_businessProduct
                    where busiProd.BusinessProductEd > DateTime.Now &&
                          busiProd.ProjectDetail == "sell"
                    select busiProd;

                DataTable dtSource = businessProd.ToDataTable();
                Session["dtSources"] = dtSource;

                businessProductGV.DataSource = Session["dtSources"];
                businessProductGV.DataBind();

                //GridViewRow gvr = businessProductGV.Rows[index];

                //Label lbl = gvr.FindControl("lblMessage") as Label;
                //lbl.Visible = false;
            }
            else
            {
                //Label lbl = (lbt.Parent.Parent as GridViewRow).FindControl("lblMessage") as Label;
                //lbl.Text = "无权删除";
                //lbl.Visible = true;
                Page.ClientScript.ShowAlertWindow("无权修改", this.GetType());
            }

            businessProductGV.SelectedIndex = -1;
            businessProductGV.Enabled = true; 
            
            btnDelAccept.Visible = false;
            btnCancel.Visible = false;

            btnAdd.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            businessProductGV.SelectedIndex = -1;
            businessProductGV.Enabled = true;
            //businessProductGV.DataSource = Session["dtSources"];
            //businessProductGV.DataBind();

            btnDelAccept.Visible = false;
            btnCancel.Visible = false;

            btnAdd.Visible = true;

            int index = businessProductGV.SelectedIndex;
            GridViewRow gvr = businessProductGV.Rows[index];

            //Label lbl = gvr.FindControl("lblMessage") as Label;
            //lbl.Visible = false;

            LinkButton lbt = gvr.FindControl("toDel") as LinkButton;
            lbt.Visible = true;
        }
    }
}