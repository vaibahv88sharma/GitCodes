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
var TemplateDrivenComponent = (function () {
    function TemplateDrivenComponent() {
        this.user = {
            username: 'test',
            email: 'test@gmail.com',
            password: '1234',
            gender: 'male'
        };
        this.genders = [
            'male',
            'female'
        ];
    }
    TemplateDrivenComponent.prototype.onSubmit = function (form) {
        console.log(form.value);
        console.log(this.user);
    };
    TemplateDrivenComponent.prototype.ngOnInit = function () {
    };
    return TemplateDrivenComponent;
}());
TemplateDrivenComponent = __decorate([
    core_1.Component({
        selector: 'app-template-driven',
        templateUrl: './template-driven.component.html',
        styleUrls: ['./template-driven.component.css']
    }),
    __metadata("design:paramtypes", [])
], TemplateDrivenComponent);
exports.TemplateDrivenComponent = TemplateDrivenComponent;
//# sourceMappingURL=template-driven.component.js.map