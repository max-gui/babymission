using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using System.IO;
namespace xm_mis.Main.stockInfoManager.productStockView
{
    public partial class productStockView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnRtn_Click(object sender, EventArgs e)
        {
            int usrId = int.Parse(Session["usrId"] as string);

            DataSet myDst = new DataSet();
            ProductStockProcess pspView = new ProductStockProcess(myDst);

            pspView.RealProductStockCheckManView();
            DataTable taskTable = pspView.MyDst.Tables["view_productStockCheckMan"].DefaultView.ToTable();

            string strFilter =
                " usrId = " + usrId;

            DataRow dr = (DataRow)taskTable.Rows[0]; 
            Response.Buffer = true; 
　　　　    Response.Clear();
            Response.ContentType = dr["contentType"].ToString();
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(dr["checkTextName"].ToString(), System.Text.Encoding.UTF8));
            Response.BinaryWrite((Byte[])dr["productCheckText"]);
            Response.Flush();
            Response.End();
        }
    }
}