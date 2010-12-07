<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="receiptApply.aspx.cs" Inherits="xm_mis.Main.paymentReceiptManager.receiptApply" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;</p>
<asp:GridView ID="selfReceiptGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="开票申请信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="selfReceiptGV_PageIndexChanging" 
        onsorting="selfReceiptGV_Sorting" style="margin-left: 0px">
    <Columns>
        <asp:BoundField DataField="custMaxReceipt" HeaderText="供应商已开票" DataFormatString="{0:p}" />
        <asp:BoundField DataField="toReceipt" HeaderText="申请开票额" DataFormatString="{0:p}" />
        <asp:BoundField DataField="hasReceiptPercent" HeaderText="已开票额" DataFormatString="{0:p}" />
        <asp:BoundField DataField="receiptPercent" HeaderText="总计开票额" DataFormatString="{0:p}" />
        <asp:BoundField DataField="receiptExplication" HeaderText="申请内容" />
        <asp:BoundField DataField="receiptApplyResult" HeaderText="是否批准" />
        <asp:BoundField DataField="receiptComment" HeaderText="审批意见" />
        <asp:BoundField DataField="Done" HeaderText="完成日期" /> 
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <%--<asp:Label ID="lblMessage" runat="server" Text="message" Visible="False"></asp:Label>--%>
                <asp:LinkButton ID="toDel" runat="server" CausesValidation="False" OnClick="toDel_Click"
                CommandName="toDel" CommandArgument='<%# Bind("receiptId") %>' Text="删除"></asp:LinkButton>
                <asp:LinkButton ID="toEdit" runat="server" CausesValidation="False" OnClick="toEdit_Click"
                CommandName="toEdit" CommandArgument='<%# Bind("receiptId") %>' Text="修改"></asp:LinkButton>
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
<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" 
            style="height: 21px" Text="提交新的申请" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAccept" runat="server" onclick="btnAccept_Click" 
            Text="确认提交" Visible="False" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
            style="height: 21px" Text="取消" Visible="False" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnNo" runat="server" onclick="btnNo_Click" Text="返回" />
</asp:Content>
