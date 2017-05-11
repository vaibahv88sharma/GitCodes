/// <reference path="_references.js" />
//'use strict';

(function () {

    //App Module
    var app = angular.module('bkiMobileInfo', ['ngResource']);

    //App Provider
    angular.module('bkiMobileInfo').provider('bkiService', function () {
        //document.getElementById('mobileFormWell2').style.display = 'none';
        var usrNm = '';
        this.userName = function (name) {
            usrNm = name;
        };

        this.$get = ['$q', function ($q) {
            var calcNameService = {};
            var context = SP.ClientContext.get_current();
            var user = context.get_web().get_currentUser();
            //debugger;
            var printableName = {
                userLoginName: "",
                userEmail: ""
                };
            //var printableName = "";
            calcNameService.getUserName = function () {
                var deferred = $q.defer();
                context.load(user);
                context.executeQueryAsync(
                    function () {
                        //printableName = user.get_title().split("\\")[usrNm];
                        //$('#message').text('Hello ' + printableName + ", ");
                        //debugger;
                        printableName["userLoginName"] = user.get_title().split("\\")[usrNm];
                        printableName["userEmail"] = user.get_email();
                        $('#message').text('Hello ' + printableName["userLoginName"] + ", ");
                        deferred.resolve(printableName);
                    },
                    function () {
                        console.log('Failed to get user name. Error:' + args.get_message());
                        deferred.reject(args.get_message());
                    }
                );
                return deferred.promise;
            };

            return calcNameService;
        }];
    });

    //App config
    angular.module('bkiMobileInfo').config(['bkiServiceProvider', function (bkiServiceProvider) {
        bkiServiceProvider.userName('0');
    }]);

    //App Directive for Forms Data
    angular.module('bkiMobileInfo').directive('ngAppFrame', ['$timeout', '$window', function ($timeout, $window) {

        return {
            restrict: 'A',
            link: function (scope, element, attrs) {

                element.css("display", "block");

                function getQsParam(name) {
                    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
                    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
                    results = regex.exec(location.search);
                    return results === null ? "" : results[1].replace(/\+/g, " ");
                }

                scope.$watch
                (
                    function () {
                        return element[0].offsetHeight;
                    },
                    function (newHeight, oldHeight) {
                        newHeight = 2180 + 400;
                        //newHeight = 400;
                        if (newHeight != oldHeight) {
                            $timeout(function () {
                                var height =  attrs.minheight ? newHeight + parseInt(attrs.minheight) : newHeight;
                                var id = getQsParam("SenderId")
                                var message = "<message senderId=" + id + ">resize(100%," + height + ")</message>";
                                $window.parent.postMessage(message, "*");
                            }, 0);// timeout needed to wait for DOM to update
                        }
                    }
                );
            }
        };

    }]);

    // Controller
    angular.module('bkiMobileInfo').controller('bkiMobileInfoCtrl', ['details', 'bkiService', 'constants', '$q', '$log', '$scope', function (details, bkiService, constants, $q, $log, $scope) {

        var vm = this;

        $scope.firstRecord = true;

        $scope.item = [];
        $scope.auditId = "";

        $scope.isSubmittingForButton = false;
        $scope.item.additionalDeviceNumber = "";

        $scope.submitForm = function (item) {
            debugger;
            $scope.isSubmittingForButton = true;

            angular.forEach($scope.additionalDeviceNumberArray, function (item, index) {
                console.log(item.value);
                $scope.item.additionalDeviceNumber += index + "--" + item.value + ",";
            })

            details.postNewResource(item).then(function (data) {
                console.log(data);
                $scope.auditId = 200 + data.d.ID;
                var userNameBkp = $scope.item.userName;
                var userEmailBkp = $scope.item.userEmail;

                $scope.item = [];

                $scope.item.userName = userNameBkp;
                $scope.item.userEmail = userEmailBkp;

                $scope.isSubmittingForButton = false;

                debugger;
                vm.hideMobileForm();
                //details.getSelectiveRequest($scope.item.userEmail)
                //    .then(function (data) {
                //        debugger;
                //        console.log(data);
                //        //result.d.results.length;
                //    })
                //    .catch(function (e) {
                //        debugger;
                //        console.log(e);
                //        // handle errors in processing or in error.
                //    });

            });
        };

        vm.hideMobileForm = function () {
            debugger;
            details.getSelectiveRequest($scope.item.userEmail)
                .then(function (data) {
                    debugger;
                    console.log(data);
                    if (data.d.results.length > 0) {
                        $scope.auditId = 200 + data.d.results[0].ID;
                        $scope.firstRecord = !$scope.firstRecord;
                        //document.getElementById('mobileFormWell').style.display = 'none';                        
                        //document.getElementById('mobileFormWell2').style.display = 'block';
                    }
                    //else {
                    //    document.getElementById('mobileFormWell').style.display = 'block';
                    //    document.getElementById('mobileFormWell2').style.display = 'none';
                    //}
                })
                .catch(function (e) {
                    debugger;
                    console.log(e);
                    // handle errors in processing or in error.
                });

        }

        //$scope.showDiv = false;

        $scope.loginName = bkiService.getUserName().then(function (response) {
            console.log(response);
            
            //$scope.item.userName = response;
            $scope.item.userName = response["userLoginName"];
            $scope.item.userEmail = response["userEmail"];
            debugger;
            vm.hideMobileForm();

            //details.getSelectiveRequest($scope.item.userEmail)
            //    .then(function (data) {
            //        debugger;
            //        console.log(data);
            //        //result.d.results.length;
            //    })
            //    .catch(function (e) {
            //        debugger;
            //        console.log(e);
            //        // handle errors in processing or in error.
            //    });
        });

        $scope.showDiv = false;
        $scope.toggleShowDiv = function () {
            console.log('toggleShowDiv');
            $scope.showDiv = !$scope.showDiv;
        }



        $scope.showMobileDiv = false;
        $scope.toggleMobileDiv = function () {
            console.log('toggleMobileDiv');
            $scope.showMobileDiv = !$scope.showMobileDiv;
        }

        $scope.toggleMobileDiv2 = function () {
            switch ($scope.item.hasMobileNumber) {
                case 'Yes':
                    $scope.showMobileDiv = true;
                    break;
                case 'No':
                    $scope.showMobileDiv = false;
                    break;
                default:
                    $scope.showMobileDiv = false;
            }
            //$scope.showMobileDiv = !$scope.showMobileDiv;
        }



        $scope.showTableteDiv = false;
        $scope.toggleTabletDiv = function () {
            console.log('toggleMobileDiv');
            //$scope.showTableteDiv = !$scope.showTableteDiv;
            switch ($scope.item.hasTabletorNot) {
                case 'Yes':
                    $scope.showTableteDiv = true;
                    break;
                case 'No':
                    $scope.showTableteDiv = false;
                    break;
                default:
                    $scope.showTableteDiv = false;
            }
        }


        $scope.showDataDeviceDiv = false;
        $scope.toggleDataDeviceDiv = function () {
            console.log('toggleMobileDiv');
            //$scope.showDataDeviceDiv = !$scope.showDataDeviceDiv;
            switch ($scope.item.hasBKIdata) {
                case 'Yes':
                    $scope.showDataDeviceDiv = true;
                    break;
                case 'No':
                    $scope.showDataDeviceDiv = false;
                    break;
                default:
                    $scope.showDataDeviceDiv = false;
            }
        }


        // Button OnClick event to add Text Control dynamically         
        $scope.additionalDeviceNumberArray = [];

        $scope.addTextControl = function () {
            debugger;
            $scope.additionalDeviceNumberArray.push({});        
        }


        //$scope.title = constants.APP_HEADTITLE;
        //$scope.hyperlink = constants.APP_LIST;

        //$scope.search = {};

        //details.getRequestDetails()
        //    .then(function (data) {
        //        $scope.formsData = data.d.results;
        //        $log.info(data.d.results);
        //    })
        //    .catch(function (data) {
        //        $log.error(data);
        //    });

    }]);

    // Factory Service
    angular.module('bkiMobileInfo').factory("details", ["constants", "$http", "$q", "$resource", '$log', function (constants, $http, $q, $resource, $log) {

        //Forms Data
        function getRequestDetails(userEmail) {
            debugger;
            var resource = getdetails(userEmail);
            //var resource = getdetails();
            var deferred = $q.defer();

            resource.query({},
                function (data) {
                    debugger;
                    deferred.resolve(data);
                }, function (error) {
                    debugger;
                    deferred.reject(error);
                });
            return deferred.promise;
        }

        function getdetails_old(id) {
            //var urlSite = decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")) +
            //            "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + constants.APP_LIST + "')/items?@target='" +
            //            decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'";
            var url = constants.HYPERLINK_URL;
            if (id) {
                return $resource(
                    //urlSite,
                    url,
                    { id: id },
                    {
                        query: {
                            method: 'GET', //isArray: false, 
                            headers: { "Accept": "application/json; odata=verbose" },
                            params: {
                                '$select': constants.HYPERLINK_SELECT,
                                '$expand': constants.HYPERLINK_EXPAND
                            },
                        }
                    }
                );
            }
            else {
                return $resource(
                    url,
                    //urlSite,
                    {},
                    {
                        query: {
                            method: 'GET',
                            headers: { "Accept": "application/json; odata=verbose" },
                            params: {
                                //'$select': "'" + constants.HYPERLINK_SELECT + "'",
                                //'$expand': "'" + constants.HYPERLINK_EXPAND + "'"
                                '$select': constants.HYPERLINK_SELECT,
                                '$expand': constants.HYPERLINK_EXPAND
                            },
                        }
                    }
                );
            }
        }

        //Forms Data Category
        function postNewResource(noteData) {
            var resource = getDetails();
            var deferred = $q.defer();
            debugger;
            var dataMeta = {
                "__metadata": { "type": constants.ITEM_TYPE }
            };
            var dataUpdate = $.extend({}, dataMeta, noteData);
            resource.post(dataUpdate,
                function (data) {
                    deferred.resolve(data);
                }, function (error) {
                    console.log(error.data.error.message.value);
                    console.log(error);
                    deferred.reject(error);
                });
            return deferred.promise;
        }

        function getDetails(id) {
            debugger;
            if (id) {
                debugger;
                return $resource(
                    constants.CATEGORY_URL,
                    { id: id },
                    {
                        query: {
                            method: 'GET', isArray: false,
                            headers: {
                                "Accept": "application/json; odata=verbose",
                                "X-RequestDigest": $("#__REQUESTDIGEST").val()
                            },
                            params: {
                                //'$select': 'hasMobileNumber, userName, mobileNumber, mobileBrandName, mobileModelName, serialNumberMobile, providerList1, dataOnMobile, mobilePhoneJustification, hasTabletorNot, brandTablet, tabletModel, ipadSerial, providerList4, ipadJustification, hasBKIdata, dataModel, providerList3, deviceJustification, userEmail',
                                '$select': constants.SELECT_QUERY_MOBILE,
                                //'$filter': 'userEmail eq ' + id,
                                // '$expand': 'user/Id,category'
                            },
                        },
                        post: {
                            method: "POST",
                            headers: {
                                "accept": "application/json;odata=verbose",
                                "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                                "content-type": "application/json;odata=verbose"
                            }
                        }
                    }
                );
            }
            else {
                return $resource(
                    constants.CATEGORY_URL,
                    {},
                    {
                        query: {
                            method: 'GET', isArray: false,
                            headers: {
                                "Accept": "application/json; odata=verbose",
                                "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                            },
                            params: {
                                '$select': constants.SELECT_QUERY_MOBILE,
                                //'$select': 'hasMobileNumber, userName, mobileNumber, mobileBrandName, mobileModelName, serialNumberMobile, providerList1, dataOnMobile, mobilePhoneJustification, hasTabletorNot, brandTablet, tabletModel, ipadSerial, providerList4, ipadJustification, hasBKIdata, dataModel, providerList3, deviceJustification, userEmail',
                                //'$expand': 'user/Id,category'
                            },
                        },
                        post: {
                            method: "POST",
                            headers: {
                                "accept": "application/json;odata=verbose",
                                "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                                "content-type": "application/json;odata=verbose"
                            }
                        }
                    }
                );
            }
        }

        //Get Selective User based on EmailId

        var getSelectiveRequest = function (userEmail) {
            //var fullUrlSelective = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('NoteList')/items('" + id + "')?" +
            //                        "$select=Id,Title,Description,userName,categoryName,content&@target='" + hostweburl + "'";

            var fullUrlSelective = constants.CATEGORY_URL + '&$select=' + constants.SELECT_QUERY_MOBILE + '&$filter=' + 'userEmail eq ' + "'"+userEmail +"'";

            var deferred = $q.defer();
            $http({
                url: fullUrlSelective,
                method: "GET",
                headers: {
                    "accept": "application/json;odata=verbose",
                    "content-Type": "application/json;odata=verbose"
                }
            })
                .success(function (result) {
                    //debugger;
                    deferred.resolve(result);
                })
                .error(function (result, status) {
                    //debugger;
                    deferred.reject(status);
                });
            return deferred.promise;
        };




        return {
            postNewResource: postNewResource,
            //getRequestDetails: getRequestDetails,
            getSelectiveRequest: getSelectiveRequest
        };
    }]);

    //Constant Service
    angular.module('bkiMobileInfo').constant('constants', {
        //APP_TITLE: 'Book Logger',
        //APP_HEADTITLE: decodeURIComponent(getQueryStringParameter("AppTitle")),
        //APP_LIST: decodeURIComponent(getQueryStringParameter("ListName")),
        //HYPERLINK_SELECT: 'ID,Title,hyperlink,hyperlinkTitle,hyperlinkTarget,DepartmentCategoryId,DepartmentCategory/DepartmentCategory,DepartmentCategory/Id',
        //HYPERLINK_EXPAND: 'DepartmentCategory',
        //HYPERLINK_URL : decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")) +
        //                "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + decodeURIComponent(getQueryStringParameter("ListName")) + "')/items?@target='" +
        //                decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'",

        //CATEGORY_SELECT: 'ID,DepartmentCategory',
        CATEGORY_URL: decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")) +
                        "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + decodeURIComponent(getQueryStringParameter("ListName")) + "')/items?@target='" +
                        decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'",

        ITEM_TYPE: getItemTypeForListName(decodeURIComponent(getQueryStringParameter("ListName"))),

        SELECT_QUERY_MOBILE: "ID, userName, userEmail, hasMobileNumber, laptopNumber, typrOfUse, mobilePhoneJustification, hasTabletorNot, desktopNumber, typrOfUse2, hasBKIdata, additionalDeviceNumber",


    });

    //General
    //General -- Get App Part Preperties
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
    //General -- Get APP URL
    function manageQueryStringParameter(paramToRetrieve) {
        var params =
        document.URL.split("?")[1].split("&");
        //var strParams = "";
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0] == paramToRetrieve) {
                return singleParam[1];
            }
        }
    }
    //General -- Item Type for post call
    function getItemTypeForListName(name) {
        return "SP.Data." + name.charAt(0).toUpperCase() + name.split(" ").join("").slice(1) + "ListItem";
    }
})();
