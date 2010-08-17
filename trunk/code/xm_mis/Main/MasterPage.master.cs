using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace xm_mis.Main
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void MainMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            string strUsrAuth = Session["totleAuthority"] as string;
            string usrId = Session["usrId"] as string;
            Session.Clear();

            Session["totleAuthority"] = strUsrAuth;
            Session["usrId"] = usrId;

            string s = e.Item.Value.ToString().Trim();
            string strUrl = string.Empty;
            switch (s)
            {
                case ("departEdit"):
                    strUrl = "~/Main/self_depart_title/selfDep/SelfDepartment.aspx";
                    break;
                case ("titleEdit"):
                    strUrl = "~/Main/self_depart_title/selfTitle/SelfTitle.aspx";
                    break;
                case ("usrInfoModify"):
                    strUrl = "~/Main/usrManagerment/usrInfoManagerment.aspx";
                    break;
                case ("usrAuth"):
                    strUrl = "~/Main/usrManagerment/usrAuthManagerment.aspx";
                    break;
                case ("usrDepartTitle"):
                    strUrl = "~/Main/usrManagerment/usrDepartTitleManagerment.aspx";
                    break;               
                case ("selfPwdModify"):
                    strUrl = "~/Main/usrSelfModify/usrSelfPwdModify.aspx";
                    break;
                case ("selfContectModify"):
                    strUrl = "~/Main/usrSelfModify/usrSelfContactModify.aspx";
                    break;
                //case ("custCompAdd"):
                //    strUrl = "~/Main/custInfoManager/custCompManager/custCompAdd.aspx";
                //    break;
                //case ("custManAdd"):
                    //strUrl = "~/Main/custInfoManager/custCompManager/custManAdd.aspx";
                    //break;
                case ("custCompEdit"):
                    strUrl = "~/Main/custInfoManager/custCompManager/custCompEdit.aspx";
                    break;
                case ("supplierEdit"):
                    strUrl = "~/Main/custInfoManager/supplierManager/supplierEdit.aspx";
                    break;
                case ("projectSearch"):
                    strUrl = "~/Main/projectTagInfoManager/projectSearch.aspx";
                    break;
                case ("newProject"):
                    strUrl = "~/Main/projectTagInfoManager/projectTagAdd.aspx";
                    break;
                case ("productView"):
                    strUrl = "~/Main/stockInfoManager/productInfoManager/productView.aspx";
                    break;
                case ("contractView"):
                    strUrl = "~/Main/contractManager/contractView.aspx";
                    break;
                case ("addContract"):
                    strUrl = "~/Main/contractManager/addContract.aspx";
                    break;
                case ("subContractEdit"):
                    strUrl = "~/Main/contractManager/subContractEdit.aspx";
                    break;
                case ("mainContractReceiptView"):
                    strUrl = "~/Main/paymentReceiptManager/mainContractReceiptView.aspx";
                    break;
                case ("receiptView"):
                    strUrl = "~/Main/infoViewManager/receiptView.aspx";
                    break;
                case ("paymentView"):
                    strUrl = "~/Main/infoViewManager/paymentInfo/paymentView.aspx";
                    break;
            }
            Response.Redirect(strUrl);
        }
    }
}