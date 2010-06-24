using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data.SqlClient;
using System.Data;
/// <summary>
///tbl_usr_department 的摘要说明
/// </summary>
namespace xm_mis.App_Code.db
{
    public class tbl_usr_department : DataBase
    {
        public tbl_usr_department()
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
        }

        public void SelfUsrDepartUpdate(int usrDepId, int usrId, int depId)
        {
            #region sqlPara declare
            //usrDepId
            SqlParameter sqlParaUsrDepId = null;
            ////endTime
            //SqlParameter sqlParaEnd = null;
            //usrId
            SqlParameter sqlParaUsrId = null;
            //depId
            SqlParameter sqlParaDepId = null;
            #endregion

            SqlCommand sqlCmd = null;

            string strSQL = "tbl_usr_department_update";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;
            sqlCmd.CommandType = CommandType.StoredProcedure;

            #region sqlParaInit
            //DateTime st = DateTime.Now;

            sqlParaUsrDepId = new SqlParameter("@usrDepId", usrDepId);
            //sqlParaEnd = new SqlParameter("@delEndTime", st);
            sqlParaUsrId = new SqlParameter("@newUsrId", usrId);
            sqlParaDepId = new SqlParameter("@newDepId", depId);
            #endregion

            #region sqlParaAdd
            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaUsrDepId);
            //sqlCmd.Parameters.Add(sqlParaEnd);
            sqlCmd.Parameters.Add(sqlParaUsrId);
            sqlCmd.Parameters.Add(sqlParaDepId);
            #endregion

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }

        public void SelectAdd(string usrName, string depName)
        {
            SqlParameter sqlParaUNM = null;
            SqlParameter sqlParaDNM = null;
            SqlParameter sqlParaSt = null;
            SqlCommand sqlCmd = null;

            string strSQL =
                "insert into " +
                "tbl_usr_department " +
                "(usrName , departmentName , startTime) " +
                "values(@usrName , @departmentName , @startTime) ";
            //"WHERE " +
            //"isDel = @isDel ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;

            sqlParaUNM = new SqlParameter("@usrName", usrName);
            sqlParaDNM = new SqlParameter("@departmentName", depName);
            DateTime st = DateTime.Now;
            sqlParaSt = new SqlParameter("@startTime", st);

            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaUNM);
            sqlCmd.Parameters.Add(sqlParaDNM);
            sqlCmd.Parameters.Add(sqlParaSt);

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }

        public void SelectDel(string usrName, string depName, DateTime depSt)
        {
            SqlParameter sqlParaUNM = null;
            SqlParameter sqlParaDNM = null;
            SqlParameter sqlParaDSt = null;
            SqlParameter sqlParaDEnd = null;
            SqlParameter sqlParaDNEnd = null;
            SqlCommand sqlCmd = null;

            string strSQL =
                "update " +
                "tbl_usr_department " +
                "set " +
                "endTime = @nendTime " +
                "where usrName = @usrName and departmentName = @departmentName and startTime = @startTime and endTime = @endTime";
            //"WHERE " +
            //"isDel = @isDel ";

            sqlCmd = this.SqlCom;
            sqlCmd.CommandText = strSQL;

            sqlParaUNM = new SqlParameter("@usrName", usrName);
            sqlParaDNM = new SqlParameter("@departmentName", depName);
            sqlParaDSt = new SqlParameter("@startTime", depSt);
            sqlParaDEnd = new SqlParameter("@endTime", DateTime.Parse("9999-12-31"));
            sqlParaDNEnd = new SqlParameter("@nendTime", DateTime.Now);


            sqlCmd.Parameters.Clear();
            sqlCmd.Parameters.Add(sqlParaUNM);
            sqlCmd.Parameters.Add(sqlParaDNM);
            sqlCmd.Parameters.Add(sqlParaDSt);
            sqlCmd.Parameters.Add(sqlParaDEnd);
            sqlCmd.Parameters.Add(sqlParaDNEnd);

            sqlCmd.Connection.Open();

            sqlCmd.ExecuteNonQuery();

            sqlCmd.Connection.Close();
        }
        //public void SelectUsrAuthCommit(DataSet dataSet)
        //{
        //    //sqlParaName = new SqlParameter("@usrName", SqlDbType.Char, 10);
        //    //sqlParaName.Value = dataSet.Tables["view_usr_info"].Rows[0]["usrName"].ToString().Trim();
        //    //       sqlParaIsDel = new SqlParameter("@isDel", SqlDbType.Char, 10);
        //    //       sqlParaIsDel.Value = bool.FalseString.ToString().Trim();
        //    //sqlCmd.Parameters.Add(sqlParaName);
        //    //       sqlCmd.Parameters.Add(sqlParaIsDel);

        //    SqlDataAdapter da = this.SqlDA;
        //    SqlCommandBuilder scb = new SqlCommandBuilder(da);
        //    //SqlCommandBuilder userScb = new SqlCommandBuilder(userDataAdapter);
        //    da.Update(dataSet, "tbl_usr_authority");
        //    dataSet.AcceptChanges();
        //}
    }
}