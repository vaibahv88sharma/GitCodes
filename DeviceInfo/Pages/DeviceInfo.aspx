<%@ Page language="C#" Inherits="Microsoft.SharePoint.WebPartPages.WebPartPage, Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="WebPartPages" Namespace="Microsoft.SharePoint.WebPartPages" Assembly="Microsoft.SharePoint, Version=16.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>

<WebPartPages:AllowFraming ID="AllowFraming" runat="server" />
<form runat="server">
    <SharePoint:FormDigest ID="FormDigest1" runat="server"></SharePoint:FormDigest>
</form>
<html>
<head>
    <title>Device Information</title>

    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no">

    <script type="text/javascript" src="../Scripts/jquery-2.2.4.min.js"></script>
    <script type="text/javascript" src="/_layouts/15/MicrosoftAjax.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.runtime.js"></script>
    <script type="text/javascript" src="/_layouts/15/sp.js"></script>

    <script type="text/javascript" src="../Scripts/angular.min.js"></script>
    <script type="text/javascript" src="../Scripts/angular-resource.min.js"></script>
    <script type="text/javascript" src="../Scripts/bootstrap.min.js"></script>

<%--    <script type="text/javascript" src="../Scripts/App.js"></script>--%>
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

        //$('#repeatTextControl').submit(function () {
        //    return false;
        //});
        

    </script>
</head>
<body ng-app="bkiMobileInfo" style="padding: 20px;">
    <div class="container-fluid" ng-controller="bkiMobileInfoCtrl" ng-app-frame minheight="80">

        <div class="row">
            <div class="jumbotron">
                <p id="message1">
                    Hello {{item.userName}},
                </p>
            </div>
        </div>

        <div class="row" ng-if="!firstRecord">
            <div class="well" id="mobileFormWell2" style="height: 12em;">
                <div class="col-md-12 col-md-12 col-sm-12 col-xs-12">
                    <span style="font-size: 2.5em; font-style: italic;">
                        Thanks for updating the Device Details, your Reference Number is : {{auditId}}.
                    </span>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="well" id="mobileFormWell" ng-if="firstRecord">

                <form class="form-horizontal" name="ContactForm" id="ContactForm" method="post" role="form" novalidate ng-submit="submitForm(item)">
                    <fieldset>


                        <%--Laptop Section--%>

                        <div class="form-group" id="ques1">
                            <label class="col-lg-5 col-md-5 col-sm-6 col-xs-12 control-label" style="font-size: 1.5em; font-weight: bold; font-style: italic;" for="mobileNumberGrp">Do you have a BKI provided Laptop</label>
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <select id="mobileNumberGrp" name="mobileNumberGrp" class="form-control" data-ng-model="item.hasMobileNumber" ng-change="toggleMobileDiv2()">
                                    <option style="display: none" value="">Click to select Laptop</option>
                                    <option value="Yes">Yes</option>
                                    <option value="No">No</option>
                                </select>
                            </div>
                        </div>

                        <div id="child1" ng-if="showMobileDiv">

                            <!-- Text input-->
                            <div class="form-group">
                                <label class="col-lg-5 col-md-5 col-sm-3 col-xs-12 control-label" for="laptopNumber">Laptop Number</label>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <input id="laptopNumber" name="laptopNumber" type="text" placeholder="Enter Laptop Number" class="form-control input-md" data-ng-model="item.laptopNumber">
                                    <span class="help-block">
                                        <div class="row">
                                            Please refer the below mentioned images or document to find Laptop Number
                                        </div>
                                        <div class="row">
                                            <%--<img src="/PortableComputer.png" alt="Mountain View" style="width:304px;height:228px;">--%>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                <img src="https://portal.myselfserve.com.au/sites/StaffPortal/PublishingImages/DeviceSurvey/Img1.jpg" alt="Laptop Number" style="height: 4.8em !important;">
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                <img src="https://portal.myselfserve.com.au/sites/StaffPortal/PublishingImages/DeviceSurvey/Img2.jpg" alt="Laptop Number" style="height: 4.8em !important;">
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                <img src="https://portal.myselfserve.com.au/sites/StaffPortal/PublishingImages/DeviceSurvey/Img3.jpg" alt="Laptop Number" style="height: 4.8em !important;">
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <br />
                                                <a href="http://staffportal.myselfserve.com.au/sites/StaffPortal/Documents/Laptop.docx?d=w0a2ae0d650ab4f4dbcf6b5a0a2753762" target="_blank">Click here to view the steps to find machine number</a>
                                            </div>
                                        </div>
                                    </span>

                                    <div class="row">
                                        <div class="col-lg-10 col-lg-offset-1 col-md-10 col-md-offset-1 col-sm-10 col-sm-offset-1 well">
                                            <span style="font-weight: bold; font-style: italic;">Justification for Laptop</span>
                                        </div>
                                    </div>

