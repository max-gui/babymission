using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.contractManager
{
    public partial class subContractEditing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.newContract);
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
                lblCust.Text = sessionDr["custCompName"].ToString();
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

        protected void subContractGV_Sorting(object sender, GridViewSortEventArgs e)
        {
            // By default, set the sort direction to ascending.
            if (subContractGV.SelectedIndex == -1)
            {
                DataTable dt = Session["dtSources"] as DataTable;

                if (dt != null)
                {
                    //Sort the data.
                    dt.DefaultView.Sort = e.SortExpression.GetSortDirectionExpression(ViewState); //GetSortDirectionExpression(e.SortExpression, ViewState);
                    subContractGV.DataSource = Session["dtSources"];
                    subContractGV.DataBind();
                }
            }
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

        protected void toDel_Click(object sender, EventArgs e)
        {
            LinkButton lbt = sender as LinkButton;

            subContractGV.SelectedIndex = (lbt.Parent.Parent as GridViewRow).DataItemIndex;
            subContractGV.Enabled = false;
            subContractGV.DataSource = Session["dtSources"];
            subContractGV.DataBind();

            btnAccept.Visible = true;
            btnCancel.Visible = true;
            btnAdd.Visible = false;
        }

        protected void toEdit_Click(object sender, EventArgs e)
        {
            int subContractId = int.Parse((sender as LinkButton).CommandArgument);
            Xm_db xmDataCont = Xm_db.GetInstance();
            LinkButton lkb = sender as LinkButton;

            var subContractEdit =
                (from subContract in xmDataCont.Tbl_subContract
                 where subContract.SubContractId == subContractId &&
                       subContract.EndTime > DateTime.Now
                 select subContract).First();

            bool hasSupply = subContractEdit.Tbl_subContrctProduct.All(p => p.HasSupplied == 0);
            bool applyment = subContractEdit.Tbl_paymentApply.Count(p => p.EndTime > DateTime.Now) == 0;

            bool businessProductCount =
                (from businessProduct in xmDataCont.Tbl_businessProduct
                 where businessProduct.Tbl_projectTagInfo.ProjectTagId == subContractEdit.Tbl_mainContract.ProjectTagId &&
                       businessProduct.EndTime > DateTime.Now
                 select businessProduct).Count() == 0;

            if (hasSupply && applyment && businessProductCount)
            {
                Session["seldSubContractId"] = subContractId.ToString();

                Response.Redirect("~/Main/contractManager/subContractToEdit.aspx");
            }
            else
            {
                //Label lbl = (lkb.Parent.Parent as GridViewRow).FindControl("lblMessage") as Label;
                //lbl.Text = "无权更改";
                //lbl.Visible = true;

                Page.ClientScript.ShowAlertWindow("无权修改", this.GetType());
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            int index = subContractGV.SelectedIndex;
            LinkButton lkb = subContractGV.Rows[index].FindControl("toDel") as LinkButton;
            lkb.Visible = true;

            int subContractId = int.Parse(lkb.CommandArgument);

            Xm_db xmDataCont = Xm_db.GetInstance();

            var subContractEdit =
                (from subContract in xmDataCont.Tbl_subContract
                where subContract.SubContractId == subContractId &&
                      subContract.EndTime > DateTime.Now
                select subContract).First();

            bool hasSupply = subContractEdit.Tbl_subContrctProduct.All(p => p.HasSupplied == 0);
            bool applyment = subContractEdit.Tbl_paymentApply.Count(p => p.EndTime > DateTime.Now) == 0;

            bool businessProductCount =
                (from businessProduct in xmDataCont.Tbl_businessProduct
                 where businessProduct.Tbl_projectTagInfo.ProjectTagId == subContractEdit.Tbl_mainContract.ProjectTagId &&
                       businessProduct.EndTime > DateTime.Now
                 select businessProduct).Count() == 0;

            if (hasSupply && applyment && businessProductCount)
            {
                subContractEdit.EndTime = DateTime.Now;
                subContractEdit.Tbl_mainContract.SupplierOk = bool.FalseString;

                foreach (Tbl_subContrctProduct scp in subContractEdit.Tbl_subContrctProduct)
                {
                    scp.EndTime = DateTime.Now;
                    scp.Tbl_subContract.Tbl_mainContract.Tbl_mainContrctProduct.
                        TakeWhile(p => p.ProductId == scp.ProductId && p.EndTime > DateTime.Now).
                        First().HasSupplier = bool.FalseString;
                }

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

                var subContract_supplierView =
                from subContract_supplier in xmDataCont.View_subContract_supplier
                where subContract_supplier.EndTime > DateTime.Now &&
                      subContract_supplier.MainContractId == subContractEdit.MainContractId
                select subContract_supplier;

                DataTable taskTable = subContract_supplierView.ToDataTable();

                DataTable dtSource = taskTable;
                Session["dtSources"] = dtSource;

                subContractGV.DataSource = Session["dtSources"];               
            }
            else
            {
                //Label lbl = (lkb.Parent.Parent as GridViewRow).FindControl("lblMessage") as Label;
                //lbl.Text = "无权删除";
                //lbl.Visible = true;

                Page.ClientScript.ShowAlertWindow("无权修改", this.GetType());
            }

            subContractGV.SelectedIndex = -1;
            subContractGV.Enabled = true;

            btnAccept.Visible = false;
            btnCancel.Visible = false;

            btnAdd.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //DataTable dtSource = Session["dtSources"] as DataTable;

            subContractGV.SelectedIndex = -1;
            subContractGV.Enabled = true;
            //subContractGV.DataSource = dtSource;
            //subContractGV.DataBind();

            btnAccept.Visible = false;
            btnCancel.Visible = false;

            btnAdd.Visible = true;

            //int index = subContractGV.SelectedIndex;
            //GridViewRow gvr = subContractGV.Rows[index];

            //Label lbl = gvr.FindControl("lblMessage") as Label;
            //lbl.Visible = false;
        }
    }
}