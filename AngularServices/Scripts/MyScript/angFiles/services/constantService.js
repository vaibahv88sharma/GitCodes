(function () {

	angular.module('app')
		.constant('constants',{
			APP_TITLE : 'Book Logger',
			APP_DESCRIPTION : 'Track which books you read.',
			APP_VERSION : '1.0',		

			urlBooks : decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")).split("#")[0] +
						"/_api/SP.AppContextSite(@target)/web/lists/getbytitle('Books')/items?$select=ID,Title,book_id,AuthorName,YearPublished&@target='" + 
						decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'",
			urlBookPost : decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")).split("#")[0] +
						"/_api/SP.AppContextSite(@target)/web/lists/getbytitle('Books')/items?@target='" + 
						decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'",
			urlBooksById : decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")).split("#")[0] +
						"/_api/SP.AppContextSite(@target)/web/lists/getbytitle('Books')/GetItemById(@itemId)?$select=ID,Title,book_id,AuthorName,YearPublished&@target='" + 
						decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'&@itemId=",
			urlUpdateDeleteBooksById : decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")).split("#")[0] +
						"/_api/SP.AppContextSite(@target)/web/lists/getbytitle('Books')/GetItemById(@itemId)?@target='" + 
						decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'&@itemId=",
																		
			urlReader : decodeURIComponent(manageQueryStringParameter("SPAppWebUrl")).split("#")[0] +
						"/_api/SP.AppContextSite(@target)/web/lists/getbytitle('AllReaders')/items?$select=ID,reader_id,name,weeklyReadingGoal,totalMinutesRead&@target='" + 
						decodeURIComponent(manageQueryStringParameter("SPHostUrl")) + "'",
							
			dataBooks : {
					    "__metadata": { "type": getItemTypeForListName('Books') }
					},
			dataReader : {
					    "__metadata": { "type": getItemTypeForListName('AllReaders') }
					}
			
		});

/////////////////////

 function manageQueryStringParameter(paramToRetrieve) {
     var params =
     document.URL.split("?")[1].split("&");
     //var strParams = "";
     for (var i = 0; i < params.length; i = i + 1) {
         var singleParam = params[i].split("=");
         if (singleParam[0] == paramToRetrieve) {
             return singleParam[1];
         }
     }
 }

function getItemTypeForListName (name) {
    return "SP.Data." + name.charAt(0).toUpperCase() + name.split(" ").join("").slice(1) + "ListItem";
}

var hostweburl = decodeURIComponent(manageQueryStringParameter("SPHostUrl"));
var appweburl = decodeURIComponent(manageQueryStringParameter("SPAppWebUrl"));	
var appWebUrlNew1 = appweburl.split("#")[0];
var fullUrlGet = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('NoteList')/items?"+
                "@target='" + hostweburl + "'";		
var itemType = getItemTypeForListName('Books');	
var itemTypeBooks = getItemTypeForListName('Books');
var itemTypeAllReaders = getItemTypeForListName('Books');
var data1 = {
//var dataMeta = {
    "__metadata": { "type": itemType }
};

var formDigest =  $('#__REQUESTDIGEST').val();

/*var fullUrlGetUserDetails = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('UserDetails')/items?"+
                "@target='" + hostweburl + "'";	

var fullUrlGetCategoryDetails = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('category')/items?"+
                "@target='" + hostweburl + "'";*/
/////////////////////

}());