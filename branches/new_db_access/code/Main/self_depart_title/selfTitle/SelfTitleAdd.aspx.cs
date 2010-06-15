using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
public partial class Main_self_depart_title_selfTitle_SelfTitleAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected string input_check(string titleName)
    {
        DataTable dt = (Session["dtSources"] as DataTable).DefaultView.ToTable();
        DataColumn[] key = new DataColumn[1];
        key[0] = dt.Columns["titleName"];

        dt.PrimaryKey = key;

        dt.Rows.Contains(titleName);

        string strRtn = string.Empty;

        if (string.IsNullOrWhiteSpace(titleName))
        {
            strRtn = "职位名称不能为空！";
        }
        else if (titleName.Length > 25)
        {
            strRtn = "职位名称不能超过25个字！";
        }
        else if (dt.Rows.Contains(titleName))
        {
            strRtn = "职位名称不能重复！";
        }
        else if (titleName.Equals("职位名称不能为空！"))
        {
            strRtn = "职位名称不能为空！  ";
        }
        else if (titleName.Equals("职位名称不能超过25个字！"))
        {
            strRtn = "职位名称不能超过25个字！  ";
        }
        else if (titleName.Equals("职位名称不能重复！"))
        {
            strRtn = "职位名称不能重复！  ";
        }
        else
        {
            strRtn = titleName;
        }

        return strRtn;
    }

    protected void btnAccept_Click(object sender, EventArgs e)
    {
        SelfTitleProcess stp = Session["SelfTitleProcess"] as SelfTitleProcess;

        string newTitleName = txtTitleName.Text.ToString().Trim();

        string strCheck = newTitleName;

        newTitleName = input_check(strCheck.Trim());
        if (newTitleName.Equals(strCheck))
        {
            stp.SelfTitleAdd(newTitleName);

            Response.Redirect("~/Main/self_depart_title/selfTitle/SelfTitle.aspx");
        }
        else
        {
            txtTitleName.Text = newTitleName;
        }
    }
    protected void btnNo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Main/self_depart_title/selfTitle/SelfTitle.aspx");
    }
}