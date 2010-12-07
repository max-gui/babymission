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
    public class ProjectTagProcess : SelectLogic
    {
        public ProjectTagProcess(DataSet dataSet)
            : base(dataSet)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            tp = (tbl_projectTagInfo_old)InitDatabaseProc("xm_mis.db.tbl_projectTagInfo_old");
        }

        private tbl_projectTagInfo_old tp = null;
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

        public string compProjectCount(string custCompId)
        {
            return tp.CompProjectCount(custCompId);
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

        public void RealProjTagView(string userId)
        {
            MyDst = tp.projectTag_view();

            string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");
            string projectDetail = "sell";

            string strFilter =
                " endTime > " + "'" + end + "'" +
                " and doneTime > " + "'" + end + "'" +
                " and applymentUsrId = " + "'" + userId + "'" +
                " and projectDetail = " + "'" + projectDetail + "'";
            MyDst.Tables["projectTag_view"].DefaultView.RowFilter = strFilter;

            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //MyDst = db.SelectView();
            //StrRtn = db.selectNum().ToString();
        }

        public void RealProjTagList(string projectDetail)
        {
            MyDst = tp.RealProjTagList();

            string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

            string strFilter =
                " endTime > " + "'" + end + "'" +
                " and projectDetail = " + "'" + projectDetail + "'";
            MyDst.Tables["view_project_tag"].DefaultView.RowFilter = strFilter;

            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //MyDst = db.SelectView();
            //StrRtn = db.selectNum().ToString();
        }

        //public void RealProjTagView()
        //{
        //    MyDst = tp.SelectView();

        //    string end = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff");

        //    string strFilter =
        //        " endTime > " + "'" + end + "'";
        //    MyDst.Tables["view_project_tag"].DefaultView.RowFilter = strFilter;

        //    //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
        //    //MyDst = db.SelectView();
        //    //StrRtn = db.selectNum().ToString();
        //}

        //public void SelfCustCompDel(string custCompId)
        //{
        //    tp.CustCompDel(custCompId);
        //}

        public override void Del()
        {
            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //TAB_DATA_LOG dbLog = (TAB_DATA_LOG)("Database", "DataBase.TAB_DATA_LOG");
            //db.Delete(int.Parse(StrRtn), IntRtn, dbLog);
        }

        public override void Add()
        {
            string projId = tp.SelectAdd(MyDst);

            StrRtn = projId;
        }

        //public void custCompUpdate(int custCompId, string custCompName, string custCompAddr, string custCompTag)
        //{
        //    tp.CustCompUpdate(custCompId, custCompName, custCompAddr, custCompTag);
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
            //MyDst = tp.SelectView();
        }

        public override void Search()
        {
            //TAB_DATA_USERDatabase db = (TAB_DATA_USERDatabase)("Database", "DataBase.TAB_DATA_USERDatabase");
            //this.MyDst = db.SelectComb(int.Parse(StrRtn));
        }
    }
}