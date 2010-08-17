﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
namespace xm_mis.db
{
    public class tbl_usr_title : DataBase
    {
        public tbl_usr_title()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public void SelfUsrTitleUpdate(int usrTitleId, int usrId, int titleId)
        {
            #region sqlPara declare
            //usrTitleId
            SqlParameter sqlParaUsrTitleId = null;
            ////endTime
            //SqlParameter sqlParaEnd = null;
            //usrId
            SqlParameter sqlParaUsrId = null;
            //titleId
            SqlParameter sqlParaTitleId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_usr_title_update";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            //DateTime st = DateTime.Now;

            sqlParaUsrTitleId = new SqlParameter("@usrTitleId", usrTitleId);
            //sqlParaEnd = new SqlParameter("@delEndTime", st);
            sqlParaUsrId = new SqlParameter("@newUsrId", usrId);
            sqlParaTitleId = new SqlParameter("@newTitleId", titleId);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaUsrTitleId);
            //sqlCmd.Parameters.Add(sqlParaEnd);
            sqlCmd.Parameters.Add(sqlParaUsrId);
            sqlCmd.Parameters.Add(sqlParaTitleId);
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }

        public void SelectAdd(string usrName, string titleName)
        {
            SqlParameter sqlParaUNM = null;
            SqlParameter sqlParaTNM = null;
            SqlParameter sqlParaSt = null;
            SqlCommand sqlCmd = null;

            string strSQL =
                "insert into " +
                "tbl_usr_title " +
                "(usrName , titleName , startTime) " +
                "values(@usrName , @titleName , @startTime) ";
            //"WHERE " +
            //"isDel = @isDel ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;

            sqlParaUNM = new SqlParameter("@usrName", usrName);
            sqlParaTNM = new SqlParameter("@titleName", titleName);
            DateTime st = DateTime.Now;
            sqlParaSt = new SqlParameter("@startTime", st);

            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaUNM);
            sqlCmd.Parameters.Add(sqlParaTNM);
            sqlCmd.Parameters.Add(sqlParaSt);

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }

        public void SelectDel(string usrName, string titleName, DateTime TitleSt)
        {
            SqlParameter sqlParaUNM = null;
            SqlParameter sqlParaTNM = null;
            SqlParameter sqlParaTSt = null;
            SqlParameter sqlParaTEnd = null;
            SqlParameter sqlParaTNEnd = null;
            SqlCommand sqlCmd = null;

            string strSQL =
                "update " +
                "tbl_usr_title " +
                "set " +
                "endTime = @nendTime " +
                "where usrName = @usrName and titleName = @titleName and startTime = @startTime and endTime = @endTime";
            //"WHERE " +
            //"isDel = @isDel ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;

            sqlParaUNM = new SqlParameter("@usrName", usrName);
            sqlParaTNM = new SqlParameter("@titleName", titleName);
            sqlParaTSt = new SqlParameter("@startTime", TitleSt);
            sqlParaTEnd = new SqlParameter("@endTime", DateTime.Parse("9999-12-31"));
            sqlParaTNEnd = new SqlParameter("@nendTime", DateTime.Now);

            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaUNM);
            sqlCmd.Parameters.Add(sqlParaTNM);
            sqlCmd.Parameters.Add(sqlParaTSt);
            sqlCmd.Parameters.Add(sqlParaTEnd);
            sqlCmd.Parameters.Add(sqlParaTNEnd);

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }
    }
}