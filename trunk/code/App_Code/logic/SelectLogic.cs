using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Web.Services;
using System.Web.Services.Protocols;
using System.Reflection;

/// <summary>
///SelectLogic 的摘要说明
/// </summary>
public abstract class SelectLogic
{
    private System.Data.DataSet myDst;
    private int intRtn;
    private string strRtn;

    public System.Data.DataSet MyDst
    {
        get
        {
            return myDst;
        }
        set
        {
            myDst = value;
        }
    }

    public int IntRtn
    {
        get
        {
            return intRtn;
        }
        set
        {
            intRtn = value;
        }
    }

    public string StrRtn
    {
        get
        {
            return strRtn;
        }
        set
        {
            strRtn = value;
        }
    }

    public SelectLogic()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public DataBase InitDatabaseProc(string className)
    {
        Type type = Type.GetType(className);
        
        DataBase database = (DataBase)Activator.CreateInstance(type);
        
        return database;
    }

    abstract public void Process();

    //abstract protected void WriteLog();

    //abstract protected void DoSelect();

    abstract public void Add();
    abstract public void Del();
    abstract public void Updata();
    abstract public void View();
    abstract public void Search();
    public virtual void DoLogin()
    {
    }

    public virtual void PwdEdit()
    {
    }
}