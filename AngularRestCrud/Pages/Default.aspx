<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">
	
    <script type="text/javascript" src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.min.js"></script>
<!--    <script type="text/javascript" src="../Scripts/jquery-2.1.3.min.js"></script>-->
    <script src="../Scripts/bootstrap.min.js"></script>
    <script src="../Scripts/angular.min.js"></script>
    <script src="../Scripts/ui-bootstrap-tpls.min.js"></script>
		
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>
    <meta name="WebPartPageExpansion" content="full" />

    <script src="../Scripts/Module/module.js" type="text/javascript"></script>
    <script src="../Scripts/Services/service.js" type="text/javascript"></script>
    <script src="../Scripts/Controllers/controller.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/App.js"></script>

    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />
    <link href="../Content/bootstrap-theme.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />


</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Page Title
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

<br></br>
<br></br>
<div data-ng-app="spsmodule">
	<div data-ng-controller="spscontroller">
		<div class="well">
			<div class="container">
                <form class="form-inline" role="form">
                    <div class="form-group">
                        <label for="myTaskName">Select Task List Name:</label>
                        <select id="myTaskName" name="myTaskName" class="form-control">
                          <option></option>
                          <option ng-repeat="job in jobs">{{job.jobAddress}}</option>
                        </select>
                    </div>
					<div class="form-group"><label> </label></div>
                    <div class="form-group">
                        <label for="mySupervisor">Select Supervisor:</label>
                        <select id="mySupervisor" name="mySupervisor" class="form-control">
                          <option></option>
                          <option ng-repeat="siteSupervisor in siteSupervisors">{{siteSupervisor.siteSupervisorName}}</option>
                        </select>
                    </div>
					<div class="form-group"><label> </label></div>
                    <div class="form-group">
                        <label for="myTasksCat">Select Supervisor:</label>
                        <select id="myTasksCat" name="myTasksCat" class="form-control">
                          <option></option>
                          <option ng-repeat="taskCategory in taskCategories">{{taskCategory.taskCategoryName}}</option>
                        </select>
                    </div>										
                    <button type="button" id="createListBtn" ng-click="edit(course, siteSupervisor, taskCategory)" class="btn btn-default">Create Task List</button>                
				</form>
            </div>
		</div>			
    </div>
</div>

</asp:Content>
