<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="subContractEdit.aspx.cs" Inherits="xm_mis.Main.contractManager.subContractEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;</p>
<asp:GridView ID="mainContractGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="主合同信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="mainContractGV_PageIndexChanging" 
        onsorting="mainContractGV_Sorting" style="margin-left: 0px" 
    onselectedindexchanged="mainContractGV_SelectedIndexChanged" 
        onrowdatabound="mainContractGV_RowDataBound">
    <Columns>
        <asp:TemplateField Visible="False">
            <ItemTemplate>
                <asp:Label ID="lblMainContractId" runat="server" Text='<%# Bind("mainContractId") %>' Visible="false"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="mainContractTag" HeaderText="主合同编号" SortExpression="mainContractTag"/>
        <asp:BoundField DataField="projectTag" HeaderText="项目号" SortExpression="projectTag"/>
        <asp:BoundField DataField="custCompName" HeaderText="客户公司名称（需方）" SortExpression="custCompName"/>
        <asp:BoundField DataField="cash" DataFormatString="{0:c}" HeaderText="合同金额" />
        <asp:TemplateField HeaderText="签约产品（名称，数量）">
            <ItemTemplate>
                <asp:ListBox ID="contractProductLsB" runat="server" Enabled="False" >
                </asp:ListBox>
            </ItemTemplate>
        </asp:TemplateField>
        <%--<asp:TemplateField HeaderText="ccc">
            <ItemTemplate>
                <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>--%>
        <asp:BoundField DataField="dateLine" HeaderText="需货日期" SortExpression="dateLine"/>
        <asp:BoundField DataField="paymentMode" HeaderText="付款方式" />
        <asp:CommandField ShowSelectButton="True" SelectText="编辑副合同" />        
        <asp:TemplateField ShowHeader="False">
            <ItemTemplate>
                <%--<asp:Label ID="lblMessage" runat="server" Text="message" Visible="False"></asp:Label>--%>
                <asp:LinkButton ID="toDel" runat="server" CausesValidation="False" OnClick="toDel_Click"
                CommandName="toDel" CommandArgument='<%# Bind("mainContractId") %>' Text="删除"></asp:LinkButton>
                <asp:LinkButton ID="toEdit" runat="server" CausesValidation="False" OnClick="toEdit_Click"
                CommandName="toEdit" CommandArgument='<%# Bind("mainContractId") %>' Text="修改"></asp:LinkButton>
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
<%--<script type="text/javascript">

    function OpenWindow() {

        window.open("http://www.163.com");

        window.open("http://social.microsoft.com");

//        window.alert("fuck!");

//        window.confirm("fuck you are!");
    }
            </script>
    <br />
            <input type="button" value="Open Window" onclick="OpenWindow();" />
            <asp:button id="Button1" runat="server" 
    text="Button1" 
      OnClientClick="OpenWindow();"/>--%>
            
    <br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button 
            ID="btnAdd" runat="server" onclick="btnAdd_Click" Text="添加主合同" />
        &nbsp;&nbsp;&nbsp; 
    <asp:Button ID="btnAccept" runat="server" onclick="btnAccept_Click" 
            Text="确认提交" style="height: 21px" Visible="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
            style="height: 21px" Text="取消" Visible="False" />
        </asp:Content>
