<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="subContractPaymentView.aspx.cs" Inherits="xm_mis.Main.paymentReceiptManager.subContractPaymentView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;项目号：&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblProjectTag" runat="server" Text="lblProjectTag"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        主合同编号：&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblMainContractTag" runat="server" Text="lblMainContractTag"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        客户公司名称（需方）：&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblCust" runat="server" Text="lblCust"></asp:Label>
    </p>
    <p>
        主合同金额：&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Label ID="lblMainContractMoney" runat="server" Text="lblMainContractMoney"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 主合同需货日期：<asp:Label 
            ID="lblMainContractDateLine" runat="server" Text="lblMainContractDateLine"></asp:Label>
&nbsp;&nbsp; 主合同付款方式：<asp:Label ID="lblMainContractPayment" runat="server" 
            Text="lblMainContractPayment"></asp:Label>
    </p>
    <asp:GridView ID="subContractGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="所属副合同信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="subContractGV_PageIndexChanging" 
        onsorting="subContractGV_Sorting" style="margin-left: 0px" 
        onrowdatabound="subContractGV_RowDataBound" 
        onrowcommand="subContractGV_RowCommand">
        <Columns>
            <asp:BoundField DataField="subContractTag" HeaderText="副合同编号" />
            <asp:BoundField DataField="supplierName" HeaderText="供应商" />
            <asp:BoundField DataField="cash" HeaderText="合同金额" />
            <asp:BoundField DataField="dateLine" HeaderText="供货日期" />
            <asp:BoundField DataField="paymentMode" HeaderText="付款方式" />
            <asp:BoundField DataField="selfPay" HeaderText="已付款额" />
            <asp:TemplateField HeaderText="已收票额">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlReceipt" runat="server" Enabled="False">
                </asp:DropDownList>
            </ItemTemplate>
            </asp:TemplateField>                
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="receiptEdit" runat="server" CausesValidation="False" OnCommand="receiptEdit_Click"
                        CommandName="receiptEdit" Text="修改已开票额">
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnPayApply" runat="server" onclick="btnPayApply_Click"
                    Text="付款申请" />
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
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" 
            style="height: 21px" Text="确认提交修改" Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnNo" runat="server" onclick="btnNo_Click" Text="放弃" 
        Visible="False" Width="40px" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnRtn" runat="server" onclick="btnRtn_Click" Text="返回" 
            Width="40px" />
    </p>
</asp:Content>
