<%@ Page Title="注册" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Register.aspx.cs" Inherits="Account_Register" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <style type="text/css">
        .style1
        {
            font-size: large;
        }
        .style2
        {
            font-size: x-large;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p class="style2">
        创建新用户</p>
    <p>
        <br />
        <span class="style1">请用以下表单填写注册信息</span><br />
    </p>
    <p>
        用户名：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    </p>
    <p>
        密码：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    </p>
    <p>
        联系方式：&nbsp;&nbsp;&nbsp; 
    </p>
    <p>
        <br />
        <br />
    </p>
</asp:Content>