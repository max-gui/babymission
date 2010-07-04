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
///custCompProcess 的摘要说明
/// </summary>
public class custCompProcess : SelectLogic
{
	public custCompProcess(DataSet dataSet)
        :base(dataSet)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        tcc = (tbl_customer_company)InitDatabaseProc("tbl_customer_company");
    }

    private tbl_customer_company tcc = null;

    public override void Process()
    {
    }

    public void DoCheckCompName(string compName)
    {
        string strFilter =
            " custCompName = " + "'" + compName + "'";
        MyDst.Tables["tbl_customer_company"].DefaultView.RowFilter = strFilter;

        IntRtn = MyDst.Tables["tbl_customer_company"].DefaultView.Count;
    }

    public void DoCheckCompTag(string compTag)
    {
        string strFilter =
            " custCompTag = " + "'" + compTag + "'";
        MyDst.Tables["tbl_customer_company"].DefaultView.RowFilter = strFilter;

        IntRtn = MyDst.Tables["tbl_customer_company"].DefaultView.Count;
    }

    public override void Del()
    {
        //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)InitDatabaseProc("Database", "DataBase.TAB_DATA_USERDatabase");
        //TAB_DATA_LOG dbLog = (TAB_DATA_LOG)InitDatabaseProc("Database", "DataBase.TAB_DATA_LOG");
        //db.Delete(int.Parse(StrRtn), IntRtn, dbLog);
    }

    public override void Add()
    {
        string compId = tcc.SelectAdd(MyDst);

        StrRtn = compId;
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
        MyDst = tcc.SelectView();
    }

    public override void Search()
    {
        //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)InitDatabaseProc("Database", "DataBase.TAB_DATA_USERDatabase");
        //this.MyDst = db.SelectComb(int.Parse(StrRtn));
    }
}