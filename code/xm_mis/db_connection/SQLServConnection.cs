using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;


namespace xm_mis.db_connection
{
    public class SQLServConnection
    {
        private ConnectionStringSettings connStrSet = null;

        private DbProviderFactory dbFac = null;

        private DbConnection dbConn = null;

        private DbCommand dbCom = null;

        private DbTransaction dbTrans = null;

        #region constructor
        public SQLServConnection()
        {
            ConnectInit();
        }
        #endregion

        private void ConnectInit()
        {
            string c_str;
            c_str = "xm_dbConnectionString";

            connStrSet =
               ConfigurationManager.ConnectionStrings[c_str];
            dbFac =
                DbProviderFactories.GetFactory(connStrSet.ProviderName);
            dbConn =
                dbFac.CreateConnection();
            dbCom =
                dbFac.CreateCommand();
            dbConn.ConnectionString =
                connStrSet.ConnectionString;
            dbCom.Connection =
                dbConn;
        }

        public DbCommand DBCom
        {
            get { return dbCom; }
        }

        public DbTransaction DBTrans
        {
            get { return dbTrans; }
            set { dbTrans = value; }
        }

        public DbConnection DBConn
        {
            get { return dbConn; }
        }
    }
}