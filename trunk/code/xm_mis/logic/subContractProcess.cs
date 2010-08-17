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
    public class subContractProcess : SelectLogic
    {
        public subContractProcess(DataSet dataSet)
            : base(dataSet)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            tsc = (tbl_subContract)InitDatabaseProc("xm_mis.db.tbl_subContract");
            vscs = (view_subContract_supplier)InitDatabaseProc("xm_mis.db.view_subContract_supplier");
        }

        private tbl_subContract tsc = null;
        private view_subContract_supplier vscs = null;
        //private string usrId = string.Empty;

        //public string UsrId
        //{
        //    get
        //    {
        //        return usrId;
        //    }
        //    set
        //    {
        //        usrId = value;
        //    }
        //}

        public override void Process()
        {
        }

        //public void DoCheckProductName()
        //{
        //    string productNm = MyDst.Tables["tbl_product"].Rows[0]["productName"].ToString();

        //    MyDst = tmc.SelectView();

        //    string strFilter =
        //        " productName = " + "'" + productNm + "'";
        //    MyDst.Tables["tbl_product"].DefaultView.RowFilter = strFilter;

        //    IntRtn = MyDst.Tables["tbl_product"].DefaultView.Count;
        //}

        //public void DoCheckCompTag(string compTag)
        //{
        //    string strFilter =
        //        " custCompTag = " + "'" + compTag + "'";
        //    MyDst.Tables["tbl_customer_company"].DefaultView.RowFilter = strFilter;

        //    IntRtn = MyDst.Tables["tbl_customer_company"].DefaultView.Count;
        //}

        public void RealSubContractSupplierView()
        {
            MyDst = vscs.SelectView();

            string end = DateTime.Now.ToShortDateString();

            string strFilter =
                " endTime > " + "'" + end + "'";
            MyDst.Tables["view_subContract_supplier"].DefaultView.RowFilter = strFilter;
        }

        public void FullSubContractInfo()
        {
            MyDst = vscs.SelectFullInfoView();

            string end = DateTime.Now.ToShortDateString();

            string strFilter =
                " endTime > " + "'" + end + "'";
            MyDst.Tables["view_full_subContractInfo"].DefaultView.RowFilter = strFilter;
        }

        //public void RealProductView()
        //{
        //    MyDst = tmc.SelectView();

        //    string end = DateTime.Now.ToShortDateString();

        //    string strFilter =
        //        " endTime > " + "'" + end + "'";
        //    MyDst.Tables["tbl_product"].DefaultView.RowFilter = strFilter;
        //}

        //public void ProductDel(string productId)
        //{
        //    tmc.ProductDel(productId);
        //}

        public override void Del()
        {
            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //TAB_DATA_LOG dbLog = (TAB_DATA_LOG)("Database", "DataBase.TAB_DATA_LOG");
            //db.Delete(int.Parse(StrRtn), IntRtn, dbLog);
        }

        public override void Add()
        {
            string subContractId = tsc.SelectAdd(MyDst);

            StrRtn = subContractId;
        }

        public void SubContractReceiptPercentUpdate(int subContractId, string receiptPercent)
        {
            tsc.SubContractReceiptPercentUpdate(subContractId, receiptPercent);
        }

        //public void ProductUpdate(int productId, string productName)
        //{
        //    tmc.ProductUpdate(productId, productName);
        //}

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