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
    public class SupplierProcess : SelectLogic
    {
        public SupplierProcess(DataSet dataSet)
            : base(dataSet)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            tsc = (tbl_supplier_company)InitDatabaseProc("xm_mis.db.tbl_supplier_company");
        }

        private tbl_supplier_company tsc = null;

        public override void Process()
        {
        }

        public void DoCheckSupplierName(string supplierName)
        {
            string strFilter =
                " supplierName = " + "'" + supplierName + "'";
            MyDst.Tables["tbl_supplier_company"].DefaultView.RowFilter = strFilter;

            IntRtn = MyDst.Tables["tbl_supplier_company"].DefaultView.Count;
        }

        public void RealSupplierView()
        {
            MyDst = tsc.SelectView();

            string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

            string strFilter =
                " endTime > " + "'" + end + "'";
            MyDst.Tables["tbl_supplier_company"].DefaultView.RowFilter = strFilter;
        }

        public void SupplierDel(string supplierId)
        {
            tsc.SupplierDel(supplierId);
        }

        public override void Del()
        {
            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //TAB_DATA_LOG dbLog = (TAB_DATA_LOG)("Database", "DataBase.TAB_DATA_LOG");
            //db.Delete(int.Parse(StrRtn), IntRtn, dbLog);
        }

        public override void Add()
        {
            string supplierId = tsc.SelectAdd(MyDst);

            StrRtn = supplierId;
        }

        public void SupplierUpdate(int supplierId, string supplierName, string supplierAddr)
        {
            tsc.SupplierUpdate(supplierId, supplierName, supplierAddr);
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
            //MyDst = tcc.SelectView();
        }

        public override void Search()
        {
            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //this.MyDst = db.SelectComb(int.Parse(StrRtn));
        }
    }
}