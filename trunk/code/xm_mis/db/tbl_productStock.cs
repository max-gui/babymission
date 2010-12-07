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
    public class tbl_productStock_Old : DataBase
    {
        public tbl_productStock_Old()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string SelectAdd(DataSet dataSet, ref string error)
        {
            #region sqlPara declare
            //productId
            SqlParameter sqlParaProductId = null;
            //productTag
            SqlParameter sqlParaProductTag = null;
            //supplierId
            SqlParameter sqlParaSupplierId = null;
            //usrId
            SqlParameter sqlParaUsrId = null;
            //productStockId
            SqlParameter sqlParaProductStockId = null;
            //error
            SqlParameter sqlParaError = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "product_In";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            string pId = dataSet.Tables["addTable"].Rows[0]["productId"].ToString().Trim();
            string pTag = dataSet.Tables["addTable"].Rows[0]["productTag"].ToString().Trim();
            string pSId = dataSet.Tables["addTable"].Rows[0]["supplierId"].ToString().Trim();
            string usrId = dataSet.Tables["addTable"].Rows[0]["usrId"].ToString().Trim();
            error = string.Empty;

            sqlParaProductId = new SqlParameter("@productId", pId);
            sqlParaProductTag = new SqlParameter("@productTag", pTag);
            sqlParaSupplierId = new SqlParameter("@supplierId", pSId);
            sqlParaUsrId = new SqlParameter("@usrId", usrId);
            sqlParaProductStockId = new SqlParameter("@productStockId", SqlDbType.Int);
            sqlParaError = new SqlParameter("@error", SqlDbType.NVarChar, 50);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaProductId);
            sqlCmd.Parameters.Add(sqlParaProductTag);
            sqlCmd.Parameters.Add(sqlParaSupplierId);
            sqlCmd.Parameters.Add(sqlParaUsrId);
            sqlCmd.Parameters.Add(sqlParaProductStockId);
            sqlCmd.Parameters.Add(sqlParaError);
            #endregion

            #region sqlDirection
            sqlParaProductStockId.Direction = ParameterDirection.Output;
            sqlParaError.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string productStockId = sqlParaProductStockId.Value.ToString();
            error = sqlParaError.Value.ToString();

            return productStockId;
        }

        //public void ProductUpdate(int productId, string productName)
        //{
        //    #region sqlPara declare
        //    //productId
        //    SqlParameter sqlParaProductId = null;
        //    //productName
        //    SqlParameter sqlParaProductName = null;
        //    #endregion

        //    SqlCommand sqlCmd = null;

        //    string strSQL = "tbl_product_update";

        //    sqlCmd = this.SqlCom;
        //    sqlCmd.CommandText = strSQL;
        //    sqlCmd.CommandType = CommandType.StoredProcedure;

        //    #region sqlParaInit

        //    sqlParaProductId = new SqlParameter("@productId", productId);
        //    sqlParaProductName = new SqlParameter("@newProductName", productName);
        //    #endregion

        //    #region sqlParaAdd
        //    sqlCmd.Parameters.Clear();
        //    sqlCmd.Parameters.Add(sqlParaProductId);
        //    sqlCmd.Parameters.Add(sqlParaProductName);
        //    #endregion

        //    sqlCmd.Connection.Open();

        //    sqlCmd.ExecuteNonQuery();

        //    sqlCmd.Connection.Close();
        //}

        //public void ProductDel(string productId)
        //{
        //    #region sqlPara declare
        //    //productId
        //    SqlParameter sqlParaProductId = null;
        //    //productEnd
        //    SqlParameter sqlParaProductEnd = null;
        //    #endregion

        //    SqlCommand sqlCmd = null;

        //    string strSQL = "tbl_product_delete";

        //    sqlCmd = this.SqlCom;
        //    sqlCmd.CommandText = strSQL;
        //    sqlCmd.CommandType = CommandType.StoredProcedure;

        //    #region sqlParaInit
        //    int productIdTemp = int.Parse(productId);
        //    DateTime et = DateTime.Now;

        //    sqlParaProductId = new SqlParameter("@delProductId", productIdTemp);
        //    sqlParaProductEnd = new SqlParameter("@delEndTime", et);
        //    #endregion

        //    #region sqlParaAdd
        //    sqlCmd.Parameters.Clear();
        //    sqlCmd.Parameters.Add(sqlParaProductId);
        //    sqlCmd.Parameters.Add(sqlParaProductEnd);
        //    #endregion

        //    sqlCmd.Connection.Open();

        //    sqlCmd.ExecuteNonQuery();

        //    sqlCmd.Connection.Close();
        //}

        public DataSet RealProductStockCheckManView()
        {
            SqlCommand sqlCmd = null;

            string strSQL =
                "SELECT " +
                "* " +
                "FROM view_productStockCheckMan ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;

            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "view_productStockCheckMan");

            return myDataSet;
        }

        //public DataSet RealProductStockCheckView()
        //{
        //    SqlCommand sqlCmd = null;

        //    string strSQL =
        //        "SELECT " +
        //        "* " +
        //        "FROM view_productStockCheck ";

        //    sqlCmd = this.SqlCom;
        //    sqlCmd.CommandText = strSQL;
        //    sqlCmd.CommandType = CommandType.Text;

        //    SqlDataAdapter userDataAdapter = this.SqlDA;
        //    SqlDA.SelectCommand = sqlCmd;

        //    DataSet myDataSet = new DataSet();
        //    userDataAdapter.Fill(myDataSet, "view_productStockCheck");

        //    return myDataSet;
        //}

        public void ProductInCheck(string productInCheckId, string check, byte[] checkText, string checkTextName, string contentType)
        {
            #region sqlPara declare
            //productInCheckId
            SqlParameter sqlParaProductInCheckId = null;
            //check
            SqlParameter sqlParaCheck = null;
            //checkText
            SqlParameter sqlParaCheckText = null;
            //checkTextName
            SqlParameter sqlParaCheckTextName = null;
            //contentType
            SqlParameter sqlParaContentType = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "productIn_Check";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int productInCheckIdTemp = int.Parse(productInCheckId);

            sqlParaProductInCheckId = new SqlParameter("@productInCheckId", productInCheckIdTemp);
            sqlParaCheck = new SqlParameter("@productCheck", check);
            sqlParaCheckText = new SqlParameter("@productCheckText", checkText);
            sqlParaCheckTextName = new SqlParameter("@checkTextName", checkTextName);
            sqlParaContentType = new SqlParameter("@contentType", contentType);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaProductInCheckId);
            sqlCmd.Parameters.Add(sqlParaCheck);
            sqlCmd.Parameters.Add(sqlParaCheckText);
            sqlCmd.Parameters.Add(sqlParaCheckTextName);
            sqlCmd.Parameters.Add(sqlParaContentType);
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
                "FROM tbl_productStock ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;

            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "tbl_productStock");

            return myDataSet;
        }
    }
}