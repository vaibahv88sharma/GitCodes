/*noteWrangler.controller('NotesCreateController', function ($scope) {});*/
angular.module('homeModule').controller('homeController', function ($scope, $routeParams, $location, $http, homeService) {
    homeService.getAllResource().then(function (data) {
        $scope.tabs = data.tabs;
    }).finally(function (data) {
        //console.log(data);
    });

});