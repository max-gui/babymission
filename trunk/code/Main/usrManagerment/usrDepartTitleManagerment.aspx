<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeFile="usrDepartTitleManagerment.aspx.cs" Inherits="Main_usrManagerment_usrDepartTitleManagerment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" Runat="Server">
    <p>
        <br />
    </p>
        <asp:GridView ID="usrGV" runat="server" AllowPaging="True" 
            AllowSorting="True" AutoGenerateColumns="False" 
            AutoGenerateSelectButton="True" 
            onselectedindexchanging="usrGV_SelectedIndexChanging" 
            HorizontalAlign="Center" onrowupdating="usrGV_RowUpdating" 
            onsorting="usrGV_Sorting">
            <Columns>
                <asp:BoundField HeaderText="员工名字" DataField="realName" />
                <asp:BoundField DataField="departmentName" HeaderText="所属部门" />
                <asp:BoundField DataField="titleName" HeaderText="职位" />
            </Columns>
        </asp:GridView>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>

