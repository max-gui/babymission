<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="paymentExamine.aspx.cs" Inherits="xm_mis.Main.infoViewManager.paymentInfo.paymentExamine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;项目号：&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblProjectTag" runat="server" Text="lblProjectTag"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;副合同编号：&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblSubContractTag" runat="server" Text="lblSubContractTag"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;供应商名称：&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblSupplier" runat="server" Text="lblSupplier"></asp:Label>
    </p>
    <p>
        副合同金额：&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="lblSubContractMoney" runat="server" Text="lblSubContractMoney"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 副合同供货日期：<asp:Label 
            ID="lblSubContractDateLine" runat="server" Text="lblSubContractDateLine"></asp:Label>
&nbsp;&nbsp; 主合同付款方式：<asp:Label ID="lblSubContractPayment" runat="server" 
            Text="lblSubContractPayment"></asp:Label>
    </p>
    <p>
        客户已付款： 
        <asp:Label ID="lblCustPayMax" runat="server" Text="lblCustPayMax"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        申请付款：<asp:Label ID="lblSelfToPay" runat="server" Text="lblSelfToPay"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 我方已付款：<asp:Label ID="lblSelfHasPay" 
            runat="server" Text="lblSelfHasPay"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 我方付款总计：<asp:Label 
            ID="lblSelfTotlePay" runat="server" Text="lblSelfTotlePay"></asp:Label>
    </p>
    <p>
        申请内容：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtPayExplication" runat="server" Height="65px" TextMode="MultiLine" 
            Width="312px" ReadOnly="True"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        完成日期：&nbsp;<asp:Label ID="lblDone" runat="server" Text="lblDone"></asp:Label>
    </p>
    <p>
        审批意见：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtPayComment" runat="server" Height="65px" 
            ontextchanged="txtPayComment_TextChanged" TextMode="MultiLine" 
            Width="312px"></asp:TextBox>
    </p>
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnOK" runat="server" onclick="btnOK_Click" 
            Text="批准" Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button 
            ID="btnNo" runat="server" onclick="btnNo_Click" Text="暂缓" 
            Visible="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnRtn" runat="server" onclick="btnRtn_Click" 
            Text="返回" />
    </p>
</asp:Content>
