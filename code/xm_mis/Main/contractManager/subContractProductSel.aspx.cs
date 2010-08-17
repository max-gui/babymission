﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
namespace xm_mis.Main.contractManager
{
    public partial class subContractProductSel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                int usrAuth = 0;
                string strUsrAuth = Session["totleAuthority"] as string;
                usrAuth = int.Parse(strUsrAuth);
                int flag = 0x5 << 4;

                if ((usrAuth & flag) == 0)
                    Response.Redirect("~/Main/NoAuthority.aspx");
            }
            else
            {
                Response.Redirect("~/Account/Login.aspx");
            }

            if (!IsPostBack)
            {
                if (null == Session["subProductSelDs"])
                {
                    DataTable dtMainContractProduct = (Session["mainContractProductDtSources"] as DataTable).DefaultView.ToTable();
                    //DataTable subProductTable = (Session["dtSources"] as DataTable).DefaultView.ToTable();

                    DataRow sessionDr = Session["selMainContractDr"] as DataRow;
                    string mainContractId = sessionDr["mainContractId"].ToString();
                    string strFilter =
                        " mainContractId = " + "'" + mainContractId + "'" +
                        " and hasSupplier = " + "'" + bool.FalseString + "'";
                    dtMainContractProduct.DefaultView.RowFilter = strFilter;
                    dtMainContractProduct = dtMainContractProduct.DefaultView.ToTable();

                    DataColumn colCheck = new DataColumn("checkOrNot", System.Type.GetType("System.Boolean"));
                    dtMainContractProduct.Columns.Add(colCheck);
                    Session["subProductSelDs"] = dtMainContractProduct.DefaultView.ToTable();                    
                    
                    productSelGV.DataSource = Session["subProductSelDs"];
                    productSelGV.DataBind();

                }
                else
                {
                    DataTable dt = Session["subProductSelDs"] as DataTable;

                    string end = DateTime.Now.ToShortDateString();
                    string strFilter =
                        " endTime > " + "'" + end + "'";
                    dt.DefaultView.RowFilter = strFilter;

                    productSelGV.DataSource = dt;
                    productSelGV.DataBind();
                }
            }
        }

        protected void productSelGV_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            productSelGV.PageIndex = e.NewPageIndex;

            productSelGV.DataSource = Session["subProductSelDs"];//["dtSources"] as DataTable;  
            productSelGV.DataBind();
        }

        protected void productSelGV_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void productSelGV_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBox cb = e.Row.Cells[3].Controls[0] as CheckBox;
                if (cb != null)
                {
                    cb.Enabled = true;
                }
            }
        }

        protected void btnOk_Click(object sender, EventArgs e)
        {
            DataTable dt = Session["subProductSelDs"] as DataTable;

            int index = -1;
            CheckBox cb = null;
            foreach (GridViewRow row in productSelGV.Rows)
            {
                index = row.DataItemIndex;

                cb = row.Cells[3].Controls[0] as CheckBox;
                dt.Rows[index]["checkOrNot"] = cb.Checked;
            }

            dt.AcceptChanges();
            Session["subProductSelDs"] = dt;

            Response.Redirect("~/Main/contractManager/subContractAdd.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Main/contractManager/subContractAdd.aspx");
        }
    }
}