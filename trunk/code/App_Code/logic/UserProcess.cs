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
///UserProcess 的摘要说明
/// </summary>
public class UserProcess : SelectLogic
{
    public UserProcess(DataSet dataSet)
        :base(dataSet)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        vuiDB = (view_usr_info)InitDatabaseProc("view_usr_info");
        tu = (tbl_usr)InitDatabaseProc("tbl_usr");
        td = (tbl_department)InitDatabaseProc("tbl_department");
        ta = (tbl_authority)InitDatabaseProc("tbl_authority");
        tt = (tbl_title)InitDatabaseProc("tbl_title");
        tud = (tbl_usr_department)InitDatabaseProc("tbl_usr_department");
        tua = (tbl_usr_authority)InitDatabaseProc("tbl_usr_authority");
        tut = (tbl_usr_title)InitDatabaseProc("tbl_usr_title");
    }

    private view_usr_info vuiDB = null;
    private tbl_usr tu = null;
    private tbl_department td = null;
    private tbl_authority ta = null;
    private tbl_title tt = null;
    private tbl_usr_department tud = null;
    private tbl_usr_authority tua = null;
    private tbl_usr_title tut = null;

    public override void Process()
    {
    }

    public override void DoLogin()
    {
        MyDst = vuiDB.SelectLogin(MyDst);

        //if (MyDst.Tables["view_usr_info"].Rows.Count != 0)
        //{
        //    IntRtn = int.Parse(MyDst.Tables["view_usr_info"].Rows[0]["totleAuthority"].ToString().Trim());
        //}
        //else
        //{
        //    IntRtn = -1;
        //}

        IntRtn = MyDst.Tables["view_usr_info"].Rows.Count;
    }

    public void DoCheckUsrName()
    {
        MyDst = tu.SelectUsr(MyDst);

        if (MyDst.Tables["tbl_usr"].Rows.Count == 0)
        {
            StrRtn = bool.TrueString.ToString().Trim();
        }
        else
        {
            StrRtn = bool.FalseString.ToString().Trim();
        }
    }

    //public void commit()
    //{
    //    int titleId = tt.SelectNull();
    //    int authId = ta.SelectNull();
    //    int depId = td.SelectNull();
    //    int usrId = 0;

        
    //    tu.SelecttUsrCommit(MyDst);

    //    tud.SelectAdd(usrId, depId);
    //    tut.SelectAdd(usrId, titleId);
 

    //}

    public override void Add()
    {
        tu.SelectAdd(MyDst);

        int titleId = tt.SelectNull();
        int auth = ta.SelectNull();
        int depId = td.SelectNull();
        int usrId = tu.SelectNew(MyDst.Tables["tbl_usr"].Rows[0]["usrName"].ToString().Trim());

        tud.SelectAdd(usrId, depId);
        tut.SelectAdd(usrId, titleId);
        tua.SelectAdd(usrId, auth);
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
        MyDst = vuiDB.SelectUsrView(MyDst);
    }

    public override void Search()
    {
        //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)InitDatabaseProc("Database", "DataBase.TAB_DATA_USERDatabase");
        //this.MyDst = db.SelectComb(int.Parse(StrRtn));
    }
}