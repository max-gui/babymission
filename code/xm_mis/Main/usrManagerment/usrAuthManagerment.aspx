﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="usrAuthManagerment.aspx.cs" Inherits="xm_mis.Main.usrManagerment.usrAuthManagerment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        <br />
    </p>
    <p>
        &nbsp;</p>
    <asp:GridView ID="usrGV" runat="server" AllowPaging="True" AllowSorting="True" 
        AutoGenerateColumns="False" AutoGenerateSelectButton="True" 
        HorizontalAlign="Center" onpageindexchanging="usrGV_PageIndexChanging" 
        onrowdatabound="usrGV_RowDataBound" 
        onselectedindexchanging="usrGV_SelectedIndexChanging" onsorting="usrGV_Sorting">
        <Columns>
            <asp:BoundField DataField="realName" HeaderText="员工名字" />
            <asp:BoundField DataField="usrName" HeaderText="用户名" />
            <asp:BoundField DataField="departmentName" HeaderText="所属部门" />
            <asp:BoundField DataField="titleName" HeaderText="职位" />
            <asp:TemplateField HeaderText="用户权限">
                <ItemTemplate>
                    <asp:CheckBoxList ID="cblUsrAuth" runat="server" 
                        ondatabound="cblUsrAuth_DataBound" Visible="False">
                    </asp:CheckBoxList>
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
    <p>
    </p>
</asp:Content>
