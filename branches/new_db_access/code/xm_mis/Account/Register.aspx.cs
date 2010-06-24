using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.App_Code.logic;
namespace xm_mis.Account
{
    public partial class Register : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //        txtName.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
            if (!IsPostBack)
            {

            }
        }

        /*
        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(this.txtUsrName.Text.ToString().Trim(), false);

            string continueUrl = Request.QueryString["ReturnUrl"];
            if (String.IsNullOrEmpty(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }
        */
        protected void btnReg_Click(object sender, EventArgs e)
        {
            if (inputCheck())
            {
                string sn = txtName.Text.ToString().Trim();
                string sun = txtUsrName.Text.ToString().Trim();
                string spw = txtPassWord.Text.ToString().Trim();
                string sc = txtContact.Text.ToString().Trim();
                //int sc = int.Parse(txtContact.Text.ToString().Trim());

                #region dataset
                DataSet dataSet = new DataSet();
                DataRow userRow = null;

                DataColumn colName = new DataColumn("realName", System.Type.GetType("System.String"));
                DataColumn colUsrName = new DataColumn("usrName", System.Type.GetType("System.String"));
                DataColumn colContact = new DataColumn("usrContact", System.Type.GetType("System.String"));
                DataColumn colPwd = new DataColumn("usrPassWord", System.Type.GetType("System.String"));

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
                userTable.Columns.Add(colUsrName);
                userTable.Columns.Add(colContact);
                userTable.Columns.Add(colPwd);

                userRow = userTable.NewRow();
                userRow["realName"] = sn;
                userRow["usrName"] = sun;
                userRow["usrPassWord"] = spw;
                userRow["usrContact"] = sc;
                userTable.Rows.Add(userRow);

                dataSet.Tables.Add(userTable);
                #endregion

                UserProcess up = new UserProcess(dataSet);

                up.DoCheckUsrName();
                int rowRtn = up.IntRtn;
                if (0 == rowRtn)
                {
                    using (DataTable dt =
                                up.MyDst.Tables["tbl_usr"].DefaultView.ToTable("addTable"))
                    {
                        DataRow dr = dt.NewRow();
                        dr["realName"] = sn;
                        dr["usrName"] = sun;
                        dr["usrPassWord"] = spw;
                        dr["usrContact"] = sc;

                        dt.Rows.Add(dr);
                        up.MyDst.Tables.Add(dt);
                        //up.MyDst.Tables["tbl_usr"].Rows.Add(dr);

                        up.Add();
                        //up.commit();

                        int nullAuth = 0;
                        Session["totleAuthority"] = nullAuth;
                        Session["usrId"] = up.StrRtn;

                        FormsAuthentication.SetAuthCookie(sn, false);

                        string continueUrl = "~/Main/DefaultMainSite.aspx";//Request.QueryString["ReturnUrl"];

                        Response.Redirect(continueUrl);
                        //aspxName = myLogin.StrRtn + "Main.aspx";
                        //Server.Transfer(aspxName);
                    }
                }
                else
                {
                    lblUsrName.Text = "用户名已存在!";
                }

                //ConnectionStringSettings cfg =
                //    ConfigurationManager.ConnectionStrings["My DB"];
                //DbProviderFactory factory =
                //    DbProviderFactories.GetFactory(cfg.ProviderName);
                //DbConnection da = factory.CreateConnection();
                //DbCommand dcmd = factory.CreateCommand();
                //da.ConnectionString = cfg.ConnectionString;
                //dcmd.Connection = da;
                //dcmd.CommandText = "select * from tbl_usr";

                /*
                using (SqlConnection cnx = 
                    new SqlConnection(cfg.ConnectionString.ToString().Trim()))
                {*/
                //using (SqlDataAdapter dAdapter = new SqlDataAdapter())
                //{
                //    dAdapter.SelectCommand = dcmd as SqlCommand;
                //    /*
                //    dAdapter.SelectCommand = new SqlCommand(
                //        "select * from tbl_usr",cnx );
                //     * */
                //    dAdapter.Fill(dSet, "tbl_usr");

                //}
                ////}

                //DataColumn dCol1 = dSet.Tables["tbl_usr"].Columns["usrName"];

                //string strTemp = dSet.Tables["tbl_usr"].Rows[0]["usrName"].ToString().Trim();




                //string continueUrl = Request.QueryString["ReturnUrl"];
                //if (String.IsNullOrEmpty(continueUrl))
                //{
                //    continueUrl = "~/";
                //}
                //Response.Redirect(continueUrl);
            }
        }
        protected bool txtName_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtName.Text.ToString().Trim()))
            {
                lblName.Text = "*必填项!";
                flag = false;
            }
            else if (txtName.Text.ToString().Trim().Length > 20)
            {
                lblName.Text = "真实姓名太长!";
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblName.Text = string.Empty;
                //Session["flagUsrName"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }
        protected bool txtUsrName_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtUsrName.Text.ToString().Trim()))
            {
                lblUsrName.Text = "*必填项!";
                flag = false;
            }
            else if (txtUsrName.Text.ToString().Trim().Length > 20)
            {
                lblUsrName.Text = "用户名太长!";
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblUsrName.Text = string.Empty;
                //Session["flagUsrName"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }
        protected bool txtPassWord_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtPassWord.Text.ToString().Trim()))
            {
                lblPassWord.Text = "*必填项!";
                flag = false;
            }
            else if (txtPassWord.Text.ToString().Trim().Length > 10)
            {
                lblPassWord.Text = "密码太长!";
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblPassWord.Text = string.Empty;
                //Session["flagPassWord"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }
        protected bool txtRPassWord_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtRPassWord.Text.ToString().Trim()))
            {
                lblRPassWord.Text = "*必填项!";
                flag = false;
            }
            else if (!(txtRPassWord.Text.ToString().Equals(txtPassWord.Text.ToString())))
            {
                lblRPassWord.Text = "两次输入的密码不同!";
                //Session["flagUsrName"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                lblRPassWord.Text = string.Empty;
                //Session["flagPassWord"] = bool.TrueString.ToString().Trim();
                //btnOk();
            }

            return flag;
        }
        protected bool txtContact_TextCheck()
        {
            bool flag = true;
            if (string.IsNullOrWhiteSpace(txtContact.Text.ToString().Trim()))
            {
                lblContact.Text = "*必填项!";
                //Session["flagContact"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else if (txtContact.Text.ToString().Trim().Length != 11)
            {
                lblContact.Text = "手机号码应为11位!";
                //Session["flagContact"] = bool.FalseString.ToString().Trim();
                flag = false;
            }
            else
            {
                try
                {
                    long sc = long.Parse(txtContact.Text.ToString().Trim());
                    lblContact.Text = string.Empty;
                    //Session["flagContact"] = bool.TrueString.ToString().Trim();
                }
                catch (FormatException e)
                {
                    lblContact.Text = "手机号码只能包含数字!";
                    //Session["flagContact"] = bool.FalseString.ToString().Trim();
                    Console.WriteLine("{0} Exception caught.", e);
                    flag = false;
                }
            }

            //btnOk();

            return flag;
        }
        //protected void btnOk()
        //{
        //    btnReg.Enabled = false;

        //    if (Session["flagName"].Equals(bool.FalseString.ToString().Trim()))
        //    {            
        //    }
        //    else if (Session["flagUsrName"].Equals(bool.FalseString.ToString().Trim()))
        //    {
        //    }
        //    else if (Session["flagPassWord"].Equals(bool.FalseString.ToString().Trim()))
        //    {
        //    }
        //    else if (Session["flagContact"].Equals(bool.FalseString.ToString().Trim()))
        //    {
        //        btnReg.Enabled = true;
        //    }
        //}
        protected bool inputCheck()
        {
            bool flag = true;
            if (!txtName_TextCheck())
            {
                flag = false;
            }
            else if (!txtUsrName_TextCheck())
            {
                flag = false;
            }
            else if (!txtPassWord_TextCheck())
            {
                flag = false;
            }
            else if (!txtRPassWord_TextCheck())
            {
                flag = false;
            }
            else if (!txtContact_TextCheck())
            {
                flag = false;
            }

            return flag;
        }

    }
}
