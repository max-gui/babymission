using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;

using xm_mis.App_Code.db;

/// <summary>
///tbl_customer_company 的摘要说明
/// </summary>
public class tbl_customer_company : DataBase
{
	public tbl_customer_company()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}
    
    public string SelectAdd(DataSet dataSet)
    {
        #region sqlPara declare
        //CompName
        SqlParameter sqlParaCompName = null;
        //CompAddr
        SqlParameter sqlParaCompAddr = null;
        //CompTag
        SqlParameter sqlParaCompTag = null;
        //start
        SqlParameter sqlParaSt = null;
        //newCompId
        SqlParameter sqlParaId = null;
        #endregion

        SqlCommand sqlCmd = null;

        string strSQL = "tbl_customer_company_Insert";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;
        sqlCmd.CommandType = CommandType.StoredProcedure;

        #region sqlParaInit
        string cn = dataSet.Tables["tbl_customer_company"].Rows[0]["custCompName"].ToString().Trim();
        string ca = dataSet.Tables["tbl_customer_company"].Rows[0]["custCompAddress"].ToString().Trim();
        string ct = dataSet.Tables["tbl_customer_company"].Rows[0]["custCompTag"].ToString().Trim();
        DateTime st = DateTime.Now;

        sqlParaCompName = new SqlParameter("@custCompName", cn);
        sqlParaCompAddr = new SqlParameter("@custCompAddress", ca);
        sqlParaCompTag = new SqlParameter("@custCompTag", ct);
        sqlParaSt = new SqlParameter("@startTime", st);
        sqlParaId = new SqlParameter("@Identity", SqlDbType.BigInt, 0, "custCompyId");
        #endregion

        #region sqlParaAdd
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaCompName);
        sqlCmd.Parameters.Add(sqlParaCompAddr);
        sqlCmd.Parameters.Add(sqlParaCompTag);
        sqlCmd.Parameters.Add(sqlParaSt);
        sqlCmd.Parameters.Add(sqlParaId);
        #endregion

        #region sqlDirection
        sqlParaId.Direction = ParameterDirection.Output;
        #endregion

        sqlCmd.Connection.Open();

        sqlCmd.ExecuteNonQuery();

        sqlCmd.Connection.Close();

        string compId = sqlParaId.Value.ToString();
        return compId;
    }

    /*
    //public void SelectAdd(DataSet dataSet)
    //{
    //    SqlParameter sqlParaRN = null;
    //    //SqlParameter sqlParaTA = null;
    //    SqlParameter sqlParaUC = null;
    //    SqlParameter sqlParaUN = null;
    //    SqlParameter sqlParaPW = null;
    //    SqlParameter sqlParaSt = null;
    //    SqlCommand sqlCmd = null;

    public void SelectDel(string usrId)
    {
        #region sqlPara declare
        //usrId
        SqlParameter sqlParaUsrId = null;
        //endTIme
        SqlParameter sqlParaEnd = null;
        #endregion

        SqlCommand sqlCmd = null;

        string strSQL = "tbl_usr_delete";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;
        sqlCmd.CommandType = CommandType.StoredProcedure;

        #region sqlParaInit
        DateTime end = DateTime.Now;

        sqlParaUsrId = new SqlParameter("@delUsrId", usrId);
        sqlParaEnd = new SqlParameter("@delEndTime", end);
        #endregion

        #region sqlParaAdd
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaUsrId);
        sqlCmd.Parameters.Add(sqlParaEnd);
        #endregion

        sqlCmd.Connection.Open();

        sqlCmd.ExecuteNonQuery();

        sqlCmd.Connection.Close();
    }
    //public void SelectAdd(DataSet dataSet)
    //{
    //    SqlParameter sqlParaRN = null;
    //    //SqlParameter sqlParaTA = null;
    //    SqlParameter sqlParaUC = null;
    //    SqlParameter sqlParaUN = null;
    //    SqlParameter sqlParaPW = null;
    //    SqlParameter sqlParaSt = null;
    //    SqlCommand sqlCmd = null;

    //    string strSQL =
    //        "insert into " +
    //        "tbl_usr " +
    //        "(realName , usrContact , usrName , usrPassWord , startTime) " +
    //        "values(@realName , @usrContact , @usrName , @usrPassWord , @startTime) ";
    //    //"WHERE " +
    //    //"isDel = @isDel ";

    //    sqlCmd = this.SqlCom;
    //    sqlCmd.CommandText = strSQL;

    //    string rn = dataSet.Tables["addTable"].Rows[0]["realName"].ToString().Trim();
    //    //int ta = int.Parse(dataSet.Tables["tbl_usr"].Rows[0]["totleAuthority"].ToString().Trim());
    //    string uc = dataSet.Tables["addTable"].Rows[0]["usrContact"].ToString().Trim();
    //    string un = dataSet.Tables["addTable"].Rows[0]["usrName"].ToString().Trim();
    //    string pw = dataSet.Tables["addTable"].Rows[0]["usrPassWord"].ToString().Trim();
    //    DateTime st = DateTime.Now;

    //    sqlParaRN = new SqlParameter("@realName", rn);
    //    //sqlParaTA = new SqlParameter("@totleAuthority", ta);
    //    sqlParaUC = new SqlParameter("@usrContact", uc);
    //    sqlParaUN = new SqlParameter("@usrName", un);
    //    sqlParaPW = new SqlParameter("@usrPassWord", pw);
    //    sqlParaSt = new SqlParameter("@startTime", st);

    //    sqlCmd.Parameters.Clear();
    //    sqlCmd.Parameters.Add(sqlParaRN);
    //    //sqlCmd.Parameters.Add(sqlParaTA);
    //    sqlCmd.Parameters.Add(sqlParaUC);
    //    sqlCmd.Parameters.Add(sqlParaUN);
    //    sqlCmd.Parameters.Add(sqlParaPW);
    //    sqlCmd.Parameters.Add(sqlParaSt);

    //    sqlCmd.Connection.Open();

    //    sqlCmd.ExecuteNonQuery();

    //    sqlCmd.Connection.Close();
    //}

    public void usrPwdModify(int usrId, string pwd)
    {
        #region sqlPara declare
        //usrId
        SqlParameter sqlParaUsrId = null;
        //passWord
        SqlParameter sqlParaPwd = null;
        #endregion

        SqlCommand sqlCmd = null;

        string strSQL = "tbl_usr_pwdModify";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;
        sqlCmd.CommandType = CommandType.StoredProcedure;

        #region sqlParaInit

        sqlParaUsrId = new SqlParameter("@usrId", usrId);
        sqlParaPwd = new SqlParameter("@usrPassWord", pwd);
        #endregion

        #region sqlParaAdd
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaUsrId);
        sqlCmd.Parameters.Add(sqlParaPwd);
        #endregion

        sqlCmd.Connection.Open();

        sqlCmd.ExecuteNonQuery();

        sqlCmd.Connection.Close();
    }

    public void usrContactModify(int usrId, string contact)
    {
        #region sqlPara declare
        //usrId
        SqlParameter sqlParaUsrId = null;
        //passWord
        SqlParameter sqlParaContact = null;
        #endregion

        SqlCommand sqlCmd = null;

        string strSQL = "tbl_usr_contactModify";

        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;
        sqlCmd.CommandType = CommandType.StoredProcedure;

        #region sqlParaInit

        sqlParaUsrId = new SqlParameter("@usrId", usrId);
        sqlParaContact = new SqlParameter("@usrContact", contact);
        #endregion

        #region sqlParaAdd
        sqlCmd.Parameters.Clear();
        sqlCmd.Parameters.Add(sqlParaUsrId);
        sqlCmd.Parameters.Add(sqlParaContact);
        #endregion

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
    }*/

    public DataSet SelectView()
    {
        SqlCommand sqlCmd = null;

        string strSQL =
            "SELECT " +
            "* " +
            "FROM tbl_customer_company ";
        
        sqlCmd = this.SqlCom;
        sqlCmd.CommandText = strSQL;

        SqlDataAdapter userDataAdapter = this.SqlDA;
        SqlDA.SelectCommand = sqlCmd;
        
        DataSet myDataSet = new DataSet();
        userDataAdapter.Fill(myDataSet, "tbl_customer_company");

        return myDataSet;
    }
}