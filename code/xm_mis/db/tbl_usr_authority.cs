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
namespace xm_mis.App_Code.db
{
    public class tbl_usr_authority : DataBase
    {
        public tbl_usr_authority()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public void UsrAuthAdd(int usrId, int authorityId)
        {
            SqlParameter sqlParaUNM = null;
            SqlParameter sqlParaAu = null;
            SqlParameter sqlParaSt = null;
            SqlCommand sqlCmd = null;

            string strSQL =
                "insert into " +
                "tbl_usr_authority " +
                "(usrId , authorityId , startTime) " +
                "values(@usrId , @authorityId , @startTime) ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;

            DateTime st = DateTime.Now;

            sqlParaUNM = new SqlParameter("@usrId", usrId);
            sqlParaAu = new SqlParameter("@authorityId", authorityId);
            sqlParaSt = new SqlParameter("@startTime", st);

            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaUNM);
            sqlCmd.Parameters.Add(sqlParaAu);
            sqlCmd.Parameters.Add(sqlParaSt);

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }

        public void UsrAuthDel(int usrAuhId)
        {
            SqlParameter sqlParaUsrAuthId = null;
            SqlCommand sqlCmd = null;

            string strSQL =
                "delete  " +
                "from tbl_usr_authority " +
                "where usrAuhId = @usrAuhId";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;

            sqlParaUsrAuthId = new SqlParameter("@usrAuhId", usrAuhId);

            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaUsrAuthId);

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
                "FROM view_usr_autority ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.Text;

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
            userDataAdapter.Fill(myDataSet, "view_usr_autority");

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
}