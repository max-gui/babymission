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
    public class tbl_subContrctProduct : DataBase
    {
        public tbl_subContrctProduct()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string SelectAdd(DataSet dataSet)
        {
            #region sqlPara declare
            //subContractId
            SqlParameter sqlParaSubContractId = null;
            //productId
            SqlParameter sqlParaProductId = null;
            //productNum
            SqlParameter sqlParapProductNum = null;
            //mainContractProductId
            SqlParameter sqlParapMainContractProductId = null;
            //startTime
            SqlParameter sqlParaStartTime = null;
            //subContrctProductId
            SqlParameter sqlParaSubContrctProductId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "subContractProduct_Insert";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit

            int subContractId = 0;
            string productId = string.Empty;
            string productNum = string.Empty;
            string mainContractProductId = string.Empty;
            DateTime st = DateTime.Now;

            sqlParaSubContractId = new SqlParameter("@subContractId", subContractId);
            sqlParaProductId = new SqlParameter("@productId", productId);
            sqlParapProductNum = new SqlParameter("@productNum", productNum);
            sqlParapMainContractProductId = new SqlParameter("@mainContractProductId", mainContractProductId);
            sqlParaStartTime = new SqlParameter("@startTime", st);
            sqlParaSubContrctProductId = new SqlParameter("@subContrctProductId", SqlDbType.Int);
            foreach (DataRow dr in dataSet.Tables["tbl_subContrctProduct"].Rows)
            {
                subContractId = int.Parse(dr["subContractId"].ToString());
                productId = dr["productId"].ToString();
                productNum = dr["productNum"].ToString();
                mainContractProductId = dr["mainContractProductId"].ToString();

                sqlParaSubContractId.Value = subContractId;
                sqlParaProductId.Value = productId;
                sqlParapProductNum.Value = productNum;
                sqlParapMainContractProductId.Value = mainContractProductId;
            #endregion

                #region sqlParaAdd
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.Add(sqlParaSubContractId);
                sqlCmd.Parameters.Add(sqlParaProductId);
                sqlCmd.Parameters.Add(sqlParapProductNum);
                sqlCmd.Parameters.Add(sqlParapMainContractProductId);
                sqlCmd.Parameters.Add(sqlParaStartTime);
                sqlCmd.Parameters.Add(sqlParaSubContrctProductId);
                #endregion

                #region sqlDirection
                sqlParaSubContrctProductId.Direction = ParameterDirection.Output;
                #endregion

                sqlCmd.Connection.Open();

                sqlCmd.ExecuteNonQuery();

                sqlCmd.Connection.Close();
            }

            return "not over yet!";
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

        public DataSet SelectView()
        {
            SqlCommand sqlCmd = null;

            string strSQL =
                "SELECT " +
                "* " +
                "FROM view_subContractProduct ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;


            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "view_subContractProduct");

            return myDataSet;
        }
    }
}