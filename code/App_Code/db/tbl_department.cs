using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
/// <summary>
///tbl_department 的摘要说明
/// </summary>
public class tbl_department :DataBase
{
	public tbl_department()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public DataSet SelectSelfDepatView(DataSet dataSet)
    {
 //       SqlParameter sqlParaName = null;
        SqlParameter sqlParaIsDel = null;
        SqlCommand sqlCmd = null;

        string strSQL =
            "SELECT " +
            "departmentId , departmentName , isDel " +
            "FROM tbl_department " +
            "WHERE " +
            "isDel = @isDel ";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;

        //sqlParaName = new SqlParameter("@usrName", SqlDbType.Char, 10);
        //sqlParaName.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrName"].ToString().Trim();
        sqlParaIsDel = new SqlParameter("@isDel", SqlDbType.Char, 10);
        sqlParaIsDel.Value = bool.FalseString.ToString().Trim();
        //sqlCmd.Parameters.Add(sqlParaName);
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaIsDel);

        SqlDataAdapter userDataAdapter = this.SqlDA;

        //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
        DataSet myDataSet = new DataSet();
        userDataAdapter.Fill(myDataSet, "tbl_department");

        return myDataSet;
    }

    public void SelectSelfDepatCommit(DataSet dataSet)
    {
        //sqlParaName = new SqlParameter("@usrName", SqlDbType.Char, 10);
        //sqlParaName.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrName"].ToString().Trim();
        //       sqlParaIsDel = new SqlParameter("@isDel", SqlDbType.Char, 10);
        //       sqlParaIsDel.Value = bool.FalseString.ToString().Trim();
        //sqlCmd.Parameters.Add(sqlParaName);
        //       sqlCmd.Parameters.Add(sqlParaIsDel);

        SqlDataAdapter da = this.SqlDA;
        SqlCommandBuilder scb = new SqlCommandBuilder(da);
        //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
        da.Update(dataSet, "tbl_department");
    }
}