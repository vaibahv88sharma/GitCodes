<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CoordinationCodeReport.ascx.cs" Inherits="Sadara.DigiCom.ReportFinder.WebParts.CoordinationCodeReport.CoordinationCodeReport" %>
<asp:Repeater
     ID="CoordinationCodeRepeater" runat="server">
     <HeaderTemplate>
         <div class='well'>
             <div class="row">
                 <div class='block_inline'>Application Category</div>
                 <div class='block_inline'>Evaluation Question</div>
             </div>
             </div>
     </HeaderTemplate>
     <ItemTemplate>
         <div>
             <div class='block_inline'>
             <b><asp:Literal ID="CoordinationCode" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeDescription) %>'></asp:Literal>
             </b>
                <asp:HyperLink ID="ViewCoordinationCodeHyperLink" runat="server" Target="_blank" NavigateUrl='<%#Sadara.DigiCom.ReportFinder.ViewModel.Constants.Page_ApplicationRequirementDetailsURL+
                    Sadara.DigiCom.ReportFinder.ViewModel.Constants.QueryString_COORDINATIONCODEKEY+"="+ Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSCoordinationCode_FIELDS_CoordinationCodeKey)%>'>View Requirements</asp:HyperLink> 
             </div>
         </div>
     </ItemTemplate>    
</asp:Repeater>