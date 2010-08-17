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
    public partial class supplierEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                int usrAuth = 0;
                string strUsrAuth = Session["totleAuthority"] as string;
                usrAuth = int.Parse(strUsrAuth);
                int flag = 0x3 << 4;

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
                SupplierProcess myView = new SupplierProcess(MyDst);

                myView.RealSupplierView();
                DataTable taskTable = myView.MyDst.Tables["tbl_supplier_company"];

                Session["SupplierProcess"] = myView;
                Session["dtSources"] = taskTable;


                supplierGV.DataSource = Session["dtSources"];
                supplierGV.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/custInfoManager/supplierManager/supplierAdd.aspx");
        }

        protected void btnAcceptDel_Click(object sender, EventArgs e)
        {
            int dataIndex = supplierGV.SelectedRow.DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];
            string depId = dt.DefaultView[dataIndex].Row["supplierId"].ToString();

            Button btn = null;
            btn = (supplierGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = (supplierGV.SelectedRow.FindControl("btnCancle") as Button);
            btn.Visible = false;
            btn = (supplierGV.SelectedRow.FindControl("btnSupplierManEdit") as Button);
            btn.Visible = false;
            btn = (supplierGV.SelectedRow.FindControl("btnDel") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;
            btn = btnDelCancel;
            btn.Visible = false;

            SupplierProcess sp = Session["SupplierProcess"] as SupplierProcess;

            sp.SupplierDel(depId);

            sp.RealSupplierView();

            DataTable taskTable = sp.MyDst.Tables["tbl_supplier_company"];

            Session["dtSources"] = sp.MyDst.Tables["tbl_supplier_company"] as DataTable;
            supplierGV.DataSource = Session["dtSources"];

            supplierGV.SelectedIndex = -1;
            supplierGV.EditIndex = -1;
            supplierGV.DataBind();

            supplierGV.Enabled = true;
            btnAdd.Visible = true;
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

            supplierGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

            supplierGV.SelectedIndex = -1;
            supplierGV.EditIndex = -1;
            supplierGV.DataBind();
            supplierGV.Enabled = true;
        }

        protected void supplierGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            supplierGV.PageIndex = e.NewPageIndex;

            supplierGV.DataSource = Session["dtSources"];
            supplierGV.DataBind();
        }

        protected void supplierGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (supplierGV.SelectedIndex == -1)
            {
                e.Cancel = false;
                int index = e.NewSelectedIndex;

                supplierGV.EditIndex = index;
                supplierGV.DataSource = Session["dtSources"];
                supplierGV.DataBind();

                Button btn = null;
                btn = (supplierGV.Rows[index].FindControl("btnDel") as Button);
                btn.Visible = true;
                btn = (supplierGV.Rows[index].FindControl("btnUpdate") as Button);
                btn.Visible = true;
                btn = (supplierGV.Rows[index].FindControl("btnCancle") as Button);
                btn.Visible = true;
                btn = (supplierGV.Rows[index].FindControl("btnSupplierManEdit") as Button);
                btn.Visible = true;
            }
            else
            {
                e.Cancel = true;
            }

            btnAdd.Visible = false;
        }

        protected void supplierGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            supplierGV.Enabled = false;

            Button btn = null;
            btn = (supplierGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = (supplierGV.SelectedRow.FindControl("btnCancle") as Button);
            btn.Visible = false;
            btn = (supplierGV.SelectedRow.FindControl("btnSupplierManEdit") as Button);
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int index = supplierGV.SelectedIndex;

            int dataIndex = supplierGV.Rows[index].DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];

            GridViewRow row = supplierGV.Rows[index];
            TextBox tbSupplierName = row.Cells[1].Controls[0] as TextBox;
            string newSupplierName = tbSupplierName.Text.ToString().Trim();
            TextBox tbSupplierAddr = row.Cells[2].Controls[0] as TextBox;
            string newSupplierAddr = tbSupplierAddr.Text.ToString().Trim();

            DataTable dtCheckTemp = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            string supplierIdTemp = dt.DefaultView[dataIndex].Row["supplierId"].ToString();
            string strFilter =
                " supplierId <> " + "'" + supplierIdTemp + "'";
            dtCheckTemp.DefaultView.RowFilter = strFilter;
            DataTable dtCheck = dtCheckTemp.DefaultView.ToTable();

            string strNameCheck = newSupplierName;
            string strAddrCheck = newSupplierAddr;

            newSupplierName = input_check("custCompName", strNameCheck, dtCheck);
            newSupplierAddr = input_check("custCompAddress", strAddrCheck, dtCheck);

            if (newSupplierName.Equals(strNameCheck) && newSupplierAddr.Equals(strAddrCheck))
            {
                int supplierId = int.Parse(dt.DefaultView[dataIndex].Row["supplierId"].ToString());

                SupplierProcess sp = Session["SupplierProcess"] as SupplierProcess;

                sp.SupplierUpdate(supplierId, newSupplierName, newSupplierAddr);

                sp.RealSupplierView();

                DataTable taskTable = sp.MyDst.Tables["tbl_supplier_company"];
                Session["dtSources"] = sp.MyDst.Tables["tbl_supplier_company"] as DataTable;

                //Button btn = null;
                //btn = (custCompGV.Rows[index].FindControl("btnDel") as Button);
                //btn.Visible = false;
                //btn = (custCompGV.Rows[index].FindControl("btnCancle") as Button);
                //btn.Visible = false;
                //btn = sender as Button;
                //btn.Visible = false;

                Button btn = null;
                btn = sender as Button;
                btn.Visible = false;
                btn = (supplierGV.SelectedRow.FindControl("btnCancle") as Button);
                btn.Visible = false;
                btn = (supplierGV.SelectedRow.FindControl("btnSupplierManEdit") as Button);
                btn.Visible = false;
                btn = (supplierGV.SelectedRow.FindControl("btnDel") as Button);
                btn.Visible = false;
                btn = btnDelCancel;
                btn.Visible = false;
                btn = btnAcceptDel;
                btn.Visible = false;

                supplierGV.SelectedIndex = -1;
                supplierGV.EditIndex = -1;

                supplierGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;  
                supplierGV.DataBind();

                btnAdd.Visible = true;
            }
            else
            {
                tbSupplierName.Text = newSupplierName;
                tbSupplierAddr.Text = newSupplierAddr;

                supplierGV.SelectedIndex = index;
                supplierGV.EditIndex = index;
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            int index = supplierGV.SelectedIndex;

            Button btn = null;
            btn = (supplierGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;
            btn = (supplierGV.SelectedRow.FindControl("btnSupplierManEdit") as Button);
            btn.Visible = false;
            btn = (supplierGV.SelectedRow.FindControl("btnDel") as Button);
            btn.Visible = false;
            btn = btnDelCancel;
            btn.Visible = false;
            btn = btnAcceptDel;
            btn.Visible = false;

            supplierGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

            supplierGV.SelectedIndex = -1;
            supplierGV.EditIndex = -1;
            supplierGV.DataBind();

            btnAdd.Visible = true;
        }

        protected void btnSupplierManEdit_Click(object sender, EventArgs e)
        {
            int numIndex = supplierGV.SelectedRow.DataItemIndex;

            DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            DataRow dr = dt.Rows[numIndex];

            Session["supplierDr"] = dr;

            Response.Redirect("~/Main/custInfoManager/supplierManager/supplierEditing.aspx");
        }
    }
}