<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="custManAdd.aspx.cs" Inherits="xm_mis.Main.custInfoManager.custManManager.custManAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        客户项目经理信息录入</p>
    <p>
        所属公司：&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCustComp" 
            runat="server" Text="Label" Width="145px"></asp:Label>
    </p>
    <p>
        姓名：
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblName" runat="server" Font-Size="Large" ForeColor="Red" 
            Text="*必填项"></asp:Label>
    </p>
    <p>
        部门：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:TextBox ID="txtDep" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblDep" runat="server" Font-Size="Large" ForeColor="Red" 
            Text="*必填项"></asp:Label>
    </p>
    <p>
        职位：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:TextBox ID="txtTitle" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblTitle" runat="server" Font-Size="Large" ForeColor="Red" 
            Text="*必填项"></asp:Label>
    </p>
    <p>
        联系方式：&nbsp;&nbsp;&nbsp; 
        <asp:TextBox ID="txtContact" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
        </p>
    <p>
        Email：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
        </p>
    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" 
            style="height: 21px" Text="确认添加" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnNo" runat="server" onclick="btnNo_Click" Text="放弃" />
        </p>
</asp:Content>
