/*noteWrangler.controller('NotesIndexController', function ($scope) {});*/
angular.module('NoteWrangler').controller('NotesIndexController', function (Note, $scope) {

	Note.getRequest()
				.then(function (response) {
				    //$scope.notes = response.d.results;
				});	
				
	Note.getAllResource().then(function(data){
		 $scope.notes = data.d.results;
	});
		
	$scope.search = {};
	window.sc= $scope;	
	
});