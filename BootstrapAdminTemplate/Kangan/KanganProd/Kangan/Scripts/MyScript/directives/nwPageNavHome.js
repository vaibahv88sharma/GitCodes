angular.module('moduleApp').directive('nwPageNavHome', function () {
	return {
		replace: true,
		restrict: "E",
		templateUrl: "/_catalogs/masterpage/kangan/HTML/directives/nwPageNavHome.html",
		controller:function($scope, $location){
			//$scope.isPage = function(name){
			//	return new RegExp("/"+name+"($|/)").test($location.path());
		    //};
		    console.log();
		}
	};
});