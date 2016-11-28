<%@ Page Language="C#" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<WebPartPages:AllowFraming ID="AllowFraming" runat="server" height="100%" width="100%"/>

<html>
<head>
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">

    <%--    <script src="../Scripts/jquery-3.1.1.intellisense.js"></script>--%>
    <%--<script type="text/javascript" src="../Scripts/jquery-1.9.1.js"></script>--%>
    <script type="text/javascript" src="../Scripts/jquery-2.2.4.min.js"></script>
    <%--<script type="text/javascript" src="../Scripts/jquery-3.1.1.min.js"></script>--%>
    <script type="text/javascript" src="/_layouts/15/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>

    <script type="text/javascript" src="../Scripts/angular.min.js"></script>
    <%--	<script type="text/javascript"src="../Scripts/angular-route.min.js"></script>
	<script type="text/javascript"src="../Scripts/angular-cookies.min.js"></script>--%>
    <script type="text/javascript" src="../Scripts/angular-resource.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>

    <script type="text/javascript" src="../Scripts/appSite.js"></script>

    <%--    <link href="../Content/bootstrap-theme.min.css" rel="stylesheet" />--%>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />

    <script type="text/javascript">
        // Set the style of the client web part page to be consistent with the host web.
        //(function () {
        //'use strict';
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

        //Link App Properties

        function getQueryStringParameter(paramToRetrieve) {
            var params;
            var strParams;

            params = document.URL.split("?")[1].split("&");
            strParams = "";
            for (var i = 0; i < params.length; i = i + 1) {
                var singleParam = params[i].split("=");
                if (singleParam[0] == paramToRetrieve)
                    return singleParam[1];
            }
        }
        //var sProp = decodeURIComponent(getQueryStringParameter("AppTitle"));
        //document.write('Value of StringProperty1 : ' + sProp + '</br>');

        //var eProp = decodeURIComponent(getQueryStringParameter("ListName"));
        //document.write('Value of EnumProperty1 : ' + eProp + '</br>');

        //})();

    </script>

    <style>
        .dotted-gradient {
            /*background-image: linear-gradient(to right, #333 40%, rgba(255, 255, 255, 0) 20%);
            background-position: bottom;
            background-size: 3px 1px;
            background-repeat: repeat-x;*/
	        /*border-bottom :dashed; 
            border-width: 3px;
            border-color : #00ffff;*/
            text-align: center;
            font-size: 250%;
            vertical-align: middle;
            text-transform: uppercase;
        }
    </style>

</head>
<body ng-app="app">

    <div class="container-fluid" ng-controller="HomeController">
        <div class="row" ng-app-frame minheight="80">
            <div class="row dotted-gradient">
                <div class="col-md-12">
                    {{title}}
                </div>
            </div>
            <div class="row">
                <div class="list-group">
                    <a
                        class="list-group-item list-group-item-action"
                        style="color: blue;"
                        ng-href="{{hyperlink.hyperlink}}"
                        target="{{hyperlink.hyperlinkTarget}}"
                        ng-repeat="hyperlink in hyperlinks">{{ hyperlink.hyperlinkTitle }}
                    </a>
                </div>
            </div>
        </div>
    </div>

</body>
</html>
