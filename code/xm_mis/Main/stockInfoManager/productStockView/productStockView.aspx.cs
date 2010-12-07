using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using System.IO;
using xm_mis.db;
using System.Reflection;
namespace xm_mis.Main.stockInfoManager.productStockView
{
    public partial class productStockView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.stockManager);
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

                reFlash(xmDataCont);
            }
        }

        private void reFlash(Xm_db xmDataCont)
        {
            #region productStockGV

            var stockEdit =
                from stock in xmDataCont.View_stock
                where stock.ProductStockEd > DateTime.Now //&&
                      //(stock.ProductPurposeRelationEd.HasValue ? 
                      //stock.ProductPurposeRelationEd > DateTime.Now : true) &&
                orderby stock.ProductPurposeRelationEd descending orderby stock.ProductPurposeId ascending 
                select stock;
            
            //var productIdInfo =
            //    from product in stockEdit
            //    select product.ProductId;

            //List<View_stock> ls = new List<View_stock>(20);
            //foreach (var o in productIdInfo)
            //{
            //    ls.Add(stockEdit.TakeWhile(e => e.ProductId == o).First());
            //}
            
            //DataTable dtSources = stockEdit.ToDataTable();
            //var a = ((IEnumerable<View_stock>)stockEdit).Distinct();//new xm_mis.db.StockIdComparer());
            DataTable dtSources = stockEdit.AsEnumerable<View_stock>().Distinct(new MainIdComparer<View_stock>("ProductStockId")).ToDataTable();
            Session["dtSources"] = dtSources;

            var productInfo =
                from product in xmDataCont.Tbl_product
                where product.EndTime > DateTime.Now
                select product;

            DataTable productInfoDt = productInfo.ToDataTable();
            Session["productInfoDt"] = productInfoDt;

            #endregion

            productStockGV.DataSource = Session["dtSources"];
            productStockGV.DataBind();
        }

        protected void btnRtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/DefaultMainSite.aspx");
        }

        protected void checkText_Down(object sender, EventArgs e)
        {
            GridViewRow gvr = (sender as LinkButton).Parent.Parent as GridViewRow;

            Label lbl = gvr.FindControl("lblProductInCheckId") as Label;

            int productInCheckId = int.Parse(lbl.Text.ToString());

            Xm_db xmDataCont = Xm_db.GetInstance();

            var productInCheckEdit =
                (from productInCheck in xmDataCont.Tbl_productInCheck
                where productInCheck.ProductInCheckId == productInCheckId
                select productInCheck).First();

            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = productInCheckEdit.ContentType;
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(productInCheckEdit.CheckTextName, System.Text.Encoding.UTF8));
            Response.BinaryWrite(productInCheckEdit.ProductCheckText.ToArray());
            Response.Flush();
            Response.End();
        }

        protected void toEdit_Click(object sender, EventArgs e)
        {
            LinkButton lkb = sender as LinkButton;
            GridViewRow gvr = lkb.Parent.Parent as GridViewRow;
            int index = gvr.DataItemIndex;
            productStockGV.SelectedIndex = index;

            Label lbl = gvr.FindControl("lblProductName") as Label;
            lbl.Visible = false;
            lbl = gvr.FindControl("lblProductTag") as Label;
            lbl.Visible = false;

            DropDownList ddl = gvr.FindControl("ddlProductName") as DropDownList;
            ddl.Visible = true;

            TextBox txb = gvr.FindControl("txtProductTag") as TextBox;
            txb.Visible = true;

            productStockGV.Columns[11].Visible = false;

            btnOk.CommandArgument = "toEdit";
            btnOk.Visible = true;
            btnNo.Visible = true;
            btnRtn.Visible = false;

            //productStockGV.EnableSortingAndPagingCallbacks = false;
        }

        protected void toDel_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (sender as LinkButton).Parent.Parent as GridViewRow;
            int index = gvr.DataItemIndex;
            productStockGV.SelectedIndex = index;

            productStockGV.Columns[11].Visible = false;

            btnOk.CommandArgument = "toDel";
            btnOk.Visible = true;
            btnNo.Visible = true;
            btnRtn.Visible = false;

            //productStockGV.EnableSortingAndPagingCallbacks = false;
        }

        protected void productStockGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            productStockGV.PageIndex = e.NewPageIndex;

            productStockGV.DataSource = Session["dtSources"];
            productStockGV.DataBind();
        }

        protected void productStockGV_Sorting(object sender, GridViewSortEventArgs e)
        {
            // By default, set the sort direction to ascending.
            if (productStockGV.SelectedIndex == -1)
            {
                DataTable dt = Session["dtSources"] as DataTable;

                if (dt != null)
                {
                    //Sort the data.
                    dt.DefaultView.Sort = e.SortExpression.GetSortDirectionExpression(ViewState); //GetSortDirectionExpression(e.SortExpression, ViewState);
                    productStockGV.DataSource = Session["dtSources"];
                    productStockGV.DataBind();
                }
            }
        }

        //private string GetSortDirectionExpression(string sortExpression, StateBag stBg)
        //{
        //    System.Text.StringBuilder sb = new System.Text.StringBuilder(30);
        //    sb.Append(sortExpression).Append(" ");
        //    string sortDirection = stBg[sortExpression] as string;
                
        //    if (sortDirection == null ? true :sortDirection.Equals("ASC"))
        //    {
        //        sb.Append("DESC");
        //        stBg[sortExpression] = "DESC";
        //    }
        //    else
        //    {
        //        sb.Append("ASC");
        //        stBg[sortExpression] = "ASC";
        //    }            

        //    return sb.ToString();
        //}

        protected void productStockGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;

                Label lbl = e.Row.FindControl("lblProductId") as Label;

                string productId = lbl.Text;

                DropDownList ddl = e.Row.FindControl("ddlProductName") as DropDownList;

                if (null != ddl)
                {
                    ddl.DataSource = Session["productInfoDt"];
                    ddl.DataTextField = "productName";
                    ddl.DataValueField = "productId";
                    ddl.DataBind();

                    ddl.SelectedValue = productId;                
                }
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            //Xm_db xmDataCont = Xm_db.GetInstance();
            
            //var stockEdit =
            //    from stock in xmDataCont.View_stock
            //    where stock.ProductStockEd > DateTime.Now
            //    select stock;            

            //int mainContractId = int.Parse((sender as LinkButton).CommandArgument);
            //LinkButton lkb = sender as LinkButton;

            //var mainContractEdit =
            //    (from mainContract in xmDataCont.Tbl_mainContract
            //     where mainContract.MainContractId == mainContractId &&
            //           mainContract.EndTime > DateTime.Now
            //     select mainContract).First();

            //int mainContractProductCount = mainContractEdit.Tbl_mainContrctProduct.
            //    Count(p => p.MainContractId == mainContractId && p.EndTime > DateTime.Now && p.HasSupplier.Equals(bool.TrueString));
            //int applyment = mainContractEdit.Tbl_receiptApply.Count(p => p.MainContractId == mainContractId && p.EndTime > DateTime.Now);

            //if ((mainContractProductCount == 0) && (applyment == 0))
            //{
            //    Session["seldMainContractId"] = mainContractId.ToString();

            //    Response.Redirect("~/Main/contractManager/mainContractEdit.aspx");
            //}
            //else
            //{
            //    Label lbl = (lkb.Parent.Parent as GridViewRow).FindControl("lblMessage") as Label;
            //    lbl.Text = "无权更改";
            //    lbl.Visible = true;
            //}

            GridViewRow gvr = productStockGV.SelectedRow;
            productStockGV.SelectedIndex = -1;
            string commName = btnOk.CommandArgument;
            
            Label lbl = gvr.FindControl("lblProductName") as Label;
            lbl.Visible = true;
            lbl = gvr.FindControl("lblProductTag") as Label;
            lbl.Visible = true;

            DropDownList ddl = gvr.FindControl("ddlProductName") as DropDownList;
            ddl.Visible = false;

            TextBox txb = gvr.FindControl("txtProductTag") as TextBox;
            txb.Visible = false;

            productStockGV.Columns[11].Visible = true;

            btnOk.CommandArgument = string.Empty;
            btnOk.Visible = false;
            btnNo.Visible = false;
            btnRtn.Visible = true;

            //productStockGV.EnableSortingAndPagingCallbacks = true;

            lbl = gvr.FindControl("lblProductStockId") as Label;
            int productStockId = int.Parse(lbl.Text);
            lbl = gvr.FindControl("lblProductPurpose") as Label;
            string productPurpose = lbl.Text;
            lbl = gvr.FindControl("lblToOut") as Label;
            string toOut = lbl.Text;
            lbl = gvr.FindControl("lblProductTag") as Label;
            string productTag = lbl.Text;
            bool flag = string.IsNullOrEmpty(productPurpose);
            flag &= toOut.Equals(bool.FalseString);

            if (flag)
            {
                Xm_db xmDataCont = Xm_db.GetInstance();

                var stockEdit =
                    (from productStock in xmDataCont.Tbl_productStock
                     where productStock.ProductStockId == productStockId &&
                           productStock.EndTime > DateTime.Now
                     select productStock).First();

                DateTime inTime = stockEdit.StartTime;
                int productId = stockEdit.ProductId;

                var projectUndo =
                     from projectProduct in xmDataCont.View_projectProduct_info
                     where projectProduct.DoneTime == inTime &&
                             projectProduct.ProductId == productId &&
                             projectProduct.ProductTag.Equals(productTag)
                     select projectProduct;//.First();//.First().DoneTime = DateTime.MaxValue; 

                if (projectUndo.Count() != 0)
                {
                    projectUndo.First().DoneTime = DateTime.MaxValue;
                }

                if (commName.Equals("toEdit"))
                {
                    stockEdit.ProductId = int.Parse(ddl.SelectedValue);
                    stockEdit.ProductTag = txb.Text;

                    submitChangesInSafe(xmDataCont);

                    reFlash(xmDataCont);
                }
                else if (commName.Equals("toDel"))
                {
                    var checkEdit = stockEdit.Tbl_productInCheck.TakeWhile(element => element.ProductStockId == productStockId && element.EndTime > DateTime.Now).First();

                    xmDataCont.Tbl_productInCheck.DeleteOnSubmit(checkEdit);
                    xmDataCont.Tbl_productStock.DeleteOnSubmit(stockEdit);

                    submitChangesInSafe(xmDataCont);

                    reFlash(xmDataCont);
                }
            }
            else
            {
                Page.ClientScript.ShowAlertWindow("无权修改", this.GetType()); 
                
                productStockGV.DataSource = Session["dtSources"];
                productStockGV.DataBind();
            }
        }

        private static void submitChangesInSafe(Xm_db xmDataCont)
        {
            try
            {
                xmDataCont.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
            }
            catch (System.Data.Linq.ChangeConflictException cce)
            {
                string strEx = cce.Message;
                foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataCont.ChangeConflicts)
                {
                    occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                }

                xmDataCont.SubmitChanges();
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = productStockGV.SelectedRow;
            productStockGV.SelectedIndex = -1;

            Label lbl = gvr.FindControl("lblProductName") as Label;
            lbl.Visible = true;
            lbl = gvr.FindControl("lblProductTag") as Label;
            lbl.Visible = true;

            DropDownList ddl = gvr.FindControl("ddlProductName") as DropDownList;
            ddl.Visible = false;

            TextBox txb = gvr.FindControl("txtProductTag") as TextBox;
            txb.Visible = false;

            productStockGV.Columns[11].Visible = true;

            btnOk.CommandArgument = string.Empty;
            btnOk.Visible = false;
            btnNo.Visible = false;
            btnRtn.Visible = true;

            //productStockGV.EnableSortingAndPagingCallbacks = false;
        }
    }
}