<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="mainContractReceiptView.aspx.cs" Inherits="xm_mis.Main.paymentReceiptManager.mainContractReceiptView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;</p>
<asp:GridView ID="mainContractGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="主合同信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="mainContractGV_PageIndexChanging" 
        onsorting="mainContractGV_Sorting" style="margin-left: 0px" 
        onrowdatabound="mainContractGV_RowDataBound" 
        onrowcommand="mainContractGV_RowCommand">
    <Columns>
        <asp:BoundField DataField="projectTag" HeaderText="项目号" />
        <asp:BoundField DataField="mainContractTag" HeaderText="主合同编号" />
        <asp:BoundField DataField="contractCompName" HeaderText="客户公司名称（需方）" />
        <asp:BoundField DataField="cash" HeaderText="合同金额" />
        <asp:BoundField DataField="dateLine" HeaderText="需货日期" />
        <asp:BoundField DataField="paymentMode" HeaderText="付款方式" />
        <asp:TemplateField HeaderText="已收款额">
            <ItemTemplate>
                <asp:DropDownList ID="ddlPay" runat="server" Enabled="False">
                </asp:DropDownList>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" 
                    Text='<%# Bind("selfReceivingPercent") %>'></asp:TextBox>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="selfReceiptPercent" HeaderText="已开票额" />
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <asp:LinkButton ID="payEdit" runat="server" CausesValidation="False" 
                    CommandName="payEdit" Text="修改已付款额"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnRecieptApply" runat="server" onclick="btnRecieptApply_Click" 
                    Text="开票申请" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnSubContractEdit" runat="server" 
                    onclick="btnSubContractEdit_Click" Text="查看副合同" />
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
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" 
            style="height: 21px" Text="确认提交修改" Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnNo" runat="server" onclick="btnNo_Click" Text="放弃" 
        Visible="False" />
        </asp:Content>
