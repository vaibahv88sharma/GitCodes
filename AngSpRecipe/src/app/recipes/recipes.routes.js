"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var recipe_start_component_1 = require("./recipe-start.component");
var recipe_detail_component_1 = require("./recipe-detail/recipe-detail.component");
var recipe_edit_component_1 = require("./recipe-edit/recipe-edit.component");
exports.RECIPE_ROUTES = [
    { path: '', component: recipe_start_component_1.RecipeStartComponent },
    { path: 'new', component: recipe_edit_component_1.RecipeEditComponent },
    { path: ':id', component: recipe_detail_component_1.RecipeDetailComponent },
    { path: ':id/edit', component: recipe_edit_component_1.RecipeEditComponent }
];
//# sourceMappingURL=recipes.routes.js.map