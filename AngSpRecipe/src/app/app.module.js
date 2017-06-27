"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var platform_browser_1 = require("@angular/platform-browser");
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/http");
var app_component_1 = require("./app.component");
var header_component_1 = require("./header.component");
var recipes_component_1 = require("./recipes/recipes.component");
var recipe_list_component_1 = require("./recipes/recipe-list/recipe-list.component");
var recipe_item_component_1 = require("./recipes/recipe-list/recipe-item.component");
var recipe_detail_component_1 = require("./recipes/recipe-detail/recipe-detail.component");
var shopping_list_component_1 = require("./shopping-list/shopping-list.component");
var shopping_list_add_component_1 = require("./shopping-list/shopping-list-add.component");
var dropdown_directive_1 = require("./dropdown.directive");
var shopping_list_service_1 = require("./shopping-list/shopping-list.service");
var app_routes_1 = require("./app.routes");
var recipe_start_component_1 = require("./recipes/recipe-start.component");
var recipe_edit_component_1 = require("./recipes/recipe-edit/recipe-edit.component");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        declarations: [
            app_component_1.AppComponent,
            header_component_1.HeaderComponent,
            recipes_component_1.RecipesComponent,
            recipe_list_component_1.RecipeListComponent,
            recipe_item_component_1.RecipeItemComponent,
            recipe_detail_component_1.RecipeDetailComponent,
            shopping_list_component_1.ShoppingListComponent,
            shopping_list_add_component_1.ShoppingListAddComponent,
            dropdown_directive_1.DropdownDirective,
            recipe_start_component_1.RecipeStartComponent,
            recipe_edit_component_1.RecipeEditComponent
        ],
        imports: [
            platform_browser_1.BrowserModule,
            forms_1.FormsModule,
            http_1.HttpModule,
            forms_1.ReactiveFormsModule,
            app_routes_1.routing
        ],
        providers: [shopping_list_service_1.ShoppingListService],
        bootstrap: [app_component_1.AppComponent]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map