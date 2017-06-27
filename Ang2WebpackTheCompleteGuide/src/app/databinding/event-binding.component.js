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
var EventBindingComponent = (function () {
    function EventBindingComponent() {
        this.clicked = new core_1.EventEmitter();
    }
    EventBindingComponent.prototype.onClicked = function () {
        this.clicked.emit('It works!');
    };
    return EventBindingComponent;
}());
__decorate([
    core_1.Output(),
    __metadata("design:type", Object)
], EventBindingComponent.prototype, "clicked", void 0);
EventBindingComponent = __decorate([
    core_1.Component({
        selector: 'fa-event-binding',
        template: "\n    <button (click)=\"onClicked()\">Click Button!</button>\n    <input type='button' (click)=\"onClicked()\" value=\"Click Input Element!\" />\n  ",
        styles: []
    })
], EventBindingComponent);
exports.EventBindingComponent = EventBindingComponent;
//# sourceMappingURL=event-binding.component.js.map