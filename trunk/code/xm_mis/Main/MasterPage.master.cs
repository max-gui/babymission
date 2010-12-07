using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using xm_mis.logic;
namespace xm_mis.Main
{
    public partial class MasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void MainMenu_MenuItemClick(object sender, MenuEventArgs e)
        {
            string urlTarget = e.Item.Value.ToString().Trim();

            Clear_Response(urlTarget);
        }

        void Clear_Response(string urlTarget)
        {
            //string strUsrAuth = dt.Rows[0]["totleAuthority"].ToString();
            //AuthAttributes usrAuthAttr;
            //Enum.TryParse<AuthAttributes>(strUsrAuth, out usrAuthAttr);

            AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];
            //Session["totleAuthority"] =
            //    dt.Rows[0]["totleAuthority"].ToString();
            //Session["totleAuthority"] =
            //    usrAuthAttr;
            //string strUsrAuth = Session["totleAuthority"] as string;
            string usrId = Session["usrId"] as string;
            Session.Clear();

            Session["totleAuthority"] = usrAuthAttr;
            Session["usrId"] = usrId;

            string strUrl = string.Empty;
            switch (urlTarget)
            {
                case ("projectTagInfo"):
                    strUrl = "~/Main/PublicInformation/projectTagInfo.aspx";
                    break;
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
                case ("userSelfEmailModify"):
                    strUrl = "~/Main/usrSelfModify/userSelfEmailModify.aspx";
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
                case ("projectStepSearch"):
                    strUrl = "~/Main/projectTagInfoManager/projectStepSearch.aspx";
                    break;
                //case ("newProject"):
                //    strUrl = "~/Main/projectTagInfoManager/projectTagAdd.aspx";
                //    break;
                case ("returnedSearch"):
                    strUrl = "~/Main/projectTagInfoManager/returned/returnSearch.aspx";
                    break;
                case ("returnOk"):
                    strUrl = "~/Main/projectTagInfoManager/returned/returnOk.aspx";
                    break;
                //case ("newReturn"):
                //    strUrl = "~/Main/projectTagInfoManager/returned/newReturn.aspx";
                //    break;
                case ("borrowedSearch"):
                    strUrl = "~/Main/projectTagInfoManager/borrowed/borrowSearch.aspx";
                    break;
                case ("borrowOk"):
                    strUrl = "~/Main/projectTagInfoManager/borrowed/borrowOk.aspx";
                    break;
                //case ("newBorrow"):
                //    strUrl = "~/Main/projectTagInfoManager/borrowed/newBorrow.aspx";
                //    break;
                case ("productView"):
                    strUrl = "~/Main/stockInfoManager/productInfoManager/productView.aspx";
                    break;
                case ("productIn"):
                    strUrl = "~/Main/stockInfoManager/productIn.aspx";
                    break;
                case ("productCheckView"):
                    strUrl = "~/Main/stockInfoManager/productCheckView.aspx";
                    break;
                case ("productStockDetail"):
                    strUrl = "~/Main/stockInfoManager/productStockView/productStockDetail.aspx";
                    break;
                case ("productStockView"):
                    strUrl = "~/Main/stockInfoManager/productStockView/productStockView.aspx";
                    break;
                case ("sellProductStockOutList"):
                    strUrl = "~/Main/stockInfoManager/productOutManager/productOutRelationManager/sellProductStockOutList.aspx";
                    break;
                case ("returnProductStockOutList"):
                    strUrl = "~/Main/stockInfoManager/productOutManager/productOutRelationManager/returnProductStockOutList.aspx";
                    break;
                case ("productStockOutAccept"):
                    strUrl = "~/Main/stockInfoManager/productOutManager/productStockOutAccept.aspx";
                    break;
                //case ("contractView"):
                //    strUrl = "~/Main/contractManager/contractView.aspx";
                //    break;
                //case ("addContract"):
                //    strUrl = "~/Main/contractManager/addContract.aspx";
                //    break;
                case ("subContractEdit"):
                    strUrl = "~/Main/contractManager/subContractEdit.aspx";
                    break;
                case ("mainContractReceiptView"):
                    strUrl = "~/Main/paymentReceiptManager/mainContractReceiptView.aspx";
                    break;
                case ("paymentOk"):
                    strUrl = "~/Main/paymentReceiptManager/paymentOk.aspx";
                    break;
                case ("receiptOk"):
                    strUrl = "~/Main/paymentReceiptManager/receiptOk.aspx";
                    break;
                case ("receiptView"):
                    strUrl = "~/Main/infoViewManager/receiptView.aspx";
                    break;
                case ("paymentView"):
                    strUrl = "~/Main/infoViewManager/paymentInfo/paymentView.aspx";
                    break;
                default:
                    break;
            }
            if (!string.IsNullOrEmpty(strUrl))
            {
                Response.Redirect(strUrl);
            }
        }
    }
}