import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { RouterModule }  from '@angular/router';
import { FormsModule }  from '@angular/forms';

import { AppRoutingModule } from './app.routing';
import { AppComponent } from './app.component';
import { MainNavigationComponent } from './main-navigation/main-navigation.component';
import { HomeModule } from './home/home.module';
import { BannerTopComponent } from './banner-top/banner-top.component';
import { AboutComponent } from './about/about.component';
import { DisclaimerComponent } from './disclaimer/disclaimer.component';

@NgModule({
  imports: [
    BrowserModule,
    AppRoutingModule,
    HomeModule
  ],
  declarations: [
    AppComponent,
    MainNavigationComponent,
    BannerTopComponent,
    AboutComponent,
    DisclaimerComponent
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }