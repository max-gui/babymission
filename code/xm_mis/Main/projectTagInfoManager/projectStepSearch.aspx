<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="projectStepSearch.aspx.cs" Inherits="xm_mis.Main.projectTagInfoManager.projectStepSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        <br />
&nbsp;<asp:GridView ID="projectInfoGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="项目信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="projectInfoGV_PageIndexChanging" 
        onsorting="projectInfoGV_Sorting" style="margin-left: 0px" 
            onrowdatabound="projectInfoGV_RowDataBound">
        <Columns>
            <asp:TemplateField HeaderText="项目编号">
                <ItemTemplate>
                    <asp:Label ID="projectTag" runat="server" Text='<%# Bind("projectTag") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="合同签订">
                <ItemTemplate>
                    <%--<asp:Label ID="supplierOk" runat="server" Text='<%# Bind("supplierOk") %>'></asp:Label>--%>
                    <asp:Label ID="supplierOk" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="开票">
                <ItemTemplate>
                    <%--<asp:Label ID="receiptOk" runat="server" Text='<%# Bind("receiptOk") %>'></asp:Label>--%>
                    <asp:Label ID="receiptOk" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="付款">
                <ItemTemplate>
                    <%--<asp:Label ID="receivingOk" runat="server" Text='<%# Bind("receivingOk") %>'></asp:Label>--%>
                    <asp:Label ID="receivingOk" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="发货">
                <ItemTemplate>
                    <%--<asp:Label ID="productOk" runat="server" Text='<%# Bind("productOk") %>'></asp:Label>--%>
                    <asp:Label ID="productOk" runat="server" ></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
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
        </p>
</asp:Content>
