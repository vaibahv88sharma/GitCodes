var app = angular.module("myapp", ["ngRoute"]);

//app.config(['$routeProvider', function ($routeProvider) {
app.config( function ($routeProvider) {
    $routeProvider.
    when('/Items', {
        templateUrl: '../views/view-list.html',
        controller: 'listController'
    }).
    when('/Items/add', {
        templateUrl: '../views/view-detail.html',
        controller: 'addController'
    }).
    when('/Items/:index', {
        templateUrl: '../views/view-detail.html',
        controller: 'editController'
    }).
    otherwise({
        redirectTo: '/Items'
    });
}
);