using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.custInfoManager.supplierManManager
{
    public partial class supplierManAdd : System.Web.UI.Page
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

            if (null == Session["supplierDr"])
            {
                Response.Redirect("~/Main/custInfoManager/supplierManager/supplierEditing.aspx");
            }

            if (!IsPostBack)
            {
                DataRow sessionDr = Session["supplierDr"] as DataRow;

                lblSupplier.Text = sessionDr["supplierName"].ToString();
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                DataRow sessionDr = Session["supplierDr"] as DataRow;
                string supplierId = sessionDr["supplierId"].ToString().Trim();

                string smn = txtName.Text.ToString().Trim();
                string smd = txtDep.Text.ToString().Trim();
                string smt = txtTitle.Text.ToString().Trim();
                string smc = txtContact.Text.ToString().Trim();
                string sme = txtEmail.Text.ToString().Trim();
                string sId = supplierId;

                #region dataset
                DataSet dataSet = new DataSet();
                DataRow supplierManRow = null;

                DataColumn colSupplierManName = new DataColumn("supplierManName", System.Type.GetType("System.String"));
                DataColumn colSupplierManDep = new DataColumn("supplierManDepart", System.Type.GetType("System.String"));
                DataColumn colSupplierManTitle = new DataColumn("supplierManTitle", System.Type.GetType("System.String"));
                DataColumn colSupplierManContact = new DataColumn("supplierManContact", System.Type.GetType("System.String"));
                DataColumn colSupplierManEmail = new DataColumn("supplierManEmail", System.Type.GetType("System.String"));
                DataColumn colSupplierId = new DataColumn("supplierId", System.Type.GetType("System.String"));

                DataTable supplierManTable = new DataTable("tbl_supplier_manager");

                supplierManTable.Columns.Add(colSupplierManName);
                supplierManTable.Columns.Add(colSupplierManDep);
                supplierManTable.Columns.Add(colSupplierManTitle);
                supplierManTable.Columns.Add(colSupplierManContact);
                supplierManTable.Columns.Add(colSupplierManEmail);
                supplierManTable.Columns.Add(colSupplierId);

                supplierManRow = supplierManTable.NewRow();
                supplierManRow["supplierManName"] = smn;
                supplierManRow["supplierManDepart"] = smd;
                supplierManRow["supplierManTitle"] = smt;
                supplierManRow["supplierManContact"] = smc;
                supplierManRow["supplierManEmail"] = sme;
                supplierManRow["supplierId"] = sId;
                supplierManTable.Rows.Add(supplierManRow);

                dataSet.Tables.Add(supplierManTable);
                #endregion

                SupplierManProcess smp = new SupplierManProcess(dataSet);

                smp.Add();

                string continueUrl = "~/Main/custInfoManager/supplierManager/supplierEditing.aspx";

                Response.Redirect(continueUrl);
            }
        }

        protected bool txtName_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtName.Text.ToString().Trim()))
            {
                lblName.Text = "*必填项!";
                flag = false;
            }
            else if (txtName.Text.ToString().Trim().Length > 20)
            {
                lblName.Text = "名字太长!";
                flag = false;
            }
            else
            {
                lblName.Text = string.Empty;
            }

            return flag;
        }

        protected bool txtDep_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtDep.Text.ToString().Trim()))
            {
                lblDep.Text = "*必填项!";
                flag = false;
            }
            else if (txtDep.Text.ToString().Trim().Length > 20)
            {
                lblDep.Text = "部门名称太长!";
                flag = false;
            }
            else
            {
                lblDep.Text = string.Empty;
            }

            return flag;
        }
        protected bool txtTitle_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtTitle.Text.ToString().Trim()))
            {
                lblTitle.Text = "*必填项!";
                flag = false;
            }
            else if (txtTitle.Text.ToString().Trim().Length > 10)
            {
                lblTitle.Text = "职位名称太长!";
                flag = false;
            }
            else
            {
                lblTitle.Text = string.Empty;
            }

            return flag;
        }
        //protected bool txtContact_TextCheck()
        //{
        //    bool flag = true;
        //    if (string.IsNullOrWhiteSpace(txtContact.Text.ToString().Trim()))
        //    {
        //        lblContact.Text = "*必填项!";
        //        flag = false;
        //    }
        //    else if (txtContact.Text.ToString().Trim().Length != 11)
        //    {
        //        lblContact.Text = "手机号码应为11位!";
        //        flag = false;
        //    }
        //    else
        //    {
        //        long sc = 0;
        //        try
        //        {
        //            sc = long.Parse(txtContact.Text.ToString().Trim());
        //            lblContact.Text = string.Empty;
        //        }
        //        catch (FormatException e)
        //        {
        //            lblContact.Text = "手机号码只能包含数字!";
        //            Console.WriteLine("{0} Exception caught.", e);
        //            flag = false;
        //        }
        //    }

        //    return flag;
        //}
        //protected bool txtEmail_TextCheck()
        //{
        //    string strLblEmail = txtEmail.Text.ToString().Trim();

        //    bool flag = true;
        //    if (string.IsNullOrWhiteSpace(strLblEmail))
        //    {
        //        lblEmail.Text = "*必填项!";
        //        flag = false;
        //    }
        //    else if (!strLblEmail.Contains("@") || strLblEmail.StartsWith("@") || strLblEmail.EndsWith("@"))
        //    {
        //        lblEmail.Text = "邮件格式不对!";
        //        flag = false;
        //    }
        //    else
        //    {
        //        lblEmail.Text = string.Empty;
        //    }

        //    return flag;
        //}

        protected bool inputCheck()
        {
            bool flag = true;
            if (!txtName_TextCheck())
            {
                flag = false;
            }
            else if (!txtDep_TextCheck())
            {
                flag = false;
            }
            else if (!txtTitle_TextCheck())
            {
                flag = false;
            }
            //else if (!txtContact_TextCheck())
            //{
            //    flag = false;
            //}
            //else if (!txtEmail_TextCheck())
            //{
            //    flag = false;
            //}

            return flag;
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/custInfoManager/supplierManager/supplierEditing.aspx");
        }
    }
}