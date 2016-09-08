angular.module('NoteWrangler').controller('UsersShowController', function (User, $scope, $routeParams) {
 
	User.getSelUserResource($routeParams.id).then(function(data){
		 $scope.user = data.d;
	}).finally(function(data){
		//
	});

});