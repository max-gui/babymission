using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Globalization;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
namespace xm_mis.db
{
    public class tbl_subContract : DataBase
    {
        public tbl_subContract()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string SelectAdd(DataSet dataSet)
        {
            #region sqlPara declare
            //mainContractId
            SqlParameter sqlParaMainContractId = null;
            //supplierId
            SqlParameter sqlParaSupplierIdName = null;
            //subContractTag
            SqlParameter sqlParaSubContractTag = null;
            //cash
            SqlParameter sqlParaCash = null;
            //dateLine
            SqlParameter sqlParaDateLine = null;
            //paymentMode
            SqlParameter sqlParaPaymentMode = null;
            //startTime
            SqlParameter sqlParaStartTime = null;
            //subContractId
            SqlParameter sqlParaSubContractId = null;
            //contractRelationId
            SqlParameter sqlParaContractRelationId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "subContract_Insert";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int mainContractId = int.Parse(dataSet.Tables["tbl_subContract"].Rows[0]["mainContractId"].ToString());
            string contSupplierId = dataSet.Tables["tbl_subContract"].Rows[0]["supplierId"].ToString();
            string subContTag = dataSet.Tables["tbl_subContract"].Rows[0]["subContractTag"].ToString();

            string cash = dataSet.Tables["tbl_subContract"].Rows[0]["cash"].ToString();
            DateTime dateLine = DateTime.Parse(dataSet.Tables["tbl_subContract"].Rows[0]["dateLine"].ToString());
            string paymentMode = dataSet.Tables["tbl_subContract"].Rows[0]["paymentMode"].ToString();
            DateTime st = DateTime.Now;

            sqlParaMainContractId = new SqlParameter("@mainContractId", mainContractId);
            sqlParaSupplierIdName = new SqlParameter("@supplierId", contSupplierId);
            sqlParaSubContractTag = new SqlParameter("@subContractTag", subContTag);
            sqlParaCash = new SqlParameter("@cash", cash);
            sqlParaDateLine = new SqlParameter("@dateLine", dateLine);
            sqlParaPaymentMode = new SqlParameter("@paymentMode", paymentMode);
            sqlParaStartTime = new SqlParameter("@startTime", st);
            sqlParaSubContractId = new SqlParameter("@subContractId", SqlDbType.Int);
            sqlParaContractRelationId = new SqlParameter("@contractRelationId", SqlDbType.Int);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaMainContractId);
            sqlCmd.Parameters.Add(sqlParaSupplierIdName);
            sqlCmd.Parameters.Add(sqlParaSubContractTag);
            sqlCmd.Parameters.Add(sqlParaCash);
            sqlCmd.Parameters.Add(sqlParaDateLine);
            sqlCmd.Parameters.Add(sqlParaPaymentMode);
            sqlCmd.Parameters.Add(sqlParaStartTime);
            sqlCmd.Parameters.Add(sqlParaSubContractId);
            sqlCmd.Parameters.Add(sqlParaContractRelationId);
            #endregion

            #region sqlDirection
            sqlParaSubContractId.Direction = ParameterDirection.Output;
            sqlParaContractRelationId.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string subContractId = sqlParaSubContractId.Value.ToString();
            string contractRelationId = sqlParaContractRelationId.Value.ToString();

            return subContractId;
        }

        public void SubContractReceiptPercentUpdate(int subContractId, string receiptPercent)
        {
            #region sqlPara declare
            //subContractId
            SqlParameter sqlParaSubContractId = null;
            //receiptPercent
            SqlParameter sqlParaReceiptPercent = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "subContract_receiptPercent_update";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit

            sqlParaSubContractId = new SqlParameter("@subContractId", subContractId);
            sqlParaReceiptPercent = new SqlParameter("@receiptPercent", receiptPercent);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaSubContractId);
            sqlCmd.Parameters.Add(sqlParaReceiptPercent);
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
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

        //public DataSet SelectView()
        //{
        //    SqlCommand sqlCmd = null;

        //    string strSQL =
        //        "SELECT " +
        //        "* " +
        //        "FROM tbl_product ";

        //    sqlCmd = this.SqlCom;
        //    sqlCmd.CommandText = strSQL;
        //    sqlCmd.CommandType = CommandType.Text;


        //    SqlDataAdapter userDataAdapter = this.SqlDA;
        //    SqlDA.SelectCommand = sqlCmd;

        //    DataSet myDataSet = new DataSet();
        //    userDataAdapter.Fill(myDataSet, "tbl_product");

        //    return myDataSet;
        //}
    }
}