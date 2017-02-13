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

    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">

    <script type="text/javascript" src="../Scripts/jquery-2.2.4.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>

    <script type="text/javascript" src="../Scripts/angular.min.js"></script>
    <script type="text/javascript" src="../Scripts/angular-resource.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>

    <script type="text/javascript" src="../Scripts/App.js"></script>
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


        $('#ContactForm').submit(function () {
            //submitForm(contact);
            //alert("reached");
            return false;
        });

    </script>
</head>
<body ng-app="bkiMobileInfo" style="padding: 20px;">

    <div class="container-fluid" ng-controller="bkiMobileInfoCtrl" ng-app-frame minheight="80">
        <%--        <div class="">--%>
        <div class="row">
            <div class="jumbotron">
                <p id="message1">
                    Hello {{item.userName}},
                </p>
            </div>
        </div>
        <div class="row">
            <div class="well">

                <form class="form-horizontal" name="ContactForm" id="ContactForm" method="post" role="form" novalidate ng-submit="submitForm(item)">
                    <fieldset>

                        <!-- Form Name -->

                        <!-- Multiple Radios -->
                        <%--                            <div class="form-group">
                                <label class="col-md-4 control-label" for="HaveBKIMobilephone">Do you have a BKI provided mobile phone</label>
                                <div class="col-md-4">
                                    <div class="radio">
                                        <label for="HaveBKIMobilephone-0">
                                            <input type="radio" name="HaveBKIMobilephone" id="HaveBKIMobilephone-0" value="1" checked="checked">
                                            Yes
                                        </label>
                                    </div>
                                    <div class="radio">
                                        <label for="HaveBKIMobilephone-1">
                                            <input type="radio" name="HaveBKIMobilephone" id="HaveBKIMobilephone-1" value="2">
                                            No
                                        </label>
                                    </div>
                                </div>
                            </div>--%>
                        <!-- Select Basic -->
                        <%--                        <div class="form-group">
                            <label class="col-md-4 control-label" for="HaveBKIMobilephone">Do you have a BKI provided mobile phone</label>
                            <div class="col-md-4">
                                <select id="HaveBKIMobilephone" name="HaveBKIMobilephone" class="form-control">
                                    <option style="display: none" value="">Do you have Mobile</option>
                                    <option value="Yes">Yes</option>
                                    <option value="No">No</option>
                                </select>
                            </div>
                        </div>--%>

                        <%--                        <div class="form-group">
                            <label class="col-md-4 control-label" for="mobileNumberGrp">Do you have a BKI provided mobile phone</label>
                            <div class="col-md-4">
                                <div class="checkbox">
                                    <label for="mobileNumberGrp-0">
                                        <input type="checkbox" name="mobileNumberGrp" id="mobileNumberGrp-0" value="Yes" ng-click="toggleMobileDiv()" data-ng-model="item.hasMobileNumber">
                                        Click to select mobile phone
                                    </label>
                                </div>
                            </div>
                        </div>--%>


                        <div class="form-group" id="ques1">
                            <label class="col-md-4 control-label" style="font-size: 1.5em; font-weight: bold; font-style: italic;" for="mobileNumberGrp">Do you have a BKI provided mobile phone</label>
                            <div class="col-md-4">
                                <select id="mobileNumberGrp" name="mobileNumberGrp" class="form-control" data-ng-model="item.hasMobileNumber" ng-change="toggleMobileDiv2()">
                                    <option style="display: none" value="">Click to select mobile phone</option>
                                    <option value="Yes">Yes</option>
                                    <option value="No">No</option>
                                </select>
                            </div>
                        </div>



                        <div id="child1" ng-if="showMobileDiv">
                            <!-- Text input-->
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="color: blue;" for="mobileNumber">Mobile Number</label>
                                <div class="col-md-4">
                                    <input id="mobileNumber" name="mobileNumber" type="text" placeholder="Enter Mobile Number" class="form-control input-md" data-ng-model="item.mobileNumber">
                                    <%--                                <span class="help-block">help</span>--%>
                                </div>
                            </div>

                            <!-- Select Basic -->
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="color: blue;" for="mobileBrandName">Brand</label>
                                <div class="col-md-4">
                                    <select id="mobileBrandName" name="mobileBrandName" class="form-control" data-ng-model="item.mobileBrandName">
                                        <option style="display: none" value="">Select Mobile</option>
                                        <option value="Apple">Apple</option>
                                        <option value="Lenovo">Lenovo</option>
                                        <option value="Microsoft">Microsoft</option>
                                        <option value="Samsung">Samsung</option>
                                        <option value="Sony">Sony</option>
                                        <option value="Other">Other</option>
                                    </select>
                                </div>
                            </div>

                            <!-- Text input-->
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="color: blue;" for="mobileModelName">Model</label>
                                <div class="col-md-4">
                                    <input id="mobileModelName" name="mobileModelName" type="text" placeholder="Enter Model" class="form-control input-md" data-ng-model="item.mobileModelName">
                                    <%--                                <span class="help-block">help</span>--%>
                                </div>
                            </div>

                            <!-- Text input-->
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="color: blue;" for="serialNumberMobile">Serial Number</label>
                                <div class="col-md-4">
                                    <input id="serialNumberMobile" name="serialNumberMobile" type="text" placeholder="Enter Serial Number" class="form-control input-md" data-ng-model="item.serialNumberMobile">
                                    <%--                                <span class="help-block">help</span>--%>
                                </div>
                            </div>

                            <!-- Select Basic -->
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="color: blue;" for="providerList1">Provider</label>
                                <div class="col-md-4">
                                    <select id="providerList1" name="providerList1" class="form-control" data-ng-model="item.providerList1">
                                        <option style="display: none" value="">Select Provider</option>
                                        <option value="Telstra">Telstra</option>
                                        <option value="Optus">Optus</option>
                                        <%--<option value="Telstra">Telstra</option>
