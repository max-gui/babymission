using System;

namespace xm_mis.logic
{
    [Flags, Serializable]
    public enum AuthAttributes
    {
        unKnow = 0x0,
        systemManager = 0x1,
        selfCompany = 0x2,
        custManager = 0x4,
        projectTagApply = 0x8,
        bor_retApply = 0x10,
        newContract = 0x20,
        pay_receiptApply = 0x40,
        pay_receiptOk = 0x80,
        stockManager = 0x100,
        productCheck = 0x200,
        productDetail = 0x400,
        sellProductRelation = 0x800,
        returnProductRelation = 0x1000,
        pay_receiptExamine = 0x2000,
        bor_retExamine = 0x4000,
        projectStepView = 0x8000
    }
    
    internal static class AuthAttributesMethods
    {
        public static bool HasOneFlag(this AuthAttributes falgs, AuthAttributes processFlag)
        {
            bool rtn = (falgs & processFlag) != 0;

            return rtn;
        }

        public static AuthAttributes ToAuthAttr(this int falgs)
        {
            AuthAttributes authAttr;

            Enum.TryParse<AuthAttributes>(falgs.ToString(), out authAttr);

            return authAttr;
        }

        public static AuthAttributes Set(this AuthAttributes falgs, AuthAttributes processFlag)
        {
            AuthAttributes authAttr = falgs | processFlag;            

            return authAttr;
        }
    }
}