angular.module('homeModule').directive('taxationThumbnails', function () {
    return {
        //replace: true,
        restrict: "AE",
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