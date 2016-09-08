'use strict';

var context = SP.ClientContext.get_current();
var user = context.get_web().get_currentUser();

(function () {

    // This code runs when the DOM is ready and creates a context object which is 
    // needed to use the SharePoint object model
    $(document).ready(function () {
        getUserName();
    });

    // This function prepares, loads, and then executes a SharePoint query to get 
    // the current users information
    function getUserName() {
        context.load(user);
        context.executeQueryAsync(onGetUserNameSuccess, onGetUserNameFail);
    }

    // This function is executed if the above call is successful
    // It replaces the contents of the 'message' element with the user name
    function onGetUserNameSuccess() {
        $('#message').text('Hello ' + user.get_title());
    }

    // This function is executed if the above call fails
    function onGetUserNameFail(sender, args) {
        alert('Failed to get user name. Error:' + args.get_message());
    }

})();


//////////////////// Custom

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
	
var itemType = getItemTypeForListName('NoteList');
var data1 = {
//var dataMeta = {
    "__metadata": { "type": itemType }
};

var formDigest =  $('#__REQUESTDIGEST').val();

var fullUrlGetUserDetails = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('UserDetails')/items?"+
                "@target='" + hostweburl + "'";	

var fullUrlGetCategoryDetails = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('category')/items?"+
                "@target='" + hostweburl + "'";