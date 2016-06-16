'use strict';

var context = SP.ClientContext.get_current();
var user = context.get_web().get_currentUser();

//vaibhav
var BasicJSOM = window.BasicJSOM ||{};
BasicJSOM.Exception = BasicJSOM.Exception || {};
BasicJSOM.Utilities = BasicJSOM.Utilities || {};
//vaibhav

(function () {

    // This code runs when the DOM is ready and creates a context object which is 
    // needed to use the SharePoint object model
    $(document).ready(function () {
        getUserName();
        //vaibhav
        listAllCategories();
		BasicJSOM.Exception.SimpleDemo = new BasicJSOM.Exception.Simple();
	    BasicJSOM.Exception.Init();		
		//vaibhav  
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

//#region Vaibhav

BasicJSOM.Utilities.LogResult =function(msg){
	$("logResultsDiv").append("<p class='highlighted'>"+msg +"</p>");
	$("logResultsDiv").append("<hr>");
}

BasicJSOM.Exception.Init = function() {
	 $("#CustButton").click(BasicJSOM.Exception.SimpleDemo.simpleExample);
 }
 
 BasicJSOM.Exception.Simple = function(){
	 
	 //#region private variables
	 var _web;
	 var errScope;
	 var listName;
	 //#endregion private variables
	 
	 //#region private functions
	 
	 function getRandomListName(){
		 var now = new Date();
		 return "sampleList_" + now.getMilliseconds().toString() +  (Math.floor(Math.random()*100)+1).toString();
	 }
	 
	 function _simpleExample(){
		 var template = SP.ListTemplateType.genericList;
		 var desc = "Created Prog to demonstrate JSOM error handling";
		 
		 var context = new SP.ClientContext();
         listName = getRandomListName();
		 
		 errScope = new SP.ExceptionHandlingScope(context);
		 var scopeStart = errScope.startScope();
		 
		 var tryBlock = errScope.startTry();
			 var theList = context.get_web().get_lists().getByTitle(listName);
		 tryBlock.dispose();
		 
		 var catchBlock = errScope.startCatch();
		 	var listCI = new SP.ListCreationInformation();
			listCI.set_title(listName);
			listCI.set_templateType(template);
			listCI.set_description(desc);
			theList = context.get_web().get_lists().add(listCI);
		catchBlock.dispose();
		
		var finallyBlock = errScope.startFinally();
			context.load(theList);
		finallyBlock.dispose();
		
		scopeStart.dispose();
		
		//context.executeQueryAsync(_onSucceed,BasicJSOM.Utilities.Fail);
		context.executeQueryAsync(_onSucceed,_onFail);
	 }	 
	 //#endregion private functions
	 
	 //#region Callbacks	 
	 function _onSucceed(){
		 BasicJSOM.Utilities.LogResult("Returned Successfully from Server");
		 //if (errScope.get_hasException())
		 if (errScope.get_hasException())
		 {
			 BasicJSOM.Utilities.LogResult("List '"+listName+"' did not exists");
		 }
		 else
		 {
			 BasicJSOM.Utilities.LogResult("List '"+listName+"' retreived");
		 }
	 }	 
	 
	 function _onFail(){
		 BasicJSOM.Utilities.LogResult("Did not returned from Server");		
	 }
	 //#endregion Callbacks
	 
	 //#region public members
	 var publicMembers = {
		 simpleExample: _simpleExample
	 }
	 
	 return publicMembers;
	 //#endregion public members 
 }
 
//#endregion Vaibhav
})();