angular.module('NoteWrangler').directive('nwPageNav', function () {
	return {
		replace: true,
		restrict: "E",
		templateUrl: "/sites/PublishingSite/_catalogs/masterpage/CstmMstrPage/AngBootMaster/HTML/Templates/directives/nwPageNav.html",
		controller:function($scope, $location){
			$scope.isPage = function(name){
				return new RegExp("/"+name+"($|/)").test($location.path());
			};
		}
	};
});