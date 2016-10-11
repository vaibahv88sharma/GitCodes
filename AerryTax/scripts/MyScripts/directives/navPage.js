angular.module('homeModule').directive('navPage', function () {
    return {
        replace: true,
        restrict: "E",
        //scope:false,
        //scope: {
        //    image: "=",
        //    title: "=",
        //    description: "=",
        //},
        //require: "?ngModel",
        templateUrl: "template/directive/navPage.html",
        controller: function ($scope, $location) {
            $scope.isPage = function (name) {
                return new RegExp("/" + name + "($|/)").test($location.path());
            };
        }

    };
});