<option value="Vodafone">Vodafone</option>
<option value="Optus">Optus</option>
<option value="Other">Other</option>
<option value="ALDImobile">ALDImobile</option>
<option value="Amaysim">Amaysim</option>
<option value="Bendigo Bank Telco">Bendigo Bank Telco</option>
<option value="Boost Mobile">Boost Mobile</option>
<option value="Commander 6">Commander 6</option>
<option value="ClubTelco 6">ClubTelco 6</option>
<option value="CMobile">CMobile</option>
<option value="Dodo 6">Dodo 6</option>
<option value="Exetel">Exetel</option>
<option value="GoTalk">GoTalk</option>
<option value="Hello Mobile">Hello Mobile</option>
<option value="iiNet 7">iiNet 7</option>
<option value="Internode 7">Internode 7</option>
<option value="iPrimus 6">iPrimus 6</option>
<option value="Jeenee Mobile">Jeenee Mobile</option>
<option value="Just Mobile">Just Mobile</option>
<option value="Kogan Mobile">Kogan Mobile</option>
<option value="Live Connected 8">Live Connected 8</option>
<option value="Lebara Mobile">Lebara Mobile</option>
<option value="Lycamobile">Lycamobile</option>
<option value="OVO">OVO</option>
<option value="Southern Phone">Southern Phone</option>
<option value="Startel">Startel</option>
<option value="Reward Mobile">Reward Mobile</option>
<option value="TeleChoice">TeleChoice</option>
<option value="Think Mobile">Think Mobile</option>
<option value="TPG">TPG</option>
<option value="Vaya Mobile">Vaya Mobile</option>
<option value="Virgin Mobile 5">Virgin Mobile 5</option>
<option value="Woolworths Connect 9">Woolworths Connect 9</option>
<option value="Yomojo 10">Yomojo 10</option>--%>
                                    </select>
                                </div>
                            </div>

                            <!-- Multiple Radios -->
                            <%--                            <div class="form-group">
                                <label class="col-md-4 control-label" for="dataOnMobile">Do you have data on your Mobile</label>
                                <div class="col-md-4">
                                    <div class="radio">
                                        <label for="dataOnMobile-0">
                                            <input type="radio" name="dataOnMobile" id="dataOnMobile-0" value="1" checked="checked">
                                            Yes
                                        </label>
                                    </div>
                                    <div class="radio">
                                        <label for="dataOnMobile-1">
                                            <input type="radio" name="dataOnMobile" id="dataOnMobile-1" value="2">
                                            No
                                        </label>
                                    </div>
                                </div>
                            </div>--%>

                            <!-- Select Basic -->
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="color: blue;" for="dataOnMobile">Do you have data on your Mobile</label>
                                <div class="col-md-4">
                                    <select id="dataOnMobile" name="dataOnMobile" class="form-control" data-ng-model="item.dataOnMobile">
                                        <option style="display: none" value="">Select Data Option</option>
                                        <option value="Yes">Yes</option>
                                        <option value="No">No</option>
                                    </select>
                                </div>
                            </div>


                            <%--      <div class="form-group col-md-4 col-lg-offset-4">
        <label class="control-label" for="location">Location</label>
        <br/><input class="form-control" id="location" name="location" type="text" ng-model="location" placeholder="Location"/>
      </div>

