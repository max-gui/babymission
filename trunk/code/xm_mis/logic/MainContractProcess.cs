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
    public class MainContractProcess : SelectLogic
    {
        public MainContractProcess(DataSet dataSet)
            : base(dataSet)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            tmc = (tbl_mainContract)InitDatabaseProc("xm_mis.db.tbl_mainContract");
            vmpu = (view_mainContract_project_usr)InitDatabaseProc("xm_mis.db.view_mainContract_project_usr");
        }

        private tbl_mainContract tmc = null;
        private view_mainContract_project_usr vmpu = null;
        private string usrId = string.Empty;

        public string UsrId
        {
            get
            {
                return usrId;
            }
            set
            {
                usrId = value;
            }
        }

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

        public void RealmainContractProjectUsrView()
        {
            MyDst = vmpu.SelectView();

            string end = DateTime.Now.ToShortDateString();

            string strFilter =
                " endTime > " + "'" + end + "'" +
                " and usrId = " + "'" + usrId + "'";
            MyDst.Tables["view_mainContract_project_usr"].DefaultView.RowFilter = strFilter;

            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //MyDst = db.SelectView();
            //StrRtn = db.selectNum().ToString();
        }

        public void RealmainContractProjectView()
        {
            MyDst = vmpu.SelectView();

            string end = DateTime.Now.ToShortDateString();

            string strFilter =
                " endTime > " + "'" + end + "'";
            MyDst.Tables["view_mainContract_project_usr"].DefaultView.RowFilter = strFilter;
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
            string mainContractId = tmc.SelectAdd(MyDst);

            StrRtn = mainContractId;
        }

        public void MainContractPayPercentUpdate(int mainContractId, string payPercent)
        {
            tmc.MainContractPayPercentUpdate(mainContractId, payPercent);
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