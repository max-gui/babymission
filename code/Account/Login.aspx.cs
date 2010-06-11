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
        //DataColumn colAuth = new DataColumn("totleAuthority", System.Type.GetType("System.Int32"));
        DataColumn colPwd = new DataColumn("usrPassWord", System.Type.GetType("System.String"));
        //DataColumn colId = new DataColumn("usrId", System.Type.GetType("System.Int32"));
        //DataColumn colEnd = new DataColumn("endTime", System.Type.GetType("System.DateTime"));
        
        DataTable userTable = new DataTable("tbl_usr");

        //colName.DataType = System.Type.GetType("System.String");
        //colAuth.DataType = System.Type.GetType("System.Int32");
        //colPwd.DataType = System.Type.GetType("System.String");
        //colId.DataType = System.Type.GetType("System.Int32");

        //colName.ColumnName = "usrName";
        //colPwd.ColumnName = "usrPassWord";
        //colId.ColumnName = "usrId";
        //colAuth.ColumnName = "totleAuthority";

        userTable.Columns.Add(colName);
        //userTable.Columns.Add(colAuth);
        userTable.Columns.Add(colPwd);
        //userTable.Columns.Add(colId);
        //userTable.Columns.Add(colEnd);

        userRow = userTable.NewRow();
        userRow["usrName"] = loginCon.UserName.ToString().Trim();
        userRow["usrPassWord"] = loginCon.Password.ToString().Trim();
        //userRow["endTime"] = DateTime.Today;
        userTable.Rows.Add(userRow);

        dataSet.Tables.Add(userTable);
        #endregion

        


        UserProcess myLogin = new UserProcess(dataSet);

        myLogin.DoLogin();
        int rowRtn = myLogin.IntRtn;
                
        if (0 != rowRtn)
        {
            using (DataTable dt =
                        myLogin.MyDst.Tables["tbl_usr"].DefaultView.ToTable())
            {
                Session["totleAuthority"] =
                    dt.Rows[0]["totleAuthority"].ToString();
                Session["usrId"] =
                    dt.Rows[0]["usrId"].ToString();

                string strRealName = 
                    dt.Rows[0]["realName"].ToString();
                FormsAuthentication.SetAuthCookie(strRealName, false);
                                
                string continueUrl = "~/Main/DefaultMainSite.aspx";//Request.QueryString["ReturnUrl"];
                if (String.IsNullOrEmpty(continueUrl))
                {
                    continueUrl = "~/";
                }
                Response.Redirect(continueUrl);
                //aspxName = myLogin.StrRtn + "Main.aspx";
                //Server.Transfer(aspxName);
            }
        }
        else
        {
            
        }
    }
}
