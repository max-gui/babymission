using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class Account_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
    }
    
    protected void loginCon_Authenticate(object sender, AuthenticateEventArgs e)
    {
        #region dataset
        DataSet dataSet = new DataSet();
        DataRow userRow = null;

        DataColumn colName = new DataColumn("usrName", System.Type.GetType("System.String"));
        DataColumn colAuth = new DataColumn("totleAuthority", System.Type.GetType("System.Int32"));
        DataColumn colPwd = new DataColumn("usrPassWord", System.Type.GetType("System.String"));
        DataColumn colId = new DataColumn("usrId", System.Type.GetType("System.Int32"));

        DataTable userTable = new DataTable("view_usr_info");

        //colName.DataType = System.Type.GetType("System.String");
        //colAuth.DataType = System.Type.GetType("System.Int32");
        //colPwd.DataType = System.Type.GetType("System.String");
        //colId.DataType = System.Type.GetType("System.Int32");

        //colName.ColumnName = "usrName";
        //colPwd.ColumnName = "usrPassWord";
        //colId.ColumnName = "usrId";
        //colAuth.ColumnName = "totleAuthority";

        userTable.Columns.Add(colId);
        userTable.Columns.Add(colName);
        userTable.Columns.Add(colAuth);
        userTable.Columns.Add(colPwd);

        userRow = userTable.NewRow();
        userRow["usrName"] = loginCon.UserName.ToString().Trim();
        userRow["usrPassWord"] = loginCon.Password.ToString().Trim();
        userTable.Rows.Add(userRow);

        dataSet.Tables.Add(userTable);
        #endregion

        


        int usrAuth = 0;
        UserProcess myLogin = new UserProcess(dataSet);

        myLogin.DoLogin();
        usrAuth = myLogin.IntRtn;
        if (0 != usrAuth)
        {
            FormsAuthentication.SetAuthCookie(this.loginCon.UserName.ToString().Trim(), false /* createPersistentCookie */);

            Session["totleAuthority"] =
                myLogin.MyDst.Tables["view_usr_info"].Rows[0]["totleAuthority"].ToString().Trim();
            Session["usrName"] =
                myLogin.MyDst.Tables["view_usr_info"].Rows[0]["usrName"].ToString().Trim();
            Session["usrId"] =
                myLogin.MyDst.Tables["view_usr_info"].Rows[0]["usrId"].ToString().Trim();

            string continueUrl = "~/Main/DefaultMainSite.aspx";//Request.QueryString["ReturnUrl"];
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
            //aspxName = myLogin.StrRtn + "Main.aspx";
            //Server.Transfer(aspxName);
        }
        else
        {
            
        }
    }
}
