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
    public class tbl_mainContract : DataBase
    {
        public tbl_mainContract()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string SelectAdd(DataSet dataSet)
        {
            #region sqlPara declare
            //projectTagId
            SqlParameter sqlParaProjectTagId = null;
            //contractCompName
            SqlParameter sqlParaContractCompName = null;
            //mainContractTag
            SqlParameter sqlParaMainContractTag = null;
            //cash
            SqlParameter sqlParaCash = null;
            //dateLine
            SqlParameter sqlParaDateLine = null;
            //paymentMode
            SqlParameter sqlParaPaymentMode = null;
            //startTime
            SqlParameter sqlParaStartTime = null;
            //Identity
            SqlParameter sqlParaId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_mainContract_Insert";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int proTagId = int.Parse(dataSet.Tables["tbl_mainContract"].Rows[0]["projectTagId"].ToString());
            string contCompName = dataSet.Tables["tbl_mainContract"].Rows[0]["contractCompName"].ToString();
            string MainContTag = dataSet.Tables["tbl_mainContract"].Rows[0]["mainContractTag"].ToString();

            string Cash = dataSet.Tables["tbl_mainContract"].Rows[0]["cash"].ToString();
            DateTime dateLine = DateTime.Parse(dataSet.Tables["tbl_mainContract"].Rows[0]["dateLine"].ToString());
            string paymentMode = dataSet.Tables["tbl_mainContract"].Rows[0]["paymentMode"].ToString();
            DateTime st = DateTime.Now;

            sqlParaProjectTagId = new SqlParameter("@projectTagId", proTagId);
            sqlParaContractCompName = new SqlParameter("@contractCompName", contCompName);
            sqlParaMainContractTag = new SqlParameter("@mainContractTag", MainContTag);
            sqlParaCash = new SqlParameter("@cash", Cash);
            sqlParaDateLine = new SqlParameter("@dateLine", dateLine);
            sqlParaPaymentMode = new SqlParameter("@paymentMode", paymentMode);
            sqlParaStartTime = new SqlParameter("@startTime", st);
            sqlParaId = new SqlParameter("@Identity", SqlDbType.Int);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaProjectTagId);
            sqlCmd.Parameters.Add(sqlParaContractCompName);
            sqlCmd.Parameters.Add(sqlParaMainContractTag);
            sqlCmd.Parameters.Add(sqlParaCash);
            sqlCmd.Parameters.Add(sqlParaDateLine);
            sqlCmd.Parameters.Add(sqlParaPaymentMode);
            sqlCmd.Parameters.Add(sqlParaStartTime);
            sqlCmd.Parameters.Add(sqlParaId);
            #endregion

            #region sqlDirection
            sqlParaId.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string mainContractId = sqlParaId.Value.ToString();
            return mainContractId;
        }

        public void MainContractPayPercentUpdate(int mainContractId, string payPercent)
        {
            #region sqlPara declare
            //mainContractId
            SqlParameter sqlParaMainContractId = null;
            //selfReceivingPercent
            SqlParameter sqlParaSelfReceivingPercent = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "mainContract_payPercent_update";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit

            sqlParaMainContractId = new SqlParameter("@mainContractId", mainContractId);
            sqlParaSelfReceivingPercent = new SqlParameter("@selfReceivingPercent", payPercent);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaMainContractId);
            sqlCmd.Parameters.Add(sqlParaSelfReceivingPercent);
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