angular.module('NoteWrangler').controller('NotesEditController', function (Note, $scope, $routeParams,$location) {

	$scope.isSubmitting = false;
	

	Note.getSelectiveRequest($routeParams.id)
				.then(function (response) {
				    //$scope.note = response.d;
				});	
				
	Note.getSelResource($routeParams.id).then(function(data){
		 $scope.note = data.d;
	});				

	$scope.saveNote=function(note){
		$scope.isSubmitting = true;
/*		Note.updateRequest(note,$routeParams.id)
					.finally(function (response) {
				$scope.isSubmitting = false;
				$location.path("/notes/"+note.Id);
			});	*/
			
		Note.postSelResource(note, $routeParams.id).then(function(data){
				$scope.isSubmitting = false;
				$location.path("/notes/"+note.Id);
		});			
							
	};
	

});

var dataUpdate = "";