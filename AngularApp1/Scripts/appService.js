app.factory('ContactService', function ($http) {
	
    var factory = {};
	
    factory.addContact = function (objContact) {				
		var data = $.extend({}, data1, objContact);	
		return $http({
                url: fullUrlPost ,
                method: "POST",
                processData: false,
                data: JSON.stringify(data),
                transformRequest: angular.identity,
                headers: {
                    "accept": "application/json;odata=verbose",
                    "X-RequestDigest":  $("#__REQUESTDIGEST").val(),
                    "content-type": "application/json;odata=verbose"
                }
		});		      
    };
		
    factory.delContact = function (id) {				
			var fullUrlDelete = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('ContactList')/items("+id+")?"+
			                "@target='" + hostweburl + "'";	

		return $http({
	                url: fullUrlDelete,
			        method: "POST",
					body: data1 ,
					headers: {
						"Accept": "application/json; odata=verbose",
						"content-type": "application/json; odata=verbose",
						"X-RequestDigest":  $("#__REQUESTDIGEST").val(),
						//"content-length": data1.length,
						"X-HTTP-Method": "DELETE",
						"IF-MATCH": "*"
					}		
				});		      
    };	
		
    factory.editContact = function (contact, id) {				
		var fullUrlUpdate = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('ContactList')/items("+id+")?"+
			                "@target='" + hostweburl + "'";	
		var dataUpdate = $.extend({}, data1, contact);	
		return $http({
	                url: fullUrlUpdate,
			        method: "POST",
	                processData: false,
	                data: JSON.stringify(dataUpdate),
	                transformRequest: angular.identity,					
					headers: {
						"Accept": "application/json; odata=verbose",
						"content-type": "application/json; odata=verbose",
						"X-RequestDigest":  $("#__REQUESTDIGEST").val(),
						"content-length": dataUpdate.length,
						"X-HTTP-Method": "MERGE",
						"IF-MATCH": "*"
					}		
				});		      
    };		
	
    factory.getContact = function (objContact) {
		return $http({  
            method: 'GET',  
            url: fullUrlPost, 
            headers: { "Accept": "application/json;odata=verbose" }  
        });      
    };

    factory.getSelContact = function (selContactId) {
		var fullUrlSelGet = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('ContactList')/items?$filter=Id eq "+selContactId+"&"+
			                "@target='" + hostweburl + "'";
		return $http({  
            method: 'GET',  
            url: fullUrlSelGet, 
            headers: { "Accept": "application/json;odata=verbose" }
        });      
    };
		
    return factory;
});
