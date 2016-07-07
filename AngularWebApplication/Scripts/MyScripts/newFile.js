/// <reference path="../angular.js" />
myApp.controller("ProductControllerOld", function ($scope) {
    $scope.name = "Bat";
});

myApp.controller("ProductController", function ($scope) {
    $scope.product1 = {
        name: 'Phone',
        price: 100,
        stock: true
    };
    $scope.product2 = {
        name: 'TV',
        price: 1000,
        stock: false
    };
    $scope.product3 = {
        name: 'Laptop',
        price: 800,
        stock: false
    };
    $scope.ShowData = function () {
        alert("Display Data");
    }
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

myApp.directive("myFirstDirectiveOld1", function () {
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
myApp.directive("inventoryProductOld", function () {
    return {        
        restrict: 'E',
        scope: false,
        template: '<div>{{product1.name}} costs {{product1.price}} $</div><div><button class="btn btn-lg btn-success" ng-click="name=\'Bat\'">Change Name</button></div>'
    }
});
myApp.directive("inventoryProduct", function () {
    return {
        restrict: 'E',
        scope: {
            name: '@',
            price: '@'
        },
        template: '<div><h2>{{name}} costs {{price}} $</h2></div><div><a class="btn btn-lg btn-success" ng-click="name=\'Bat\'">Change Name</a></div>'        
    }
});
myApp.directive("inventoryProductNew", function () {
    return {
        restrict: 'E',
        scope: {
            data:'='
        },
        template: '<div><h2>{{data.name}} costs {{data.price}} $</h2></div><div><a class="btn btn-lg btn-success" ng-click="data.name=\'IG\'">Change Name</a></div>'
    }
});