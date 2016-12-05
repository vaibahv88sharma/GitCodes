<%@ Page Language="C#" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<WebPartPages:AllowFraming ID="AllowFraming" runat="server" />

<html>
<head>
    <title></title>

    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">

    <script type="text/javascript" src="../Scripts/jquery-2.2.4.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>

    <script type="text/javascript" src="../Scripts/angular.min.js"></script>
    <script type="text/javascript" src="../Scripts/angular-resource.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>

    <script type="text/javascript" src="../Scripts/appSite.js"></script>

    <link href="../Content/bootstrap.min.css" rel="stylesheet" />

    <script type="text/javascript">
        // Set the style of the client web part page to be consistent with the host web.
        (function () {
            'use strict';

            var hostUrl = '';
            var link = document.createElement('link');
            link.setAttribute('rel', 'stylesheet');
            if (document.URL.indexOf('?') != -1) {
                var params = document.URL.split('?')[1].split('&');
                for (var i = 0; i < params.length; i++) {
                    var p = decodeURIComponent(params[i]);
                    if (/^SPHostUrl=/i.test(p)) {
                        hostUrl = p.split('=')[1];
                        link.setAttribute('href', hostUrl + '/_layouts/15/defaultcss.ashx');
                        break;
                    }
                }
            }
            if (hostUrl == '') {
                link.setAttribute('href', '/_layouts/15/1033/styles/themable/corev15.css');
            }
            document.head.appendChild(link);
        })();
    </script>
</head>
<body ng-app="displayLinks" style="padding: 20px;">

    <div class="container-fluid" ng-controller="HomeController">
        <div class="row" ng-app-frame minheight="80">
            <div class="row">
                <nw-category-selector ng-model="search.category"></nw-category-selector>
            </div>
            <div class="row">
                <%--<table class="table table-bordered" ng-app-frame minheight="80">
                    --%>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Departments</th>
                                <th>Links</th>
                                <th>
                                    <div class="form-group">
                                        <label class="control-label" for="searchinput">Search Forms</label>
                                        <div class="">
<%--                                            <input 
                                                id="searchinput" 
                                                name="searchinput" 
                                                type="search" 
                                                placeholder="Enter Form Name" 
                                                class="form-control input-md" 
                                                ng-model="search.input.$">--%>
                                            <input 
                                                id="searchinput" 
                                                name="searchinput" 
                                                type="search" 
                                                placeholder="Enter Form Name" 
                                                class="form-control input-md" 
                                                ng-model="search.input">
                                        </div>
                                    </div>
                                </th>
                            </tr>
                        </thead>

                        <tbody>
                            <%--
                            <tr ng-repeat="formData in formsData | categoryFilter : search.category | filter : search.input">--%>
                            <tr ng-repeat="formData in formsData | categoryFilter : search.category | filter : {hyperlinkTitle:search.input}">
                                <td>{{formData.DepartmentCategory.DepartmentCategory}}</td>
                                <td colspan="2">
                                    <a style="color: blue; display: block; height: 100%; width: 100%;"
                                       ng-href="{{formData.hyperlink}}"
                                       target="{{formData.hyperlinkTarget}}">
                                        {{ formData.hyperlinkTitle }}
                                    </a>
                                </td>
                            </tr>
                        </tbody>
                    </table>
            </div>
        </div>
    </div>

</body>
</html>
