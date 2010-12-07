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
    public class tbl_mainContrctProduct : DataBase
    {
        public tbl_mainContrctProduct()
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
            //productId
            SqlParameter sqlParaProductId = null;
            //productNum
            SqlParameter sqlParapProductNum = null;
            //Identity
            SqlParameter sqlParaId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_mainContrctProduct_Insert";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit

            int mainContractId = 0;
            string productId = string.Empty;
            string productNum = string.Empty;

            sqlParaMainContractId = new SqlParameter("@mainContractId", mainContractId);
            sqlParaProductId = new SqlParameter("@productId", productId);
            sqlParapProductNum = new SqlParameter("@productNum", productNum);
            sqlParaId = new SqlParameter("@Identity", SqlDbType.Int);
            foreach (DataRow dr in dataSet.Tables["tbl_mainContrctProduct"].Rows)
            {
                mainContractId = int.Parse(dr["mainContractId"].ToString());
                productId = dr["productId"].ToString();
                productNum = dr["productNum"].ToString();

                sqlParaMainContractId.Value = mainContractId;
                sqlParaProductId.Value = productId;
                sqlParapProductNum.Value = productNum;
                #endregion

                #region sqlParaAdd
                sqlCmd.Parameters.Clear();
                sqlCmd.Parameters.Add(sqlParaMainContractId);
                sqlCmd.Parameters.Add(sqlParaProductId);
                sqlCmd.Parameters.Add(sqlParapProductNum);
                sqlCmd.Parameters.Add(sqlParaId);
                #endregion

                #region sqlDirection
                sqlParaId.Direction = ParameterDirection.Output;
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
                "FROM view_mainContractProduct ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;


            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "view_mainContractProduct");

            return myDataSet;
        }
    }
}