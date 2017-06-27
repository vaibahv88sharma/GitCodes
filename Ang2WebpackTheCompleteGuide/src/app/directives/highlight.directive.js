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
//import {Directive, ElementRef, Renderer} from '@angular/core';
var HighlightDirective = (function () {
    function HighlightDirective() {
        this.defaultColor = 'white';
        this.highlightColor = 'green';
    }
    HighlightDirective.prototype.mouseover = function () {
        this.backgroundColor = this.highlightColor;
    };
    ;
    HighlightDirective.prototype.mouseleave = function () {
        this.backgroundColor = this.defaultColor;
    };
    ;
    Object.defineProperty(HighlightDirective.prototype, "setColor", {
        get: function () {
            return this.backgroundColor;
        },
        enumerable: true,
        configurable: true
    });
    ;
    //constructor(private elementRef: ElementRef, private renderer: Renderer) {
    //    //this.elementRef.nativeElement.style.backgroundColor = 'green';
    //    this.renderer.setElementStyle(this.elementRef.nativeElement, 'background-color', 'green');
    //}
    HighlightDirective.prototype.ngOnInit = function () {
        this.backgroundColor = this.defaultColor;
    };
    return HighlightDirective;
}());
__decorate([
    core_1.HostListener('mouseenter'),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", void 0)
], HighlightDirective.prototype, "mouseover", null);
__decorate([
    core_1.HostListener('mouseleave'),
    __metadata("design:type", Function),
    __metadata("design:paramtypes", []),
    __metadata("design:returntype", void 0)
], HighlightDirective.prototype, "mouseleave", null);
__decorate([
    core_1.HostBinding('style.backgroundColor'),
    __metadata("design:type", Object),
    __metadata("design:paramtypes", [])
], HighlightDirective.prototype, "setColor", null);
__decorate([
    core_1.Input(),
    __metadata("design:type", Object)
], HighlightDirective.prototype, "defaultColor", void 0);
__decorate([
    core_1.Input('highlight'),
    __metadata("design:type", Object)
], HighlightDirective.prototype, "highlightColor", void 0);
HighlightDirective = __decorate([
    core_1.Directive({
        selector: '[highlight]'
    }),
    __metadata("design:paramtypes", [])
], HighlightDirective);
exports.HighlightDirective = HighlightDirective;
//# sourceMappingURL=highlight.directive.js.map