import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
//import { OtherComponent } from './other/other.component';
//import { AnotherComponent } from './another.component';
import { DatabindingComponent } from './databinding/databinding.component';
import { EventBindingComponent } from './databinding/event-binding.component';
import { PropertyBindingComponent } from './databinding/property-binding.component';
import { TwoWayBindingComponent } from './databinding/two-way-binding.component';
import { LifecycleComponent } from './lifecycle.component';
import { DirectivesComponent } from './directives/directives.component';
import { HighlightDirective } from './directives/highlight.directive';
import { UnlessDirective } from './directives/unless.directive';
import { CmpBComponent } from './services/cmp-b.component';
import { CmpAComponent } from './services/cmp-a.component';
import { LogService } from './services/log.service';
import { UserComponent } from './user/user.component';
import { HomeComponent } from './home-component.component';
import { routing } from "./app.routing";

////////////////  import { APP_BASE_HREF } from '@angular/common'; //provide: APP_BASE_HREF, useValue: '/'
////////////////  import { LocationStrategy, HashLocationStrategy } from '@angular/common'; //provide: LocationStrategy, useClass: HashLocationStrategy

import { UserEditComponent } from './user/user-edit.component';
import { UserDetailComponent } from './user/user-detail.component';
import { UserDetailGuard } from "./user/user-detail.guard";
import { UserEditGuard } from "./user/user-edit.guard";
import { DataDrivenComponent } from "./11_Forms/data-driven/data-driven.component";
import { TemplateDrivenComponent } from "./11_Forms/template-driven/template-driven.component";
import { PipesComponent } from "./pipes/pipes.component";
import { DoublePipe } from "./pipes/double.pipe";
import { FilterPipe } from "./pipes/filter.pipe";
import { HttpComponent } from "./http/http.component";
import { HttpService } from "./http/http.service";

@NgModule({
  declarations: [
    AppComponent,
    //OtherComponent,
    //AnotherComponent,
    DatabindingComponent,
    EventBindingComponent,
    PropertyBindingComponent,
    TwoWayBindingComponent,
    LifecycleComponent,
    DirectivesComponent,
    HighlightDirective,
    UnlessDirective,
    CmpBComponent,
      CmpAComponent,
      UserComponent,
      HomeComponent,
      UserEditComponent,
      UserDetailComponent,
      DataDrivenComponent,
      TemplateDrivenComponent,
      PipesComponent,
      DoublePipe,
      FilterPipe,
      HttpComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
      HttpModule,
      ReactiveFormsModule,
      routing      
  ],
  //providers: [LogService],
  providers: [
      LogService//, {
      //    provide: APP_BASE_HREF, useValue: '/sites/dev/Ang2WebpackTheCompleteGuide/Pages/Default.aspx'
          //provide: LocationStrategy, useClass: HashLocationStrategy
      //}
      , UserDetailGuard,
      UserEditGuard,
      HttpService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
