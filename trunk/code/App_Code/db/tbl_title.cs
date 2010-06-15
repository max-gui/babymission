using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
/// <summary>
///tbl_title 的摘要说明
/// </summary>
public class tbl_title : DataBase
{
	public tbl_title()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    //public int SelectNull()
    //{
    //    int selId = -1;
    //    SqlCommand sqlCmd = null;

    //    string strSQL =
    //        "SELECT " +
    //        "titleId " +
    //        "FROM tbl_title " +
    //        "WHERE " +
    //        "titleName = '无'";//@titleName";

    //    sqlCmd = this.SqlCom;
    //    sqlCmd.CommandText = strSQL;

    //    sqlCmd.Connection.Open();
        
    //    using (SqlDataReader sdr = sqlCmd.ExecuteReader())
    //    {
    //        while (sdr.Read())
    //        {
    //            selId = sdr.GetInt32(0);
    //        }
    //    }

    //    sqlCmd.Connection.Close();

    //    return selId;
    //}

    public DataSet SelectSelfTitleatView(DataSet dataSet)
    {
        //SqlParameter sqlParaTName = null;
        //SqlParameter sqlParaIsDel = null;
        SqlCommand sqlCmd = null;

        string strSQL =
            "SELECT " +
            "* " +
            "FROM tbl_title ";// +
            //"WHERE " +
            //"isDel = @isDel";//@titleName";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;
        sqlCmd.CommandType = CommandType.Text;

        //sqlParaTName = new SqlParameter("@titleName", SqlDbType.Char, 10);
        //sqlParaTName.Value = "无职位";
        //sqlParaIsDel = new SqlParameter("@isDel", SqlDbType.Char, 10);
        //sqlParaIsDel.Value = bool.FalseString.ToString().Trim();
        sqlCmd.Parameters.Clear();
        //sqlCmd.Parameters.Add(sqlParaTName);
        //sqlCmd.Parameters.Add(sqlParaIsDel);

        SqlDataAdapter userDataAdapter = this.SqlDA;

        //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
        DataSet myDataSet = new DataSet();
        userDataAdapter.Fill(myDataSet, "tbl_title");

        return myDataSet;
    }

    public void SelectSelfTitleatCommit(DataSet dataSet)
    {
        //sqlParaName = new SqlParameter("@usrName", SqlDbType.Char, 10);
        //sqlParaName.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrName"].ToString().Trim();
        //       sqlParaIsDel = new SqlParameter("@isDel", SqlDbType.Char, 10);
        //       sqlParaIsDel.Value = bool.FalseString.ToString().Trim();
        //sqlCmd.Parameters.Add(sqlParaName);
        //       sqlCmd.Parameters.Add(sqlParaIsDel);

        SqlDataAdapter da = this.SqlDA;
        SqlCommandBuilder scb = new SqlCommandBuilder(da);
        //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
        da.Update(dataSet, "tbl_title");
        dataSet.AcceptChanges();
    }

    public void SelfTitleDel(int titleId)
    {
        #region sqlPara declare
        //realName
        SqlParameter sqlParaTitleId = null;
        //usrContact
        SqlParameter sqlParaTitleEnd = null;
        #endregion

        SqlCommand sqlCmd = null;

        string strSQL = "tbl_title_delete";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;
        sqlCmd.CommandType = CommandType.StoredProcedure;

        #region sqlParaInit
        DateTime st = DateTime.Now;

        sqlParaTitleId = new SqlParameter("@delTitleId", titleId);
        sqlParaTitleEnd = new SqlParameter("@delEndTime", st);
        #endregion

        #region sqlParaAdd
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaTitleId);
        sqlCmd.Parameters.Add(sqlParaTitleEnd);
        #endregion

        sqlCmd.Connection.Open();

        sqlCmd.ExecuteNonQuery();

        sqlCmd.Connection.Close();
    }

    public void SelfTitleUpdate(int titleId, string titleName)
    {
        #region sqlPara declare
        //titleId
        SqlParameter sqlParaTitleId = null;
        ////titleEnd
        //SqlParameter sqlParaTitleEnd = null;
        //titleName
        SqlParameter sqlParaTitleName = null;
        #endregion

        SqlCommand sqlCmd = null;

        string strSQL = "tbl_title_update";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;
        sqlCmd.CommandType = CommandType.StoredProcedure;

        #region sqlParaInit
        //DateTime st = DateTime.Now;

        sqlParaTitleId = new SqlParameter("@titleId", titleId);
        //sqlParaTitleEnd = new SqlParameter("@delEndTime", st);
        sqlParaTitleName = new SqlParameter("@newTitletName", titleName);
        #endregion

        #region sqlParaAdd
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaTitleId);
        //sqlCmd.Parameters.Add(sqlParaTitleEnd);
        sqlCmd.Parameters.Add(sqlParaTitleName);
        #endregion

        sqlCmd.Connection.Open();

        sqlCmd.ExecuteNonQuery();

        sqlCmd.Connection.Close();
    }

    public string SelfTitleAdd(string titleName)
    {
        #region sqlPara declare
        //realName
        SqlParameter sqlParaTitleId = null;
        //usrContact
        SqlParameter sqlParaTitleEnd = null;
        //usrContact
        SqlParameter sqlParaTitleName = null;
        #endregion

        SqlCommand sqlCmd = null;

        string strSQL = "tbl_title_Insert";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;
        sqlCmd.CommandType = CommandType.StoredProcedure;

        #region sqlParaInit
        DateTime st = DateTime.Now;

        sqlParaTitleId = new SqlParameter("@Identity", SqlDbType.BigInt);
        sqlParaTitleEnd = new SqlParameter("@startTime", st);
        sqlParaTitleName = new SqlParameter("@titleName", titleName);
        #endregion

        #region sqlParaAdd
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaTitleId);
        sqlCmd.Parameters.Add(sqlParaTitleEnd);
        sqlCmd.Parameters.Add(sqlParaTitleName);
        #endregion

        #region sqlDirection
        sqlParaTitleId.Direction = ParameterDirection.Output;
        //sqlParaDepId.Direction = ParameterDirection.Output;
        //sqlParaTitleId.Direction = ParameterDirection.Output;
        //sqlParaAuthId.Direction = ParameterDirection.Output;
        #endregion

        sqlCmd.Connection.Open();

        sqlCmd.ExecuteNonQuery();

        sqlCmd.Connection.Close();

        string depId = sqlParaTitleId.Value.ToString();
        return depId;
    }
}