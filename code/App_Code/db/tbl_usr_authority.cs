using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
/// <summary>
///tbl_usr_authority 的摘要说明
/// </summary>
public class tbl_usr_authority : DataBase
{
	public tbl_usr_authority()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public void SelectAdd(int usrId, int auth)
    {
        SqlParameter sqlParaUid = null;
        SqlParameter sqlParaAu = null;
        SqlCommand sqlCmd = null;

        string strSQL =
            "insert into " +
            "tbl_usr_authority " +
            "(usrId , authority) " +
            "values(@usrId , @authority) ";
        //"WHERE " +
        //"isDel = @isDel ";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;

        sqlParaUid = new SqlParameter("@usrId", usrId);
        sqlParaAu = new SqlParameter("@authority", auth);

        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaUid);
        sqlCmd.Parameters.Add(sqlParaAu);

        sqlCmd.Connection.Open();

        sqlCmd.ExecuteNonQuery();

        sqlCmd.Connection.Close();
    }

    public DataSet SelectUsrAuthView(DataSet dataSet)
    {
        //       SqlParameter sqlParaName = null;
        //SqlParameter sqlParaIsDel = null;
        SqlCommand sqlCmd = null;

        string strSQL =
            "SELECT " +
            "* " +
            "FROM tbl_usr_authority " +
            "WHERE " +
            "authority != 0 ";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;

        //sqlParaName = new SqlParameter("@usrName", SqlDbType.Char, 10);
        //sqlParaName.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrName"].ToString().Trim();
        //sqlParaIsDel = new SqlParameter("@isDel", SqlDbType.Char, 10);
        //sqlParaIsDel.Value = bool.FalseString.ToString().Trim();
        //sqlCmd.Parameters.Add(sqlParaName);
        //sqlCmd.Parameters.Clear();
        //sqlCmd.Parameters.Add(sqlParaIsDel);

        SqlDataAdapter userDataAdapter = this.SqlDA;

        //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
        DataSet myDataSet = new DataSet();
        userDataAdapter.Fill(myDataSet, "tbl_usr_authority");

        return myDataSet;
    }

    public void SelectUsrAuthCommit(DataSet dataSet)
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
        da.Update(dataSet, "tbl_usr_authority");
        dataSet.AcceptChanges();
    }
}