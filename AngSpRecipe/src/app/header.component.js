"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var recipe_service_1 = require("./recipes/recipe.service");
var HeaderComponent = (function () {
    function HeaderComponent(recipeService) {
        this.recipeService = recipeService;
        this.recipesPosted = [];
    }
    HeaderComponent.prototype.ngOnInit = function () {
    };
    HeaderComponent.prototype.onStore = function () {
        var _this = this;
        for (var i = 0; i < this.recipeService.recipes.length; i++) {
            //this.recipesPosted = [];
            var obj = {
                '__metadata': {
                    'type': this.recipeService.getItemTypeForListName('recipe')
                },
                'recipe': this.recipeService.recipes[i]['name'],
                'image': this.recipeService.recipes[i]['imagePath'],
                'description': this.recipeService.recipes[i]['description']
            };
            this.body = JSON.stringify(obj);
            this.recipeService.storeData(
            //this.recipeService.recipes[i],
            this.body, 'recipe').subscribe(function (data) { return _this.createIngredientsLookup(data); }, function (error) { return console.error(error); });
        }
    };
    HeaderComponent.prototype.createIngredientsLookup = function (data) {
        console.log(data);
        for (var i = 0; i < this.recipeService.recipes.length; i++) {
            if (this.recipeService.recipes[i]['name'] === data.d.recipe) {
                for (var j = 0; j < this.recipeService.recipes[i]["ingredients"].length; j++) {
                    var obj = {
                        '__metadata': {
                            'type': this.recipeService.getItemTypeForListName('ingredient')
                        },
                        'name': this.recipeService.recipes[i]["ingredients"][j]["name"],
                        'amount': this.recipeService.recipes[i]["ingredients"][j]["amount"],
                        'recipeId': data.d.Id,
                    };
                    this.body = JSON.stringify(obj);
                    this.recipeService.storeData(
                    //this.recipeService.recipes[i],
                    this.body, 'ingredient').subscribe(function (data) { return console.log(data); }, function (error) { return console.error(error); });
                }
            }
        }
        //this.recipesPosted.push(
        //    new Recipe(data.d.recipe, "", "", data.d.Id, [])
        //);
    };
    HeaderComponent.prototype.onFetch = function () {
        this.recipeService.fetchData('ingredient');
    };
    return HeaderComponent;
}());
HeaderComponent = __decorate([
    core_1.Component({
        selector: 'rb-header',
        templateUrl: './header.component.html',
        styleUrls: ['./header.component.css']
    }),
    __metadata("design:paramtypes", [recipe_service_1.RecipeService])
], HeaderComponent);
exports.HeaderComponent = HeaderComponent;
//# sourceMappingURL=header.component.js.map