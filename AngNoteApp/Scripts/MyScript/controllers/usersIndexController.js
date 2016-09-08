angular.module('NoteWrangler').controller('UsersIndexController', function (User, $scope) {	

	User.getAllResource().then(function(data){
		 $scope.users = data.d.results;
	}).finally(function(data){
		//
	});
	
 
});