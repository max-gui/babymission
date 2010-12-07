using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;

using System.Text;
namespace xm_mis.Main
{
    static public class AddrComputer
    {
        static public string toNewUrlForMail(this Uri servUrl, string redirectAddr)
        {
            string varAddr = servUrl.AbsolutePath;

            StringBuilder sb = new StringBuilder(servUrl.AbsoluteUri, 50);

            int index = varAddr.LastIndexOf("/Main/");                        
            
            string subAddrIncServPath = varAddr.Substring(index);

            sb.Replace(subAddrIncServPath, string.Empty);

            string newUrl = sb.Append(redirectAddr).ToString();

            return newUrl;
        }
    }
}
