using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
/// <summary>
///tbl_department 的摘要说明
/// </summary>
public class tbl_department :DataBase
{
	public tbl_department()
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
    //        "departmentId " +
    //        "FROM tbl_department " +
    //        "WHERE " +
    //        "departmentName = '无'";//@titleName";

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

    public DataSet SelectSelfDepatView(DataSet dataSet)
    {
        SqlCommand sqlCmd = null;

        string strSQL =
            "SELECT " +
            "* " +
            "FROM tbl_department ";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;
        sqlCmd.CommandType = CommandType.Text;

        SqlDataAdapter userDataAdapter = this.SqlDA;

        DataSet myDataSet = new DataSet();
        userDataAdapter.Fill(myDataSet, "tbl_department");

        return myDataSet;
    }

    public void SelectSelfDepatCommit(DataSet dataSet)
    {
        //sqlParaName = new SqlParameter("@usrName", SqlDbType.Char, 10);
        //sqlParaName.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrName"].ToString().Trim();
        //       sqlParaIsDel = new SqlParameter("@isDel", SqlDbType.Char, 10);
        //       sqlParaIsDel.Value = bool.FalseString.ToString().Trim();
        //sqlCmd.Parameters.Add(sqlParaName);
        //       sqlCmd.Parameters.Add(sqlParaIsDel);
        
        SqlDataAdapter da = this.SqlDA;
        SqlConnection sqlCon = this.SqlCom.Connection;
        da.InsertCommand = new SqlCommand("tbl_department_Insert", sqlCon);
        da.DeleteCommand = new SqlCommand("tbl_department_delete", sqlCon);
        da.UpdateCommand = new SqlCommand("tbl_department_update", sqlCon);
        da.InsertCommand.CommandType = CommandType.StoredProcedure;
        da.DeleteCommand.CommandType = CommandType.StoredProcedure;
        da.UpdateCommand.CommandType = CommandType.StoredProcedure;
        SqlCommandBuilder scb = new SqlCommandBuilder(da);
        //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
        da.Update(dataSet, "tbl_department");
        dataSet.AcceptChanges();
    }

    public void SelfDepDel(int depId)
    {
        #region sqlPara declare
        //realName
        SqlParameter sqlParaDepId = null;
        //usrContact
        SqlParameter sqlParaDepEnd = null;
        #endregion

        SqlCommand sqlCmd = null;

        string strSQL = "tbl_department_delete";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;
        sqlCmd.CommandType = CommandType.StoredProcedure;

        #region sqlParaInit
        DateTime st = DateTime.Now;

        sqlParaDepId = new SqlParameter("@delDepartmentId", depId);
        sqlParaDepEnd = new SqlParameter("@delEndTime", st);
        #endregion

        #region sqlParaAdd
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaDepId);
        sqlCmd.Parameters.Add(sqlParaDepEnd);
        #endregion

        sqlCmd.Connection.Open();

        sqlCmd.ExecuteNonQuery();

        sqlCmd.Connection.Close();
    }

    public void SelfDepUpdate(int depId , string depName)
    {
        #region sqlPara declare
        //realName
        SqlParameter sqlParaDepId = null;
        //usrContact
        SqlParameter sqlParaDepEnd = null;
        //usrContact
        SqlParameter sqlParaDepName = null;
        #endregion

        SqlCommand sqlCmd = null;

        string strSQL = "tbl_department_update";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;
        sqlCmd.CommandType = CommandType.StoredProcedure;

        #region sqlParaInit
        DateTime st = DateTime.Now;

        sqlParaDepId = new SqlParameter("@delDepartmentId", depId);
        sqlParaDepEnd = new SqlParameter("@delEndTime", st);
        sqlParaDepName = new SqlParameter("@newDepartmentName", depName);
        #endregion

        #region sqlParaAdd
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaDepId);
        sqlCmd.Parameters.Add(sqlParaDepEnd);
        sqlCmd.Parameters.Add(sqlParaDepName);
        #endregion

        sqlCmd.Connection.Open();

        sqlCmd.ExecuteNonQuery();

        sqlCmd.Connection.Close();
    }
}