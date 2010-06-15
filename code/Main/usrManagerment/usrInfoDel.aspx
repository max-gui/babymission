<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeFile="usrInfoDel.aspx.cs" Inherits="Main_usrManagerment_usrInfoDel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" Runat="Server">
    <p>
        <br />
    </p>
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:GridView ID="usrGV" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            HorizontalAlign="Center" 
            onsorting="usrGV_Sorting" 
            onrowdeleting="usrGV_RowDeleting" 
            onpageindexchanging="usrGV_PageIndexChanging" 
            onselectedindexchanged="usrGV_SelectedIndexChanged">
            <Columns>
                <asp:BoundField HeaderText="员工名字" DataField="realName" />
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
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOk" 
            runat="server" onclick="btnOk_Click" Text="确认删除" Visible="False" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Button ID="btnCancel" 
            runat="server" onclick="btnCancel_Click" Text="放弃" Visible="False" />
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

