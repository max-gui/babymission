<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="productStockDetail.aspx.cs" Inherits="xm_mis.Main.stockInfoManager.productStockView.productStockDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;</p>
    <asp:GridView ID="productStockGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="产品库存信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="productStockGV_PageIndexChanging" 
        onsorting="productStockGV_Sorting" style="margin-left: 0px" 
        onrowdatabound="productStockGV_RowDataBound" 
        onrowcommand="productStockGV_RowCommand">
        <Columns>
            <asp:BoundField DataField="productName" HeaderText="产品型号" />
            <asp:BoundField DataField="productTag" HeaderText="产品编号" />
            <asp:BoundField DataField="productPurposeResult" HeaderText="产品属性" />
            <asp:BoundField DataField="productCheckResult" HeaderText="检验结果" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="productEdit" runat="server" CausesValidation="False" OnClick="productEdit_Click"
                        CommandName="productEdit" Text="定义产品属性"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:RadioButtonList ID="rblProductDetail" runat="server" 
                        RepeatDirection="Horizontal" Visible="False">
                        <asp:ListItem Value="forSell">销售产品</asp:ListItem>
                        <asp:ListItem Value="forBorrow">试用产品</asp:ListItem>
                    </asp:RadioButtonList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="False" Visible="False">
                <ItemTemplate>
                    <asp:LinkButton ID="purposeEmpty" runat="server" CausesValidation="False" OnClick="purposeEmpty_Click"
                        CommandName="purposeEmpty" Text="清空产品定义"></asp:LinkButton>
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
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
            ID="btnOK" runat="server" onclick="btnOK_Click" 
            Text="确认" Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button 
            ID="btnNo" runat="server" onclick="btnNo_Click" Text="放弃" 
            Visible="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnRtn" runat="server" onclick="btnRtn_Click" Text="返回" 
            Width="40px" />
    </p>
</asp:Content>
