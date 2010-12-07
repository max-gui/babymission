<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="borrowDetail.aspx.cs" Inherits="xm_mis.Main.projectTagInfoManager.borrowed.borrowDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        项目编号：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="lblProjectTag" runat="server" Text="projectTag"></asp:Label>
    </p>
    <p>
        项目描述：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="lblProjectSynopsis" runat="server" Text="projectSynopsis"></asp:Label>
    </p>
    <p>
        客户公司：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="lblCustCompName" runat="server" Text="custCompName"></asp:Label>
    </p>
    <p>
        客户项目经理： 
        <asp:Label ID="lblCustManName" runat="server" Text="custManName"></asp:Label>
        &nbsp;</p>
    <p>
        借用产品型号： 
        <asp:Label ID="lblProductName" runat="server" Text="productName"></asp:Label>
        &nbsp;</p>
    <p>
        借用产品编号： 
        <asp:Label ID="lblProductTag" runat="server" Text="productTag"></asp:Label>
        &nbsp;&nbsp;</p>
    <p>
        申请人：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="lblApplymentUsrName" runat="server" Text="applymentUsrName"></asp:Label>
        &nbsp;</p>
    <p>
        申请时间：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="lblStartTime" runat="server" Text="startTime"></asp:Label>
    </p>
    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" Text="批准" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnNo" runat="server" onclick="btnNo_Click" Text="不批准" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnRtn" runat="server" onclick="btnRtn_Click" Text="返回" />
    </p>
</asp:Content>
