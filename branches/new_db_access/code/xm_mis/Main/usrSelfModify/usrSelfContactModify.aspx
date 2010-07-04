<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="usrSelfContactModify.aspx.cs" Inherits="xm_mis.Main.usrSelfModify.usrSelfContactModify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        <br />
        新联系方式：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:TextBox ID="txtContact" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblContact" runat="server" Font-Size="Large" ForeColor="Red" 
            Text="*必填项"></asp:Label>
    </p>
    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" 
            style="height: 21px" Text="确认修改" />
        &nbsp;</p>
</asp:Content>
