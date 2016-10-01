<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewReportListData.ascx.cs" Inherits="Sadara.DigiCom.ReportFinder.WebParts.ViewReportListData.ViewReportListData" %>
<asp:GridView ID="ValueListData1" runat="server"></asp:GridView>


<div>
    <asp:GridView ID="ViewValueListData"
        runat="server"
        Width="550px"
        AutoGenerateColumns="false"
        AlternatingRowStyle-BackColor="#C2D69B"
        HeaderStyle-BackColor="green"
        ShowFooter="true">
        <Columns>
            <%--<asp:TemplateField HeaderText="Customer Name">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTREFERENCE) %>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTKEY) %>" />
                    <asp:HiddenField ID="IDHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>" />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="REQUIREMENTREFERENCELabel" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Company Name">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTNUMBER) %>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="REQUIREMENTNUMBERLabel" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Value Description Details">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name) %>
                </ItemTemplate>
             <%--   <FooterTemplate>
                    <asp:DropDownList ID="RequirementTypeDropDownList" runat="server"></asp:DropDownList>
                </FooterTemplate>--%>
            </asp:TemplateField>            
        </Columns>
        <AlternatingRowStyle BackColor="#C2D69B" />       
    </asp:GridView>
</div>

<div>
    <asp:GridView ID="ViewApplicationListData"
        runat="server"
        Width="550px"
        AutoGenerateColumns="false"
        AlternatingRowStyle-BackColor="#C2D69B"
        HeaderStyle-BackColor="green"
        ShowFooter="true">
        <Columns>
            <%--<asp:TemplateField HeaderText="Customer Name">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTREFERENCE) %>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTKEY) %>" />
                    <asp:HiddenField ID="IDHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>" />
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="REQUIREMENTREFERENCELabel" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Company Name">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTNUMBER) %>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Label ID="REQUIREMENTNUMBERLabel" runat="server" Text="Label"></asp:Label>
                </FooterTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Application Description Details">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name) %>
                </ItemTemplate>
             <%--   <FooterTemplate>
                    <asp:DropDownList ID="RequirementTypeDropDownList" runat="server"></asp:DropDownList>
                </FooterTemplate>--%>
            </asp:TemplateField>            
        </Columns>
        <AlternatingRowStyle BackColor="#C2D69B" />       
    </asp:GridView>
</div>
