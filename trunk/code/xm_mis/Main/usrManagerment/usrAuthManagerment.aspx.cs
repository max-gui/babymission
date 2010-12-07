using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using xm_mis.db;
namespace xm_mis.Main.usrManagerment
{
    public partial class usrAuthManagerment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.systemManager);
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
                Xm_db xmDataCont = Xm_db.GetInstance();

                var usrAuthView =
                    from usrAuth in xmDataCont.View_usr
                    where usrAuth.UsrEd > DateTime.Now
                    select new { usrAuth.UsrId, usrAuth.UsrName, usrAuth.RealName, usrAuth.DepartmentName, usrAuth.TitleName, usrAuth.TotleAuthority };

                DataTable taskTable = usrAuthView.Distinct().ToDataTable();

                var authView =
                    from authInfo in xmDataCont.Tbl_authority
                    select authInfo;

                DataTable auTable = authView.ToDataTable();
                
                Session["dtSources"] = taskTable;
                Session["auTable"] = auTable;
                
                usrGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;
                usrGV.DataBind();
            }
        }

        protected void usrGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void toEdit_Click(object sender, EventArgs e)
        {
            LinkButton lkb = sender as LinkButton;
            lkb.Visible = false;

            GridViewRow gvr = lkb.Parent.Parent as GridViewRow;
            usrGV.SelectedIndex = gvr.RowIndex;

            CheckBoxList cbl = gvr.FindControl("cblUsrAuth") as CheckBoxList;
            cbl.Visible = true;

            btnAccept.Visible = true;
            this.btnCancel.Visible = true;

            usrGV.Columns[6].Visible = true;
            usrGV.Columns[7].Visible = false;
        }
        
        protected void usrGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataTable dtAu = (Session["auTable"] as DataTable).DefaultView.ToTable();

                CheckBoxList cbl = e.Row.FindControl("cblUsrAuth") as CheckBoxList;
                if (cbl != null)
                {
                    cbl.DataSource = dtAu;
                    cbl.DataTextField = "authorityName".ToString();
                    cbl.DataValueField = "authority".ToString();
                    cbl.DataBind();
                }

                Label lbl = e.Row.FindControl("lblTotAuth") as Label;
                string totleAuthority = lbl.Text;

                AuthAttributes authAttr;
                Enum.TryParse<AuthAttributes>(totleAuthority, out authAttr);
                var authList = authAttr.ToString().Split(",".ToCharArray()).TakeWhile(element => !element.Equals(AuthAttributes.unKnow.ToString()));

                AuthAttributes authAttrElement;
                string strElement = string.Empty;
                ListItem li = null;
                foreach (var auth in authList)
                {
                    Enum.TryParse<AuthAttributes>(auth, out authAttrElement);
                    strElement = authAttrElement.ToString("d");

                    li = cbl.Items.FindByValue(strElement);
                    li.Selected = true;
                    
                }
            }
        }     

        protected void usrGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            usrGV.PageIndex = e.NewPageIndex;

            usrGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;  
            usrGV.DataBind();
        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {
            int index = usrGV.SelectedIndex;
            Label lblUsrId = usrGV.Rows[index].FindControl("lblUsrId") as Label;
            CheckBoxList cbl = usrGV.Rows[index].FindControl("cblUsrAuth") as CheckBoxList;

            AuthAttributes authAttr = AuthAttributes.unKnow;
            AuthAttributes authAttrTemp;
            foreach (ListItem li in cbl.Items)
            {
                if (li.Selected)
                {
                    Enum.TryParse<AuthAttributes>(li.Value, out authAttrTemp);
                    authAttr = authAttr.Set(authAttrTemp);
                }
                else
                {                    
                }
            }

            int usrId = int.Parse(lblUsrId.Text);

            Xm_db xmDataCont = Xm_db.GetInstance();

            var usrAuthInfo =
                from usr in xmDataCont.Tbl_usr
                where usr.EndTime > DateTime.Now &&
                      usr.UsrId == usrId
                select usr;

            usrAuthInfo.First().TotleAuthority = (Int32)authAttr;

            try
            {
                xmDataCont.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
            }
            catch (System.Data.Linq.ChangeConflictException cce)
            {
                string strEx = cce.Message;
                foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataCont.ChangeConflicts)
                {
                    occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                }

                xmDataCont.SubmitChanges();
            }

            var usrAuthView =
                from usrAuth in xmDataCont.View_usr
                where usrAuth.UsrEd > DateTime.Now
                select new { usrAuth.UsrId, usrAuth.UsrName, usrAuth.RealName, usrAuth.DepartmentName, usrAuth.TitleName, usrAuth.TotleAuthority };

            DataTable taskTable = usrAuthView.Distinct().ToDataTable();
                        
            Session["dtSources"] = taskTable;

            usrGV.DataSource = Session["dtSources"];
            usrGV.DataBind();

            usrGV.SelectedIndex = -1;

            cbl.Visible = false;

            LinkButton lkb = usrGV.Rows[index].FindControl("toEdit") as LinkButton;
            lkb.Visible = true;

            btnAccept.Visible = false;
            btnCancel.Visible = false;

            usrGV.Columns[7].Visible = true;
            usrGV.Columns[6].Visible = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            int index = usrGV.SelectedIndex;


            CheckBoxList cbl = usrGV.Rows[index].FindControl("cblUsrAuth") as CheckBoxList;
            cbl.Visible = false;

            LinkButton lkb = usrGV.Rows[index].FindControl("toEdit") as LinkButton;
            lkb.Visible = true;

            btnAccept.Visible = false;
            btnCancel.Visible = false;

            usrGV.DataSource = Session["dtSources"];//["dtSources"] as DataTable;  
            usrGV.DataBind();

            usrGV.SelectedIndex = -1;

            usrGV.Columns[7].Visible = true;
            usrGV.Columns[6].Visible = false;
        }
    }
}