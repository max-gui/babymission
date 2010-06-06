using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;

public partial class Account_Register : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
//        txtName.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        if (!IsPostBack)
        {
            Session["flagName"] = bool.FalseString.ToString().Trim();
            Session["flagUsrName"] = bool.FalseString.ToString().Trim();
            Session["flagPassWord"] = bool.FalseString.ToString().Trim();
            Session["flagContact"] = bool.FalseString.ToString().Trim();
        }
    }

    /*
    protected void RegisterUser_CreatedUser(object sender, EventArgs e)
    {
        FormsAuthentication.SetAuthCookie(this.txtUsrName.Text.ToString().Trim(), false);

        string continueUrl = Request.QueryString["ReturnUrl"];
        if (String.IsNullOrEmpty(continueUrl))
        {
            continueUrl = "~/";
        }
        Response.Redirect(continueUrl);
    }
    */
    protected void btnReg_Click(object sender, EventArgs e)
    {
        string sn = txtName.Text.ToString().Trim();
        string sun = txtUsrName.Text.ToString().Trim();
        string spw = txtPassWord.Text.ToString().Trim();
        int sc = int.Parse(txtContact.Text.ToString().Trim());
        
        FormsAuthentication.SetAuthCookie(this.txtUsrName.Text.ToString().Trim(), false /* createPersistentCookie */);

        DataSet dSet = new DataSet();

        ConnectionStringSettings cfg = 
            ConfigurationManager.ConnectionStrings["My DB"];
        DbProviderFactory factory =
            DbProviderFactories.GetFactory(cfg.ProviderName);
        DbConnection da = factory.CreateConnection();
        DbCommand dcmd = factory.CreateCommand();
        da.ConnectionString = cfg.ConnectionString;
        dcmd.Connection = da;
        dcmd.CommandText = "select * from tbl_usr";
        
        /*
        using (SqlConnection cnx = 
            new SqlConnection(cfg.ConnectionString.ToString().Trim()))
        {*/
            using ( SqlDataAdapter dAdapter = new SqlDataAdapter() )
            {
                dAdapter.SelectCommand = dcmd as SqlCommand;
                /*
                dAdapter.SelectCommand = new SqlCommand(
                    "select * from tbl_usr",cnx );
                 * */
                dAdapter.Fill(dSet, "tbl_usr" );

            }
        //}

        DataColumn dCol1 = dSet.Tables["tbl_usr"].Columns["usrName"];

        string strTemp = dSet.Tables["tbl_usr"].Rows[0]["usrName"].ToString().Trim();




        string continueUrl = Request.QueryString["ReturnUrl"];
        if (String.IsNullOrEmpty(continueUrl))
        {
            continueUrl = "~/";
        }
        Response.Redirect(continueUrl);
    }
    protected void txtName_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtName.Text.ToString().Trim()))
        {
            lblName.Text = "*必填项";
        }
        else
        {
            lblName.Text = string.Empty;
            Session["flagName"] = bool.TrueString.ToString().Trim();
            btnOk();
        }
    }
    protected void txtUsrName_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtUsrName.Text.ToString().Trim()))
        {
            txtUsrName.Text = "*必填项";
        }
        else
        {
            lblUsrName.Text = string.Empty;
            Session["flagUsrName"] = bool.TrueString.ToString().Trim();
            btnOk();
        }
    }
    protected void txtPassWord_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtPassWord.Text.ToString().Trim()))
        {
            txtPassWord.Text = "*必填项";
        }
        else
        {
            lblPassWord.Text = string.Empty;
            Session["flagPassWord"] = bool.TrueString.ToString().Trim();
            btnOk();
        }
    }
    protected void txtContact_TextChanged(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtContact.Text.ToString().Trim()))
        {
            txtContact.Text = "*必填项";
            Session["flagContact"] = bool.FalseString.ToString().Trim();
        }
        else if (txtContact.Text.ToString().Trim().Length < 11)
        {
            lblContact.Text = "手机号码不能短于11位";
            Session["flagContact"] = bool.FalseString.ToString().Trim();
        }
        else if (txtContact.Text.ToString().Trim().Length < 11)
        {
            lblContact.Text = "手机号码不能超过11位";
            Session["flagContact"] = bool.FalseString.ToString().Trim();
        }
        else
        {
            try
            {
                int sc = int.Parse(txtContact.Text.ToString().Trim());
                lblContact.Text = string.Empty;
                Session["flagContact"] = bool.TrueString.ToString().Trim();
            }
            catch (FormatException fe)
            {
                lblContact.Text = "手机号码只能包含数字";
                Session["flagContact"] = bool.FalseString.ToString().Trim();
            }            
        }

        btnOk();
    }
    protected void btnOk()
    {
        btnReg.Enabled = false;

        if (Session["flagName"].Equals(bool.FalseString.ToString().Trim()))
        {            
        }
        else if (Session["flagUsrName"].Equals(bool.FalseString.ToString().Trim()))
        {
        }
        else if (Session["flagPassWord"].Equals(bool.FalseString.ToString().Trim()))
        {
        }
        else if (Session["flagContact"].Equals(bool.FalseString.ToString().Trim()))
        {
            btnReg.Enabled = true;
        }
    }
}
