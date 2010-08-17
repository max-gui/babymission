using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using xm_mis.db;
namespace xm_mis.logic
{
    public class SupplierManProcess : SelectLogic
    {
        public SupplierManProcess(DataSet dataSet)
            : base(dataSet)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            tsm = (tbl_supplier_manager)InitDatabaseProc("xm_mis.db.tbl_supplier_manager");
        }

        private tbl_supplier_manager tsm = null;
        private string supplierId = string.Empty;

        public string SupplierId
        {
            get
            {
                return supplierId;
            }
            set
            {
                supplierId = value;
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

        public void RealSupplierManView()
        {
            MyDst = tsm.SelectView();

            string end = DateTime.Now.ToShortDateString();

            string strFilter =
                 " endTime > " + "'" + end + "'" +
                 " and supplierId = " + supplierId;
            MyDst.Tables["tbl_supplier_manager"].DefaultView.RowFilter = strFilter;
        }

        public void SupplierManUpdate(int supplierManId, string supplierManName, string supplierManCont, string supplierManEmail, string supplierManDep, string supplierManTitle)
        {
            tsm.SupplierManUpdate(supplierManId, supplierManName, supplierManCont, supplierManEmail, supplierManDep, supplierManTitle);
        }

        public void SupplierManDel(string supplierManId)
        {
            tsm.SupplierManDel(supplierManId);
        }

        public override void Del()
        {
            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //TAB_DATA_LOG dbLog = (TAB_DATA_LOG)("Database", "DataBase.TAB_DATA_LOG");
            //db.Delete(int.Parse(StrRtn), IntRtn, dbLog);
        }

        public override void Add()
        {
            string supplierManId = tsm.SelectAdd(MyDst);

            StrRtn = supplierManId;
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
            //MyDst = tsm.SelectView();
        }

        public override void Search()
        {
            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //this.MyDst = db.SelectComb(int.Parse(StrRtn));
        }
    }
}