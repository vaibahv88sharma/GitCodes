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
var http_1 = require("@angular/http");
var Rx_1 = require("rxjs/Rx");
var recipe_1 = require("./recipe");
var ingredient_1 = require("./ingredient");
//import { REST } from "../shared/REST";
var RecipeService = (function () {
    function RecipeService(http) {
        //REST
        // this.spApiUrl = this.appweburl + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('Demo')/items?" +
        //    "@target='" + this.hostweburl + "'";
        this.http = http;
        this.recipes = [
            new recipe_1.Recipe('Biryani', 'Very Spicy', '//staffportal.myselfserve.com.au/sites/dev/Style%20Library/App/Vegetable-biriyani.jpg', '', [
                new ingredient_1.Ingredient('Rice', 2),
                new ingredient_1.Ingredient('Vegitable', 1),
            ]),
            new recipe_1.Recipe('Cauliflower Potato', 'Tasty', '//staffportal.myselfserve.com.au/sites/dev/Style%20Library/App/Cauliflower-Potato.jpg', '', [])
        ];
        // REST
        this.hostweburl = decodeURIComponent(this.manageQueryStringParameter("SPHostUrl"));
        this.appweburl = decodeURIComponent(this.manageQueryStringParameter("SPAppWebUrl"));
        this.spListName = 'recipe';
    }
    RecipeService.prototype.getRecipes = function () {
        return this.recipes;
    };
    RecipeService.prototype.getRecipe = function (id) {
        return this.recipes[id];
    };
    RecipeService.prototype.deleteRecipe = function (recipe) {
        this.recipes.splice(this.recipes.indexOf(recipe), 1);
    };
    RecipeService.prototype.addRecipe = function (recipe) {
        this.recipes.push(recipe);
    };
    RecipeService.prototype.editRecipe = function (oldRecipe, newRecipe) {
        this.recipes[this.recipes.indexOf(oldRecipe)] = newRecipe;
    };
    //storeData(recipeItems: Recipe, listName: string) {
    RecipeService.prototype.storeData = function (body, listName) {
        var headers = this.getHeaders();
        return this.http.post(this.getURL(listName), body, { headers: this.getHeaders("POST") })
            .map(function (data) { return data.json(); })
            .catch(this.handleError);
    };
    RecipeService.prototype.fetchData = function (listName) {
        var _this = this;
        return this.http.get(this.getURL(listName) + "&$select=name,amount,recipe/recipe,recipe/image,recipe/description&$expand=recipe/recipe", { headers: this.getHeaders() })
            .map(function (response) { return response.json(); })
            .catch(this.handleError)
            .subscribe(function (data) {
            _this.recipes = data.d.results;
        });
    };
    // REST Error Handling
    RecipeService.prototype.handleError = function (error) {
        console.log(error);
        return Rx_1.Observable.throw(error.json());
    };
    // List URL
    RecipeService.prototype.getURL = function (listName) {
        return this.spApiUrl = this.appweburl + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + listName + "')/items?" +
            "@target='" + this.hostweburl + "'";
    };
    //REST GET HEADERS, this function resolves the headers according to the verb that we are using
    RecipeService.prototype.getHeaders = function (verb) {
        var headers = new http_1.Headers();
        //var digest = document.getElementById('__REQUESTDIGEST').value;
        var digest = document.getElementById('__REQUESTDIGEST').value;
        headers.set('X-RequestDigest', digest);
        headers.set('Accept', 'application/json;odata=verbose');
        switch (verb) {
            case "POST":
                headers.set('Content-type', 'application/json;odata=verbose');
                break;
            case "PUT":
                headers.set('Content-type', 'application/json;odata=verbose');
                headers.set("IF-MATCH", "*");
                headers.set("X-HTTP-Method", "MERGE");
                break;
            case "DELETE":
                headers.set("IF-MATCH", "*");
                headers.set("X-HTTP-Method", "DELETE");
                break;
        }
        return headers;
    };
    // REST URL
    RecipeService.prototype.manageQueryStringParameter = function (paramToRetrieve) {
        var params = document.URL.split("?")[1].split("&");
        //var strParams = "";
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0] == paramToRetrieve) {
                return singleParam[1];
            }
        }
    };
    // REST SP.Data.List
    RecipeService.prototype.getItemTypeForListName = function (name) {
        return "SP.Data." + name.charAt(0).toUpperCase() + name.split(" ").join("").slice(1) + "ListItem";
    };
    return RecipeService;
}());
RecipeService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], RecipeService);
exports.RecipeService = RecipeService;
//# sourceMappingURL=recipe.service.js.map