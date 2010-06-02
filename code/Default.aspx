﻿<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        欢迎使用 宝宝任务</h2>
    <asp:Menu ID="Menu1" runat="server" BackColor="#F7F6F3" 
        DynamicHorizontalOffset="2" Font-Italic="False" Font-Names="Verdana" 
        Font-Overline="False" Font-Size="Small" Font-Strikeout="False" 
        ForeColor="#7C6F57" Orientation="Horizontal" StaticSubMenuIndent="10px">
        <DynamicHoverStyle BackColor="#7C6F57" ForeColor="White" />
        <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
        <DynamicMenuStyle BackColor="#F7F6F3" />
        <DynamicSelectedStyle BackColor="#5D7B9D" />
        <Items>
            <asp:MenuItem Text="个人信息更改" Value="个人信息更改"></asp:MenuItem>
            <asp:MenuItem Text="用户管理" Value="用户管理">
                <asp:MenuItem Text="用户权限管理" Value="用户权限管理"></asp:MenuItem>
                <asp:MenuItem Text="删除用户信息" Value="删除用户信息"></asp:MenuItem>
                <asp:MenuItem Text="更改用户所属部门和职务" Value="更改用户所属部门和职务"></asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="公司结构管理" Value="公司结构管理">
                <asp:MenuItem Text="公司部门管理" Value="公司部门管理"></asp:MenuItem>
                <asp:MenuItem Text="公司职位管理" Value="公司职位管理"></asp:MenuItem>
            </asp:MenuItem>
        </Items>
        <StaticHoverStyle BackColor="#7C6F57" ForeColor="White" />
        <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
        <StaticSelectedStyle BackColor="#5D7B9D" />
    </asp:Menu>
    <p>
        &nbsp;</p>
    <p>
        若要了解关于宝宝任务 的详细信息，请致电宝宝。
    </p>
    <p>
        您还可以找到 宝宝任务的文档。
    </p>
</asp:Content>
