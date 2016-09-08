angular.module('NoteWrangler').controller('NotesShowController', function (Note, $scope, $routeParams,$location) {

	Note.getSelResource($routeParams.id).then(function(data){
		 $scope.note = data.d;
	});

	Note.getSelectiveRequest($routeParams.id)
				.then(function (response) {					
				    //$scope.note = response.d;
				});	
 
	$scope.deleteNote = function(note){
/*		Note.deleteSelectiveRequest(note,$routeParams.id)
					.then(function (response) {					
					    //$scope.note = response.d;
				});*/
		Note.delSelResource($routeParams.id)
					.then(function (response) {	
						$location.path("/notes");
					});
	}
 
});