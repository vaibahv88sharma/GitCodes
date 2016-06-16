(function (app) {
app.controller('spscontroller', function ($scope,spsservice) {

$scope.seletedDdValues = [];

    //Job Details Dropdown : Begin
	//spsservice.getDetails(appWebUrl + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('JobDetailsLookup')/items?$select=ID,Title,JobAddress&@target='" + hostWebUrl + "'").then(
	spsservice.getDetails("JobDetailsLookup","$select=ID,Title,JobAddress&").then(
        function (result) {
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
    //courseListService.getCourses(appWebUrl + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('SiteSupervisor')/items?$select=SiteSupervisor/Title,SiteSupervisor/EMail,SiteSupervisor/FirstName,SiteSupervisor/LastName,SiteSupervisor/EMail&$expand=SiteSupervisor/Id&@target='" + hostWebUrl +
	// "'").then(
	spsservice.getDetails("SiteSupervisor","$select=SiteSupervisor/Title,SiteSupervisor/EMail,SiteSupervisor/FirstName,SiteSupervisor/LastName,SiteSupervisor/EMail&$expand=SiteSupervisor/Id&").then(
        function (result) {
			debugger;
            $scope.siteSupervisors = [];
            angular.forEach(result, function (siteSupervisor) {
                var newSiteSup = {
                    id: siteSupervisor.ID,
                    title: siteSupervisor.Title,
                    siteSupervisorName: siteSupervisor.SiteSupervisor.Title
                }
                $scope.siteSupervisors.push(newSiteSup);
            });
        },
        function (reason) {
            $scope.errMessage = reason;
        });
    //Supervisor Dropdown : End

	//Tasks Category Dropdown : Begin
//    courseListService.getCourses(appWebUrl + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('TaskCategoryLookup')/items?$select=ID,Title,TaskCategoryLookup&@target='" + hostWebUrl + "'").then(
	spsservice.getDetails("TaskCategoryLookup","$select=ID,Title,TaskCategoryLookup&").then(
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

});
}(angular.module('spsmodule')));