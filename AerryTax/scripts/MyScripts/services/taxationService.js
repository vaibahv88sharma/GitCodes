angular.module('homeModule').factory('taxationService', ["$http", "$q", "$resource", function ($http, $q, $resource) {


    function getAllTaxrResource(id) {
        if (id) {
            return $resource(
                "/scripts/MyScripts/sourceTaxation.json",
                { id: id },
                {
                    query: {
                        method: 'GET',
                    }
                }
            );
        }
        else {
            return $resource(
                "/scripts/MyScripts/sourceTaxation.json",
                {},
                {
                    query: {
                        method: 'GET',
                    }
                }
            );
        }
    }

    function getAllResource() {
        var resource = getAllTaxrResource();
        var deferred = $q.defer();

        resource.query({},
            function (data) {
                deferred.resolve(data);
            }, function (error) {
                deferred.reject(error);
            });
        return deferred.promise;
    }

    function getSelTaxResource(id) {
        var resource = getAllTaxrResource(id);
        var deferred = $q.defer();

        resource.query({},
            function (data) {
                deferred.resolve(data);
            }, function (error) {
                deferred.reject(error);
            });
        return deferred.promise;
    }

    return {
        getAllResource: getAllResource,
        getSelTaxResource: getSelTaxResource
    };

}]);