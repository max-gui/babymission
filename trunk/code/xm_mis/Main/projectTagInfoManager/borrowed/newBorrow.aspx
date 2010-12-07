<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="newBorrow.aspx.cs" Inherits="xm_mis.Main.projectTagInfoManager.borrowed.newBorrow" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <strong>借出申请</strong></p>
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnOut" runat="server" onclick="btnOut_Click" Text="转为客户公司借用申请" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnIn" runat="server" onclick="btnIn_Click" Text="转为本公司员工借用申请" 
            Visible="False" />
    </p>
    <p>
        <asp:Label ID="lblCustCompNameShow" runat="server" Text="客户公司名称：" 
            Visible="False"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:DropDownList ID="ddlCustComp" runat="server" 
            onselectedindexchanged="ddlCustComp_SelectedIndexChanged" 
            AutoPostBack="True" Height="16px" Visible="False">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblCustCompAddrShow" runat="server" Text="客户公司地址：" 
            Visible="False"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCustCompAddr" runat="server" Text="custCompAddr" 
            Visible="False"></asp:Label>
    </p>
    <p>
        <asp:Label ID="lblCustManShow" runat="server" Text="客户项目经理姓名：" 
            Visible="False"></asp:Label>
    &nbsp;<asp:DropDownList ID="ddlCustMan" runat="server" 
            onselectedindexchanged="ddlCustMan_SelectedIndexChanged" 
            AutoPostBack="True" Visible="False">
        </asp:DropDownList>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblCustManContantShow" runat="server" Text="联系方式：" 
            Visible="False"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
         
        <asp:Label ID="lblCustManCont" runat="server" Text="custManContant" 
            Visible="False"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="lblCustManEmailShow" runat="server" Text="Email：" 
            Visible="False"></asp:Label>
        &nbsp;&nbsp;&nbsp; 
        <asp:Label ID="lblCustManEmail" runat="server" Text="custManEmail" 
            Visible="False"></asp:Label>
  


    
        </p>
    <p>
        产品列表：</p>
<asp:GridView ID="borrowProductGV" runat="server" AllowPaging="True" 
        AllowSorting="True" AutoGenerateColumns="False" BackColor="White" 
        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" Caption="试用产品信息" 
        CellPadding="4" GridLines="Horizontal" HorizontalAlign="Center" 
        onpageindexchanging="borrowProductGV_PageIndexChanging" 
        onsorting="borrowProductGV_Sorting" style="margin-left: 0px">
    <Columns>
        <asp:BoundField DataField="productName" HeaderText="产品名称" />
        <asp:BoundField DataField="productTag" HeaderText="产品编号" />
        <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="selectOk" runat="server" CausesValidation="False" OnClick="selectOk_Click"
                        CommandName="selectOk" Text="选定"></asp:LinkButton>
                </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ShowHeader="False" Visible="False">
                <ItemTemplate>
                    <asp:LinkButton ID="selectNo" runat="server" CausesValidation="False" OnClick="selectNo_Click"
                        CommandName="selectNo" Text="重选"></asp:LinkButton>
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
        &nbsp;申请内容：   <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtSynopsis" runat="server" Height="77px" TextMode="MultiLine" 
            Width="623px" ontextchanged="txtSynopsis_TextChanged"></asp:TextBox>
    </p>
    <p>
        &nbsp;发货地址：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <asp:TextBox ID="txtProjAddr" runat="server" Width="627px"></asp:TextBox>
    </p>
    <p>
        &nbsp;</p>
    <p>
        审批人：&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="ddlApproveUsr" runat="server">
        </asp:DropDownList>
    </p>
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnOk" runat="server" onclick="btnOk_Click" Text="确认提交申请" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
            Text="取消提交申请" />
    </p>
</asp:Content>
