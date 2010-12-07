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
    public class ProductPurposeRelationProcess : SelectLogic
    {
        public ProductPurposeRelationProcess(DataSet dataSet)
            : base(dataSet)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            tppr = (tbl_productPurpose_relation_Old)InitDatabaseProc("xm_mis.db.tbl_productPurpose_relation_Old");
        }

        private tbl_productPurpose_relation_Old tppr = null;

        public override void Process()
        {
        }

        //public void DoCheckProductStock()
        //{
        //    string productId = MyDst.Tables["tbl_productStock"].Rows[0]["productId"].ToString();
        //    string productTag = MyDst.Tables["tbl_productStock"].Rows[0]["productTag"].ToString();
        //    string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

        //    MyDst = tppr.SelectView();

        //    string strFilter =
        //        " productId = " + "'" + productId + "'" +
        //        " and productTag = " + "'" + productTag + "'" +
        //        " and endTime > " + "'" + end + "'";
        //    MyDst.Tables["tbl_productStock"].DefaultView.RowFilter = strFilter;

        //    IntRtn = MyDst.Tables["tbl_productStock"].DefaultView.Count;
        //}

        //public void RealProductStockCheckManView()
        //{
        //    MyDst = tppr.RealProductStockCheckManView();

        //    string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

        //    string strFilter =
        //        " productInSt = " + "productStockSt" +
        //        " and productStockEd > " + "'" + end + "'";
        //    MyDst.Tables["view_productStockCheckMan"].DefaultView.RowFilter = strFilter;
        //}

        public void RealProductPurposeRelationView()
        {
            MyDst = tppr.RealProductPurposeRelationView();

            string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

            string strFilter =
                " productPurposeRelationEd > " + "'" + end + "'";
            MyDst.Tables["view_productStockRelation"].DefaultView.RowFilter = strFilter;
        }

        public void RealProductPurposeView()
        {
            MyDst = tppr.RealProductPurposeView();

            //string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

            //string strFilter =
            //    " endTime > " + "'" + end + "'";
            //MyDst.Tables["tbl_productPurpose"].DefaultView.RowFilter = strFilter;
        }

        public void AllProductPurposeRelationView()
        {
            MyDst = tppr.AllProductPurposeRelationView();

            string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

            string strFilter =
                " productStockEd > " + "'" + end + "'" +
                " and toOut = " + "'" + bool.FalseString + "'";
            MyDst.Tables["productStockRelation_view"].DefaultView.RowFilter = strFilter;
        }
        //public void ProductInCheck(string productInId, string check, byte[] checkText, string checkTextName, string contentType)
        //{
        //    tppr.ProductInCheck(productInId, check, checkText, checkTextName, contentType);
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
            string productPurposeRelationId = tppr.SelectAdd(MyDst);

            StrRtn = productPurposeRelationId;
        }

        public void ProductPurposeRelationIdUpdata(string productPurposeRelationId, string productPurposeId)
        {
            tppr.ProductPurposeRelationIdUpdata(productPurposeRelationId, productPurposeId);
        }

        public void ProductPurposeEmpty(string productPurposeRelationId)
        {
            tppr.ProductPurposeEmpty(productPurposeRelationId);
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