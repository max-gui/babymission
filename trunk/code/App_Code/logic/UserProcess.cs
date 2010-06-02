using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
/// <summary>
///UserProcess 的摘要说明
/// </summary>
public class UserProcess : SelectLogic
{
    public UserProcess()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }

    public override void Process()
    {
    }

    public override void DoLogin()
    {
        view_usr_info db = (view_usr_info)InitDatabaseProc("DataBase.view_usr_info");
        MyDst = db.SelectLogin(MyDst);

        if (MyDst.Tables["userTable"].Rows.Count != 0)
        {
            StrRtn = MyDst.Tables["userTable"].Rows[0]["customer"].ToString().Trim();
        }
        else
        {
            StrRtn = string.Empty;
        }
    }

    public override void Add()
    {
        //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)InitDatabaseProc("Database", "DataBase.TAB_DATA_USERDatabase");
        //TAB_DATA_LOG dbLog = (TAB_DATA_LOG)InitDatabaseProc("Database", "DataBase.TAB_DATA_LOG");

        //db.Insert(MyDst, int.Parse(StrRtn), dbLog);
    }

    public override void Del()
    {
        //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)InitDatabaseProc("Database", "DataBase.TAB_DATA_USERDatabase");
        //TAB_DATA_LOG dbLog = (TAB_DATA_LOG)InitDatabaseProc("Database", "DataBase.TAB_DATA_LOG");
        //db.Delete(int.Parse(StrRtn), IntRtn, dbLog);
    }

    public override void Updata()
    {
        //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)InitDatabaseProc("Database", "DataBase.TAB_DATA_USERDatabase");
        //TAB_DATA_LOG dbLog = (TAB_DATA_LOG)InitDatabaseProc("Database", "DataBase.TAB_DATA_LOG");

        //db.UpdataUser(this.MyDst, int.Parse(StrRtn), dbLog);
    }

    public override void PwdEdit()
    {
        //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)InitDatabaseProc("Database", "DataBase.TAB_DATA_USERDatabase");
        //db.UpdataPwd(this.MyDst);
    }

    public override void View()
    {
        //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)InitDatabaseProc("Database", "DataBase.TAB_DATA_USERDatabase");
        //MyDst = db.SelectView();
        //StrRtn = db.selectNum().ToString();
    }

    public override void Search()
    {
        //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)InitDatabaseProc("Database", "DataBase.TAB_DATA_USERDatabase");
        //this.MyDst = db.SelectComb(int.Parse(StrRtn));
    }
}