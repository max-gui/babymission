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
    public class tbl_receiptApply : DataBase
    {
        public tbl_receiptApply()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string SubContractReceiptMax(string mainContractId)
        {
            #region sqlPara declare
            //mainContractId
            SqlParameter sqlParaMainContractId = null;
            //now
            SqlParameter sqlParaNow = null;
            //receiptPercent
            SqlParameter sqlParaReceiptPercent = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "subContract_MaxReceipt";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int mcId = int.Parse(mainContractId);
            DateTime st = DateTime.Now;

            sqlParaMainContractId = new SqlParameter("@mainContractId", mcId);
            sqlParaNow = new SqlParameter("@now", st);
            sqlParaReceiptPercent = new SqlParameter("@receiptPercent", SqlDbType.Int);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaMainContractId);
            sqlCmd.Parameters.Add(sqlParaNow);
            sqlCmd.Parameters.Add(sqlParaReceiptPercent);
            #endregion

            #region sqlDirection
            sqlParaReceiptPercent.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string maxReceipt = sqlParaReceiptPercent.Value.ToString();
            return maxReceipt;
        }

        public void SelfReceiptExamine(string receiptId, string isAccept, string receiptComment)
        {
            #region sqlPara declare
            //receiptId
            SqlParameter sqlParaReceiptId = null;
            //isAccept
            SqlParameter sqlParaIsAccept = null;
            //receiptComment
            SqlParameter sqlParaReceiptComment = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "selfReceipt_Examine";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int rId = int.Parse(receiptId);

            sqlParaReceiptId = new SqlParameter("@receiptId", rId);
            sqlParaIsAccept = new SqlParameter("@isAccept", isAccept);
            sqlParaReceiptComment = new SqlParameter("@receiptComment", receiptComment);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaReceiptId);
            sqlCmd.Parameters.Add(sqlParaIsAccept);
            sqlCmd.Parameters.Add(sqlParaReceiptComment);
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
            //mainContractId
            SqlParameter sqlParaMainContractId = null;
            //custMaxReceipt
            SqlParameter sqlParaCustMaxReceipt = null;
            //receiptPercent
            SqlParameter sqlParaReceiptPercent = null;
            //receiptExplication
            SqlParameter sqlParaReceiptExplication = null;
            //startTime
            SqlParameter sqlParaStartTime = null;
            //Identity
            SqlParameter sqlParaId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_receiptApply_Insert";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            string mainContractId = dataSet.Tables["tbl_receiptApply"].Rows[0]["mainContractId"].ToString();
            string custMaxReceipt = dataSet.Tables["tbl_receiptApply"].Rows[0]["custMaxReceipt"].ToString();
            string receiptPercent = dataSet.Tables["tbl_receiptApply"].Rows[0]["receiptPercent"].ToString();
            string receiptExplication = dataSet.Tables["tbl_receiptApply"].Rows[0]["receiptExplication"].ToString();
            DateTime st = DateTime.Now;

            sqlParaMainContractId = new SqlParameter("@mainContractId", mainContractId);
            sqlParaCustMaxReceipt = new SqlParameter("@custMaxReceipt", custMaxReceipt);
            sqlParaReceiptPercent = new SqlParameter("@receiptPercent", receiptPercent);
            sqlParaReceiptExplication = new SqlParameter("@receiptExplication", receiptExplication);
            sqlParaStartTime = new SqlParameter("@startTime", st);
            sqlParaId = new SqlParameter("@Identity", SqlDbType.Int);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaMainContractId);
            sqlCmd.Parameters.Add(sqlParaCustMaxReceipt);
            sqlCmd.Parameters.Add(sqlParaReceiptPercent);
            sqlCmd.Parameters.Add(sqlParaReceiptExplication);
            sqlCmd.Parameters.Add(sqlParaStartTime);
            sqlCmd.Parameters.Add(sqlParaId);
            #endregion

            #region sqlDirection
            sqlParaId.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string receiptId = sqlParaId.Value.ToString();
            return receiptId;
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
                "FROM tbl_receiptApply ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;


            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "tbl_receiptApply");

            return myDataSet;
        }
    }
}