using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.contractManager
{
    public partial class mainContractEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.newContract);
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
                #region ddlCustComp

                DataSet dst = new DataSet();

                custCompProcess ddlCustCompView = new custCompProcess(dst);

                ddlCustCompView.RealCompView();
                DataTable ddlCustCompTable = ddlCustCompView.MyDst.Tables["tbl_customer_company"].DefaultView.ToTable();

                DataRow dr = ddlCustCompTable.NewRow();
                dr["custCompyId"] = -1;
                dr["custCompName"] = string.Empty;
                dr["endTime"] = "9999-12-31";
                ddlCustCompTable.Rows.Add(dr);

                Session["ddlProjectDtS"] = ddlCustCompTable;

                ddlCustComp.DataValueField = "custCompyId";
                ddlCustComp.DataTextField = "custCompName";
                ddlCustComp.DataSource = Session["ddlProjectDtS"];
                ddlCustComp.DataBind();

                #endregion

                int mainContractId = int.Parse(Session["seldMainContractId"] as string);
                //Xm_db xmDataCont = new Xm_db(System.Configuration.ConfigurationManager.ConnectionStrings["xm_dbConnectionString"].ConnectionString);
                Xm_db xmDataCont = Xm_db.GetInstance();

                var mainContractEdit =
                    (from mainContract in xmDataCont.Tbl_mainContract
                     where mainContract.MainContractId == mainContractId &&
                           mainContract.EndTime > DateTime.Now
                     select mainContract).First();

                ddlCustComp.SelectedValue = mainContractEdit.CustCompyId.ToString();
                txtMainContractTag.Text = mainContractEdit.MainContractTag;
                txtMoney.Text = mainContractEdit.Cash.ToString();
                btnDate.Text = mainContractEdit.DateLine.ToString();
                txtPayment.Text = mainContractEdit.PaymentMode;
                txtProjAddr.Text = mainContractEdit.Tbl_projectTagInfo.ProjectOutAddress;

                #region mainProductGV

                var mainProductSelDs =
                    from mainProduct in mainContractEdit.Tbl_mainContrctProduct
                    join product in xmDataCont.Tbl_product on
                         mainProduct.ProductId equals product.ProductId
                    select new { product.ProductName, mainProduct.ProductNum };

                DataTable dt = mainProductSelDs.ToDataTable();

                if (dt.Rows.Count > 0)
                {
                    mainProductGV.DataSource = dt;
                    mainProductGV.DataBind();
                }
                #endregion                
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                string custmor = ddlCustComp.SelectedValue;
                string mainContractTag = txtMainContractTag.Text.ToString().Trim();
                string cash = txtMoney.Text.ToString().Trim();
                string payment = txtPayment.Text.ToString().Trim();
                string projectOutAddress = txtProjAddr.Text.ToString();

                int mainContractId = int.Parse(Session["seldMainContractId"] as string);
                Xm_db xmDataCont = Xm_db.GetInstance();

                var mainContractEdit =
                    (from mainContract in xmDataCont.Tbl_mainContract
                     where mainContract.MainContractId == mainContractId &&
                           mainContract.EndTime > DateTime.Now
                     select mainContract).First();

                mainContractEdit.CustCompyId = int.Parse(custmor);
                mainContractEdit.MainContractTag = mainContractTag;
                mainContractEdit.Cash = decimal.Parse(cash);
                mainContractEdit.DateLine = calendarCust.SelectedDate;
                mainContractEdit.PaymentMode = payment;
                mainContractEdit.Tbl_projectTagInfo.ProjectOutAddress = projectOutAddress;

                try
                {
                    xmDataCont.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
                }
                catch (System.Data.Linq.ChangeConflictException cce)
                {
                    string strEx = cce.Message;
                    foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataCont.ChangeConflicts)
                    {
                        occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                    }

                    xmDataCont.SubmitChanges();
                }


                Session.Remove("ddlProjectDtS");
                Session.Remove("mainContractTable");
                Session.Remove("mainProductSelDs");
                Response.Redirect("~/Main/contractManager/subContractEdit.aspx");
            }
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            Session.Remove("ddlProjectDtS");
            Session.Remove("mainContractTable");
            Session.Remove("mainProductSelDs");
            Response.Redirect("~/Main/contractManager/subContractEdit.aspx");
        }

        protected void btnDate_Click(object sender, EventArgs e)
        {
            calendarCust.Visible = true;
        }

        protected void calendarCust_SelectionChanged(object sender, EventArgs e)
        {
            btnDate.Text = calendarCust.SelectedDate.ToString();

            calendarCust.Visible = false;
        }

        protected void mainProductGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mainProductGV.PageIndex = e.NewPageIndex;

            mainProductGV.DataSource = Session["mainProductSelDs"];//["dtSources"] as DataTable;  
            mainProductGV.DataBind();
        }

        protected void mainProductGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void txtProductNum_TextChanged(object sender, EventArgs e)
        {
            TextBox txtNum = sender as TextBox;

            bool flag = txtProductNum_TextCheck(txtNum);
        }

        protected bool txtProductNum_TextCheck(TextBox txtBx)
        {
            bool flag = true;
            int sc = 0;
            try
            {
                sc = int.Parse(txtBx.Text.ToString().Trim());
                txtBx.Text = sc.ToString();
                //Session["flagContact"] = bool.TrueString.ToString().Trim();
            }
            catch (ArgumentNullException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }
            catch (FormatException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }
            catch (OverflowException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }

            //btnOk();

            return flag;
        }

        protected bool txtDoubleNumber_Check(TextBox txtBx)
        {
            bool flag = true;

            string strBx = string.Empty;
            double sc = 0;
            try
            {
                strBx = txtBx.Text.ToString().Trim();
                sc = double.Parse(strBx);
                txtBx.Text = strBx;
                //Session["flagContact"] = bool.TrueString.ToString().Trim();
            }
            catch (ArgumentNullException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }
            catch (FormatException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }
            catch (OverflowException parseEx)
            {
                txtBx.Text = parseEx.Message;
                flag = false;
            }

            //btnOk();

            return flag;
        }

        protected bool txtNullOrLenth_Check(TextBox txtBx)
        {
            bool flag = true;

            string strTxt = txtBx.Text.ToString().Trim();
            if (string.IsNullOrWhiteSpace(strTxt))
            {
                txtBx.Text = "不能为空！";
                flag = false;
            }
            else if (strTxt.Length > 50)
            {
                txtBx.Text = "不能超过50个字！";
                flag = false;
            }
            else if (strTxt.Equals("不能为空！"))
            {
                txtBx.Text = "不能为空！  ";
                flag = false;
            }
            else if (strTxt.Equals("不能超过50个字！"))
            {
                txtBx.Text = "不能超过50个字！  ";
                flag = false;
            }

            return flag;
        }        

        protected void txtMainContractTag_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBx = sender as TextBox;
            txtNullOrLenth_Check(txtBx);
        }

        protected void txtCustmor_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBx = sender as TextBox;
            txtNullOrLenth_Check(txtBx);
        }

        protected void txtMoney_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBx = sender as TextBox;
            txtDoubleNumber_Check(txtBx);
        }

        protected void txtPayment_TextChanged(object sender, EventArgs e)
        {
            TextBox txtBx = sender as TextBox;
            txtNullOrLenth_Check(txtBx);
        }

        protected bool ddlUnSelect_Check(DropDownList ddl)
        {
            bool flag = true;

            if (ddl.SelectedValue.Equals("-1"))
            {
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

            flag = txtNullOrLenth_Check(txtMainContractTag)
                && txtNullOrLenth_Check(txtProjAddr)
                && ddlUnSelect_Check(ddlCustComp)
                && txtDoubleNumber_Check(txtMoney)
                && txtNullOrLenth_Check(txtPayment);

            return flag;
        }
    }
}