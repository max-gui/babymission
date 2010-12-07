<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="subContractEditing.aspx.cs" Inherits="xm_mis.Main.contractManager.subContractEditing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        主合同编号：&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblMainContractTag" runat="server" Text="lblMainContractTag"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 项目号：&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblProjectTag" runat="server" Text="lblProjectTag"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
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
    <p>
        主合同简约产品（名称，数量）：&nbsp;<asp:ListBox ID="contractProductLsB" runat="server" 
            Enabled="False" Rows="1"></asp:ListBox>
    </p>
    <asp:GridView ID="subContractGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="所属副合同信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="subContractGV_PageIndexChanging" 
        onsorting="subContractGV_Sorting" style="margin-left: 0px" 
        onrowdatabound="subContractGV_RowDataBound">
        <Columns>
            <asp:BoundField DataField="subContractTag" HeaderText="副合同编号" SortExpression="subContractTag" />
            <asp:BoundField DataField="supplierName" HeaderText="供应商" />
            <asp:BoundField DataField="cash" HeaderText="合同金额" DataFormatString="{0:c}" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:ListBox ID="subContractProductLsB" runat="server" Enabled="False" Rows="1">
                    </asp:ListBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="dateLine" HeaderText="供货日期" SortExpression="dateLine" />
            <asp:BoundField DataField="paymentMode" HeaderText="付款方式" />                    
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <%--<asp:Label ID="lblMessage" runat="server" Text="message" Visible="False"></asp:Label>--%>
                    <asp:LinkButton ID="toDel" runat="server" CausesValidation="False" OnClick="toDel_Click"
                    CommandName="toDel" CommandArgument='<%# Bind("subContractId") %>' Text="删除"></asp:LinkButton>
                    <asp:LinkButton ID="toEdit" runat="server" CausesValidation="False" OnClick="toEdit_Click"
                    CommandName="toEdit" CommandArgument='<%# Bind("subContractId") %>' Text="修改"></asp:LinkButton>
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
            Text="添加副合同" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:Button ID="btnAccept" runat="server" onclick="btnAccept_Click" 
            Text="确认提交" Visible="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
            style="height: 21px" Text="取消" Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </p>
</asp:Content>
