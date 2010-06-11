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
/// <summary>
///SelfTitleProcess 的摘要说明
/// </summary>
public class SelfTitleProcess : SelectLogic
{
	public SelfTitleProcess(DataSet dataSet)
        :base(dataSet)
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//

        ttDB = (tbl_title)InitDatabaseProc("tbl_title");
	}

    private tbl_title ttDB = null;
    public override void Process()
    {
    }

    public void commit()
    {
        ttDB.SelectSelfTitleatCommit(MyDst);
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
        //MyDst = ttDB.SelectSelfTitleatView(MyDst);

        //string end = DateTime.Now.ToShortDateString();

        //string strFilter =
        //    " endTime > " + "'" + end + "'" +
        //    " and titleName <> '无' ";
        //MyDst.Tables["tbl_title"].DefaultView.RowFilter = strFilter;
        ////taskTable.DefaultView.RowFilter =
        ////    "isDel = " + bool.FalseString.ToString().Trim() + " and titleName <> '无' ";

        ////MyDst = ttDB.SelectSelfTitleatView(MyDst);
        ////TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)InitDatabaseProc("Database", "DataBase.TAB_DATA_USERDatabase");
        ////MyDst = db.SelectView();
        ////StrRtn = db.selectNum().ToString();
    }

    public void SelTitleView()
    {
        MyDst = ttDB.SelectSelfTitleatView(MyDst);

        string end = DateTime.Now.ToShortDateString();

        string strFilter =
            " endTime > " + "'" + end + "'" +
            " and titleName <> '无' ";
        MyDst.Tables["tbl_title"].DefaultView.RowFilter = strFilter;
        //taskTable.DefaultView.RowFilter =
        //    "isDel = " + bool.FalseString.ToString().Trim() + " and titleName <> '无' ";

        //MyDst = ttDB.SelectSelfTitleatView(MyDst);
        //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)InitDatabaseProc("Database", "DataBase.TAB_DATA_USERDatabase");
        //MyDst = db.SelectView();
        //StrRtn = db.selectNum().ToString();
    }

    public void RealTitleView()
    {
        MyDst = ttDB.SelectSelfTitleatView(MyDst);

        string end = DateTime.Now.ToShortDateString();

        string strFilter =
            " endTime > " + "'" + end + "'";
        MyDst.Tables["tbl_title"].DefaultView.RowFilter = strFilter;
        //taskTable.DefaultView.RowFilter =
        //    "isDel = " + bool.FalseString.ToString().Trim() + " and titleName <> '无' ";

        //MyDst = ttDB.SelectSelfTitleatView(MyDst);
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