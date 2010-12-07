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
    public class BusinessProductProcess : SelectLogic
    {
        public BusinessProductProcess(DataSet dataSet)
            : base(dataSet)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            tbp = (tbl_businessProduct_Old)InitDatabaseProc("xm_mis.db.tbl_businessProduct_Old");
        }

        private tbl_businessProduct_Old tbp = null;

        public override void Process()
        {
        }

        //public void DoCheckProductStock()
        //{
        //    string productId = MyDst.Tables["tbl_productStock"].Rows[0]["productId"].ToString();
        //    string productTag = MyDst.Tables["tbl_productStock"].Rows[0]["productTag"].ToString();
        //    string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

        //    MyDst = tbp.SelectView();

        //    string strFilter =
        //        " productId = " + "'" + productId + "'" +
        //        " and productTag = " + "'" + productTag + "'" +
        //        " and endTime > " + "'" + end + "'";
        //    MyDst.Tables["tbl_productStock"].DefaultView.RowFilter = strFilter;

        //    IntRtn = MyDst.Tables["tbl_productStock"].DefaultView.Count;
        //}

        public void RealBusinessProductView()
        {
            MyDst = tbp.RealBusinessProductView();

            string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

            string strFilter =
                " endTime > " + "'" + end + "'";
            MyDst.Tables["view_businessProductOut"].DefaultView.RowFilter = strFilter;
        }

        public void BusinessProductOutDone()
        {
            tbp.BusinessProductOutDone(MyDst);
        }

        //public void RealProductStockBadView()
        //{
        //    MyDst = tps.RealProductStockCheckView();

        //    string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
        //    string productCheck = bool.FalseString;

        //    string strFilter =
        //        " toOut = " + "'" + bool.FalseString + "'" +
        //        " and endTime > " + "'" + end + "'" +
        //        " and productCheck = " + "'" + productCheck + "'";
        //    MyDst.Tables["view_productStockCheck"].DefaultView.RowFilter = strFilter;
        //}
        //public void RealProductStockCheckView()
        //{
        //    MyDst = tps.RealProductStockCheckView();

        //    string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

        //    string strFilter =
        //        " productStockEd > " + "'" + end + "'";
        //    MyDst.Tables["view_productStockCheck"].DefaultView.RowFilter = strFilter;
        //}

        //public void ProductInCheck(string productInCheckId, string check, byte[] checkText, string checkTextName, string contentType)
        //{
        //    tbp.ProductInCheck(productInCheckId, check, checkText, checkTextName, contentType);
        //}

        //public void DoCheckCompTag(string compTag)
        //{
        //    string strFilter =
        //        " custCompTag = " + "'" + compTag + "'";
        //    MyDst.Tables["tbl_customer_company"].DefaultView.RowFilter = strFilter;

        //    IntRtn = MyDst.Tables["tbl_customer_company"].DefaultView.Count;
        //}

        //public void RealProductView()
        //{
        //    MyDst = tpi.SelectView();

        //    string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

        //    string strFilter =
        //        " endTime > " + "'" + end + "'";
        //    MyDst.Tables["tbl_product"].DefaultView.RowFilter = strFilter;
        //}

        //public void ProductDel(string productId)
        //{
        //    tpi.ProductDel(productId);
        //}

        public override void Del()
        {
            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //TAB_DATA_LOG dbLog = (TAB_DATA_LOG)("Database", "DataBase.TAB_DATA_LOG");
            //db.Delete(int.Parse(StrRtn), IntRtn, dbLog);
        }

        public override void Add()
        {
            
        }

        //public string Add_includeError()
        //{
        //    string error = string.Empty;
        //    string productStockId = tbp.SelectAdd(MyDst, ref error);

        //    StrRtn = productStockId;

        //    return error;
        //}
        //public void ProductUpdate(int productId, string productName)
        //{
        //    tpi.ProductUpdate(productId, productName);
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