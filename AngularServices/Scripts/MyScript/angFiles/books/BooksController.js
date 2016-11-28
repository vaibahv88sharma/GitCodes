(function() {

	angular.module('app')
		.controller('BooksController',['books', 'dataService','logger','badgeService','$q','$cookies', '$cookieStore','$log','$route',BooksController]);
		
	function BooksController(books, dataService,logger,badgeService,$q, $cookies, $cookieStore,$log,$route) {
		
		var vm = this;
		vm.appName = books.appName;

		dataService.getUserSummary()
			.then(function(summaryData){
				$log.debug(summaryData);
				vm.summaryData = summaryData;
			});

		dataService.getAllBooks()
			.then(getBooksSuccess,null,getBooksNotification)
			.catch(errorCallback)
			.finally(getAllBooksComplete);
		function getBooksSuccess(books){
			vm.allBooks = books;
		}
//		function getBooksError(reason){
//			console.log(reason);
//		}	
		function errorCallback(errorMsg){
			console.log('Error Message: '+errorMsg);
		}
		function getBooksNotification(notification){
			console.log('Promise Notification: '+notification);
		}	
		function getAllBooksComplete(){
			console.log('getAllBooks has completed');
		}		
		
		dataService.getAllReaders()
			.then(getReadersSuccess)
			.catch(errorCallback)
			.finally(getAllReadersComplete);
		function getReadersSuccess(readers){
			vm.allReaders = readers;
			$log.log('All Readers retreived');
			$log.awesome('All Readers retreived');
		}
		function getAllReadersComplete(){
			console.log('getAllReaders has completed');
		}		
		
		
		vm.deleteBook = function (book) {
			dataService.deleteBook(book)
				.then(deleteBookSuccess)
				.catch(deleteBookError);			
		}
			function deleteBookSuccess(message){
				$log.info(message);
				$route.reload();
			}
			function deleteBookError(errorMessage){
				$log.error(errorMessage);
			}		
		
		
		vm.getBadge = badgeService.retreiveBadge;
		
/*		vm.favoriteBook = $cookies.favoriteBook;*/
		vm.favoriteBook = $cookieStore.get('favoriteBook');			
		vm.lastEdited = $cookieStore.get('lastEdited');		
		
/*		$log.log('logging with log');
		$log.info('logging with info');
		$log.warn('logging with warn');
		$log.error('logging with error');
		$log.debug('logging with debug');*/
		
	}

}()); 