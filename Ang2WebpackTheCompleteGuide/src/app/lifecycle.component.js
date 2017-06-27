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
var LifecycleComponent = (function () {
    function LifecycleComponent() {
        this.bindable = 1000;
    }
    LifecycleComponent.prototype.ngOnChanges = function () {
        this.log('1 ngOnChanges');
    };
    LifecycleComponent.prototype.ngOnInit = function () {
        this.log('2 ngOnInit');
    };
    LifecycleComponent.prototype.ngDoCheck = function () {
        this.log('3 ngDoCheck');
    };
    LifecycleComponent.prototype.ngAfterContentInit = function () {
        this.log('4 ngAfterContentInit');
        console.log(this.boundContent);
    };
    LifecycleComponent.prototype.ngAfterContentChecked = function () {
        this.log('5 ngAfterContentChecked');
    };
    LifecycleComponent.prototype.ngAfterViewInit = function () {
        this.log('6 ngAfterViewInit');
        console.log(this.boundParagraph);
    };
    LifecycleComponent.prototype.ngAfterViewChecked = function () {
        this.log('7 ngAfterViewChecked');
    };
    LifecycleComponent.prototype.ngOnDestroy = function () {
        this.log('8 ngOnDestroy');
    };
    LifecycleComponent.prototype.log = function (hook) {
        console.log(hook);
    };
    return LifecycleComponent;
}());
__decorate([
    core_1.Input(),
    __metadata("design:type", Object)
], LifecycleComponent.prototype, "bindable", void 0);
__decorate([
    core_1.ViewChild('boundParagraph'),
    __metadata("design:type", HTMLElement)
], LifecycleComponent.prototype, "boundParagraph", void 0);
__decorate([
    core_1.ContentChild('boundContent'),
    __metadata("design:type", HTMLElement)
], LifecycleComponent.prototype, "boundContent", void 0);
LifecycleComponent = __decorate([
    core_1.Component({
        selector: 'fa-lifecycle',
        template: "\n    <ng-content></ng-content>\n    <hr>\n    <p #boundParagraph>{{bindable}}</p>\n  ",
        styles: []
    }),
    __metadata("design:paramtypes", [])
], LifecycleComponent);
exports.LifecycleComponent = LifecycleComponent;
//# sourceMappingURL=lifecycle.component.js.map