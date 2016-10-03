angular.module('homeModule').controller('contactController', function ($scope, $routeParams, $location, $http, homeService) {
    //homeService.getAllResource().then(function (data) {
    //    $scope.tabs = data.tabs;
    //}).finally(function (data) {
    //    //console.log(data);
    //});
    $scope.map = { center: { latitude: -37.790437, longitude: 144.862211 }, zoom: 8 };

});