"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var platform_browser_1 = require("@angular/platform-browser");
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var http_1 = require("@angular/http");
var app_component_1 = require("./app.component");
//import { OtherComponent } from './other/other.component';
//import { AnotherComponent } from './another.component';
var databinding_component_1 = require("./databinding/databinding.component");
var event_binding_component_1 = require("./databinding/event-binding.component");
var property_binding_component_1 = require("./databinding/property-binding.component");
var two_way_binding_component_1 = require("./databinding/two-way-binding.component");
var lifecycle_component_1 = require("./lifecycle.component");
var directives_component_1 = require("./directives/directives.component");
var highlight_directive_1 = require("./directives/highlight.directive");
var unless_directive_1 = require("./directives/unless.directive");
var cmp_b_component_1 = require("./services/cmp-b.component");
var cmp_a_component_1 = require("./services/cmp-a.component");
var log_service_1 = require("./services/log.service");
var user_component_1 = require("./user/user.component");
var home_component_component_1 = require("./home-component.component");
var app_routing_1 = require("./app.routing");
////////////////  import { APP_BASE_HREF } from '@angular/common'; //provide: APP_BASE_HREF, useValue: '/'
////////////////  import { LocationStrategy, HashLocationStrategy } from '@angular/common'; //provide: LocationStrategy, useClass: HashLocationStrategy
var user_edit_component_1 = require("./user/user-edit.component");
var user_detail_component_1 = require("./user/user-detail.component");
var user_detail_guard_1 = require("./user/user-detail.guard");
var user_edit_guard_1 = require("./user/user-edit.guard");
var data_driven_component_1 = require("./11_Forms/data-driven/data-driven.component");
var template_driven_component_1 = require("./11_Forms/template-driven/template-driven.component");
var pipes_component_1 = require("./pipes/pipes.component");
var double_pipe_1 = require("./pipes/double.pipe");
var filter_pipe_1 = require("./pipes/filter.pipe");
var http_component_1 = require("./http/http.component");
var http_service_1 = require("./http/http.service");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        declarations: [
            app_component_1.AppComponent,
            //OtherComponent,
            //AnotherComponent,
            databinding_component_1.DatabindingComponent,
            event_binding_component_1.EventBindingComponent,
            property_binding_component_1.PropertyBindingComponent,
            two_way_binding_component_1.TwoWayBindingComponent,
            lifecycle_component_1.LifecycleComponent,
            directives_component_1.DirectivesComponent,
            highlight_directive_1.HighlightDirective,
            unless_directive_1.UnlessDirective,
            cmp_b_component_1.CmpBComponent,
            cmp_a_component_1.CmpAComponent,
            user_component_1.UserComponent,
            home_component_component_1.HomeComponent,
            user_edit_component_1.UserEditComponent,
            user_detail_component_1.UserDetailComponent,
            data_driven_component_1.DataDrivenComponent,
            template_driven_component_1.TemplateDrivenComponent,
            pipes_component_1.PipesComponent,
            double_pipe_1.DoublePipe,
            filter_pipe_1.FilterPipe,
            http_component_1.HttpComponent
        ],
        imports: [
            platform_browser_1.BrowserModule,
            forms_1.FormsModule,
            http_1.HttpModule,
            forms_1.ReactiveFormsModule,
            app_routing_1.routing
        ],
        //providers: [LogService],
        providers: [
            log_service_1.LogService //, {
            //    provide: APP_BASE_HREF, useValue: '/sites/dev/Ang2WebpackTheCompleteGuide/Pages/Default.aspx'
            //provide: LocationStrategy, useClass: HashLocationStrategy
            //}
            ,
            user_detail_guard_1.UserDetailGuard,
            user_edit_guard_1.UserEditGuard,
            http_service_1.HttpService
        ],
        bootstrap: [app_component_1.AppComponent]
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map