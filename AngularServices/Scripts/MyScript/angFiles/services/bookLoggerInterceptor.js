(function () {

/*	angular.module('app')
		.factory('bookLoggerInterceptor',['$q', '$log'], bookLoggerInterceptor);
		
	function bookLoggerInterceptor($q, $log) {
		return{
			request: requestInterseptor,
			responseError: responseErrorInterseptor
		};
		
		function requestInterseptor(config) {
			$log.debug('HTTP ' + config.method + 'request - ' + config.url);
			return config;
		}
		
		function responseErrorInterseptor(response) {
			$log.debug('HTTP ' + response.config.method + 'response error - ' + response.config);
			return $q.reject(response);
		}		
		
	}*/
	
	angular.module('app')
		.factory('bookLoggerInterceptor',['$q', '$log', function ($q, $log) {
			return{
				request: requestInterseptor,
				responseError: responseErrorInterseptor
			};
			
			function requestInterseptor(config) {
				$log.debug('HTTP ' + config.method + ' request - ' + config.url);
				return config;
			}
			
			function responseErrorInterseptor(response) {
				$log.debug('HTTP ' + response.config.method + 'response error - ' + response.config);
				return $q.reject(response);
			}		
		
		}]);	

}());