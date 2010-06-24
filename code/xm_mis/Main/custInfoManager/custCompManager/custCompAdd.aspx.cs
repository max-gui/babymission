using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.App_Code.logic;
namespace xm_mis.Main.custInfoManager.custCompManager
{
    public partial class custCompAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                int usrAuth = 0;
                string strUsrAuth = Session["totleAuthority"].ToString();
                usrAuth = int.Parse(strUsrAuth);
                int flag = 0x3 << 4;

                if ((usrAuth & flag) == 0)
                    Response.Redirect("~/Main/NoAuthority.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }
        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                string cn = txtCompName.Text.ToString().Trim();
                string ca = txtCompAddr.Text.ToString().Trim();
                string ct = txtCompTag.Text.ToString().Trim();

                #region dataset
                DataSet dataSet = new DataSet();
                DataRow compRow = null;

                DataColumn colCompName = new DataColumn("custCompName", System.Type.GetType("System.String"));
                DataColumn colCompAddr = new DataColumn("custCompAddress", System.Type.GetType("System.String"));
                DataColumn colCompTag = new DataColumn("custCompTag", System.Type.GetType("System.String"));

                DataTable compTable = new DataTable("tbl_customer_company");

                compTable.Columns.Add(colCompName);
                compTable.Columns.Add(colCompAddr);
                compTable.Columns.Add(colCompTag);

                compRow = compTable.NewRow();
                compRow["custCompName"] = cn;
                compRow["custCompAddress"] = ca;
                compRow["custCompTag"] = ct;
                compTable.Rows.Add(compRow);

                dataSet.Tables.Add(compTable);
                #endregion

                DataSet dsCheck = new DataSet();
                custCompProcess ccp = new custCompProcess(dsCheck);

                ccp.View();
                int rowRtn = -1;

                ccp.DoCheckCompName(cn);
                rowRtn = ccp.IntRtn;
                if (0 == rowRtn)
                {
                    ccp.DoCheckCompTag(ct);
                    rowRtn = ccp.IntRtn;

                    if (0 == rowRtn)
                    {
                        ccp.MyDst = dataSet;

                        ccp.Add();

                        string newCompId = ccp.StrRtn;

                        string continueUrl = "~/Main/DefaultMainSite.aspx";//Request.QueryString["ReturnUrl"];

                        Response.Redirect(continueUrl);
                    }
                    else
                    {
                        lblCompTag.Text = "公司简称已存在!";
                    }
                }
                else
                {
                    lblCompName.Text = "公司名已存在!";
                }
            }
        }

        protected bool txtCompName_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtCompName.Text.ToString().Trim()))
            {
                lblCompName.Text = "*必填项!";
                flag = false;
            }
            else if (txtCompName.Text.ToString().Trim().Length > 20)
            {
                lblCompName.Text = "公司名字太长!";
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblCompName.Text = string.Empty;
                //Session["flagUsrName"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }
        protected bool txtCompAddr_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtCompAddr.Text.ToString().Trim()))
            {
                lblCompAddr.Text = "*必填项!";
                flag = false;
            }
            else if (txtCompAddr.Text.ToString().Trim().Length > 20)
            {
                lblCompAddr.Text = "公司地址太长!";
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblCompAddr.Text = string.Empty;
                //Session["flagUsrName"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }
        protected bool txtCompTag_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtCompTag.Text.ToString().Trim()))
            {
                lblCompTag.Text = "*必填项!";
                flag = false;
            }
            else if (txtCompTag.Text.ToString().Trim().Length > 10)
            {
                lblCompTag.Text = "公司简称太长!";
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblCompTag.Text = string.Empty;
                //Session["flagPassWord"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }

        protected bool inputCheck()
        {
            bool flag = true;
            if (!txtCompName_TextCheck())
            {
                flag = false;
            }
            else if (!txtCompAddr_TextCheck())
            {
                flag = false;
            }
            else if (!txtCompTag_TextCheck())
            {
                flag = false;
            }

            return flag;
        }
    }
}