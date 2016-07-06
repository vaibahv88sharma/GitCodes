/// <reference path="../angular.js" />
myApp.controller("ProductController", function ($scope) {
    $scope.name = "Bat";
});

myApp.controller("StudentController", ['$scope', function ($scope) {
    $scope.student = {
        name: "Vaibhav",
        age: 32,
        subject: [
            "math",
            "geography"
        ]
    };
    $scope.setGrade = function (student) {
        student.grade = "A+"
    };
}]);

myApp.directive("myFirstDirective", function () {
    return {
        //template: "<div><b>Test Dir</b></div>",
        templateUrl: "/AngularFiles/Directive.html",
        replace: true,
        restrict: 'EA',
        controller: function ($scope) {
            $scope.setGrade = function (student) {
                student.grade = "B+"
            }
        }
    }
});