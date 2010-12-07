using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.custInfoManager.custCompManager
{
    public partial class custCompEditing : System.Web.UI.Page
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

            if (null == Session["selCustCompDr"])
            {
                Response.Redirect("~/Main/custInfoManager/custCompManager/custCompEdit.aspx");
            }

            if (!IsPostBack)
            {
                DataRow sessionDr = Session["selCustCompDr"] as DataRow;

                lblCustCompName.Text = sessionDr["custCompName"].ToString();
                lblCustCompAddr.Text = sessionDr["custCompAddress"].ToString();
                lblCustCompTag.Text = sessionDr["custCompTag"].ToString();

                string custCompId = sessionDr["custCompyId"].ToString();

                DataSet MyDst = new DataSet();
                custManProcess myView = new custManProcess(MyDst);
                myView.CustCompId = custCompId;

                myView.RealCustManView();
                DataTable taskTable = myView.MyDst.Tables["tbl_customer_manager"];

                Session["custManProcess"] = myView;
                Session["dtSources"] = taskTable;


                custCompManGV.DataSource = Session["dtSources"];
                custCompManGV.DataBind();
            }
        }

        protected void custCompManGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            custCompManGV.PageIndex = e.NewPageIndex;

            custCompManGV.DataSource = Session["dtSources"];
            custCompManGV.DataBind();
        }

        protected void custCompManGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (custCompManGV.SelectedIndex == -1)
            {
                e.Cancel = false;
                int index = e.NewSelectedIndex;

                custCompManGV.EditIndex = index;
                custCompManGV.DataSource = Session["dtSources"];
                custCompManGV.DataBind();

                Button btn = null;
                btn = (custCompManGV.Rows[index].FindControl("btnDel") as Button);
                btn.Visible = true;
                btn = (custCompManGV.Rows[index].FindControl("btnUpdate") as Button);
                btn.Visible = true;
                btn = (custCompManGV.Rows[index].FindControl("btnCancle") as Button);
                btn.Visible = true;
            }
            else
            {
                e.Cancel = true;
            }

            btnAdd.Visible = false;
        }

        protected void custCompManGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            custCompManGV.Enabled = false;

            Button btn = null;
            btn = (custCompManGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = (custCompManGV.SelectedRow.FindControl("btnCancle") as Button);
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

        //protected string input_check(string depItem, string depValue)//, DataTable dt)
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
            else if (testValue.Length > 50)
            {
                strRtn = "不能超过50个字！";
            }
            //else if (dt.Rows.Contains(depValue))
            //{
            //    strRtn = "不能重复！";
            //}
            else if (testValue.Equals("不能为空！"))
            {
                strRtn = "不能为空！  ";
            }
            else if (testValue.Equals("不能超过50个字！"))
            {
                strRtn = "不能超过50个字！  ";
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
            int index = custCompManGV.SelectedIndex;

            int dataIndex = custCompManGV.Rows[index].DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];

            GridViewRow row = custCompManGV.Rows[index];
            TextBox tbCompManName = row.Cells[1].Controls[0] as TextBox;
            string newCompManName = tbCompManName.Text.ToString().Trim();
            TextBox tbCompManCont = row.Cells[2].Controls[0] as TextBox;
            string newCompManCont = tbCompManCont.Text.ToString().Trim();
            TextBox tbCompManEmail = row.Cells[3].Controls[0] as TextBox;
            string newCompManEmail = tbCompManEmail.Text.ToString().Trim();
            TextBox tbCompManDep = row.Cells[4].Controls[0] as TextBox;
            string newCompManDep = tbCompManDep.Text.ToString().Trim();
            TextBox tbCompManTitle = row.Cells[5].Controls[0] as TextBox;
            string newCompManTitle = tbCompManTitle.Text.ToString().Trim();

            //DataTable dtCheckTemp = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            //string strFilter =
            //    " custCompTag = " + "'" + tbCompManName + "'";
            //dtCheckTemp.DefaultView.RowFilter = strFilter;
            //DataTable dtCheck = dtCheckTemp.DefaultView.ToTable();

            string strNameCheck = newCompManName;
            string strContCheck = newCompManCont;
            string strEmailCheck = newCompManEmail;
            string strDepCheck = newCompManDep;
            string strTitleCheck = newCompManTitle;

            newCompManName = input_check(strNameCheck);//, dtCheck);
            newCompManCont = input_check(strContCheck);
            newCompManEmail = input_check(strEmailCheck);
            newCompManDep = input_check(strDepCheck);
            newCompManTitle = input_check(strTitleCheck);

            bool checkFlag = true;
            checkFlag = checkFlag && newCompManName.Equals(strNameCheck);
            checkFlag = checkFlag && newCompManCont.Equals(strContCheck);
            checkFlag = checkFlag && newCompManEmail.Equals(strEmailCheck);
            checkFlag = checkFlag && newCompManDep.Equals(strDepCheck);
            checkFlag = checkFlag && newCompManTitle.Equals(strTitleCheck);

            if (true == checkFlag)
            {
                int custManId = int.Parse(dt.DefaultView[dataIndex].Row["custManId"].ToString());

                custManProcess cmp = Session["custManProcess"] as custManProcess;

                cmp.custCompManUpdate(custManId, newCompManName, newCompManCont, newCompManEmail, newCompManDep, newCompManTitle);

                cmp.RealCustManView();

                DataTable taskTable = cmp.MyDst.Tables["tbl_customer_manager"];
                Session["dtSources"] = cmp.MyDst.Tables["tbl_customer_manager"] as DataTable;

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
                btn = (custCompManGV.SelectedRow.FindControl("btnCancle") as Button);
                btn.Visible = false;
                btn = (custCompManGV.SelectedRow.FindControl("btnDel") as Button);
                btn.Visible = false;
                btn = btnDelCancel;
                btn.Visible = false;
                btn = btnAcceptDel;
                btn.Visible = false;

                custCompManGV.SelectedIndex = -1;
                custCompManGV.EditIndex = -1;

                custCompManGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;  
                custCompManGV.DataBind();

                btnAdd.Visible = true;
            }
            else
            {
                tbCompManName.Text = newCompManName;
                tbCompManCont.Text = newCompManCont;
                tbCompManEmail.Text = newCompManEmail;
                tbCompManDep.Text = newCompManDep;
                tbCompManTitle.Text = newCompManTitle;

                custCompManGV.SelectedIndex = index;
                custCompManGV.EditIndex = index;
            }
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {            
            int index = custCompManGV.SelectedIndex;

            Button btn = null;
            btn = (custCompManGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;
            btn = (custCompManGV.SelectedRow.FindControl("btnDel") as Button);
            btn.Visible = false;
            btn = btnDelCancel;
            btn.Visible = false;
            btn = btnAcceptDel;
            btn.Visible = false;

            custCompManGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

            custCompManGV.SelectedIndex = -1;
            custCompManGV.EditIndex = -1;
            custCompManGV.DataBind();

            btnAdd.Visible = true;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/custInfoManager/custManManager/custManAdd.aspx");
        }

        protected void btnAcceptDel_Click(object sender, EventArgs e)
        {
            int dataIndex = custCompManGV.SelectedRow.DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];
            string manId = dt.DefaultView[dataIndex].Row["custManId"].ToString();

            Button btn = null;
            btn = (custCompManGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = (custCompManGV.SelectedRow.FindControl("btnCancle") as Button);
            btn.Visible = false;
            btn = (custCompManGV.SelectedRow.FindControl("btnDel") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;
            btn = btnDelCancel;
            btn.Visible = false;

            custManProcess cmp = Session["custManProcess"] as custManProcess;

            cmp.CustCompManDel(manId);

            cmp.RealCustManView();

            DataTable taskTable = cmp.MyDst.Tables["tbl_customer_manager"];

            Session["dtSources"] = cmp.MyDst.Tables["tbl_customer_manager"] as DataTable;
            custCompManGV.DataSource = Session["dtSources"];

            custCompManGV.SelectedIndex = -1;
            custCompManGV.EditIndex = -1;
            custCompManGV.DataBind();

            custCompManGV.Enabled = true;
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

            custCompManGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

            custCompManGV.SelectedIndex = -1;
            custCompManGV.EditIndex = -1;
            custCompManGV.DataBind();
            custCompManGV.Enabled = true;
        }
    }
}