using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.projectTagInfoManager.borrowed
{
    public partial class newBorrow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.bor_retApply);
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

                #region productStockRelationTable

                DataSet myDst = new DataSet();
                ProductPurposeRelationProcess pprpView = new ProductPurposeRelationProcess(myDst);

                Session["ProductPurposeRelationProcess"] = pprpView;
                pprpView.RealProductPurposeRelationView();

                DataTable productStockRelationTable = pprpView.MyDst.Tables["view_productStockRelation"].DefaultView.ToTable();

                string strFilter =
                    " productPurpose = " + "'" + "forBorrow".ToString() + "'";
                productStockRelationTable.DefaultView.RowFilter = strFilter;
                Session["view_productStockRelation"] = productStockRelationTable.DefaultView.ToTable();

                borrowProductGV.DataSource = Session["view_productStockRelation"];
                borrowProductGV.DataBind();

                btnIn.CommandArgument = bool.TrueString;
                btnOut.CommandArgument = bool.FalseString;
                #endregion

                #region ddlApproveUsr

                UsrAuthProcess usrAuthView = new UsrAuthProcess(myDst);

                usrAuthView.View();
                DataTable ddlEngineerTable = usrAuthView.MyDst.Tables["view_usr_autority"].DefaultView.ToTable();

                string authorityName = "送修/借用审批";
                DataRow ddlApproveUsrDr = ddlEngineerTable.NewRow();
                ddlApproveUsrDr["realName"] = string.Empty;
                ddlApproveUsrDr["usrId"] = -1;
                ddlApproveUsrDr["authorityName"] = authorityName;
                ddlEngineerTable.Rows.Add(ddlApproveUsrDr);

                strFilter =
                    " authorityName = " + "'" + authorityName + "'";
                ddlEngineerTable.DefaultView.RowFilter = strFilter;

                ddlApproveUsr.DataSource = ddlEngineerTable;
                ddlApproveUsr.DataTextField = "realName";
                ddlApproveUsr.DataValueField = "usrId";
                ddlApproveUsr.DataBind();
                ddlApproveUsr.SelectedValue = "-1";

                #endregion
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                DataTable dt = (Session["view_productStockRelation"] as DataTable).DefaultView.ToTable();


                string productPurposeRelationId = btnOk.CommandArgument;
                string strFilter =
                   " productPurposeRelationId = " + "'" + productPurposeRelationId + "'";
                dt.DefaultView.RowFilter = strFilter;

                string applyUsrId = Session["usrId"].ToString();
                string approveUsrId = ddlApproveUsr.SelectedValue;
                string borrowedSynopsis = txtSynopsis.Text.ToString();
                string custManId = ddlCustMan.SelectedValue.ToString();
                string projectOutAddress = txtProjAddr.Text.ToString();
                string borrowedTag = tag_init();
                string productStockId = dt.DefaultView[0]["productStockId"].ToString();

                #region dataset
                DataSet dataSet = new DataSet();
                DataRow borrowedRow = null;

                DataColumn colProductPurposeRelationId = new DataColumn("productPurposeRelationId", System.Type.GetType("System.String"));
                DataColumn colApplyUsrId = new DataColumn("applyUsrId", System.Type.GetType("System.String"));
                DataColumn colApproveUsrId = new DataColumn("approveUsrId", System.Type.GetType("System.String"));
                DataColumn colSynopsis = new DataColumn("borrowedSynopsis", System.Type.GetType("System.String"));
                DataColumn colBorrowedTag = new DataColumn("borrowedTag", System.Type.GetType("System.String"));
                DataColumn colProductStockId = new DataColumn("productStockId", System.Type.GetType("System.String"));
                DataColumn colManId = new DataColumn("custManId", System.Type.GetType("System.String"));

                DataTable borrowedTable = new DataTable("addTable");

                borrowedTable.Columns.Add(colProductPurposeRelationId);
                borrowedTable.Columns.Add(colApplyUsrId);
                borrowedTable.Columns.Add(colApproveUsrId);
                borrowedTable.Columns.Add(colSynopsis);
                borrowedTable.Columns.Add(colBorrowedTag);
                borrowedTable.Columns.Add(colProductStockId);
                borrowedTable.Columns.Add(colManId);

                borrowedRow = borrowedTable.NewRow();
                borrowedRow["productPurposeRelationId"] = productPurposeRelationId;
                borrowedRow["applyUsrId"] = applyUsrId;
                borrowedRow["approveUsrId"] = approveUsrId;
                borrowedRow["borrowedSynopsis"] = borrowedSynopsis;
                borrowedRow["borrowedTag"] = borrowedTag;
                borrowedRow["productStockId"] = productStockId;
                borrowedRow["custManId"] = custManId;
                borrowedTable.Rows.Add(borrowedRow);

                dataSet.Tables.Add(borrowedTable);
                #endregion

                BorrowedProductProcesscs bpp = new BorrowedProductProcesscs(dataSet);
                bpp.MyDst = dataSet;

                bpp.Add();
                Xm_db xmDataCont = Xm_db.GetInstance();

                int businessProductId = int.Parse(bpp.StrRtn);

                var businessEdit =
                    (from business in xmDataCont.Tbl_businessProduct
                     where business.BusinessProductId == businessProductId
                    select business).First();

                int projectTagId = businessEdit.ProjectTagId;

                var projectEdit =
                    (from project in xmDataCont.Tbl_projectTagInfo
                     where project.ProjectTagId == projectTagId
                    select project).First();

                projectEdit.ProjectOutAddress = projectOutAddress;

                try
                {
                    //xmDataCont.Refresh(System.Data.Linq.RefreshMode.KeepChanges, xmDataCont.tbl_mainContract);
                    //xmDataCont.Refresh(System.Data.Linq.RefreshMode.KeepChanges, xmDataCont.tbl_projectTagInfo);
                    xmDataCont.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
                }
                catch (System.Data.Linq.ChangeConflictException cce)
                {
                    string strEx = cce.Message;
                    foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataCont.ChangeConflicts)
                    {
                        //No database values are merged into current.
                        occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                    }

                    xmDataCont.SubmitChanges();
                }

                Response.Redirect("~/Main/projectTagInfoManager/borrowed/borrowSearch.aspx");
            }
        }

        protected string tag_init()
        {
            Xm_db xmDataCont = Xm_db.GetInstance();

            DateTime dtDate = new DateTime(DateTime.Now.Year, 1, 1);

            var borrowEdit =
                from borrowProj in xmDataCont.View_project_tag
                where borrowProj.ProjectDetail == "borrow" && borrowProj.StartTime >= dtDate
                select borrowProj.ProductTag;

            int count = borrowEdit.Distinct().Count() + 1;

            string splitTemp = "-";
            string newTag = "borrow" + splitTemp + dtDate.Year.ToString() + splitTemp + count.ToString();
            //string newTag = "returned";

            return newTag;
        }

        protected void ddlCustComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable custManDt = Session["ddlCustManDtS"] as DataTable;

            string strFilter =
                 " custCompyId = " + "'" + ddlCustComp.SelectedValue.ToString() + "'";
            custManDt.DefaultView.RowFilter = strFilter;
            custManDt = custManDt.DefaultView.ToTable();

            DataRow newDr = custManDt.NewRow();
            string strSelV = string.Empty;
            if (this.ddlCustComp.SelectedValue.Equals("-2"))
            {

                newDr["custManId"] = -2;
                newDr["custManName"] = "无";
                custManDt.Rows.Add(newDr);

                strSelV = "-2";
            }
            else
            {
                newDr = custManDt.NewRow();
                newDr["custManId"] = -1;
                newDr["custManName"] = "";
                custManDt.Rows.Add(newDr);

                strSelV = "-1";
            }

            ddlCustMan.DataValueField = "custManId";
            ddlCustMan.DataTextField = "custManName";
            ddlCustMan.DataSource = custManDt;
            ddlCustMan.DataBind();

            ddlCustMan.SelectedValue = strSelV;

            DataTable custCompDt = Session["ddlCustCompDtS"] as DataTable;
            DataRow dr = custCompDt.Rows.Find(ddlCustComp.SelectedValue.ToString());
            lblCustCompAddr.Text = dr["custCompAddress"].ToString();
            lblCustCompAddr.Visible = true;
        }

        protected void ddlCustMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable custManDt = Session["ddlCustManDtS"] as DataTable;
            DataRow dr = custManDt.Rows.Find(ddlCustMan.SelectedValue.ToString());
            lblCustManCont.Text = dr["custManContact"].ToString();
            lblCustManCont.Visible = true;
            lblCustManEmail.Text = dr["custManEmail"].ToString();
            lblCustManEmail.Visible = true;
        }

        protected bool txtNullOrLenth_Check(TextBox txtBx)
        {
            bool flag = true;

            string strTxt = txtBx.Text.ToString().Trim();
            if (string.IsNullOrWhiteSpace(strTxt))
            {
                txtBx.Text = "不能为空！";
                flag = false;
            }
            else if (strTxt.Length > 50)
            {
                txtBx.Text = "不能超过50个字！";
                flag = false;
            }
            else if (strTxt.Equals("不能为空！"))
            {
                txtBx.Text = "不能为空！  ";
                flag = false;
            }
            else if (strTxt.Equals("不能超过50个字！"))
            {
                txtBx.Text = "不能超过50个字！  ";
                flag = false;
            }

            return flag;
        }

        protected bool ddlUnSelect_Check(DropDownList ddl)
        {
            bool flag = true;

            if (ddl.SelectedValue.Equals("-1"))
            {
                flag = false;
            }
            else
            {
            }

            return flag;
        }

        protected bool inputCheck()
        {
            bool flag = true;

            flag = txtNullOrLenth_Check(txtSynopsis)
                && txtNullOrLenth_Check(txtProjAddr)
                && ddlUnSelect_Check(ddlApproveUsr)
                && !string.IsNullOrEmpty(btnOk.CommandArgument.Trim());

            if (btnOut.CommandArgument.Equals(bool.FalseString))
            {
                ddlCustComp.SelectedValue = "-1";
                ddlCustMan.SelectedValue = "-1";
            }
            else
            {
                flag = flag
                && ddlUnSelect_Check(ddlCustComp)
                && ddlUnSelect_Check(ddlCustMan);
            }

            return flag;
        }

        protected void txtSynopsis_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBx = sender as TextBox;
            txtNullOrLenth_Check(txtBx);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/projectTagInfoManager/borrowed/borrowSearch.aspx");
        }

        protected void outBorrowInit()
        {
            DataSet MyDst = new DataSet();

            custCompProcess ddlCustCompView = new custCompProcess(MyDst);
            custManProcess ddlCustManView = new custManProcess(MyDst);
            ddlCustManView.CustCompId = "-1".ToString();
            ProjectTagProcess ptView = new ProjectTagProcess(MyDst);

            ddlCustCompView.RealCompView();
            ddlCustManView.RealCustManView();
            DataTable ddlCustCompTable = ddlCustCompView.MyDst.Tables["tbl_customer_company"];
            DataTable ddlCustManTable = ddlCustManView.MyDst.Tables["tbl_customer_manager"];

            DataColumn[] custCompKey = new DataColumn[1];
            custCompKey[0] = ddlCustCompTable.Columns["custCompyId"];
            ddlCustCompTable.PrimaryKey = custCompKey;
            DataColumn[] custManpKey = new DataColumn[1];
            custManpKey[0] = ddlCustManTable.Columns["custManId"];
            ddlCustManTable.PrimaryKey = custManpKey;

            Session["custCompProcess"] = ddlCustCompView;
            Session["ddlCustCompDtS"] = ddlCustCompTable;
            Session["custManProcess"] = ddlCustManView;
            Session["ddlCustManDtS"] = ddlCustManTable;
            Session["ProjectTagProcess"] = ptView;

            DataRow dr = ddlCustCompTable.NewRow();
            dr["custCompyId"] = -1;
            dr["custCompName"] = string.Empty;
            dr["endTime"] = "9999-12-31";
            ddlCustCompTable.Rows.Add(dr);

            ddlCustComp.DataValueField = "custCompyId";
            ddlCustComp.DataTextField = "custCompName";
            ddlCustComp.DataSource = ddlCustCompTable;
            ddlCustComp.DataBind();
            ddlCustComp.SelectedValue = "-1";  
        }

        protected void btnIn_Click(object sender, EventArgs e)
        {
            lblCustCompAddrShow.Visible = false;
            lblCustCompAddr.Visible = false;
            lblCustCompNameShow.Visible = false;
            ddlCustComp.Visible = false;
            lblCustManShow.Visible = false;
            ddlCustMan.Visible = false;
            lblCustManContantShow.Visible = false;
            lblCustManCont.Visible = false;
            lblCustManEmailShow.Visible = false;
            lblCustManEmail.Visible = false;

            btnIn.Visible = false;
            btnOut.Visible = true;
            btnIn.CommandArgument = bool.TrueString;
            btnOut.CommandArgument = bool.FalseString;  
        }

        protected void btnOut_Click(object sender, EventArgs e)
        {
            lblCustCompAddrShow.Visible = true;
            lblCustCompAddr.Visible = true;
            lblCustCompNameShow.Visible = true;
            ddlCustComp.Visible = true;
            lblCustManShow.Visible = true;
            ddlCustMan.Visible = true;
            lblCustManContantShow.Visible = true;
            lblCustManCont.Visible = true;
            lblCustManEmailShow.Visible = true;
            lblCustManEmail.Visible = true;

            btnIn.Visible = true;
            btnOut.Visible = false;
            btnIn.CommandArgument = bool.FalseString;
            btnOut.CommandArgument = bool.TrueString;

            outBorrowInit();
        }

        protected void selectOk_Click(object sender, EventArgs e)
        {
            GridViewRow gvr = (sender as LinkButton).Parent.Parent as GridViewRow;

            int index = gvr.DataItemIndex;

            DataTable dt = (Session["view_productStockRelation"] as DataTable).DefaultView.ToTable();

            string productPurposeRelationId = dt.Rows[index]["productPurposeRelationId"].ToString();

            string strFilter =
                " productPurposeRelationId = " + "'" + productPurposeRelationId + "'";
            dt.DefaultView.RowFilter = strFilter;

            DataTable newDs = dt.DefaultView.ToTable();
            dt.Clear();

            borrowProductGV.Columns[2].Visible = false;
            borrowProductGV.Columns[3].Visible = true;

            borrowProductGV.Caption = "送修产品";
            borrowProductGV.DataSource = newDs;
            borrowProductGV.DataBind();

            newDs.Clear();

            btnOk.CommandArgument = productPurposeRelationId;
        }

        protected void selectNo_Click(object sender, EventArgs e)
        {
            borrowProductGV.Columns[2].Visible = true;
            borrowProductGV.Columns[3].Visible = false;

            borrowProductGV.Caption = "产品信息";
            borrowProductGV.DataSource = Session["view_productStockRelation"];
            borrowProductGV.DataBind();

            btnOk.CommandArgument = string.Empty;
        }

        protected void borrowProductGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            borrowProductGV.PageIndex = e.NewPageIndex;

            borrowProductGV.DataSource = Session["view_productStockRelation"];
            borrowProductGV.DataBind();
        }

        protected void borrowProductGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }
    }
}