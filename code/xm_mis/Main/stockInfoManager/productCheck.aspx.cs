using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using xm_mis.logic;
using System.IO;
using xm_mis.db;
namespace xm_mis.Main.stockInfoManager
{
    public partial class productCheck : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!(null == Session["totleAuthority"]))
            {
                AuthAttributes usrAuthAttr = (AuthAttributes)Session["totleAuthority"];

                bool flag = usrAuthAttr.HasOneFlag(AuthAttributes.productCheck);
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
                DataRow sessionDr = Session["seldProductStock"] as DataRow;
                
                #region context_input
                lblProductName.Text = sessionDr["productName"].ToString();
                lblProductTag.Text = sessionDr["productTag"].ToString();
                #endregion

                //string strCheck = sessionDr["productCheck"].ToString();

                //if (strCheck.Equals(bool.FalseString))
                //{
                //    btnNo.Visible = false;
                //}
                //else if (strCheck.Equals(bool.TrueString))
                //{
                //    btnOK.Visible = false;
                //}
            }
        }

        protected void productToCheck(string accessOrNot)
        {
            DataRow sessionDr = Session["seldProductStock"] as DataRow;
            string strProductInCheckId = sessionDr["productInCheckId"].ToString();
                
            //if (inputCheck())
            //{                
            byte[] FileArray = UpLoadFile(fuCheck);
            string checkTextName = fuCheck.FileName;
            //得到上传文件的客户端MIME类型
            string strContentType = fuCheck.PostedFile.ContentType;

            //DataSet dst = new DataSet();
            //ProductStockProcess psp = new ProductStockProcess(dst);

            Xm_db xmDataCont = Xm_db.GetInstance();

            int productInCheckId = int.Parse(strProductInCheckId);

            try
            {
                xmDataCont.ProductIn_Check(productInCheckId, accessOrNot, FileArray, checkTextName, strContentType);
                xmDataCont.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);

                mailDetail(accessOrNot, sessionDr, xmDataCont);
            }
            catch (System.Data.Linq.ChangeConflictException cce)
            {
                string strEx = cce.Message;
                foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataCont.ChangeConflicts)
                {
                    //No database values are merged into current.
                    occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
                }

                xmDataCont.ProductIn_Check(productInCheckId, accessOrNot, FileArray, checkTextName, strContentType);
                xmDataCont.SubmitChanges();

                mailDetail(accessOrNot, sessionDr, xmDataCont);
            }
                //psp.ProductInCheck(strProductInCheckId, accessOrNot, FileArray, checkTextName, strContentType);

                //mailDetail(accessOrNot, sessionDr, xmDataCont);
            
            //}
            //else
            //{
            //    Xm_db xmDataCont = Xm_db.GetInstance();

            //    int productInCheckId = int.Parse(strProductInCheckId);

            //    var productInCheckEdit =
            //        (from productInCheck in xmDataCont.Tbl_productInCheck
            //         where productInCheck.ProductInCheckId == productInCheckId
            //         select productInCheck).First();

            //    productInCheckEdit.ProductCheck = accessOrNot;
            //    //productInCheckEdit.ProductCheckResult == ""

            //    try
            //    {
            //        //xmDataCont.Refresh(System.Data.Linq.RefreshMode.KeepChanges, xmDataCont.tbl_mainContract);
            //        //xmDataCont.Refresh(System.Data.Linq.RefreshMode.KeepChanges, xmDataCont.tbl_projectTagInfo);
            //        xmDataCont.SubmitChanges(System.Data.Linq.ConflictMode.ContinueOnConflict);
                    
            //        mailDetail(accessOrNot, sessionDr, xmDataCont);
            //    }
            //    catch (System.Data.Linq.ChangeConflictException cce)
            //    {
            //        string strEx = cce.Message;
            //        foreach (System.Data.Linq.ObjectChangeConflict occ in xmDataCont.ChangeConflicts)
            //        {
            //            //No database values are merged into current.
            //            occ.Resolve(System.Data.Linq.RefreshMode.KeepChanges);
            //        }

            //        xmDataCont.SubmitChanges();

            //        mailDetail(accessOrNot, sessionDr, xmDataCont);
            //    }
            //}

            Response.Redirect("~/Main/stockInfoManager/productCheckView.aspx");
        }

        private void mailDetail(string accessOrNot, DataRow sessionDr, Xm_db xmDataCont)
        {
            //var usr_autority =
            //from usr in xmDataCont.Tbl_usr
            //join auth in xmDataCont.View_usr_autority on usr.UsrId equals auth.UsrId
            //where auth.AuthorityId == 22 &&
            //      auth.UsrAuEnd > DateTime.Now
            //select usr;

            //int flag = 0x400;
            var usr_autority =
                from usr in xmDataCont.Tbl_usr
                //join auth in xmDataCont.View_usr_autority on usr.UsrId equals auth.UsrId
                where (usr.TotleAuthority & (UInt32)AuthAttributes.productDetail) != 0 &&
                      usr.EndTime > DateTime.Now
                select usr;
                //where usr.TotleAuthority.ToAuthAttr().HasOneFlag(AuthAttributes.productDetail) &&
                //      usr.EndTime > DateTime.Now
                //select usr;

            string strProductTag = sessionDr["productTag"].ToString();
            string strProductName = sessionDr["productName"].ToString();
            string strAccessOrNot = string.Empty;
            if (accessOrNot.Equals(bool.TrueString))
            {
                strAccessOrNot = "检验通过";
            }
            else
            {
                strAccessOrNot = "检验未通过";
            }

            foreach (var usr in usr_autority)
            {
                BeckSendMail.getMM().NewMail(usr.UsrEmail,
                    "mis系统货物检验通知",
                    "编号为" + strProductTag + "的" + strProductName + strAccessOrNot + ",等待您为其定义产品属性" +
                    System.Environment.NewLine + Request.Url.toNewUrlForMail("/Main/stockInfoManager/productStockView/productStockDetail.aspx"));
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            string accessOrNot = bool.TrueString;

            productToCheck(accessOrNot);
        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            string accessOrNot = bool.FalseString;

            productToCheck(accessOrNot);
        }

        protected void btnRtn_Click(object sender, EventArgs e)
        {
            //DataRow sessionDr = Session["seldProductStock"] as DataRow;

            //string strProductInId = sessionDr["productInId"].ToString();
            //string strOk = bool.TrueString;
            //byte[] FileArray = UpLoadFile(fuCheck);
            ////得到上传文件的客户端MIME类型
            //string strContentType = fuCheck.PostedFile.ContentType;

            //DataSet dst = new DataSet();
            //ProductStockProcess psp = new ProductStockProcess(dst);

            //psp.ProductInCheck(strProductInId, strOk, FileArray, strContentType);            

            //Response.Buffer = true;
            //Response.Clear();
            //Response.ContentType = strContentType;
            //Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode("a.xlsx".ToString(), System.Text.Encoding.UTF8));
            //Response.BinaryWrite((Byte[])FileArray);
            //Response.Flush();
            //Response.End();

            Response.Redirect("~/Main/stockInfoManager/productCheckView.aspx");
        }

        public byte[] UpLoadFile(FileUpload fu)
        {
            //获取由客户端指定的上传文件的访问
            HttpPostedFile upFile = fu.PostedFile;
            //得到上传文件的长度
            int upFileLength = upFile.ContentLength;

            byte[] FileArray = new Byte[upFileLength];

            Stream fileStream = upFile.InputStream;

            fileStream.Read(FileArray, 0, upFileLength);
            return FileArray;
        } 

        //protected bool fileUploadNoFile_Check(FileUpload fu)
        //{
        //    bool flag = true;

        //    if (!fu.HasFile)
        //    {
        //        flag = false;
        //    }

        //    return flag;
        //}

        //protected bool inputCheck()
        //{
        //    bool flag = true;

        //    flag = fileUploadNoFile_Check(fuCheck);
            
        //    return flag;
        //}
    }
}