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
    public partial class custCompEdit : System.Web.UI.Page
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

            if (!IsPostBack)
            {
                DataSet MyDst = new DataSet();
                custCompProcess myView = new custCompProcess(MyDst);

                myView.RealCompView();
                DataTable taskTable = myView.MyDst.Tables["tbl_customer_company"];

                Session["custCompProcess"] = myView;
                Session["dtSources"] = taskTable;


                custCompGV.DataSource = Session["dtSources"];
                custCompGV.DataBind();
            }
        }

        protected void btnCustManEdit_Click(object sender, EventArgs e)
        {
            int numIndex = custCompGV.SelectedRow.DataItemIndex;

            DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            DataRow dr = dt.Rows[numIndex];

            Session["selCustCompDr"] = dr;

            Response.Redirect("~/Main/custInfoManager/custCompManager/custCompEditing.aspx");
        }

        protected void custCompGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            custCompGV.PageIndex = e.NewPageIndex;

            custCompGV.DataSource = Session["dtSources"];
            custCompGV.DataBind();
        }

        protected void custCompGV_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            if (custCompGV.SelectedIndex == -1)
            {
                e.Cancel = false;
                int index = e.NewSelectedIndex;

                custCompGV.EditIndex = index;
                custCompGV.DataSource = Session["dtSources"];
                custCompGV.DataBind();

                Button btn = null;
                btn = (custCompGV.Rows[index].FindControl("btnDel") as Button);
                btn.Visible = true;
                btn = (custCompGV.Rows[index].FindControl("btnUpdate") as Button);
                btn.Visible = true;
                btn = (custCompGV.Rows[index].FindControl("btnCancle") as Button);
                btn.Visible = true;
                btn = (custCompGV.Rows[index].FindControl("btnCustManEdit") as Button);
                btn.Visible = true;
            }
            else
            {
                e.Cancel = true;
            }

            btnAdd.Visible = false;
        }

        protected void custCompGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/custInfoManager/custCompManager/custCompAdd.aspx");
        }

        protected void btnDel_Click(object sender, EventArgs e)
        {
            //int dataIndex = custCompGV.SelectedRow.DataItemIndex;
            //DataTable dt = (DataTable)Session["dtSources"];
            //int depId = int.Parse(dt.DefaultView[dataIndex].Row["custCompyId"].ToString());

            //Button btn = null;
            //btn = (custCompGV.SelectedRow.FindControl("btnUpdate") as Button);
            //btn.Visible = false;
            //btn = (custCompGV.SelectedRow.FindControl("btnCancle") as Button);
            //btn.Visible = false;
            //btn = (custCompGV.SelectedRow.FindControl("btnCustManEdit") as Button);
            //btn.Visible = false;
            //btn = sender as Button;
            //btn.Visible = false;

            //custCompProcess ccp = Session["custCompProcess"] as custCompProcess;

            //*****************
            //ccp.SelfDepDel(depId);

            //ccp.SelDepView();
            //*****************

            //DataTable taskTable = ccp.MyDst.Tables["tbl_department"];

            //Session["dtSources"] = ccp.MyDst.Tables["tbl_department"] as DataTable;
            //custCompGV.DataSource = Session["dtSources"];

            //custCompGV.SelectedIndex = -1;
            //custCompGV.EditIndex = -1;
            //custCompGV.DataBind();

            //btnAdd.Enabled = true;

            custCompGV.Enabled = false;

            Button btn = null;
            btn = (custCompGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = (custCompGV.SelectedRow.FindControl("btnCancle") as Button);
            btn.Visible = false;
            btn = (custCompGV.SelectedRow.FindControl("btnCustManEdit") as Button);
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

        protected void btnDelCancel_Click(object sender, EventArgs e)
        {
            Button btn = null;
            btn = btnAcceptDel;
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;
            btn = btnAdd;
            btn.Visible = true;

            custCompGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

            custCompGV.SelectedIndex = -1;
            custCompGV.EditIndex = -1;
            custCompGV.DataBind();
            custCompGV.Enabled = true;
        }

        protected void btnAcceptDel_Click(object sender, EventArgs e)
        {
            int dataIndex = custCompGV.SelectedRow.DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];
            string depId = dt.DefaultView[dataIndex].Row["custCompyId"].ToString();

            Button btn = null;
            btn = (custCompGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = (custCompGV.SelectedRow.FindControl("btnCancle") as Button);
            btn.Visible = false;
            btn = (custCompGV.SelectedRow.FindControl("btnCustManEdit") as Button);
            btn.Visible = false;
            btn = (custCompGV.SelectedRow.FindControl("btnDel") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;
            btn = btnDelCancel;
            btn.Visible = false;

            custCompProcess ccp = Session["custCompProcess"] as custCompProcess;

            ccp.SelfCustCompDel(depId);

            ccp.RealCompView();

            DataTable taskTable = ccp.MyDst.Tables["tbl_customer_company"];

            Session["dtSources"] = ccp.MyDst.Tables["tbl_customer_company"] as DataTable;
            custCompGV.DataSource = Session["dtSources"];

            custCompGV.SelectedIndex = -1;
            custCompGV.EditIndex = -1;
            custCompGV.DataBind();

            custCompGV.Enabled = true;
            btnAdd.Visible = true;

            //int index = custCompGV.SelectedIndex;
            //int itemIdex = custCompGV.Rows[index].DataItemIndex;

            //DataTable dt = (Session["upDtSources"] as DataTable).DefaultView.ToTable();
            //string usrId = dt.Rows[itemIdex]["usrId"].ToString();

            //UserProcess up = Session["UserProcess"] as UserProcess;

            //up.usrDel(usrId);

            //up.UsrSelfDepartTitleView();
            //DataTable upTable = up.MyDst.Tables["view_usr_department_title"];
            //Session["upDtSources"] = upTable;

            //custCompGV.DataSource = Session["upDtSources"];
            //custCompGV.DataBind();

            //Button btn = sender as Button;
            //btn.Visible = false;
            //btn = btnDelCancel;
            //btn.Visible = false;
            //btn = btnAdd;
            //btn.Visible = true;

            //custCompGV.SelectedIndex = -1;
            //custCompGV.Enabled = true;
        }

        protected void btnCancle_Click(object sender, EventArgs e)
        {
            int index = custCompGV.SelectedIndex;

            Button btn = null;
            btn = (custCompGV.SelectedRow.FindControl("btnUpdate") as Button);
            btn.Visible = false;
            btn = sender as Button;
            btn.Visible = false;
            btn = (custCompGV.SelectedRow.FindControl("btnCustManEdit") as Button);
            btn.Visible = false;
            btn = (custCompGV.SelectedRow.FindControl("btnDel") as Button);
            btn.Visible = false;
            btn = btnDelCancel;
            btn.Visible = false;
            btn = btnAcceptDel;
            btn.Visible = false;

            custCompGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;

            custCompGV.SelectedIndex = -1;
            custCompGV.EditIndex = -1;
            custCompGV.DataBind();

            btnAdd.Visible = true;
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
            else if (depValue.Length > 50)
            {
                strRtn = "不能超过50个字！";
            }
            else if (dt.Rows.Contains(depValue))
            {
                strRtn = "不能重复！";
            }
            else if (depValue.Equals("不能为空！"))
            {
                strRtn = "不能为空！  ";
            }
            else if (depValue.Equals("不能超过50个字！"))
            {
                strRtn = "不能超过50个字！  ";
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
            int index = custCompGV.SelectedIndex;

            int dataIndex = custCompGV.Rows[index].DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];

            GridViewRow row = custCompGV.Rows[index];
            TextBox tbCompName = row.Cells[1].Controls[0] as TextBox;
            string newCompName = tbCompName.Text.ToString().Trim();
            TextBox tbCompAddr = row.Cells[2].Controls[0] as TextBox;
            string newCompAddr = tbCompAddr.Text.ToString().Trim();
            TextBox tbCompTag = row.Cells[3].Controls[0] as TextBox;
            string newCompTag = tbCompTag.Text.ToString().Trim();

            DataTable dtCheckTemp = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            string custCompIdTemp = dt.DefaultView[dataIndex].Row["custCompyId"].ToString();
            string strFilter =
                " custCompyId <> " + "'" + custCompIdTemp + "'";
            dtCheckTemp.DefaultView.RowFilter = strFilter;
            DataTable dtCheck = dtCheckTemp.DefaultView.ToTable();

            string strNameCheck = newCompName;
            string strAddrCheck = newCompAddr;
            string strTagCheck = newCompTag;

            newCompName = input_check("custCompName", strNameCheck, dtCheck);
            newCompAddr = input_check("custCompAddress", strAddrCheck, dtCheck);
            newCompTag = input_check("custCompTag", strTagCheck, dtCheck);

            if (newCompName.Equals(strNameCheck) && newCompAddr.Equals(strAddrCheck) && newCompTag.Equals(strTagCheck))
            {
                int custCompId = int.Parse(dt.DefaultView[dataIndex].Row["custCompyId"].ToString());

                custCompProcess ccp = Session["custCompProcess"] as custCompProcess;

                ccp.custCompUpdate(custCompId, newCompName, newCompAddr, newCompTag);

                ccp.RealCompView();

                DataTable taskTable = ccp.MyDst.Tables["tbl_customer_company"];
                Session["dtSources"] = ccp.MyDst.Tables["tbl_customer_company"] as DataTable;

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
                btn = (custCompGV.SelectedRow.FindControl("btnCancle") as Button); 
                btn.Visible = false;
                btn = (custCompGV.SelectedRow.FindControl("btnCustManEdit") as Button);
                btn.Visible = false;
                btn = (custCompGV.SelectedRow.FindControl("btnDel") as Button);
                btn.Visible = false;
                btn = btnDelCancel;
                btn.Visible = false;
                btn = btnAcceptDel;
                btn.Visible = false;

                custCompGV.SelectedIndex = -1;
                custCompGV.EditIndex = -1;

                custCompGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;  
                custCompGV.DataBind();

                btnAdd.Visible = true;
            }
            else
            {
                tbCompName.Text = newCompName;
                tbCompAddr.Text = newCompAddr;
                tbCompTag.Text = newCompTag;

                custCompGV.SelectedIndex = index;
                custCompGV.EditIndex = index;
            }
        }
    }
}