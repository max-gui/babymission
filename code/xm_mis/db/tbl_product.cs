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
    public class tbl_product : DataBase
    {
        public tbl_product()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string SelectAdd(DataSet dataSet)
        {
            #region sqlPara declare
            //productName
            SqlParameter sqlParaProductName = null;
            //startTime
            SqlParameter sqlParaStartTime = null;
            //Identity
            SqlParameter sqlParaId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_product_Insert";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            string pn = dataSet.Tables["tbl_product"].Rows[0]["productName"].ToString().Trim();
            DateTime st = DateTime.Now;

            sqlParaProductName = new SqlParameter("@productName", pn);
            sqlParaStartTime = new SqlParameter("@startTime", st);
            sqlParaId = new SqlParameter("@Identity", SqlDbType.Int);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaProductName);
            sqlCmd.Parameters.Add(sqlParaStartTime);
            sqlCmd.Parameters.Add(sqlParaId);
            #endregion

            #region sqlDirection
            sqlParaId.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string productId = sqlParaId.Value.ToString();
            return productId;
        }

        public void ProductUpdate(int productId, string productName)
        {
            #region sqlPara declare
            //productId
            SqlParameter sqlParaProductId = null;
            //productName
            SqlParameter sqlParaProductName = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_product_update";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit

            sqlParaProductId = new SqlParameter("@productId", productId);
            sqlParaProductName = new SqlParameter("@newProductName", productName);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaProductId);
            sqlCmd.Parameters.Add(sqlParaProductName);
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }

        public void ProductDel(string productId)
        {
            #region sqlPara declare
            //productId
            SqlParameter sqlParaProductId = null;
            //productEnd
            SqlParameter sqlParaProductEnd = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_product_delete";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int productIdTemp = int.Parse(productId);
            DateTime et = DateTime.Now;

            sqlParaProductId = new SqlParameter("@delProductId", productIdTemp);
            sqlParaProductEnd = new SqlParameter("@delEndTime", et);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaProductId);
            sqlCmd.Parameters.Add(sqlParaProductEnd);
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
                "FROM tbl_product ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;


            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "tbl_product");

            return myDataSet;
        }
    }
}