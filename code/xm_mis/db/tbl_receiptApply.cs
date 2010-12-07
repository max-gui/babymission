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
            sqlParaReceiptPercent = new SqlParameter("@receiptPercent", SqlDbType.Real);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaMainContractId);
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

        public void SelfReceiptDone(DataSet dataSet)
        {
            #region sqlPara declare
            //receiptId
            SqlParameter sqlParaReceiptId = null;
            //receiptPercent
            SqlParameter sqlParaReceiptPercent = null;
            //mainContractId
            SqlParameter sqlParaMainContractId = null;
            //projectTagId
            SqlParameter sqlParaProjectTagId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "selfReceipt_done";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            string receiptId = dataSet.Tables["addTable"].Rows[0]["receiptId"].ToString();
            string receiptPercent = dataSet.Tables["addTable"].Rows[0]["receiptPercent"].ToString();
            string mainContractId = dataSet.Tables["addTable"].Rows[0]["mainContractId"].ToString();
            string projectTagId = dataSet.Tables["addTable"].Rows[0]["projectTagId"].ToString();

            sqlParaReceiptId = new SqlParameter("@receiptId", receiptId);
            sqlParaReceiptPercent = new SqlParameter("@receiptPercent", receiptPercent);
            sqlParaMainContractId = new SqlParameter("@mainContractId", mainContractId);
            sqlParaProjectTagId = new SqlParameter("@projectTagId", projectTagId);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaReceiptId);
            sqlCmd.Parameters.Add(sqlParaReceiptPercent);
            sqlCmd.Parameters.Add(sqlParaMainContractId);
            sqlCmd.Parameters.Add(sqlParaProjectTagId);
            #endregion

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

            sqlParaMainContractId = new SqlParameter("@mainContractId", mainContractId);
            sqlParaCustMaxReceipt = new SqlParameter("@custMaxReceipt", custMaxReceipt);
            sqlParaReceiptPercent = new SqlParameter("@receiptPercent", receiptPercent);
            sqlParaReceiptExplication = new SqlParameter("@receiptExplication", receiptExplication);
            sqlParaId = new SqlParameter("@Identity", SqlDbType.Int);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaMainContractId);
            sqlCmd.Parameters.Add(sqlParaCustMaxReceipt);
            sqlCmd.Parameters.Add(sqlParaReceiptPercent);
            sqlCmd.Parameters.Add(sqlParaReceiptExplication);
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
                "FROM view_mainReceipt ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;


            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "view_mainReceipt");

            return myDataSet;
        }
    }
}