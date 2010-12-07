using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.stockInfoManager
{
    public partial class productCheckView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.productCheck);
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
                int usrId = int.Parse(Session["usrId"] as string);

                #region productStockGV
                DataSet myDst = new DataSet();
                ProductStockProcess pspView = new ProductStockProcess(myDst);

                pspView.RealProductStockCheckManView();
                DataTable taskTable = pspView.MyDst.Tables["view_productStockCheckMan"].DefaultView.ToTable();

                string strFilter =
                    " usrId = " + usrId;// +
                    //" and productCheck = " + "'" + "unChecked".ToString() + "'";

                dt_modify(taskTable, strFilter);

                Session["ProductStockProcess"] = pspView;
                Session["dtSources"] = taskTable;

                productStockGV.DataSource = Session["dtSources"];
                productStockGV.DataBind();
                #endregion
            }
        }

        protected void dt_modify(DataTable dt, string strFilter)
        {
            dt.DefaultView.RowFilter = strFilter;            
        }

        protected void checkEdit_Click(object sender, EventArgs e)
        {
            GridViewRow dvr = (sender as LinkButton).Parent.Parent as GridViewRow;
            //int index = dvr.RowIndex;
            //int numIndex = productStockGV.Rows[index].DataItemIndex;
            int numIndex = dvr.DataItemIndex;

            DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            DataRow dr = dt.Rows[numIndex];

            Session["seldProductStock"] = dr;

            Response.Redirect("~/Main/stockInfoManager/productCheck.aspx");
        }

        protected void productStockGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            productStockGV.PageIndex = e.NewPageIndex;

            productStockGV.DataSource = Session["dtSources"];
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

        }
        
        protected void btnRtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/DefaultMainSite.aspx");
        }
    }
}