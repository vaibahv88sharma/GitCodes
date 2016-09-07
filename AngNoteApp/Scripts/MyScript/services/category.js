angular.module('NoteWrangler').factory("Category", ["$http", "$q","$resource", function ($http, $q,$resource) {

			function getNoteResource(id){
				if(id){
					return $resource(
						appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('UserDetails')/items(':id')?"+ "@target='" + hostweburl + "'", 
						{id: id}, 
						{
						    query: {
								method: 'GET', isArray: false, 
							    headers: { "Accept": "application/json; odata=verbose" },
								params: {
									'$select': 'Id,Title,site,name,bio'
								},
						    }
						}
					);						
				}
				else{					
					return $resource(
						fullUrlGetUserDetails, 
						{}, 
						{
						    query: {
								method: 'GET',
				                headers: { "Accept": "application/json; odata=verbose" },
								params: {
									'$select': 'Id,Title,site,name,bio'
								},
						    }				
						}
					);				
				}
			}	

			function getAllNoteResource(){
				var resource = getNoteResource();
				var deferred = $q.defer();

				resource.query({},
					function(data){
						deferred.resolve(data);
					},function(error){
						deferred.reject(error);
					});				
				return  deferred.promise;
			}
			
			function getSelNoteResource(id){
				var resource = getAllNoteResource(id);
				var deferred = $q.defer();

				resource.get({},
					function(data){
						deferred.resolve(data);
					},function(error){
						deferred.reject(error);
					});
				return  deferred.promise;
			}				
			
            return {
				getAllNoteResource : getAllNoteResource,
				getSelNoteResource : getSelNoteResource
            };		
}]);