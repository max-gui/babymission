using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
/// <summary>
///tbl_usr 的摘要说明
/// </summary>
public class tbl_usr : DataBase
{
	public tbl_usr()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public DataSet SelectUsr(DataSet dataSet)
    {
        SqlParameter sqlParaName = null;
        //SqlParameter sqlParaPassWord = null;
        //SqlParameter sqlParaContact = null;
        SqlCommand sqlCmd = null;

        string strSQL =
            "select " +
            "usrId , realName , usrName , usrPassWord , usrContact " +
            "from tbl_usr " +
            "WHERE " +
            "usrName = @usrName ";
             
        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;

        sqlParaName = new SqlParameter("@usrName", SqlDbType.Char, 10);
        sqlParaName.Value = dataSet.Tables["tbl_usr"].Rows[0]["usrName"].ToString().Trim();
        //sqlParaPassWord = new SqlParameter("@usrPassWord", SqlDbType.Char, 10);
        //sqlParaPassWord.Value = dataSet.Tables["tbl_usr"].Rows[0]["usrPassWord"].ToString().Trim();
        //sqlParaContact = new SqlParameter("@usrContact", SqlDbType.Char, 10);
        //sqlParaContact.Value = dataSet.Tables["tbl_usr"].Rows[0]["usrContact"].ToString().Trim();

        sqlCmd.Parameters.Add(sqlParaName);
        //sqlCmd.Parameters.Add(sqlParaPassWord);
        //sqlCmd.Parameters.Add(sqlParaContact);

        SqlDataAdapter userDataAdapter = this.SqlDA;
        //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
        DataSet myDataSet = new DataSet();
        userDataAdapter.Fill(myDataSet, "tbl_usr");

        return myDataSet;
    }

    public void SelecttUsrCommit(DataSet dataSet)
    {
        SqlDataAdapter da = this.SqlDA;
        SqlCommandBuilder scb = new SqlCommandBuilder(da);
        //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
        da.Update(dataSet, "tbl_usr");
        dataSet.AcceptChanges();
    }
}