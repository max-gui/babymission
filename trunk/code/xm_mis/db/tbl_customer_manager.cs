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
            int custCompyId = int.Parse(dataSet.Tables["tbl_customer_manager"].Rows[0]["custCompyId"].ToString());

            sqlParaCustManName = new SqlParameter("@custManName", custManName);
            sqlParaCustManContact = new SqlParameter("@custManContact", custManContact);
            sqlParaCustManEmail = new SqlParameter("@custManEmail", custManEmail);
            sqlParaCustManDepart = new SqlParameter("@custManDepart", custManDepart);
            sqlParaCustManTitle = new SqlParameter("@custManTitle", custManTitle);
            sqlParaCustCompyId = new SqlParameter("@custCompyId", custCompyId);
            sqlParaId = new SqlParameter("@Identity", SqlDbType.Int, 0, "custManId");
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaCustManName);
            sqlCmd.Parameters.Add(sqlParaCustManContact);
            sqlCmd.Parameters.Add(sqlParaCustManEmail);
            sqlCmd.Parameters.Add(sqlParaCustManDepart);
            sqlCmd.Parameters.Add(sqlParaCustManTitle);
            sqlCmd.Parameters.Add(sqlParaCustCompyId);
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
        
        public void custCompManUpdate(int custManId, string compManName, string compManCont, string compManEmail, string compManDep,string compManTitle)
        {
            #region sqlPara declare
            //custManId
            SqlParameter sqlParaCustManId = null;
            //custManName
            SqlParameter sqlParaCustManName = null;
            //custManContact
            SqlParameter sqlParaCustManCont = null;
            //custManEmail
            SqlParameter sqlParaCustManEmail = null;
            //custManDepart
            SqlParameter sqlParaCustManDepart = null;
            //custManTitle
            SqlParameter sqlParaCustManTitle = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_customer_manager_update";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit

            sqlParaCustManId = new SqlParameter("@custManId", custManId);
            //sqlParaDepEnd = new SqlParameter("@delEndTime", st);
            sqlParaCustManName = new SqlParameter("@newCustManName", compManName);
            sqlParaCustManCont = new SqlParameter("@newCustManCont", compManCont);
            sqlParaCustManEmail = new SqlParameter("@newCustManEmail", compManEmail);
            sqlParaCustManDepart = new SqlParameter("@newCustManDep", compManDep);
            sqlParaCustManTitle = new SqlParameter("@newCustManTitle", compManTitle);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaCustManId);
            //sqlCmd.Parameters.Add(sqlParaDepEnd);
            sqlCmd.Parameters.Add(sqlParaCustManName);
            sqlCmd.Parameters.Add(sqlParaCustManCont);
            sqlCmd.Parameters.Add(sqlParaCustManEmail);
            sqlCmd.Parameters.Add(sqlParaCustManDepart);
            sqlCmd.Parameters.Add(sqlParaCustManTitle);
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }

        public void CustCompManDel(string custCompManId)
        {
            #region sqlPara declare
            //custCompManId
            SqlParameter sqlParaCustCompManId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_customer_manager_delete";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int custCompManIdTemp = int.Parse(custCompManId);

            sqlParaCustCompManId = new SqlParameter("@delCustManId", custCompManIdTemp);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaCustCompManId);
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
                "FROM tbl_customer_manager ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;

            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;
            sqlCmd.CommandType = CommandType.Text;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "tbl_customer_manager");

            return myDataSet;
        }
    }
}