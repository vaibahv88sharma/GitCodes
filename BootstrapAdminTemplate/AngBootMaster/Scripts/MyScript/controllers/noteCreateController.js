/*noteWrangler.controller('NotesCreateController', function ($scope) {});*/
angular.module('NoteWrangler').controller('NotesCreateController', function (Note, $scope, $routeParams,$location) {

	$scope.isSubmitting = false;

	$scope.saveNote=function(note){
		$scope.isSubmitting = true;
			
		Note.postNewResource(note).then(function(data){
				$scope.isSubmitting = false;
				$location.path("/notes");
		});
	};	

});