myapp.config(['$routeProvider', function($routeProvider) {
        $routeProvider.
        when('/addStudent', {
            templateUrl: 'addStudent.html',
            controller: 'AddStudentController'
        }).
        when('/viewStudents', {
            templateUrl: 'viewStudents.html',
            controller: 'ViewStudentsController'
        }).
        otherwise({
            redirectTo: '/addStudent'
        });
    } 
]);


myapp.controller("HelloController", function ($scope) {
    $scope.title = "AngularJS";
    $scope.moneyAmount = 23;
    $scope.collectiveData = [{ name: 'Vishal', city: 'Jalandhar' }, { name: 'Naveen', city: 'Patna' }, { name: 'Peter', city: 'New York'}];
    $scope.marks = [80, 90, 75, 73, 60];
    $scope.data = { firstname: 'Mahesh', lastname: 'Parashar', rollno: 101 }
    $scope.fName = "John";
    $scope.lName = "Doe";
    $scope.firstName1 = "Vaibhav";
    $scope.lastName1 = "Sharma";
    $scope.email1 = "vaibhav.s.sharma@accenture.com";

    $scope.fullName = function () {
        return $scope.fName + ' ' + $scope.lName;
    }
    $scope.reset = function () {
        $scope.firstName1 = "Vbhv";
        $scope.lastName1 = "Shrma";
        $scope.email1 = "v.s@accenture.com";
    }
});


// AJAX
myapp.controller("HelloController_2", function ($scope, $http) {
    $scope.title = "AngularJS Text";
    var url = "/Files/data.txt";
    $http.get(url).success(function (response) {
        $scope.students = response;
    });
});

// Views Controller : Begin
myapp.controller('AddStudentController', function ($scope) {
    $scope.message = "This page will be used to display add student form";
});


myapp.controller('ViewStudentsController', function ($scope) {
    $scope.message = "This page will be used to display all the students";
});
// Views Controller : End

// Factory and Service : Begin
myapp.value("defaultInput", 5);

myapp.factory('MathService', function () {
    var factory = {};
    factory.multiply = function (a, b) {
        return a * b;
    }
    return factory;
});

myapp.service('CalcService', function (MathService) {
    this.square = function (a) {
        return MathService.multiply(a , a);
    }
});

myapp.controller('CalcController', function ($scope, CalcService) {
    $scope.square = function () {
        $scope.result = CalcService.square($scope.number);
    }
});

myapp.controller('CalcController_2', function ($scope, CalcService, defaultInput) {
    $scope.number = defaultInput;
    $scope.result = CalcService.square($scope.number);
    $scope.square = function () {
        $scope.result = CalcService.square($scope.number);
    }
});
// Factory and Service : End


//Provider: Begin
myapp.config(function ($provide) {
    $provide.provider("game1", function () {
        return {
            $get: function () {
                return {
                    title: "StarCraft1"
                };
            }
        };
    });
    $provide.provider("game2", function () {
        return {
            $get: function () {
                return {
                    title: "StarCraft2"
                };
            }
        };
    });
});

myapp.controller("ProviderController", function ($scope, game1, game2) {
    $scope.titleMathService1 = game1.title;
    $scope.titleMathService2 = game2.title;
});
//Provider: End

// $rootscope and $scope : Begin
myapp.controller("RootScopeTest1", function ($scope, $rootScope) {
    $scope.title = "AngularJS";
    $rootScope.moneyAmount = 26;
});

myapp.controller("RootScopeTest2", function ($scope, $rootScope) {
    $scope.title = "AngularJS by Google";
    $scope.moneyAmount = $rootScope.moneyAmount;
});

// $rootscope and $scope : End

// Custom Directive : Begin
myapp.directive('student', function () {
    var directive = {};
    directive.restrict = 'E';
    directive.template = "Student: <b>{{student.name}}</b> , Roll No: <b>{{student.rollno}}</b>";
    directive.scope = {
        student : "=name"
    }
    directive.compile = function(element, attributes) {
        element.css("border", "1px solid #cccccc");
        var linkFunction = function($scope, element, attributes) {
            element.html("Student: <b>"+$scope.student.name +"</b> , Roll No: <b>"+$scope.student.rollno+"</b><br/>");
            element.css("background-color", "#ff00ff");
        }
        return linkFunction;
    }
    return directive;
});
myapp.controller('StudentController', function ($scope) {
    $scope.Mahesh = {};
    $scope.Mahesh.name = "Mahesh Parashar";
    $scope.Mahesh.rollno = 1;
    $scope.Piyush = {};
    $scope.Piyush.name = "Piyush Parashar";
    $scope.Piyush.rollno = 2;
});
// Custom Directive : End

myapp.controller('AppController', function ($scope) {
    $scope.fees = 100;
    $scope.admissiondate = new Date();
    $scope.rollno = 123.45;
});