<br />--%>
                            <div class="row">
                            <div class="col-lg-4 col-lg-offset-4 col-md-4 col-md-offset-4 col-sm-4 col-sm-offset-4 well">
                                <span style="font-weight: bold; font-style: italic;">Justification for Mobile</span>
                            </div>
                                </div>

                                <%--                            <div id="justificationGroup1">--%>
                                <div id="justificationGroup1" class="col-lg-offset-5 col-md-offset-5">

                                    <!-- Select Basic -->
                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="typrOfUse">Type of Use</label>
                                            <select id="typrOfUse" name="typrOfUse" class="form-control" data-ng-model="item.typrOfUse">
                                                <option style="display: none" value="">Select Type of Use</option>
                                                <option value="Light">Light</option>
                                                <option value="Medium">Medium</option>
                                                <option value="Heavy">Heavy</option>
                                            </select>
                                        </div>
                                    </div>

                                    <!-- Select Basic -->
                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="timeWorkedOfCampus">How much Time Do You Worked Of Campus</label>
                                            <select id="timeWorkedOfCampus" name="timeWorkedOfCampus" class="form-control" data-ng-model="item.timeWorkedOfCampus">
                                                <option style="display: none" value="">Select Worked Time</option>
                                                <option value="0">0  %</option>
                                                <option value="10">10%</option>
                                                <option value="20">20%</option>
                                                <option value="30">30%</option>
                                                <option value="40">40%</option>
                                                <option value="50">50%</option>
                                                <option value="60">60%</option>
                                                <option value="70">70%</option>
                                                <option value="80">80%</option>
                                                <option value="90">90%</option>
                                                <option value="100">100%</option>
                                            </select>
                                        </div>
                                    </div>
                                    <!-- Select Basic -->
                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="dataUse">Data Use</label>
                                            <select id="dataUse" name="dataUse" class="form-control" data-ng-model="item.dataUse">
                                                <option style="display: none" value="">Select Data Use</option>
                                                <option value="Light">Light</option>
                                                <option value="Medium">Medium</option>
                                                <option value="Heavy">Heavy</option>
                                            </select>
                                        </div>
                                    </div>

                                    <!-- Select Basic -->
                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="emailRequired">Email Required</label>
                                            <select id="emailRequired" name="emailRequired" class="form-control" data-ng-model="item.emailRequired">
                                                <option style="display: none" value="">Select Email Required</option>
                                                <option value="Yes">Yes</option>
                                                <option value="No">No</option>
                                            </select>
                                        </div>
                                    </div>
                                    <!-- Text input-->
                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="mobilePhoneJustification">Additional Comments</label>
                                            <input id="mobilePhoneJustification" name="mobilePhoneJustification" type="text" placeholder="Enter Justification" class="form-control input-md" data-ng-model="item.mobilePhoneJustification">
                                        </div>
                                    </div>
                                </div>

                        </div>
                        <!-- Multiple Radios -->
                        <%--                            <div class="form-group">
                                <label class="col-md-4 control-label" for="radios">Do you have a BKI provided Tablet</label>
                                <div class="col-md-4">
                                    <div class="radio">
                                        <label for="radios-0">
                                            <input type="radio" name="radios" id="radios-0" value="1" checked="checked">
                                            Yes
                                        </label>
                                    </div>
                                    <div class="radio">
                                        <label for="radios-1">
                                            <input type="radio" name="radios" id="radios-1" value="2">
                                            No
                                        </label>
                                    </div>
                                </div>
                            </div>--%>


                        <!-- Multiple Checkboxes (inline) -->
                        <%--                        <div class="form-group">
                            <label class="col-md-4 control-label" for="hasTabletorNot">Do you have a BKI provided Tablet</label>
                            <div class="col-md-4">
                                <label class="checkbox-inline" for="hasTabletorNot-0">
                                    <input type="checkbox" name="hasTabletorNot" id="hasTabletorNot-0" value="1" ng-click="toggleTabletDiv()" data-ng-model="item.hasTabletorNot">
                                    Click to select Tablet
                                </label>
                            </div>
                        </div>--%>

                        <div class="form-group" id="ques2">
                            <label class="col-md-4 control-label" style="font-size: 1.5em; font-weight: bold; font-style: italic;" for="hasTabletorNot">Do you have a BKI provided Tablet</label>
                            <div class="col-md-4">
                                <select id="hasTabletorNot" name="hasTabletorNot" class="form-control" data-ng-model="item.hasTabletorNot" ng-change="toggleTabletDiv()">
                                    <option style="display: none" value="">Click to select Tablet</option>
                                    <option value="Yes">Yes</option>
                                    <option value="No">No</option>
                                </select>
                            </div>
                        </div>


                        <div id="child2" ng-if="showTableteDiv">
                            <!-- Select Basic -->
                            <%--                            <div class="form-group">
                                <label class="col-md-4 control-label" for="hasTabletorNot">Do you have a BKI provided Tablet</label>
                                <div class="col-md-4">
                                    <select id="hasTabletorNot" name="hasTabletorNot" class="form-control" data-ng-model="item.hasTabletorNot">
                                        <option style="display: none" value="">Select Data Option</option>
                                        <option value="1">Yes</option>
                                        <option value="2">No</option>
                                    </select>
                                </div>
                            </div>--%>

                            <!-- Select Basic -->
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="color: blue;" for="brandTablet">Brand</label>
                                <div class="col-md-4">
                                    <select id="brandTablet" name="brandTablet" class="form-control" data-ng-model="item.brandTablet">
                                        <option style="display: none" value="">Select Brand</option>
                                        <option value="Apple">Apple</option>
                                        <option value="Lenovo">Lenovo</option>
                                        <option value="Microsoft">Microsoft</option>
                                        <option value="Samsung">Samsung</option>
                                        <option value="Sony">Sony</option>
                                        <option value="Other">Other</option>
                                    </select>
                                </div>
                            </div>

                            <!-- Select Basic -->
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="color: blue;" for="tabletModel">Model</label>
                                <div class="col-md-4">
                                    <select id="tabletModel" name="tabletModel" class="form-control" data-ng-model="item.tabletModel">
                                        <option style="display: none" value="">Select Tablet Model</option>
                                        <option value="Galaxy">Galaxy</option>
                                        <option value="Ipad">Ipad</option>
                                        <option value="Surface">Surface</option>
                                        <option value="Xperia">Xperia</option>
                                        <option value="Other">Other</option>
                                    </select>
                                </div>
                            </div>

                            <!-- Text input-->
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="color: blue;" for="ipadSerial">Serial Number</label>
                                <div class="col-md-4">
                                    <input id="ipadSerial" name="ipadSerial" type="text" placeholder="Enter Serial Number" class="form-control input-md" data-ng-model="item.ipadSerial">
                                    <%--                                <span class="help-block">help</span>--%>
                                </div>
                            </div>

                            <!-- Multiple Radios -->
                            <%--                            <div class="form-group">
                                <label class="col-md-4 control-label" for="ipadData">Do you have data on your iPad</label>
                                <div class="col-md-4">
                                    <div class="radio">
                                        <label for="ipadData-0">
                                            <input type="radio" name="ipadData" id="ipadData-0" value="1" checked="checked">
                                            Yes
                                        </label>
                                    </div>
                                    <div class="radio">
                                        <label for="ipadData-1">
                                            <input type="radio" name="ipadData" id="ipadData-1" value="2">
                                            No
                                        </label>
                                    </div>
                                </div>
                            </div>--%>

                            <!-- Select Basic -->
                            <%--                        <div class="form-group">
                            <label class="col-md-4 control-label" for="ipadData">Do you have data on your iPad</label>
                            <div class="col-md-4">
                                <select id="ipadData" name="ipadData" class="form-control" data-ng-model="item.ipadData">
                                    <option style="display: none" value="">Select Provider</option>
                                    <option value="1">Yes</option>
                                    <option value="2">No</option>
                                </select>
                            </div>
                        </div>--%>

                            <!-- Multiple Checkboxes (inline) -->
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="color: blue;" for="ipadData">Do you have data on your iPad</label>
                                <div class="col-md-4">
                                    <label class="checkbox-inline" style="color: blue;" for="ipadData-0">
                                        <input type="checkbox" name="ipadData" id="ipadData-0" value="1" ng-click="toggleShowDiv()">
                                        Click to select Provider
                                    </label>
                                </div>
                            </div>


                            <!-- Select Basic -->
                            <div class="form-group" ng-if="showDiv">
                                <label class="col-md-4 control-label" style="color: blue;" for="providerList4">Provider</label>
                                <div class="col-md-4">
                                    <select id="providerList4" name="providerList4" class="form-control" data-ng-model="item.providerList4">
                                        <option style="display: none" value="">Select Provider</option>
                                        <option value="Telstra">Telstra</option>
                                        <option value="Optus">Optus</option>
                                        <%--<option value="Telstra">Telstra</option>
