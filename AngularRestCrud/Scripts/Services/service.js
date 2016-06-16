(function (app) {
app.service('spsservice',  ['$http', '$q',function ($http, $q) {

    //Public Variables: Begin
    var context = SP.ClientContext.get_current();
    var user = context.get_web().get_currentUser();
    var hostWebUrl = decodeURIComponent(manageQueryStringParameter('SPHostUrl'));
    var appWebUrl = decodeURIComponent(manageQueryStringParameter('SPAppWebUrl'));

    var currentcontext = new SP.ClientContext.get_current();
    var hostcontext = new SP.AppContextSite(currentcontext, hostWebUrl);
    var hostweb = hostcontext.get_web();
    //Public Variables: End
    function manageQueryStringParameter(paramToRetrieve) {
        var params =
        document.URL.split("?")[1].split("&");
        var strParams = "";
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0] == paramToRetrieve) {
                return singleParam[1];
            }
        }
    }

    //Get the courses
    this.getDetails = function (listTitle, items) {
        var dfd = $q.defer();
		//    courseListService.getCourses(appWebUrl + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('JobDetailsLookup')/items?$select=ID,Title,JobAddress&@target='" + hostWebUrl + "'").then(

        var fullUrl = appWebUrl + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + listTitle + "')/items?" + items +
                "@target='" + hostWebUrl + "'";
        debugger;				
        $.ajax({
            url: fullUrl,
            type: "GET",
            headers: { "Accept": "application/json; odata=verbose", "content-type": "application/json;odata=verbose" },
            success: function (data) {
                dfd.resolve(data.d.results);
            },
            error: function (xhr) {
                dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
            }
        });

        return dfd.promise;
    }
    
	    //Create a course
    this.addCourse = function (listTitle, course) {
        var dfd = $q.defer();
        //var fullUrl = _spPageContextInfo.webAbsoluteUrl + &quot;/_api/web/lists/getbytitle('&quot; + listTitle + &quot;')/items&quot;;
        var fullUrl = appWebUrl + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + listTitle + "')/items?" +
	            "@target='" + hostWebUrl + "'";
        var itemType = "SP.Data." + listTitle + "ListItem";

        var cat = {
            "__metadata": { "type": itemType },
            "Title": course.title,
            "Location": course.location
        };

        $.ajax({
            url: fullUrl,
            type: "POST",
            contentType: "application/json;odata=verbose",
            data: JSON.stringify(cat),
            headers: {
                "Accept": "application/json;odata=verbose", // return data format
                "X-RequestDigest": $("#__REQUESTDIGEST").val()
            },
            success: function (data) {
                //resolve the new data
                dfd.resolve(data.d);
            },
            error: function (xhr) {
                dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
            }
        });

        return dfd.promise;
    }
	
}]);
}(angular.module('spsmodule')));