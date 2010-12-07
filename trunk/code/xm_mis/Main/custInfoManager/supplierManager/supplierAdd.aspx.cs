using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.custInfoManager.supplierManager
{
    public partial class supplierAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.custManager);
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
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                string sn = txtSupplierName.Text.ToString().Trim();
                string sa = txtSupplierAddr.Text.ToString().Trim();

                #region dataset
                DataSet dataSet = new DataSet();
                DataRow supplierRow = null;

                DataColumn colSupplierName = new DataColumn("supplierName", System.Type.GetType("System.String"));
                DataColumn colSupplierAddr = new DataColumn("supplierAddress", System.Type.GetType("System.String"));

                DataTable supplierTable = new DataTable("tbl_supplier_company");

                supplierTable.Columns.Add(colSupplierName);
                supplierTable.Columns.Add(colSupplierAddr);

                supplierRow = supplierTable.NewRow();
                supplierRow["supplierName"] = sn;
                supplierRow["supplierAddress"] = sa;
                supplierTable.Rows.Add(supplierRow);

                dataSet.Tables.Add(supplierTable);
                #endregion

                DataSet dsCheck = new DataSet();
                SupplierProcess sp = new SupplierProcess(dsCheck);

                sp.RealSupplierView();
                int rowRtn = -1;

                sp.DoCheckSupplierName(sn);
                rowRtn = sp.IntRtn;
                if (0 == rowRtn)
                {
                    sp.MyDst = dataSet;

                    sp.Add();

                    Response.Redirect("~/Main/custInfoManager/supplierManager/supplierEdit.aspx");
                }
                else
                {
                    lblSupplierName.Text = "公司名已存在!";
                }
            }
        }

        protected bool txtSupplierName_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtSupplierName.Text.ToString().Trim()))
            {
                lblSupplierName.Text = "*必填项!";
                flag = false;
            }
            else if (txtSupplierName.Text.ToString().Trim().Length > 20)
            {
                lblSupplierName.Text = "公司名字太长!";
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblSupplierName.Text = string.Empty;
                //Session["flagUsrName"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }

        protected bool txtSupplierAddr_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtSupplierAddr.Text.ToString().Trim()))
            {
                lblSupplierAddr.Text = "*必填项!";
                flag = false;
            }
            else if (txtSupplierAddr.Text.ToString().Trim().Length > 20)
            {
                lblSupplierAddr.Text = "公司地址太长!";
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblSupplierAddr.Text = string.Empty;
                //Session["flagUsrName"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }

        protected bool inputCheck()
        {
            bool flag = true;
            if (!txtSupplierName_TextCheck())
            {
                flag = false;
            }
            else if (!txtSupplierAddr_TextCheck())
            {
                flag = false;
            }

            return flag;
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/custInfoManager/supplierManager/supplierEdit.aspx");
        }
    }
}