<option value="Vodafone">Vodafone</option>
<option value="Optus">Optus</option>
<option value="Other">Other</option>
<option value="ALDImobile">ALDImobile</option>
<option value="Amaysim">Amaysim</option>
<option value="Bendigo Bank Telco">Bendigo Bank Telco</option>
<option value="Boost Mobile">Boost Mobile</option>
<option value="Commander 6">Commander 6</option>
<option value="ClubTelco 6">ClubTelco 6</option>
<option value="CMobile">CMobile</option>
<option value="Dodo 6">Dodo 6</option>
<option value="Exetel">Exetel</option>
<option value="GoTalk">GoTalk</option>
<option value="Hello Mobile">Hello Mobile</option>
<option value="iiNet 7">iiNet 7</option>
<option value="Internode 7">Internode 7</option>
<option value="iPrimus 6">iPrimus 6</option>
<option value="Jeenee Mobile">Jeenee Mobile</option>
<option value="Just Mobile">Just Mobile</option>
<option value="Kogan Mobile">Kogan Mobile</option>
<option value="Live Connected 8">Live Connected 8</option>
<option value="Lebara Mobile">Lebara Mobile</option>
<option value="Lycamobile">Lycamobile</option>
<option value="OVO">OVO</option>
<option value="Southern Phone">Southern Phone</option>
<option value="Startel">Startel</option>
<option value="Reward Mobile">Reward Mobile</option>
<option value="TeleChoice">TeleChoice</option>
<option value="Think Mobile">Think Mobile</option>
<option value="TPG">TPG</option>
<option value="Vaya Mobile">Vaya Mobile</option>
<option value="Virgin Mobile 5">Virgin Mobile 5</option>
<option value="Woolworths Connect 9">Woolworths Connect 9</option>
<option value="Yomojo 10">Yomojo 10</option>--%>
                                    </select>
                                </div>
                            </div>

                            <!-- Text input-->
