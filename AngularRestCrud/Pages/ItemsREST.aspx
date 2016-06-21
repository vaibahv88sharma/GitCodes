<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
	
    <script type="text/javascript" src="//ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js"></script>
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/angular.min.js"></script>
    <script src="//ajax.googleapis.com/ajax/libs/angularjs/1.3.8/angular-route.min.js"></script>		
    <script src="../Scripts/ui-bootstrap-tpls.min.js"></script>
		
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>	
	<script type="text/javascript" src="/_layouts/15/SP.RequestExecutor.js"></script>	
	<script type="text/javascript" src="/_layouts/15/SP.workflowservices.js"></script>
    <meta name="WebPartPageExpansion" content="full" />

    <script src="../Scripts/REST/ModuleREST/myModuleREST.js" type="text/javascript"></script>
	<script src="../Scripts/REST/ServiceREST/myServiceRestNew.js" type="text/javascript"></script>
    <script src="../Scripts/REST/ControllerREST/myControllerREST.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/App.js"></script>

    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <link href="../Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />


</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Scheduling Application
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

<br></br>
    <div>
        <p id="message">
            initializing...
        </p>
    </div>
<br></br>
<div data-ng-app="moduleRest">
	<div>
		<div class="well">
			<div class="container">
<!--				<div class="jumbotron">-->
					<div data-ng-view></div>
				<!--</div>-->
            </div>
		</div>			
    </div>
</div>		
	</div>
</div>

</asp:Content>
