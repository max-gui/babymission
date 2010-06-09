﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeFile="SelfDepartment.aspx.cs" Inherits="SelfDepartment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" Runat="Server">
    <br />
<br />
<br />
<asp:GridView ID="SelfDepartGV" runat="server" AllowPaging="True" 
    AllowSorting="True" AutoGenerateDeleteButton="True" 
    AutoGenerateEditButton="True" BackColor="White" BorderColor="#336666" 
    BorderStyle="Double" BorderWidth="3px" Caption="部门信息" CellPadding="4" 
    GridLines="Horizontal" HorizontalAlign="Center" AutoGenerateColumns="False" 
        onrowcancelingedit="SelfDepartGV_RowCancelingEdit" 
        onrowcommand="SelfDepartGV_RowCommand" 
        onrowdatabound="SelfDepartGV_RowDataBound" 
        onrowdeleting="SelfDepartGV_RowDeleting" onrowediting="SelfDepartGV_RowEditing" 
        onrowupdating="SelfDepartGV_RowUpdating" onsorting="SelfDepartGV_Sorting" 
        style="margin-left: 0px">
    <Columns>
        <asp:ButtonField ButtonType="Button" CommandName="Add" Text="添加" />
        <asp:BoundField DataField="departmentName" HeaderText="部门名称" />
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" Text="提交所有修改" 
        Enabled="False" />
<br />
<br />
<br />
</asp:Content>

