using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
namespace xm_mis.db
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
            sqlParaId = new SqlParameter("@Identity", SqlDbType.Int, 0, "custCompyId");
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

        public void CustCompUpdate(int custCompId, string custCompName, string custCompAddr, string custCompTag)
        {
            #region sqlPara declare
            //custCompId
            SqlParameter sqlParaCustCompId = null;
            //custCompName
            SqlParameter sqlParaCustCompName = null;
            //custCompName
            SqlParameter sqlParaCustCompAddr = null;
            //custCompName
            SqlParameter sqlParaCustCompTag = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_customer_company_update";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit

            sqlParaCustCompId = new SqlParameter("@custCompyId", custCompId);
            //sqlParaDepEnd = new SqlParameter("@delEndTime", st);
            sqlParaCustCompName = new SqlParameter("@newCustCompName", custCompName);
            sqlParaCustCompAddr = new SqlParameter("@newCustCompAddress", custCompAddr);
            sqlParaCustCompTag = new SqlParameter("@newCustCompTag", custCompTag);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaCustCompId);
            //sqlCmd.Parameters.Add(sqlParaDepEnd);
            sqlCmd.Parameters.Add(sqlParaCustCompName);
            sqlCmd.Parameters.Add(sqlParaCustCompAddr);
            sqlCmd.Parameters.Add(sqlParaCustCompTag);
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }

        public void CustCompDel(string custCompId)
        {
            #region sqlPara declare
            //custCompId
            SqlParameter sqlParaCustCompId = null;
            //custCompEnd
            SqlParameter sqlParaCustCompEnd = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_customer_company_delete";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int custCompIdL = int.Parse(custCompId);
            DateTime st = DateTime.Now;

            sqlParaCustCompId = new SqlParameter("@delCustCompyId", custCompIdL);
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
            sqlCmd.CommandType = CommandType.Text;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "tbl_customer_company");

            return myDataSet;
        }
    }
}