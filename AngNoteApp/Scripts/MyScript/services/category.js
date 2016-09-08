angular.module('NoteWrangler').factory("Category", ["$http", "$q","$resource", function ($http, $q,$resource) {

			function getCategoryResource(id){
				if(id){
					return $resource(
						appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('category')/items(':id')?"+ "@target='" + hostweburl + "'", 
						{id: id}, 
						{
						    query: {
								method: 'GET', isArray: false, 
							    headers: { "Accept": "application/json; odata=verbose" },
								params: {
									'$select': 'Id,Title,category'
								},
						    }
						}
					);						
				}
				else{					
					return $resource(
						fullUrlGetCategoryDetails, 
						{}, 
						{
						    query: {
								method: 'GET',
				                headers: { "Accept": "application/json; odata=verbose" },
								params: {
									'$select': 'Id,Title,category'
								},
						    }				
						}
					);				
				}
			}	

			function getAllCategoryResource(){
				var resource = getCategoryResource();
				var deferred = $q.defer();

				resource.query({},
					function(data){
						deferred.resolve(data);
					},function(error){
						deferred.reject(error);
					});				
				return  deferred.promise;
			}
			
			function getSelCategoryResource(id){
				var resource = getCategoryResource(id);
				var deferred = $q.defer();

				resource.query({},
					function(data){
						deferred.resolve(data);
					},function(error){
						deferred.reject(error);
					});
				return  deferred.promise;
			}				
			
            return {
				getAllCategoryResource : getAllCategoryResource,
				getSelCategoryResource : getSelCategoryResource
            };		
}]);