<%--                            <div class="form-group">
                                <label class="col-md-4 control-label" style="color: blue;" for="ipadJustification">iPad Justification</label>
                                <div class="col-md-4">
                                    <input id="ipadJustification" name="ipadJustification" type="text" placeholder="Enter Justification" class="form-control input-md" data-ng-model="item.ipadJustification">
                                </div>
                            </div>--%>

                            <div class="row">
                            <div class="col-lg-4 col-lg-offset-4 col-md-4 col-md-offset-4 col-sm-4 col-sm-offset-4 well">
                                <span style="font-weight: bold; font-style: italic;">Justification for Tablet</span>
                            </div>
                                </div>

                                <%--                            <div id="justificationGroup1">--%>
                                <div id="justificationGroup2" class="col-lg-offset-5 col-md-offset-5">

                                    <!-- Select Basic -->
                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="typrOfUse2">Type of Use</label>
                                            <select id="typrOfUse2" name="typrOfUse2" class="form-control" data-ng-model="item.typrOfUse2">
                                                <option style="display: none" value="">Select Type of Use</option>
                                                <option value="Light">Light</option>
                                                <option value="Medium">Medium</option>
                                                <option value="Heavy">Heavy</option>
                                            </select>
                                        </div>
                                    </div>

                                    <!-- Select Basic -->
                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="timeWorkedOfCampus2">How much Time Do You Worked Of Campus</label>
                                            <select id="timeWorkedOfCampus2" name="timeWorkedOfCampus2" class="form-control" data-ng-model="item.timeWorkedOfCampus2">
                                                <option style="display: none" value="">Select Worked Time</option>
                                                <option value="0">0  %</option>
                                                <option value="10">10%</option>
                                                <option value="20">20%</option>
                                                <option value="30">30%</option>
                                                <option value="40">40%</option>
                                                <option value="50">50%</option>
                                                <option value="60">60%</option>
                                                <option value="70">70%</option>
                                                <option value="80">80%</option>
                                                <option value="90">90%</option>
                                                <option value="100">100%</option>
                                            </select>
                                        </div>
                                    </div>
                                    <!-- Select Basic -->

                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="dataUse2">Data Use</label>
                                            <select id="dataUse2" name="dataUse2" class="form-control" data-ng-model="item.dataUse2">
                                                <option style="display: none" value="">Select Data Use</option>
                                                <option value="Light">Light</option>
                                                <option value="Medium">Medium</option>
                                                <option value="Heavy">Heavy</option>
                                            </select>
                                        </div>
                                    </div>

                                    <!-- Select Basic -->
                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="emailRequired2">Email Required</label>
                                            <select id="emailRequired2" name="emailRequired2" class="form-control" data-ng-model="item.emailRequired2">
                                                <option style="display: none" value="">Select Email Required</option>
                                                <option value="Yes">Yes</option>
                                                <option value="No">No</option>
                                            </select>
                                        </div>
                                    </div>
                                    <!-- Text input-->
                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="ipadJustification">Additional Comments</label>
                                            <input id="ipadJustification" name="ipadJustification" type="text" placeholder="Enter Justification" class="form-control input-md" data-ng-model="item.ipadJustification">
                                        </div>
                                    </div>
                                </div>


                        </div>

                        <!-- Multiple Radios -->
                        <%--                            <div class="form-group">
                                <label class="col-md-4 control-label" for="hasBKIdata">Do you have a BKI provided data device</label>
                                <div class="col-md-4">
                                    <div class="radio">
                                        <label for="hasBKIdata-0">
                                            <input type="radio" name="hasBKIdata" id="hasBKIdata-0" value="1" checked="checked">
                                            USB
                                        </label>
                                    </div>
                                    <div class="radio">
                                        <label for="hasBKIdata-1">
                                            <input type="radio" name="hasBKIdata" id="hasBKIdata-1" value="2">
                                            Pocket WiFi
                                        </label>
                                    </div>
                                </div>
                            </div>--%>





                        <!-- Multiple Checkboxes (inline) -->
                        <%--                        <div class="form-group">
                            <label class="col-md-4 control-label" for="hasBKIdata">Do you have a BKI provided data device</label>
                            <div class="col-md-4">
                                <label class="checkbox-inline" for="hasBKIdata-0">
                                    <input type="checkbox" name="hasBKIdata" id="hasBKIdata-0" value="1" ng-click="toggleDataDeviceDiv()" data-ng-model="item.hasBKIdata">
                                    Click to select Data Device
                                </label>
                            </div>
                        </div>--%>

                        <div class="form-group">
                            <label class="col-md-4 control-label" style="font-size: 1.5em; font-weight: bold; font-style: italic;" for="hasBKIdata">Do you have a BKI provided data device</label>
                            <div class="col-md-4">
                                <select id="hasBKIdata" name="hasBKIdata" class="form-control" data-ng-model="item.hasBKIdata" ng-change="toggleDataDeviceDiv()">
                                    <option style="display: none" value="">Click to select Data Service</option>
                                    <option value="Yes">Yes</option>
                                    <option value="No">No</option>
                                </select>
                            </div>
                        </div>




                        <div id="child3" ng-if="showDataDeviceDiv">
                            <!-- Select Basic -->
                            <%--                        <div class="form-group">
                            <label class="col-md-4 control-label" for="hasBKIdata">Do you have a BKI provided data device</label>
                            <div class="col-md-4">
                                <select id="hasBKIdata" name="hasBKIdata" class="form-control" data-ng-model="item.hasBKIdata">
                                    <option style="display: none" value="">Select Provider</option>
                                    <option value="1">Yes</option>
                                    <option value="2">No</option>
                                </select>
                            </div>
                        </div>--%>

                            <!-- Select Basic -->
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="color: blue;" for="dataModel">Model</label>
                                <div class="col-md-4">
                                    <select id="dataModel" name="dataModel" class="form-control" data-ng-model="item.dataModel">
                                        <option style="display: none" value="">Select Model</option>
                                        <option value="USB">USB</option>
                                        <option value="Pocket WiFi">Pocket WiFi</option>
                                    </select>
                                </div>
                            </div>

                            <!-- Select Basic -->
                            <div class="form-group">
                                <label class="col-md-4 control-label" style="color: blue;" for="providerList3">Provider</label>
                                <div class="col-md-4">
                                    <select id="providerList3" name="providerList3" class="form-control" data-ng-model="item.providerList3">
                                        <option style="display: none" value="">Select Provider</option>
                                        <option value="Telstra">Telstra</option>
                                        <option value="Optus">Optus</option>
                                        <%--<option value="Telstra">Telstra</option>
