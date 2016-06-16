"use strict";
(function () {
    angular.module("moduleRest")
        .service("myServiceRest", ['$http', '$q',function ($http, $q) {

    //Public Variables: Begin
    var context = SP.ClientContext.get_current();
    var user = context.get_web().get_currentUser();
    var hostWebUrl = decodeURIComponent(manageQueryStringParameter('SPHostUrl'));
    var appWebUrl = decodeURIComponent(manageQueryStringParameter('SPAppWebUrl'));
    var appWebUrlNew = appWebUrl.split("#")[0];
    var currentcontext = new SP.ClientContext.get_current();
    var hostcontext = new SP.AppContextSite(currentcontext, hostWebUrl);
    var hostweb = hostcontext.get_web();
    //Public Variables: End
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
    //Get the courses
    this.getDetails = function (listTitle, items) {
		debugger;
        var dfd = $q.defer();
        //var appWebUrlNew = appWebUrl.split("#")[0];
        var fullUrl = appWebUrlNew + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + listTitle + "')/items?" + items +
                "@target='" + hostWebUrl + "'";			
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
	// Get-Set Job Items
	this.Items = [];
	this.clear = function() {
		 this.Items = {
	         jobName:"",
	         supervisorName: "",
	         taskName:""
	     };
	}	
	this.setItems = function (items) {
		//debugger;
		alert(items.jobName.id +"--"+items.supervisorName.siteSupervisorId);
		 this.Items = {
			 jobId:items.jobName.id,
	         jobName:items.jobName.jobAddress,
	         supervisorName: items.supervisorName.siteSupervisorName,
			 supervisorId: items.supervisorName.siteSupervisorId,
	         taskName:items.taskName.taskCategoryName,
			 
	     };
    }
	//Create jobs
	    this.addjobs = function (listTitle) {
		//debugger;	
        var dfd = $q.defer();
		var listCreationInfo = new SP.ListCreationInformation();
		listCreationInfo.set_title(listTitle.jobName);
		listCreationInfo.set_templateType(SP.ListTemplateType.tasksWithTimelineAndHierarchy);
		var lists = hostweb.get_lists();
		var newList = lists.add(listCreationInfo);
		currentcontext.load(newList);
		currentcontext.executeQueryAsync(
				function (data) {
					alert("start view");
					var oList = hostweb.get_lists().getByTitle(listTitle.jobName);
					var viewCollection = oList.get_views();
					currentcontext.load(viewCollection);
					var createView = new SP.ViewCreationInformation();
					createView.set_title("TasksCreated");
					var viewFields = ["Checkmark", "PercentComplete", "Title", "DueDate", "AssignedTo", "Body", "Priority", "Author"];
					createView.set_viewFields(viewFields);
					createView.set_rowLimit(30);
					createView.set_viewTypeKind(1); //0, 1, 2048, 524288, 8193, 131072, 67108864  
					createView.set_setAsDefaultView(true);
					viewCollection.add(createView);
					currentcontext.load(viewCollection);
					currentcontext.executeQueryAsync(
						function (data) {
								debugger;
							   var fullUrl = appWebUrlNew + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('JobDetailsLookup')/items(" + listTitle.jobId + ")?"+
												"@target='" + hostWebUrl + "'";	
												//debugger;
							    $.ajax({
						        url: fullUrl ,//_spPageContextInfo.webAbsoluteUrl + url,
						        type: "DELETE",
						        headers: {
								    "ACCEPT": "application/json;odata=verbose",
								    "content-type": "application/json;odata=verbose",
								    "X-RequestDigest": $("#__REQUESTDIGEST").val(),
								    "IF-MATCH": "*",
								    "X-HTTP-Method": "DELETE"									
						        },
						        success: function (data) {
									//debugger;
									alert("success delete");
						            dfd.resolve(data.d);
						        },
						        error: function (xhr) {
									//debugger;
									alert("failed delete");
						            dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
						        }
						    });									
						}, 
						function (xhr) {
							alert("failed view");
							dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
						}
					);										
				}, 
				function (xhr) {
					alert("failed list");
					dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
				}		
		);
		
        return dfd.promise;
    };
/*    this.addjobs = function (listTitle) {
		debugger;	
        var dfd = $q.defer();
		
       // var fullUrl = appWebUrlNew + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + listTitle + "')/items?" + items +
            //    "@target='" + hostWebUrl + "'";
		var fullUrl =appWebUrlNew + "/_api/SP.AppContextSite(@target)/web/lists?@target='" + hostWebUrl + "'";
		var data =	{ 
					//'__metadata': { 'type': 'SP.List' }, 'AllowContentTypes': true, 'BaseTemplate': 100, 'ContentTypesEnabled': true, 'Description': 'My New List', 'Title': ' +listTitle.jobName+ '
						__metadata: { 'type': 'SP.List' },
						AllowContentTypes: true,
						BaseTemplate: 100,
						ContentTypesEnabled: true,
						Description: 'My New List',
						Title: 'NewList1'//listTitle.jobName 
					};					
        $.ajax({
            url: fullUrl,
            type: "POST",
            //body: "{ '__metadata': { 'type': 'SP.List' }, 'AllowContentTypes': true, 'BaseTemplate': 100, 'ContentTypesEnabled': true, 'Description': 'My New List', 'Title': '"+listTitle.jobName+"' }",
	        headers: { "content-type": "application/json; odata=verbose" },
			data: JSON.stringify(data),
            success: function (data) {
                dfd.resolve(data.d.results);
            },
            error: function (xhr) {
                dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
            }
        });				
	    var hostweburl = decodeURIComponent(manageQueryStringParameter("SPHostUrl"));
	    var appweburl = decodeURIComponent(manageQueryStringParameter("SPAppWebUrl"));	
		var appWebUrlNew1 = appweburl.split("#")[0];
		var scriptbase = hostweburl + "/_layouts/15/";
		
	    var executor;
	    executor = new SP.RequestExecutor(appWebUrlNew1);
	    executor.executeAsync({
	        url: appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists?@target='" + hostweburl + "'",
	        method: "POST",
	        body: "{ '__metadata': { 'type': 'SP.List' }, 'AllowContentTypes': true, 'BaseTemplate': 100, 'ContentTypesEnabled': true, 'Description': 'My New List', 'Title': '"+listTitle.jobName+"' }",
	        headers: { "content-type": "application/json; odata=verbose" },
	        success: function (data) {
			            dfd.resolve(data.d.results);
			        },
	        error: function (xhr) {
			            dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
			        }
	    });
//$.getScript(scriptbase + "SP.RequestExecutor.js", execCrossDomainRequest);
        return dfd.promise;
    };*/
	//Delete Job Address
   this.deleteJob = function (listTitle) {
		debugger;	
        var dfd = $q.defer();
	    var hostweburl = decodeURIComponent(manageQueryStringParameter("SPHostUrl"));
	    var appweburl = decodeURIComponent(manageQueryStringParameter("SPAppWebUrl"));	
		var appWebUrlNew1 = appweburl.split("#")[0];
		var scriptbase = hostweburl + "/_layouts/15/";

	   var fullUrl = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('JobDetailsLookup')/items(" + listTitle.jobId + ")?"+
						"@target='" + hostweburl + "'";	
						//debugger;
	    $.ajax({
	       url: fullUrl ,//_spPageContextInfo.webAbsoluteUrl + url,
	      type: "DELETE",
	       headers: {
		    "ACCEPT": "application/json;odata=verbose",
		    "content-type": "application/json;odata=verbose",
		    "X-RequestDigest": $("#__REQUESTDIGEST").val(),
		    "IF-MATCH": "*",
		    "X-HTTP-Method": "DELETE"									
	       },
	       success: function (data) {
			//debugger;
				alert("success delete");
		          dfd.resolve(data.d);
	        },
	       error: function (xhr) {
			//debugger;
				alert("failed delete");
	           dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
	        }
	    });		
		
        return dfd.promise;
    };	
	
	
 }]);
})();