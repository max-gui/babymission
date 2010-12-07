using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.stockInfoManager.productStockView
{
    public partial class productStockDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                int usrAuth = 0;
                string strUsrAuth = Session["totleAuthority"] as string;
                usrAuth = int.Parse(strUsrAuth);
                int flag = 0x1 << 2;

                if ((usrAuth & flag) == 0)
                    Response.Redirect("~/Main/NoAuthority.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                DataSet myDst = new DataSet();
                ProductPurposeRelationProcess pprpView = new ProductPurposeRelationProcess(myDst);

                Session["ProductPurposeRelationProcess"] = pprpView;

                #region productStockGV

                pprpView.AllProductPurposeRelationView();
                DataTable productStockGVTable = pprpView.MyDst.Tables["productStockRelation_view"].DefaultView.ToTable("productStockGVTable");

                string strFilter =
                    " productCheck = " + "'" + bool.TrueString +"'";

                dt_modify(productStockGVTable, strFilter);

                Session["productStockGVTable"] = productStockGVTable;

                #endregion
                
                //#region productStockRelationTable

                //pprpView.RealProductPurposeRelationView();
                //DataTable productStockRelationTable = pprpView.MyDst.Tables["view_productStockRelation"].DefaultView.ToTable();

                //Session["productStockRelationTable"] = productStockRelationTable;

                //#endregion

                #region productPurposeTable

                pprpView.RealProductPurposeView();
                DataTable productPurposeTable = pprpView.MyDst.Tables["tbl_productPurpose"].DefaultView.ToTable();

                Session["productPurposeTable"] = productPurposeTable;

                #endregion

                productStockGV.DataSource = Session["productStockGVTable"];
                productStockGV.DataBind();
            }
        }

        protected void dt_modify(DataTable dt, string strFilter)
        {
            dt.DefaultView.RowFilter = strFilter;

            string strTest = string.Empty;
            string strNotDone = "未定义".ToString();
            foreach (DataRow dr in dt.Rows)
            {
                strTest = dr["productPurpose"].ToString();

                if (string.IsNullOrEmpty(strTest))
                {
                    dr["productPurpose"] = strNotDone;
                }
            }
        }
        protected void productEdit_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (sender as LinkButton).Parent.Parent as GridViewRow;

            string index = gvr.RowIndex.ToString();

            RadioButtonList rbl = gvr.FindControl("rblProductDetail") as RadioButtonList;

            rbl.Visible = true;
            string selValue = rbl.SelectedValue;

            productStockGV.Columns[3].Visible = false;

            btnOK.Visible = true;
            btnNo.Visible = true;
            btnRtn.Visible = false;

            btnOK.CommandName = selValue;
            btnOK.CommandArgument = index;
            btnNo.CommandName = selValue;
            btnNo.CommandArgument = index;
        }

        protected void btnRtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/DefaultMainSite.aspx");
        }

        protected void productStockGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            productStockGV.PageIndex = e.NewPageIndex;

            productStockGV.DataSource = Session["productStockGVTable"];
            productStockGV.DataBind();
        }

        protected void productStockGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void productStockGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void productStockGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable psGVdt = Session["productStockGVTable"] as DataTable;

                int index = e.Row.DataItemIndex;

                string strProdutCheck = psGVdt.Rows[index]["productCheck"].ToString();
                string strProductPurposeId = psGVdt.Rows[index]["productPurposeId"].ToString();

                RadioButtonList rbl = e.Row.FindControl("rblProductDetail") as RadioButtonList;
                if (rbl != null)
                {
                    rbl.DataTextField = "productPurpose";
                    rbl.DataValueField = "productPurposeId";

                    rbl.DataSource = Session["productPurposeTable"];
                    rbl.DataBind();

                    try
                    {
                        rbl.SelectedValue = strProductPurposeId;
                    }
                    catch
                    {
                        rbl.SelectedValue = null;
                    }
                }
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int rowIndex = int.Parse(btn.CommandArgument);

            GridViewRow gvr = productStockGV.Rows[rowIndex];

            int index = gvr.DataItemIndex;

            RadioButtonList rbl = gvr.FindControl("rblProductDetail") as RadioButtonList;

            rbl.Visible = false;
            string selValue = rbl.SelectedValue;

            DataTable dt = Session["productStockGVTable"] as DataTable;

            string strProductPurposeRelationId = dt.Rows[index]["productPurposeRelationId"].ToString();

            ProductPurposeRelationProcess pprp = Session["ProductPurposeRelationProcess"] as ProductPurposeRelationProcess;

            if (string.IsNullOrEmpty(strProductPurposeRelationId))
            {
                pprp.MyDst.Tables.Add(dt.DefaultView.ToTable("productStockGVTable"));
                pprp.MyDst.Tables["productStockGVTable"].Clear();

                DataRow dr = pprp.MyDst.Tables["productStockGVTable"].NewRow();
                dr["productInId"] = dt.Rows[index]["productInId"].ToString();
                dr["productPurposeId"] = selValue;
                
                pprp.MyDst.Tables["productStockGVTable"].Rows.Add(dr);
                dt.Clear();

                pprp.Add();
            }
            else
            {
                pprp.ProductPurposeRelationIdUpdata(strProductPurposeRelationId, selValue);
            }

            #region productStockGV

            pprp.AllProductPurposeRelationView();
            DataTable productStockGVTable = pprp.MyDst.Tables["productStockRelation_view"].DefaultView.ToTable();

            string strFilter =
                " productCheck = " + "'" + bool.TrueString + "'";

            dt_modify(productStockGVTable, strFilter);

            Session["productStockGVTable"] = productStockGVTable;

            #endregion

            productStockGV.DataSource = Session["productStockGVTable"];
            productStockGV.DataBind();

            productStockGV.Columns[3].Visible = true;

            btnOK.Visible = false;
            btnNo.Visible = false;
            btnRtn.Visible = true;
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int index = int.Parse(btn.CommandArgument);

            RadioButtonList rbl = productStockGV.Rows[index].FindControl("rblProductDetail") as RadioButtonList;

            try
            {
                rbl.SelectedValue = btn.CommandName;
            }
            catch
            {
                rbl.SelectedValue = null;
            }

            rbl.Visible = false;

            productStockGV.Columns[3].Visible = true; ;

            btnOK.Visible = false;
            btnNo.Visible = false;
            btnRtn.Visible = true;

            //productStockGV.DataSource = Session["productStockGVTable"];
            //productStockGV.DataBind();
        }
    }
}