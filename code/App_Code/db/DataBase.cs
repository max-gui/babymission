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
abstract public class DataBase
{
	private SqlCommand sqlCom = null;
    private DbTransaction dbTrans = null;

    public DataBase( )
    {
        SQLServConnection mySqlConn = new SQLServConnection();
        sqlCom = mySqlConn.SqlCom;

        dbTrans = mySqlConn.DBTrans;
    }

    public DbTransaction DBTrans
    {
        set { dbTrans = value; }
        get { return dbTrans; }
    }

    public SqlCommand SqlCom
    {
        set { sqlCom = value; }
        get { return sqlCom; }
    }
}