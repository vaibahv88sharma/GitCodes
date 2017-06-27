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
var router_1 = require("@angular/router");
var forms_1 = require("@angular/forms");
var recipe_service_1 = require("../recipe.service");
var RecipeEditComponent = (function () {
    function RecipeEditComponent(route, recipeService, formBuilder, router) {
        this.route = route;
        this.recipeService = recipeService;
        this.formBuilder = formBuilder;
        this.router = router;
        this.isNew = true;
    }
    RecipeEditComponent.prototype.ngOnInit = function () {
        var _this = this;
        this.subscription = this.route.params.subscribe(function (params) {
            if (params.hasOwnProperty('id')) {
                _this.isNew = false;
                _this.recipeIndex = +params['id'];
                _this.recipe = _this.recipeService.getRecipe(_this.recipeIndex);
            }
            else {
                _this.isNew = true;
                _this.recipe = null;
            }
            _this.initForm();
        });
    };
    RecipeEditComponent.prototype.onSubmit = function () {
        var newRecipe = this.recipeForm.value;
        if (this.isNew) {
            this.recipeService.addRecipe(newRecipe);
        }
        else {
            this.recipeService.editRecipe(this.recipe, newRecipe);
        }
        this.navigateBack();
    };
    RecipeEditComponent.prototype.onCancel = function () {
        this.navigateBack();
    };
    RecipeEditComponent.prototype.onAddItem = function (name, amount) {
        this.recipeForm.controls['ingredients'].push(new forms_1.FormGroup({
            name: new forms_1.FormControl(name, forms_1.Validators.required),
            amount: new forms_1.FormControl(amount, [
                forms_1.Validators.required,
                forms_1.Validators.pattern("\\d+")
            ])
        }));
    };
    RecipeEditComponent.prototype.onRemoveItem = function (index) {
        this.recipeForm.controls['ingredients'].removeAt(index);
    };
    RecipeEditComponent.prototype.ngOnDestroy = function () {
        this.subscription.unsubscribe();
    };
    RecipeEditComponent.prototype.navigateBack = function () {
        this.router.navigate(['../']);
    };
    RecipeEditComponent.prototype.initForm = function () {
        var recipeName = '';
        var recipeImageUrl = '';
        var recipeContent = '';
        var recipeIngredients = new forms_1.FormArray([]);
        if (!this.isNew) {
            if (this.recipe.hasOwnProperty('ingredients')) {
                for (var i = 0; i < this.recipe.ingredients.length; i++) {
                    recipeIngredients.push(new forms_1.FormGroup({
                        name: new forms_1.FormControl(this.recipe.ingredients[i].name, forms_1.Validators.required),
                        amount: new forms_1.FormControl(this.recipe.ingredients[i].amount, [
                            forms_1.Validators.required,
                            forms_1.Validators.pattern("\\d+")
                        ])
                    }));
                }
            }
            recipeName = this.recipe.name;
            recipeImageUrl = this.recipe.imagePath;
            recipeContent = this.recipe.description;
        }
        this.recipeForm = this.formBuilder.group({
            name: [recipeName, forms_1.Validators.required],
            imagePath: [recipeImageUrl, forms_1.Validators.required],
            description: [recipeContent, forms_1.Validators.required],
            ingredients: recipeIngredients
        });
    };
    return RecipeEditComponent;
}());
RecipeEditComponent = __decorate([
    core_1.Component({
        selector: 'rb-recipe-edit',
        templateUrl: './recipe-edit.component.html',
        styles: []
    }),
    __metadata("design:paramtypes", [router_1.ActivatedRoute,
        recipe_service_1.RecipeService,
        forms_1.FormBuilder,
        router_1.Router])
], RecipeEditComponent);
exports.RecipeEditComponent = RecipeEditComponent;
//# sourceMappingURL=recipe-edit.component.js.map