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
    public class ProductProcess : SelectLogic
    {
        public ProductProcess(DataSet dataSet)
            : base(dataSet)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            tp = (tbl_product)InitDatabaseProc("xm_mis.db.tbl_product");
        }

        private tbl_product tp = null;

        public override void Process()
        {
        }

        public void DoCheckProductName()
        {
            string productNm = MyDst.Tables["tbl_product"].Rows[0]["productName"].ToString();

            MyDst = tp.SelectView();

            string strFilter =
                " productName = " + "'" + productNm + "'";
            MyDst.Tables["tbl_product"].DefaultView.RowFilter = strFilter;

            IntRtn = MyDst.Tables["tbl_product"].DefaultView.Count;            
        }

        //public void DoCheckCompTag(string compTag)
        //{
        //    string strFilter =
        //        " custCompTag = " + "'" + compTag + "'";
        //    MyDst.Tables["tbl_customer_company"].DefaultView.RowFilter = strFilter;

        //    IntRtn = MyDst.Tables["tbl_customer_company"].DefaultView.Count;
        //}

        public void RealProductView()
        {
            MyDst = tp.SelectView();

            string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

            string strFilter =
                " endTime > " + "'" + end + "'";
            MyDst.Tables["tbl_product"].DefaultView.RowFilter = strFilter;
        }

        public void ProductDel(string productId)
        {
            tp.ProductDel(productId);
        }

        public override void Del()
        {
            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //TAB_DATA_LOG dbLog = (TAB_DATA_LOG)("Database", "DataBase.TAB_DATA_LOG");
            //db.Delete(int.Parse(StrRtn), IntRtn, dbLog);
        }

        public override void Add()
        {
            
        }

        public string Add_includeError()
        {
            string error = string.Empty;
            string productId = tp.SelectAdd(MyDst, ref error);

            StrRtn = productId;

            return error;
        }

        public void ProductUpdate(int productId, string productName)
        {
            tp.ProductUpdate(productId, productName);
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