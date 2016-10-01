﻿<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportSearchByApplicability.ascx.cs" Inherits="Sadara.DigiCom.ReportFinder.WebParts.ReportSearchByApplicability.ReportSearchByApplicability" %>

<div id="MainDiv" runat="server">
<asp:Repeater
     ID="ApplicationCategoryRepeater" OnItemDataBound="ApplicationCategoryRepeater_ItemDataBound" runat="server">
     <HeaderTemplate>
         <div class='well'>
             <div class="row">
                  <table style="width:100%">
                      <tr style="width:100%">
                        <td style="width:3%"><div class='block_inline'></div></td>
                        <td style="width:20%"><div class='block_inline'>Category</div></td>
                        <td style="width:77%"><div class='block_inline'>Evaluation Question</div></td>		
                      </tr>
                   </table>
             <%--     <div class='block_inline'>Application Category</div>
                 <div class='block_inline'>Evaluation Question</div> --%>
                    
             </div>
     </HeaderTemplate>
     <ItemTemplate>
         <div>              
  
             <%-- <div class='block_inline'>--%>
             <div id ='h<%#Container.ItemIndex%>' class='block_inline' onclick="getdata(<%#Container.ItemIndex %>)" >
                <table style="width:100%" >
                    <tr style="width:100%">
                        <td style="width:3%">
                            <img id="_prvCtlImagePlusMinus" runat="server" alt="Expand/Collapse" src="../../Style Library/plus.png"/>
                        </td>
                        <td style="width:20%">
                            <b><asp:Literal ID="ApplicationCategoryLiteral" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSApplicationCategory_FIELDS_ApplicationCategory) %>'></asp:Literal>        --    </b>
                        </td>
                        <td style="width:77%">
                             <asp:Literal ID="EvaluationQuestionLiteral" runat="server"  Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSApplicationCategory_FIELDS_EvaluationQuestion) %>'></asp:Literal>
                        </td>		
                    </tr>
                </table>      
             </div>
          
              <div id ='d<%#Container.ItemIndex%>' class='block_inline2'>
                 <%--Start Repeator for Perticular Application Category details --%>
                 <asp:Repeater ID="ApplicationRepeater" runat="server">
                     <HeaderTemplate>
                         <ul>
                     </HeaderTemplate>
                     <%--  --%>
                     <ItemTemplate>
                         <li style="list-style:none">
                            <table style="width:100%">
                                <tr style="width:100%">                                    
                                    <td style="width:70%"> 
                                        <b>                                       
                                        <asp:Literal ID="DetailLiteral" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSApplication_FIELDS_ApplicationCategory) %>'></asp:Literal>                                        
                                        -- </b>
                                        <asp:Literal ID="Literal2" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSApplication_FIELDS_ApplicationDescription) %>'></asp:Literal>
                                        <asp:HiddenField ID ="HiddenFieldApplicationKey" runat="server" Value='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSApplication_FIELDS_ApplicationID) %>'></asp:HiddenField>                                        
                                    </td>
                                    <td style="width:30%">                                        
                                        <%-- <asp:CheckBox Text="selectApplicability" id="selectApplicability" runat="server" /> --%>
                                         <asp:HyperLink ID="AddDeleteHyperLink" runat="server" Target="_blank" NavigateUrl='<%#Sadara.DigiCom.ReportFinder.ViewModel.ReqFinderInfoMethods.Page_ApplicationRequirementDetailsPageURL+                             
                                            Sadara.DigiCom.ReportFinder.ViewModel.Constants.QueryString_APPLICATIONKEY+"="+ Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSApplication_FIELDS_ApplicationID)%>'>View Requirements</asp:HyperLink>
                                    </td>                                    
                                </tr>
                            </table>
                         </li>

                     </ItemTemplate>
                     <FooterTemplate>
                         </ul>
                     </FooterTemplate>
                 </asp:Repeater>
                 </div>         

         </div>
     </ItemTemplate>    
</asp:Repeater>

</div>

<%--
<div id="SecondDiv" runat="server">
<asp:TextBox ID="chkboxCount" runat="server"></asp:TextBox>
    <asp:Repeater
     ID="RequirementRepeater" runat="server">
     <HeaderTemplate>
        
     </HeaderTemplate>
     <ItemTemplate>
         <div>
             <div class='block_inline'>
             <asp:Literal ID="RequirementKey" runat="server"  Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY) %>'></asp:Literal>
             <asp:Literal ID="ApplicationCategoryLiteral" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTDESCRIPTION) %>'></asp:Literal>
                             
             </div>
         </div>
     </ItemTemplate>


 </asp:Repeater>
</div>
--%>>
<script type="text/javascript" src="//code.jquery.com/jquery-1.11.0.min.js"></script>
<script type ="text/javascript">
    function getdata(id) {

        var e = document.getElementById('d' + id);
        if (e) {
            if (e.style.display != 'block') {
                e.style.display = 'block';
                e.style.visibility = 'visible';

            }
            else {
                e.style.display = 'none';
                e.style.visibility = 'hidden';
            }
        }
    }
</script>


<script type="text/javascript">
    $(document).ready(function () {
        $(".block_inline2").hide();
    });
</script>