<%@ Page Title="登录" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        &nbsp;</h2>
    <p>
        请输入用户名和密码。
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">注册</asp:HyperLink> 如果您没有帐户。</p>
    <p>
        &nbsp;</p>
    <p>
        <asp:Login ID="loginCon" runat="server" BackColor="#F7F7DE" 
            BorderColor="#CCCC99" BorderStyle="Solid" BorderWidth="1px" 
            Font-Names="Verdana" Font-Size="10pt" Height="182px" 
            onauthenticate="loginCon_Authenticate" style="margin-top: 25px" TitleText="请登录" 
            VisibleWhenLoggedIn="False" Width="286px">
            <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
        </asp:Login>
    </p>
    </asp:Content>