<%--                                    <div id="justificationGroup1" class="col-lg-offset-3 col-md-offset-3 col-sm-offset-3">--%>
                                    <div id="justificationGroup1" class="col-lg-offset-1 col-md-offset-1 col-sm-offset-1">

                                        <!-- Select Basic -->
<%--                                        <div class="row">
                                            <div class="form-group col-sm-8 col-md-8 col-lg-8">
                                                <label class="control-label" style="color: blue;" for="typrOfUse">Type of Use</label>
                                                <select id="typrOfUse" name="typrOfUse" class="form-control" data-ng-model="item.typrOfUse">
                                                    <option style="display: none" value="">Select Type of Use</option>
                                                    <option value="Light">Light</option>
                                                    <option value="Medium">Medium</option>
                                                    <option value="Heavy">Heavy</option>
                                                </select>
                                            </div>
                                        </div>--%>

                                        <!-- Multiline Text input-->
                                        <div class="row">
                                            <div class="form-group col-sm-11 col-md-11 col-lg-11">
                                                <label class="control-label" style="color: blue;" for="mobilePhoneJustification">Primary Reason of Usage</label>
<%--                                                <input id="mobilePhoneJustification" name="mobilePhoneJustification" type="text" placeholder="Enter Justification" class="form-control input-md" data-ng-model="item.mobilePhoneJustification">--%>
                                                <textarea   id="mobilePhoneJustification" 
                                                            name="mobilePhoneJustification" 
                                                            class="form-control"   
                                                            placeholder="Enter Primary Reason"  
                                                            rows="6"            
                                                            data-ng-model="item.mobilePhoneJustification">
                                                </textarea>
                                            </div>
                                        </div>

                                    </div>


                                </div>
                            </div>

                        </div>

                        

                        <%--Desktop Section--%>

                        <div class="form-group" id="ques2">
                            <label class="col-lg-5 col-md-5 col-sm-6 col-xs-12 control-label" style="font-size: 1.5em; font-weight: bold; font-style: italic;" for="hasTabletorNot">Do you have a BKI provided Desktop</label>
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <select id="hasTabletorNot" name="hasTabletorNot" class="form-control" data-ng-model="item.hasTabletorNot" ng-change="toggleTabletDiv()">
                                    <option style="display: none" value="">Click to select Desktop</option>
                                    <option value="Yes">Yes</option>
                                    <option value="No">No</option>
                                </select>
                            </div>
                        </div>

                        <div id="child2" ng-if="showTableteDiv">

                            <!-- Text input-->
                            <div class="form-group">
                                <label class="col-lg-5 col-md-5 col-sm-3 col-xs-12 control-label" for="desktopNumber">Desktop Number</label>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <input id="desktopNumber" name="desktopNumber" type="text" placeholder="Enter Laptop Number" class="form-control input-md" data-ng-model="item.desktopNumber">
                                    <span class="help-block">
                                        <div class="row">
                                            Please refer the below mentioned images or document to find Laptop Number
                                        </div>
                                        <div class="row">
                                            <%--<img src="/PortableComputer.png" alt="Mountain View" style="width:304px;height:228px;">--%>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                <img src="https://portal.myselfserve.com.au/sites/StaffPortal/PublishingImages/DeviceSurvey/Img1.jpg" alt="Laptop Number" style="height: 4.8em !important;">
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                <img src="https://portal.myselfserve.com.au/sites/StaffPortal/PublishingImages/DeviceSurvey/Img2.jpg" alt="Laptop Number" style="height: 4.8em !important;">
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                <img src="https://portal.myselfserve.com.au/sites/StaffPortal/PublishingImages/DeviceSurvey/Img3.jpg" alt="Laptop Number" style="height: 4.8em !important;">
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <br />
                                                <a href="http://staffportal.myselfserve.com.au/sites/StaffPortal/Documents/Laptop.docx?d=w0a2ae0d650ab4f4dbcf6b5a0a2753762" target="_blank">Click here to view the steps to find machine number</a>
                                            </div>
                                        </div>
                                    </span>

                                    <div class="row">
                                        <div class="col-lg-10 col-lg-offset-1 col-md-10 col-md-offset-1 col-sm-10 col-sm-offset-1 well">
                                            <span style="font-weight: bold; font-style: italic;">Justification for Desktop</span>
                                        </div>
                                    </div>

                                    <div id="justificationGroup2" class="col-lg-offset-1 col-md-offset-1 col-sm-offset-1">

                                        <!-- Select Basic -->
