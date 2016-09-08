angular.module('NoteWrangler').directive('title', function (Category) {
	return function(scope, element, attrs){	
		/*$timeout(function(){
			$(element).tooltip();
		});	*/
		
		scope.$on('$destroy',function(){
			$(element).tooltip('destroy');
		});
	};
});