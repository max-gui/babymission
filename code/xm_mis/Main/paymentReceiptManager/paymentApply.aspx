<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="paymentApply.aspx.cs" Inherits="xm_mis.Main.paymentReceiptManager.paymentApply" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;</p>
    <asp:GridView ID="selfPaymentGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="付款申请信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="selfPaymentGV_PageIndexChanging" 
        onsorting="selfPaymentGV_Sorting" style="margin-left: 0px">
        <Columns>
            <asp:BoundField DataField="custMaxPay" HeaderText="客户已付款" DataFormatString="{0:p}"
                ReadOnly="True" />
            <asp:BoundField DataField="toPay" HeaderText="申请付款额" DataFormatString="{0:p}" />
            <asp:BoundField DataField="hasPayPercent" HeaderText="已付款额" DataFormatString="{0:p}"
                ReadOnly="True" />
            <asp:BoundField DataField="payPercent" HeaderText="总计付款额" DataFormatString="{0:p}"
                ReadOnly="True" />
            <asp:BoundField DataField="paymentExplication" HeaderText="申请内容" />
            <asp:BoundField DataField="paymentApplyResult" HeaderText="是否批准" ReadOnly="True" />
            <asp:BoundField DataField="paymentComment" HeaderText="审批意见" ReadOnly="True" />
            <asp:BoundField DataField="Done" HeaderText="完成日期" ReadOnly="True" />
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <%--<asp:Label ID="lblMessage" runat="server" Text="message" Visible="False"></asp:Label>--%>
                    <asp:LinkButton ID="toDel" runat="server" CausesValidation="False" OnClick="toDel_Click"
                    CommandName="toDel" CommandArgument='<%# Bind("paymentId") %>' Text="删除"></asp:LinkButton>
                    <asp:LinkButton ID="toEdit" runat="server" CausesValidation="False" OnClick="toEdit_Click"
                    CommandName="toEdit" CommandArgument='<%# Bind("paymentId") %>' Text="修改"></asp:LinkButton>
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
    <br />&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAdd" runat="server" onclick="btnAdd_Click" 
            style="height: 21px" Text="提交新的申请" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAccept" runat="server" onclick="btnAccept_Click" 
            Text="确认提交" Visible="False" />
&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
            style="height: 21px" Text="取消" Visible="False" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnNo" runat="server" onclick="btnNo_Click" Text="返回" />
</asp:Content>
