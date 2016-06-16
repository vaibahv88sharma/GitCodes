"use strict";
(function () {
    angular.module("moduleRest")
		.controller("controllerRest", ["$scope", "$location", "$routeParams", "myServiceRest", 
		function ($scope, $location, $routeParams, myServiceRest) {

	debugger;
	//Job Details Dropdown : Begin
	//$state.reload() ;
	myServiceRest.getDetails("JobDetailsLookup","$select=ID,Title,JobAddress&").then(
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
	myServiceRest.getDetails("SiteSupervisor","$select=SiteSupervisor/Id,SiteSupervisor/Title,SiteSupervisor/EMail,SiteSupervisor/FirstName,SiteSupervisor/LastName,SiteSupervisor/EMail&$expand=SiteSupervisor/Id&").then(
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
	myServiceRest.getDetails("TaskCategoryLookup","$select=ID,Title,TaskCategoryLookup&").then(
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
												debugger;
												var promiseAssociateWF = myServiceRest.associateWF($scope.Item, result);
												promiseAssociateWF.then(
													function (result) {
														var promiseDeleteJob = myServiceRest.deleteJob($scope.Item);
														promiseDeleteJob.then(
															function (result) {
																//$("containerDiv").prop('disabled', false);	
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
							function (err) { $scope.Message = "Error " + err.status; });
			        }, 
					function (err) { $scope.Message = "Error " + err.status; }
				);	
			};
/*			$scope.save = function () {
				var promiseAddJobs = myServiceRest.addjobs($scope.Item);
		        promiseAddJobs.then(function (response) {
			        var promiseDeleteJob = myServiceRest.deleteJob($scope.Item);
			        promiseDeleteJob.then(function (response) {
						$location.path("/Items");
			            //$scope.Categories = resp;
			        }, function (err) {
			            $scope.Message = "Error " + err.status;
			        });				
		            //$scope.Categories = resp;
		        }, function (err) {
		            $scope.Message = "Error " + err.status;
		        });	
			};*/
	        

							
/*			$scope.save = function () {
				myServiceRest.addjobs($scope.Item).then(
						function (result) {
							$location.path("/Items");	
						});
				//$location.path("/Items");	
			};*/	
			$scope.cancel = function () {
		        $location.path("/Items");
			};

		}]);
})();

/*moduleRest.controller("controllerRest", ["$scope", "$location", "$routeParams", "myServiceRest", 
						function ($scope, $location, $routeParams, myServiceRest) {
	debugger;
	//Job Details Dropdown : Begin
	//$state.reload() ;
	myServiceRest.getDetails("JobDetailsLookup","$select=ID,Title,JobAddress&").then(
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
	myServiceRest.getDetails("SiteSupervisor","$select=SiteSupervisor/Id,SiteSupervisor/Title,SiteSupervisor/EMail,SiteSupervisor/FirstName,SiteSupervisor/LastName,SiteSupervisor/EMail&$expand=SiteSupervisor/Id&").then(
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
	myServiceRest.getDetails("TaskCategoryLookup","$select=ID,Title,TaskCategoryLookup&").then(
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

moduleRest.controller("addControllerRest", ["$scope", "$location", "$routeParams", "myServiceRest", 
						function ($scope, $location, $routeParams, myServiceRest) {
	$scope.Item = myServiceRest.Items;
	$scope.save = function () {
		myServiceRest.addjobs($scope.Item);
		$location.path("/Items");	
	};	
	$scope.save = function () {
		//myServiceRest.addjobs($scope.Item);
		//$location.path("/Items");
		myServiceRest.createJobList($scope.Item).then(
        function (result) {
			debugger;
			alert("success");
        },
        function (reason) {
            $scope.errMessage = reason;
			alert("fail");
        });		
	};	
	$scope.save = function () {
		//myServiceRest.addjobs($scope.Item);
		//$location.path("/Items");
		myServiceRest.createJobList($scope.Item).then(
        function (result) {
			debugger;
			myServiceRest.createTask($scope.Item).then(
			        function (result) {
						debugger;
						myServiceRest.deleteJob($scope.Item).then(
					        function (result) {
								debugger;
								alert("deleted");
								//$location.path("/Items");
					        },
					        function (reason) {
					            $scope.errMessage = reason;
					        });
			        },
			        function (reason) {
			            $scope.errMessage = reason;
			        });
        },
        function (reason) {
            $scope.errMessage = reason;
        });		
	};
	$scope.cancel = function () {
        $location.path("/Items");
	};
}]);*/
