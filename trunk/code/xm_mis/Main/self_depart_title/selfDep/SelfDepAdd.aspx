<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="SelfDepAdd.aspx.cs" Inherits="xm_mis.Main.self_depart_title.selfDep.SelfDepAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <asp:Label ID="lblDepName" runat="server" Text="新部门名称"></asp:Label>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:TextBox ID="txtDepName" runat="server"></asp:TextBox>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnAccept" runat="server" onclick="btnAccept_Click" Text="确认" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnNo" runat="server" onclick="btnNo_Click" Text="放弃" />
</asp:Content>
