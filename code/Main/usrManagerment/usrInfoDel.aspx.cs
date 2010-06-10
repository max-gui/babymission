using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

public partial class Main_usrManagerment_usrInfoDel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(null == Session["totleAuthority"]))
        {
            int usrAuth = 0;
            string strUsrAuth = Session["totleAuthority"].ToString().Trim();
            usrAuth = int.Parse(strUsrAuth);
            int flag = 0x1 << 3;

            if ((usrAuth & flag) == 0)
                Response.Redirect("~/Main/NoAuthority.aspx");
        }
        else
        {
            Response.Redirect("~/Account/Login.aspx");
        }

        //DataSet MyDst = new DataSet();
        //SelfDepartProcess myView = new SelfDepartProcess(MyDst);


        if (!IsPostBack)
        {
            DataSet upDst = new DataSet();
            UserProcess upView = new UserProcess(upDst);

            upView.View();
            DataTable upTable = upView.MyDst.Tables["view_usr_departTitle"];
            
            Session["UserProcess"] = upView;
            Session["upDtSources"] = upTable;
            //if (Session["PAGESIZE"] != null)
            //{
            //    SelfDepartGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
            //    SelfDepartGV.DataBind();
            //}
            //else
            //{
            //}
            usrGV.DataSource = Session["upDtSources"];//["dtSources"] as DataTable;
            //string[] strKeyNames = new string[1];
            //strKeyNames[0] = "departmentName";
            //SelfDepartGV.DataKeyNames = strKeyNames;
            usrGV.DataBind();
        }
    }
    protected void  usrGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int tbIndex = usrGV.Rows[e.RowIndex].DataItemIndex;

        DataTable dt = (DataTable)Session["dtSources"];
        int usrId = int.Parse(dt.Rows[tbIndex]["usrId"].ToString().Trim());
        //        if (usrGV.SelectedIndex == -1)
        //        {
        //            e.Cancel = false;
        //            int index = e.NewSelectedIndex;
        //            int tbIndex = usrGV.Rows[index].DataItemIndex;

        //            DataTable dt = (DataTable)Session["dtSources"];
        //            int usrId = int.Parse(dt.Rows[tbIndex]["usrId"].ToString().Trim());


        //            Session["error"] = bool.FalseString.ToString().Trim();
        //            dt.DefaultView[row.DataItemIndex].Row["departmentName"] = strTxt;
        //            SelfDepartGV.EditIndex = -1;
        //            SelfDepartGV.Columns[0].Visible = true;
        //            btnOk.Enabled = true;
        //            }

        //            SelfDepartGV.DataSource = Session["dtSources"] as DataTable;
        //            SelfDepartGV.DataBind();





        //            Button btnOk = (usrGV.Rows[index].FindControl("btnDel") as Button);
        //            btnOk.Visible = true;
        //        }
        //        else
        //        {
        //            e.Cancel = true;


        //}
    }
    protected void usrGV_Sorting(object sender, GridViewSortEventArgs e)
    {

    }
    protected void usrGV_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void  btnOk_Click(object sender, EventArgs e)
    {

    }
}