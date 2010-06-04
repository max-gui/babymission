using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;

/// <summary>
///view_usr_info 的摘要说明
/// </summary>
public class view_usr_info : DataBase
{
	public view_usr_info()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public DataSet SelectLogin(DataSet dataSet)
    {
        SqlParameter sqlParaName = null;
        SqlParameter sqlParaPassWord = null;
        SqlCommand sqlCmd = null;

        string strSQL =
            "SELECT " +
            "* " +
            "FROM view_usr_info " +
            "WHERE " +
            "usrName = @usrName and usrPassWord = @usrPassWord";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;

        sqlParaName = new SqlParameter("@usrName", SqlDbType.Char, 10);
        sqlParaName.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrName"].ToString().Trim();
        sqlParaPassWord = new SqlParameter("@usrPassWord", SqlDbType.Char, 10);
        sqlParaPassWord.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrPassWord"].ToString().Trim();
        sqlCmd.Parameters.Add(sqlParaName);
        sqlCmd.Parameters.Add(sqlParaPassWord);

        SqlDataAdapter userDataAdapter = this.SqlDA;
        //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
        DataSet myDataSet = new DataSet();
        userDataAdapter.Fill(myDataSet, "view_usr_info");

        return myDataSet;
    }
}