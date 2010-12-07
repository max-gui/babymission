<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="productStockView.aspx.cs" Inherits="xm_mis.Main.stockInfoManager.productStockView.productStockView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;</p>
    <asp:GridView ID="productStockGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="产品库存信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="productStockGV_PageIndexChanging" 
        onsorting="productStockGV_Sorting" style="margin-left: 0px" 
        onrowdatabound="productStockGV_RowDataBound">
        <Columns>
            <asp:TemplateField Visible="False" >
                <ItemTemplate>
                    <asp:Label ID="lblProductStockId" runat="server" Text='<%# Bind("productStockId") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblProductId" runat="server" Text='<%# Bind("productId") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblProductPurpose" runat="server" Text='<%# Bind("productPurpose") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblToOut" runat="server" Text='<%# Bind("toOut") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="False" >
                <ItemTemplate>
                    <asp:Label ID="lblProductInCheckId" runat="server" Text='<%# Bind("productInCheckId") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="productStockSt" HeaderText="入库时间" SortExpression="productStockSt"/>
            <asp:TemplateField HeaderText="产品型号" SortExpression="productName">
                <ItemTemplate>
                    <asp:Label ID="lblProductName" runat="server" Text='<%# Bind("productName") %>'></asp:Label>
                    <asp:DropDownList ID="ddlProductName" runat="server" Visible="False"></asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="产品编号" SortExpression="productTag">
                <ItemTemplate>
                    <asp:Label ID="lblProductTag" runat="server" Text='<%# Bind("productTag") %>'></asp:Label>
                    <asp:TextBox ID="txtProductTag" runat="server" Text='<%# Bind("productTag") %>' Visible="False"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="productCheckResult" HeaderText="检验结果" SortExpression="productCheckResult"/>
            <asp:BoundField DataField="productPurposeResult" HeaderText="产品属性" SortExpression="productPurposeResult"/>
            <asp:BoundField DataField="toOutResult" HeaderText="预定出库" SortExpression="toOutResult"/>
            <asp:TemplateField ShowHeader="False" >
                <ItemTemplate>
                    <asp:LinkButton ID="checkTextDown" runat="server" CausesValidation="False" OnClick="checkText_Down"
                        CommandName="checkTextDown" Text="检验报告"></asp:LinkButton>                    
                    <asp:LinkButton ID="toEdit" runat="server" CausesValidation="False" OnClick="toEdit_Click"
                        CommandName="toEdit" Text="修改"></asp:LinkButton>
                    <asp:LinkButton ID="toDel" runat="server" CausesValidation="False" OnClick="toDel_Click"
                        CommandName="toDel" Text="删除"></asp:LinkButton>
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

<SortedAscendingCellStyle BackColor="#F7F7F7"></SortedAscendingCellStyle>

<SortedAscendingHeaderStyle BackColor="#487575"></SortedAscendingHeaderStyle>

<SortedDescendingCellStyle BackColor="#E5E5E5"></SortedDescendingCellStyle>

<SortedDescendingHeaderStyle BackColor="#275353"></SortedDescendingHeaderStyle>
    </asp:GridView>
<p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" 
            style="height: 21px" Text="确认提交修改" Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnNo" runat="server" onclick="btnNo_Click" Text="放弃" 
        Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnRtn" runat="server" onclick="btnRtn_Click" Text="返回" 
            Width="40px" />
</p>
</asp:Content>
