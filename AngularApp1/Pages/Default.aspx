<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
	
<!--    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.5.7/angular.js"></script> 	-->
    <script src="../Scripts/angular.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/angularjs/1.3.8/angular-route.min.js"></script>		
    <script src="../Scripts/ui-bootstrap-tpls.min.js"></script>
		
		
    <script src="/_layouts/1033/init.js"></script>
    <script src="/_layouts/15/MicrosoftAjax.js"></script>
    <script src="/_layouts/15/sp.core.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>	
	<script type="text/javascript" src="/_layouts/15/SP.RequestExecutor.js"></script>	
    <meta name="WebPartPageExpansion" content="full" />

    <script type="text/javascript" src="../Scripts/App.js"></script>
	<script type="text/javascript" src="../Scripts/angularApp.js"></script>
    <script type="text/javascript" src="../Scripts/appService.js"></script>

<!--    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />-->
    <link href="../Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />

</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Page Title
	
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <div>
        <p id="message">
            <!-- The following content will be replaced with the user name when you run the app - see App.js -->
            initializing...
        </p>
    </div>


    <div data-ng-app="ContactsApp">
		<div class="container">
	        <div class="header">
	            <ul class="nav nav-pills pull-right" data-ng-controller="NavbarController">
	                <li data-ng-class="{'active':getClass('/all-contacts')}"><a href="#/all-contacts">All Contacts</a></li>
	                <li data-ng-class="{'active':getClass('/add-contacts')}"><a href="#/add-contacts">Add Contacts</a></li>
	            </ul>
	
	            <h3>Address Book</h3>
	        </div>
	
	        <hr>
	        <div ng-view></div>
	
	        <hr>
	        <div class="footer">
	            <p>&copy; Address Book 2016</p>
	        </div>

    	</div>
    </div>
	
	<script type="text/javascript">
		

	</script>

</asp:Content>
