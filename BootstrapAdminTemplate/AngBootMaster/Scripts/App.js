'use strict';



(function () {

    // This code runs when the DOM is ready and creates a context object which is 
    // needed to use the SharePoint object model
    $(document).ready(function () {
    });


})();


//////////////////// Custom

 //function manageQueryStringParameter(paramToRetrieve) {
 //    var params =
 //    document.URL.split("?")[1].split("&");
 //    //var strParams = "";
 //    for (var i = 0; i < params.length; i = i + 1) {
 //        var singleParam = params[i].split("=");
 //        if (singleParam[0] == paramToRetrieve) {
 //            return singleParam[1];
 //        }
 //    }
 //}

function getItemTypeForListName (name) {
    return "SP.Data." + name.charAt(0).toUpperCase() + name.split(" ").join("").slice(1) + "ListItem";
}

//var hostweburl = decodeURIComponent(manageQueryStringParameter("SPHostUrl"));
//var appweburl = decodeURIComponent(manageQueryStringParameter("SPAppWebUrl"));	

var hostweburl ="";
var appweburl ="";

var appWebUrlNew1 = appweburl.split("#")[0];
//var fullUrlGet = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('NoteList')/items?"+
//                "@target='" + hostweburl + "'";		
var fullUrlGet = "/sites/PublishingSite/_api/Web/Lists/GetByTitle('NoteList')/Items";
var itemType = getItemTypeForListName('NoteList');
var data1 = {
//var dataMeta = {
    "__metadata": { "type": itemType }
};

var formDigest =  $('#__REQUESTDIGEST').val();

var fullUrlGetUserDetails = "/sites/PublishingSite/_api/Web/Lists/GetByTitle('UserDetails')/Items";	
//var fullUrlGetUserDetails = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('UserDetails')/items?"+
//                "@target='" + hostweburl + "'";	

var fullUrlGetCategoryDetails = "/sites/PublishingSite/_api/Web/Lists/GetByTitle('category')/Items";
//var fullUrlGetCategoryDetails = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('category')/items?"+
//                "@target='" + hostweburl + "'";