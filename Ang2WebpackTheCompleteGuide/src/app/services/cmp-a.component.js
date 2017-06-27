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
var log_service_1 = require("./log.service");
var data_service_1 = require("./data.service");
var CmpAComponent = (function () {
    function CmpAComponent(logService, dataService) {
        this.logService = logService;
        this.dataService = dataService;
        this.value = '';
        this.items = [];
    }
    CmpAComponent.prototype.onLog = function (value) {
        this.logService.writeToLog(value);
    };
    CmpAComponent.prototype.onStore = function (value) {
        this.dataService.addData(value);
    };
    CmpAComponent.prototype.onGet = function () {
        this.items = this.dataService.getData().slice(0);
    };
    CmpAComponent.prototype.onSend = function (value) {
        this.dataService.pushData(value);
    };
    CmpAComponent.prototype.ngOnInit = function () {
    };
    return CmpAComponent;
}());
CmpAComponent = __decorate([
    core_1.Component({
        selector: 'app-cmp-a',
        templateUrl: './cmp-a.component.html',
        styleUrls: ['./cmp-a.component.css'] //,
        //providers: [LogService, DataService]
    }),
    __metadata("design:paramtypes", [log_service_1.LogService, data_service_1.DataService])
], CmpAComponent);
exports.CmpAComponent = CmpAComponent;
//# sourceMappingURL=cmp-a.component.js.map