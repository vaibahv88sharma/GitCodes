<%-- SPG:

This HTML file has been associated with a SharePoint Page Layout (.aspx file) carrying the same name.  While the files remain associated, you will not be allowed to edit the .aspx file, and any rename, move, or deletion operations will be reciprocated.

To build the page layout directly from this HTML file, simply fill in the contents of content placeholders.  Use the Snippet Generator at http://staffportal.myselfserve.com.au/sites/portal/_layouts/15/ComponentHome.aspx?Url=http%3A%2F%2Fstaffportal%2Emyselfserve%2Ecom%2Eau%2Fsites%2Fportal%2F%5Fcatalogs%2Fmasterpage%2FStaffPortal%2FStaffPortalWebpartPL%2Easpx to create and customize additional content placeholders and other useful SharePoint entities, then copy and paste them as HTML snippets into your HTML code.   All updates to this file within content placeholders will automatically sync to the associated page layout.

 --%>
<%@Page language="C#" Inherits="Microsoft.SharePoint.Publishing.PublishingLayoutPage, Microsoft.SharePoint.Publishing, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%>
<%@Register TagPrefix="PageFieldFieldValue" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%>
<%@Register TagPrefix="Publishing" Namespace="Microsoft.SharePoint.Publishing.WebControls" Assembly="Microsoft.SharePoint.Publishing, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%>
<%@Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c"%>
<asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitle">
            <SharePoint:ProjectProperty Property="Title" runat="server" />
            
            
            <PageFieldFieldValue:FieldValue FieldName="fa564e0f-0c70-4ab9-b863-0177e6ddd247" runat="server">
            </PageFieldFieldValue:FieldValue>
            
        </asp:Content><asp:Content runat="server" ContentPlaceHolderID="PlaceHolderAdditionalPageHead">
            
            
            
            <Publishing:EditModePanel runat="server" id="editmodestyles">
                <SharePoint:CssRegistration name="&lt;% $SPUrl:~sitecollection/Style Library/~language/Themable/Core Styles/editmode15.css %&gt;" After="&lt;% $SPUrl:~sitecollection/Style Library/~language/Themable/Core Styles/pagelayouts15.css %&gt;" runat="server">
                </SharePoint:CssRegistration>
            </Publishing:EditModePanel>
            
        </asp:Content><asp:Content runat="server" ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea">
            
            
            <PageFieldFieldValue:FieldValue FieldName="fa564e0f-0c70-4ab9-b863-0177e6ddd247" runat="server">
            </PageFieldFieldValue:FieldValue>
            
        </asp:Content><asp:Content runat="server" ContentPlaceHolderID="PlaceHolderMain">
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                <div data-name="WebPartZone">
                    
                    
                    <div>
                        <WebPartPages:WebPartZone runat="server" AllowPersonalization="false" ID="x83c94f17ab1a4b2492ac5f3080237906" FrameType="TitleBarOnly" Orientation="Vertical">
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </WebPartPages:WebPartZone>
                    </div>
                    
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                <div data-name="WebPartZone">
                    
                    
                    <div>
                        <WebPartPages:WebPartZone runat="server" AllowPersonalization="false" ID="x662ede9ff45449f4918ea33ba518eb02" FrameType="TitleBarOnly" Orientation="Vertical">
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </WebPartPages:WebPartZone>
                    </div>
                    
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                <div data-name="WebPartZone">
                    
                    
                    <div>
                        <WebPartPages:WebPartZone runat="server" AllowPersonalization="false" ID="x7c9cfac06ce746b28b7eea85a0ba39e4" FrameType="TitleBarOnly" Orientation="Vertical">
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </WebPartPages:WebPartZone>
                    </div>
                    
                </div>
            </div>
            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                <div data-name="WebPartZone">
                    
                    
                    <div>
                        <WebPartPages:WebPartZone runat="server" AllowPersonalization="false" ID="xc4093e442bf2442399fbc08682581f96" FrameType="TitleBarOnly" Orientation="Vertical">
                            <ZoneTemplate>
                                
                            </ZoneTemplate>
                        </WebPartPages:WebPartZone>
                    </div>
                    
                </div>
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            </div>
        </asp:Content>