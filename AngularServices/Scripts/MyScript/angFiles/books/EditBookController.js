(function() {

	angular.module('app').controller('EditBookController', 
			['$routeParams', 'books','$cookies', '$cookieStore', 'dataService', '$log', '$location', EditBookController]);

	function EditBookController($routeParams, books,$cookies, $cookieStore, dataService, $log, $location) {
		var vm = this;

		/*		dataService.getAllBooks()
			.then(function(books){
				vm.currentBook = books.filter(function(item){
					return item.book_id == $routeParams.bookID;
				})[0];
			});*/

/*		vm.currentBook = books.filter(function(item) {
			return item.book_id == $routeParams.bookID;
		})[0];*/
		
		dataService.getBookById($routeParams.bookID)
			.then(getIdBooksSuccess)
			.catch(errorIdCallback);
			
		function getIdBooksSuccess(book){
			vm.currentBook = book;
			$cookieStore.put('lastEdited', vm.currentBook,{
			    expires: new Date(2016, 1, 1)
			});			
		}	
		function errorIdCallback(reason){
			$log.error(reason);
		}
		
		vm.saveBook = function(currentBook){
			dataService.updateBook(currentBook,$routeParams.bookID)
			.then(updateBooks)
			.catch(errorCallbackUpdate);
		}	
		function updateBooks(message){
			$log.info(message);
			$location.path('/');			
		}	
		function errorCallbackUpdate(reason){
			$log.error(reason);
		}		
		
		vm.setAsFavorite = function(){
			/*$cookies.favoriteBook = vm.currentBook.title;	*/
			$cookieStore.put('favoriteBook', vm.currentBook.title,{
			    expires: new Date(2016, 1, 1)
			});			
		};
/*		$cookieStore.put('lastEdited', vm.currentBook,{
		    expires: new Date(2016, 1, 1)
		});*/
		
		
/*$cookies.put("id", 1, {
    expires: new Date(2016, 1, 1)
});*/		
	}

}());