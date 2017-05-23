angular.module('homeModule').factory('contactService', ["$http", "$q", "$resource", function ($http, $q, $resource) {

/* Resource */
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
                "./scripts/MyScripts/data.php",
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
                        method: 'post',
                        headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
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
                console.log(error.data);
                console.log(error);
                deferred.reject(error);
            });
        return deferred.promise;
    }
    
    function postEmailContactHttp(contact) {

        var deferred = $q.defer();
        $http({
            //////////url: "//formspree.io/vic.sharma.it@email.com",
            ////////////url: "https://vaibahv88sharma.github.io/TarunAerry/scripts/MyScripts/data.php",
            //////////method: 'POST',
            ////////////processData: false,
            //////////data: data,//JSON.stringify(data),
            ////////////data : data.serialize(),
            ////////////dataType: "jsonp",
            ////////////crossDomain: true,
            ////////////cache: false,
            ////////////transformRequest: angular.identity,
            //////////headers: {
            //////////'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8',
            //////////'Access-Control-Allow-Methods': 'POST, GET, OPTIONS',			
            //////////'Access-Control-Allow-Methods': 'GET, POST, OPTIONS',
            //////////'Access-Control-Allow-Origin': '*',
            //////////'Access-Control-Allow-Headers': 'X-Requested-With'
            //////////}
            url: "//formspree.io/vic.sharma.it@gmail.com",
            method: "POST",
            //processData: false,
            data: contact,//JSON.stringify(data),
            //transformRequest: angular.identity,
            headers: {
                "accept": "application/json;odata=verbose",
                "content-type": "application/json;odata=verbose"
            }
        })
            .success(function (result) {
                deferred.resolve(result);
            })
            .error(function (result, status) {
                deferred.reject(status);
            });
        return deferred.promise;
    }
/* HTTP */

    var postNewContactHttp = function (data) {
										
        var deferred = $q.defer();
        $http({
            url: "./scripts/MyScripts/data.php",
			//url: "https://vaibahv88sharma.github.io/TarunAerry/scripts/MyScripts/data.php",
            method: 'POST',
            processData: false,
            data: JSON.stringify(data),
			//data : data.serialize(),
            dataType    : "jsonp",
            crossDomain : true,
            cache       : false,			
            transformRequest: angular.identity,
            //headers: {
				//'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8',
				//'Access-Control-Allow-Methods': 'POST, GET, OPTIONS',			
				//'Access-Control-Allow-Methods': 'GET, POST, OPTIONS',
				//'Access-Control-Allow-Origin': '*',
				//'Access-Control-Allow-Headers': 'X-Requested-With'
            //}
        })
            .success(function (result) {
                deferred.resolve(result);
            })
            .error(function (result, status) {
                deferred.reject(status);
            });
        return deferred.promise;
    };

/* Return Method */

    return {
        getAllResource: getAllResource,
        getSelContactResource: getSelContactResource,
        postNewContactResource: postNewContactResource,
        postNewContactHttp: postNewContactHttp,
        postEmailContactHttp: postEmailContactHttp
    };
}]);