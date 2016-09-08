angular.module('NoteWrangler').controller('NotesEditController', function (Note, User, Category, $scope, $routeParams,$location) {

	$scope.isSubmitting = false;
	
// Begin Region : Get Note,User,Category
	Note.getSelectiveRequest($routeParams.id)
				.then(function (response) {
				    //$scope.note = response.d;
				});	
	Note.getSelResource($routeParams.id).then(function(data){
		 $scope.note = data.d;
	});	
	
	User.getAllResource().then(function(data){
		 $scope.users = data.d.results;
	});				

	Category.getAllCategoryResource().then(function(data){
		 $scope.categories = data.d.results;
	});

// End Region : Get Note,User,Category

	$scope.saveNote=function(note){
		$scope.isSubmitting = true;
		
		if (note.user){
		delete note.user;
		}
		if (note.category){
		delete note.category;
		}
		
		Note.postSelResource(note, $routeParams.id).then(function(data){
				$scope.isSubmitting = false;
				$location.path("/notes/"+note.Id);
		});			
/*		Note.updateRequest(note,$routeParams.id)
					.finally(function (response) {
				$scope.isSubmitting = false;
				$location.path("/notes/"+note.Id);
			});	*/							
	};
	

});

var dataUpdate = "";