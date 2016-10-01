<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportFinders.ascx.cs" Inherits="Sadara.DigiCom.ReportFinder.WebParts.ReportFinders.ReportFinders" %>



<link rel="stylesheet" href="//code.jquery.com/ui/1.10.3/themes/smoothness/jquery-ui.css" />
<script type="text/javascript" src="//code.jquery.com/jquery-1.11.0.min.js"></script>
<script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>

<%--<script type="text/javascript" src="../../_layouts/15/init.js"></script>
<script type="text/javascript" src="../../_layouts/15/sp.runtime.js"></script>
<script type="text/javascript" src="../../_layouts/15/sp.js"></script>--%>

<asp:Label Text="" ForeColor="Red" ID="ScriptLiteral" runat="server" />

<style>
    div {
        border: 0px solid #333;
    }

    .well {
        display: table;
        width: 100%;
    }

    .row {
        display: table-row;
        margin-top: 0px;
        padding-top: 0px;
    }

    .block_inline {
        vertical-align: top;
        display: inline-block;
        width: 25%;
    }
</style>

<script type="text/javascript">

    var clientContext;
    var collListItem;
    var sArray = [];
    $(document).ready(function () {
        //  ExecuteOrDelayUntilScriptLoaded(getAutocompleteSource, "sp.js");
        var AllRequirementReferenceHiddenFieldval = $("#AllRequirementReferenceHiddenField").val();
        var AllRequirementReferenceNameHiddenField = $("#AllRequirementReferenceNameHiddenField").val();
        var arrayRR = AllRequirementReferenceHiddenFieldval.split('-');
        var arrayRRN = AllRequirementReferenceNameHiddenField.split('-');

        for (var i = 0; i < arrayRR.length; i++) {
            sArray.push(arrayRR[i]);
            sArray.push(arrayRRN[i]);
        }
        $("#autocompleteTextBox").autocomplete({
            source: sArray,
            minLength: 1,
            select: function (event, ui) {
                var selectedObj = ui.item;
                $("#RequirementReferenceHiddenField").val(selectedObj.value);
                $("#SearchButton").click();
            }
        });
    });

</script>

<div style="margin-top: 15px; margin-bottom: 15px;">
    REQUIREMENT REFERENCE:  
    <asp:TextBox ID="autocompleteTextBox" CssClass="autocompleteTextBox" ClientIDMode="Static" runat="server" Style="width: 60%"></asp:TextBox>
    <asp:HiddenField ID="AllRequirementReferenceHiddenField" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="AllRequirementReferenceNameHiddenField" ClientIDMode="Static" runat="server" />
    <asp:HiddenField ID="RequirementReferenceHiddenField" ClientIDMode="Static" runat="server" />
    <asp:Button ID="SearchButton" ClientIDMode="Static" runat="server" Text="Search" Style="display: none" />
</div>


<div>
    <asp:RadioButton ID="DriverDetailsRadioButton" GroupName="Requirement" Checked="true" Text="Driver Details" runat="server" AutoPostBack="true" />
    <asp:RadioButton ID="ApplicationDetailsRadioButton" GroupName="Requirement" Text="Application Details" runat="server" AutoPostBack="true" />
    <asp:RadioButton ID="ReqDiffCodeMappingRadioButton" GroupName="Requirement" Text="Requirement Differentiation Code" runat="server" AutoPostBack="true" />
    <asp:RadioButton ID="ValueCriteriaRadioButton" GroupName="Requirement" Text="Value Criteria" runat="server" AutoPostBack="true" />
    <asp:RadioButton ID="CoordinationCodeRadioButton" GroupName="Requirement" Text="Coordination Code" runat="server" AutoPostBack="true" />
    <asp:RadioButton ID="RequirementsOnlyRadioButton" GroupName="Requirement" Text="Requirements Only(Excel Export)" runat="server" AutoPostBack="true" />

