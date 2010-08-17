using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.contractManager
{
    public partial class mainContractProductSel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!(null == Session["totleAuthority"]))
            {
                int usrAuth = 0;
                string strUsrAuth = Session["totleAuthority"] as string;
                usrAuth = int.Parse(strUsrAuth);
                int flag = 0x5 << 4;

                if ((usrAuth & flag) == 0)
                    Response.Redirect("~/Main/NoAuthority.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                if (null == Session["mainProductSelDs"])
                {
                    DataSet MyDst = new DataSet();
                    ProductProcess myView = new ProductProcess(MyDst);

                    myView.RealProductView();
                    DataTable taskTable = myView.MyDst.Tables["tbl_product"];

                    DataColumn colCheck = new DataColumn("checkOrNot", System.Type.GetType("System.Boolean"));
                    DataColumn colNum = new DataColumn("productNum", System.Type.GetType("System.Int32"));
                    taskTable.Columns.Add(colCheck);
                    taskTable.Columns.Add(colNum);

                    foreach (DataRow dr in taskTable.Rows)
                    {
                        dr["productNum"] = 1;
                    }
                    
                    Session["mainProductSelDs"] = taskTable;
                                        
                    productSelGV.DataSource = Session["mainProductSelDs"];
                    productSelGV.DataBind(); 
                    
                }
                else
                {
                    DataTable dt = Session["mainProductSelDs"] as DataTable;

                    string end = DateTime.Now.ToShortDateString();
                    string strFilter =
                        " endTime > " + "'" + end + "'";
                    dt.DefaultView.RowFilter = strFilter;
                                        
                    productSelGV.DataSource = dt;
                    productSelGV.DataBind(); 
                }
            }
        }

        protected void productSelGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            productSelGV.PageIndex = e.NewPageIndex;

            productSelGV.DataSource = Session["mainProductSelDs"];//["dtSources"] as DataTable;  
            productSelGV.DataBind();
        }

        protected void productSelGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/contractManager/addContract.aspx");
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["mainProductSelDs"] as DataTable;

            int index = -1;
            CheckBox cb = null;
            foreach (GridViewRow row in productSelGV.Rows)
            {
                index = row.DataItemIndex;

                cb = row.Cells[2].Controls[0] as CheckBox;
                dt.Rows[index]["checkOrNot"] = cb.Checked;
            }

            dt.AcceptChanges();
            Session["mainProductSelDs"] = dt;

            Response.Redirect("~/Main/contractManager/addContract.aspx");
        }

        protected void productSelGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb = e.Row.Cells[2].Controls[0] as CheckBox;
                if (cb != null)
                {
                    cb.Enabled = true;
                }
            }
        }
    }
}