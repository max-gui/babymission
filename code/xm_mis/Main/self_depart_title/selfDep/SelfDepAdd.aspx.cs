using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.App_Code.logic;
namespace xm_mis.Main.self_depart_title.selfDep
{
    public partial class SelfDepAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                int usrAuth = 0;
                string strUsrAuth = Session["totleAuthority"].ToString();
                usrAuth = int.Parse(strUsrAuth);
                int flag = 0x1 << 3;

                if ((usrAuth & flag) == 0)
                    Response.Redirect("~/Main/NoAuthority.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected string input_check(string depName)
        {
            DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();
            DataColumn[] key = new DataColumn[1];
            key[0] = dt.Columns["departmentName"];

            dt.PrimaryKey = key;

            dt.Rows.Contains(depName);

            string strRtn = string.Empty;

            if (string.IsNullOrWhiteSpace(depName))
            {
                strRtn = "部门名称不能为空！";
            }
            else if (depName.Length > 25)
            {
                strRtn = "部门名称不能超过25个字！";
            }
            else if (dt.Rows.Contains(depName))
            {
                strRtn = "部门名称不能重复！";
            }
            else if (depName.Equals("部门名称不能为空！"))
            {
                strRtn = "部门名称不能为空！  ";
            }
            else if (depName.Equals("部门名称不能超过25个字！"))
            {
                strRtn = "部门名称不能超过25个字！  ";
            }
            else if (depName.Equals("部门名称不能重复！"))
            {
                strRtn = "部门名称不能重复！  ";
            }
            else
            {
                strRtn = depName;
            }

            return strRtn;
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            SelfDepartProcess sdp = Session["SelfDepartProcess"] as SelfDepartProcess;

            string newDepName = txtDepName.Text.ToString().Trim();

            string strCheck = newDepName;

            newDepName = input_check(strCheck.Trim());
            if (newDepName.Equals(strCheck))
            {
                sdp.SelfDepAdd(newDepName);

                //sdp.SelDepView();

                //DataTable taskTable = sdp.MyDst.Tables["tbl_department"];
                ////taskTable.DefaultView.RowFilter =
                ////        "isDel = " + bool.FalseString.ToString().Trim() + " and departmentName <> '无' ";
                //Session["dtSources"] = sdp.MyDst.Tables["tbl_department"] as DataTable;

                //SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;  
                //SelfDepartGV.DataBind();

                //SelfDepartGV.Enabled = true;
                //lblDepName.Visible = false;
                //txtDepName.Text = string.Empty;
                //txtDepName.Visible = false;
                //btnAccept.Visible = false;
                //btnNo.Visible = false;

                //btnAdd.Enabled = true;
                Response.Redirect("~/Main/self_depart_title/selfDep/SelfDepartment.aspx");
            }
            else
            {
                txtDepName.Text = newDepName;
            }
        }
        protected void btnNo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/self_depart_title/selfDep/SelfDepartment.aspx");

            //SelfDepartGV.Enabled = true;
            //lblDepName.Visible = false;
            //txtDepName.Text = string.Empty;
            //txtDepName.Visible = false;
            //btnAccept.Visible = false;
            //btnNo.Visible = false;

            //btnAdd.Enabled = true;
        }
    }
}