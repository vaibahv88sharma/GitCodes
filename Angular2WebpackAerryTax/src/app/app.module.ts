import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { MainNavigationComponent } from './main-navigation/main-navigation.component';

@NgModule({
  imports: [
    BrowserModule
  ],
  declarations: [
    AppComponent,
    MainNavigationComponent
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
