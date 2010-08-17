<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="projectSearch.aspx.cs" Inherits="xm_mis.Main.projectTagInfoManager.projectSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;<asp:GridView ID="projectInfoGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="项目信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="projectInfoGV_PageIndexChanging" 
        onsorting="projectInfoGV_Sorting" style="margin-left: 0px">
        <Columns>
            <asp:BoundField DataField="projectTag" HeaderText="项目编号" />
            <asp:BoundField DataField="projectSynopsis" HeaderText="项目内容" />
            <asp:BoundField DataField="custCompName" HeaderText="客户公司名称" />
            <asp:BoundField DataField="custCompAddress" HeaderText="客户公司地址" />
            <asp:BoundField DataField="custManName" HeaderText="客户项目经理姓名" />
            <asp:BoundField DataField="custManContact" HeaderText="客户项目经理电话" />
            <asp:BoundField DataField="custManEmail" HeaderText="客户项目经理Email" />
            <asp:BoundField DataField="realName" HeaderText="我方项目经理姓名" />
            <asp:BoundField DataField="usrContact" HeaderText="我方项目经理电话" />
            <asp:BoundField DataField="startTime" HeaderText="项目号申请时间" />
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
        &nbsp;</p>
</asp:Content>
