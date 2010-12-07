using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
using xm_mis.Main;
namespace xm_mis.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string url = Session["backUrl"] as string;

                Session.Clear();
                if (!string.IsNullOrEmpty(url))
                {
                    Session["backUrl"] = url;
                }
                else
                {
                }
            }

            //BeckSendMail.getMM().NewMail("vampiler@163.com", "mis系统票务通知", "开票申请已通过审批，请尽快完成后续工作" + Request.Url.AbsoluteUri + "/Main/paymentReceiptManager/receiptOk.aspx");
            //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void loginCon_Authenticate(object sender, AuthenticateEventArgs e)
        {
            #region dataset
            DataSet dataSet = new DataSet();
            DataRow userRow = null;

            DataColumn colName = new DataColumn("usrName", System.Type.GetType("System.String"));
            //DataColumn colAuth = new DataColumn("totleAuthority", System.Type.GetType("System.Int32"));
            DataColumn colPwd = new DataColumn("usrPassWord", System.Type.GetType("System.String"));
            //DataColumn colId = new DataColumn("usrId", System.Type.GetType("System.Int32"));
            //DataColumn colEnd = new DataColumn("endTime", System.Type.GetType("System.DateTime"));

            DataTable userTable = new DataTable("tbl_usr");

            //colName.DataType = System.Type.GetType("System.String");
            //colAuth.DataType = System.Type.GetType("System.Int32");
            //colPwd.DataType = System.Type.GetType("System.String");
            //colId.DataType = System.Type.GetType("System.Int32");

            //colName.ColumnName = "usrName";
            //colPwd.ColumnName = "usrPassWord";
            //colId.ColumnName = "usrId";
            //colAuth.ColumnName = "totleAuthority";

            userTable.Columns.Add(colName);
            //userTable.Columns.Add(colAuth);
            userTable.Columns.Add(colPwd);
            //userTable.Columns.Add(colId);
            //userTable.Columns.Add(colEnd);

            userRow = userTable.NewRow();
            userRow["usrName"] = loginCon.UserName.ToString().Trim();
            userRow["usrPassWord"] = loginCon.Password.ToString().Trim();
            //userRow["endTime"] = DateTime.Today;
            userTable.Rows.Add(userRow);

            dataSet.Tables.Add(userTable);
            #endregion


            //Xm_db xmDataCont = Xm_db.GetInstance();

            //var a =
            //    from b in xmDataCont.Tbl_usr
            //    where b.RealName.Equals("joker")
            //    select b;

            //int usrId = a.First().Tbl_usr_title.Where(p => p.UsrId == a.First().UsrId).Count(p => p.Tbl_title.EndTime > DateTime.Now);

            UserProcess myLogin = new UserProcess(dataSet);

            myLogin.DoLogin();
            int rowRtn = myLogin.IntRtn;

            if (0 != rowRtn)
            {
                using (DataTable dt =
                            myLogin.MyDst.Tables["tbl_usr"].DefaultView.ToTable())
                {
                    string strUsrAuth = dt.Rows[0]["totleAuthority"].ToString();
                    AuthAttributes usrAuthAttr;
                    Enum.TryParse<AuthAttributes>(strUsrAuth, out usrAuthAttr);

                    //Session["totleAuthority"] =
                    //    dt.Rows[0]["totleAuthority"].ToString();
                    Session["totleAuthority"] =
                        usrAuthAttr;
                    Session["usrId"] =
                        dt.Rows[0]["usrId"].ToString();

                    string strRealName =
                        dt.Rows[0]["realName"].ToString();
                    FormsAuthentication.SetAuthCookie(strRealName, false);

                    string backUrl = Session["backUrl"] as string;
                    string continueUrl = "~/Main/DefaultMainSite.aspx";//Request.QueryString["ReturnUrl"];
                    if (!String.IsNullOrEmpty(backUrl))
                    {
                        continueUrl = backUrl;
                    }
                    else
                    {
                    }
                    Response.Redirect(continueUrl);
                    //aspxName = myLogin.StrRtn + "Main.aspx";
                    //Server.Transfer(aspxName);
                }
            }
            else
            {

            }
        }
    }
}