</div>
<asp:Button runat="server" Text="Export To Excel" ID="btnExportToExcel" OnClick="btnExportToExcel_Click" Visible = "false"/>​
 <asp:Repeater
     ID="RequirementRepeater"
     OnItemDataBound="RequirementRepeater_ItemDataBound"
     runat="server">
     <HeaderTemplate>
         <div class='well'>
             <div class="row">
                 <div class='block_inline'>Requirement Number</div>
                 <div class='block_inline'>Requirement Detail</div>
                 <div class='block_inline'></div>
                 <div class='block_inline'></div>
             </div>
     </HeaderTemplate>

     <ItemTemplate>
         <div class="row">
             <div class='block_inline' style="width: 15%">
                 <asp:Literal ID="RequirementNumberLiteral" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTNUMBER) %>'></asp:Literal>
                 <asp:HiddenField ID="requirementKeyHiddenField" runat="server" Value='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY) %>'></asp:HiddenField>

             </div>
             <div class='block_inline' style="width: 60%">
                 <asp:Literal ID="RequirementDetailLiteral" runat="server"
                     Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTDESCRIPTION) %>'></asp:Literal>
                 <%--Start Repeator for only All details --%>
                 <asp:Repeater ID="ApplicationDetailsRepeater"
                     runat="server" OnItemDataBound="ApplicationDetailsRepeater_ItemDataBound">
                     <HeaderTemplate>
                         <ul>
                             <b><asp:Label ID="DetailHeaderLabel" runat="server" Text=""></asp:Label></b>
                     </HeaderTemplate>

                     <ItemTemplate>
                         <li>
                             <asp:Literal ID="ApplicationLiteral" runat="server"
                                 Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_FirstColumn) %>'></asp:Literal>
                             <br />
                             <asp:Literal ID="Literal1" runat="server"
                                 Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_SecondColumn) %>'></asp:Literal>
                         </li>
                     </ItemTemplate>
                     <FooterTemplate>
                         </ul>
                     </FooterTemplate>
                 </asp:Repeater>
                 <%--End Repeator for only All details --%>
             </div>
             <div class='block_inline' style="width: 10%">
                 <asp:HyperLink ID="AddDeleteHyperLink" runat="server" Target="_blank" NavigateUrl='<%#Sadara.DigiCom.ReportFinder.ViewModel.Constants.Page_RequirementPageDetailsURL+
                    Sadara.DigiCom.ReportFinder.ViewModel.Constants.QueryString_REQUIREMENTNUMBER+"="+ Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTNUMBER)+
                    "&"+ Sadara.DigiCom.ReportFinder.ViewModel.Constants.QueryString_REQUIREMENTKEY+"="+ Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY)+
                   "&"+ Sadara.DigiCom.ReportFinder.ViewModel.Constants.QueryString_REQUIREMENTREFERENCE+"="+RequirementReferenceHiddenField.Value      +
                   "&"+Sadara.DigiCom.ReportFinder.ViewModel.Constants.QueryString_Type+"="+ Sadara.DigiCom.ReportFinder.ViewModel.Constants.SelectedType %>'>Add/Remove</asp:HyperLink>
             </div>
             <div class='block_inline' style="width: 10%">
                 <asp:HyperLink ID="SeeDetailsHyperLink" runat="server" Target="_blank" NavigateUrl='<%#Sadara.DigiCom.ReportFinder.ViewModel.Constants.Page_SeeRequirementDetailsURL+
                    Sadara.DigiCom.ReportFinder.ViewModel.Constants.QueryString_REQUIREMENTNUMBER+"="+ Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTNUMBER)+
                    "&"+ Sadara.DigiCom.ReportFinder.ViewModel.Constants.QueryString_REQUIREMENTKEY+"="+ Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY)+
                   "&"+ Sadara.DigiCom.ReportFinder.ViewModel.Constants.QueryString_REQUIREMENTREFERENCE+"="+RequirementReferenceHiddenField.Value      +
                   "&"+Sadara.DigiCom.ReportFinder.ViewModel.Constants.QueryString_Type+"="+ Sadara.DigiCom.ReportFinder.ViewModel.Constants.SelectedType %>'>See Details</asp:HyperLink>
             </div>

         </div>
     </ItemTemplate>

     <FooterTemplate>
         <div>
     </FooterTemplate>     
 </asp:Repeater>


