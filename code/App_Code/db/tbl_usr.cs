using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
/// <summary>
///tbl_usr 的摘要说明
/// </summary>
public class tbl_usr : DataBase
{
	public tbl_usr()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

    public int SelectNew(string usrName)
    {
        int selId = -1;
        SqlParameter sqlParaUN = null;
        SqlCommand sqlCmd = null;

        string strSQL =
            "SELECT " +
            "usrId " +
            "FROM tbl_usr " +
            "WHERE " +
            "usrName = @usrName";//@titleName";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;

        sqlParaUN = new SqlParameter("@usrName", usrName);

        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaUN);

        sqlCmd.Connection.Open();

        using (SqlDataReader sdr = sqlCmd.ExecuteReader())
        {
            while (sdr.Read())
            {
                selId = sdr.GetInt32(0);
            }
        }

        sqlCmd.Connection.Close();

        return selId;
    }

    public void SelectAdd(DataSet dataSet)
    {
        SqlParameter sqlParaRN = null;
        //SqlParameter sqlParaTA = null;
        SqlParameter sqlParaUC = null;
        SqlParameter sqlParaUN = null;
        SqlParameter sqlParaPW = null;
        SqlCommand sqlCmd = null;

        string strSQL =
            "insert into " +
            "tbl_usr " +
            "(realName , usrContact , usrName , usrPassWord ) " +
            "values(@realName , @usrContact , @usrName , @usrPassWord ) ";
        //"WHERE " +
        //"isDel = @isDel ";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;

        string rn = dataSet.Tables["tbl_usr"].Rows[0]["realName"].ToString().Trim();
        //int ta = int.Parse(dataSet.Tables["tbl_usr"].Rows[0]["totleAuthority"].ToString().Trim());
        string uc = dataSet.Tables["tbl_usr"].Rows[0]["usrContact"].ToString().Trim();
        string un = dataSet.Tables["tbl_usr"].Rows[0]["usrName"].ToString().Trim();
        string pw = dataSet.Tables["tbl_usr"].Rows[0]["usrPassWord"].ToString().Trim();

        sqlParaRN = new SqlParameter("@realName", rn);
        //sqlParaTA = new SqlParameter("@totleAuthority", ta);
        sqlParaUC = new SqlParameter("@usrContact", uc);
        sqlParaUN = new SqlParameter("@usrName", un);
        sqlParaPW = new SqlParameter("@usrPassWord", pw);

        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaRN);
        //sqlCmd.Parameters.Add(sqlParaTA);
        sqlCmd.Parameters.Add(sqlParaUC);
        sqlCmd.Parameters.Add(sqlParaUN);
        sqlCmd.Parameters.Add(sqlParaPW);

        sqlCmd.Connection.Open();

        sqlCmd.ExecuteNonQuery();

        sqlCmd.Connection.Close();
    }

    public DataSet SelectUsr(DataSet dataSet)
    {
        SqlParameter sqlParaName = null;
        //SqlParameter sqlParaPassWord = null;
        //SqlParameter sqlParaContact = null;
        SqlCommand sqlCmd = null;

        string strSQL =
            "select " +
            "usrId , realName , usrName , usrPassWord , usrContact " +
            "from tbl_usr " +
            "WHERE " +
            "usrName = @usrName ";
             
        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;

        sqlParaName = new SqlParameter("@usrName", SqlDbType.Char, 10);
        sqlParaName.Value = dataSet.Tables["tbl_usr"].Rows[0]["usrName"].ToString().Trim();
        //sqlParaPassWord = new SqlParameter("@usrPassWord", SqlDbType.Char, 10);
        //sqlParaPassWord.Value = dataSet.Tables["tbl_usr"].Rows[0]["usrPassWord"].ToString().Trim();
        //sqlParaContact = new SqlParameter("@usrContact", SqlDbType.Char, 10);
        //sqlParaContact.Value = dataSet.Tables["tbl_usr"].Rows[0]["usrContact"].ToString().Trim();

        sqlCmd.Parameters.Add(sqlParaName);
        //sqlCmd.Parameters.Add(sqlParaPassWord);
        //sqlCmd.Parameters.Add(sqlParaContact);

        SqlDataAdapter userDataAdapter = this.SqlDA;
        //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
        DataSet myDataSet = new DataSet();
        userDataAdapter.Fill(myDataSet, "tbl_usr");

        return myDataSet;
    }

    //public void SelecttUsrCommit(DataSet dataSet)
    //{
    //    SqlDataAdapter da = this.SqlDA;
    //    SqlCommandBuilder scb = new SqlCommandBuilder(da);
    //    //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
    //    da.Update(dataSet, "tbl_usr");
    //    dataSet.AcceptChanges();
    //}
}