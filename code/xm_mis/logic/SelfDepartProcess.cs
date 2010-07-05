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

using xm_mis.db;
namespace xm_mis.logic
{
    public class SelfDepartProcess : SelectLogic
    {
        public SelfDepartProcess(DataSet dataSet)
            : base(dataSet)
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
            tdDB = (tbl_department)InitDatabaseProc("xm_mis.db.tbl_department");

        }

        private tbl_department tdDB = null;
        public override void Process()
        {
        }

        public void SelfDepDel(int depId)
        {
            tdDB.SelfDepDel(depId);
        }

        public void SelfDepUpdate(int depId, string depName)
        {
            tdDB.SelfDepUpdate(depId, depName);
        }

        public void SelfDepAdd(string depName)
        {
            StrRtn = tdDB.SelfDepAdd(depName);
        }

        public void commit()
        {
            tdDB.SelectSelfDepatCommit(MyDst);
            //Table tb = MyDst.Tables["tbl_department"] as Table;
        }

        public override void Add()
        {
           
        }

        public override void Del()
        {
            
        }

        public override void Updata()
        {
           
        }

        public override void PwdEdit()
        {
            
        }

        public override void View()
        {
            
        }

        public void SelDepView()
        {
            MyDst = tdDB.SelectSelfDepatView(MyDst);

            string end = DateTime.Now.ToShortDateString();

            string strFilter =
                " endTime > " + "'" + end + "'" +
                " and departmentName <> '无' ";
            MyDst.Tables["tbl_department"].DefaultView.RowFilter = strFilter;

            //DataTable depTable = MyDst.Tables["tbl_department"];

            //DataColumn[] keys = new DataColumn[2];
            //keys[0] = depTable.Columns["departmentId"];

            //depTable.PrimaryKey = keys;
            
        }

        public void RealDepView()
        {
            MyDst = tdDB.SelectSelfDepatView(MyDst);

            string end = DateTime.Now.ToShortDateString();

            string strFilter =
                " endTime > " + "'" + end + "'";
            MyDst.Tables["tbl_department"].DefaultView.RowFilter = strFilter;

            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //MyDst = db.SelectView();
            //StrRtn = db.selectNum().ToString();
        }

        public override void Search()
        {
            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //this.MyDst = db.SelectComb(int.Parse(StrRtn));
        }
    }
}