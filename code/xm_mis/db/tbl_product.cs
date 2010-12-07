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

        public string SelectAdd(DataSet dataSet, ref string error)
        {
            #region sqlPara declare
            //productName
            SqlParameter sqlParaProductName = null;
            //productId
            SqlParameter sqlParaProductId = null;
            //error
            SqlParameter sqlParaError = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "addNewProduct";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            string pn = dataSet.Tables["addTable"].Rows[0]["productName"].ToString().Trim();
            error = string.Empty;

            sqlParaProductName = new SqlParameter("@productName", pn);
            sqlParaProductId = new SqlParameter("@productId", SqlDbType.Int);
            sqlParaError = new SqlParameter("@error", SqlDbType.NVarChar, 50);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaProductName);
            sqlCmd.Parameters.Add(sqlParaProductId);
            sqlCmd.Parameters.Add(sqlParaError);
            #endregion

            #region sqlDirection
            sqlParaProductId.Direction = ParameterDirection.Output;
            sqlParaError.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string productId = sqlParaProductId.Value.ToString();
            error = sqlParaError.Value.ToString();

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
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_product_delete";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int productIdTemp = int.Parse(productId);

            sqlParaProductId = new SqlParameter("@delProductId", productIdTemp);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaProductId);
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