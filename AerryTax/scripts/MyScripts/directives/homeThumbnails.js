angular.module('homeModule').directive('homeThumbnails', function () {
    return {
        replace: true,
        restrict: "E",
        scope:false,
        templateUrl: "template/directive/homeThumbnails.html",

    };
});