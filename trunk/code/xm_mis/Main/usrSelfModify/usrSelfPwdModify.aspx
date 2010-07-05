<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="usrSelfPwdModify.aspx.cs" Inherits="xm_mis.Main.usrSelfModify.usrSelfPwdModify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;</p>
    <p>
        新密码：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblPassWord" runat="server" Font-Size="Large" ForeColor="Red" 
            Text="*必填项"></asp:Label>
    </p>
    <p>
        新密码确认：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:TextBox ID="txtRPassWord" runat="server" TextMode="Password"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblRPassWord" runat="server" Font-Size="Large" ForeColor="Red" 
            Text="*必填项"></asp:Label>
    </p>
    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" 
            style="height: 21px" Text="确认修改" />
        &nbsp;</p>
    &nbsp;&nbsp;&nbsp; 
</asp:Content>
