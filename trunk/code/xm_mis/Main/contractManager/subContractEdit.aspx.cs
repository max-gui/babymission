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
    public partial class subContractEdit : System.Web.UI.Page
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
                string usrId = Session["usrId"] as string;

                DataSet MyDst = new DataSet();
                #region productList
                MainContractProductProcess mcppView = new MainContractProductProcess(MyDst);

                mcppView.RealMainContractProductView();

                DataTable mcppTable = mcppView.MyDst.Tables["view_mainContractProduct"];

                DataColumn colproductAndNum = new DataColumn("productAndNum", System.Type.GetType("System.String"));
                mcppTable.Columns.Add(colproductAndNum);

                string strSplit = " , ".ToString();
                foreach (DataRow dr in mcppTable.Rows)
                {
                    dr["productAndNum"] = dr["productName"].ToString() + strSplit + dr["productNum"].ToString();
                }

                Session["mainContractProductDtSources"] = mcppTable;
                #endregion

                #region mainContractGV
                MainContractProcess mainContractView = new MainContractProcess(MyDst);
                mainContractView.UsrId = usrId;

                mainContractView.RealmainContractProjectUsrView();
                DataTable taskTable = mainContractView.MyDst.Tables["view_mainContract_project_usr"];

                Session["ProjectTagProcess"] = mainContractView;
                Session["dtSources"] = taskTable;


                mainContractGV.DataSource = Session["dtSources"];
                mainContractGV.DataBind();
                #endregion                
            }
        }

        protected void mainContractGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mainContractGV.PageIndex = e.NewPageIndex;

            mainContractGV.DataSource = Session["usrDtSources"];//["dtSources"] as DataTable;  
            mainContractGV.DataBind();
        }

        protected void mainContractGV_SelectedIndexChanged(object sender, EventArgs e)
        {
            int numIndex = mainContractGV.SelectedRow.DataItemIndex;

            DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            DataRow dr = dt.Rows[numIndex];

            Session["selMainContractDr"] = dr;
            Response.Redirect("~/Main/contractManager/subContractEditing.aspx");
        }

        protected void mainContractGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void mainContractGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                int index = e.Row.DataItemIndex;

                DataTable dt = Session["dtSources"] as DataTable;
                DataTable dtMainContractProduct = (Session["mainContractProductDtSources"] as DataTable).DefaultView.ToTable();

                string mainContractId = dt.DefaultView[index]["mainContractId"].ToString();
                string strFilter =
                    " mainContractId = " + "'" + mainContractId + "'";
                dtMainContractProduct.DefaultView.RowFilter = strFilter;

                ListBox lsB = e.Row.FindControl("contractProductLsB") as ListBox;
                if (lsB != null)
                {
                    lsB.Rows = dtMainContractProduct.DefaultView.Count;

                    lsB.DataSource = dtMainContractProduct;
                    lsB.DataTextField = "productAndNum".ToString();
                    lsB.DataValueField = "mainContractProductId".ToString();
                    lsB.DataBind();
                }
            }
        }
    }
}