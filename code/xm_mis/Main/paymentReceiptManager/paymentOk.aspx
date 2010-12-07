<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="paymentOk.aspx.cs" Inherits="xm_mis.Main.paymentReceiptManager.paymentOk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;</p>
    <asp:GridView ID="selfPaymentGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="付款申请信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="selfPaymentGV_PageIndexChanging" 
        onsorting="selfPaymentGV_Sorting" style="margin-left: 0px">
        <Columns>
            <asp:BoundField DataField="projectTag" HeaderText="项目编号" />
            <asp:BoundField DataField="realName" HeaderText="我方项目经理" />
            <asp:BoundField DataField="custCompName" HeaderText="客户公司" />
            <asp:BoundField DataField="supplierName" HeaderText="供应商" />
            <asp:BoundField DataField="mainContractTag" HeaderText="主合同编号" />
            <asp:BoundField DataField="subContractTag" HeaderText="副合同编号" />
            <asp:BoundField DataField="custMaxPay" HeaderText="客户已付款" DataFormatString="{0:p}" />
            <asp:BoundField DataField="toPayCash" HeaderText="申请付款额" DataFormatString="{0:c}" />
            <asp:BoundField DataField="hasPayPercent" HeaderText="已付款额" DataFormatString="{0:p}" />
            <asp:BoundField DataField="payPercent" HeaderText="总计付款额" DataFormatString="{0:p}" />
            <asp:BoundField DataField="paymentExplication" HeaderText="申请内容" />
            <asp:BoundField DataField="paymentApplyResult" HeaderText="是否批准" />
            <asp:BoundField DataField="paymentComment" HeaderText="审批意见" />
            <asp:BoundField DataField="Done" HeaderText="完成日期" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="paymentDone" runat="server" CausesValidation="False"  OnClick="paymentDone_Click" 
                    CommandName="paymentDone" Text="付款确认"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#CCFFCC" BorderStyle="Solid" Font-Names="宋体" />
        <FooterStyle BackColor="White" ForeColor="#333333" />
        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="White" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#487575" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#275353" />
    </asp:GridView>
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" 
            style="height: 21px" Text="确认" Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnNo" runat="server" onclick="btnNo_Click" Text="取消" 
        Visible="False" />
    </asp:Content>
