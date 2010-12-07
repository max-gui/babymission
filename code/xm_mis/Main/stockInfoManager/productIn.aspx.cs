using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.stockInfoManager
{
    public partial class productIn : System.Web.UI.Page
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
                #region ddlProduct
                
                ProductProcess productView = new ProductProcess(MyDst);
                
                productView.RealProductView();
                DataTable ddlProductTable = productView.MyDst.Tables["tbl_product"].DefaultView.ToTable();
                
                DataRow ddlProductDr = ddlProductTable.NewRow();
                ddlProductDr["productName"] = string.Empty;
                ddlProductDr["productId"] = -1;
                ddlProductTable.Rows.Add(ddlProductDr);

                ddlProduct.DataSource = ddlProductTable;
                ddlProduct.DataTextField = "productName";
                ddlProduct.DataValueField = "productId";
                ddlProduct.DataBind();
                ddlProduct.SelectedValue = "-1";
                
                #endregion

                #region ddlEngineer

                UsrAuthProcess usrAuthView = new UsrAuthProcess(MyDst);

                usrAuthView.View();
                DataTable ddlEngineerTable = usrAuthView.MyDst.Tables["view_usr_autority"].DefaultView.ToTable();

                string authorityName = "货物检验";
                DataRow ddlEngigeerDr = ddlEngineerTable.NewRow();
                ddlEngigeerDr["realName"] = string.Empty;
                ddlEngigeerDr["usrId"] = -1;
                ddlEngigeerDr["authorityName"] = authorityName;
                ddlEngineerTable.Rows.Add(ddlEngigeerDr);

                string strFilter =
                    " authorityName = " + "'" + authorityName + "'";
                ddlEngineerTable.DefaultView.RowFilter = strFilter;

                ddlEngineer.DataSource = ddlEngineerTable;
                ddlEngineer.DataTextField = "realName";
                ddlEngineer.DataValueField = "usrId";
                ddlEngineer.DataBind();
                ddlEngineer.SelectedValue = "-1";

                #endregion

                #region ddlSupplier

                SupplierProcess supplierView = new SupplierProcess(MyDst);

                supplierView.RealSupplierView();
                DataTable ddlSupplierTable = supplierView.MyDst.Tables["tbl_supplier_company"].DefaultView.ToTable();

                DataRow ddlSupplierDr = ddlSupplierTable.NewRow();
                ddlSupplierDr["supplierName"] = string.Empty;
                ddlSupplierDr["supplierId"] = -1;
                ddlSupplierTable.Rows.Add(ddlSupplierDr);

                ddlSupplier.DataSource = ddlSupplierTable;
                ddlSupplier.DataTextField = "supplierName";
                ddlSupplier.DataValueField = "supplierId";
                ddlSupplier.DataBind();
                ddlSupplier.SelectedValue = "-1";

                #endregion
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                string strProductId = ddlProduct.SelectedValue;
                string strProductTag = txtProductTag.Text.Trim();
                string strSupplierId = ddlSupplier.SelectedValue;
                string strUsrId = ddlEngineer.SelectedValue;

                DataRow productInRow = null;

                DataColumn colProductId = new DataColumn("productId", System.Type.GetType("System.String"));
                DataColumn colProductTag = new DataColumn("productTag", System.Type.GetType("System.String"));
                DataColumn colSupplierId = new DataColumn("supplierId", System.Type.GetType("System.String"));
                DataColumn colUsrId = new DataColumn("usrId", System.Type.GetType("System.String"));

                DataTable productInTable = new DataTable("addTable");

                productInTable.Columns.Add(colProductId);
                productInTable.Columns.Add(colProductTag);
                productInTable.Columns.Add(colSupplierId);
                productInTable.Columns.Add(colUsrId);

                productInRow = productInTable.NewRow();
                productInRow["productId"] = strProductId;
                productInRow["productTag"] = strProductTag;
                productInRow["supplierId"] = strSupplierId;
                productInRow["usrId"] = strUsrId;
                productInTable.Rows.Add(productInRow);
                
                #region dataset
                DataSet dataSet = new DataSet();

                dataSet.Tables.Add(productInTable);
                #endregion

                ProductStockProcess psp = new ProductStockProcess(dataSet);

                //psp.DoCheckProductStock();
                //int rowRtn = psp.IntRtn;
                
                string error = psp.Add_includeError();

                if (string.IsNullOrEmpty(error))
                {
                    Xm_db xmDataCont = Xm_db.GetInstance();

                    int usrId = int.Parse(strUsrId);
                    var usrInfo =
                        from usr in xmDataCont.Tbl_usr
                        where usr.UsrId == usrId &&
                              usr.EndTime > DateTime.Now
                        select usr;

                    string strProductName = ddlProduct.SelectedItem.Text;
                    
                    BeckSendMail.getMM().NewMail(usrInfo.First().UsrEmail, 
                        "mis系统入库通知",
                        "编号为" + strProductTag + "的" + strProductName + "等待您的校验" + System.Environment.NewLine
                        + Request.Url.toNewUrlForMail("/Main/stockInfoManager/productCheckView.aspx"));

                    Response.Redirect("~/Main/DefaultMainSite.aspx");
                }
                else
                {
                    this.txtProductTag.Text = error;
                }
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/DefaultMainSite.aspx");
        }

        protected void txtProductTag_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBx = sender as TextBox;
            txtNullOrLenth_Check(txtBx);
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
            else if (strTxt.Equals("已在库"))
            {
                txtBx.Text = "已在库  ";
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

            flag = txtNullOrLenth_Check(txtProductTag)
                && ddlUnSelect_Check(ddlProduct)
                && ddlUnSelect_Check(ddlSupplier) 
                && ddlUnSelect_Check(ddlEngineer);

            return flag;
        }
    }
}