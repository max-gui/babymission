<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="custCompEdit.aspx.cs" Inherits="xm_mis.Main.custInfoManager.custCompManager.custCompEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    </p>
    <asp:GridView ID="custCompGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="客户公司信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="custCompGV_PageIndexChanging" 
        onselectedindexchanging="custCompGV_SelectedIndexChanging" 
        onsorting="custCompGV_Sorting" style="margin-left: 0px">
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="custCompName" HeaderText="客户公司名称" />
            <asp:BoundField DataField="custCompAddress" HeaderText="客户公司地址" />
            <asp:BoundField DataField="custCompTag" HeaderText="客户公司代号" />
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
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnCustManEdit" runat="server" onclick="btnCustManEdit_Click" 
                        Text="所属项目经理" Visible="False" />
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="添加新客户公司" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button 
            ID="btnAcceptDel" runat="server" onclick="btnAcceptDel_Click" Text="确认删除" 
            Visible="False" />
&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnDelCancel" runat="server" onclick="btnDelCancel_Click" Text="放弃" 
            Visible="False" />
    </p>
</asp:Content>
