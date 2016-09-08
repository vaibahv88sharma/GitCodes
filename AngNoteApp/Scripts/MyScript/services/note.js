//angular.module('NoteWrangler').factory('Note', function ($resource) {
angular.module('NoteWrangler').factory("Note", ["$http", "$q","$resource", function ($http, $q,$resource) {

// Begin Region : $HTTP Call	
            var getRequest = function (query) {
                var deferred = $q.defer();
                $http({
                    url: fullUrlGet,//baseUrl + query,
                    method: "GET",
                    headers: {
                        "accept": "application/json;odata=verbose",
                        "content-Type": "application/json;odata=verbose"
                    }
                })
                    .success(function (result) {
                        deferred.resolve(result);
                    })
                    .error(function (result, status) {
                        deferred.reject(status);
                    });
                return deferred.promise;
            };	

            var getSelectiveRequest = function (id) {
				var fullUrlSelective = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('NoteList')/items('"+id+"')?"+
										"$select=Id,Title,Description,userName,categoryName,content&@target='" + hostweburl + "'";					
                var deferred = $q.defer();
                $http({
                    url: fullUrlSelective,
                    method: "GET",
                    headers: {
                        "accept": "application/json;odata=verbose",
                        "content-Type": "application/json;odata=verbose"
                    }
                })
                    .success(function (result) {
                        deferred.resolve(result);
                    })
                    .error(function (result, status) {
                        deferred.reject(status);
                    });
                return deferred.promise;
            };	

            var updateRequest = function (data,id) {
				var fullUrlSelective = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('NoteList')/items('"+id+"')?"+
										"@target='" + hostweburl + "'";		
				//var dataUpdate = $.extend({}, data1, data);	
				var dataMeta = {
				    "__metadata": { "type": itemType }
				};	
				var dataUpdate = $.extend({}, dataMeta, data);
				//console.log(dataUpdate);													
                var deferred = $q.defer();
                $http({
	                url: fullUrlSelective,
			        method: "POST",
	                processData: false,
	                data: JSON.stringify(dataUpdate),
	                transformRequest: angular.identity,					
					headers: {
						"Accept": "application/json; odata=verbose",
						"content-type": "application/json; odata=verbose",
						"X-RequestDigest":  $("#__REQUESTDIGEST").val(),
						//"content-length": dataUpdate.length,
						"X-HTTP-Method": "MERGE",
						"IF-MATCH": "*"
					}	
                })
                    .success(function (result) {
                        deferred.resolve(result);
                    })
                    .error(function (result, status) {
                        deferred.reject(status);
                    });
                return deferred.promise;
            };
		
            var deleteSelectiveRequest = function (data,id) {
				var fullUrlSelective = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('NoteList')/items('"+id+"')?"+
										"@target='" + hostweburl + "'";		
				//var dataUpdate = $.extend({}, data1, data);	
				var dataMeta = {
				    "__metadata": { "type": itemType }
				};	
				var dataUpdate = $.extend({}, dataMeta, data);
				//console.log(dataUpdate);													
                var deferred = $q.defer();
                $http({
	                url: fullUrlSelective,
			        method: "POST",
	                processData: false,
	                data: JSON.stringify(dataUpdate),
	                transformRequest: angular.identity,					
					headers: {
						"Accept": "application/json; odata=verbose",
						"content-type": "application/json; odata=verbose",
						"X-RequestDigest":  $("#__REQUESTDIGEST").val(),
						//"content-length": data1.length,
						"X-HTTP-Method": "DELETE",
						"IF-MATCH": "*"
					}
                })
                    .success(function (result) {
                        deferred.resolve(result);
                    })
                    .error(function (result, status) {
                        deferred.reject(status);
                    });
                return deferred.promise;
            };
// End Region : $HTTP Call

// Begin Region : Resource All Data	
			function getAllResource(){
				var resource = getAllResourceREST();
				var deferred = $q.defer();
				
				resource.query({},
				//resource.get({},
					function(data){
						deferred.resolve(data);
					},function(error){
						deferred.reject(error);
					});				
				return  deferred.promise;
			}
			
			function postNewResource(noteData){
				var resource = getAllResourceREST();
				var deferred = $q.defer();
				
				var dataMeta = {
								    "__metadata": { "type": itemType }
								};	
				var dataUpdate = $.extend({}, dataMeta, noteData);				
				resource.post(dataUpdate,
					function(data){
						deferred.resolve(data);
					},function(error){
						console.log(error.data.error.message.value);
						console.log(error);
						deferred.reject(error);
					});
				return  deferred.promise;
			}
										
			function getAllResourceREST(){
				return $resource(fullUrlGet, 
				{}, 
				{
				    query: {
						method: 'GET', isArray: false, 
					    headers: { "Accept": "application/json; odata=verbose" },
						params: {
							'$select': 'Id,Title,Description,userName,categoryName,categoryId,userId,content,user/Name,user/Title,category/category',
							'$expand': 'user/Id,category'
						},						
				    },
				    post: {
		                method: "POST",
		                headers: {
		                    "accept": "application/json;odata=verbose",
		                    "X-RequestDigest":  $("#__REQUESTDIGEST").val(),
		                    "content-type": "application/json;odata=verbose"
		                }
				    }					
				});
			}			
// End Region : Resource All Data

// Begin Region : Resource Selective Data
			function getSelResource(id){
				var resource = gelSelResourceREST(id);
				var deferred = $q.defer();

				resource.get({},
					function(data){
						deferred.resolve(data);
					},function(error){
						deferred.reject(error);
					});
				return  deferred.promise;
			}		
				
			function postSelResource(noteData, id){
				var resource = gelSelResourceREST(id);
				var deferred = $q.defer();
				
				resource.post(noteData,
					function(data){
						deferred.resolve(data);
					},function(error){
						console.log(error.data.error.message.value);
						console.log(error);
						deferred.reject(error);
					});
				return  deferred.promise;
			}				

			function delSelResource(id){
				var resource = gelSelResourceREST(id);
				var deferred = $q.defer();
				
				resource.delete({},
					function(data){
						deferred.resolve(data);
					},function(error){
						console.log(error.data.error.message.value);
						console.log(error);
						deferred.reject(error);
					});
				return  deferred.promise;
			}		
								
			function gelSelResourceREST(id){
				var fullUrlSelective = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('NoteList')/items(':id')?"+
						    "@target='" + hostweburl + "'"
							//"&$select=Id,Title,Description,userName,categoryName,content,Author/Name,Author/Title&$expand=Author/Id";
							//debugger;	
			    return $resource(fullUrlSelective, 
				{id: id},
				{		
					get: { 
							method: 'GET',
			                headers: { "Accept": "application/json; odata=verbose" },
							params: {
								'$select': 'Id,Title,Description,userName,userId,categoryName,categoryId,content,user/Name,user/Title,category/category',
								'$expand': 'user/Id,category'								
/*								'$select': 'Id,Title,Description,userName,categoryName,content,Author/Name,Author/Title',
								'$expand': 'Author/Id'*/
							},
					},
					post : {
				        method: "POST",			
						headers: {
							"Accept": "application/json; odata=verbose",
							"content-type": "application/json; odata=verbose",
							"X-RequestDigest":  $("#__REQUESTDIGEST").val(),
							"X-HTTP-Method": "MERGE",
							"IF-MATCH": "*"
						}						
					},
					delete:{
				        method: "POST",
						headers: {
							"Accept": "application/json; odata=verbose",
							"content-type": "application/json; odata=verbose",
							"X-RequestDigest":  $("#__REQUESTDIGEST").val(),
							"X-HTTP-Method": "DELETE",
							"IF-MATCH": "*"
						}						
					}
			    });
			}			
// End Region : Resource Selective Data	
			
            return {
                getRequest : getRequest,
				getSelectiveRequest : getSelectiveRequest,
                updateRequest : updateRequest,
				deleteSelectiveRequest : deleteSelectiveRequest,
				getAllResource : getAllResource,
				postNewResource : postNewResource,
				getSelResource : getSelResource,
				postSelResource : postSelResource,
				delSelResource : delSelResource
            };		
}]);