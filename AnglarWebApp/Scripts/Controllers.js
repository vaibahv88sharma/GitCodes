myapp.config(['$routeProvider', function ($routeProvider) {
    $routeProvider.
    when('/Items', {
        templateUrl: '../Pages/views/view-list.html',
        controller: 'listController'
    }).
    when('/Items/add', {
        templateUrl: '../Pages/views/view-detail.html',
        controller: 'addController'
    }).
    when('/Items/:index', {
        templateUrl: '../Pages/views/view-detail.html',
        controller: 'editController'
    }).
    otherwise({
        redirectTo: '/Items'
    });
}
]);

myapp.factory("musicService", ["$rootScope", function ($rootScope) {
    var svc = {};
    var data = [
    { name: "Vaibhav", genre: "drama", rating: 4 },
    { name: "Ajay", genre: "crime", rating: 5 },
    { name: "Vaibhav1", genre: "drama1", rating: 41 },
    { name: "Vaibhav2", genre: "drama2", rating: 42 },
    { name: "Vaibhav3", genre: "drama3", rating: 43 },
    { name: "Vijay", genre: "thriller", rating: 3 }
    ];
    svc.getArtists = function(){
        return data;
    };
    svc.addArtists = function (artist) {
        data.push(artist);
    };
    svc.editArtists = function(index, artist){
        alert("hi edit");
        data[index] = artist;
    };
    return svc;
}]);

myapp.controller("listController", ["$scope", "$location", "$routeParams", "musicService", function ($scope, $location, $routeParams, musicService) {

    $scope.data = musicService.getArtists();
    $scope.addArtist = function () {
        $location.path("/Items/add");
    };
    $scope.editItem = function (index) {
        $location.path("/Items/" + index);
    };

}]);

myapp.controller("addController", ["$scope", "$location", "$routeParams", "musicService", function ($scope, $location, $routeParams, musicService) {
    $scope.save = function () {
        //Save the new item
        musicService.addArtists({ name: $scope.Item.name, genre: $scope.Item.genre, rating: $scope.Item.rating });
        $location.path("/Items");
    };
    $scope.cancel = function () {
        //other funcitonality
        $location.path("/Items");
    };
}]);

myapp.controller("editController", ["$scope", "$location", "$routeParams", "musicService", function ($scope, $location, $routeParams, musicService) {
    $scope.Item = musicService.getArtists()[parseInt($routeParams.index)];
    $scope.save = function () {
        musicService.addArtists(parseInt($routeParams.index), { name: $scope.Item.name, genre: $scope.Item.genre, rating: $scope.Item.rating });
        $location.path("/Items");
    };
    $scope.cancel = function () {
        //other funcitonality
        $location.path("/Items");
    };
}]);