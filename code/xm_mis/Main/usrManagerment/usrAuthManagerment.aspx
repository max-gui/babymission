<%@ Page Title="" Language="C#" MasterPageFile="~/Main/MasterPage.master" AutoEventWireup="true" CodeBehind="usrAuthManagerment.aspx.cs" Inherits="xm_mis.Main.usrManagerment.usrAuthManagerment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MasterMainContent" runat="server">
    <p>
        <br />
    </p>
    <p>
        &nbsp;</p>
    <asp:GridView ID="usrGV" runat="server" AllowPaging="True" AllowSorting="True" 
        AutoGenerateColumns="False" 
        HorizontalAlign="Center" onpageindexchanging="usrGV_PageIndexChanging" 
        onrowdatabound="usrGV_RowDataBound" 
        onsorting="usrGV_Sorting">
        <Columns>
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblUsrId" runat="server" Text='<%# Bind("usrId") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="realName" HeaderText="员工名字" />
            <asp:BoundField DataField="usrName" HeaderText="用户名" />
            <asp:BoundField DataField="departmentName" HeaderText="所属部门" />
            <asp:BoundField DataField="titleName" HeaderText="职位" />   
            <asp:TemplateField Visible="False">
                <ItemTemplate>
                    <asp:Label ID="lblTotAuth" runat="server" Text='<%# Bind("totleAuthority") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="用户权限" Visible="false">
                <ItemTemplate>
                    <asp:CheckBoxList ID="cblUsrAuth" runat="server" Visible="False">
                    </asp:CheckBoxList>
                </ItemTemplate>                
            </asp:TemplateField>
            <asp:TemplateField HeaderText="用户权限">
                <ItemTemplate>
                    <asp:LinkButton ID="toEdit" runat="server" CausesValidation="False" OnClick="toEdit_Click"
                        CommandName="toEdit" CommandArgument='<%# Bind("totleAuthority") %>' Text="修改">
                    </asp:LinkButton>
                </ItemTemplate>                
            </asp:TemplateField>            
        </Columns>
        <SelectedRowStyle BackColor="#339966" />
    </asp:GridView>
    <p>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
    <asp:Button ID="btnAccept" runat="server" onclick="btnAccept_Click" 
            Text="确认提交" style="height: 21px" Visible="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnCancel" runat="server" onclick="btnCancel_Click" 
            style="height: 21px" Text="取消" Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
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
