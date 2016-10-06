angular.module('homeModule').factory('contactService', ["$http", "$q", "$resource", function ($http, $q, $resource) {

    function getAllContactResource(id) {
        if (id) {
            return $resource(
                //appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('UserDetails')/items(':id')?" + "@target='" + hostweburl + "'",
                "",
                { id: id },
                {
                    query: {
                        method: 'GET', //isArray: false, 
                        headers: { "Accept": "application/json; odata=verbose" },
                        params: {
                            //'$select': 'Id,Title,site,name,bio'
                            '$select': 'Id,Title,site,name,bio,email,nameDetailsId,nameDetails/Name,nameDetails/Title',
                            '$expand': 'nameDetails/Id'
                        },
                    }
                }
            );
        }
        else {
            return $resource(
                "/scripts/MyScripts/sourceContact.json",
                {},
                {
                    query: {
                        method: 'GET',
                        //params: {
                        //    '$select': 'Id,Title,site,name,bio,email,nameDetailsId,nameDetails/Name,nameDetails/Title',
                        //    '$expand': 'nameDetails/Id'
                        //},
                    },
                    post: {
                        method: "POST",
                    }
                }
            );
        }
    }

    function getAllResource() {
        var resource = getAllContactResource();
        var deferred = $q.defer();

        resource.query({},
            function (data) {
                deferred.resolve(data);
            }, function (error) {
                deferred.reject(error);
            });
        return deferred.promise;
    }

    function getSelContactResource(id) {
        var resource = getAllContactResource(id);
        var deferred = $q.defer();

        resource.query({},
            function (data) {
                deferred.resolve(data);
            }, function (error) {
                deferred.reject(error);
            });
        return deferred.promise;
    }

    function postNewContactResource(contact) {
        var resource = getAllContactResource();
        var deferred = $q.defer();

        resource.post(contact,
            function (data) {
                deferred.resolve(data);
            }, function (error) {
                console.log(error.data.error.message.value);
                console.log(error);
                deferred.reject(error);
            });
        return deferred.promise;
    }

    return {
        getAllResource: getAllResource,
        getSelContactResource: getSelContactResource,
        postNewContactResource: postNewContactResource
    };
}]);