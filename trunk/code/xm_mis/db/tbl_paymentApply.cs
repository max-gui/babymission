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
    public class tbl_paymentApply : DataBase
    {
        public tbl_paymentApply()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string MainContractPayMax(string mainContractId)
        {
            #region sqlPara declare
            //mainContractId
            SqlParameter sqlParaMainContractId = null;
            //now
            SqlParameter sqlParaNow = null;
            //payPercent
            SqlParameter sqlParaPayPercent = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "mainContract_MaxPay";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int mcId = int.Parse(mainContractId);
            DateTime st = DateTime.Now;

            sqlParaMainContractId = new SqlParameter("@mainContractId", mcId);
            sqlParaNow = new SqlParameter("@now", st);
            sqlParaPayPercent = new SqlParameter("@payPercent", SqlDbType.Int);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaMainContractId);
            sqlCmd.Parameters.Add(sqlParaNow);
            sqlCmd.Parameters.Add(sqlParaPayPercent);
            #endregion

            #region sqlDirection
            sqlParaPayPercent.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string maxPay = sqlParaPayPercent.Value.ToString();
            return maxPay;
        }

        public void SelfPaymentExamine(string payId, string isAccept, string paymentComment)
        {
            #region sqlPara declare
            //paymentId
            SqlParameter sqlParaPaymentId = null;
            //isAccept
            SqlParameter sqlParaIsAccept = null;
            //paymentComment
            SqlParameter sqlParaPaymentComment = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "selfPayment_Examine";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int pId = int.Parse(payId);

            sqlParaPaymentId = new SqlParameter("@paymentId", pId);
            sqlParaIsAccept = new SqlParameter("@isAccept", isAccept);
            sqlParaPaymentComment = new SqlParameter("@paymentComment", paymentComment);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaPaymentId);
            sqlCmd.Parameters.Add(sqlParaIsAccept);
            sqlCmd.Parameters.Add(sqlParaPaymentComment);
            #endregion

            //#region sqlDirection
            //sqlParaReceiptComment.Direction = ParameterDirection.Output;
            //#endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }

        public string SelectAdd(DataSet dataSet)
        {
            #region sqlPara declare
            //subContractId
            SqlParameter sqlParaSubContractId = null;
            //custMaxPay
            SqlParameter sqlParaCustMaxPay = null;
            //payPercent
            SqlParameter sqlParaPayPercent = null;
            //paymentExplication
            SqlParameter sqlParaPaymentExplication = null;
            //startTime
            SqlParameter sqlParaStartTime = null;
            //Identity
            SqlParameter sqlParaId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_paymentApply_Insert";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            string subContractId = dataSet.Tables["tbl_paymentApply"].Rows[0]["subContractId"].ToString();
            string custMaxPay = dataSet.Tables["tbl_paymentApply"].Rows[0]["custMaxPay"].ToString();
            string payPercent = dataSet.Tables["tbl_paymentApply"].Rows[0]["payPercent"].ToString();
            string paymentExplication = dataSet.Tables["tbl_paymentApply"].Rows[0]["paymentExplication"].ToString();
            DateTime st = DateTime.Now;

            sqlParaSubContractId = new SqlParameter("@subContractId", subContractId);
            sqlParaCustMaxPay = new SqlParameter("@custMaxPay", custMaxPay);
            sqlParaPayPercent = new SqlParameter("@payPercent", payPercent);
            sqlParaPaymentExplication = new SqlParameter("@paymentExplication", paymentExplication);
            sqlParaStartTime = new SqlParameter("@startTime", st);
            sqlParaId = new SqlParameter("@Identity", SqlDbType.Int);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaSubContractId);
            sqlCmd.Parameters.Add(sqlParaCustMaxPay);
            sqlCmd.Parameters.Add(sqlParaPayPercent);
            sqlCmd.Parameters.Add(sqlParaPaymentExplication);
            sqlCmd.Parameters.Add(sqlParaStartTime);
            sqlCmd.Parameters.Add(sqlParaId);
            #endregion

            #region sqlDirection
            sqlParaId.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string payId = sqlParaId.Value.ToString();
            return payId;
        }

        //public void MainContractPayPercentUpdate(int mainContractId, string payPercent)
        //{
        //    #region sqlPara declare
        //    //mainContractId
        //    SqlParameter sqlParaMainContractId = null;
        //    //selfReceivingPercent
        //    SqlParameter sqlParaSelfReceivingPercent = null;
        //    #endregion

        //    SqlCommand sqlCmd = null;

        //    string strSQL = "mainContract_payPercent_update";

        //    sqlCmd = this.SqlCom;
        //    sqlCmd.CommandText = strSQL;
        //    sqlCmd.CommandType = CommandType.StoredProcedure;

        //    #region sqlParaInit

        //    sqlParaMainContractId = new SqlParameter("@mainContractId", mainContractId);
        //    sqlParaSelfReceivingPercent = new SqlParameter("@selfReceivingPercent", payPercent);
        //    #endregion

        //    #region sqlParaAdd
        //    sqlCmd.Parameters.Clear();
        //    sqlCmd.Parameters.Add(sqlParaMainContractId);
        //    sqlCmd.Parameters.Add(sqlParaSelfReceivingPercent);
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

        public DataSet SelectView()
        {
            SqlCommand sqlCmd = null;

            string strSQL =
                "SELECT " +
                "* " +
                "FROM tbl_paymentApply ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;


            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "tbl_paymentApply");

            return myDataSet;
        }
    }
}