﻿using System;
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
///SelfDepartProcess 的摘要说明
/// </summary>
public class SelfDepartProcess :SelectLogic
{
    public SelfDepartProcess(DataSet dataSet)
        :base(dataSet)
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
        tdDB = (tbl_department)InitDatabaseProc("tbl_department");
        
	}

    private tbl_department tdDB = null;
    public override void Process()
    {
    }

    public void commit()
    {
        tdDB.SelectSelfDepatCommit(MyDst);
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
        //MyDst = tdDB.SelectSelfDepatView(MyDst);

        //string end = DateTime.Now.ToShortDateString();

        //string strFilter =
        //    " endTime > " + "'" + end + "'" +
        //    " and departmentName <> '无' ";
        //MyDst.Tables["tbl_department"].DefaultView.RowFilter = strFilter;

        ////TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)InitDatabaseProc("Database", "DataBase.TAB_DATA_USERDatabase");
        ////MyDst = db.SelectView();
        ////StrRtn = db.selectNum().ToString();
    }

    public void SelDepView()
    {
        MyDst = tdDB.SelectSelfDepatView(MyDst);

        string end = DateTime.Now.ToShortDateString();

        string strFilter =
            " endTime > " + "'" + end + "'" +
            " and departmentName <> '无' ";
        MyDst.Tables["tbl_department"].DefaultView.RowFilter = strFilter;

        DataTable depTable = MyDst.Tables["tbl_department"];

        DataColumn[] keys = new DataColumn[2];
        keys[0] = depTable.Columns["departmentName"];
        keys[1] = depTable.Columns["endTime"];

        depTable.PrimaryKey = keys;
        //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)InitDatabaseProc("Database", "DataBase.TAB_DATA_USERDatabase");
        //MyDst = db.SelectView();
        //StrRtn = db.selectNum().ToString();
    }

    public void RealDepView()
    {
        MyDst = tdDB.SelectSelfDepatView(MyDst);

        string end = DateTime.Now.ToShortDateString();

        string strFilter =
            " endTime > " + "'" + end + "'";
        MyDst.Tables["tbl_department"].DefaultView.RowFilter = strFilter;

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