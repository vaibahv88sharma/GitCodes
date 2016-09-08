angular.module('NoteWrangler').directive('nwCategorySelector', function (Category) {
	return {
		replace : true,
		restrict : "E",
		require : "?ngModel",
		templateUrl : "../HTML/Templates/directives/nwCategorySelector.html",
		link : function(scope, element, attrs, ngModelCtrl){	
			var activeCategory = {};
					
			Category.getAllCategoryResource().then(function(data){
				scope.categories = data.d.results;				
			});
			
			scope.isActive = function(category){
				return activeCategory && activeCategory.ID === category.ID;
			}
			
			scope.toggleCategory = function(category){
				if(category === activeCategory){
				activeCategory = {};
				}
				else{
					activeCategory = category;					
				}
				ngModelCtrl.$setViewValue(activeCategory);
			}
			
			ngModelCtrl.$render = function(){
				activeCategory = ngModelCtrl.$viewState;
			}
		}
	};
});