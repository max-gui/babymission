using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using xm_mis.App_Code.db;

/// <summary>
///UsrAuthProcess 的摘要说明
/// </summary>
public class UsrAuthProcess : SelectLogic
{
    public UsrAuthProcess(DataSet dataSet)
        : base(dataSet)
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
        tua = (tbl_usr_authority)InitDatabaseProc("tbl_usr_authority");
	}

    private tbl_usr_authority tua = null;

    public override void Process()
    {
    }

    public void UsrAuthAdd(int usrId, int authorityId)
    {
        tua.UsrAuthAdd(usrId, authorityId);
    }

    public void UsrAuthDel(int usrAuhId)
    {
        tua.UsrAuthDel(usrAuhId);
    }

    public void commit()
    {
        tua.SelectUsrAuthCommit(MyDst);
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
        MyDst = tua.SelectUsrAuthView(MyDst);
        
        string end = DateTime.Now.ToShortDateString();

        string strFilter =
            " usrEnd > " + "'" + end + "'" +
            " and usrAuEnd > " + "'" + end + "'" +
            " and authorityName <> '无' " +
            " and authorityName <> 'superman' ";
        MyDst.Tables["view_usr_autority"].DefaultView.RowFilter = strFilter;
    }

    public override void Search()
    {
        //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)InitDatabaseProc("Database", "DataBase.TAB_DATA_USERDatabase");
        //this.MyDst = db.SelectComb(int.Parse(StrRtn));
    }
}