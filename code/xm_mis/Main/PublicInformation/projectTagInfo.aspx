<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="projectTagInfo.aspx.cs" Inherits="xm_mis.Main.PublicInformation.projectTagInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;</p>
<asp:GridView ID="projectTagGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="项目号一览" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="projectTagGV_PageIndexChanging" 
        onsorting="projectTagGV_Sorting" style="margin-left: 0px">
    <Columns>
        <asp:BoundField DataField="startTime" HeaderText="时间" />
        <asp:BoundField DataField="projectTag" HeaderText="项目号" />
        <asp:BoundField DataField="projectSynopsis" HeaderText="项目内容" />
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