<%--                                        <div class="row">
                                            <div class="form-group col-sm-8 col-md-8 col-lg-8">
                                                <label class="control-label" style="color: blue;" for="typrOfUse2">Type of Use</label>
                                                <select id="typrOfUse2" name="typrOfUse2" class="form-control" data-ng-model="item.typrOfUse2">
                                                    <option style="display: none" value="">Select Type of Use</option>
                                                    <option value="Light">Light</option>
                                                    <option value="Medium">Medium</option>
                                                    <option value="Heavy">Heavy</option>
                                                </select>
                                            </div>
                                        </div>--%>

                                        <!-- Text input-->
                                        <div class="row">
                                            <div class="form-group col-sm-11 col-md-11 col-lg-11">
                                                <label class="control-label" style="color: blue;" for="ipadJustification">Primary Reason of Usage</label>
<%--                                                <input id="ipadJustification" name="ipadJustification" type="text" placeholder="Enter Justification" class="form-control input-md" data-ng-model="item.ipadJustification">--%>
                                                <textarea   id="ipadJustification" 
                                                            name="ipadJustification" 
                                                            class="form-control"   
                                                            placeholder="Enter Primary Reason"  
                                                            rows="6"            
                                                            data-ng-model="item.ipadJustification">
                                                </textarea>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>

                        </div>



                        <%--Additional Device Section--%>

                        <div class="form-group">
                            <label class="col-lg-5 col-md-5 col-sm-6 col-xs-12 control-label" style="font-size: 1.5em; font-weight: bold; font-style: italic;" for="hasBKIdata">Do you have additional laptop, desktop or monitor</label>
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <select id="hasBKIdata" name="hasBKIdata" class="form-control" data-ng-model="item.hasBKIdata" ng-change="toggleDataDeviceDiv()">
                                    <option style="display: none" value="">Click to select additional device</option>
                                    <option value="Yes">Yes</option>
                                    <option value="No">No</option>
                                </select>
                            </div>
                        </div>

                        <div id="child3" ng-if="showDataDeviceDiv">
                            <!-- Text input-->

                            <div class="form-group">
                                <label class="col-lg-5 col-md-5 col-sm-3 col-xs-12 control-label" for="additionalDevice">Additional Device</label>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <%--<input id="additionalDevice" name="additionalDevice" type="text" placeholder="Please enter additional device details" class="form-control input-md" data-ng-model="item.additionalDeviceNumber">--%>
                                    <input id="additionalDevice" 
                                        name="additionalDevice" 
                                        type="text" 
                                        placeholder="Please enter additional device details" 
                                        class="form-control input-md" 
                                        data-ng-repeat="singleItemDeviceNumber in additionalDeviceNumberArray"
                                        data-ng-model="singleItemDeviceNumber.value">

