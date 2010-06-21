<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeFile="usrSelfContactModify.aspx.cs" Inherits="Main_usrSelfModify_usrSelfContactModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" Runat="Server">
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
        <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" Text="确认修改" 
            style="height: 21px" />
        &nbsp;</p>
</asp:Content>

