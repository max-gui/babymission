<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="receiptView.aspx.cs" Inherits="xm_mis.Main.infoViewManager.receiptView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;</p>
<asp:GridView ID="selfReceiptGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="开票申请信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="selfReceiptGV_PageIndexChanging" 
        onsorting="selfReceiptGV_Sorting" style="margin-left: 0px" 
        onrowdatabound="selfReceiptGV_RowDataBound" 
        onrowcommand="selfReceiptGV_RowCommand">
    <Columns>
        <asp:BoundField DataField="custMaxReceiptPercent" HeaderText="供应商已开票" />
        <asp:BoundField DataField="selfReceiptPercent" HeaderText="申请开票额" />
        <asp:BoundField DataField="receiptExplication" HeaderText="申请内容" />
        <asp:BoundField DataField="acceptOrNot" HeaderText="是否批准" />
        <asp:BoundField DataField="receiptComment" HeaderText="审批意见" />
        <asp:BoundField DataField="Done" HeaderText="完成日期" />
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:LinkButton ID="detailView" runat="server" CausesValidation="False" 
                    CommandName="detailView" Text="查看细节"></asp:LinkButton>
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
</asp:Content>
