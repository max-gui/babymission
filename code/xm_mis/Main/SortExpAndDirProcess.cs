using System;
using System.Web.UI;
using System.Text;

namespace xm_mis.Main
{
    static public class SortExpAndDirProcess
    {    
        static public string GetSortDirectionExpression(this string sortExpression, StateBag stBg)
        {
            StringBuilder sb = new StringBuilder(30);
            sb.Append(sortExpression).Append(" ");
            string sortDirection = stBg[sortExpression] as string;
                
            if (sortDirection == null ? true :sortDirection.Equals("ASC"))
            {
                sb.Append("DESC");
                stBg[sortExpression] = "DESC";
            }
            else
            {
                sb.Append("ASC");
                stBg[sortExpression] = "ASC";
            }            

            return sb.ToString();
        }
    }

    static public class ShowWindow
    {
        static public void ShowAlertWindow(this ClientScriptManager pageClientScriptManager, string alertString, Type scriptType)
        {
            StringBuilder sb = new StringBuilder(50);

            sb.Append(" <script> window.alert('").Append(alertString).Append("!'); </script> ");

            if (!pageClientScriptManager.IsClientScriptBlockRegistered(scriptType, alertString))
            {
                pageClientScriptManager.RegisterClientScriptBlock(scriptType, alertString, sb.ToString());
            }
        }
    }
}