angular.module('NoteWrangler').config(function($routeProvider){
/*noteWrangler.config(function($routeProvider){*/
	$routeProvider
		.when('/',{
			redirectTo:'/notes'
		})
		.when('/notes',{
			templateUrl: "/sites/PublishingSite/_catalogs/masterpage/CstmMstrPage/AngBootMaster/HTML/Templates/notes/index.html",
			controller: "NotesIndexController"
		})
		.when('/notes/new',{
			templateUrl: "/sites/PublishingSite/_catalogs/masterpage/CstmMstrPage/AngBootMaster/HTML/Templates/notes/new.html",
			controller: "NotesCreateController"
		})
		.when('/notes/:id',{
			templateUrl: "/sites/PublishingSite/_catalogs/masterpage/CstmMstrPage/AngBootMaster/HTML/Templates/notes/show.html",
			controller: "NotesShowController"
		})
		.when('/notes/:id/edit',{
			templateUrl: "/sites/PublishingSite/_catalogs/masterpage/CstmMstrPage/AngBootMaster/HTML/Templates/notes/edit.html",
			controller: "NotesEditController"
		})
		.when('/users',{
			templateUrl: "/sites/PublishingSite/_catalogs/masterpage/CstmMstrPage/AngBootMaster/HTML/Templates/users/index.html",
			controller: "UsersIndexController"
		})
		.when('/users/:id',{
			templateUrl: "/sites/PublishingSite/_catalogs/masterpage/CstmMstrPage/AngBootMaster/HTML/Templates/users/show.html",
			controller: "UsersShowController"
		})				
});