"use strict";
(function () {
    angular.module("moduleRest")
		.controller("controllerRest", ["$scope", "$location", "$routeParams", "myServiceRest",
		function ($scope, $location, $routeParams, myServiceRest) {

		    debugger;
		    //Job Details Dropdown : Begin
		    //$state.reload() ;
		    myServiceRest.getDetails("JobDetailsLookup", "$select=ID,Title,JobAddress&").then(
                function (result) {
                    debugger;
                    $scope.jobs = [];
                    angular.forEach(result, function (job) {
                        var allJobs = {
                            id: job.ID,
                            title: job.Title,
                            jobAddress: job.JobAddress
                        }
                        $scope.jobs.push(allJobs);
                    });
                },
                function (reason) {
                    $scope.errMessage = reason;
                });
		    //Job Details Dropdown : End

		    //Supervisor Dropdown : Begin
		    myServiceRest.getDetails("SiteSupervisor", "$select=SiteSupervisor/Id,SiteSupervisor/Title,SiteSupervisor/EMail,SiteSupervisor/FirstName,SiteSupervisor/LastName,SiteSupervisor/EMail&$expand=SiteSupervisor/Id&").then(
                function (result) {
                    debugger;
                    $scope.siteSupervisors = [];
                    angular.forEach(result, function (siteSupervisor) {
                        var newSiteSup = {
                            id: siteSupervisor.ID,
                            title: siteSupervisor.Title,
                            siteSupervisorName: siteSupervisor.SiteSupervisor.Title,
                            siteSupervisorId: siteSupervisor.SiteSupervisor.Id
                        }
                        $scope.siteSupervisors.push(newSiteSup);
                    });
                },
                function (reason) {
                    $scope.errMessage = reason;
                });
		    //Supervisor Dropdown : End
		    //Tasks Category Dropdown : Begin
		    myServiceRest.getDetails("TaskCategoryLookup", "$select=ID,Title,TaskCategoryLookup&").then(
                function (result) {
                    debugger;
                    $scope.taskCategories = [];
                    angular.forEach(result, function (taskCategory) {
                        var allTaskCat = {
                            id: taskCategory.ID,
                            title: taskCategory.Title,
                            taskCategoryName: taskCategory.TaskCategoryLookup
                        }
                        $scope.taskCategories.push(allTaskCat);
                    });
                },
                function (reason) {
                    $scope.errMessage = reason;
                });
		    //Tasks Category Dropdown : End	

		    $scope.createTask = function () {
		        myServiceRest.setItems($scope.Item);
		        $location.path("/Items/add");
		    };

		}]);
})();


