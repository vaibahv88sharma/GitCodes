//angular.module('moduleApp', ['ngResource']);

//'use strict';

(function () {

    //angular.module('moduleApp', ['ngResource']);
    var app = angular.module('moduleApp', ['ngResource', 'ngRoute']);

    app.provider('home', ['constants', function (constants) {
        this.$get = function () {

            var appName = "";

            if (includeTitle) {
                appName = constants.APP_TITLE;
            }

            return {
                appName: appName,
            };
        };

        var includeTitle = false;
        this.setTitle = function (value) {
            includeTitle = value;
        };
    }]);

    app.config(['homeProvider', 'constants', '$routeProvider', function (homeProvider, constants, $routeProvider) {

				    homeProvider.setTitle(true);

				}]);


})();