<option value="Vodafone">Vodafone</option>
<option value="Optus">Optus</option>
<option value="Other">Other</option>
<option value="ALDImobile">ALDImobile</option>
<option value="Amaysim">Amaysim</option>
<option value="Bendigo Bank Telco">Bendigo Bank Telco</option>
<option value="Boost Mobile">Boost Mobile</option>
<option value="Commander 6">Commander 6</option>
<option value="ClubTelco 6">ClubTelco 6</option>
<option value="CMobile">CMobile</option>
<option value="Dodo 6">Dodo 6</option>
<option value="Exetel">Exetel</option>
<option value="GoTalk">GoTalk</option>
<option value="Hello Mobile">Hello Mobile</option>
<option value="iiNet 7">iiNet 7</option>
<option value="Internode 7">Internode 7</option>
<option value="iPrimus 6">iPrimus 6</option>
<option value="Jeenee Mobile">Jeenee Mobile</option>
<option value="Just Mobile">Just Mobile</option>
<option value="Kogan Mobile">Kogan Mobile</option>
<option value="Live Connected 8">Live Connected 8</option>
<option value="Lebara Mobile">Lebara Mobile</option>
<option value="Lycamobile">Lycamobile</option>
<option value="OVO">OVO</option>
<option value="Southern Phone">Southern Phone</option>
<option value="Startel">Startel</option>
<option value="Reward Mobile">Reward Mobile</option>
<option value="TeleChoice">TeleChoice</option>
<option value="Think Mobile">Think Mobile</option>
<option value="TPG">TPG</option>
<option value="Vaya Mobile">Vaya Mobile</option>
<option value="Virgin Mobile 5">Virgin Mobile 5</option>
<option value="Woolworths Connect 9">Woolworths Connect 9</option>
<option value="Yomojo 10">Yomojo 10</option>--%>
                                    </select>
                                </div>
                            </div>


                            <div class="row">
                            <div class="col-lg-4 col-lg-offset-4 col-md-4 col-md-offset-4 well col-sm-4 col-sm-offset-4 well">
                                <span style="font-weight: bold; font-style: italic;">Justification for Data Device</span>
                            </div>
                                </div>

                                <%--                            <div id="justificationGroup1">--%>
                                <div id="justificationGroup3" class="col-lg-offset-5 col-md-offset-5">

                                    <!-- Select Basic -->
                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="typrOfUse3">Type of Use</label>
                                            <select id="typrOfUse3" name="typrOfUse3" class="form-control" data-ng-model="item.typrOfUse3">
                                                <option style="display: none" value="">Select Type of Use</option>
                                                <option value="Light">Light</option>
                                                <option value="Medium">Medium</option>
                                                <option value="Heavy">Heavy</option>
                                            </select>
                                        </div>
                                    </div>

                                    <!-- Select Basic -->
                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="timeWorkedOfCampus3">How much Time Do You Worked Of Campus</label>
                                            <select id="timeWorkedOfCampus3" name="timeWorkedOfCampus3" class="form-control" data-ng-model="item.timeWorkedOfCampus3">
                                                <option style="display: none" value="">Select Worked Time</option>
                                                <option value="0">0  %</option>
                                                <option value="10">10%</option>
                                                <option value="20">20%</option>
                                                <option value="30">30%</option>
                                                <option value="40">40%</option>
                                                <option value="50">50%</option>
                                                <option value="60">60%</option>
                                                <option value="70">70%</option>
                                                <option value="80">80%</option>
                                                <option value="90">90%</option>
                                                <option value="100">100%</option>
                                            </select>
                                        </div>
                                    </div>
                                    <!-- Select Basic -->
                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="dataUse3">Data Use</label>
                                            <select id="dataUse3" name="dataUse3" class="form-control" data-ng-model="item.dataUse3">
                                                <option style="display: none" value="">Select Data Use</option>
                                                <option value="Light">Light</option>
                                                <option value="Medium">Medium</option>
                                                <option value="Heavy">Heavy</option>
                                            </select>
                                        </div>
                                    </div>

                                    <!-- Select Basic -->
                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="emailRequired3">Email Required</label>
                                            <select id="emailRequired3" name="emailRequired3" class="form-control" data-ng-model="item.emailRequired3">
                                                <option style="display: none" value="">Select Email Required</option>
                                                <option value="Yes">Yes</option>
                                                <option value="No">No</option>
                                            </select>
                                        </div>
                                    </div>
                                    <!-- Text input-->
                                    <div class="row">
                                        <div class="form-group col-sm-5 col-md-5 col-lg-5">
                                            <label class="control-label" style="color: blue;" for="deviceJustification">Additional Comments</label>
                                            <input id="deviceJustification" name="deviceJustification" type="text" placeholder="Enter Justification" class="form-control input-md" data-ng-model="item.deviceJustification">
                                        </div>
                                    </div>
                                </div>
                            


                            <!-- Text input-->
<%--                            <div class="form-group">
                                <label class="col-md-4 control-label" style="color: blue;" for="deviceJustification">Justification for Device</label>
                                <div class="col-md-4">
                                    <input id="deviceJustification" name="deviceJustification" type="text" placeholder="Enter Justification" class="form-control input-md" data-ng-model="item.deviceJustification">
                                </div>
                            </div>--%>

                        </div>

                        <!-- Button -->
                        <div class="form-group">
                            <label class="col-md-4 control-label" style="font-size: 1.5em; font-weight: bold; font-style: italic;" for="singlebutton">Click Here to Submit</label>
                            <div class="col-md-4">
                                <%--                                    <button id="singlebutton" name="singlebutton" class="btn btn-primary">Submit</button>--%>
                                <input type="submit" name="singlebutton" value="{{isSubmittingForButton === true ? 'saving...' : 'Save'}}" class="btn btn-default" ng-disabled="isSubmittingForButton"></input>
                                <%--                                <input type="submit" name="singlebutton" value="Save" class="btn btn-default" ng-disabled="isSubmittingForButton"></input>--%>
                            </div>
                        </div>

                    </fieldset>
                </form>


            </div>
        </div>
        <%--</div>--%>
    </div>

</body>
</html>
