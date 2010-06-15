using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
/// <summary>
///tbl_authority 的摘要说明
/// </summary>
public class tbl_authority : DataBase
{
	public tbl_authority()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public int SelectNull()
    {
        int selAu = -1;
        SqlCommand sqlCmd = null;

        string strSQL =
            "SELECT " +
            "authority " +
            "FROM tbl_authority " +
            "WHERE " +
            "authorityName = '无'";//@titleName";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;

        sqlCmd.Connection.Open();

        using (SqlDataReader sdr = sqlCmd.ExecuteReader())
        {
            while (sdr.Read())
            {
                selAu = sdr.GetInt32(0);
            }
        }

        sqlCmd.Connection.Close();

        return selAu;
    }

    public DataSet SelectAuthorityView()
    {
        //       SqlParameter sqlParaName = null;
        SqlCommand sqlCmd = null;

        string strSQL =
            "SELECT " +
            "* " +
            "FROM tbl_authority ";
            //"WHERE " +
            //"authorityName != '无' and authorityName != 'superMan'";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;
        sqlCmd.CommandType = CommandType.Text;

        //sqlParaName = new SqlParameter("@usrName", SqlDbType.Char, 10);
        //sqlParaName.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrName"].ToString().Trim();
        //sqlCmd.Parameters.Add(sqlParaName);
        //sqlCmd.Parameters.Clear();
        //sqlCmd.Parameters.Add(sqlParaIsDel);

        SqlDataAdapter userDataAdapter = this.SqlDA;

        //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
        DataSet myDataSet = new DataSet();
        userDataAdapter.Fill(myDataSet, "tbl_authority");

        return myDataSet;
    }
}