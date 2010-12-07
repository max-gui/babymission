using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using xm_mis.db;
namespace xm_mis.logic
{
    public class custManProcess : SelectLogic
    {
        public custManProcess(DataSet dataSet)
            : base(dataSet)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            tcm = (tbl_customer_manager)InitDatabaseProc("xm_mis.db.tbl_customer_manager");
        }

        private tbl_customer_manager tcm = null;
        private string custCompId = string.Empty;

        public string CustCompId
        {
            get
            {
                return custCompId;
            }
            set
            {
                custCompId = value;
            }
        }

        public override void Process()
        {
        }

        //public void DoCheckCompName(string compName)
        //{
        //    string strFilter =
        //        " custCompName = " + "'" + compName + "'";
        //    MyDst.Tables["tbl_customer_company"].DefaultView.RowFilter = strFilter;

        //    IntRtn = MyDst.Tables["tbl_customer_company"].DefaultView.Count;
        //}

        //public void DoCheckCompTag(string compTag)
        //{
        //    string strFilter =
        //        " custCompTag = " + "'" + compTag + "'";
        //    MyDst.Tables["tbl_customer_company"].DefaultView.RowFilter = strFilter;

        //    IntRtn = MyDst.Tables["tbl_customer_company"].DefaultView.Count;
        //}

        public void RealCustManView()
        {
            MyDst = tcm.SelectView();

            string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

            string strFilter =
                 " endTime > " + "'" + end + "'" +
                 " and custCompyId = " + custCompId;
            MyDst.Tables["tbl_customer_manager"].DefaultView.RowFilter = strFilter;

            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //MyDst = db.SelectView();
            //StrRtn = db.selectNum().ToString();
        }
        
        public void custCompManUpdate(int custManId, string compManName, string compManCont, string compManEmail, string compManDep,string compManTitle)
        {
            tcm.custCompManUpdate(custManId, compManName, compManCont, compManEmail, compManDep, compManTitle);
        }

        public void CustCompManDel(string custCompManId)
        {
            tcm.CustCompManDel(custCompManId);
        }

        public override void Del()
        {
            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //TAB_DATA_LOG dbLog = (TAB_DATA_LOG)("Database", "DataBase.TAB_DATA_LOG");
            //db.Delete(int.Parse(StrRtn), IntRtn, dbLog);
        }

        public override void Add()
        {
            string custManId = tcm.SelectAdd(MyDst);

            StrRtn = custManId;
        }

        public override void Updata()
        {
            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //TAB_DATA_LOG dbLog = (TAB_DATA_LOG)("Database", "DataBase.TAB_DATA_LOG");

            //db.UpdataUser(this.MyDst, int.Parse(StrRtn), dbLog);
        }

        public override void PwdEdit()
        {
            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //db.UpdataPwd(this.MyDst);
        }

        public override void View()
        {
            MyDst = tcm.SelectView();
        }

        public override void Search()
        {
            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //this.MyDst = db.SelectComb(int.Parse(StrRtn));
        }
    }
}