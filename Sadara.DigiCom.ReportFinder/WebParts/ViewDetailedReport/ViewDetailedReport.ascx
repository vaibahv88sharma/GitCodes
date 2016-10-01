<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViewDetailedReport.ascx.cs" Inherits="Sadara.DigiCom.ReportFinder.WebParts.ViewDetailedReport.ViewDetailedReport" %>

<div>
    <div class="block_inline">
        <b>
        <asp:Literal ID="RequirementID" runat="server" Text="Requirement Details"></asp:Literal>
        </b>
    </div>
    <div>
        <asp:Repeater ID="RequirementRepeater" runat="server">        
            <HeaderTemplate>       </HeaderTemplate>
            <ItemTemplate>
                <div>
                    <div class="block_inline">                  
                        <asp:Literal ID="RequirementKey" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTKEY) %>'></asp:Literal>
                        <asp:Literal ID="RequirementDesc" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTDESCRIPTION)%>' ></asp:Literal>                        
                        <asp:Literal ID="RequirementNum" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTNUMBER) %>'></asp:Literal>
                        <asp:Literal ID="RequirementRef" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.ODMSREQUIREMENT_FIELDS_REQUIREMENTREFERENCE) %>'></asp:Literal>                                     
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>        </FooterTemplate>
        </asp:Repeater>
    </div>
</div>

<br><br />

<div>
    <div class="block_inline">
        <b>
        <asp:Literal ID="ValueID" runat="server" Text="Value Details"></asp:Literal>    
            </b>    
    </div>
    <div>
        <asp:Repeater ID="ValueRepeater" runat="server">        
            <HeaderTemplate>       </HeaderTemplate>
            <ItemTemplate>
                <div>
                    <div class="block_inline">                    
                            <asp:Literal ID="ValueID" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>'></asp:Literal>
                            <asp:Literal ID="ValueDetails" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name)%>' ></asp:Literal>                                                                
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>        </FooterTemplate>
        </asp:Repeater>
    </div>
</div>

<br><br />


<div>
    <div class="block_inline">
        <b>
        <asp:Literal ID="ApplicationID" runat="server" Text="Application Details"></asp:Literal>    
        </b>  
    </div>
    <div>
        <asp:Repeater ID="ApplicationRepeater" runat="server">        
            <HeaderTemplate>       </HeaderTemplate>
            <ItemTemplate>
                <div>
                    <div class="block_inline">                    
                            <asp:Literal ID="ApplicationID" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.ReqFinderInfoMethods.DataTable_FIELDS_ApplicationCategory) %>'></asp:Literal>
                            <asp:Literal ID="ApplicationDetails" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name)%>' ></asp:Literal>                                                          
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>        </FooterTemplate>
        </asp:Repeater>
    </div>
</div>

<br><br />
<div>
    <div class="block_inline">
        <b>
        <asp:Literal ID="CoOrdinationID" runat="server" Text="CoOrdination Details"></asp:Literal>   
        </b>
    </div>
    <div>
        <asp:Repeater ID="CoOrdinationRepeater" runat="server">        
            <HeaderTemplate>       </HeaderTemplate>
            <ItemTemplate>
                <div>
                    <div class="block_inline">                    
                            <asp:Literal ID="CoOrdinationID" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>'></asp:Literal>
                            <asp:Literal ID="CoOrdinationDetails" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name)%>' ></asp:Literal>                                                                
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>        </FooterTemplate>
        </asp:Repeater>
    </div>
</div>

<br><br />
<div>
    <div class="block_inline">
        <b>
        <asp:Literal ID="RDCodeID" runat="server" Text="RDCode Details"></asp:Literal>
        </b>
    </div>
    <div>
        <asp:Repeater ID="RDCodeRepeater" runat="server">        
            <HeaderTemplate>       </HeaderTemplate>
            <ItemTemplate>
                <div>
                    <div class="block_inline">
                  
                            <asp:Literal ID="RDCodeID" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>'></asp:Literal>
                            <asp:Literal ID="RDCodeDetails" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name)%>' ></asp:Literal>                        
                                        
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>        </FooterTemplate>
        </asp:Repeater>
    </div>
</div>

<br><br />
<div>
    <div class="block_inline">
        <b>
        <asp:Literal ID="DriverID" runat="server" Text="Driver Details"></asp:Literal>
        </b>
    </div>
    <div>
        <asp:Repeater ID="DriverRepeater" runat="server">        
            <HeaderTemplate>       </HeaderTemplate>
            <ItemTemplate>
                <div>
                    <div class="block_inline">
                  
                            <asp:Literal ID="DriverDetails" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name) %>'></asp:Literal>
                            <asp:Literal ID="DetailedDriverDescription" runat="server" Text='<%#Eval(Sadara.DigiCom.ReportFinder.ViewModel.ReqFinderInfoMethods.DataTable_FIELDS_DetailedDriverDescription)%>' ></asp:Literal>                        
                                     
                    </div>
                </div>
            </ItemTemplate>
            <FooterTemplate>        </FooterTemplate>
        </asp:Repeater>
    </div>
</div>

