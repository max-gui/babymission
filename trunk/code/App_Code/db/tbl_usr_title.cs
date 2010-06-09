using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
/// <summary>
///tbl_usr_title 的摘要说明
/// </summary>
public class tbl_usr_title : DataBase
{
	public tbl_usr_title()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public void SelectAdd(int usrId, int titleId)
    {
        SqlParameter sqlParaUid = null;
        SqlParameter sqlParaTid = null;
        SqlCommand sqlCmd = null;

        string strSQL =
            "insert into " +
            "tbl_usr_title " +
            "(usrId , titleId) " +
            "values(@usrId , @titleId) ";
        //"WHERE " +
        //"isDel = @isDel ";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;

        sqlParaUid = new SqlParameter("@usrId", usrId);
        sqlParaTid = new SqlParameter("@titleId", titleId);

        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaUid);
        sqlCmd.Parameters.Add(sqlParaTid);

        sqlCmd.Connection.Open();

        sqlCmd.ExecuteNonQuery();

        sqlCmd.Connection.Close();
    }

    public void SelectUpdate(int usrId, int titleId)
    {
        SqlParameter sqlParaUid = null;
        SqlParameter sqlParaTid = null;
        SqlCommand sqlCmd = null;

        string strSQL =
            "update " +
            "tbl_usr_title " +
            "set " +
            "titleId = @titleId " +
            "where usrId = @usrId ";
        //"WHERE " +
        //"isDel = @isDel ";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;

        sqlParaUid = new SqlParameter("@usrId", usrId);
        sqlParaTid = new SqlParameter("@titleId", titleId);

        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaUid);
        sqlCmd.Parameters.Add(sqlParaTid);

        sqlCmd.Connection.Open();

        sqlCmd.ExecuteNonQuery();

        sqlCmd.Connection.Close();
    }
}