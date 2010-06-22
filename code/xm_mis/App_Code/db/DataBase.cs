using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;

/// <summary>
///DataBase 的摘要说明
/// </summary>
namespace xm_mis.App_Code.db
{
    abstract public class DataBase
    {
        private SqlCommand sqlCom = null;
        private SqlTransaction sqlTrans = null;
        private SqlDataAdapter sqlDA = null;

        public DataBase()
        {
            SQLServConnection mySqlConn = new SQLServConnection();
            sqlCom = mySqlConn.DBCom as SqlCommand;

            sqlDA = new SqlDataAdapter(sqlCom);

            sqlTrans = mySqlConn.DBTrans as SqlTransaction;
        }

        public SqlTransaction SqlTrans
        {
            set { sqlTrans = value; }
            get { return sqlTrans; }
        }

        public SqlCommand SqlCom
        {
            set { sqlCom = value; }
            get { return sqlCom; }
        }

        public SqlDataAdapter SqlDA
        {
            set { sqlDA = value; }
            get { return sqlDA; }
        }
    }
}