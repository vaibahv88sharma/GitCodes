<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

	<script type="text/javascript" src="../Scripts/jquery-2.1.3.min.js"></script>
	<script src="//ajax.googleapis.com/ajax/libs/angularjs/1.5.8/angular.js"></script>
    <!--<script src="../Scripts/angular.min.js"></script>-->
	<script src="../Scripts/angular-route.js"></script>
	<script src="../Scripts/angular-resource.js"></script>
	<script src="../Scripts/bootstrap.min.js"></script>
<!--    <script src="../Scripts/ui-bootstrap-tpls.min.js"></script>	-->
<!--	<script type="text/javascript" src="../Scripts/MyScript/ngGravatar.js"></script>
	<script type="text/javascript" src="//cdnjs.cloudflare.com/ajax/libs/crypto-js/3.1.2/rollups/md5.js"></script>-->		
		
    <script src="/_layouts/1033/init.js"></script>
    <script src="/_layouts/15/MicrosoftAjax.js"></script>
    <script src="/_layouts/15/sp.core.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>	
	<script type="text/javascript" src="/_layouts/15/SP.RequestExecutor.js"></script>	
    <meta name="WebPartPageExpansion" content="full" />

    <!-- Add your App JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/App.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/module.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/routes.js"></script>
    <!-- Add your Directives JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/MyScript/directives/nwPageNav.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/directives/nwCategorySelector.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/directives/title.js"></script>
    <!-- Add your Controllers JavaScript to the following file -->
	<script type="text/javascript" src="../Scripts/MyScript/controllers/noteIndexController.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/controllers/noteShowController.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/controllers/noteEditController.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/controllers/noteCreateController.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/controllers/usersShowController.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/controllers/usersIndexController.js"></script>	
    <!-- Add your Filter JavaScript to the following file -->
	<script type="text/javascript" src="../Scripts/MyScript/filters/categoryFilter.js"></script>	
    <!-- Add your Services JavaScript to the following file -->
<!--	<script type="text/javascript" src="../Scripts/MyScript/services/noteService.js"></script>-->
	<script type="text/javascript" src="../Scripts/MyScript/services/note.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/services/user.js"></script>
	<script type="text/javascript" src="../Scripts/MyScript/services/category.js"></script>
	
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <link href="../Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />

    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />

</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
	Note Wrangler
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

<div ng-app="NoteWrangler">
    <div>
        <p id="message">
            <!-- The following content will be replaced with the user name when you run the app - see App.js -->
            initializing...
        </p>
    </div>
	
	<nav class="navbar navbar-default">
		<div class="container-fluid">
			<div class="navbar-header">
				<div class="navbar-brand">
					<a href="#/">NoteWrangler</a>
					<!--<a href="/#/">NoteWrangler</a>-->
				</div>
			</div>
			<nw-page-nav></nw-page-nav>
		</div>
	</nav>
	
	<!--<div ng-app="NoteWrangler">-->
	<div>
		<div ng-view class="container-fluid"></div>
	</div>
</div>
	
</asp:Content>
