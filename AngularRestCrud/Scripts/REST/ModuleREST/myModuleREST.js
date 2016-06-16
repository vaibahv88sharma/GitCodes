/*var moduleRest = angular.module("moduleRest", ["ngRoute"]);
moduleRest.config( function ($routeProvider) {
    $routeProvider.
    when('/Items', {
        templateUrl: '../views/REST/view-listREST.html',
        controller: 'controllerRest'
    }).
    when('/Items/add', {
        templateUrl: '../views/REST/view-detailREST.html',
        controller: 'addControllerRest'
    }).
//    when('/Items/:index', {
    //    templateUrl: '../views/REST/view-detailREST.html',
  //      controller: 'addControllerRest'
 //   }).
    otherwise({
        redirectTo: '/Items'
    });
}
);*/
"use strict";
(function () {
    angular.module("moduleRest", ["ngRoute"])
		.config(["$routeProvider", function ($routeProvider) {
		    $routeProvider.when("/Items", {
		        templateUrl: "../views/REST/view-listREST.html",
		        controller: "controllerRest"
		    }).when("/Items/add", {
		        templateUrl: "../views/REST/view-detailREST.html",
		        controller: "addControllerRest"
		    }).otherwise({
		        redirectTo: '/Items'
		    });
		}]);
})();