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
    public class SubContractProductProcess : SelectLogic
    {
        public SubContractProductProcess(DataSet dataSet)
            : base(dataSet)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            tscp = (tbl_subContrctProduct)InitDatabaseProc("xm_mis.db.tbl_subContrctProduct");
        }

        private tbl_subContrctProduct tscp = null;

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

        public void RealSubContractProductView()
        {
            MyDst = tscp.SelectView();

            string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

            string strFilter =
                " endTime > " + "'" + end + "'";
            MyDst.Tables["view_subContractProduct"].DefaultView.RowFilter = strFilter;
        }

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
            string subContractProductId = tscp.SelectAdd(MyDst);

            StrRtn = subContractProductId;
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