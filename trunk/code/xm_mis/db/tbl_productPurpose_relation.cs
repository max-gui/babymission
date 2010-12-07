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
    public class tbl_productPurpose_relation_Old : DataBase
    {
        public tbl_productPurpose_relation_Old()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string SelectAdd(DataSet dataSet)
        {
            #region sqlPara declare
            //productStockId
            SqlParameter sqlParaProductStockId = null;
            //productPurposeId
            SqlParameter sqlParaProductPurposeId = null;
            //Identity
            SqlParameter sqlParaIdentity = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_productPurpose_relation_Insert";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            string productStockId = dataSet.Tables["productStockGVTable"].Rows[0]["productStockId"].ToString().Trim();
            string productPurposeId = dataSet.Tables["productStockGVTable"].Rows[0]["productPurposeId"].ToString().Trim();

            sqlParaProductStockId = new SqlParameter("@productStockId", productStockId);
            sqlParaProductPurposeId = new SqlParameter("@productPurposeId", productPurposeId);
            sqlParaIdentity = new SqlParameter("@Identity", SqlDbType.Int);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaProductStockId);
            sqlCmd.Parameters.Add(sqlParaProductPurposeId);
            sqlCmd.Parameters.Add(sqlParaIdentity);
            #endregion

            #region sqlDirection
            sqlParaIdentity.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string productPurposeRelationId = sqlParaIdentity.Value.ToString();
            return productPurposeRelationId;
        }

        public void ProductPurposeRelationIdUpdata(string productPurposeRelationId, string productPurposeId)
        {
            #region sqlPara declare
            //productPurposeRelationId
            SqlParameter sqlParaProductPurposeRelationId = null;
            //productPurposeId
            SqlParameter sqlParaProductPurposeId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_productPurpose_relation_update";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit

            sqlParaProductPurposeRelationId = new SqlParameter("@productPurposeRelationId", productPurposeRelationId);
            sqlParaProductPurposeId = new SqlParameter("@productPurposeId", productPurposeId);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaProductPurposeRelationId);
            sqlCmd.Parameters.Add(sqlParaProductPurposeId);
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }

        public void ProductPurposeEmpty(string productPurposeRelationId)
        {
            #region sqlPara declare
            //productPurposeRelationId
            SqlParameter sqlParaProductPurposeRelationId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_productPurpose_relation_delete";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit

            sqlParaProductPurposeRelationId = new SqlParameter("@productPurposeRelationId", productPurposeRelationId);

            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaProductPurposeRelationId);
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }

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

        //public DataSet RealProductStockCheckManView()
        //{
        //    SqlCommand sqlCmd = null;

        //    string strSQL =
        //        "SELECT " +
        //        "* " +
        //        "FROM view_productStockCheckMan ";

        //    sqlCmd = this.SqlCom;
        //    sqlCmd.CommandText = strSQL;
        //    sqlCmd.CommandType = CommandType.Text;

        //    SqlDataAdapter userDataAdapter = this.SqlDA;
        //    SqlDA.SelectCommand = sqlCmd;

        //    DataSet myDataSet = new DataSet();
        //    userDataAdapter.Fill(myDataSet, "view_productStockCheckMan");

        //    return myDataSet;
        //}

        public DataSet RealProductPurposeRelationView()
        {
            SqlCommand sqlCmd = null;

            string strSQL =
                "SELECT " +
                "* " +
                "FROM view_productStockRelation ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;

            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "view_productStockRelation");

            return myDataSet;
        }

        public DataSet RealProductPurposeView()
        {
            SqlCommand sqlCmd = null;

            string strSQL =
                "SELECT " +
                "* " +
                "FROM tbl_productPurpose ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;

            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "tbl_productPurpose");

            return myDataSet;
        }

        public DataSet AllProductPurposeRelationView()
        {           
            SqlCommand sqlCmd = null;

            string strSQL = "productStockRelation_view";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            #endregion

            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "productStockRelation_view");

            return myDataSet;
        }

        //public void ProductInCheck(string productInId, string check, byte[] checkText, string checkTextName, string contentType)
        //{
        //    #region sqlPara declare
        //    //productInId
        //    SqlParameter sqlParaProductInId = null;
        //    //check
        //    SqlParameter sqlParaCheck = null;
        //    //checkText
        //    SqlParameter sqlParaCheckText = null;
        //    //checkTextName
        //    SqlParameter sqlParaCheckTextName = null;
        //    //contentType
        //    SqlParameter sqlParaContentType = null;
        //    #endregion

        //    SqlCommand sqlCmd = null;

        //    string strSQL = "productIn_Check";

        //    sqlCmd = this.SqlCom;
        //    sqlCmd.CommandText = strSQL;
        //    sqlCmd.CommandType = CommandType.StoredProcedure;

        //    #region sqlParaInit
        //    int productInIdTemp = int.Parse(productInId);

        //    sqlParaProductInId = new SqlParameter("@productInId", productInIdTemp);
        //    sqlParaCheck = new SqlParameter("@productCheck", check);
        //    sqlParaCheckText = new SqlParameter("@productCheckText", checkText);
        //    sqlParaCheckTextName = new SqlParameter("@checkTextName", checkTextName);
        //    sqlParaContentType = new SqlParameter("@contentType", contentType);
        //    #endregion

        //    #region sqlParaAdd
        //    sqlCmd.Parameters.Clear();
        //    sqlCmd.Parameters.Add(sqlParaProductInId);
        //    sqlCmd.Parameters.Add(sqlParaCheck);
        //    sqlCmd.Parameters.Add(sqlParaCheckText);
        //    sqlCmd.Parameters.Add(sqlParaCheckTextName);
        //    sqlCmd.Parameters.Add(sqlParaContentType);
        //    #endregion

        //    sqlCmd.Connection.Open();

        //    sqlCmd.ExecuteNonQuery();

        //    sqlCmd.Connection.Close();
        //}

        //public DataSet SelectView()
        //{
        //    SqlCommand sqlCmd = null;

        //    string strSQL =
        //        "SELECT " +
        //        "* " +
        //        "FROM tbl_productStock ";

        //    sqlCmd = this.SqlCom;
        //    sqlCmd.CommandText = strSQL;
        //    sqlCmd.CommandType = CommandType.Text;

        //    SqlDataAdapter userDataAdapter = this.SqlDA;
        //    SqlDA.SelectCommand = sqlCmd;

        //    DataSet myDataSet = new DataSet();
        //    userDataAdapter.Fill(myDataSet, "tbl_productStock");

        //    return myDataSet;
        //}
    }
}