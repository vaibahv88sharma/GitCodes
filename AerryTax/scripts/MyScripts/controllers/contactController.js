angular.module('homeModule').controller('contactController', function ($scope, $routeParams, $location, $http, contactService) {
    //homeService.getAllResource().then(function (data) {
    //    $scope.tabs = data.tabs;
    //}).finally(function (data) {
    //    //console.log(data);
    //});
    $scope.map = { center: { latitude: -37.790437, longitude: 144.862211 }, zoom: 8 };



    $scope.submitForm = function (contact) {
        if ($scope.ContactForm.$valid) {

            contactService.postNewContactHttp(contact)
            //contactService.postNewContactResource(contact)
                .then(function (data) {
                //$scope.isSubmitting = false;
                //$location.path("/notes");
                alert("Success");
            }).finally(function (response) {
               // $scope.isSubmitting = false;
                //$location.path("/notes/" + note.Id);
                alert("finally");
            });;
            //ContactService.addContact(contact)
			//.success(function () {
			//    $scope.ContactForm.$setPristine();
			//    $scope.contact = null;
			//})
            //.error(function (data, status) {
            //    console.error('Repos error', status, data);
            //    alert(status);
            //    alert(JSON.stringify(data));
            //});
        }
    };

});