<%--                                    <input id="repeatTextControl" type="submit" data-ng-click="addTextControl()" value="Click here to add additional device " />--%>
                                    <button type="button" class="btn btn-primary" data-ng-click="addTextControl()">Click here to add additional device</button>


                                    <span class="help-block">
                                        <div class="row">
                                            Please refer the below mentioned images or document to find Laptop Number
                                        </div>
                                        <div class="row">
                                            <%--<img src="/PortableComputer.png" alt="Mountain View" style="width:304px;height:228px;">--%>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                <img src="https://portal.myselfserve.com.au/sites/StaffPortal/PublishingImages/DeviceSurvey/Img1.jpg" alt="Laptop Number" style="height: 4.8em !important;">
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                <img src="https://portal.myselfserve.com.au/sites/StaffPortal/PublishingImages/DeviceSurvey/Img2.jpg" alt="Laptop Number" style="height: 4.8em !important;">
                                            </div>
                                            <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                                <img src="https://portal.myselfserve.com.au/sites/StaffPortal/PublishingImages/DeviceSurvey/Img3.jpg" alt="Laptop Number" style="height: 4.8em !important;">
                                            </div>
                                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <br />
                                                <a href="http://staffportal.myselfserve.com.au/sites/StaffPortal/Documents/Laptop.docx?d=w0a2ae0d650ab4f4dbcf6b5a0a2753762" target="_blank">Click here to view the steps to find machine number</a>
                                            </div>
                                        </div>
                                    </span>

                                    <div class="row">
                                        <div class="col-lg-10 col-lg-offset-1 col-md-10 col-md-offset-1 col-sm-10 col-sm-offset-1 well">
                                            <span style="font-weight: bold; font-style: italic;">Justification for Additional Device</span>
                                        </div>
                                    </div>

                                    <div id="justificationGroup3" class="col-lg-offset-1 col-md-offset-1 col-sm-offset-1">

                                        <!-- Select Basic -->
<%--                                        <div class="row">
                                            <div class="form-group col-sm-8 col-md-8 col-lg-8">
                                                <select id="typrOfUse3" name="typrOfUse3" class="form-control" data-ng-model="item.typrOfUse3">
                                                    <option style="display: none" value="">Select Type of Use</option>
                                                    <option value="Light">Light</option>
                                                    <option value="Medium">Medium</option>
                                                    <option value="Heavy">Heavy</option>
                                                </select>
                                            </div>
                                        </div>--%>

                                        <!-- Text input-->
                                        <div class="row">
                                            <div class="form-group col-sm-11 col-md-11 col-lg-11">
                                                <label class="control-label" style="color: blue;" for="deviceJustification">Primary Reason of Usage</label>
<%--                                                <input id="deviceJustification" name="deviceJustification" type="text" placeholder="Enter Justification" class="form-control input-md" data-ng-model="item.deviceJustification">--%>
                                                <textarea   id="deviceJustification" 
                                                            name="deviceJustification" 
                                                            class="form-control input-md"   
                                                            placeholder="Enter Primary Reason"  
                                                            rows="6"            
                                                            data-ng-model="item.deviceJustification">
                                                </textarea>
                                            </div>
                                        </div>

                                    </div>

                                </div>
                            </div>
                        </div>



                        <%--Submit Button--%>

                        <div class="form-group">
                            <label class="col-lg-5 col-md-5 col-sm-3 control-label" style="font-size: 1.5em; font-weight: bold; font-style: italic;" for="singlebutton">Click Here to Submit</label>
                            <div class="col-lg-6 col-md-6 col-sm-12">
                                <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <input type="submit" name="singlebutton" value="{{isSubmittingForButton === true ? 'saving...' : 'Save'}}" class="btn btn-primary" ng-disabled="isSubmittingForButton"></input>
                                </div>
                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                    <span><h4>Please note your Reference Number after submitting Survey</h4></span>
                                </div>
                            </div>
                        </div>


                    </fieldset>
                </form>


            </div>
        </div>

    </div>
</body>
</html>
