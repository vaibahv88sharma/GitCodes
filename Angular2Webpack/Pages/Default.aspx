<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

    <SharePoint:ScriptLink name="sp.js" runat="server" OnDemand="true" LoadAfterUI="true" Localizable="false" />
    <meta name="WebPartPageExpansion" content="full" />

    <%--webpack--%>
    <link href="../dist/app.css" rel="stylesheet" />

    <%--npm run build--%>
    <%--    <link href="../dist/app.23d184000ff182021e74.css" rel="stylesheet" />--%>


</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Page Title
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

    <my-app>
        Loading...
    </my-app>



    <script type="text/javascript">window.Zone = undefined;</script>

    <%--webpack--%>
    <script type="text/javascript" src="../dist/polyfills.js"></script>
    <script type="text/javascript" src="../dist/vendor.js"></script>
    <script type="text/javascript" src="../dist/app.js"></script>

    <%--npm run build--%>
    <%--    <script type="text/javascript" src="../dist/polyfills.23d184000ff182021e74.js"></script>
    <script type="text/javascript" src="../dist/vendor.23d184000ff182021e74.js"></script>
    <script type="text/javascript" src="../dist/app.23d184000ff182021e74.js"></script>--%>

</asp:Content>
