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
    public partial class productView : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                DataSet MyDst = new DataSet();
                ProductProcess myView = new ProductProcess(MyDst);

                myView.RealProductView();
                DataTable taskTable = myView.MyDst.Tables["tbl_product"];

                Session["ProductProcess"] = myView;
                Session["dtSources"] = taskTable;

                productGV.DataSource = Session["dtSources"];
                productGV.DataBind();
            }
        }

        protected string input_check(string depItem, string depValue, DataTable dt)
        {


            DataColumn[] key = new DataColumn[1];
            key[0] = dt.Columns[depItem];

            dt.PrimaryKey = key;

            //dt.Rows.Contains(depName);

            string strRtn = string.Empty;

            if (string.IsNullOrWhiteSpace(depValue))
            {
                strRtn = "不能为空！";
            }
            else if (depValue.Length > 25)
            {
                strRtn = "不能超过25个字！";
            }
            else if (dt.Rows.Contains(depValue))
            {
                strRtn = "不能重复！";
            }
            else if (depValue.Equals("不能为空！"))
            {
                strRtn = "不能为空！  ";
            }
            else if (depValue.Equals("不能超过25个字！"))
            {
                strRtn = "不能超过25个字！  ";
            }
            else if (depValue.Equals("不能重复！"))
            {
                strRtn = "不能重复！  ";
            }
            else
            {
                strRtn = depValue;
            }

            return strRtn;
        }

        protected void btnDelCancel_Click(object sender, EventArgs e)
        {
            Button btn = null;
            btn = btnAcceptDel;
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;
            btn = btnAdd;
            btn.Visible = true;

            productGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

            productGV.SelectedIndex = -1;
            productGV.EditIndex = -1;
            productGV.DataBind();

            productGV.Enabled = true;
        }

        protected void btnAcceptDel_Click(object sender, EventArgs e)
        {
            int dataIndex = productGV.SelectedRow.DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];
            string productId = dt.DefaultView[dataIndex].Row["productId"].ToString();

            Button btn = null;
            btn = (productGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = (productGV.SelectedRow.FindControl("btnCancle") as Button);
            btn.Visible = false;
            btn = (productGV.SelectedRow.FindControl("btnDel") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;
            btn = btnDelCancel;
            btn.Visible = false;

            ProductProcess pp = Session["ProductProcess"] as ProductProcess;

            pp.ProductDel(productId);

            pp.RealProductView();

            DataTable taskTable = pp.MyDst.Tables["tbl_product"];

            Session["dtSources"] = pp.MyDst.Tables["tbl_product"] as DataTable;
            productGV.DataSource = Session["dtSources"];

            productGV.SelectedIndex = -1;
            productGV.EditIndex = -1;
            productGV.DataBind();

            productGV.Enabled = true;
            btnAdd.Visible = true;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/stockInfoManager/productInfoManager/productAdd.aspx");
        }

        protected void productGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            productGV.PageIndex = e.NewPageIndex;

            productGV.DataSource = Session["dtSources"];
            productGV.DataBind();
        }

        protected void productGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (productGV.SelectedIndex == -1)
            {
                e.Cancel = false;
                int index = e.NewSelectedIndex;

                productGV.EditIndex = index;
                productGV.DataSource = Session["dtSources"];
                productGV.DataBind();

                Button btn = null;
                btn = (productGV.Rows[index].FindControl("btnDel") as Button);
                btn.Visible = true;
                btn = (productGV.Rows[index].FindControl("btnUpdate") as Button);
                btn.Visible = true;
                btn = (productGV.Rows[index].FindControl("btnCancle") as Button);
                btn.Visible = true;

                //TextBox tb = null;
                //tb = (productGV.Rows[index].Cells[2].Controls[0] as TextBox);
                //tb.Enabled = false;
            }
            else
            {
                e.Cancel = true;
            }

            btnAdd.Visible = false;
        }

        protected void productGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            productGV.Enabled = false;

            Button btn = null;
            btn = (productGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = (productGV.SelectedRow.FindControl("btnCancle") as Button);
            btn.Visible = false;
            btn = (productGV.SelectedRow.FindControl("btnCustManEdit") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;

            btn = btnAcceptDel;
            btn.Visible = true;
            btn = btnDelCancel;
            btn.Visible = true;
            btn = btnAdd;
            btn.Visible = false;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int index = productGV.SelectedIndex;

            int dataIndex = productGV.Rows[index].DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];

            GridViewRow row = productGV.Rows[index];
            TextBox tbProductName = row.Cells[1].Controls[0] as TextBox;
            string newProductName = tbProductName.Text.ToString().Trim();
            
            DataTable dtCheckTemp = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            string productIdTemp = dt.DefaultView[dataIndex].Row["productId"].ToString();
            string strFilter =
                " productId <> " + "'" + productIdTemp + "'";
            dtCheckTemp.DefaultView.RowFilter = strFilter;
            
            DataTable dtCheck = dtCheckTemp.DefaultView.ToTable();

            string strNameCheck = newProductName;

            newProductName = input_check("productName", strNameCheck, dtCheck);

            if (newProductName.Equals(strNameCheck))
            {
                int productId = int.Parse(dt.DefaultView[dataIndex].Row["productId"].ToString());

                ProductProcess pp = Session["ProductProcess"] as ProductProcess;

                pp.ProductUpdate(productId, newProductName);

                pp.RealProductView();

                DataTable taskTable = pp.MyDst.Tables["tbl_product"];
                Session["dtSources"] = taskTable as DataTable;
                                
                Button btn = null;
                btn = sender as Button;
                btn.Visible = false;
                btn = (productGV.SelectedRow.FindControl("btnCancle") as Button);
                btn.Visible = false;
                btn = (productGV.SelectedRow.FindControl("btnDel") as Button);
                btn.Visible = false;
                btn = btnDelCancel;
                btn.Visible = false;
                btn = btnAcceptDel;
                btn.Visible = false;

                productGV.SelectedIndex = -1;
                productGV.EditIndex = -1;

                productGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;  
                productGV.DataBind();

                btnAdd.Visible = true;
            }
            else
            {
                tbProductName.Text = newProductName;
                
                productGV.SelectedIndex = index;
                productGV.EditIndex = index;
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            int index = productGV.SelectedIndex;

            Button btn = null;
            btn = (productGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;
            btn = (productGV.SelectedRow.FindControl("btnDel") as Button);
            btn.Visible = false;
            btn = btnDelCancel;
            btn.Visible = false;
            btn = btnAcceptDel;
            btn.Visible = false;

            productGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

            productGV.SelectedIndex = -1;
            productGV.EditIndex = -1;
            productGV.DataBind();

            btnAdd.Visible = true;
        }
    }
}