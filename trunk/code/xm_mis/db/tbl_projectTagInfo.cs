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
    public class tbl_projectTagInfo_old : DataBase
    {
        public tbl_projectTagInfo_old()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public string CompProjectCount(string custCompId)
        {
            #region sqlPara declare
            //custCompId
            SqlParameter sqlParaCustCompId = null;
            //projectCount
            SqlParameter sqlParaProjectCount = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "view_project_tag_TagInitCount";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int ccId = int.Parse(custCompId);

            sqlParaCustCompId = new SqlParameter("@custCompyId", ccId);
            sqlParaProjectCount = new SqlParameter("@countRtn", SqlDbType.Int);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaCustCompId);
            sqlCmd.Parameters.Add(sqlParaProjectCount);
            #endregion

            #region sqlDirection
            sqlParaProjectCount.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string proCount = sqlParaProjectCount.Value.ToString();
            return proCount;
        }

        public string SelectAdd(DataSet dataSet)
        {
            #region sqlPara declare
            //usrId
            SqlParameter sqlParaUsrId = null;
            //projectSynopsis
            SqlParameter sqlParaSynopsis = null;
            //custManId
            SqlParameter sqlParaManId = null;
            //projectTag
            SqlParameter sqlParaProjTag = null;
            //projectDetail
            SqlParameter sqlParaProjectDetail = null;
            //projectTagId
            SqlParameter sqlParaProjectTagId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_projectTagInfo_Insert";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            int usrId = int.Parse(dataSet.Tables["tbl_projectTagInfo"].Rows[0]["usrId"].ToString().Trim());
            string projectSynopsis = dataSet.Tables["tbl_projectTagInfo"].Rows[0]["projectSynopsis"].ToString().Trim();
            string custManId = dataSet.Tables["tbl_projectTagInfo"].Rows[0]["custManId"].ToString().Trim();
            string projectTag = dataSet.Tables["tbl_projectTagInfo"].Rows[0]["projectTag"].ToString().Trim();
            string projectDetail = "sell";

            sqlParaUsrId = new SqlParameter("@usrId", usrId);
            sqlParaSynopsis = new SqlParameter("@projectSynopsis", projectSynopsis);
            sqlParaManId = new SqlParameter("@custManId", custManId);
            sqlParaProjTag = new SqlParameter("@projectTag", projectTag);
            sqlParaProjectDetail = new SqlParameter("@projectDetail", projectDetail);
            sqlParaProjectTagId = new SqlParameter("@projectTagId", SqlDbType.Int);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaUsrId);
            sqlCmd.Parameters.Add(sqlParaSynopsis);
            sqlCmd.Parameters.Add(sqlParaManId);
            sqlCmd.Parameters.Add(sqlParaProjTag);
            sqlCmd.Parameters.Add(sqlParaProjectDetail);
            sqlCmd.Parameters.Add(sqlParaProjectTagId);
            #endregion

            #region sqlDirection
            sqlParaProjectTagId.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string projId = sqlParaProjectTagId.Value.ToString();
            return projId;
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

        public DataSet RealProjTagList()
        {
            SqlCommand sqlCmd = null;

            string strSQL =
                "SELECT " +
                "* " +
                "FROM view_project_tag ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;

            SqlDataAdapter projTagDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;
            sqlCmd.CommandType = CommandType.Text;

            DataSet myDataSet = new DataSet();
            projTagDataAdapter.Fill(myDataSet, "view_project_tag");

            return myDataSet;
        }

        public DataSet projectTag_view()
        {
            SqlCommand sqlCmd = null;

            string strSQL = "projectTag_view";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            #endregion

            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;

            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "projectTag_view");

            return myDataSet;
        }
    }
}