using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.stockInfoManager.productOutManager
{
    public partial class productStockOutAccept : System.Web.UI.Page
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
                DataSet MyDst = new DataSet();

                #region selfPaymentGV
                BusinessProductProcess businessProductView = new BusinessProductProcess(MyDst);

                //businessProductView.RealBusinessProductView();
                //DataTable taskTable = businessProductView.MyDst.Tables["view_businessProductOut"].DefaultView.ToTable();

                //string strFilter = string.Empty;

                //dt_modify(taskTable, strFilter);

                ////dt.DefaultView.RowFilter = strFilter;
                ////DataTable taskTable = dt.DefaultView.ToTable();
                ////dt.Clear();                

                //Session["BusinessProductProcess"] = businessProductView;
                //Session["dtSources"] = taskTable.DefaultView.ToTable();

                //taskTable.Clear();


                Xm_db xmDbDateContext = Xm_db.GetInstance();

                var taskDs =
                    from view_businessProductOut in xmDbDateContext.View_businessProductOut
                    //where view_businessProductOut.Approve == null ||
                    //        view_businessProductOut.Approve == bool.TrueString &&
                    //        view_businessProductOut.EndTime > DateTime.Now
                    //select view_businessProductOut;
                    where (view_businessProductOut.Approve == null ? 
                            true : view_businessProductOut.Approve.Equals(bool.TrueString)) &&
                            view_businessProductOut.EndTime > DateTime.Now
                    select view_businessProductOut;
                
                DataTable dt = taskDs.ToDataTable();

                string strFilter = string.Empty;

                dt_modify(dt, strFilter);

                Session["BusinessProductProcess"] = businessProductView;
                Session["dtSources"] = dt.DefaultView.ToTable();

                dt.Clear();

                businessProductGV.DataSource = Session["dtSources"];
                businessProductGV.DataBind();
                #endregion
            }
        }

        protected void dt_modify(DataTable dt, string strFilter)
        {
            dt.DefaultView.RowFilter = strFilter;

            DataColumn colGoalAddr = new DataColumn("goalAddr", System.Type.GetType("System.String"));
            dt.Columns.Add(colGoalAddr);

            //string projectDetail = string.Empty;
            //string sellProject = "sell";
            string productCheck = string.Empty;
            //string projectOutAddr = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                //projectDetail = dr["projectDetail"].ToString();
                productCheck = dr["productCheck"].ToString();
                //if (projectDetail.Equals(sellProject))
                //{
                //    dr["goalAddr"] = dr["sellOutAddr"].ToString();
                //}
                //else if (productCheck.Equals(bool.TrueString))
                //{
                //    projectOutAddr = dr["projectOutAddr"].ToString();
                //    if (string.IsNullOrEmpty(projectOutAddr))
                //    {
                //        dr["goalAddr"] = dr["projectSynopsis"].ToString();
                //    }
                //    else
                //    {
                //        dr["goalAddr"] = dr["projectOutAddress"].ToString();
                //    }
                //}
                //else
                //{
                //    dr["goalAddr"] = dr["supplierAddress"].ToString();
                //}

                if (productCheck.Equals(bool.TrueString))
                {
                    dr["goalAddr"] = dr["projectOutAddress"].ToString();
                }
                else
                {
                    dr["goalAddr"] = dr["supplierAddress"].ToString();
                }
            }
        }

        protected void productOutDone_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (sender as LinkButton).Parent.Parent as GridViewRow;

            int index = gvr.RowIndex;

            businessProductGV.SelectedIndex = index;
            businessProductGV.Enabled = false;
            businessProductGV.Columns[3].Visible = false;

            businessProductGV.DataSource = Session["dtSources"];
            businessProductGV.DataBind();

            btnOk.Visible = true;
            btnNo.Visible = true;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = businessProductGV.SelectedRow;
            Label lbl = gvr.FindControl("lblBusinessProductId") as Label;
            //int index = gvr.DataItemIndex;

            DataTable dt = Session["dtSources"] as DataTable;

            //string businessProductId = dt.Rows[index]["businessProductId"].ToString();
            string businessProductId = lbl.Text;

            string strFilter =
                    " businessProductId = " + "'" + businessProductId + "'";
            dt.DefaultView.RowFilter = strFilter;

            DataTable doneTable = dt.DefaultView.ToTable("addTable");
            dt.Clear();

            BusinessProductProcess bpp = Session["BusinessProductProcess"] as BusinessProductProcess;

            bpp.MyDst.Tables.Add(doneTable);
            bpp.BusinessProductOutDone();

            bpp.RealBusinessProductView();
            DataTable taskTable = bpp.MyDst.Tables["view_businessProductOut"].DefaultView.ToTable();

            string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
            strFilter = string.Empty;

            dt_modify(taskTable, strFilter);

            Session["dtSources"] = taskTable.DefaultView.ToTable();

            businessProductGV.SelectedIndex = -1;
            businessProductGV.Enabled = true;
            businessProductGV.Columns[3].Visible = true;

            businessProductGV.DataSource = Session["dtSources"];
            businessProductGV.DataBind();

            btnOk.Visible = false;
            btnNo.Visible = false;
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            businessProductGV.SelectedIndex = -1;
            businessProductGV.Enabled = true;
            businessProductGV.Columns[3].Visible = true;

            businessProductGV.DataSource = Session["dtSources"];
            businessProductGV.DataBind();

            btnOk.Visible = false;
            btnNo.Visible = false;
        }

        protected void selfPaymentGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            businessProductGV.PageIndex = e.NewPageIndex;

            businessProductGV.DataSource = Session["dtSources"];
            businessProductGV.DataBind();
        }

        protected void selfPaymentGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}