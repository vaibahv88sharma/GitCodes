angular.module('homeModule').directive('homeThumbnails', function () {
    return {
        //replace: true,
        restrict: "E",
        scope:false,
        //scope: {
        //    image: "=",
        //    title: "=",
        //    description: "=",
        //},
        //require: "?ngModel",
        templateUrl: "template/directive/homeThumbnails.html",


        //link: function (scope, element, attrs, ngModelCtrl) {
        //    var activeCategory = {};

        //    Category.getAllCategoryResource().then(function (data) {
        //        scope.categories = data.d.results;
        //    });

        //    scope.isActive = function (category) {
        //        return activeCategory && activeCategory.ID === category.ID;
        //    }

        //    scope.toggleCategory = function (category) {
        //        if (category === activeCategory) {
        //            activeCategory = {};
        //        }
        //        else {
        //            activeCategory = category;
        //        }
        //        ngModelCtrl.$setViewValue(activeCategory);
        //    }

        //    ngModelCtrl.$render = function () {
        //        activeCategory = ngModelCtrl.$viewState;
        //    }

        //    //<div class="nw-category-selector btn-group" role="group">
        //    //	<a 
        //    //		href=""
        //    //		ng-repeat="category in categories"
        //    //		class="btn btn-default"
        //    //		ng-class="{'active':isActive(category)}"
        //    //		ng-click="toggleCategory(category)">
        //    //			{{category.category}}		
        //    //	</a>
        //    //</div>

        //}


    };
});