//import { RouterModule, Routes, providerRoutes, RouterConfig } from '@angular/router';
"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var router_1 = require("@angular/router");
var user_component_1 = require("./user/user.component");
var home_component_component_1 = require("./home-component.component");
var user_routes_1 = require("./user/user.routes");
var data_driven_component_1 = require("./11_Forms/data-driven/data-driven.component");
var template_driven_component_1 = require("./11_Forms/template-driven/template-driven.component");
var APP_ROUTES = [
    { path: 'user', redirectTo: '/user/1', pathMatch: 'full' },
    { path: 'user/:id', component: user_component_1.UserComponent },
    { path: 'user/:id', component: user_component_1.UserComponent, children: user_routes_1.USER_ROUTES },
    //{ path: '', component: HomeComponent }
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: home_component_component_1.HomeComponent },
    //11 Forms
    { path: 'formsData', component: data_driven_component_1.DataDrivenComponent },
    { path: 'formsTemplate', component: template_driven_component_1.TemplateDrivenComponent },
    { path: '**', redirectTo: '/user/2', pathMatch: 'full' }
];
exports.routing = router_1.RouterModule.forRoot(APP_ROUTES);
//export const APP_ROUTES_PROVIDER = [
//    provideRoutes(APP_ROUTES)
//]; 
//# sourceMappingURL=app.routing.js.map