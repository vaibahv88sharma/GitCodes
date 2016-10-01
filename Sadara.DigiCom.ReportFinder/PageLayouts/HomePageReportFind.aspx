<%@ Page Language="C#" Inherits="Microsoft.SharePoint.Publishing.PublishingLayoutPage,Microsoft.SharePoint.Publishing,Version=15.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" meta:webpartpageexpansion="full" meta:progid="SharePoint.WebPartPage.Document" %>

<%@ Register TagPrefix="SharePointWebControls" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="PublishingWebControls" Namespace="Microsoft.SharePoint.Publishing.WebControls" Assembly="Microsoft.SharePoint.Publishing, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="PublishingNavigation" Namespace="Microsoft.SharePoint.Publishing.Navigation" Assembly="Microsoft.SharePoint.Publishing, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>


<asp:Content contentplaceholderid="PlaceHolderPageTitle" runat="server">

	<SharePointWebControls:FieldValue id="PageTitle" FieldName="Title" runat="server"/>
</asp:Content>


<asp:Content runat="server" contentplaceholderid="PlaceHolderMain"> 
	<WebPartPages:SPProxyWebPartManager runat="server" id="spproxywebpartmanager"></WebPartPages:SPProxyWebPartManager>
	
	<main id="main-container" class="main-container">
    <div id="page-holder" class="wrapper-content">
        <div class="float_layout">
            <div class="float_layout__item">
                <div id="page-content" class="layout">
                    <div class="float_layout__item 1/4">
                        <div class="side-navigation">
                               <WebPartPages:webpartzone id="g_EEADD354D2364628B4EB51603D415DE5" runat="server" title="Left Content Zone"><ZoneTemplate></ZoneTemplate></WebPartPages:webpartzone>
                   		</div>   
                    </div>
                    <div class="float_layout__item 3/4">                          
                               <div class="content-article-components">
                                <div class="row-component">
                               	<WebPartPages:webpartzone id="g_EEADD354D2364628B4EB51603D415DE6" runat="server" title="Main Content Zone"><ZoneTemplate></ZoneTemplate></WebPartPages:webpartzone>
                         </div>
                            </div>
                                                                            </div>
        </div>
    </div>
</div></div></main>


       
</asp:Content>
