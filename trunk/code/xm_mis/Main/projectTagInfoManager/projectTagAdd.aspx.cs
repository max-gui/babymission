using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.projectTagInfoManager
{
    public partial class projectTagAdd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.projectTagApply);
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
                DataSet custCompDst = new DataSet();
                DataSet custManDst = new DataSet();
                DataSet projTagDst = new DataSet();
                
                custCompProcess ddlCustCompView = new custCompProcess(custCompDst);
                custManProcess ddlCustManView = new custManProcess(custManDst);
                ddlCustManView.CustCompId = "-1".ToString();
                ProjectTagProcess ptView = new ProjectTagProcess(projTagDst);

                ddlCustCompView.RealCompView(); 
                ddlCustManView.RealCustManView();
                DataTable ddlCustCompTable = ddlCustCompView.MyDst.Tables["tbl_customer_company"];
                DataTable ddlCustManTable = ddlCustManView.MyDst.Tables["tbl_customer_manager"];
                
                DataColumn[] custCompKey = new DataColumn[1];
                custCompKey[0] = ddlCustCompTable.Columns["custCompyId"];
                ddlCustCompTable.PrimaryKey = custCompKey;
                DataColumn[] custManpKey = new DataColumn[1];
                custManpKey[0] = ddlCustManTable.Columns["custManId"];
                ddlCustManTable.PrimaryKey = custManpKey;

                Session["custCompProcess"] = ddlCustCompView;
                Session["ddlCustCompDtS"] = ddlCustCompTable;
                Session["custManProcess"] = ddlCustManView;
                Session["ddlCustManDtS"] = ddlCustManTable;
                Session["ProjectTagProcess"] = ptView;

                DataRow dr = ddlCustCompTable.NewRow();
                dr["custCompyId"] = -1;
                dr["custCompName"] = string.Empty;
                dr["endTime"] = "9999-12-31";
                ddlCustCompTable.Rows.Add(dr);
                //ddlCustCompTable.AcceptChanges();

                ddlCustComp.DataValueField = "custCompyId";
                ddlCustComp.DataTextField = "custCompName";
                ddlCustComp.DataSource = ddlCustCompTable;
                ddlCustComp.DataBind();
                ddlCustComp.SelectedValue = "-1";
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                string usrId = Session["usrId"].ToString();
                string projSynopsis = txtSynopsis.Text.ToString();
                string custManId = ddlCustMan.SelectedValue.ToString();
                string projectTag = tag_init();

                #region dataset
                DataSet dataSet = new DataSet();
                DataRow projRow = null;

                DataColumn colUsrId = new DataColumn("usrId", System.Type.GetType("System.String"));
                DataColumn colSynopsis = new DataColumn("projectSynopsis", System.Type.GetType("System.String"));
                DataColumn colManId = new DataColumn("custManId", System.Type.GetType("System.String"));
                DataColumn colProjTag = new DataColumn("projectTag", System.Type.GetType("System.String"));

                DataTable projTable = new DataTable("tbl_projectTagInfo");

                projTable.Columns.Add(colUsrId);
                projTable.Columns.Add(colSynopsis);
                projTable.Columns.Add(colManId);
                projTable.Columns.Add(colProjTag);
                
                projRow = projTable.NewRow();
                projRow["usrId"] = usrId;
                projRow["projectSynopsis"] = projSynopsis;
                projRow["custManId"] = custManId;
                projRow["projectTag"] = projectTag;
                projTable.Rows.Add(projRow);

                dataSet.Tables.Add(projTable);
                #endregion

                ProjectTagProcess ptp = Session["ProjectTagProcess"] as ProjectTagProcess;
                ptp.MyDst = dataSet;
                
                ptp.Add();
                Response.Redirect("~/Main/projectTagInfoManager/projectSearch.aspx");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/projectTagInfoManager/projectSearch.aspx");
        }

        protected void ddlCustComp_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable custManDt = Session["ddlCustManDtS"] as DataTable;

            string strFilter =
                 " custCompyId = " + "'" + ddlCustComp.SelectedValue.ToString() + "'";
            custManDt.DefaultView.RowFilter = strFilter;
            custManDt = custManDt.DefaultView.ToTable();

            DataRow newDr = custManDt.NewRow();
            newDr["custManId"] = -1;
            newDr["custManName"] = "";
            custManDt.Rows.Add(newDr);

            ddlCustMan.DataValueField = "custManId";
            ddlCustMan.DataTextField = "custManName";
            ddlCustMan.DataSource = custManDt;
            ddlCustMan.SelectedValue = "-1";
            ddlCustMan.DataBind();

            DataTable custCompDt = Session["ddlCustCompDtS"] as DataTable;
            DataRow dr = custCompDt.Rows.Find(ddlCustComp.SelectedValue.ToString());
            lblCustCompAddr.Text = dr["custCompAddress"].ToString();
            lblCustCompAddr.Visible = true;
            
        }

        protected string tag_init()
        {            
            DateTime dtDate = new DateTime(DateTime.Now.Year, 1, 1);

            string custCompId = ddlCustComp.SelectedValue.ToString();
            DataTable custCompDt = Session["ddlCustCompDtS"] as DataTable;
            DataRow custCompdr = custCompDt.Rows.Find(custCompId);
            string custCompTag = custCompdr["custCompTag"].ToString();

            //string custManId = ddlCustMan.SelectedValue.ToString();

            ProjectTagProcess ptp = Session["ProjectTagProcess"] as ProjectTagProcess;
            string custCompProCount = ptp.compProjectCount(custCompId);

            string splitTemp = "-";
            string newTag = custCompTag + dtDate.Year.ToString() + splitTemp + custCompProCount;

            return newTag;
        }

        protected void ddlCustMan_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DataTable custManDt = Session["ddlCustManDtS"] as DataTable;

            //string strFilter =
            //     " custCompyId = " + "'" + ddlCustComp.SelectedValue.ToString() + "'";
            //custManDt.DefaultView.RowFilter = strFilter;
            //custManDt = custManDt.DefaultView.ToTable();


            //ddlCustMan.DataValueField = "custManId";
            //ddlCustMan.DataTextField = "custManName";
            //ddlCustMan.DataSource = custManDt;
            //ddlCustMan.DataBind();
            
            DataTable custManDt = Session["ddlCustManDtS"] as DataTable;
            DataRow dr = custManDt.Rows.Find(ddlCustMan.SelectedValue.ToString());
            lblCustManCont.Text = dr["custManContact"].ToString();
            lblCustManCont.Visible = true;
            lblCustManEmail.Text = dr["custManEmail"].ToString();
            lblCustManEmail.Visible = true;
        }

        protected bool txtSynopsis_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtSynopsis.Text.ToString().Trim()))
            {
                txtSynopsis.Text = "不能为空！";
                flag = false;
            }
            else if (txtSynopsis.Text.ToString().Trim().Equals("不能为空！"))
            {
                txtSynopsis.Text = "不能为空！   ";
                flag = false;
            }
            else
            {
            }

            return flag;
        }
        
        protected bool inputCheck()
        {
            bool flag = true;
            if (!txtSynopsis_TextCheck())
            {
                flag = false;
            }
            else if (ddlCustComp.SelectedValue.ToString().Equals(-1))
            {
                flag = false;
            }
            else if (ddlCustMan.SelectedValue.ToString().Equals(-1))
            {
                flag = false;
            }

            return flag;
        }
    }
}