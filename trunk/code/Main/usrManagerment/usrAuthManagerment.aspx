<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeFile="usrAuthManagerment.aspx.cs" Inherits="Main_usrManagerment_usrAuthManagerment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" Runat="Server">
    
    <p>
        <br />
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
        <asp:GridView ID="usrGV" runat="server" AllowPaging="True" 
            Style="left: 357px; position: absolute;top: 272px; width: 100px; right: 370px;"
            AllowSorting="True" AutoGenerateColumns="False" 
            AutoGenerateSelectButton="True" 
            onselectedindexchanging="usrGV_SelectedIndexChanging">
            <Columns>
                <asp:BoundField HeaderText="员工名字" />
            </Columns>
        </asp:GridView>
    </p>
    
    <p>
    </p>
    <p>
    </p>
    <p>
        &nbsp;</p>
    <asp:CheckBoxList ID="cblAuth" runat="server" 
            Style="left: 592px; position: absolute;top: 318px" Enabled="False">
        <asp:ListItem Value="1">工程师</asp:ListItem>
        <asp:ListItem Value="2">仓库管理员</asp:ListItem>
        <asp:ListItem Value="4">前台助理</asp:ListItem>
        <asp:ListItem Value="8">系统管理员</asp:ListItem>
        <asp:ListItem Value="16">我方项目经理</asp:ListItem>
        <asp:ListItem Value="32">客户关系管理员</asp:ListItem>
        <asp:ListItem Value="64">总经理</asp:ListItem>
        <asp:ListItem Value="128">财务管理员</asp:ListItem>
    </asp:CheckBoxList>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>

