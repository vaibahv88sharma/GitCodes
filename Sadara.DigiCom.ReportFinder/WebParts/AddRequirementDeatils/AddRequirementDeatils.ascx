<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AddRequirementDeatils.ascx.cs" Inherits="Sadara.DigiCom.ReportFinder.WebParts.AddRequirementDeatils.AddRequirementDeatils" %>


<style>
    .button {
        min-width: 6em;
        padding: 7px 10px;
        border: 1px solid #ababab;
        background-color: #fdfdfd;
        background-color: #fdfdfd;
        margin-left: 10px;
        font-family: "Segoe UI","Segoe",Tahoma,Helvetica,Arial,sans-serif;
        font-size: 11px;
        color: #444;
    }

    /*table tr {
        min-height: 40px;
        
    }
    table tr td {
       min-height: 40px;
        
    }*/
</style>
<asp:Label Text="" ForeColor="Red" ID="ScriptLiteral" runat="server" />
<div>

    <div>
        Requirement Reference:
        <asp:Label ID="REQUIREMENTREFERENCELabel" runat="server" Text=""></asp:Label>
        <br />
        Requirement Number:
        <asp:Label ID="REQUIREMENTNUMBERLabel" runat="server" Text=""></asp:Label>
        <br />
        Requirement Description:
        <asp:Label ID="RequirementDescriptionLabel1" runat="server" Text=""></asp:Label>
    </div>

    <div style="text-align: center">
        <asp:Label ID="ErrorLabel" runat="server" Text=""></asp:Label>
    </div>

    <asp:GridView ID="AddRequirementGridView"
        runat="server"
        Width="60%"
        AutoGenerateColumns="false"
        AlternatingRowStyle-BackColor="#C2D69B"
        HeaderStyle-BackColor="green"
        ShowFooter="true"
        Visible="false"
        GridLines="None"
        OnRowDataBound="AddRequirementGridView_RowDataBound"
        OnRowCommand="AddRequirementGridView_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="Driver Description">
                <ItemTemplate>

                    <asp:HiddenField ID="ODMSDriverListIDHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ODMSDriverListID) %>" />
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTKEY) %>" />
                    <asp:HiddenField ID="IDHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>" />
                    <div>
                        <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name) %>
                    </div>
                    <div>
                        <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_DetailDescription) %>
                    </div>

                </ItemTemplate>
                <FooterTemplate>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="" />
                    <div>
                        <asp:Label ID="RequirementTypeLabel" runat="server" ClientIDMode="Static" Text="Driver Description: "></asp:Label>
                        <asp:DropDownList ID="RequirementTypeDropDownList" ClientIDMode="Static" runat="server"></asp:DropDownList>
                    </div>
                    <div>
                        <asp:Label ID="RequirementDescrtiptionLabel" runat="server" Text="Detailed Driver Description: " ClientIDMode="Static"></asp:Label>
                        <asp:TextBox ID="RequirementDescrtiptionLabelTextBox" ValidationGroup="btnAddDriver" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            runat="server"
                            ID="reqName"
                            ForeColor="Red"
                            SetFocusOnError="true"
                            ValidationGroup="btnAddDriver"
                            EnableClientScript="true"
                            ControlToValidate="RequirementDescrtiptionLabelTextBox"
                            ErrorMessage="*" />

                    </div>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <div>
                        <asp:LinkButton CssClass="button" ID="lnkBtnDel" runat="server" CommandName="DeleteRow"
                            OnClientClick="return confirm('Are you sure you want to Remove this Record?');"
                            CommandArgument='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTKEY) %>'>Remove</asp:LinkButton>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="btnAddDriver" ValidationGroup="btnAddDriver" runat="server" Text="Add" OnClick="Add" CommandName="Footer" />
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="#C2D69B" />
        <EmptyDataTemplate>
            <tr style="background-color: Green;">
                <th scope="col">NAME
                </th>
                <th scope="col"></th>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="" />
                    <div>
                        <asp:Label ID="RequirementTypeLabel" runat="server" ClientIDMode="Static" Text="Driver Description: "></asp:Label>
                        <asp:DropDownList ID="RequirementTypeDropDownList" ClientIDMode="Static" runat="server"></asp:DropDownList>
                    </div>
                    <div>
                        <asp:Label ID="RequirementDescrtiptionLabel" runat="server" Text="Detailed Driver Description: " ClientIDMode="Static"></asp:Label>
                        <asp:TextBox ID="RequirementDescrtiptionLabelTextBox" ValidationGroup="btnAddDriver" runat="server" ClientIDMode="Static"></asp:TextBox>
                        <asp:RequiredFieldValidator
                            runat="server"
                            ID="reqName"
                            ForeColor="Red"
                            SetFocusOnError="true"
                            ValidationGroup="btnAddDriver"
                            EnableClientScript="true"
                            ControlToValidate="RequirementDescrtiptionLabelTextBox"
                            ErrorMessage="*" />

                    </div>
                </td>
                <td>
                    <asp:Button ID="btnAddDriver" ValidationGroup="btnAddDriver" runat="server" Text="Add" OnClick="Add" CommandName="Footer" />
                </td>

            </tr>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:GridView ID="ODMSReqDiffCodeGridView"
        runat="server"
        Width="60%"
        GridLines="None"
        AutoGenerateColumns="false"
        AlternatingRowStyle-BackColor="#C2D69B"
        HeaderStyle-BackColor="green"
        ShowFooter="true"
        Visible="false"
        OnRowDataBound="ODMSReqDiffCodeGridView_RowDataBound"
        OnRowCommand="ODMSReqDiffCodeGridView_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="Geography">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Geography) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Compliance Plane Category">
                <ItemTemplate>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTKEY) %>" />
                    <asp:HiddenField ID="IDHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>" />
                    <div>
                        <b>Category</b> <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ReqDiffCodeID) %>- 
                    </div>
                    <div>
                        <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ReqDiffCodeDescription) %>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="" />
                    <div>
                        <asp:Label ID="RequirementTypeLabel" runat="server" ClientIDMode="Static" Text="Geography: "></asp:Label>
                        <asp:DropDownList ID="GeographyDropDownList" ClientIDMode="Static" runat="server"></asp:DropDownList>
                    </div>
                    <div>
                        <asp:Label ID="RequirementDescrtiptionLabel" runat="server" Text="Compliance Plane Category: " ClientIDMode="Static"></asp:Label>
                        <asp:DropDownList ID="CompliancePlaneCategoryDropDownList" ClientIDMode="Static" runat="server"></asp:DropDownList>
                    </div>
                    <div>
                        <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Text=""></asp:Label>
                    </div>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton CssClass="button" ID="lnkBtnDel" runat="server" CommandName="DeleteRow"
                        OnClientClick="return confirm('Are you sure you want to Remove this Record?');"
                        CommandArgument='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTKEY) %>'>Remove</asp:LinkButton>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:Button ID="btnAddDriver" runat="server" Text="Add" OnClick="AddReqDiffCode" CommandName="Footer" />
                </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="#C2D69B" />
        <EmptyDataTemplate>
            <tr style="background-color: Green;">
                <th scope="col">NAME
                </th>
                <th scope="col"></th>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="" />
                    <div>
                        <asp:Label ID="RequirementTypeLabel" runat="server" ClientIDMode="Static" Text="Geography: "></asp:Label>
                        <asp:DropDownList ID="GeographyDropDownList" ClientIDMode="Static" runat="server"></asp:DropDownList>
                    </div>
                    <div>
                        <asp:Label ID="RequirementDescrtiptionLabel" runat="server" Text="Compliance Plane Category: " ClientIDMode="Static"></asp:Label>
                        <asp:DropDownList ID="CompliancePlaneCategoryDropDownList" ClientIDMode="Static" runat="server"></asp:DropDownList>
                    </div>
                    <div>
                        <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Text=""></asp:Label>
                    </div>
                </td>
                <td>
                    <asp:Button ID="btnAddDriver" runat="server" Text="Add" OnClick="AddReqDiffCode" CommandName="Footer" />
                </td>

            </tr>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:GridView ID="CoordinationCodeGridView"
        runat="server"
        Width="60%"
        GridLines="None"
        Visible="false"
        AutoGenerateColumns="false"
        AlternatingRowStyle-BackColor="#C2D69B"
        HeaderStyle-BackColor="green"
        ShowFooter="true"
        OnRowDataBound="CoordinationCodeGridView_RowDataBound"
        OnRowCommand="CoordinationCodeGridView_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="NAME">
                <ItemTemplate>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTKEY) %>" />
                    <asp:HiddenField ID="IDHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>" />

                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name) %>  <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_DetailDescription) %>
                </ItemTemplate>
                <FooterTemplate>
                    <%-- <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="" />
                    <div>
                        <asp:Label ID="CoordinationCodeTypeLabel" runat="server" ClientIDMode="Static" Text="Coordination Code: "></asp:Label>
                        <asp:DropDownList ID="CoordinationCodeDropDownList" Style="width: 100%" ClientIDMode="Static" runat="server"></asp:DropDownList>
                        <asp:RequiredFieldValidator
                            runat="server"
                            ID="reqName"
                            ForeColor="Red"
                            SetFocusOnError="true"
                            ValidationGroup="btnAddCordinationCode"
                            EnableClientScript="true"
                            ControlToValidate="CoordinationCodeDropDownList"
                            ErrorMessage="*" />
                    </div>--%>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="" />
                    <div>
                        <asp:Label ID="CoordinationCodeTypeLabel" runat="server" ClientIDMode="Static" Text="Coordination Code: "></asp:Label>
                        <asp:GridView ID="CoordinationCodeTypeGridView"
                            runat="server"
                            Width="550px"
                            GridLines="None"
                            AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="#C2D69B"
                            HeaderStyle-BackColor="green"
                            ShowFooter="true" OnRowCommand="CoordinationCodeTypeGridView_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Coordination Code">
                                    <ItemTemplate>
                                        <%#Eval("Value") %>
                                        <asp:HiddenField ID="CoordinationCodeHiddenField" runat="server" Value='<%#Eval("Key") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coordination Code">
                                    <ItemTemplate>
                                        <asp:Button ID="Button1"
                                            runat="server"
                                            Text="Add"
                                            CommandArgument='<%#Eval("Key") %>'
                                            CommandName="Footer" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton CssClass="button" ID="lnkBtnDel" runat="server" CommandName="DeleteRow"
                        OnClientClick="return confirm('Are you sure you want to Remove this Record?');"
                        CommandArgument='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTKEY) %>'>Remove</asp:LinkButton>
                </ItemTemplate>
                <%--<FooterTemplate>
                    <asp:Button ID="btnAddCordinationCode" ValidationGroup="btnAddCordinationCode" runat="server" Text="Add" OnClick="AddCordinationCode" CommandName="Footer" />
                </FooterTemplate>--%>                
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="#C2D69B" />
        <EmptyDataTemplate>
            <tr style="background-color: Green;">
                <th scope="col">NAME
                </th>
                <th scope="col"></th>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="" />
                    <div>
                        <asp:Label ID="CoordinationCodeTypeLabel" runat="server" ClientIDMode="Static" Text="Coordination Code: "></asp:Label>
                        <asp:GridView ID="CoordinationCodeTypeGridView"
                            runat="server"
                            Width="550px"
                            GridLines="None"
                            AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="#C2D69B"
                            HeaderStyle-BackColor="green"
                            ShowFooter="true" OnRowCommand="CoordinationCodeTypeGridView_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Coordination Code">
                                    <ItemTemplate>
                                        <%#Eval("Value") %>
                                        <asp:HiddenField ID="CoordinationCodeHiddenField" runat="server" Value='<%#Eval("Key") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Coordination Code">
                                    <ItemTemplate>
                                        <asp:Button ID="Button1"
                                            runat="server"
                                            Text="Add"
                                            CommandArgument='<%#Eval("Key") %>'
                                            CommandName="Footer" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>
                <%-- <td>
                    <asp:Button ID="btnAddCordinationCode" runat="server" Text="Add" OnClick="AddCordinationCode" CommandName="Footer" />
                </td>--%>
            </tr>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:GridView ID="ViewODMSValueGridView"
        runat="server"
        Visible="false"
        GridLines="None"
        AutoGenerateColumns="false"
        AlternatingRowStyle-BackColor="#C2D69B"
        HeaderStyle-BackColor="green"
        ShowFooter="true"
        Width="60%"
        OnRowDataBound="ViewODMSValueGridView_RowDataBound"
        OnRowCommand="ViewODMSValueGridView_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="NAME">
                <ItemTemplate>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTKEY) %>" />
                    <asp:HiddenField ID="IDHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>" />

                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name) %>  <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_DetailDescription) %>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="" />
                    <div>

                        <asp:GridView ID="ODMSValueTypeGridView"
                            runat="server"
                            GridLines="None"
                            AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="#C2D69B"
                            HeaderStyle-BackColor="green"
                            ShowFooter="true" OnRowCommand="ODMSValueTypeGridView_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Value Code">
                                    <ItemTemplate>
                                        <%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Value) %>
                                        <asp:HiddenField ID="IDHiddenField" runat="server" Value='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value Description">
                                    <ItemTemplate>
                                        <%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ValueDescription) %>
                                        <asp:HiddenField ID="ValueIDField" runat="server" Value='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ValueId) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="Button1"
                                            runat="server"
                                            Text="Add"
                                            CommandArgument='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>'
                                            CommandName="Footer" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton CssClass="button" ID="lnkBtnDel" runat="server" CommandName="DeleteRow"
                        OnClientClick="return confirm('Are you sure you want to Remove this Record?');"
                        CommandArgument='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTKEY) %>'>Remove</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="#C2D69B" />
        <EmptyDataTemplate>
            <tr style="background-color: Green;">
                <th scope="col">NAME
                </th>
                <th scope="col"></th>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="" />
                    <div>

                        <asp:GridView ID="ODMSValueTypeGridView"
                            runat="server"
                            GridLines="None"
                            AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="#C2D69B"
                            HeaderStyle-BackColor="green"
                            ShowFooter="true" OnRowCommand="ODMSValueTypeGridView_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Value Code">
                                    <ItemTemplate>
                                        <%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Value) %>
                                        <asp:HiddenField ID="IDHiddenField" runat="server" Value='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Value Description">
                                    <ItemTemplate>
                                        <%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ValueDescription) %>
                                        <asp:HiddenField ID="ValueIDField" runat="server" Value='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ValueId) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="Button1"
                                            runat="server"
                                            Text="Add"
                                            CommandArgument='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>'
                                            CommandName="Footer" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>

            </tr>
        </EmptyDataTemplate>
    </asp:GridView>

    <asp:GridView ID="ApplicationViewGridView"
        runat="server"
        Visible="false"
        GridLines="None"
        AutoGenerateColumns="false"
        AlternatingRowStyle-BackColor="#C2D69B"
        HeaderStyle-BackColor="green"
        ShowFooter="true"
        Width="60%"
        OnRowDataBound="ApplicationViewGridView_RowDataBound"
        OnRowCommand="ApplicationViewGridView_RowCommand">
        <Columns>
            <asp:TemplateField HeaderText="NAME">
                <ItemTemplate>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTKEY) %>" />
                    <asp:HiddenField ID="IDHiddenField" runat="server" Value="<%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>" />
                    <div>
                        <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name) %>
                    </div>
                    <div>
                        <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_DetailDescription) %>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="" />
                    <div>

                        <asp:GridView ID="ODMSApplicationTypeGridView"
                            runat="server"
                            AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="#C2D69B"
                            HeaderStyle-BackColor="green"
                            GridLines="None"
                            OnRowCommand="ODMSApplicationTypeGridView_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Application Catigory">
                                    <ItemTemplate>
                                        <%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ApplicationCatigory) %>
                                        <asp:HiddenField ID="IDHiddenField" runat="server" Value='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Application Description">
                                    <ItemTemplate>
                                        <%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ApplicationDescription) %>
                                        <asp:HiddenField ID="ApplicationIDField" runat="server" Value='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ApplicationId) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="Button1"
                                            runat="server"
                                            Text="Add"
                                            CommandArgument='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>'
                                            CommandName="Footer" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </FooterTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:LinkButton ID="lnkBtnDel" runat="server" CssClass="button" CommandName="DeleteRow"
                        OnClientClick="return confirm('Are you sure you want to Remove this Record?');"
                        CommandArgument='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_REQUIREMENTKEY) %>'>Remove</asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle BackColor="#C2D69B" />
        <EmptyDataTemplate>
            <tr style="background-color: Green;">
                <th scope="col">NAME
                </th>
                <th scope="col"></th>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="REQUIREMENTKEYHiddenField" runat="server" Value="" />
                    <div>

                        <asp:GridView ID="ODMSApplicationTypeGridView"
                            runat="server"
                            GridLines="None"
                            AutoGenerateColumns="false"
                            AlternatingRowStyle-BackColor="#C2D69B"
                            HeaderStyle-BackColor="green"
                            OnRowCommand="ODMSApplicationTypeGridView_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="Application Catigory">
                                    <ItemTemplate>
                                        <%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ApplicationCatigory) %>
                                        <asp:HiddenField ID="IDHiddenField" runat="server" Value='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Application Description">
                                    <ItemTemplate>
                                        <%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ApplicationDescription) %>
                                        <asp:HiddenField ID="ApplicationIDField" runat="server" Value='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ApplicationId) %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <asp:Button ID="Button1"
                                            runat="server"
                                            Text="Add"
                                            CommandArgument='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>'
                                            CommandName="Footer" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </td>

            </tr>
        </EmptyDataTemplate>
    </asp:GridView>

</div>
