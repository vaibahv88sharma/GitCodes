<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FilteredRequirements.ascx.cs" Inherits="Sadara.DigiCom.ReportFinder.WebParts.FilteredRequirements.FilteredRequirements" %>
<asp:Repeater
     ID="RequirementRepeater" runat="server">
     <HeaderTemplate>
        
     </HeaderTemplate>
     <ItemTemplate>
         <div>
             <div class='block_inline'>
              <asp:Literal ID="ApplicationCategoryLiteral" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTDESCRIPTION) %>'></asp:Literal>
               <%---  <asp:Literal ID="EvaluationQuestionLiteral" runat="server"  Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY) %>'></asp:Literal>--%>
                          <asp:HyperLink ID="ViewDetailedReport" runat="server" Target="_blank" NavigateUrl='<%#Sadara.DigiCom.ReportFinder.ViewModel.ReqFinderInfoMethods.Page_DetailedReportURL+                             
                    Sadara.DigiCom.ReportFinder.ViewModel.Constants.QueryString_REQUIREMENTKEY+"="+ Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY)%>'>View Details</asp:HyperLink>
                 
             </div>
          
                

         </div>
     </ItemTemplate>


 </asp:Repeater>