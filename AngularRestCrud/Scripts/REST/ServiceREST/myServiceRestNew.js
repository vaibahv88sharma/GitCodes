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
	
	var workflowDefinitionId = '12642fda-acd1-4c06-938b-4494419bcf0d';
    var listGuid = 'B9675B05-D4AA-43B8-9D61-A8D739C7EAE4';  //PUT-YOUR-GUID-HERE';  //new	
	var historyList = '86E90715-0B4C-4DB6-8AD1-7A062C81ECF3'; //'Workflow History'
	var tasksList   = '756B1FA1-901E-4E2B-A770-5E0F9B986E8A'; //'TasksList Tasks'	
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
	//Create Jobs : Begin

	this.addjobs = function (listTitle) {
		debugger;	
        var dfd = $q.defer();
		var listCreationInfo = new SP.ListCreationInformation();
		listCreationInfo.set_title(listTitle.jobName);
		listCreationInfo.set_templateType(SP.ListTemplateType.tasksWithTimelineAndHierarchy);
		var lists = hostweb.get_lists();
		var newList = lists.add(listCreationInfo);
		currentcontext.load(newList);
		currentcontext.executeQueryAsync(
			function (data) {	
				dfd.resolve(data.d);
			},
			function (xhr) {
				alert("failed list");
				dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
			}		
		);		
        return dfd.promise;
    };

	this.addFieldToList = function (listTitle) {
		debugger;	
        var dfd = $q.defer();
	    var oList = hostweb.get_lists().getByTitle(listTitle.jobName);//document.getElementById("mySelect").value
	    var oField1 = oList.get_fields().addFieldAsXml('<Field DisplayName=\'Supplier\' Type=\'User\' />', true, SP.AddFieldOptions.defaultValue);
	    currentcontext.load(oField1);
	    var oField2 = oList.get_fields().addFieldAsXml('<Field DisplayName=\'SiteSupervisor\' Type=\'User\' />', true, SP.AddFieldOptions.defaultValue);	
	    currentcontext.load(oField2);	
		currentcontext.executeQueryAsync(
			function (data) {	
				dfd.resolve(data.d);
			},
			function (xhr) {
				alert("failed adding field");
				dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
			}		
		);		
        return dfd.promise;
    };
	
	this.createlistView = function (listTitle) {
		debugger;	
        var dfd = $q.defer();
		//var listTitle = document.getElementById("mySelect").value;
		var oList = hostweb.get_lists().getByTitle(listTitle.jobName);
		var viewCollection = oList.get_views();
		currentcontext.load(viewCollection);
		var createView = new SP.ViewCreationInformation();
		createView.set_title("TasksCreated");
		//var viewFields = ["PercentComplete","Title","DueDate","AssignedTo","Body","Priority","Author"];
		//var viewFields = ["Checkmark", "PercentComplete", "Title", "DueDate", "AssignedTo", "Body", "Priority", "Author"];
		var viewFields = ["Checkmark", "PercentComplete", "Title", "DueDate", "Supplier", "SiteSupervisor", "Body", "Priority"];
		createView.set_viewFields(viewFields);
		createView.set_rowLimit(30);
		createView.set_viewTypeKind(1); //0, 1, 2048, 524288, 8193, 131072, 67108864  
		createView.set_setAsDefaultView(true);
		viewCollection.add(createView);
		currentcontext.load(viewCollection);
		currentcontext.executeQueryAsync(
			function (data) {	
				dfd.resolve(data.d);
			},
			function (xhr) {
				alert("failed creating view");
				dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
			}		
		);		
        return dfd.promise;
    };	

	this.getListGuid = function (listTitle) {
		debugger;	
        var dfd = $q.defer();
		//var listTitle = document.getElementById("mySelect").value;
		var oList = hostweb.get_lists().getByTitle(listTitle.jobName);
		currentcontext.load(oList, 'Id');
		currentcontext.executeQueryAsync(
			function (data) {
				debugger;
				var listGuidVal = oList.get_id().toString().toUpperCase();	
				dfd.resolve(listGuidVal);//data.d
			},
			function (xhr) {
				alert("failed list");
				dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
			}		
		);		
        return dfd.promise;
    };

	this.associateWF = function (listTitle, listGuid1) {
		debugger;	
        var dfd = $q.defer();
        currentcontext.load(hostweb);
        // Get WorkflowServicesManager object for the specified web
        var servicesManager = SP.WorkflowServices.WorkflowServicesManager.newObject(currentcontext, hostweb);
        currentcontext.load(servicesManager);
		currentcontext.executeQueryAsync(
			function (data) {	
			//dfd.resolve(data.d);
				debugger;
	            // Creating the subscription
	            var sub = new SP.WorkflowServices.WorkflowSubscription(currentcontext);
	
	            //sub.set_name('DemoTaskListWf12132');
				sub.set_name('WF-'+ listTitle.jobName); //document.getElementById("mySelect").value
	            sub.set_enabled(true);
	            sub.set_definitionId(workflowDefinitionId);
	            sub.set_statusFieldName('WF status');
	            sub.set_eventSourceId(listGuid1);//listGuid
				
				var eventTypes = new Array();
				eventTypes.push("ItemAdded");
				eventTypes.push("ItemUpdated");
				eventTypes.push("WorkflowStart");
				sub.set_eventTypes(eventTypes);
				
	            sub.setProperty("TaskListId", tasksList);
				sub.setProperty("HistoryListId", historyList);
	            sub.setProperty("FormData", "");
	
	            // Associate the workflow with the list
				debugger;
				var getWfSub = servicesManager.getWorkflowSubscriptionService();
				getWfSub.publishSubscriptionForList(sub, listGuid1);//listGuid
			//var pubSub  = getWfSub.publishSubscriptionForList(sub, listGuid);
				currentcontext.executeQueryAsync(
					function (data) {	
						dfd.resolve(data.d);
					},
					function (xhr) {
						alert("failed list");
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
				//alert("success delete");
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
	//Create Jobs : End
/*	    this.addjobs = function (listTitle) {
		debugger;	
        var dfd = $q.defer();
		var listCreationInfo = new SP.ListCreationInformation();
		listCreationInfo.set_title(listTitle.jobName);
		listCreationInfo.set_templateType(SP.ListTemplateType.tasksWithTimelineAndHierarchy);
		var lists = hostweb.get_lists();
		var newList = lists.add(listCreationInfo);
		currentcontext.load(newList);
		currentcontext.executeQueryAsync(
				function (listTitle, data) {
					debugger;
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
						function (listTitle, data) {
							alert("Successfully created View");
							debugger;
							var oList = hostweb.get_lists().getByTitle(listTitle.jobName);
							var oField = oList.get_fields().addFieldAsXml(
								'<Field DisplayName=\'Supplier\' Type=\'User\' />', 
								true, 
								SP.AddFieldOptions.defaultValue
							);	
							currentcontext.load(oField);
							var oField1 = oList.get_fields().addFieldAsXml(
								'<Field DisplayName=\'SiteSupervisor\' Type=\'User\' />',
								true, 
								SP.AddFieldOptions.defaultValue
							);	
							currentcontext.load(oField1);		
						    currentcontext.executeQueryAsync(
								function (listTitle, data) {
									alert("Successfully added columns to task List");	
									//createlistView();
									debugger;
									var listTitle = listTitle.jobName;
									var oList = hostweb.get_lists().getByTitle(listTitle);
									var viewCollection = oList.get_views();
									currentcontext.load(viewCollection);
									var createView = new SP.ViewCreationInformation();
									createView.set_title("TasksCreated");
									//var viewFields = ["PercentComplete","Title","DueDate","AssignedTo","Body","Priority","Author"];
									//var viewFields = ["Checkmark", "PercentComplete", "Title", "DueDate", "AssignedTo", "Body", "Priority", "Author"];
									var viewFields = ["Checkmark", "PercentComplete", "Title", "DueDate", "Supplier", "SiteSupervisor", "Body", "Priority"];
									createView.set_viewFields(viewFields);
									createView.set_rowLimit(30);
									createView.set_viewTypeKind(1); //0, 1, 2048, 524288, 8193, 131072, 67108864  
									createView.set_setAsDefaultView(true);
									viewCollection.add(createView);
									currentcontext.load(viewCollection);									
									currentcontext.executeQueryAsync(
										function (data) {
											alert("Successfully created view for task List");	
											debugger;
											var listTitle = listTitle.jobName;
											var oList = hostweb.get_lists().getByTitle(listTitle);											
											currentcontext.load(oList, 'Id');
											currentcontext.executeQueryAsync(
												function (data) {
													alert("Success in getting list guid");	
													var listGuid1 = oList.get_id().toString().toUpperCase();
													//associateWF(oList.get_id().toString().toUpperCase());
													debugger;
													currentcontext.load(hostweb);
													// Get WorkflowServicesManager object for the specified web
													var servicesManager = SP.WorkflowServices.WorkflowServicesManager.newObject(currentcontext, hostweb);
													currentcontext.load(servicesManager);
													currentcontext.executeQueryAsync(
														function (data) {
															//alert("added columns");
															debugger;
															// Creating the subscription
															var sub = new SP.WorkflowServices.WorkflowSubscription(currentcontext);

															//sub.set_name('DemoTaskListWf12132');
															sub.set_name('WF-'+listTitle.jobName);
															sub.set_enabled(true);
															sub.set_definitionId(workflowDefinitionId);
															sub.set_statusFieldName('WF status');
															sub.set_eventSourceId(listGuid1);//listGuid
															
															var eventTypes = new Array();
															eventTypes.push("ItemAdded");
															eventTypes.push("ItemUpdated");
															eventTypes.push("WorkflowStart");
															sub.set_eventTypes(eventTypes);
															
															sub.setProperty("TaskListId", tasksList);
															sub.setProperty("HistoryListId", historyList);
															sub.setProperty("FormData", "");

															// Associate the workflow with the list
															var getWfSub = servicesManager.getWorkflowSubscriptionService();
															getWfSub.publishSubscriptionForList(sub, listGuid1);//listGuid
															//var pubSub  = getWfSub.publishSubscriptionForList(sub, listGuid);
															currentcontext.executeQueryAsync(
																function (data) {
																	debugger;
																	alert('Workflow association has been created successfully. Web: ' + hostweb.get_url() + ' for list guid:- ' + listGuid1);
																    var fullUrl = appWebUrlNew + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('JobDetailsLookup')/items(" + listTitle.jobId + ")?"+
																					"@target='" + hostWebUrl + "'";	
																	$.ajax({
																	url: fullUrl ,
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
															dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
														}
													);													
												},
												function (xhr) {
													dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
												}
											);											
										},
										function (xhr) {
											dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
										}
									);									
								},
								function (xhr) {
									dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
								}
							);
						}, 
						function (xhr) {
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
    };*/

	
 }]);
})();