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
    public partial class subContractEditing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!(null == Session["totleAuthority"]))
            {
                int usrAuth = 0;
                string strUsrAuth = Session["totleAuthority"] as string;
                usrAuth = int.Parse(strUsrAuth);
                int flag = 0x3 << 4;

                if ((usrAuth & flag) == 0)
                    Response.Redirect("~/Main/NoAuthority.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (null == Session["selMainContractDr"])
            {
                Response.Redirect("~/Main/contractManager/subContractEdit.aspx");
            }

            if (!IsPostBack)
            {
                #region selMainContract
                DataRow sessionDr = Session["selMainContractDr"] as DataRow;

                lblMainContractTag.Text = sessionDr["mainContractTag"].ToString();
                lblProjectTag.Text = sessionDr["projectTag"].ToString();
                lblCust.Text = sessionDr["contractCompName"].ToString();
                lblMainContractMoney.Text = sessionDr["cash"].ToString();
                lblMainContractDateLine.Text = sessionDr["dateLine"].ToString();
                lblMainContractPayment.Text = sessionDr["paymentMode"].ToString();

                DataTable dtMainContractProduct = (Session["mainContractProductDtSources"] as DataTable).DefaultView.ToTable();
                string mainContractId = sessionDr["mainContractId"].ToString();
                string strFilter =
                    " mainContractId = " + "'" + mainContractId + "'";
                dtMainContractProduct.DefaultView.RowFilter = strFilter;
                                
                contractProductLsB.Rows = dtMainContractProduct.DefaultView.Count;
                contractProductLsB.DataSource = dtMainContractProduct;
                contractProductLsB.DataTextField = "productAndNum".ToString();
                contractProductLsB.DataValueField = "mainContractProductId".ToString();
                contractProductLsB.DataBind();
                #endregion


                #region dllProductTable
                DataSet scpDst = new DataSet();
                SubContractProductProcess scpView = new SubContractProductProcess(scpDst);

                scpView.RealSubContractProductView();
                DataTable dllProductTable = scpView.MyDst.Tables["view_subContractProduct"].DefaultView.ToTable();

                DataColumn colproductAndNum = new DataColumn("productAndNum", System.Type.GetType("System.String"));
                dllProductTable.Columns.Add(colproductAndNum);

                string strSplit = " , ".ToString();
                foreach (DataRow dr in dllProductTable.Rows)
                {
                    dr["productAndNum"] = dr["productName"].ToString() + strSplit + dr["productNum"].ToString();
                }

                Session["dllProductTable"] = dllProductTable;
                #endregion

                #region subContractGV
                DataSet vscsDst = new DataSet();
                subContractProcess vscsView = new subContractProcess(vscsDst);

                vscsView.RealSubContractSupplierView();
                DataTable taskTable = vscsView.MyDst.Tables["view_subContract_supplier"].DefaultView.ToTable();

                taskTable.DefaultView.RowFilter = strFilter;

                Session["subContractProcess"] = vscsView;
                Session["dtSources"] = taskTable;

                subContractGV.DataSource = Session["dtSources"];
                subContractGV.DataBind();
                #endregion
            }
        }

        protected void btnDelCancel_Click(object sender, EventArgs e)
        {
        
        }

        protected void btnAcceptDel_Click(object sender, EventArgs e)
        {
        
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/contractManager/subContractAdd.aspx");
        }

        protected void subContractGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            subContractGV.PageIndex = e.NewPageIndex;

            subContractGV.DataSource = Session["dtSources"];
            subContractGV.DataBind();
        }

        protected void subContractGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (subContractGV.SelectedIndex == -1)
            {
                e.Cancel = false;
                int index = e.NewSelectedIndex;

                subContractGV.EditIndex = index;
                subContractGV.DataSource = Session["dtSources"];
                subContractGV.DataBind();

                Button btn = null;
                btn = (subContractGV.Rows[index].FindControl("btnDel") as Button);
                btn.Visible = true;
                btn = (subContractGV.Rows[index].FindControl("btnCancle") as Button);
                btn.Visible = true;
            }
            else
            {
                e.Cancel = true;
            }

            btnAdd.Visible = false;
        }

        protected void subContractGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnDel_Click(object sender, EventArgs e)
        {

        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            int index = subContractGV.SelectedIndex;

            Button btn = null;
            btn = sender as Button;
            btn.Visible = false;
            btn = (subContractGV.SelectedRow.FindControl("btnDel") as Button);
            btn.Visible = false;
            btn = btnDelCancel;
            btn.Visible = false;
            btn = btnAcceptDel;
            btn.Visible = false;

            subContractGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

            subContractGV.SelectedIndex = -1;
            subContractGV.EditIndex = -1;
            subContractGV.DataBind();

            btnAdd.Visible = true;
        }

        protected void subContractGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                int index = e.Row.DataItemIndex;

                DataTable dtSubContract = Session["dtSources"] as DataTable;
                DataTable dllProductTable = Session["dllProductTable"] as DataTable;

                string subContractId = dtSubContract.DefaultView[index]["subContractId"].ToString();
                string strFilter =
                    " subContractId = " + "'" + subContractId + "'";
                dllProductTable.DefaultView.RowFilter = strFilter;

                ListBox lsB = e.Row.FindControl("subContractProductLsB") as ListBox;
                if (lsB != null)
                {
                    lsB.Rows = dllProductTable.DefaultView.Count;

                    lsB.DataSource = dllProductTable;
                    lsB.DataTextField = "productAndNum".ToString();
                    lsB.DataValueField = "subContractProductId".ToString();
                    lsB.DataBind();
                }
            }
        }
    }
}