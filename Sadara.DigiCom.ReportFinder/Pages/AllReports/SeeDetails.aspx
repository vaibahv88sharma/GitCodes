 <%@ Page Inherits="Microsoft.SharePoint.Publishing.TemplateRedirectionPage,Microsoft.SharePoint.Publishing,Version=15.0.0.0,Culture=neutral,PublicKeyToken=71e9bce111e9429c" %>
 <%@ Reference VirtualPath="~TemplatePageUrl" %>
 <%@ Reference VirtualPath="~masterurl/custom.master" %>
 <%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<html>
    <head>
    <title></title>
       
    </head>
    <body>
         <%--<script type="text/javascript">
             $(document).ready(function () {
                 ChangeFieldName();
                 function getUrlVars() {
                     var vars = [], hash;
                     var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
                     for (var i = 0; i < hashes.length; i++) {
                         hash = hashes[i].split('=');
                         vars.push(hash[0]);
                         vars[hash[0]] = hash[1];
                     }
                     return vars;
                 }
                 var params = getUrlVars();
                 SetTitle();
                 function SetTitle() {
                     if (params.length > 0) {
                         var wpTitle = params["FilterValue1"];
                         if (wpTitle) {
                             $(".small-12.medium-9.column .ms-webpart-titleText nobr span:first-child").text(wpTitle);
                         }
                     }

                 }
                 function ChangeFieldName() {
                     $("a[id*='LinkFilenameNoMenu']").text("Title")
                     $("a[id*='Author']").text("Author");

                 }

             });
        </script>--%>
        </body>
    </html>
