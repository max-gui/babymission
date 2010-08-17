<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="subContractEdit.aspx.cs" Inherits="xm_mis.Main.contractManager.subContractEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;</p>
<asp:GridView ID="mainContractGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="主合同信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="mainContractGV_PageIndexChanging" 
        onsorting="mainContractGV_Sorting" style="margin-left: 0px" 
    onselectedindexchanged="mainContractGV_SelectedIndexChanged" 
        onrowdatabound="mainContractGV_RowDataBound">
    <Columns>
        <asp:BoundField DataField="mainContractTag" HeaderText="主合同编号" />
        <asp:BoundField DataField="projectTag" HeaderText="项目号" />
        <asp:BoundField DataField="contractCompName" HeaderText="客户公司名称（需方）" />
        <asp:BoundField DataField="cash" HeaderText="合同金额" />
        <asp:TemplateField HeaderText="签约产品（名称，数量）">
            <ItemTemplate>
                <asp:ListBox ID="contractProductLsB" runat="server" Enabled="False" Rows="1">
                </asp:ListBox>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="dateLine" HeaderText="需货日期" />
        <asp:BoundField DataField="paymentMode" HeaderText="付款方式" />
        <asp:CommandField ShowSelectButton="True" SelectText="编辑副合同" />
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
</asp:Content>
