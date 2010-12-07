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
    public partial class subContractToEdit : System.Web.UI.Page
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
                int subContractId = int.Parse(Session["seldSubContractId"] as string);
                Xm_db xmDataCont = Xm_db.GetInstance();

                var subContractEdit =
                    (from subContract in xmDataCont.Tbl_subContract
                     where subContract.MainContractId == subContractId &&
                           subContract.EndTime > DateTime.Now
                     select subContract).First();

                txtSubContractTag.Text = subContractEdit.SubContractTag;
                txtMoney.Text = subContractEdit.Cash.ToString();
                btnDate.Text = subContractEdit.DateLine.ToString();
                txtPayment.Text = subContractEdit.PaymentMode;

                #region subProductGV

                var subProductSelDs =
                    from subContrctProduct in subContractEdit.Tbl_subContrctProduct
                    join product in xmDataCont.Tbl_product on
                         subContrctProduct.ProductId equals product.ProductId
                    select new { product.ProductName, subContrctProduct.ProductNum };

                DataTable dt = subProductSelDs.ToDataTable();

                if (dt.Rows.Count > 0)
                {
                    subProductGV.DataSource = dt;
                    subProductGV.DataBind();
                }
                #endregion

                #region ddlSupplierTable

                DataSet projectDst = new DataSet();

                SupplierProcess ddlSupplierView = new SupplierProcess(projectDst);

                ddlSupplierView.RealSupplierView();
                DataTable ddlSupplierTable = ddlSupplierView.MyDst.Tables["tbl_supplier_company"];

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
                ddlSupplier.SelectedValue = subContractEdit.SupplierId.ToString();
                #endregion
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                string supplierId = ddlSupplier.SelectedValue;
                string subContractTag = txtSubContractTag.Text.ToString().Trim();
                string cash = txtMoney.Text.ToString().Trim();
                string dateLine = btnDate.Text.ToString();
                string payment = txtPayment.Text.ToString().Trim();

                int subContractId = int.Parse(Session["seldSubContractId"] as string);
                Xm_db xmDataCont = Xm_db.GetInstance();

                var subContractEdit =
                   (from subContract in xmDataCont.Tbl_subContract
                    where subContract.MainContractId == subContractId &&
                          subContract.EndTime > DateTime.Now
                    select subContract).First();

                subContractEdit.SubContractTag = subContractTag;
                subContractEdit.Cash = decimal.Parse(cash);
                subContractEdit.DateLine = calendarSupplier.SelectedDate;
                subContractEdit.PaymentMode = payment;

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

                Session.Remove("subContractTable");
                Session.Remove("subProductSelDs");
                Session.Remove("ddlProjectDtS");
                Response.Redirect("~/Main/contractManager/subContractEditing.aspx");
            }
        }

        protected void calendarSupplier_SelectionChanged(object sender, EventArgs e)
        {
            btnDate.Text = calendarSupplier.SelectedDate.ToString();

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
            Session.Remove("subContractTable");
            Session.Remove("subProductSelDs");
            Session.Remove("ddlProjectDtS");
            Response.Redirect("~/Main/contractManager/subContractEditing.aspx");
        }

        protected bool inputCheck()
        {
            bool flag = true;

            flag = txtNullOrLenth_Check(txtSubContractTag)
                && txtDoubleNumber_Check(txtMoney)
                && txtNullOrLenth_Check(txtPayment)
                && ddlSupplierCheck();

            return flag;
        }

        protected void txtProductNum_TextChanged(object sender, EventArgs e)
        {
            TextBox txtNum = sender as TextBox;
            bool flag = txtProductNum_TextCheck(txtNum);
        }
    }
}