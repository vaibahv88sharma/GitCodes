(function() {

	angular.module('app').factory('dataService', ['$q', '$timeout', '$http', 'constants', '$log', '$location', '$cacheFactory', dataService]);

	/*function dataService(logger){*/
	function dataService($q, $timeout, $http, constants, $log, $location, $cacheFactory) {
		return {
			getAllBooks: getAllBooks,
			getBookById: getBookById,
			updateBook: updateBook,
			getAllReaders: getAllReaders,
			addBook: addBook,
			deleteBook: deleteBook,
			getUserSummary: getUserSummary
		};

		function getUserSummary() {
			var deferred = $q.defer();

			var dataCache = $cacheFactory.get('bookLoggerCache');
			if (!dataCache) {
				dataCache = $cacheFactory('bookLoggerCache');
			}

			var summaryFromCache = $cacheFactory.get('summary');
			if (summaryFromCache) {
				$log.debug('returning summary from cache');
				deferred.resolve(summaryFromCache);
			} else {
				$log.debug('gathering new summary data');
					
				var booksPromise = this.getAllBooks();
				var readersPromise = getAllReaders();

				$q.all([booksPromise, readersPromise]).then(function(bookLoggerData) {
					var allBooks = bookLoggerData[0];
					var allReaders = bookLoggerData[1];

					var grandTotalMinutes = 0;

					allReaders.forEach(function(currentReader, index, array) {
						grandTotalMinutes += currentReader.totalMinutesRead;
					});

					var summaryData = {
						bookCount: allBooks.length,
						readerCount: allReaders.length,
						grandTotalMinutes: grandTotalMinutes
					};

					dataCache.put('summary', summaryData);

					deferred.resolve(summaryData);
				});
			}

			return deferred.promise;
		}

		function deleteSummaryFromCache(){
			var dataCache = $cacheFactory.get('bookLoggerCache');
			dataCache.remove('summary');
		}

		function getAllBooks() {

			return $http({
				url: constants.urlBooks,
				method: "GET",
				headers: {
					"accept": "application/json;odata=verbose",
					"content-Type": "application/json;odata=verbose"
				},
				transformResponse: function(data, headersGetter) {
					var transformed = angular.fromJson(data);
					transformed.d.results.forEach(function(currentValue, index, array) {
						currentValue.dateDownloaded = new Date();
					});
					$log.log(transformed.d.results);
					return transformed;
				},
				cache : true
			}).then(function(response) {
				return response.data.d.results;
			}).catch (function(response) {
				return $q.reject('Error retreiving book(s). (Http Status: ' + response.status + ')');
			});
		}

		function deleteAllBooksResponseFromCache() {
			var httpCache = $cacheFactory.get('$http');
			httpCache.remove(constants.urlBooks);
		}

		function getBookById(id) {

			return $http({
				url: constants.urlBooksById + id,
				//baseUrl + query,
				method: "GET",
				headers: {
					"accept": "application/json;odata=verbose",
					"content-Type": "application/json;odata=verbose"
				}
			}).then(function(response) {
				return response.data.d;
			}).
			catch (function(response) {
				return $q.reject('Error retreiving book(s). (Http Status: ' + response.status + ')');
			});

		}

		function updateBook(data, id) {

			deleteSummaryFromCache();
			deleteAllBooksResponseFromCache();

			return $http({
				url: constants.urlUpdateDeleteBooksById + id,
				        method:  "POST",
				processData: false,
				data: JSON.stringify(data),
				//dataUpdate
				transformRequest: angular.identity,
				headers:   {
					"Accept":  "application/json; odata=verbose",
					"content-type":  "application/json; odata=verbose",
					"X-RequestDigest": $("#__REQUESTDIGEST").val(),
					//"content-length": dataUpdate.length,
					"X-HTTP-Method":  "MERGE",
					"IF-MATCH":  "*"
				}
			}).then(function(response) {
				return 'Book Updated: ' + response.config; //.d.title;
			}).
			catch (function(response) {
				return $q.reject('Error retreiving book(s). (Http Status: ' + response.status + ')');
			});

		}

		function addBook(data) {

			deleteSummaryFromCache();
			deleteAllBooksResponseFromCache();

			var dataUpdate = $.extend({}, constants.dataBooks, data);
			return $http({
				url: constants.urlBookPost,
				        method:  "POST",
				//processData: false,
				data: JSON.stringify(dataUpdate),
				//dataUpdate
				//transformRequest: angular.identity,					
				headers: {
					"accept": "application/json;odata=verbose",
					"X-RequestDigest": $("#__REQUESTDIGEST").val(),
					"content-type": "application/json;odata=verbose"
				}
			}).then(function(response) {
				return 'Book added.' + response.data.d.Title;
			}).
			catch (function(response) {
				//$log.error(errorMessage);
				return $q.reject('Error adding book(s). (Http Status: ' + response.status + ')');
			});

		}

		function deleteBook(data) {

			deleteSummaryFromCache();
			deleteAllBooksResponseFromCache();

			return $http({
				url: constants.urlUpdateDeleteBooksById + data.Id,
				        method:  "POST",
				processData: false,
				data: JSON.stringify(data),
				//dataDelete
				transformRequest: angular.identity,
				headers:   {
					"Accept":  "application/json; odata=verbose",
					"content-type":  "application/json; odata=verbose",
					"X-RequestDigest": $("#__REQUESTDIGEST").val(),
					//"content-length": data1.length,
					"X-HTTP-Method":  "DELETE",
					"IF-MATCH":  "*"
				}
			}).then(function(response) {
				return 'Book deleted.';
			}).
			catch (function(response) {
				return $q.reject('Error deleting book(s). (Http Status: ' + response.status + ')');
			});

		}

		function getAllReaders() {

			//logger.output('getting all readers.');			
			var readersArray = [{
				reader_id: 1,
				name: 'Marie',
				weeklyReadingGoal: 315,
				totalMinutesRead: 5600
			}, {
				reader_id: 2,
				name: 'Daniel',
				weeklyReadingGoal: 210,
				totalMinutesRead: 3000
			}, {
				reader_id: 3,
				name: 'Lanier',
				weeklyReadingGoal: 140,
				totalMinutesRead: 600
			}];
			var deferred = $q.defer();
			$timeout(function() {
				deferred.notify('Just getting data..');
				deferred.notify('Almost done..');
				deferred.resolve(readersArray);
				/*				var successful = true;
				if(successful){
					deferred.notify('Just getting data..');
					deferred.notify('Almost done..');
					deferred.resolve(booksArray);
				}
				else{
					deferred.reject('Error in Retreiving books.');
				}*/
			}, 1500);
			return deferred.promise;
		}
	}

	dataService.$inject = ['logger'];

}());