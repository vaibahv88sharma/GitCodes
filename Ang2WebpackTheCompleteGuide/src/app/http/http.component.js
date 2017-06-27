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
var http_service_1 = require("./http.service");
var HttpComponent = (function () {
    function HttpComponent(httpService) {
        this.httpService = httpService;
        this.items = [];
        this.asyncString = this.httpService.getData();
    }
    HttpComponent.prototype.ngOnInit = function () {
        this.httpService.getData()
            .subscribe(function (data) { return console.log(data); });
    };
    HttpComponent.prototype.onSubmit = function (username, email) {
        //this.httpService.sendData({ username: username, email: email })
        this.httpService.sendData(username, email)
            .subscribe(function (data) { return console.log(data); }, function (error) { return console.log(error); });
    };
    HttpComponent.prototype.onGetData = function () {
        var _this = this;
        this.httpService.getOwnData()
            .subscribe(function (data) {
            var myArray = [];
            for (var i = 0; i < data.d.results.length; i++) {
                //for (let key in data) {
                myArray.push(data.d.results[i]);
            }
            _this.items = myArray;
        });
    };
    return HttpComponent;
}());
HttpComponent = __decorate([
    core_1.Component({
        selector: 'app-http',
        templateUrl: './http.component.html',
        styleUrls: ['./http.component.css']
    }),
    __metadata("design:paramtypes", [http_service_1.HttpService])
], HttpComponent);
exports.HttpComponent = HttpComponent;
//# sourceMappingURL=http.component.js.map