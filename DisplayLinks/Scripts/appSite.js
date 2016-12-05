/// <reference path="_references.js" />
//'use strict';

(function () {

    //App Module
    var app = angular.module('displayLinks', ['ngResource']);

    //App Directive for Forms Data
    angular.module('displayLinks').directive('ngAppFrame', ['$timeout', '$window', function ($timeout, $window) {

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
    //App Directive for Forms Data Category
    angular.module('displayLinks').directive('nwCategorySelector', function (details) {
        return {
            //replace: true,
            restrict: "E",
            require: "?ngModel",
            templateUrl: "../HTML/nwCategorySelector.html",
            link: function (scope, element, attrs, ngModelCtrl) {
                var activeCategory = {};

                details.getCategoryData().then(function (data) {
                    scope.categories = data.d.results;
                });

                scope.isActive = function (category) {
                    return activeCategory && activeCategory.ID === category.ID;
                }

                scope.toggleCategory = function (category) {
                    if (category === activeCategory) {
                        activeCategory = {};
                    }
                    else {
                        activeCategory = category;
                    }
                    ngModelCtrl.$setViewValue(activeCategory);
                }

                ngModelCtrl.$render = function () {
                    activeCategory = ngModelCtrl.$viewState;
                }
            }
        };
    });

    // Controller
    angular.module('displayLinks').controller('HomeController', ['details', 'constants', '$q', '$log', '$scope', function (details, constants, $q, $log, $scope) {
        $scope.title = constants.APP_HEADTITLE;
        $scope.hyperlink = constants.APP_LIST;

        $scope.search = {};

        details.getRequestDetails()
            .then(function (data) {
                $scope.formsData = data.d.results;
                $log.info(data.d.results);
            })
            .catch(function (data) {
                $log.error(data);
            });

    }]);

    // Factory Service
    angular.module('displayLinks').factory("details", ["constants", "$http", "$q", "$resource", '$log', function (constants, $http, $q, $resource, $log) {

        //Forms Data
        function getRequestDetails() {
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
        function getCategoryData() {
            var resource = getCategoryDetails();
            var deferred = $q.defer();

            resource.query({},
                function (data) {
                    deferred.resolve(data);
                }, function (error) {
                    deferred.reject(error);
                });
            return deferred.promise;
        }

        function getCategoryDetails(id) {
            if (id) {
                return $resource(
                    constants.CATEGORY_URL,
                    { id: id },
                    {
                        query: {
                            method: 'GET', //isArray: false, 
                            headers: { "Accept": "application/json; odata=verbose" },
                            params: {
                                '$select': constants.CATEGORY_SELECT,
                                //'$expand': constants.HYPERLINK_EXPAND
                            },
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
                            method: 'GET',
                            headers: { "Accept": "application/json; odata=verbose" },
                            params: {
                                '$select': constants.CATEGORY_SELECT,
                                //'$expand': constants.HYPERLINK_EXPAND
                            },
                        }
                    }
                );
            }
        }
        return {
            getRequestDetails: getRequestDetails,
            getCategoryData: getCategoryData
        };
    }]);

    //Custom Filter
    angular.module('displayLinks').filter('categoryFilter', function () {
        return function (collection, category) {
            var newCollection = [];
            if (category && category.ID) {
                for (var i = 0, l = collection.length ; i < l; i++) {
                    //if (collection[i].categoryId === category.Id) {
                    if (collection[i].DepartmentCategoryId === category.Id) {
                        newCollection.push(collection[i]);
                    }
                }
                return newCollection;
            }
            else {
                return collection;
            }
        };
    });

    //Constant Service
    angular.module('displayLinks').constant('constants', {
        APP_TITLE: 'Book Logger',
        APP_HEADTITLE: decodeURIComponent(getQueryStringParameter("AppTitle")),
        APP_LIST: decodeURIComponent(getQueryStringParameter("ListName")),
        HYPERLINK_SELECT: 'ID,Title,hyperlink,hyperlinkTitle,hyperlinkTarget,DepartmentCategoryId,DepartmentCategory/DepartmentCategory,DepartmentCategory/Id',
        HYPERLINK_EXPAND: 'DepartmentCategory',
        HYPERLINK_URL : decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")) +
                        "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + decodeURIComponent(getQueryStringParameter("ListName")) + "')/items?@target='" +
                        decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'",

        CATEGORY_SELECT: 'ID,DepartmentCategory',
        CATEGORY_URL: decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")) +
                        "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + decodeURIComponent(getQueryStringParameter("CategoryListName")) + "')/items?@target='" +
                        decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'"

        
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

})();
