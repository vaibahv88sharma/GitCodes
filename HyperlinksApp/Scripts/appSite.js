//'use strict';

(function () {

    //var app = angular.module('app', ['ngRoute', 'ngCookies']);

    //App Module
    var app = angular.module('app', ['ngResource']);

    //angular.module('app').directive('ngAppFrame', ['$timeout', '$window', '$watch', function ($timeout, $window, $watch) {
    angular.module('app').directive('ngAppFrame', ['$timeout', '$window', function ($timeout, $window) {

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

                        if (newHeight != oldHeight) {
                            $timeout(function () {
                                var height = attrs.minheight ? newHeight + parseInt(attrs.minheight) : newHeight;
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
    angular.module('app').controller('HomeController', ['details', 'constants', '$q', '$log', '$scope', function (details, constants, $q, $log, $scope) {
        //$scope.title = decodeURIComponent(getQueryStringParameter("AppTitle"));
        //$scope.hyperlink = decodeURIComponent(getQueryStringParameter("ListName"));
        $scope.title = constants.APP_HEADTITLE;
        $scope.hyperlink = constants.APP_LIST;

        details.getRequest()
            .then(function (data) {
                $scope.hyperlinks = data.d.results;
                $log.info(data);
                //$scope.$parent.$broadcast('resizeframe');
            })
            .catch(function (data) {
                $log.error(data);
            });

        //console.log(sProp);
        //console.log(eProp);

        //function getQueryStringParameter(paramToRetrieve) {
        //    var params;
        //    var strParams;

        //    params = document.URL.split("?")[1].split("&");
        //    strParams = "";
        //    for (var i = 0; i < params.length; i = i + 1) {
        //        var singleParam = params[i].split("=");
        //        if (singleParam[0] == paramToRetrieve)
        //            return singleParam[1];
        //    }
        //}

    }]);

    // Factory Service
    angular.module('app').factory("details", ["constants", "$http", "$q", "$resource", '$log', function (constants, $http, $q, $resource, $log) {

        var getRequest = function () {
            var deferred = $q.defer();
            var urlSite = decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")) +
                        "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + constants.APP_LIST + "')/items?@target='" +
                        decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'";
            $http({
                //url: decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")) +
                //        "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + constants.APP_LIST + "')/items?@target='" +
                //        decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'",
                url: urlSite,
                method: "GET",
                headers: {
                    "accept": "application/json;odata=verbose",
                    "content-Type": "application/json;odata=verbose",
                    "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                }//,
                //xhrFields: {
                //    withCredentials: true
                //},
                //username: "KBTM\vsharma.adm",
                //password: "india@123"
            })
                .success(function (result) {
                    //$log.info(result);
                    deferred.resolve(result);
                })
                .error(function (result, status) {
                    deferred.reject(status);
                });
            return deferred.promise;
        };

        function getRequest1() {
            var resource = getdetails();
            var deferred = $q.defer();

            resource.query({},
                function (data) {
                    deferred.resolve(data);
                }, function (error) {
                    deferred.reject(error);
                });
            return deferred.promise;
        }

        function getdetails(id) {
            if (id) {
                return $resource(
                    decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")) +
                    "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + constants.APP_LIST + "')/items?@target='" +
                    decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'",
                    { id: id },
                    {
                        query: {
                            method: 'GET', //isArray: false, 
                            headers: { "Accept": "application/json; odata=verbose" },
                            params: {
                                '$select': constants.HYPERLINK_SELECT,
                                //'$expand': 'nameDetails/Id'
                            },

                        }
                    }
                );
            }
            else {
                return $resource(
                    //decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")).split("#")[0] +
                    decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")) +
                    "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + constants.APP_LIST + "')/items?@target='" +
                    decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'",
                    {},
                    {
                        query: {
                            method: 'GET',
                            headers: { "Accept": "application/json; odata=verbose" },
                            params: {
                                '$select': constants.HYPERLINK_SELECT,
                                //'$expand': 'nameDetails/Id'
                            },
                        }
                    }
                );
            }
        }

        return {
            getRequest: getRequest
        };
    }]);

    //Constant Service
    angular.module('app').constant('constants', {
        APP_TITLE: 'Book Logger',
        APP_HEADTITLE: decodeURIComponent(getQueryStringParameter("AppTitle")),
        APP_LIST: decodeURIComponent(getQueryStringParameter("ListName")),
        HYPERLINK_SELECT: 'Id,hyperlink,hyperlinkTitle,hyperlinkTarget',
        //HYPERLINK_GETALL: decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")).split("#")[0] +
        //            "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('@list')/items?@target='" +
        //            decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'&@list='" +  + "'"
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
    //General -- App Resize
    function adjustFrameSize(contentHeight) {
        var senderId,
            resizeMessage = '<message senderId={Sender_ID}>resize({Width}, {Height}px)</message>';

        var args = document.URL.split("?");
        if (args.length < 2) return;
        var params = args[1].split("&");
        for (var i = 0; i < params.length; i = i + 1) {
            var param = params[i].split("=");
            if (param[0].toLowerCase() == "senderid") {
                senderId = decodeURIComponent(param[1]);
                senderId = senderId.split("#")[0]; //for chrome - strip out #/viewname if present
            }
        }

        var step = 30, finalHeight;
        finalHeight = (step - (contentHeight % step)) + contentHeight;

        resizeMessage = resizeMessage.replace("{Sender_ID}", senderId);
        resizeMessage = resizeMessage.replace("{Height}", finalHeight);
        resizeMessage = resizeMessage.replace("{Width}", "100%");
        console.log(resizeMessage);
        window.parent.postMessage(resizeMessage, "*");
    }

})();
