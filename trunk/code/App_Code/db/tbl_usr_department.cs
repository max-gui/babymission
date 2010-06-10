﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
/// <summary>
///tbl_usr_department 的摘要说明
/// </summary>
public class tbl_usr_department : DataBase
{
	public tbl_usr_department()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public void SelectAdd(string usrName, string depName)
    {
        SqlParameter sqlParaUNM = null;
        SqlParameter sqlParaDNM = null;
        SqlParameter sqlParaSt = null;
        SqlCommand sqlCmd = null;

        string strSQL =
            "insert into " +
            "tbl_usr_department " +
            "(usrName , departmentName , startTime) " +
            "values(@usrName , @departmentName , @startTime) ";
        //"WHERE " +
        //"isDel = @isDel ";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;

        sqlParaUNM = new SqlParameter("@usrName", usrName);
        sqlParaDNM = new SqlParameter("@departmentName", depName);
        DateTime st = DateTime.Now;
        sqlParaSt = new SqlParameter("@startTime", st);
        
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaUNM);
        sqlCmd.Parameters.Add(sqlParaDNM);
        sqlCmd.Parameters.Add(sqlParaSt);

        sqlCmd.Connection.Open();

        sqlCmd.ExecuteNonQuery();

        sqlCmd.Connection.Close();
    }

    public void SelectUpdate(int usrId, string depName)
    {
        SqlParameter sqlParaUid = null;
        SqlParameter sqlParaDNM = null;
        SqlCommand sqlCmd = null;

        string strSQL =
            "update " +
            "tbl_usr_department " +
            "set " +
            "departmentName = @departmentName " +
            "where usrId = @usrId ";
        //"WHERE " +
        //"isDel = @isDel ";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;

        sqlParaUid = new SqlParameter("@usrId", usrId);
        sqlParaDNM = new SqlParameter("@departmentName", depName);

        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaUid);
        sqlCmd.Parameters.Add(sqlParaDNM);

        sqlCmd.Connection.Open();

        sqlCmd.ExecuteNonQuery();

        sqlCmd.Connection.Close();
    }
    //public void SelectUsrAuthCommit(DataSet dataSet)
    //{
    //    //sqlParaName = new SqlParameter("@usrName", SqlDbType.Char, 10);
    //    //sqlParaName.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrName"].ToString().Trim();
    //    //       sqlParaIsDel = new SqlParameter("@isDel", SqlDbType.Char, 10);
    //    //       sqlParaIsDel.Value = bool.FalseString.ToString().Trim();
    //    //sqlCmd.Parameters.Add(sqlParaName);
    //    //       sqlCmd.Parameters.Add(sqlParaIsDel);

    //    SqlDataAdapter da = this.SqlDA;
    //    SqlCommandBuilder scb = new SqlCommandBuilder(da);
    //    //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
    //    da.Update(dataSet, "tbl_usr_authority");
    //    dataSet.AcceptChanges();
    //}
}