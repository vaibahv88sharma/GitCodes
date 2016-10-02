angular.module('homeModule').config(function ($routeProvider) {
	$routeProvider
		.when('/',{
			redirectTo:'/home'
		})
		.when('/home', {
		    templateUrl: "template/view/home.html",
		    controller: "homeController"
		})			
});