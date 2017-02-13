<%-- The following 4 lines are ASP.NET directives needed when using SharePoint components --%>

<%@ Page Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" MasterPageFile="~masterurl/default.master" Language="C#" %>

<%@ Register TagPrefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register TagPrefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<%-- The markup and script in the following Content element will be placed in the <head> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderAdditionalPageHead" runat="server">

    <meta http-equiv="refresh" content="15" />


    <script type="text/javascript" src="../Scripts/jquery-2.2.4.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>

    <script type="text/javascript" src="../Scripts/angular.min.js"></script>
    <script type="text/javascript" src="../Scripts/angular-resource.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>

    <link href="../Content/bootstrap.min.css" rel="stylesheet" />



    <script type="text/javascript" src="../Scripts/jquery-1.9.1.min.js"></script>
    <SharePoint:ScriptLink name="sp.js" runat="server" OnDemand="true" LoadAfterUI="true" Localizable="false" />
    <meta name="WebPartPageExpansion" content="full" />

    <!-- Add your CSS styles to the following file -->
    <link rel="Stylesheet" type="text/css" href="../Content/App.css" />

    <!-- Add your JavaScript to the following file -->
    <script type="text/javascript" src="../Scripts/App.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            $("#titleAreaBox").css("display", "none");
            $(".aw-more-block.aw-more-block-first").css("display", "none");
        });
        $(window).load(function () {
            $(".aw-more-block.aw-more-block-first").css("display", "none");
        });
    </script>


</asp:Content>

<%-- The markup in the following Content element will be placed in the TitleArea of the page --%>

<asp:Content ContentPlaceHolderID="PlaceHolderPageTitleInTitleArea" runat="server">
    Page Title
</asp:Content>

<%-- The markup and script in the following Content element will be placed in the <body> of the page --%>
<asp:Content ContentPlaceHolderID="PlaceHolderMain" runat="server">

<%--    <div>
        <p id="message">
            initializing...
        </p>
    </div>--%>

    <div class="container-fluid">
        <div class="jumbotron">
            <div class="row">
                <div class="col-xs-12 col-sm-12 col-lg-6 col-md-6">
                    <a href="http://www.accuweather.com/en/au/melbourne/26216/weather-forecast/26216" class="aw-widget-legal">
                        <!--
        By accessing and/or using this code snippet, you agree to AccuWeather’s terms and conditions (in English) which can be found at http://www.accuweather.com/en/free-weather-widgets/terms and AccuWeather’s Privacy Statement (in English) which can be found at http://www.accuweather.com/en/privacy.
        -->
                    </a>
                    <div id="awtd1480981026959" style="border: 3px solid rgb(255, 255, 255);" class="aw-widget-36hour" data-locationkey="" data-unit="c" data-language="en-us" data-useip="true" data-uid="awtd1480981026959" data-editlocation="true" data-lifestyle="driving"></div>
                    <script type="text/javascript" src="http://oap.accuweather.com/launch.js"></script>


                </div>
                <div class="col-xs-12 col-sm-12 col-lg-6 col-md-6">
                    <%--        <<iframe src="http://www.g-mwater.com.au/water-resources/catchments/storage-levels" style="    min-width: 600px !important;
    min-height: 600px !important;"></iframe>--%>
                    <%--<div style="border: 3px solid rgb(201, 0, 1); overflow: hidden; margin: 15px auto; max-width: 736px;">--%>
                    <div style="border: 3px solid rgb(255, 255, 255); overflow: hidden; margin: 15px auto; max-width: 736px;">
                        <iframe scrolling="no" src="http://www.g-mwater.com.au/water-resources/catchments/storage-levels" style="border: 0px none; margin-left: -185px; height: 2100px; margin-top: -540px; width: 926px;"></iframe>
                    </div>
            </div>
        </div>
    </div>
    </div>

</asp:Content>
