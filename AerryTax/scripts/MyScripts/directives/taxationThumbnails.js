angular.module('homeModule').directive('taxationThumbnails', function () {
    return {
        //replace: true,
        restrict: "E",
        scope:false,
        //scope: {
        //    image: "=",
        //    title: "=",
        //    description: "=",
        //},
        //require: "?ngModel",
        templateUrl: "template/directive/taxationThumbnails.html",



    };
});