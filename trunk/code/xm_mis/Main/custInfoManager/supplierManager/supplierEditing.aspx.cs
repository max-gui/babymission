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
    public partial class supplierEditing : System.Web.UI.Page
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

            if (null == Session["supplierDr"])
            {
                Response.Redirect("~/Main/custInfoManager/supplierManager/supplierEdit.aspx");
            }

            if (!IsPostBack)
            {
                DataRow sessionDr = Session["supplierDr"] as DataRow;

                lblSupplierName.Text = sessionDr["supplierName"].ToString();
                lblSupplierAddr.Text = sessionDr["supplierAddress"].ToString();

                string supplierId = sessionDr["supplierId"].ToString();

                DataSet MyDst = new DataSet();
                SupplierManProcess myView = new SupplierManProcess(MyDst);
                myView.SupplierId = supplierId;

                myView.RealSupplierManView();
                DataTable taskTable = myView.MyDst.Tables["tbl_supplier_manager"];

                Session["SupplierManProcess"] = myView;
                Session["dtSources"] = taskTable;


                supplierManGV.DataSource = Session["dtSources"];
                supplierManGV.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/custInfoManager/supplierManManager/supplierManAdd.aspx");
        }

        protected void btnAcceptDel_Click(object sender, EventArgs e)
        {
            int dataIndex = supplierManGV.SelectedRow.DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];
            string manId = dt.DefaultView[dataIndex].Row["supplierManId"].ToString();

            Button btn = null;
            btn = (supplierManGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = (supplierManGV.SelectedRow.FindControl("btnCancle") as Button);
            btn.Visible = false;
            btn = (supplierManGV.SelectedRow.FindControl("btnDel") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;
            btn = btnDelCancel;
            btn.Visible = false;

            SupplierManProcess smp = Session["SupplierManProcess"] as SupplierManProcess;

            smp.SupplierManDel(manId);

            smp.RealSupplierManView();

            DataTable taskTable = smp.MyDst.Tables["tbl_supplier_manager"];

            Session["dtSources"] = smp.MyDst.Tables["tbl_supplier_manager"] as DataTable;
            supplierManGV.DataSource = Session["dtSources"];

            supplierManGV.SelectedIndex = -1;
            supplierManGV.EditIndex = -1;
            supplierManGV.DataBind();

            supplierManGV.Enabled = true;
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

            supplierManGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

            supplierManGV.SelectedIndex = -1;
            supplierManGV.EditIndex = -1;
            supplierManGV.DataBind();
            supplierManGV.Enabled = true;
        }

        protected void supplierManGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            supplierManGV.PageIndex = e.NewPageIndex;

            supplierManGV.DataSource = Session["dtSources"];
            supplierManGV.DataBind();
        }

        protected void supplierManGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (supplierManGV.SelectedIndex == -1)
            {
                e.Cancel = false;
                int index = e.NewSelectedIndex;

                supplierManGV.EditIndex = index;
                supplierManGV.DataSource = Session["dtSources"];
                supplierManGV.DataBind();

                Button btn = null;
                btn = (supplierManGV.Rows[index].FindControl("btnDel") as Button);
                btn.Visible = true;
                btn = (supplierManGV.Rows[index].FindControl("btnUpdate") as Button);
                btn.Visible = true;
                btn = (supplierManGV.Rows[index].FindControl("btnCancle") as Button);
                btn.Visible = true;
            }
            else
            {
                e.Cancel = true;
            }

            btnAdd.Visible = false;
        }

        protected void supplierManGV_Sorting(object sender, GridViewSortEventArgs e)
        {
        
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            supplierManGV.Enabled = false;

            Button btn = null;
            btn = (supplierManGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = (supplierManGV.SelectedRow.FindControl("btnCancle") as Button);
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

        protected string input_check(string testValue)
        {
            //DataColumn[] key = new DataColumn[1];
            //key[0] = dt.Columns[depItem];

            //dt.PrimaryKey = key;

            //dt.Rows.Contains(depName);

            string strRtn = string.Empty;

            if (string.IsNullOrWhiteSpace(testValue))
            {
                strRtn = "不能为空！";
            }
            else if (testValue.Length > 25)
            {
                strRtn = "不能超过25个字！";
            }
            //else if (dt.Rows.Contains(depValue))
            //{
            //    strRtn = "不能重复！";
            //}
            else if (testValue.Equals("不能为空！"))
            {
                strRtn = "不能为空！  ";
            }
            else if (testValue.Equals("不能超过25个字！"))
            {
                strRtn = "不能超过25个字！  ";
            }
            //else if (depValue.Equals("不能重复！"))
            //{
            //    strRtn = "不能重复！  ";
            //}
            else
            {
                strRtn = testValue;
            }

            return strRtn;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int index = supplierManGV.SelectedIndex;

            int dataIndex = supplierManGV.Rows[index].DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];

            GridViewRow row = supplierManGV.Rows[index];
            TextBox tbSupplierManName = row.Cells[1].Controls[0] as TextBox;
            string newSupplierManName = tbSupplierManName.Text.ToString().Trim();
            TextBox tbSupplierManCont = row.Cells[2].Controls[0] as TextBox;
            string newSupplierManCont = tbSupplierManCont.Text.ToString().Trim();
            TextBox tbSupplierManEmail = row.Cells[3].Controls[0] as TextBox;
            string newSupplierManEmail = tbSupplierManEmail.Text.ToString().Trim();
            TextBox tbSupplierManDep = row.Cells[4].Controls[0] as TextBox;
            string newSupplierManDep = tbSupplierManDep.Text.ToString().Trim();
            TextBox tbSupplierManTitle = row.Cells[5].Controls[0] as TextBox;
            string newSupplierManTitle = tbSupplierManTitle.Text.ToString().Trim();

            string strNameCheck = newSupplierManName;
            string strContCheck = newSupplierManCont;
            string strEmailCheck = newSupplierManEmail;
            string strDepCheck = newSupplierManDep;
            string strTitleCheck = newSupplierManTitle;

            newSupplierManName = input_check(strNameCheck);//, dtCheck);
            newSupplierManCont = input_check(strContCheck);
            newSupplierManEmail = input_check(strEmailCheck);
            newSupplierManDep = input_check(strDepCheck);
            newSupplierManTitle = input_check(strTitleCheck);

            bool checkFlag = true;
            checkFlag = checkFlag && newSupplierManName.Equals(strNameCheck);
            checkFlag = checkFlag && newSupplierManCont.Equals(strContCheck);
            checkFlag = checkFlag && newSupplierManEmail.Equals(strEmailCheck);
            checkFlag = checkFlag && newSupplierManDep.Equals(strDepCheck);
            checkFlag = checkFlag && newSupplierManTitle.Equals(strTitleCheck);

            if (true == checkFlag)
            {
                int supplierManId = int.Parse(dt.DefaultView[dataIndex].Row["supplierManId"].ToString());

                SupplierManProcess smp = Session["SupplierManProcess"] as SupplierManProcess;

                smp.SupplierManUpdate(supplierManId, newSupplierManName, newSupplierManCont, newSupplierManEmail, newSupplierManDep, newSupplierManTitle);

                smp.RealSupplierManView();

                DataTable taskTable = smp.MyDst.Tables["tbl_supplier_manager"];
                Session["dtSources"] = smp.MyDst.Tables["tbl_supplier_manager"] as DataTable; 

                Button btn = null;
                btn = sender as Button;
                btn.Visible = false;
                btn = (supplierManGV.SelectedRow.FindControl("btnCancle") as Button);
                btn.Visible = false;
                btn = (supplierManGV.SelectedRow.FindControl("btnDel") as Button);
                btn.Visible = false;
                btn = btnDelCancel;
                btn.Visible = false;
                btn = btnAcceptDel;
                btn.Visible = false;

                supplierManGV.SelectedIndex = -1;
                supplierManGV.EditIndex = -1;

                supplierManGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;  
                supplierManGV.DataBind();

                btnAdd.Visible = true;
            }
            else
            {
                tbSupplierManName.Text = newSupplierManName;
                tbSupplierManCont.Text = newSupplierManCont;
                tbSupplierManEmail.Text = newSupplierManEmail;
                tbSupplierManDep.Text = newSupplierManDep;
                tbSupplierManTitle.Text = newSupplierManTitle;

                supplierManGV.SelectedIndex = index;
                supplierManGV.EditIndex = index;
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            int index = supplierManGV.SelectedIndex;

            Button btn = null;
            btn = (supplierManGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;
            btn = (supplierManGV.SelectedRow.FindControl("btnDel") as Button);
            btn.Visible = false;
            btn = btnDelCancel;
            btn.Visible = false;
            btn = btnAcceptDel;
            btn.Visible = false;

            supplierManGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

            supplierManGV.SelectedIndex = -1;
            supplierManGV.EditIndex = -1;
            supplierManGV.DataBind();

            btnAdd.Visible = true;
        }
    }
}