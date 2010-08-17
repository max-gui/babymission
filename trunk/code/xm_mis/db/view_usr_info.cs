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
    public class view_usr_info : DataBase
    {
        public view_usr_info()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public DataSet SelectLogin(DataSet dataSet)
        {
            SqlParameter sqlParaNM = null;
            SqlParameter sqlParaPwd = null;
            SqlParameter sqlParaEnd = null;

            SqlCommand sqlCmd = null;

            string strSQL =
                "SELECT " +
                "* " +
                "FROM tbl_usr " +
                "WHERE " +
                "usrName = @usrName " +
                "and usrPassWord = @usrPassWord " +
                "and endTime > @endTime";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;
            //userRow["endTime"]
            string usrNm = dataSet.Tables["tbl_usr"].Rows[0]["usrName"].ToString();
            sqlParaNM = new SqlParameter("@usrName", usrNm);
            string pwd = dataSet.Tables["tbl_usr"].Rows[0]["usrPassWord"].ToString();
            sqlParaPwd = new SqlParameter("@usrPassWord", pwd);
            DateTime end = DateTime.Parse(dataSet.Tables["tbl_usr"].Rows[0]["endTime"].ToString());
            sqlParaEnd = new SqlParameter("@endTime", end);

            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaNM);
            sqlCmd.Parameters.Add(sqlParaPwd);

            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.InsertCommand = sqlCmd;
            //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "tbl_usr");

            return myDataSet;
        }

        //public DataSet SelectUsrView(DataSet dataSet)
        //{
        //    //SqlParameter sqlParaName = null;
        //    //SqlParameter sqlParaPassWord = null;
        //    SqlCommand sqlCmd = null;

        //    string strSQL =
        //        "SELECT " +
        //        "* " +
        //        "FROM view_usr_info ";// +
        //    //"WHERE " +
        //    //"usrName = @usrName and usrPassWord = @usrPassWord";

        //    sqlCmd = this.SqlCom;
        //    sqlCmd.CommandText = strSQL;
        //    sqlCmd.CommandType = CommandType.Text;

        //    //sqlParaName = new SqlParameter("@usrName", SqlDbType.Char, 10);
        //    //sqlParaName.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrName"].ToString().Trim();
        //    //sqlParaPassWord = new SqlParameter("@usrPassWord", SqlDbType.Char, 10);
        //    //sqlParaPassWord.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrPassWord"].ToString().Trim();
        //    //sqlCmd.Parameters.Add(sqlParaName);
        //    //sqlCmd.Parameters.Add(sqlParaPassWord);

        //    SqlDataAdapter userDataAdapter = this.SqlDA;
        //    SqlDA.InsertCommand = sqlCmd;
        //    //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
        //    DataSet myDataSet = new DataSet();
        //    userDataAdapter.Fill(myDataSet, "view_usr_info");

        //    return myDataSet;
        //}

        public DataSet SelectUsrDepartTitleView(DataSet dataSet)
        {
            //SqlParameter sqlParaName = null;
            //SqlParameter sqlParaPassWord = null;
            SqlCommand sqlCmd = null;

            string strSQL =
                "SELECT " +
                "* " +
                "FROM view_usr_departTitle ";// +
            //"WHERE " +
            //"usrName = @usrName and usrPassWord = @usrPassWord";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;

            //sqlParaName = new SqlParameter("@usrName", SqlDbType.Char, 10);
            //sqlParaName.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrName"].ToString().Trim();
            //sqlParaPassWord = new SqlParameter("@usrPassWord", SqlDbType.Char, 10);
            //sqlParaPassWord.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrPassWord"].ToString().Trim();
            //sqlCmd.Parameters.Add(sqlParaName);
            //sqlCmd.Parameters.Add(sqlParaPassWord);

            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.InsertCommand = sqlCmd;
            //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "view_usr_departTitle");

            return myDataSet;
        }

        public DataSet SelectSelfUsrDepartTitleView()
        {
            //SqlParameter sqlParaName = null;
            //SqlParameter sqlParaPassWord = null;
            SqlCommand sqlCmd = null;

            string strSQL =
                "SELECT " +
                "* " +
                "FROM view_usr_department_title ";// +
            //"WHERE " +
            //"usrName = @usrName and usrPassWord = @usrPassWord";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;

            //sqlParaName = new SqlParameter("@usrName", SqlDbType.Char, 10);
            //sqlParaName.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrName"].ToString().Trim();
            //sqlParaPassWord = new SqlParameter("@usrPassWord", SqlDbType.Char, 10);
            //sqlParaPassWord.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrPassWord"].ToString().Trim();
            //sqlCmd.Parameters.Add(sqlParaName);
            //sqlCmd.Parameters.Add(sqlParaPassWord);

            SqlDataAdapter userDataAdapter = this.SqlDA;
            SqlDA.InsertCommand = sqlCmd;
            //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
            DataSet myDataSet = new DataSet();
            userDataAdapter.Fill(myDataSet, "view_usr_department_title");

            return myDataSet;
        }
    }
}