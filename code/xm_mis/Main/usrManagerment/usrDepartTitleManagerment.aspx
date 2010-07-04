<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="usrDepartTitleManagerment.aspx.cs" Inherits="xm_mis.Main.usrManagerment.usrDepartTitleManagerment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        <br />
    </p>
    <asp:GridView ID="usrGV" runat="server" AllowPaging="True" AllowSorting="True" 
        AutoGenerateColumns="False" HorizontalAlign="Center" 
        onpageindexchanging="usrGV_PageIndexChanging" 
        onrowdatabound="usrGV_RowDataBound" 
        onselectedindexchanging="usrGV_SelectedIndexChanging" onsorting="usrGV_Sorting">
        <Columns>
            <asp:CommandField SelectText="修改" ShowSelectButton="True" />
            <asp:BoundField DataField="realName" HeaderText="员工名字" />
            <asp:BoundField DataField="usrName" HeaderText="用户名" />
            <asp:TemplateField HeaderText="所属部门">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlDep" runat="server" Enabled="False">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="职位">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlTitle" runat="server" Enabled="False">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" Text="更新" 
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
        <SelectedRowStyle BackColor="#339966" />
    </asp:GridView>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>
