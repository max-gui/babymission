using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.paymentReceiptManager
{
    public partial class mainContractReceiptView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (!(null == Session["totleAuthority"]))
            //{
            //    int usrAuth = 0;
            //    string strUsrAuth = Session["totleAuthority"] as string;
            //    usrAuth = int.Parse(strUsrAuth);
            //    int flag = 0x11 << 6;

            //    if ((usrAuth & flag) == 0)
            //        Response.Redirect("~/Main/NoAuthority.aspx");
            //}
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];
                
                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.pay_receiptApply | AuthAttributes.pay_receiptOk);
                if (!flag)
                {
                    Response.Redirect("~/Main/NoAuthority.aspx");
                }
            }
            else
            {
                string url = Request.FilePath;
                Session["backUrl"] = url;
                Response.Redirect("~/Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                //string strUsrId = Session["usrId"] as string;

                //int usrId = int.Parse(strUsrId);

                #region mainContractGV
                Xm_db xmDataCont = Xm_db.GetInstance();

                var mailVar = reflash(xmDataCont);
                
                mainContractGV.DataSource = Session["dtSources"];
                mainContractGV.DataBind();
                #endregion

                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                //bool flag = usrAuthAttr.hasOneFlag(AuthAttributes.pay_receiptApply | AuthAttributes.pay_receiptOk);
                
                if (usrAuthAttr.HasOneFlag(AuthAttributes.pay_receiptApply))
                {
                    mainContractGV.Columns[7].Visible = false;
                }
                else if (usrAuthAttr.HasOneFlag(AuthAttributes.pay_receiptOk))
                {
                    mainContractGV.Columns[9].Visible = false; 
                }
            }
        }

        //protected void dt_modify(DataTable dt, string strFilter)
        //{
        //    //DataColumn colSelfReceipt = new DataColumn("selfReceipt", System.Type.GetType("System.String"));
        //    //dt.Columns.Add(colSelfReceipt);

        //    dt.DefaultView.RowFilter = strFilter;

        //    //float selfReceiptPercent = 0;
        //    //foreach (DataRow dr in dt.Rows)
        //    //{
        //    //    selfReceiptPercent = float.Parse(dr["selfReceiptPercent"].ToString());
        //    //    dr["selfReceipt"] = selfReceiptPercent.ToString("p");
        //    //}
        //}

        protected void mainContractGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            mainContractGV.PageIndex = e.NewPageIndex;

            mainContractGV.DataSource = Session["dtSources"];
            mainContractGV.DataBind();
        }

        protected void mainContractGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void mainContractGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int index = e.Row.RowIndex;

                Button btn = e.Row.FindControl("btnRecieptApply") as Button;

                if (null != btn)
                {
                    btn.CommandArgument = index.ToString();
                }

                DataTable dt = Session["dtSources"] as DataTable;

                float num = float.Parse(dt.DefaultView[index]["selfReceivingPercent"].ToString());

                DropDownList ddl = e.Row.FindControl("ddlPay") as DropDownList;

                if (null != ddl)
                {
                    //ListItemCollection lic = new ListItemCollection();
                    string strValue = string.Empty;
                    string strText = string.Empty;
                    for (float i = num;i < 1.05f;i = i + 0.05f)
                    {
                        strValue = i.ToString();

                        strText = i.ToString("p");
                        ddl.Items.Add(strText);
                        ddl.Items.FindByText(strText).Value = strValue;
                        //lic.Add(strText);
                        //lic.FindByText(strText).Value = strValue;
                    }

                    //ddl.DataSource = lic;
                    //ddl.DataBind();
                    ddl.SelectedValue = num.ToString();
                }
            }
        }        

        protected void mainContractGV_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string strCNM = e.CommandName;

            if (strCNM.Equals("payEdit"))
            {
                GridViewRow gvr = (e.CommandSource as LinkButton).Parent.Parent as GridViewRow;

                int index = gvr.RowIndex;
                string strIndex = index.ToString();
                DropDownList ddl = mainContractGV.Rows[index].FindControl("ddlPay") as DropDownList;
                if (null != ddl)
                {
                    ddl.Enabled = true;
                    
                    //ddl.Enabled = false;

                    mainContractGV.Columns[7].Visible = false;
                    mainContractGV.Columns[9].Visible = false;
                    mainContractGV.Columns[10].Visible = false;

                    btnOk.Visible = true;
                    btnOk.CommandArgument = strIndex;
                    btnNo.Visible = true;
                    btnNo.CommandArgument = strIndex;
                }
            }
        }

        protected void btnRecieptApply_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int index = int.Parse(btn.CommandArgument);
            int numIndex = mainContractGV.Rows[index].DataItemIndex;

            DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            DataRow dr = dt.Rows[numIndex];

            Session["seldMainContract"] = dr;

            Response.Redirect("~/Main/paymentReceiptManager/receiptApply.aspx");
        }

        protected void btnSubContractEdit_Click(object sender, EventArgs e)
        {
            GridViewRow dvr = (sender as Button).Parent.Parent as GridViewRow;
            int index = dvr.RowIndex;
            int numIndex = mainContractGV.Rows[index].DataItemIndex;

            DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();

            DataRow dr = dt.Rows[numIndex];

            Session["seldMainContract"] = dr;

            Response.Redirect("~/Main/paymentReceiptManager/subContractPaymentView.aspx");
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            int index = int.Parse(btnOk.CommandArgument);            

            int dataIndex = mainContractGV.Rows[index].DataItemIndex;
            DataTable dt = (DataTable)Session["dtSources"];

            GridViewRow row = mainContractGV.Rows[index];
            DropDownList ddlPay = row.FindControl("ddlPay") as DropDownList;
            System.Nullable<float> payPercent = float.Parse(ddlPay.SelectedValue);

            System.Nullable<int> mainContractId = int.Parse(dt.DefaultView[dataIndex].Row["mainContractId"].ToString());

            //MainContractProcess mcp = Session["MainContractProcess"] as MainContractProcess;

            //mcp.MainContractPayPercentUpdate(mainContractId, payPercent);            

            //mcp.RealmainContractProjectView();

            //DataTable taskTable = mcp.MyDst.Tables["view_mainContract_project_usr"];

            //string strUsrId = Session["usrId"] as string;
            
            //int usrId = int.Parse(strUsrId);
            Xm_db xmDataCont = Xm_db.GetInstance();

            try
            {
                xmDataCont.MainContract_payPercent_update(mainContractId, payPercent);
                            
                xmDataCont.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);

                var mailVar = reflash(xmDataCont);

                sendMail(mainContractId, mailVar);
            }
            catch (System.Data.Linq.ChangeConflictException cce)
            {
                string strEx = cce.Message;
                foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataCont.ChangeConflicts)
                {
                    occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                }

                xmDataCont.MainContract_payPercent_update(mainContractId, payPercent);

                xmDataCont.SubmitChanges();

                var mailVar = reflash(xmDataCont);

                sendMail(mainContractId, mailVar);
            }

            mainContractGV.DataSource = Session["dtSources"];
            mainContractGV.DataBind();

            Button btn = null;
            btn = sender as Button;
            btn.Visible = false;
            btn = btnNo;
            btn.Visible = false;
            mainContractGV.Columns[7].Visible = true;
            mainContractGV.Columns[9].Visible = true;
            mainContractGV.Columns[10].Visible = true;
        }

        private IQueryable<View_mainContract_project_usr> reflash(Xm_db xmDataCont)
        {
            var ViewMainContract_project_usr =
                from mainContract_project_usr in xmDataCont.View_mainContract_project_usr
                where mainContract_project_usr.EndTime > DateTime.Now
                select mainContract_project_usr;

            DataTable taskTable = ViewMainContract_project_usr.ToDataTable();

            Session["dtSources"] = taskTable;

            return ViewMainContract_project_usr;
        }

        private static void sendMail(System.Nullable<int> mainContractId, IQueryable<View_mainContract_project_usr> ViewMainContract_project_usr)
        {
            var emailDetail = ViewMainContract_project_usr.First(elm => elm.MainContractId == mainContractId);
            string usrEmail = emailDetail.UsrEmail;
            string projectTag = emailDetail.ProjectTag;
            string num = emailDetail.SelfReceivingPercent.ToString("p");

            //DataTable dtSources = projectStepEdit.Distinct().ToDataTable();

            BeckSendMail.getMM().NewMail(usrEmail, "mis系统票务通知", projectTag + "项目总共收到客户付款额" + num);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            int index = int.Parse(btnNo.CommandArgument);

            DropDownList ddl = mainContractGV.Rows[index].FindControl("ddlPay") as DropDownList;
            ddl.Enabled = false;

            mainContractGV.Columns[7].Visible = true;
            mainContractGV.Columns[9].Visible = true;
            mainContractGV.Columns[10].Visible = true;

            btnOk.Visible = false;
            btnNo.Visible = false;
        }
    }
}