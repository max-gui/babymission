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
}
