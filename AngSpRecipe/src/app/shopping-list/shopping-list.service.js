"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
//@Injectable()
var ShoppingListService = (function () {
    function ShoppingListService() {
        this.items = [];
    }
    ShoppingListService.prototype.getItems = function () {
        return this.items;
    };
    ShoppingListService.prototype.addItems = function (items) {
        Array.prototype.push.apply(this.items, items);
    };
    ShoppingListService.prototype.addItem = function (item) {
        this.items.push(item);
    };
    ShoppingListService.prototype.editItem = function (oldItem, newItem) {
        this.items[this.items.indexOf(oldItem)] = newItem;
    };
    ShoppingListService.prototype.deleteItem = function (item) {
        this.items.splice(this.items.indexOf(item), 1);
    };
    return ShoppingListService;
}());
exports.ShoppingListService = ShoppingListService;
//# sourceMappingURL=shopping-list.service.js.map