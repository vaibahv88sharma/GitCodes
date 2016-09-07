angular.module('NoteWrangler').config(function($routeProvider){
/*noteWrangler.config(function($routeProvider){*/
	$routeProvider
		.when('/',{
			redirectTo:'/notes'
		})
		.when('/notes',{
			templateUrl: "../HTML/Templates/notes/index.html",
			controller: "NotesIndexController"
		})
		.when('/notes/new',{
			templateUrl: "../HTML/Templates/notes/new.html",
			controller: "NotesCreateController"
		})
		.when('/notes/:id',{
			templateUrl: "../HTML/Templates/notes/show.html",
			controller: "NotesShowController"
		})
		.when('/notes/:id/edit',{
			templateUrl: "../HTML/Templates/notes/edit.html",
			controller: "NotesEditController"
		})
		.when('/users',{
			templateUrl: "../HTML/Templates/users/index.html",
			controller: "UsersIndexController"
		})
		.when('/users/:id',{
			templateUrl: "../HTML/Templates/users/show.html",
			controller: "UsersShowController"
		})				
});