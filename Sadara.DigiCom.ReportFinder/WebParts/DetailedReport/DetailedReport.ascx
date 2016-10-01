<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %> 
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %> 
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailedReport.ascx.cs" Inherits="Sadara.DigiCom.ReportFinder.WebParts.DetailedReport.DetailedReport" %>

<div>
    <asp:GridView ID="DisplayValueList"
        runat="server"
        Width="550px"
        AutoGenerateColumns="false"
        AlternatingRowStyle-BackColor="#C2D69B"
        HeaderStyle-BackColor="green">
        <Columns>
            <asp:TemplateField HeaderText="Value ID">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>
                </ItemTemplate>
            </asp:TemplateField>           
            <asp:TemplateField HeaderText="Value Details">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name) %>
                </ItemTemplate>
            </asp:TemplateField>  
        </Columns>
        <AlternatingRowStyle BackColor="#C2D69B" />       
    </asp:GridView>
</div>

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
    <asp:GridView ID="DisplayApplicationList"
        runat="server"
        Width="550px"
        AutoGenerateColumns="false"
        AlternatingRowStyle-BackColor="#C2D69B"
        HeaderStyle-BackColor="green">
        <Columns>
            <asp:TemplateField HeaderText="Application Category">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.ReqFinderInfoMethods.DataTable_FIELDS_ApplicationCategory) %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Application Details">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name) %>
                </ItemTemplate>
            </asp:TemplateField>                           
        </Columns>
        <AlternatingRowStyle BackColor="#C2D69B" />       
    </asp:GridView>
</div>


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

<div>
    <asp:GridView ID="DisplayCoOrdinationList"
        runat="server"
        Width="550px"
        AutoGenerateColumns="false"
        AlternatingRowStyle-BackColor="#C2D69B"
        HeaderStyle-BackColor="green">
        <Columns>
            <asp:TemplateField HeaderText="CoOrdination ID">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>
                </ItemTemplate>
            </asp:TemplateField>     
            <asp:TemplateField HeaderText="CoOrdination Details">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name) %>
                </ItemTemplate>
            </asp:TemplateField>            
        </Columns>
        <AlternatingRowStyle BackColor="#C2D69B" />       
    </asp:GridView>
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

<div>
    <asp:GridView ID="DisplayRDCodeList"
        runat="server"
        Width="550px"
        AutoGenerateColumns="false"
        AlternatingRowStyle-BackColor="#C2D69B"
        HeaderStyle-BackColor="green">
        <Columns>
            <asp:TemplateField HeaderText="ReqDiffCode ID">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_ID) %>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="DisplayRDCodeList Details">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name) %>
                </ItemTemplate>
            </asp:TemplateField>            
        </Columns>
        <AlternatingRowStyle BackColor="#C2D69B" />       
    </asp:GridView>
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

<div>
    <asp:GridView ID="DisplayDriverList"
        runat="server"
        Width="550px"
        AutoGenerateColumns="false"
        AlternatingRowStyle-BackColor="#C2D69B"
        HeaderStyle-BackColor="green">
        <Columns>
            <asp:TemplateField HeaderText="Driver Description">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.Constants.DataTable_FIELDS_Name) %>
                </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="Driver Detailed Description">
                <ItemTemplate>
                    <%# Eval(Sadara.DigiCom.ReportFinder.ViewModel.ReqFinderInfoMethods.DataTable_FIELDS_DetailedDriverDescription) %>
                </ItemTemplate>
            </asp:TemplateField>                                                           
        </Columns>
        <AlternatingRowStyle BackColor="#C2D69B" />       
    </asp:GridView>
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

<asp:Button ID="ExportToExcel" runat="server" Text="Export To Excel" OnClick="ExportToExcel_Click" />