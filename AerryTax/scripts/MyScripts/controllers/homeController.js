/*noteWrangler.controller('NotesCreateController', function ($scope) {});*/
angular.module('homeModule').controller('homeController', function ($scope, $routeParams, $location, $http, homeService) {
    homeService.getAllResource().then(function (data) {
        $scope.tabs = data.tabs;
    }).finally(function (data) {
        //console.log(data);
    });
	
	
	$scope.submitForm = function (contact) {
	    if ($scope.ContactForm.$valid) {

	        homeService.postEmailContactHttp(contact)
                .then(function (data) {
                    //$scope.isSubmitting = false;
                    //$location.path("/notes");
                    //alert("Success");
                }).finally(function (response) {
                    // $scope.isSubmitting = false;
                    //$location.path("/notes/" + note.Id);
                    //alert("finally");
                });
	    }
	};	
	

});