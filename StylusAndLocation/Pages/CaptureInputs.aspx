<%@ Page Language="C#" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<WebPartPages:AllowFraming ID="AllowFraming" runat="server" />
<form runat="server">
    <SharePoint:FormDigest ID="FormDigest1" runat="server"></SharePoint:FormDigest>
</form>

<html>
<head>
    <title></title>

    <meta name="description" content="Signature Pad - HTML5 canvas based smooth signature drawing using variable width spline interpolation.">
<%--    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">--%>
  <meta name="viewport" content="width=device-width, initial-scale=1, minimum-scale=1, maximum-scale=1, user-scalable=no">


    <meta name="apple-mobile-web-app-capable" content="yes">
    <meta name="apple-mobile-web-app-status-bar-style" content="black">

    <script type="text/javascript" src="../Scripts/jquery-2.2.4.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>

    <script type="text/javascript" src="../Scripts/angular.min.js"></script>
    <script type="text/javascript" src="../Scripts/angular-route.min.js"></script>
    <script type="text/javascript" src="../Scripts/angular-resource.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>
    <script type="text/javascript" src="../Scripts/ng-q-timeout.js"></script>

    <script type="text/javascript" src="../Scripts/sketch.js"></script>

    <script type="text/javascript" src="../Scripts/jquery.signfield-en.min.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.signfield.min.js"></script>
    <script type="text/javascript" src="../Scripts/signature_pad.js"></script>
    <script type="text/javascript" src="../Scripts/appSite.js"></script>

    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/signature-pad.css" rel="stylesheet" />

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

    <%--    <div class="container-fluid" ng-controller="HomeController">--%>
    <div class="container-fluid">

        <div class="row" ng-app-frame minheight="80">

            <div ng-view></div>
        </div>
    </div>




</body>
</html>
