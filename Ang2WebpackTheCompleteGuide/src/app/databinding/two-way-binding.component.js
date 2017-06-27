"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var TwoWayBindingComponent = (function () {
    function TwoWayBindingComponent() {
        this.person = {
            name: 'Max',
            age: 27
        };
    }
    return TwoWayBindingComponent;
}());
TwoWayBindingComponent = __decorate([
    core_1.Component({
        selector: 'fa-two-way-binding',
        template: "\n    <input type=\"text\" [(ngModel)]=\"person.name\">\n    <input type=\"text\" [(ngModel)]=\"person.name\">\n  ",
        styles: []
    })
], TwoWayBindingComponent);
exports.TwoWayBindingComponent = TwoWayBindingComponent;
//# sourceMappingURL=two-way-binding.component.js.map