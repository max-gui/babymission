using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;

/// <summary>
///SQLServConnection 的摘要说明
/// </summary>
public class SQLServConnection
{

    private ConnectionStringSettings connStrSet = null;

    private DbProviderFactory dbFac = null;

    private DbConnection dbConn = null;

    private DbCommand dbCom = null;

    private SqlCommand sqlCom = null;

    private DbTransaction dbTrans = null;

    #region constructor
    public SQLServConnection()
    {
        ConnectInit();

        sqlCom = dbCom as SqlCommand;
    }
    #endregion

    private void ConnectInit()
    {

        connStrSet =
           ConfigurationManager.ConnectionStrings["My DB"];
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

    public SqlCommand SqlCom
    {
        get { return sqlCom; }
    }

    public DbTransaction DBTrans
    {
        get { return dbTrans; }
        set { dbTrans = value; }
    }
}