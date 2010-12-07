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
    public class tbl_returnedProduct : DataBase
    {
        public tbl_returnedProduct()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        //public string CompProjectCount(DateTime dateCount, string custCompId)
        //{
        //    #region sqlPara declare
        //    //custCompId
        //    SqlParameter sqlParaCustCompId = null;
        //    //dateCount
        //    SqlParameter sqlParaDateCount = null;
        //    //projectCount
        //    SqlParameter sqlParaProjectCount = null;
        //    #endregion

        //    SqlCommand sqlCmd = null;

        //    string strSQL = "view_project_tag_TagInitCount";

        //    sqlCmd = this.SqlCom;
        //    sqlCmd.CommandText = strSQL;
        //    sqlCmd.CommandType = CommandType.StoredProcedure;

        //    #region sqlParaInit
        //    int ccId = int.Parse(custCompId);
        //    DateTime st = DateTime.Now;

        //    sqlParaCustCompId = new SqlParameter("@custCompyId", ccId);
        //    sqlParaDateCount = new SqlParameter("@now", dateCount);
        //    sqlParaProjectCount = new SqlParameter("@countRtn", SqlDbType.Int);
        //    #endregion

        //    #region sqlParaAdd
        //    sqlCmd.Parameters.Clear();
        //    sqlCmd.Parameters.Add(sqlParaCustCompId);
        //    sqlCmd.Parameters.Add(sqlParaDateCount);
        //    sqlCmd.Parameters.Add(sqlParaProjectCount);
        //    #endregion

        //    #region sqlDirection
        //    sqlParaProjectCount.Direction = ParameterDirection.Output;
        //    #endregion

        //    sqlCmd.Connection.Open();

        //    sqlCmd.ExecuteNonQuery();

        //    sqlCmd.Connection.Close();

        //    string proCount = sqlParaProjectCount.Value.ToString();
        //    return proCount;
        //}

        //public string SelectAdd(DataSet dataSet)
        //{
        //    #region sqlPara declare
        //    //productPurposeRelationId
        //    SqlParameter sqlParaProductPurposeRelationId = null;
        //    //usrId
        //    SqlParameter sqlParaUsrId = null;
        //    //returnedSynopsis
        //    SqlParameter sqlParaReturnedSynopsis = null;
        //    //returnedTag
        //    SqlParameter sqlParaReturnedTag = null;
        //    //projectDetail
        //    SqlParameter sqlParaProjectDetail = null;
        //    //productStockId
        //    SqlParameter sqlParaProductStockId = null;
        //    //custManId
        //    SqlParameter sqlParaManId = null;
        //    //startTime
        //    SqlParameter sqlParaSt = null;
        //    //returnedProductId
        //    SqlParameter sqlParaReturnedProductId = null;
        //    #endregion

        //    SqlCommand sqlCmd = null;

        //    string strSQL = "productToReturn";

        //    sqlCmd = this.SqlCom;
        //    sqlCmd.CommandText = strSQL;
        //    sqlCmd.CommandType = CommandType.StoredProcedure;

        //    #region sqlParaInit
        //    string productPurposeRelationId = dataSet.Tables["addTable"].Rows[0]["productPurposeRelationId"].ToString().Trim();
        //    int usrId = int.Parse(dataSet.Tables["addTable"].Rows[0]["usrId"].ToString().Trim());
        //    string returnedSynopsis = dataSet.Tables["addTable"].Rows[0]["returnedSynopsis"].ToString().Trim();
        //    string returnedTag = dataSet.Tables["addTable"].Rows[0]["returnedTag"].ToString().Trim();
        //    string projectDetail = "return";
        //    string productStockId = dataSet.Tables["addTable"].Rows[0]["productStockId"].ToString().Trim();
        //    string custManId = dataSet.Tables["addTable"].Rows[0]["custManId"].ToString().Trim();
        //    DateTime startTime = DateTime.Now;

        //    sqlParaProductPurposeRelationId = new SqlParameter("@productPurposeRelationId", productPurposeRelationId);
        //    sqlParaUsrId = new SqlParameter("@usrId", usrId);
        //    sqlParaReturnedSynopsis = new SqlParameter("@returnedSynopsis", returnedSynopsis);
        //    sqlParaReturnedTag = new SqlParameter("@returnedTag", returnedTag);
        //    sqlParaProjectDetail = new SqlParameter("@projectDetail", projectDetail);
        //    sqlParaProductStockId = new SqlParameter("@productStockId", productStockId);
        //    sqlParaManId = new SqlParameter("@custManId", custManId);
        //    sqlParaSt = new SqlParameter("@startTime", startTime);
        //    sqlParaReturnedProductId = new SqlParameter("@returnedProductId", SqlDbType.Int);
        //    #endregion

        //    #region sqlParaAdd
        //    sqlCmd.Parameters.Clear();
        //    sqlCmd.Parameters.Add(sqlParaProductPurposeRelationId);
        //    sqlCmd.Parameters.Add(sqlParaUsrId);
        //    sqlCmd.Parameters.Add(sqlParaReturnedSynopsis);
        //    sqlCmd.Parameters.Add(sqlParaReturnedTag);
        //    sqlCmd.Parameters.Add(sqlParaProjectDetail);
        //    sqlCmd.Parameters.Add(sqlParaProductStockId);
        //    sqlCmd.Parameters.Add(sqlParaManId);
        //    sqlCmd.Parameters.Add(sqlParaSt);
        //    sqlCmd.Parameters.Add(sqlParaReturnedProductId);
        //    #endregion

        //    #region sqlDirection
        //    sqlParaReturnedProductId.Direction = ParameterDirection.Output;
        //    #endregion

        //    sqlCmd.Connection.Open();

        //    sqlCmd.ExecuteNonQuery();

        //    sqlCmd.Connection.Close();

        //    string returnedProductId = sqlParaReturnedProductId.Value.ToString();

        //    return returnedProductId;
        //}

        public string SelectAdd(DataSet dataSet)
        {
            #region sqlPara declare
            //productPurposeRelationId
            SqlParameter sqlParaProductPurposeRelationId = null;
            //applyUsrId
            SqlParameter sqlParaApplyUsrId = null;
            //approveUsrId
            SqlParameter sqlParaApproveUsrId = null;
            //returnedSynopsis
            SqlParameter sqlParaReturnedSynopsis = null;
            //returnedTag
            SqlParameter sqlParaReturnedTag = null;
            //projectDetail
            SqlParameter sqlParaProjectDetail = null;
            //productStockId
            SqlParameter sqlParaProductStockId = null;
            //custManId
            SqlParameter sqlParaManId = null;
            //returnedProductId
            SqlParameter sqlParaReturnedProductId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "productToReturn";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            string productPurposeRelationId = dataSet.Tables["addTable"].Rows[0]["productPurposeRelationId"].ToString().Trim();
            int applyUsrId = int.Parse(dataSet.Tables["addTable"].Rows[0]["applyUsrId"].ToString().Trim());
            int approveUsrId = int.Parse(dataSet.Tables["addTable"].Rows[0]["approveUsrId"].ToString().Trim());
            string returnedSynopsis = dataSet.Tables["addTable"].Rows[0]["returnedSynopsis"].ToString().Trim();
            string returnedTag = dataSet.Tables["addTable"].Rows[0]["returnedTag"].ToString().Trim();
            string projectDetail = "return";
            string productStockId = dataSet.Tables["addTable"].Rows[0]["productStockId"].ToString().Trim();
            string custManId = dataSet.Tables["addTable"].Rows[0]["custManId"].ToString().Trim();

            sqlParaProductPurposeRelationId = new SqlParameter("@productPurposeRelationId", productPurposeRelationId);
            sqlParaApplyUsrId = new SqlParameter("@applyUsrId", applyUsrId);
            sqlParaApproveUsrId = new SqlParameter("@approveUsrId", approveUsrId);
            sqlParaReturnedSynopsis = new SqlParameter("@returnedSynopsis", returnedSynopsis);
            sqlParaReturnedTag = new SqlParameter("@returnedTag", returnedTag);
            sqlParaProjectDetail = new SqlParameter("@projectDetail", projectDetail);
            sqlParaProductStockId = new SqlParameter("@productStockId", productStockId);
            sqlParaManId = new SqlParameter("@custManId", custManId);
            sqlParaReturnedProductId = new SqlParameter("@returnedProductId", SqlDbType.Int);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaProductPurposeRelationId);
            sqlCmd.Parameters.Add(sqlParaApplyUsrId);
            sqlCmd.Parameters.Add(sqlParaApproveUsrId);
            sqlCmd.Parameters.Add(sqlParaReturnedSynopsis);
            sqlCmd.Parameters.Add(sqlParaReturnedTag);
            sqlCmd.Parameters.Add(sqlParaProjectDetail);
            sqlCmd.Parameters.Add(sqlParaProductStockId);
            sqlCmd.Parameters.Add(sqlParaManId);
            sqlCmd.Parameters.Add(sqlParaReturnedProductId);
            #endregion

            #region sqlDirection
            sqlParaReturnedProductId.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string returnedProductId = sqlParaReturnedProductId.Value.ToString();

            return returnedProductId;
        }
        //public void CustCompUpdate(int custCompId, string custCompName, string custCompAddr, string custCompTag)
        //{
        //    #region sqlPara declare
        //    //custCompId
        //    SqlParameter sqlParaCustCompId = null;
        //    //custCompName
        //    SqlParameter sqlParaCustCompName = null;
        //    //custCompName
        //    SqlParameter sqlParaCustCompAddr = null;
        //    //custCompName
        //    SqlParameter sqlParaCustCompTag = null;
        //    #endregion

        //    SqlCommand sqlCmd = null;

        //    string strSQL = "tbl_customer_company_update";

        //    sqlCmd = this.SqlCom;
        //    sqlCmd.CommandText = strSQL;
        //    sqlCmd.CommandType = CommandType.StoredProcedure;

        //    #region sqlParaInit

        //    sqlParaCustCompId = new SqlParameter("@custCompyId", custCompId);
        //    //sqlParaDepEnd = new SqlParameter("@delEndTime", st);
        //    sqlParaCustCompName = new SqlParameter("@newCustCompName", custCompName);
        //    sqlParaCustCompAddr = new SqlParameter("@newCustCompAddress", custCompAddr);
        //    sqlParaCustCompTag = new SqlParameter("@newCustCompTag", custCompTag);
        //    #endregion

        //    #region sqlParaAdd
        //    sqlCmd.Parameters.Clear();
        //    sqlCmd.Parameters.Add(sqlParaCustCompId);
        //    //sqlCmd.Parameters.Add(sqlParaDepEnd);
        //    sqlCmd.Parameters.Add(sqlParaCustCompName);
        //    sqlCmd.Parameters.Add(sqlParaCustCompAddr);
        //    sqlCmd.Parameters.Add(sqlParaCustCompTag);
        //    #endregion

        //    sqlCmd.Connection.Open();

        //    sqlCmd.ExecuteNonQuery();

        //    sqlCmd.Connection.Close();
        //}

        //public void CustCompDel(string custCompId)
        //{
        //    #region sqlPara declare
        //    //custCompId
        //    SqlParameter sqlParaCustCompId = null;
        //    //custCompEnd
        //    SqlParameter sqlParaCustCompEnd = null;
        //    #endregion

        //    SqlCommand sqlCmd = null;

        //    string strSQL = "tbl_customer_company_delete";

        //    sqlCmd = this.SqlCom;
        //    sqlCmd.CommandText = strSQL;
        //    sqlCmd.CommandType = CommandType.StoredProcedure;

        //    #region sqlParaInit
        //    int custCompIdL = int.Parse(custCompId);
        //    DateTime st = DateTime.Now;

        //    sqlParaCustCompId = new SqlParameter("@delCustCompyId", custCompIdL);
        //    sqlParaCustCompEnd = new SqlParameter("@delEndTime", st);
        //    #endregion

        //    #region sqlParaAdd
        //    sqlCmd.Parameters.Clear();
        //    sqlCmd.Parameters.Add(sqlParaCustCompId);
        //    sqlCmd.Parameters.Add(sqlParaCustCompEnd);
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
        //        "FROM view_project_tag ";

        //    sqlCmd = this.SqlCom;
        //    sqlCmd.CommandText = strSQL;

        //    SqlDataAdapter projTagDataAdapter = this.SqlDA;
        //    SqlDA.SelectCommand = sqlCmd;
        //    sqlCmd.CommandType = CommandType.Text;

        //    DataSet myDataSet = new DataSet();
        //    projTagDataAdapter.Fill(myDataSet, "view_project_tag");

        //    return myDataSet;
        //}
    }
}