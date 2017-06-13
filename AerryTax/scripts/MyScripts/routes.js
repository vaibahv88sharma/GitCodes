angular.module('homeModule').config(function ($routeProvider) {
$routeProvider.when('/', { redirectTo: '/home' })
.when('/home', { templateUrl: "template/view/home.html", controller: "homeController" })
.when('/contact', {templateUrl: "template/view/contact.html",controller: "contactController"})
.when('/about', {templateUrl: "template/view/about.html",controller: "aboutController"})
.when('/disclaimer', {templateUrl: "template/view/disclaimer.html",controller: "disclaimerController"})
.when('/taxation/:id', { templateUrl: "template/view/taxation.html", controller: "taxationController" })
});