<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="receiptExamine.aspx.cs" Inherits="xm_mis.Main.infoViewManager.receiptExamine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;项目号：&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblProjectTag" runat="server" Text="lblProjectTag"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;主合同编号：&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblMainContractTag" runat="server" Text="lblMainContractTag"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;客户公司名称（需方）：&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblCust" runat="server" Text="lblCust"></asp:Label>
    </p>
    <p>
        主合同金额：&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="lblMainContractMoney" runat="server" Text="lblMainContractMoney"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 主合同需货日期：<asp:Label 
            ID="lblMainContractDateLine" runat="server" Text="lblMainContractDateLine"></asp:Label>
&nbsp;&nbsp; 主合同付款方式：<asp:Label ID="lblMainContractPayment" runat="server" 
            Text="lblMainContractPayment"></asp:Label>
    </p>
    <p>
        供应商已开票： 
        <asp:Label ID="lblCustReceiptMax" runat="server" Text="lblCustReceiptMax"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        申请开票：<asp:Label ID="lblSelfToReceipt" runat="server" Text="lblSelfToReceipt"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 我方已开票：<asp:Label ID="lblSelfHasReceipt" 
            runat="server" Text="lblSelfHasReceipt"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 我方开票总计：<asp:Label 
            ID="lblSelfTotleReceipt" runat="server" Text="lblSelfTotleReceipt"></asp:Label>
    </p>
    <p>
        申请内容：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtReceiptExplication" runat="server" Height="65px" TextMode="MultiLine" 
            Width="312px" ReadOnly="True"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        完成日期：&nbsp;<asp:Label ID="lblDone" runat="server" Text="lblDone"></asp:Label>
    </p>
    <p>
        审批意见：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtReceiptComment" runat="server" Height="65px" 
            ontextchanged="txtReceiptComment_TextChanged" TextMode="MultiLine" 
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
