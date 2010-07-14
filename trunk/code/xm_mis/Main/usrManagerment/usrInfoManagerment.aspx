<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="usrInfoManagerment.aspx.cs" Inherits="xm_mis.Main.usrManagerment.usrInfoManagerment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        <br />
    </p>
    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:GridView ID="usrGV" runat="server" AllowPaging="True" AllowSorting="True" 
            AutoGenerateColumns="False" HorizontalAlign="Center" 
            onpageindexchanging="usrGV_PageIndexChanging" onrowdeleting="usrGV_RowDeleting" 
            onselectedindexchanged="usrGV_SelectedIndexChanged" onsorting="usrGV_Sorting">
            <Columns>
                <asp:BoundField DataField="realName" HeaderText="员工名字" />
                <asp:BoundField DataField="usrName" HeaderText="用户名" />
                <asp:BoundField DataField="usrContact" HeaderText="联系方式" />
                <asp:BoundField DataField="titleName" HeaderText="职位" />
                <asp:BoundField DataField="departmentName" HeaderText="所属部门" />
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
            <SelectedRowStyle BackColor="#339966" />
        </asp:GridView>
        &nbsp;</p>
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
            ID="btnDel" runat="server" onclick="btnDel_Click" Text="确认删除" 
            Visible="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" Text="放弃" 
            Visible="False" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button 
            ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="添加新用户" />
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>