(function () {
    angular.module("moduleRest")
		.controller("addControllerRest", ["$scope", "$location", "$routeParams", "myServiceRest",
		function ($scope, $location, $routeParams, myServiceRest) {

		    $scope.Item = myServiceRest.Items;

		    $scope.save = function () {
		        var promiseAddJobs = myServiceRest.addjobs($scope.Item);
		        promiseAddJobs.then(
					function (response) {
					    //alert("Completed addjobs");
					    //$("containerDiv").prop('disabled', true);										
					    var promiseAddFieldToList = myServiceRest.addFieldToList($scope.Item);
					    promiseAddFieldToList.then(
							function (response) {
							    var promiseCreatelistView = myServiceRest.createlistView($scope.Item);
							    promiseCreatelistView.then(
									function (response) {
									    //var promiseGetListGuid = myServiceRest.getListGuid($scope.Item);
									    //promiseGetListGuid.then(
									    //function (result) {
									    //var promiseAssociateWF = myServiceRest.associateWF($scope.Item, result);
									    //promiseAssociateWF.then(
									    //function (result) {
									    var promiseDeleteJob = myServiceRest.deleteJob($scope.Item);
									    promiseDeleteJob.then(
                                            function (result) {
                                                //$("containerDiv").prop('disabled', false);
                                                var promiseGetTaskLookup = myServiceRest.getDetails("TaskLookup",
                                                                        "$select=Description,StartDate,EndDate,DetailsOfTask,TaskCategoryLookup/ID,TaskCategoryLookup/TaskCategoryLookup&$expand=TaskCategoryLookup&$filter=TaskCategoryLookup/ID eq " + $scope.Item.taskCatId + "&");
                                                promiseGetTaskLookup.then(
                                                    function (result) {
                                                        debugger;
                                                        var promisePostTask;
                                                        var taskCatData;
                                                        angular.forEach(result, function (taskCategoryData) {
                                                            taskCatData = [];
                                                            var allTaskCat = {
                                                                Description: taskCategoryData.Description,
                                                                DetailsOfTask: taskCategoryData.DetailsOfTask
                                                            }
                                                            taskCatData.push(allTaskCat);
                                                            promisePostTask = myServiceRest.insertJobsData($scope.Item, taskCatData);
                                                        });
                                                        promisePostTask.then(
                                                            function (result) {
                                                                //alert("completed insertJobsData");
                                                                var promiseGetListGuid = myServiceRest.getListGuid($scope.Item);
                                                                promiseGetListGuid.then(
                                                                    function (result) {
                                                                        //alert("completed getListGuid");
                                                                        var promiseAssociateWF = myServiceRest.associateWF($scope.Item, result);
                                                                        promiseAssociateWF.then(
                                                                            function (result) {
                                                                                //alert("Completed associateWF");
                                                                                $location.path("/Items");
                                                                            },
                                                                            function (err) { $scope.Message = "Error " + err.status; }
                                                                        );
                                                                    },
                                                                    function (err) { $scope.Message = "Error " + err.status; }
                                                                );
                                                            },
                                                            function (err) { $scope.Message = "Error " + err.status; }
                                                        );
                                                    },
                                                    function (err) { $scope.Message = "Error " + err.status; }
                                                );
                                            },
                                            function (err) { $scope.Message = "Error " + err.status; }
                                        );
									    //},
									    //function (err) { $scope.Message = "Error " + err.status; }
									    //);
									    //},										
									    //function (err) { $scope.Message = "Error " + err.status; }
									    //);
									},
									function (err) { $scope.Message = "Error " + err.status; }
								);
							},
							function (err) { $scope.Message = "Error " + err.status; });
					},
					function (err) { $scope.Message = "Error " + err.status; }
				);
		    };

		    /*			$scope.save = function () {
                            var promiseAddJobs = myServiceRest.addjobs($scope.Item);
                            promiseAddJobs.then(
                                function (response) {	
                                    alert("Completed addjobs");
                                    //$("containerDiv").prop('disabled', true);										
                                    var promiseAddFieldToList = myServiceRest.addFieldToList($scope.Item);
                                    promiseAddFieldToList.then(
                                        function (response) {
                                            var promiseCreatelistView  = myServiceRest.createlistView($scope.Item);
                                            promiseCreatelistView.then(
                                                function (response) {
                                                    var promiseGetListGuid = myServiceRest.getListGuid($scope.Item);
                                                    promiseGetListGuid.then(
                                                        function (result) {
                                                            var promiseAssociateWF = myServiceRest.associateWF($scope.Item, result);
                                                            promiseAssociateWF.then(
                                                                function (result) {
                                                                    var promiseDeleteJob = myServiceRest.deleteJob($scope.Item);
                                                                    promiseDeleteJob.then(
                                                                        function (result) {
                                                                            //$("containerDiv").prop('disabled', false);
                                                                            var promiseGetTaskLookup = myServiceRest.getDetails("TaskLookup", 
                                                                                                    "$select=Description,StartDate,EndDate,DetailsOfTask,TaskCategoryLookup/ID,TaskCategoryLookup/TaskCategoryLookup&$expand=TaskCategoryLookup&$filter=TaskCategoryLookup/ID eq "+$scope.Item.taskCatId+"&");
                                                                            promiseGetTaskLookup.then(
                                                                                function (result) {
                                                                                    debugger;
                                                                                    var promisePostTask;
                                                                                    var taskCatData;
                                                                                    angular.forEach(result, function (taskCategoryData) {
                                                                                       taskCatData = [];
                                                                                       var allTaskCat = {
                                                                                           Description: taskCategoryData.Description,
                                                                                           DetailsOfTask: taskCategoryData.DetailsOfTask
                                                                                       }
                                                                                       taskCatData.push(allTaskCat);
                                                                                        promisePostTask = myServiceRest.insertJobsData($scope.Item, taskCatData);
                                                                                    });
                                                                                    promisePostTask.then(
                                                                                        function (result) { 
                                                                                            alert("completed insertJobsData");
                                                                                            $location.path("/Items");																				
                                                                                        },
                                                                                        function (err) { $scope.Message = "Error " + err.status; }		
                                                                                    );
                                                                                },
                                                                                function (err) { $scope.Message = "Error " + err.status; }
                                                                            );																	
                                                                        },
                                                                        function (err) { $scope.Message = "Error " + err.status; }		
                                                                    );
                                                                },
                                                                function (err) { $scope.Message = "Error " + err.status; }
                                                            );
                                                        },										
                                                        function (err) { $scope.Message = "Error " + err.status; }
                                                    );
                                                },
                                                function (err) { $scope.Message = "Error " + err.status; }
                                            );
                                        },
                                        function (err) { $scope.Message = "Error " + err.status; });
                                }, 
                                function (err) { $scope.Message = "Error " + err.status; }
                            );	
                        };*/

		    $scope.cancel = function () {
		        $location.path("/Items");
		    };

		}]);
})();