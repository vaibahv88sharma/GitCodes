(function () {

    angular.module('moduleApp')
            //.controller('homeController', function (dataService, constants, $scope) {
            //.controller('homeController',['home', 'dataService',constants,function]);
            .controller('homeController', function (dataService, constants, $scope) {

        dataService.getAllCategoryResource().then(function (data) {
            $scope.navMenu = data.d.results;

            $scope.appTitle = constants.APP_TITLE;


            // Hide site until all contents loads
            document.getElementById("initialImage").style.display = "none";
            document.getElementById("s4-bodyContainer").style.display = "block";


        });

    });

})();