﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="returnOk.aspx.cs" Inherits="xm_mis.Main.projectTagInfoManager.returned.returnOk" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;</p>
    <asp:GridView ID="projectInfoGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="送修信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="projectInfoGV_PageIndexChanging" 
        onsorting="projectInfoGV_Sorting" style="margin-left: 0px">
        <Columns>
            <asp:BoundField DataField="projectTag" HeaderText="送修编号" />
            <asp:BoundField DataField="projectSynopsis" HeaderText="送修内容" />
            <asp:BoundField DataField="productName" HeaderText="产品型号" />
            <asp:BoundField DataField="productTag" HeaderText="产品编号" />
            <asp:BoundField DataField="applymentUsrName" HeaderText="送修申请人" />
            <asp:BoundField DataField="approveUsrName" HeaderText="审批人" />
            <asp:BoundField DataField="approveResult" HeaderText="审批结果" />
            <asp:BoundField DataField="startTime" HeaderText="送修申请时间" />
            <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:LinkButton ID="moreInfo" runat="server" CausesValidation="False" OnClick="btnMoreInfo_Click"
                    CommandName="moreInfo" Text="详细性息"></asp:LinkButton>
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
    <br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <br />
</asp:Content>