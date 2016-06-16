app.controller("listController", ["$scope", "$location", "$routeParams", "musicService", function ($scope, $location, $routeParams, musicService) {
    $scope.data = musicService.getArtists();
    $scope.addArtist = function () {
        $location.path("/Items/add");	
    };
    $scope.editItem = function (index) {
        $location.path("/Items/" + index);
    };
}]);

app.controller("addController", ["$scope", "$location", "$routeParams", "musicService", function ($scope, $location, $routeParams, musicService) {
    $scope.save = function () {
        //Save the new item
		//alert($scope.Item.name);
        musicService.addArtists({ name: $scope.Item.name, genre: $scope.Item.genre, rating: $scope.Item.rating });
        $location.path("/Items");
    };
    $scope.cancel = function () {
        //other funcitonality
        $location.path("/Items");
    };
}]);

app.controller("editController", ["$scope", "$location", "$routeParams", "musicService",  function ($scope, $location, $routeParams, musicService) {
	$scope.Item = musicService.getArtists()[parseInt($routeParams.index)];
    $scope.save = function () {
        musicService.editArtists(parseInt($routeParams.index), { name: $scope.Item.name, genre: $scope.Item.genre, rating: $scope.Item.rating });
        $location.path("/Items");
    };
    $scope.cancel = function () {
        //other funcitonality
        $location.path("/Items");
    };
}]);