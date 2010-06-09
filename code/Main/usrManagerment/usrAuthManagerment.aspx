<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeFile="usrAuthManagerment.aspx.cs" Inherits="Main_usrManagerment_usrAuthManagerment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" Runat="Server">
    
    <p>
    
    <asp:CheckBoxList ID="cblAuth" runat="server" 
        Enabled="False" RepeatDirection="Horizontal" 
            ondatabinding="cblAuth_DataBinding" AutoPostBack="True" 
            ontextchanged="cblAuth_TextChanged">
        <asp:ListItem Value="1">工程师</asp:ListItem>
        <asp:ListItem Value="2">仓库管理员</asp:ListItem>
        <asp:ListItem Value="4">前台助理</asp:ListItem>
        <asp:ListItem Value="8">系统管理员</asp:ListItem>
        <asp:ListItem Value="16">我方项目经理</asp:ListItem>
        <asp:ListItem Value="32">客户关系管理员</asp:ListItem>
        <asp:ListItem Value="64">总经理</asp:ListItem>
        <asp:ListItem Value="128">财务管理员</asp:ListItem>
    </asp:CheckBoxList>
        <br />
    </p>
    <p>
        &nbsp;</p>
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
            <SelectedRowStyle BackColor="#339966" />
        </asp:GridView>
    
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        &nbsp;<asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" Text="确认修改" 
            Visible="False" />
    </p>
    <p>
        &nbsp;</p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>

