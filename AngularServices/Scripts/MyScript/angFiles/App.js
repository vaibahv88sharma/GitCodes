//'use strict';

(function () {

	var app = angular.module('app' ,['ngRoute', 'ngCookies']);
	
		app.provider('books',['constants',function (constants) {
			this.$get = function () {
				
				var appName = constants.APP_TITLE;//'Book Logger';
				var appDesc = constants.APP_DESCRIPTION;//'Track which books you read.';				
				var version = constants.APP_VERSION;//'1.0';

				if(includeVersionInTitle){
					appName +=' '+ version;
				}
				
				return{
				appName : appName,
				appDesc : appDesc					
				};
			};
			
			var includeVersionInTitle = false;
			this.setIncludeVersionInTitle = function(value){
				includeVersionInTitle = value;
			};
		}]);	
	
	app.config(['booksProvider','constants', 'dataServiceProvider','$routeProvider','$logProvider','$httpProvider', '$provide'
				,function (booksProvider,constants, dataServiceProvider,$routeProvider,$logProvider,$httpProvider,$provide) {
		
		//$provide.decorator('$log',['$delegate','books',logDecorator]);
		$provide.decorator('$log',['$delegate','books',function ($delegate, books) {
			function log(message){
				message += ' - ' + new Date() + ' (' + books.appName + ')';
				$delegate.log(message);
			}
			function info(message){
				$delegate.info(message);
			}
			function warn(message){
				$delegate.warn(message);
			}
			function error(message){
				$delegate.error(message);
			}
			function debug(message){
				$delegate.debug(message);
			}
			function awesome(message){
				message = 'Awesome!!! - ' + message;
				$delegate.debug(message);
			}
						
			return {
				log : log,
				info : info,
				warn : warn,
				error : error,
				debug : debug,
				awesome : awesome
			}
											
		}]);	
		booksProvider.setIncludeVersionInTitle(true);
		
		$logProvider.debugEnabled(true);
		
		$httpProvider.interceptors.push('bookLoggerInterceptor');
		
		console.log('title from constants service: '+ constants.APP_TITLE );
		//console.log(dataServiceProvider.$get);
		
		$routeProvider
			.when('/',{
				templateUrl: "../HTML/Templates/angFiles/books.html",
				controller: "BooksController",
				controllerAs: "books"
			})
			.when('/AddBook',{
				templateUrl: "../HTML/Templates/angFiles/addBook.html",
				controller: "AddBookController",
				controllerAs: "bookAdder",			
			})
			.when('/EditBook/:bookID',{
				templateUrl: "../HTML/Templates/angFiles/editBook.html",
				controller: "EditBookController",
				controllerAs: "bookEditor",	
/*				resolve:{
					books:function(dataService){
						return dataService.getAllBooks();
					}
				}*/		
			})		
			.otherwise('/');					
	}]);

/*	function logDecorator($delegate, books) {
		function log(message){
			message += ' - ' + new Date() + ' (' + books.appName + ')';
			$delegate.log(message);
		}
		function info(message){
			$delegate.info(message);
		}
		function warn(message){
			$delegate.warn(message);
		}
		function error(message){
			$delegate.error(message);
		}
		function debug(message){
			$delegate.debug(message);
		}								
	}*/

	app.run(['$rootScope',function($rootScope){
		
		$rootScope.$on('$routeChangeSuccess',function(event,current,previous){
			console.log("Successfully Changed Route");
		});
		$rootScope.$on('$routeChangeError',function(event,current,previous,rejection){
/*			console.log("Error Changing Routes");
			console.log(event);
			console.log(current);
			console.log(previous);
			console.log(rejection);*/
		});		
	}]);

})();
