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
    public partial class subContractAdd : System.Web.UI.Page
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
                #region subContractTable
                DataTable subContractTable = null;
                if (null == Session["subContractTable"])
                {
                    DataRow subContractRow = null;

                    DataColumn colMainContractId = new DataColumn("mainContractId", System.Type.GetType("System.String"));
                    DataColumn colSupplierIdName = new DataColumn("supplierId", System.Type.GetType("System.String"));
                    DataColumn colSubContractTag = new DataColumn("subContractTag", System.Type.GetType("System.String"));
                    DataColumn colCash = new DataColumn("cash", System.Type.GetType("System.String"));
                    DataColumn colDateLine = new DataColumn("dateLine", System.Type.GetType("System.String"));
                    DataColumn colPaymentMode = new DataColumn("paymentMode", System.Type.GetType("System.String"));

                    subContractTable = new DataTable("tbl_subContract");

                    subContractTable.Columns.Add(colMainContractId);
                    subContractTable.Columns.Add(colSupplierIdName);
                    subContractTable.Columns.Add(colSubContractTag);
                    subContractTable.Columns.Add(colCash);
                    subContractTable.Columns.Add(colDateLine);
                    subContractTable.Columns.Add(colPaymentMode);

                    DataRow sessionDr = Session["selMainContractDr"] as DataRow;
                    string mainContractId = sessionDr["mainContractId"].ToString();

                    subContractRow = subContractTable.NewRow();
                    subContractRow["mainContractId"] = mainContractId;
                    subContractRow["supplierId"] = -1;
                    subContractRow["subContractTag"] = string.Empty;
                    subContractRow["cash"] = string.Empty;
                    subContractRow["dateLine"] = DateTime.Now.ToShortDateString();
                    subContractRow["paymentMode"] = string.Empty;
                    subContractTable.Rows.Add(subContractRow);

                    Session["subContractTable"] = subContractTable;
                }
                else
                {
                    subContractTable = Session["subContractTable"] as DataTable;
                }

                txtSubContractTag.Text = subContractTable.Rows[0]["subContractTag"].ToString();
                txtMoney.Text = subContractTable.Rows[0]["cash"].ToString();
                btnDate.Text = subContractTable.Rows[0]["dateLine"].ToString();
                txtPayment.Text = subContractTable.Rows[0]["paymentMode"].ToString();
                #endregion

                #region subProductGV
                if (null == Session["subProductSelDs"])
                {
                }
                else
                {
                    DataTable dt = Session["subProductSelDs"] as DataTable;

                    string strFilter =
                        " checkOrNot = " + "'" + bool.TrueString + "'";
                    dt.DefaultView.RowFilter = strFilter;
                    subProductGV.DataSource = dt;
                    subProductGV.DataBind();
                }
                #endregion

                #region ddlSupplierTable

                DataSet projectDst = new DataSet();

                SupplierProcess ddlSupplierView = new SupplierProcess(projectDst);

                ddlSupplierView.RealSupplierView();
                DataTable ddlSupplierTable = ddlSupplierView.MyDst.Tables["tbl_supplier_company"];

                //DataColumn[] projectKey = new DataColumn[1];
                //projectKey[0] = ddlProjectTable.Columns["projectTagId"];
                //ddlProjectTable.PrimaryKey = projectKey;

                Session["ddlProjectDtS"] = ddlSupplierTable;

                DataRow dr = ddlSupplierTable.NewRow();
                dr["supplierId"] = -1;
                dr["supplierName"] = string.Empty;
                dr["endTime"] = "9999-12-31";
                ddlSupplierTable.Rows.Add(dr);

                ddlSupplier.DataValueField = "supplierId";
                ddlSupplier.DataTextField = "supplierName";
                ddlSupplier.DataSource = ddlSupplierTable;
                ddlSupplier.DataBind();
                ddlSupplier.SelectedValue = subContractTable.Rows[0]["supplierId"].ToString(); ;
                #endregion
            }
        }

        protected DataTable getInput()
        {
            string supplierId = ddlSupplier.SelectedValue;
            string subContractTag = txtSubContractTag.Text.ToString().Trim();
            string cash = txtMoney.Text.ToString().Trim();
            string dateLine = btnDate.Text.ToString();
            string payment = txtPayment.Text.ToString().Trim();

            DataTable subContractTable = Session["subContractTable"] as DataTable;

            subContractTable.Rows[0]["supplierId"] = supplierId;
            subContractTable.Rows[0]["subContractTag"] = subContractTag;
            subContractTable.Rows[0]["cash"] = cash;
            subContractTable.Rows[0]["dateLine"] = dateLine;
            subContractTable.Rows[0]["paymentMode"] = payment;

            return subContractTable;
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {                
                DataTable subContractTable = getInput();

                DataTable dt = Session["subProductSelDs"] as DataTable;
                
                foreach (GridViewRow gvr in subProductGV.Rows)
                {
                    int index = gvr.DataItemIndex;

                    TextBox txb = gvr.FindControl("txtProductNum") as TextBox;
                    if (txb != null)
                    {
                        dt.DefaultView[index]["productNum"] = txb.Text;
                    }
                }

                DataTable contractProductTable = dt.DefaultView.ToTable("tbl_subContrctProduct");
                DataColumn colSubContractId = new DataColumn("subContractId", System.Type.GetType("System.Int32"));
                contractProductTable.Columns.Add(colSubContractId);


                #region dataset
                DataSet dataSet = new DataSet();

                dataSet.Tables.Add(subContractTable);
                dataSet.Tables.Add(contractProductTable);
                #endregion

                subContractProcess scp = new subContractProcess(dataSet);
                scp.MyDst = dataSet;

                scp.Add();
                string subContractId = scp.StrRtn;
                foreach (DataRow dr in contractProductTable.Rows)
                {
                    dr["subContractId"] = subContractId;
                }

                SubContractProductProcess scpp = new SubContractProductProcess(dataSet);

                scpp.Add();

                Response.Redirect("~/Main/DefaultMainSite.aspx");
            }
        }

        protected void calendarSupplier_SelectionChanged(object sender, EventArgs e)
        {
            btnDate.Text = calendarSupplier.SelectedDate.ToShortDateString();

            calendarSupplier.Visible = false;
        }

        protected void subProductGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            subProductGV.PageIndex = e.NewPageIndex;

            subProductGV.DataSource = Session["mainProductSelDs"];
            subProductGV.DataBind();
        }

        protected void subProductGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnProductSel_Click(object sender, EventArgs e)
        {
            getInput();
            Response.Redirect("~/Main/contractManager/subContractProductSel.aspx");
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
            else if (strTxt.Length > 25)
            {
                txtBx.Text = "不能超过25个字！";
                flag = false;
            }
            else if (strTxt.Equals("不能为空！"))
            {
                txtBx.Text = "不能为空！  ";
                flag = false;
            }
            else if (strTxt.Equals("不能超过25个字！"))
            {
                txtBx.Text = "不能超过25个字！  ";
                flag = false;
            }

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

        protected bool subProductGV_Check()
        {
            bool flag = true;
            TextBox txb = null;

            if (0 == subProductGV.Rows.Count)
            {
                flag = false;
            }
            else
            {
                foreach (GridViewRow row in subProductGV.Rows)
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

        protected bool ddlSupplierCheck()
        {
            bool flag = true;

            if (ddlSupplier.SelectedValue.Equals("-1"))
            {
                flag = false;
            }
            else
            {
            }

            return flag;
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

        protected void txtSubContractTag_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBx = sender as TextBox;
            txtNullOrLenth_Check(txtBx);
        }

        protected void txtMoney_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBx = sender as TextBox;
            txtDoubleNumber_Check(txtBx);
        }

        protected void btnDate_Click(object sender, EventArgs e)
        {
            calendarSupplier.Visible = true;
        }

        protected void txtPayment_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBx = sender as TextBox;
            txtNullOrLenth_Check(txtBx);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/contractManager/subContractEditing.aspx");
        }

        protected bool inputCheck()
        {
            bool flag = true;

            flag = txtNullOrLenth_Check(txtSubContractTag)
                && txtDoubleNumber_Check(txtMoney)
                && txtNullOrLenth_Check(txtPayment)
                && subProductGV_Check()
                && ddlSupplierCheck();

            return flag;
        }

        protected void txtProductNum_TextChanged(object sender, EventArgs e)
        {
            TextBox txtNum = sender as TextBox;
            bool flag = txtProductNum_TextCheck(txtNum);
        }

        protected void subProductGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dt = Session["subProductSelDs"] as DataTable;


                int index = e.Row.DataItemIndex;

                //dt.DefaultView[index]["productNum"].ToString();

                TextBox txb = e.Row.FindControl("txtProductNum") as TextBox;
                if (txb != null)
                {
                    txb.Text = dt.DefaultView[index]["productNum"].ToString();
                }
            }
        }
    }
}