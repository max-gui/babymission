using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.stockInfoManager.productInfoManager
{
    public partial class productAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                int usrAuth = 0;
                string strUsrAuth = Session["totleAuthority"] as string;
                usrAuth = int.Parse(strUsrAuth);
                int flag = 0x1 << 1;

                if ((usrAuth & flag) == 0)
                    Response.Redirect("~/Main/NoAuthority.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/stockInfoManager/productInfoManager/productView.aspx");
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                string pn = txtName.Text.ToString().Trim();

                #region dataset
                DataSet dataSet = new DataSet();
                DataRow productRow = null;

                DataColumn productName = new DataColumn("productName", System.Type.GetType("System.String"));

                DataTable productTable = new DataTable("tbl_product");

                productTable.Columns.Add(productName);

                productRow = productTable.NewRow();
                productRow["productName"] = pn;
                productTable.Rows.Add(productRow);

                dataSet.Tables.Add(productTable);
                #endregion

                ProductProcess pp = new ProductProcess(dataSet);

                pp.DoCheckProductName();
                int rowRtn = pp.IntRtn;
                if (0 == rowRtn)
                {
                    using (DataTable dt =
                                pp.MyDst.Tables["tbl_product"].DefaultView.ToTable("addTable"))
                    {
                        DataRow dr = dt.NewRow();
                        dr["productName"] = pn;

                        dt.Rows.Add(dr);
                        pp.MyDst.Tables.Add(dt);

                        pp.Add();

                        Response.Redirect("~/Main/stockInfoManager/productInfoManager/productView.aspx");
                    }
                }
                else
                {
                    lblName.Text = "产品已存在!";
                }
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
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblName.Text = string.Empty;
                //Session["flagUsrName"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }

        protected bool inputCheck()
        {
            bool flag = true;
            if (!txtName_TextCheck())
            {
                flag = false;
            }

            return flag;
        }
    }
}