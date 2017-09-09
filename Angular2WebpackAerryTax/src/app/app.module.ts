import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { HttpModule } from '@angular/http';
import { RouterModule }  from '@angular/router';
import { FormsModule }  from '@angular/forms'; // Template Driven Forms
import { ReactiveFormsModule }  from '@angular/forms'; // Reactive Forms

import { AppRoutingModule } from './app.routing';
import { AppComponent } from './app.component';
import { MainNavigationComponent } from './main-navigation/main-navigation.component';
import { HomeModule } from './home/home.module';
import { BannerTopComponent } from './banner-top/banner-top.component';
//import { AboutComponent } from './about/about.component';
//import { DisclaimerComponent } from './disclaimer/disclaimer.component';
//import { CustDropdownDirective } from './main-navigation/cust-dropdown.directive';
//import { NgbDropdownDirective } from './shared/Directives/ngb-dropdown.directive';
//import { NgbDropdownToggleDirective } from './shared/Directives/ngb-dropdown-toggle.directive';
import { NgDropdownDirective } from './shared/Directives/ng-dropdown.directive';
import { HomeDataService } from './shared/services/home-data.service';

import { AppSettings } from './shared/app.settings';


@NgModule({
  imports: [
    BrowserModule,
    HttpModule,
    AppRoutingModule,
    HomeModule,
    FormsModule, // Template Driven Forms
    ReactiveFormsModule, // Reactive Forms
  ],
  declarations: [
    AppComponent,
    MainNavigationComponent,
    BannerTopComponent,
    //AboutComponent,
    //DisclaimerComponent
    //CustDropdownDirective
    //NgbDropdownDirective,
    //NgbDropdownToggleDirective,
    NgDropdownDirective
  ],
  providers: [ HomeDataService, AppSettings ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
