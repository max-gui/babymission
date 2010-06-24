using System;
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
    public class tbl_customer_company : DataBase
    {
        public tbl_customer_company()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string SelectAdd(DataSet dataSet)
        {
            #region sqlPara declare
            //CompName
            SqlParameter sqlParaCompName = null;
            //CompAddr
            SqlParameter sqlParaCompAddr = null;
            //CompTag
            SqlParameter sqlParaCompTag = null;
            //start
            SqlParameter sqlParaSt = null;
            //newCompId
            SqlParameter sqlParaId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_customer_company_Insert";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            string cn = dataSet.Tables["tbl_customer_company"].Rows[0]["custCompName"].ToString().Trim();
            string ca = dataSet.Tables["tbl_customer_company"].Rows[0]["custCompAddress"].ToString().Trim();
            string ct = dataSet.Tables["tbl_customer_company"].Rows[0]["custCompTag"].ToString().Trim();
            DateTime st = DateTime.Now;

            sqlParaCompName = new SqlParameter("@custCompName", cn);
            sqlParaCompAddr = new SqlParameter("@custCompAddress", ca);
            sqlParaCompTag = new SqlParameter("@custCompTag", ct);
            sqlParaSt = new SqlParameter("@startTime", st);
            sqlParaId = new SqlParameter("@Identity", SqlDbType.BigInt, 0, "custCompyId");
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaCompName);
            sqlCmd.Parameters.Add(sqlParaCompAddr);
            sqlCmd.Parameters.Add(sqlParaCompTag);
            sqlCmd.Parameters.Add(sqlParaSt);
            sqlCmd.Parameters.Add(sqlParaId);
            #endregion

            #region sqlDirection
            sqlParaId.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string compId = sqlParaId.Value.ToString();
            return compId;
        }

        public void SelfCustCompDel(string custCompId)
        {
            #region sqlPara declare
            //custCompId
            SqlParameter sqlParaCustCompId = null;
            //custCompEnd
            SqlParameter sqlParaCustCompEnd = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_department_delete";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            long custCompIdL = long.Parse(custCompId);
            DateTime st = DateTime.Now;

            sqlParaCustCompId = new SqlParameter("@delDepartmentId", custCompIdL);
            sqlParaCustCompEnd = new SqlParameter("@delEndTime", st);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaCustCompId);
            sqlCmd.Parameters.Add(sqlParaCustCompEnd);
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
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