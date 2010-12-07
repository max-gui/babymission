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
    public partial class subContractEdit : System.Web.UI.Page
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

                //Xm_db xmDataCont = Xm_db.GetInstance();

                //int uId = int.Parse(usrId);
                //var a =
                //    from mainContract_project_usr in xmDataCont.View_mainContract_project_usr
                //    where mainContract_project_usr.EndTime > DateTime.Now &&
                //          mainContract_project_usr.UsrId == uId
                //    orderby mainContract_project_usr.HasSupplier descending, mainContract_project_usr.ReceiptEd descending
                //    select new { mainContract_project_usr, ok = mainContract_project_usr. };

                //System.Type t = a.GetType();

                //DataTable dba = a.AsEnumerable().Distinct<t.>(new MainIdComparer<t>("BusinessProductId")).ToDataTable();
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
            // By default, set the sort direction to ascending.
            if (mainContractGV.SelectedIndex == -1)
            {
                DataTable dt = Session["dtSources"] as DataTable;

                if (dt != null)
                {
                    //Sort the data.
                    dt.DefaultView.Sort = e.SortExpression.GetSortDirectionExpression(ViewState); //GetSortDirectionExpression(e.SortExpression, ViewState);
                    mainContractGV.DataSource = Session["dtSources"];
                    mainContractGV.DataBind();
                }
            }
        }

        protected void mainContractGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {            
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                int index = e.Row.DataItemIndex;

                //DataTable dt = Session["dtSources"] as DataTable;
                DataTable dtMainContractProduct = (Session["mainContractProductDtSources"] as DataTable).DefaultView.ToTable();

                Label lbl = e.Row.FindControl("lblMainContractId") as Label;

                //string mainContractId = dt.DefaultView[index]["mainContractId"].ToString();
                string mainContractId = lbl.Text.ToString();
                string strFilter =
                    " mainContractId = " + "'" + mainContractId + "'";
                dtMainContractProduct.DefaultView.RowFilter = strFilter;

                DataTable mcpp = dtMainContractProduct.DefaultView.ToTable();
                dtMainContractProduct.Clear();

                //TextBox txb = e.Row.FindControl("TextBox1") as TextBox;
                //txb.Text = "aaa \n bbb \n";

                //string strSplit = " , ".ToString();
                //string newLine = "\n";
                //int row = 2;
                //foreach (DataRow dr in mcpp.Rows)
                //{
                //    txb.Text += dr["productName"].ToString() + strSplit + dr["productNum"].ToString() + newLine;
                //    row++;
                //}
                //txb.Text = txb.Text.TrimEnd(newLine.ToCharArray());
                //txb.Rows = row;

                ListBox lsB = e.Row.FindControl("contractProductLsB") as ListBox;
                if (lsB != null)
                {
                    lsB.Rows = mcpp.Rows.Count;

                    lsB.DataSource = mcpp;
                    lsB.DataTextField = "productAndNum".ToString();
                    lsB.DataValueField = "mainContractProductId".ToString();
                    lsB.DataBind(); 

                }
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            //Response.Write("fuck!");
            ////this.Page.RegisterStartupScript("open ", " <script> window.alert('fuck!'); </script> ");
            //ClientScriptManager cs = Page.ClientScript;
            //string a = " <script> window.alert('fuck!'); </script> ";
            ////cs.RegisterStartupScript(this.GetType(), "fuck", a);
            //cs.RegisterClientScriptBlock(this.GetType(), "fuck", a);
            
            Response.Redirect("~/Main/contractManager/addContract.aspx");
        }

        protected void toDel_Click(object sender, EventArgs e)
        {
            LinkButton lbt = sender as LinkButton;

            mainContractGV.SelectedIndex = (lbt.Parent.Parent as GridViewRow).DataItemIndex;
            mainContractGV.Enabled = false;
            mainContractGV.DataSource = Session["dtSources"];
            mainContractGV.DataBind();

            btnAccept.Visible = true;
            btnCancel.Visible = true;
            btnAdd.Visible = false;
        }

        protected void toEdit_Click(object sender, EventArgs e)
        {
            int mainContractId = int.Parse((sender as LinkButton).CommandArgument);
            Xm_db xmDataCont = Xm_db.GetInstance();
            LinkButton lkb = sender as LinkButton;

            var mainContractEdit =
                (from mainContract in xmDataCont.Tbl_mainContract
                 where mainContract.MainContractId == mainContractId &&
                       mainContract.EndTime > DateTime.Now
                 select mainContract).First();

            int mainContractProductCount = mainContractEdit.Tbl_mainContrctProduct.
                Count(p => p.MainContractId == mainContractId && p.EndTime > DateTime.Now && p.HasSupplier.Equals(bool.TrueString));
            int applyment = mainContractEdit.Tbl_receiptApply.Count(p => p.MainContractId == mainContractId && p.EndTime > DateTime.Now);

            if ((mainContractProductCount == 0) && (applyment == 0))
            {
                Session["seldMainContractId"] = mainContractId.ToString();

                Response.Redirect("~/Main/contractManager/mainContractEdit.aspx");
            }
            else
            {
                //ClientScriptManager cs = Page.ClientScript;
                //string warnning = " <script> window.alert('fuck!'); </script> ";
                
                //cs.RegisterClientScriptBlock(this.GetType(), "fuck", warnning);
                //Label lbl = (lkb.Parent.Parent as GridViewRow).FindControl("lblMessage") as Label;
                //lbl.Text = "无权更改";
                //lbl.Visible = true;
                Page.ClientScript.ShowAlertWindow("无权修改", this.GetType());
            }
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            int index = mainContractGV.SelectedIndex;
            LinkButton lkb = mainContractGV.Rows[index].FindControl("toDel") as LinkButton;
            lkb.Visible = true;
            
            int mainContractId = int.Parse(lkb.CommandArgument);

            Xm_db xmDataCont = Xm_db.GetInstance();

            var mainContractEdit =
                (from mainContract in xmDataCont.Tbl_mainContract
                where mainContract.MainContractId == mainContractId &&
                      mainContract.EndTime > DateTime.Now
                select mainContract).First();

            int usrId = mainContractEdit.Tbl_projectTagInfo.Tbl_applyment_user.TakeWhile(p => p.ProjectTagId == mainContractEdit.ProjectTagId).First().UsrId;

            int mainContractProductCount = mainContractEdit.Tbl_mainContrctProduct.
                Count(p => p.MainContractId == mainContractId && p.EndTime > DateTime.Now && p.HasSupplier.Equals(bool.TrueString));
            int applyment = mainContractEdit.Tbl_receiptApply.Count(p => p.MainContractId == mainContractId && p.EndTime > DateTime.Now);

            if ((mainContractProductCount == 0) && (applyment == 0))
            { 
                mainContractEdit.EndTime = DateTime.Now;

                foreach (Tbl_mainContrctProduct mcp in mainContractEdit.Tbl_mainContrctProduct)
                {
                    mcp.EndTime = DateTime.Now;
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

                //Label lbl = mainContractGV.Rows[index].FindControl("lblMessage") as Label;
                //lbl.Visible = false;

                
                //MainContractProcess mainContractView = new MainContractProcess(MyDst);
                //mainContractView.UsrId = usrId;

                //mainContractView.RealmainContractProjectUsrView();
                //DataTable taskTable = mainContractView.MyDst.Tables["view_mainContract_project_usr"];

                //Session["ProjectTagProcess"] = mainContractView;
                //Session["dtSources"] = taskTable;


                //mainContractGV.DataSource = Session["dtSources"];
                //mainContractGV.DataBind();

                var mainContractProjectUsrView =
                from mainContractProjectUsr in xmDataCont.View_mainContract_project_usr
                where mainContractProjectUsr.EndTime > DateTime.Now &&
                      mainContractProjectUsr.UsrId == usrId
                select mainContractProjectUsr;

                DataTable taskTable = mainContractProjectUsrView.ToDataTable();

                DataTable dtSource = taskTable;
                Session["dtSources"] = dtSource;

                mainContractGV.DataSource = Session["dtSources"];            
                mainContractGV.DataBind();

                //GridViewRow gvr = mainContractGV.Rows[index];
            }
            else
            {
                //Label lbl = (lkb.Parent.Parent as GridViewRow).FindControl("lblMessage") as Label;
                //lbl.Text = "无权删除";
                //lbl.Visible = true;
                Page.ClientScript.ShowAlertWindow("无权修改", this.GetType());
            }

            mainContractGV.SelectedIndex = -1;
            mainContractGV.Enabled = true;
                
            btnAccept.Visible = false;
            btnCancel.Visible = false;

            btnAdd.Visible = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            mainContractGV.SelectedIndex = -1;
            mainContractGV.Enabled = true;
            //mainContractGV.DataSource = Session["dtSources"];
            //mainContractGV.DataBind();

            btnAccept.Visible = false;
            btnCancel.Visible = false;

            btnAdd.Visible = true;

            //int index = mainContractGV.SelectedIndex;
            //GridViewRow gvr = mainContractGV.Rows[index];

            //Label lbl = gvr.FindControl("lblMessage") as Label;
            //lbl.Visible = false;
        }
    }
}