"use strict";
(function () {
    angular.module("moduleRest")
        .service("myServiceRest", ['$http', '$q', function ($http, $q) {

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
            var tasksList = '756B1FA1-901E-4E2B-A770-5E0F9B986E8A'; //'TasksList Tasks'	
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
            this.clear = function () {
                this.Items = {
                    jobName: "",
                    supervisorName: "",
                    taskName: ""
                };
            }
            this.setItems = function (items) {
                //alert(items.jobName.id +"--"+items.supervisorName.siteSupervisorId +"--"+items.taskName.id);
                this.Items = {
                    jobId: items.jobName.id,
                    jobName: items.jobName.jobAddress,
                    supervisorName: items.supervisorName.siteSupervisorName,
                    supervisorId: items.supervisorName.siteSupervisorId,
                    taskName: items.taskName.taskCategoryName,
                    taskCatId: items.taskName.id
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
                var oList = hostweb.get_lists().getByTitle(listTitle.jobName);
                var viewCollection = oList.get_views();
                currentcontext.load(viewCollection);
                var createView = new SP.ViewCreationInformation();
                createView.set_title("TasksCreated");
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
                var oList = hostweb.get_lists().getByTitle(listTitle.jobName);
                currentcontext.load(oList, 'Id');
                currentcontext.executeQueryAsync(
                    function (data) {
                        debugger;
                        var listGuidVal = oList.get_id().toString().toUpperCase();
                        dfd.resolve(listGuidVal);
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
                        debugger;
                        // Creating the subscription
                        var sub = new SP.WorkflowServices.WorkflowSubscription(currentcontext);

                        sub.set_name('WF-' + listTitle.jobName);
                        sub.set_enabled(true);
                        sub.set_definitionId(workflowDefinitionId);
                        sub.set_statusFieldName('WF status');
                        sub.set_eventSourceId(listGuid1);

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
                        getWfSub.publishSubscriptionForList(sub, listGuid1);
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

            //Create Jobs : End

            // Delete Job Address :Begin
            this.deleteJob = function (listTitle) {
                debugger;
                var dfd = $q.defer();
                var hostweburl = decodeURIComponent(manageQueryStringParameter("SPHostUrl"));
                var appweburl = decodeURIComponent(manageQueryStringParameter("SPAppWebUrl"));
                var appWebUrlNew1 = appweburl.split("#")[0];
                var fullUrl = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('JobDetailsLookup')/items(" + listTitle.jobId + ")?" +
                                "@target='" + hostweburl + "'";
                $.ajax({
                    url: fullUrl,
                    type: "DELETE",
                    headers: {
                        "ACCEPT": "application/json;odata=verbose",
                        "content-type": "application/json;odata=verbose",
                        "X-RequestDigest": $("#__REQUESTDIGEST").val(),
                        "IF-MATCH": "*",
                        "X-HTTP-Method": "DELETE"
                    },
                    success: function (data) {
                        dfd.resolve(data.d);
                    },
                    error: function (xhr) {
                        alert("failed delete");
                        dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
                    }
                });

                return dfd.promise;
            };
            // Delete Job Address :End

            // Insert Jobs Data :Begin
            this.insertJobsData = function (listTitle, result) {
                debugger;
                var dfd = $q.defer();

                var oList = hostweb.get_lists().getByTitle(listTitle.jobName);
                var itemCreateInfo = new SP.ListItemCreationInformation();
                var oListItem = oList.addItem(itemCreateInfo);

                oListItem.set_item('Title', result[0].Description);
                oListItem.set_item('Body', result[0].DetailsOfTask);
                //oListItem.set_item('DueDate', new Date(date.substring(0, 10)));

                //User Field : Begin
                var assignedToVal = new SP.FieldUserValue();
                assignedToVal.set_lookupId(listTitle.supervisorId);
                //assignedToVal.set_lookupId(10);
                oListItem.set_item('Supplier', assignedToVal);
                oListItem.set_item('SiteSupervisor', assignedToVal);
                //User Field : End
                oListItem.update();
                currentcontext.load(oListItem);
                currentcontext.executeQueryAsync(
                    function (data) {
                        //alert(JSON.stringify(data));
                        dfd.resolve(data.d);
                    },
                    function (xhr) {
                        //alert(JSON.stringify(xhr));
                        dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
                    }
                );

                return dfd.promise;
            };
            this.GetItemTypeForListName = function (name) {
                return "SP.Data." + name.charAt(0).toUpperCase() + name.split(" ").join("").slice(1) + "ListItem";
            }
            // Insert Jobs Data :End

            /*	// REST Insert Jobs Data :Begin
                this.RESTinsertJobsData = function (listTitle, result) {
                    debugger;	
                    var dfd = $q.defer();
                    var hostweburl = decodeURIComponent(manageQueryStringParameter("SPHostUrl"));
                    var appweburl = decodeURIComponent(manageQueryStringParameter("SPAppWebUrl"));	
                    var appWebUrlNew1 = appweburl.split("#")[0];
                    var fullUrl = appWebUrlNew1 + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('"+listTitle.jobName+"')/items?"+
                                    "@target='" + hostweburl + "'";	
                    var itemType = this.GetItemTypeForListName(listTitle.jobName);
                    var data = {
                        "__metadata": { "type": itemType },
                        "Title": "Description",
                        //"DueDate":"",
                        "Supplier":result.Supplier,
                        "SiteSupervisor":result.SiteSupervisor,
                        "Body":"DetailsOfTask"
                    };
                    
                    $.ajax({
                       url: fullUrl ,
                       type: "POST",
                       headers: {
                        "ACCEPT": "application/json;odata=verbose",
                        "content-type": "application/json;odata=verbose",
                        "X-RequestDigest": $("#__REQUESTDIGEST").val()
                       },
                        data: JSON.stringify(data),		   
                       success: function (data) {
                              dfd.resolve(data.d);
                        },
                       error: function (xhr) {
                            //alert("failed insert data");
                           dfd.reject('Error : (' + xhr.status + ') ' + xhr.statusText + ' Message: ' + xhr.responseJSON.error.message.value);
                        }
                    });
                    
                    return dfd.promise;
                };
                this.GetItemTypeForListName = function (name) {
                    return "SP.Data." + name.charAt(0).toUpperCase() + name.split(" ").join("").slice(1) + "ListItem";
                }	
                // REST Insert Jobs Data :End*/

        }]);
})();