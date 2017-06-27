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
var http_1 = require("@angular/http");
require("rxjs/Rx");
var Rx_1 = require("rxjs/Rx");
var HttpService = (function () {
    function HttpService(http) {
        this.http = http;
        this.hostweburl = decodeURIComponent(this.manageQueryStringParameter("SPHostUrl"));
        this.appweburl = decodeURIComponent(this.manageQueryStringParameter("SPAppWebUrl"));
        this.spListName = 'Demo';
        console.log(this.hostweburl);
        console.log(this.appweburl);
        this.spApiUrl = this.appweburl + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('Demo')/items?" +
            "@target='" + this.hostweburl + "'";
    }
    HttpService.prototype.getData = function () {
        return this.http.get(this.spApiUrl, { headers: this.getHeaders() })
            .map(function (response) { return response.json(); });
    };
    HttpService.prototype.getOwnData = function () {
        return this.http.get(this.spApiUrl, { headers: this.getHeaders() })
            .map(function (response) { return response.json(); });
    };
    HttpService.prototype.sendData = function (username, email) {
        var obj = {
            '__metadata': { 'type': "SP.Data." + this.spListName + "ListItem" },
            'Title': 'Test',
            'username': username,
            'email': email
        };
        var data = JSON.stringify(obj);
        return this.http.post(this.spApiUrl, data, { headers: this.getHeaders("POST") })
            .map(function (data) { return data.json(); })
            .catch(this.handleError);
    };
    HttpService.prototype.handleError = function (error) {
        console.log(error);
        return Rx_1.Observable.throw(error.json());
    };
    HttpService.prototype.manageQueryStringParameter = function (paramToRetrieve) {
        var params = document.URL.split("?")[1].split("&");
        //var strParams = "";
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0] == paramToRetrieve) {
                return singleParam[1];
            }
        }
    };
    // GET HEADERS, esta función resuelve las cabeceras segun el verbo que estemos utilizando
    HttpService.prototype.getHeaders = function (verb) {
        var headers = new http_1.Headers();
        //var digest = document.getElementById('__REQUESTDIGEST').value;
        var digest = document.getElementById('__REQUESTDIGEST').value;
        headers.set('X-RequestDigest', digest);
        headers.set('Accept', 'application/json;odata=verbose');
        switch (verb) {
            case "POST":
                headers.set('Content-type', 'application/json;odata=verbose');
                break;
            case "PUT":
                headers.set('Content-type', 'application/json;odata=verbose');
                headers.set("IF-MATCH", "*");
                headers.set("X-HTTP-Method", "MERGE");
                break;
            case "DELETE":
                headers.set("IF-MATCH", "*");
                headers.set("X-HTTP-Method", "DELETE");
                break;
        }
        return headers;
    };
    return HttpService;
}());
HttpService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], HttpService);
exports.HttpService = HttpService;
//# sourceMappingURL=http.service.js.map