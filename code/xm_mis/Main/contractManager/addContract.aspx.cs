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
    public partial class addContract : System.Web.UI.Page
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
                #region ddlCustComp

                DataSet dst = new DataSet();

                custCompProcess ddlCustCompView = new custCompProcess(dst);

                ddlCustCompView.RealCompView();
                DataTable ddlCustCompTable = ddlCustCompView.MyDst.Tables["tbl_customer_company"].DefaultView.ToTable();

                //DataColumn[] projectKey = new DataColumn[1];
                //projectKey[0] = ddlProjectTable.Columns["projectTagId"];
                //ddlProjectTable.PrimaryKey = projectKey;

                

                DataRow dr = ddlCustCompTable.NewRow();
                dr["custCompyId"] = -1;
                dr["custCompName"] = string.Empty;
                dr["endTime"] = "9999-12-31";
                ddlCustCompTable.Rows.Add(dr);

                Session["ddlProjectDtS"] = ddlCustCompTable;

                ddlCustComp.DataValueField = "custCompyId";
                ddlCustComp.DataTextField = "custCompName";
                ddlCustComp.DataSource = Session["ddlProjectDtS"];
                ddlCustComp.DataBind();

                #endregion

                #region mainContractTable
                DataTable mainContractTable = null;
                if (null == Session["mainContractTable"])
                {
                    DataRow mainContractRow = null;

                    DataColumn colProjectTagId = new DataColumn("projectTagId", System.Type.GetType("System.String"));
                    DataColumn colCustCompId = new DataColumn("custCompyId", System.Type.GetType("System.String"));
                    DataColumn colMainContractTag = new DataColumn("mainContractTag", System.Type.GetType("System.String"));
                    DataColumn colCash = new DataColumn("cash", System.Type.GetType("System.String"));
                    DataColumn colDateLine = new DataColumn("dateLine", System.Type.GetType("System.String"));
                    DataColumn colPaymentMode = new DataColumn("paymentMode", System.Type.GetType("System.String"));

                    mainContractTable = new DataTable("tbl_mainContract");

                    mainContractTable.Columns.Add(colProjectTagId);
                    mainContractTable.Columns.Add(colCustCompId);
                    mainContractTable.Columns.Add(colMainContractTag);
                    mainContractTable.Columns.Add(colCash);
                    mainContractTable.Columns.Add(colDateLine);
                    mainContractTable.Columns.Add(colPaymentMode);

                    mainContractRow = mainContractTable.NewRow();
                    mainContractRow["projectTagId"] = -1;
                    mainContractRow["custCompyId"] = -1;
                    mainContractRow["mainContractTag"] = string.Empty;
                    mainContractRow["cash"] = string.Empty;
                    mainContractRow["dateLine"] = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
                    mainContractRow["paymentMode"] = string.Empty;
                    mainContractTable.Rows.Add(mainContractRow);

                    Session["mainContractTable"] = mainContractTable;
                }
                else
                {
                    mainContractTable = Session["mainContractTable"] as DataTable;
                }

                ddlCustComp.SelectedValue = mainContractTable.Rows[0]["custCompyId"].ToString();
                txtMainContractTag.Text = mainContractTable.Rows[0]["mainContractTag"].ToString();
                txtMoney.Text = mainContractTable.Rows[0]["cash"].ToString();
                btnDate.Text = mainContractTable.Rows[0]["dateLine"].ToString();
                txtPayment.Text = mainContractTable.Rows[0]["paymentMode"].ToString();
                #endregion

                #region mainProductGV
                if (null == Session["mainProductSelDs"])
                {
                }
                else
                {
                    DataTable dt = Session["mainProductSelDs"] as DataTable;

                    string strFilter =
                        " checkOrNot = " + "'" + bool.TrueString + "'";
                    dt.DefaultView.RowFilter = strFilter;
                    mainProductGV.DataSource = dt;
                    mainProductGV.DataBind();
                }
                #endregion

                #region ddlProjectTable
                string usrId = Session["usrId"] as string;
                
                ProjectTagProcess ddlProjectView = new ProjectTagProcess(dst);

                ddlProjectView.RealProjTagView(usrId);
                DataTable ddlProjectTable = ddlProjectView.MyDst.Tables["projectTag_view"].DefaultView.ToTable();                

                DataColumn[] projectKey = new DataColumn[1];
                projectKey[0] = ddlProjectTable.Columns["projectTagId"];
                ddlProjectTable.PrimaryKey = projectKey;

                Session["ddlProjectDtS"] = ddlProjectTable;

                dr = ddlProjectTable.NewRow();
                dr["projectTagId"] = -1;
                dr["projectTag"] = string.Empty;
                dr["endTime"] = "9999-12-31";
                dr["applymentUsrId"] = usrId;
                dr["custCompyId"] = -1;
                ddlProjectTable.Rows.Add(dr);

                ddlProjectTag.DataValueField = "projectTagId";
                ddlProjectTag.DataTextField = "projectTag";
                ddlProjectTag.DataSource = ddlProjectTable;
                ddlProjectTag.DataBind();
                ddlProjectTag.SelectedValue = mainContractTable.Rows[0]["projectTagId"].ToString(); ;
                #endregion
            }
        }

        protected DataTable getInput()
        {            
            string projectTagId = ddlProjectTag.SelectedValue;
            string custmor = ddlCustComp.SelectedValue;
            string mainContractTag = txtMainContractTag.Text.ToString().Trim();
            string cash = txtMoney.Text.ToString().Trim();
            string dateLine = btnDate.Text.ToString();
            string payment = txtPayment.Text.ToString().Trim();

            DataTable mainContractTable = Session["mainContractTable"] as DataTable;

            mainContractTable.Rows[0]["projectTagId"] = projectTagId;
            mainContractTable.Rows[0]["custCompyId"] = custmor;
            mainContractTable.Rows[0]["mainContractTag"] = mainContractTag;
            mainContractTable.Rows[0]["cash"] = cash;
            mainContractTable.Rows[0]["dateLine"] = dateLine;
            mainContractTable.Rows[0]["paymentMode"] = payment;

            return mainContractTable;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                //string projectTagId = ddlProjectTag.SelectedValue;
                //string custmor = txtCustmor.Text.ToString().Trim();
                //string mainContractTag = txtMainContractTag.Text.ToString().Trim();
                //string cash = txtMoney.Text.ToString().Trim();
                //string dateLine = btnDate.Text.ToString();
                //string payment = txtPayment.Text.ToString().Trim();

                
                //DataTable dt = Session["mainProductSelDs"] as DataTable;
                //foreach (GridViewRow gvr in mainProductGV.Rows)
                //{
                //    int index = gvr.DataItemIndex;

                //    TextBox txb = gvr.FindControl("txtProductNum") as TextBox;
                //    if (txb != null)
                //    {
                //        dt.DefaultView[index]["productNum"] = txb.Text;
                //    }
                //}

                //DataTable contractProductTable = dt.DefaultView.ToTable("tbl_mainContrctProduct");
                //DataColumn colMainContractId = new DataColumn("mainContractId", System.Type.GetType("System.Int32"));
                //contractProductTable.Columns.Add(colMainContractId);

                //#region dataset
                //DataSet dataSet = new DataSet();
                //DataRow mainContractRow = null;

                //DataColumn colProjectTagId = new DataColumn("projectTagId", System.Type.GetType("System.String"));
                //DataColumn colContractCompName = new DataColumn("contractCompName", System.Type.GetType("System.String"));
                //DataColumn colMainContractTag = new DataColumn("mainContractTag", System.Type.GetType("System.String"));
                //DataColumn colCash = new DataColumn("cash", System.Type.GetType("System.String"));
                //DataColumn colDateLine = new DataColumn("dateLine", System.Type.GetType("System.String"));
                //DataColumn colPaymentMode = new DataColumn("paymentMode", System.Type.GetType("System.String"));

                //DataTable mainContractTable = new DataTable("tbl_mainContract");

                //mainContractTable.Columns.Add(colProjectTagId);
                //mainContractTable.Columns.Add(colContractCompName);
                //mainContractTable.Columns.Add(colMainContractTag);
                //mainContractTable.Columns.Add(colCash);
                //mainContractTable.Columns.Add(colDateLine);
                //mainContractTable.Columns.Add(colPaymentMode);

                //mainContractRow = mainContractTable.NewRow();
                //mainContractRow["projectTagId"] = projectTagId;
                //mainContractRow["contractCompName"] = custmor;
                //mainContractRow["mainContractTag"] = mainContractTag;
                //mainContractRow["cash"] = cash;
                //mainContractRow["dateLine"] = dateLine;
                //mainContractRow["paymentMode"] = payment;
                //mainContractTable.Rows.Add(mainContractRow);

                //dataSet.Tables.Add(mainContractTable);
                //dataSet.Tables.Add(contractProductTable);
                //#endregion
                DataTable mainContractTable = getInput();

                string projectOutAddress = txtProjAddr.Text.ToString();

                DataTable dt = Session["mainProductSelDs"] as DataTable;
                foreach (GridViewRow gvr in mainProductGV.Rows)
                {
                    int index = gvr.DataItemIndex;

                    TextBox txb = gvr.FindControl("txtProductNum") as TextBox;
                    if (txb != null)
                    {
                        dt.DefaultView[index]["productNum"] = txb.Text;
                    }
                }

                DataTable contractProductTable = dt.DefaultView.ToTable("tbl_mainContrctProduct");
                DataColumn colMainContractId = new DataColumn("mainContractId", System.Type.GetType("System.Int32"));
                contractProductTable.Columns.Add(colMainContractId);

                #region dataset
                DataSet dataSet = new DataSet();

                dataSet.Tables.Add(mainContractTable);
                dataSet.Tables.Add(contractProductTable);
                #endregion

                MainContractProcess mcp = new MainContractProcess(dataSet);

                mcp.Add();
                string strMainContractId = mcp.StrRtn;
                foreach (DataRow dr in contractProductTable.Rows)
                {
                    dr["mainContractId"] = strMainContractId;
                }

                MainContractProductProcess mcpp = new MainContractProductProcess(dataSet);

                mcpp.Add();
                
                Xm_db xmDataCont = Xm_db.GetInstance();

                int mainContractId = int.Parse(strMainContractId);

                var mainContractEdit =
                    (from mainContract in xmDataCont.Tbl_mainContract
                     where mainContract.MainContractId == mainContractId
                     select mainContract).First();

                int projectTagId = mainContractEdit.ProjectTagId;
                //int projectTagId = int.Parse(mainContractTable.Rows[0]["projectTagId"].ToString());
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

                Session.Remove("ddlProjectDtS");
                Session.Remove("mainContractTable");
                Session.Remove("mainProductSelDs");
                Response.Redirect("~/Main/contractManager/subContractEdit.aspx");
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Session.Remove("ddlProjectDtS");
            Session.Remove("mainContractTable");
            Session.Remove("mainProductSelDs");
            Response.Redirect("~/Main/DefaultMainSite.aspx");
        }

        protected void btnDate_Click(object sender, EventArgs e)
        {
            calendarCust.Visible = true;
        }

        protected void calendarCust_SelectionChanged(object sender, EventArgs e)
        {
            btnDate.Text = calendarCust.SelectedDate.ToString();

            calendarCust.Visible = false;
        }

        protected void btnProductSel_Click(object sender, EventArgs e)
        {
            //DataTable dt = Session["mainProductSelDs"] as DataTable;
            getInput();
            Response.Redirect("~/Main/contractManager/mainContractProductSel.aspx");
        }

        protected void mainProductGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mainProductGV.PageIndex = e.NewPageIndex;

            mainProductGV.DataSource = Session["mainProductSelDs"];//["dtSources"] as DataTable;  
            mainProductGV.DataBind();
        }

        //protected void mainProductGV_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    // By default, set the sort direction to ascending.
        //    if (mainProductGV.SelectedIndex == -1)
        //    {
        //        DataTable dt = Session["dtSources"] as DataTable;

        //        if (dt != null)
        //        {
        //            //Sort the data.
        //            dt.DefaultView.Sort = e.SortExpression.GetSortDirectionExpression(ViewState); //GetSortDirectionExpression(e.SortExpression, ViewState);
        //            mainProductGV.DataSource = Session["dtSources"];
        //            mainProductGV.DataBind();
        //        }
        //    }
        //}

        protected void mainProductGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = Session["mainProductSelDs"] as DataTable;
                

                int index = e.Row.DataItemIndex;

                //dt.DefaultView[index]["productNum"].ToString();

                TextBox txb = e.Row.FindControl("txtProductNum") as TextBox;
                if (txb != null)
                {
                    txb.Text = dt.DefaultView[index]["productNum"].ToString();
                }
            }
        }

        protected void txtProductNum_TextChanged(object sender, EventArgs e)
        {
            TextBox txtNum = sender as TextBox;

            bool flag = txtProductNum_TextCheck(txtNum);
        }

        protected bool txtProductNum_TextCheck(TextBox txtBx)
        {
            bool flag = true;
            int sc = 0;
            try
            {
                sc = int.Parse(txtBx.Text.ToString().Trim());
                txtBx.Text = sc.ToString();
                //Session["flagContact"] = bool.TrueString.ToString().Trim();
            }
            catch (ArgumentNullException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }
            catch (FormatException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }
            catch (OverflowException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }

            //btnOk();

            return flag;
        }

        protected bool txtDoubleNumber_Check(TextBox txtBx)
        {
            bool flag = true;

            string strBx = string.Empty;
            double sc = 0;
            try
            {
                strBx = txtBx.Text.ToString().Trim();
                sc = double.Parse(strBx);
                txtBx.Text = strBx;
                //Session["flagContact"] = bool.TrueString.ToString().Trim();
            }
            catch (ArgumentNullException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }
            catch (FormatException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }
            catch (OverflowException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }

            //btnOk();

            return flag;
        }

        protected bool mainProductGV_Check()
        {
            bool flag = true;

            TextBox txb = null;

            if (0 == mainProductGV.Rows.Count)
            {
                flag = false;
            }
            else
            {
                foreach (GridViewRow row in mainProductGV.Rows)
                {
                    txb = row.FindControl("txtProductNum") as TextBox;
                    if (txb != null)
                    {
                        flag = flag && txtProductNum_TextCheck(txb);
                    }
                }
            }

            return flag;
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

        protected void ddlProjectTag_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable projectDt = Session["ddlProjectDtS"] as DataTable;

            DataRow dr = projectDt.Rows.Find(ddlProjectTag.SelectedValue.ToString());

            string custCompyId = dr["custCompyId"].ToString();
            ddlCustComp.SelectedValue = custCompyId;
            
            if (custCompyId.Equals(string.Empty))
            {
                ddlCustComp.Enabled = false;
            }
            else
            {
                ddlCustComp.Enabled = true;
            }
        }

        protected void txtMainContractTag_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBx = sender as TextBox;
            txtNullOrLenth_Check(txtBx);
        }

        protected void txtCustmor_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBx = sender as TextBox;
            txtNullOrLenth_Check(txtBx);
        }

        protected void txtMoney_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBx = sender as TextBox;
            txtDoubleNumber_Check(txtBx);
        }

        protected void txtPayment_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBx = sender as TextBox;
            txtNullOrLenth_Check(txtBx);
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
                        
            flag = txtNullOrLenth_Check(txtMainContractTag)
                && txtNullOrLenth_Check(txtProjAddr)
                && ddlUnSelect_Check(ddlProjectTag)
                && ddlUnSelect_Check(ddlCustComp) 
                && txtDoubleNumber_Check(txtMoney) 
                && txtNullOrLenth_Check(txtPayment)
                && mainProductGV_Check();

            return flag;
        }
    }
}