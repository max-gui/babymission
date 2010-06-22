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
        //td = (tbl_department)InitDatabaseProc("tbl_department");
        ta = (tbl_authority)InitDatabaseProc("tbl_authority");
        //tt = (tbl_title)InitDatabaseProc("tbl_title");
        tud = (tbl_usr_department)InitDatabaseProc("tbl_usr_department");
        tua = (tbl_usr_authority)InitDatabaseProc("tbl_usr_authority");
        tut = (tbl_usr_title)InitDatabaseProc("tbl_usr_title");
    }

    private view_usr_info vuiDB = null;
    private tbl_usr tu = null;
    //private tbl_department td = null;
    private tbl_authority ta = null;
    //private tbl_title tt = null;
    private tbl_usr_department tud = null;
    private tbl_usr_authority tua = null;
    private tbl_usr_title tut = null;

    public override void Process()
    {
    }

    public override void DoLogin()
    {
        string usrNm = MyDst.Tables["tbl_usr"].Rows[0]["usrName"].ToString();
        string pwd = MyDst.Tables["tbl_usr"].Rows[0]["usrPassWord"].ToString();
        string end = DateTime.Now.ToShortDateString();
        
        MyDst = tu.SelectView();

        string strFilter =
            " usrName = " + "'" + usrNm + "'" +
            " and usrPassWord = " + "'" + pwd + "'" +
            " and endTime > " + "'" + end + "'";
        MyDst.Tables["tbl_usr"].DefaultView.RowFilter = strFilter;

        IntRtn = MyDst.Tables["tbl_usr"].DefaultView.Count;
    }

    public void usrPwdModify(int usrId , string pwd)
    {
        tu.usrPwdModify(usrId , pwd);
    }

    public void usrContactModify(int usrId, string contact)
    {
        tu.usrContactModify(usrId, contact);
    } 

    public void DoCheckUsrName()
    {
        string usrNm = MyDst.Tables["tbl_usr"].Rows[0]["usrName"].ToString();

        MyDst = tu.SelectView();

        string strFilter =
            " usrName = " + "'" + usrNm + "'";
        MyDst.Tables["tbl_usr"].DefaultView.RowFilter = strFilter;

        IntRtn = MyDst.Tables["tbl_usr"].DefaultView.Count;

        //MyDst = tu.SelectUsr(MyDst);

        //if (MyDst.Tables["tbl_usr"].Rows.Count == 0)
        //{
        //    StrRtn = bool.TrueString.ToString().Trim();
        //}
        //else
        //{
        //    StrRtn = bool.FalseString.ToString().Trim();
        //}
    }

    public void UsrSelfDepartTitleView()
    {
        MyDst = vuiDB.SelectSelfUsrDepartTitleView();

        string end = DateTime.Now.ToShortDateString();

        string strFilter =
            " usrEnd > " + "'" + end + "'" +
            " and usrTitleEnd > " + "'" + end + "'" +
            " and usrDepEnd > " + "'" + end + "'";
        MyDst.Tables["view_usr_department_title"].DefaultView.RowFilter = strFilter;
    }

    public void SelfUsrDepartUpdate(int usrDepId, int usrId, int depId)
    {
        tud.SelfUsrDepartUpdate(usrDepId, usrId, depId);
    }

    public void SelfUsrTitleUpdate(int usrTitleId, int usrId, int titleId)
    {
        tut.SelfUsrTitleUpdate(usrTitleId, usrId, titleId);
    }

    public void usrDepartTitleView()
    {
        MyDst = vuiDB.SelectUsrDepartTitleView(MyDst);
        
        string end = DateTime.Now.ToShortDateString();

        string strFilter =
            " usrEnd > " + "'" + end + "'" +
            " and usrTitleEnd > " + "'" + end + "'" +
            " and usrDepEnd > " + "'" + end + "'";
        MyDst.Tables["view_usr_departTitle"].DefaultView.RowFilter = strFilter;
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
        //string strNull = "无";
        //int authNull = 0;
        //string usrName = MyDst.Tables["addTable"].Rows[0]["usrName"].ToString().Trim();

        string usrId = tu.SelectAdd(MyDst);

        StrRtn = usrId;
        //tud.SelectAdd(usrId, strNull);
        //tut.SelectAdd(usrId, strNull);
        //tua.SelectAdd(usrId, authNull);
    }

    public void usrDepModify(string usrName, string depName, DateTime depSt, string depOldNm)
    {
        tud.SelectDel(usrName, depOldNm, depSt); 
        tud.SelectAdd(usrName, depName);        
    }

    public void usrTitleModify(string usrName, string titleName, DateTime TitleSt, string TitleOldNm)
    {
        tut.SelectDel(usrName, TitleOldNm, TitleSt);
        tut.SelectAdd(usrName, titleName);
    }

    public void usrDel(string usrId)
    {
        tu.SelectDel(usrId);
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