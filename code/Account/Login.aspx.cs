using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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

        DataColumn colName = new DataColumn();
        DataColumn colAuth = new DataColumn();
        DataColumn colPwd = new DataColumn();
        DataColumn colId = new DataColumn();

        DataTable userTable = new DataTable("view_usr_info");

        colName.DataType = System.Type.GetType("System.String");
        colAuth.DataType = System.Type.GetType("System.Int32");
        colPwd.DataType = System.Type.GetType("System.String");
        colId.DataType = System.Type.GetType("System.Int32");

        colName.ColumnName = "usrName";
        colPwd.ColumnName = "usrPassWord";
        colId.ColumnName = "usrId";
        colAuth.ColumnName = "totleAuthority";

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

        string aspxName = string.Empty;
        UserProcess myLogin = new UserProcess(dataSet);

        myLogin.DoLogin();
        aspxName = myLogin.StrRtn;
        if (String.IsNullOrEmpty(aspxName))
        {
            Session["totleAuthority"] =
                myLogin.MyDst.Tables["view_usr_info"].Rows[0]["totleAuthority"].ToString().Trim();
            Session["usrName"] =
                myLogin.MyDst.Tables["view_usr_info"].Rows[0]["usrName"].ToString().Trim();
            Session["usrId"] =
                myLogin.MyDst.Tables["view_usr_info"].Rows[0]["usrId"].ToString().Trim();

            string continueUrl = "~/Default.aspx";//Request.QueryString["ReturnUrl"];
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
