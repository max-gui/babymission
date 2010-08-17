<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="custCompEditing.aspx.cs" Inherits="xm_mis.Main.custInfoManager.custCompManager.custCompEditing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        公司名字：&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblCustCompName" runat="server" Text="lblCustCompName"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        公司地址：&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="lblCustCompAddr" runat="server" Text="lblCustCompAddr"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        公司简称：&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="lblCustCompTag" runat="server" Text="lblCustCompTag"></asp:Label>
    </p>
    <p>
        &nbsp;</p>
    <asp:GridView ID="custCompManGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="客户公司项目经理信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="custCompManGV_PageIndexChanging" 
        onselectedindexchanging="custCompManGV_SelectedIndexChanging" 
        onsorting="custCompManGV_Sorting" style="margin-left: 0px">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="custManName" HeaderText="项目经理姓名" />
            <asp:BoundField DataField="custManContact" HeaderText="项目经理电话" />
            <asp:BoundField DataField="custManEmail" HeaderText="项目经理Email" />
            <asp:BoundField DataField="custManDepart" HeaderText="项目经理所属部门" />
            <asp:BoundField DataField="custManTitle" HeaderText="项目经理职位" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnDel" runat="server" onclick="btnDel_Click" Text="删除" 
                        Visible="False" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnUpdate" runat="server" onclick="btnUpdate_Click" Text="更新" 
                        Visible="False" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnCancle" runat="server" onclick="btnCancle_Click" Text="放弃更改" 
                        Visible="False" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#CCFFCC" BorderStyle="Solid" Font-Names="宋体" />
        <FooterStyle BackColor="White" ForeColor="#333333" />
        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="White" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#487575" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#275353" />
    </asp:GridView>
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" 
            Text="添加新客户项目经理" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button 
            ID="btnAcceptDel" runat="server" onclick="btnAcceptDel_Click" Text="确认删除" 
            Visible="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDelCancel" runat="server" onclick="btnDelCancel_Click" Text="放弃" 
            Visible="False" />
    </p>
</asp:Content>
