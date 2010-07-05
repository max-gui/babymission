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

        public string SelectAdd(DataSet dataSet)
        {
            #region sqlPara declare
            //realName
            SqlParameter sqlParaRN = null;
            //usrContact
            SqlParameter sqlParaUC = null;
            //usrName
            SqlParameter sqlParaUN = null;
            //usrPwd
            SqlParameter sqlParaPW = null;
            //start
            SqlParameter sqlParaSt = null;
            //newUsrId
            SqlParameter sqlParaId = null;
            ////
            //SqlParameter sqlParaDepId = null;
            ////
            //SqlParameter sqlParaTitleId = null;
            ////
            //SqlParameter sqlParaAuthId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_usr_Insert";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            string rn = dataSet.Tables["addTable"].Rows[0]["realName"].ToString().Trim();
            string uc = dataSet.Tables["addTable"].Rows[0]["usrContact"].ToString().Trim();
            string un = dataSet.Tables["addTable"].Rows[0]["usrName"].ToString().Trim();
            string pw = dataSet.Tables["addTable"].Rows[0]["usrPassWord"].ToString().Trim();
            DateTime st = DateTime.Now;

            sqlParaRN = new SqlParameter("@realName", rn);
            sqlParaUC = new SqlParameter("@usrContact", uc);
            sqlParaUN = new SqlParameter("@usrName", un);
            sqlParaPW = new SqlParameter("@usrPassWord", pw);
            sqlParaSt = new SqlParameter("@startTime", st);
            sqlParaId = new SqlParameter("@Identity", SqlDbType.BigInt, 0, "usrId");
            //sqlParaDepId = new SqlParameter("@departmentId", SqlDbType.BigInt);
            //sqlParaTitleId = new SqlParameter("@titleId", SqlDbType.BigInt);
            //sqlParaAuthId = new SqlParameter("@authorityId", SqlDbType.BigInt);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaRN);
            sqlCmd.Parameters.Add(sqlParaUC);
            sqlCmd.Parameters.Add(sqlParaUN);
            sqlCmd.Parameters.Add(sqlParaPW);
            sqlCmd.Parameters.Add(sqlParaSt);
            sqlCmd.Parameters.Add(sqlParaId);
            //sqlCmd.Parameters.Add(sqlParaDepId);
            //sqlCmd.Parameters.Add(sqlParaTitleId);
            //sqlCmd.Parameters.Add(sqlParaAuthId);
            #endregion

            #region sqlDirection
            sqlParaId.Direction = ParameterDirection.Output;
            //sqlParaDepId.Direction = ParameterDirection.Output;
            //sqlParaTitleId.Direction = ParameterDirection.Output;
            //sqlParaAuthId.Direction = ParameterDirection.Output;
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();

            string usrId = sqlParaId.Value.ToString();
            return usrId;
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
        }

        public DataSet SelectView()
        {
            //SqlParameter sqlParaNM = null;
            //SqlParameter sqlParaPwd = null;
            //SqlParameter sqlParaEnd = null;

            SqlCommand sqlCmd = null;

            string strSQL =
                "SELECT " +
                "* " +
                "FROM tbl_usr ";// +
            //"WHERE " +
            //"usrName = @usrName " +
            //"and usrPassWord = @usrPassWord " +
            //"and endTime > @endTime";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            //userRow["endTime"]
            //string usrNm = dataSet.Tables["tbl_usr"].Rows[0]["usrName"].ToString();
            //sqlParaNM = new SqlParameter("@usrName", usrNm);
            //string pwd = dataSet.Tables["tbl_usr"].Rows[0]["usrPassWord"].ToString();
            //sqlParaPwd = new SqlParameter("@usrPassWord", pwd);
            //DateTime end = DateTime.Parse(dataSet.Tables["tbl_usr"].Rows[0]["endTime"].ToString());
            //sqlParaEnd = new SqlParameter("@endTime", end);

            //sqlCmd.Parameters.Clear();
            //sqlCmd.Parameters.Add(sqlParaNM);
            //sqlCmd.Parameters.Add(sqlParaPwd);

            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.SelectCommand = sqlCmd;
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
}