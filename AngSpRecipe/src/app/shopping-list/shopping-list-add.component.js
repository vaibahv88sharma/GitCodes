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
var ingredient_1 = require("../recipes/ingredient");
var shopping_list_service_1 = require("./shopping-list.service");
var ShoppingListAddComponent = (function () {
    function ShoppingListAddComponent(sls) {
        this.sls = sls;
        this.isAdd = true;
        this.cleared = new core_1.EventEmitter();
    }
    ShoppingListAddComponent.prototype.ngOnChanges = function (changes) {
        if (changes.item.currentValue === null) {
            this.isAdd = true;
            this.item = { name: null, amount: null };
        }
        else {
            this.isAdd = false;
        }
    };
    ShoppingListAddComponent.prototype.onSubmit = function (ingredient) {
        var newIngredient = new ingredient_1.Ingredient(ingredient.name, ingredient.amount);
        if (!this.isAdd) {
            this.sls.editItem(this.item, newIngredient);
            this.onClear();
        }
        else {
            this.item = newIngredient;
            this.sls.addItem(this.item);
        }
    };
    ShoppingListAddComponent.prototype.onDelete = function () {
        this.sls.deleteItem(this.item);
        this.onClear();
    };
    ShoppingListAddComponent.prototype.onClear = function () {
        this.isAdd = true;
        this.cleared.emit(null);
    };
    return ShoppingListAddComponent;
}());
__decorate([
    core_1.Input(),
    __metadata("design:type", ingredient_1.Ingredient)
], ShoppingListAddComponent.prototype, "item", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", Object)
], ShoppingListAddComponent.prototype, "cleared", void 0);
ShoppingListAddComponent = __decorate([
    core_1.Component({
        selector: 'rb-shopping-list-add',
        templateUrl: './shopping-list-add.component.html',
        styleUrls: ['./shopping-list-add.component.css']
    }),
    __metadata("design:paramtypes", [shopping_list_service_1.ShoppingListService])
], ShoppingListAddComponent);
exports.ShoppingListAddComponent = ShoppingListAddComponent;
//# sourceMappingURL=shopping-list-add.component.js.map