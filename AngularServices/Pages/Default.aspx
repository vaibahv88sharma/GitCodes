<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
	<script type="text/javascript" src="../Scripts/jquery-2.1.3.min.js"></script>
<!--	<script src="//ajax.googleapis.com/ajax/libs/angularjs/1.5.8/angular.js"></script>-->
    <script src="../Scripts/angular.min.js"></script>
	<script src="../Scripts/angular-route.js"></script>
	<script src="../Scripts/angular-cookies.js"></script>
	<script src="../Scripts/angular-resource.js"></script>
	<script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/ui-bootstrap-tpls.min.js"></script>
		
    <script src="/_layouts/1033/init.js"></script>
    <script src="/_layouts/15/MicrosoftAjax.js"></script>
    <script src="/_layouts/15/sp.core.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>	
	<script type="text/javascript" src="/_layouts/15/SP.RequestExecutor.js"></script>	
    <meta name="WebPartPageExpansion" content="full" />


	<script type="text/javascript" src="../Scripts/MyScript/angFiles/App.js"></script>
	<script type="text/javascript" src="../Scripts/App.js"></script>

    <script type="text/javascript" src="../Scripts/MyScript/angFiles/books/BooksController.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/angFiles/books/AddBookController.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/angFiles/books/EditBookController.js"></script>
	
	<script type="text/javascript" src="../Scripts/MyScript/angFiles/services/dataService.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/angFiles/services/loggerService.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/angFiles/services/constantService.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/angFiles/services/badgeService.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/angFiles/services/bookLoggerInterceptor.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/angFiles/services/BooksResource.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/angFiles/services/currentUser.js"></script>

	
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <link href="../Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />

    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />

</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Page Title
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

<div ng-app="app">
    <div>
        <p id="message">
            <!-- The following content will be replaced with the user name when you run the app - see App.js -->
            initializing...
        </p>
    </div>

	<div>
		<div  class="container-fluid">
<!--		<div class="row" ng-controller="BooksController as books">				
				<h1>Hello {{books.appName}}!</h1>				
			</div>-->
			<div class="row">		
				<h1>Book Logger</h1>
				<div ng-view></div>
			</div>			
		</div>
	</div>
</div>

</asp:Content>
