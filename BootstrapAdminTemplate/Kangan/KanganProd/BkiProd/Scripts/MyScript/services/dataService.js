(function () {

    angular.module('moduleApp').factory("dataService", ["$http", "$q", "$resource", function ($http, $q, $resource) {

        function getMenuResource(id) {
            if (id) {
                return $resource(
                    "/_api/Web/Lists/GetByTitle('SiteNav')/Items(':id')",
                    { id: id },
                    {
                        get: {
                            method: 'GET',
                            headers: { "Accept": "application/json; odata=verbose" },
                            params: {
                                '$select': 'Id,NavMenu,Class,pageUrl,idNo,navTitle',
                                /*								'$select': 'Id,Title,Description,userName,categoryName,content,Author/Name,Author/Title',
                                                                '$expand': 'Author/Id'*/
                            },
                        },
                        post: {
                            method: "POST",
                            headers: {
                                "Accept": "application/json; odata=verbose",
                                "content-type": "application/json; odata=verbose",
                                "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                                "X-HTTP-Method": "MERGE",
                                "IF-MATCH": "*"
                            }
                        },
                        delete: {
                            method: "POST",
                            headers: {
                                "Accept": "application/json; odata=verbose",
                                "content-type": "application/json; odata=verbose",
                                "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                                "X-HTTP-Method": "DELETE",
                                "IF-MATCH": "*"
                            }
                        }
                    }
                );
            }
            else {
                return $resource(
                    "/_api/Web/Lists/GetByTitle('SiteNav')/Items",
                    {},
                    {
                        query: {
                            method: 'GET', isArray: false,
                            headers: { "Accept": "application/json; odata=verbose" },
                            params: {
                                '$select': 'Id,NavMenu,Class,pageUrl,idNo,navTitle',
                                //'$select': 'Id,Title,Description,categoryId,userId,content,user/Name,user/Title,category/category',
                                //'$expand': 'user/Id,category'
                            },
                        },
                        post: {
                            method: "POST",
                            headers: {
                                "accept": "application/json;odata=verbose",
                                //"X-RequestDigest": $("#__REQUESTDIGEST").val(),
                                "content-type": "application/json;odata=verbose"
                            }
                        }
                    }
                );
            }
        }

        function getAllCategoryResource() {
            var resource = getMenuResource();
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
            getAllCategoryResource: getAllCategoryResource
        };
    }]);

    angular.module('moduleApp')
		.constant('constants', {

		    APP_TITLE: 'Bendigo Kangan Intranet'

		});


})();