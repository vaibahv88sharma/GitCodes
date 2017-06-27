"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var platform_browser_dynamic_1 = require("@angular/platform-browser-dynamic");
var core_1 = require("@angular/core");
//import { LogService } from './app/services/log.service';
var app_module_1 = require("./app/app.module");
if (process.env.ENV === 'production') {
    core_1.enableProdMode();
}
platform_browser_dynamic_1.platformBrowserDynamic().bootstrapModule(app_module_1.AppModule);
//platformBrowserDynamic().bootstrapModule(AppModule, [LogService]);
//# sourceMappingURL=main.js.map