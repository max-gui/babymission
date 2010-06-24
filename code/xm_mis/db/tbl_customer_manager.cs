﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;

/// <summary>
///tbl_customer_company 的摘要说明
/// </summary>

namespace xm_mis.App_Code.db
{
    public class tbl_customer_manager : DataBase
    {
        public tbl_customer_manager()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string SelectAdd(DataSet dataSet)
        {
            #region sqlPara declare
            //custManName
            SqlParameter sqlParaCustManName = null;
            //custManContact
            SqlParameter sqlParaCustManContact = null;
            //custManEmail
            SqlParameter sqlParaCustManEmail = null;
            //custManDepart
            SqlParameter sqlParaCustManDepart = null;
            //custManTitle
            SqlParameter sqlParaCustManTitle = null;
            //custCompyId
            SqlParameter sqlParaCustCompyId = null;
            //start
            SqlParameter sqlParaSt = null;
            //newCompId
            SqlParameter sqlParaId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_customer_manager_Insert";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            string custManName = dataSet.Tables["tbl_customer_manager"].Rows[0]["custManName"].ToString();
            string custManContact = dataSet.Tables["tbl_customer_manager"].Rows[0]["custManContact"].ToString();
            string custManEmail = dataSet.Tables["tbl_customer_manager"].Rows[0]["custManEmail"].ToString();
            string custManDepart = dataSet.Tables["tbl_customer_manager"].Rows[0]["custManDepart"].ToString();
            string custManTitle = dataSet.Tables["tbl_customer_manager"].Rows[0]["custManTitle"].ToString();
            long custCompyId = long.Parse(dataSet.Tables["tbl_customer_manager"].Rows[0]["custCompyId"].ToString());
            DateTime st = DateTime.Now;

            sqlParaCustManName = new SqlParameter("@custManName", custManName);
            sqlParaCustManContact = new SqlParameter("@custManContact", custManName);
            sqlParaCustManEmail = new SqlParameter("@custManEmail", custManContact);
            sqlParaCustManDepart = new SqlParameter("@custManDepart", custManEmail);
            sqlParaCustManTitle = new SqlParameter("@custManTitle", custManTitle);
            sqlParaCustCompyId = new SqlParameter("@custCompyId", custCompyId);
            sqlParaSt = new SqlParameter("@startTime", st);
            sqlParaId = new SqlParameter("@Identity", SqlDbType.BigInt, 0, "custManId");
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaCustManName);
            sqlCmd.Parameters.Add(sqlParaCustManContact);
            sqlCmd.Parameters.Add(sqlParaCustManEmail);
            sqlCmd.Parameters.Add(sqlParaCustManDepart);
            sqlCmd.Parameters.Add(sqlParaCustManTitle);
            sqlCmd.Parameters.Add(sqlParaCustCompyId);
            sqlCmd.Parameters.Add(sqlParaSt);
            sqlCmd.Parameters.Add(sqlParaId);
            #endregion

            #region sqlDirection
            sqlParaId.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string custManId = sqlParaId.Value.ToString();
            return custManId;
        }

        public DataSet SelectView()
        {
            SqlCommand sqlCmd = null;

            string strSQL =
                "SELECT " +
                "* " +
                "FROM tbl_customer_company ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;

            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "tbl_customer_company");

            return myDataSet;
        }
    }
}