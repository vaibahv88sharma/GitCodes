import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { HyperlinkComponent } from "./hyperlink/hyperlink.component";
//import { HyperlinkChildComponent } from "./hyperlink/hyperlink-child.component";
import { HomeComponent } from "./home/home.component";
import { routing } from "./app.routing";
import { DataService } from './shared/data.service';
import { SharePointApp } from './shared/sharepoint';

//import { DimensionsDirective } from 'angular2-dimensions-directive';
//import { DimensionsDirective } from 'angular2-clickoutside-directive';


@NgModule({
  imports: [
    BrowserModule,
    routing,
    HttpModule
  ],
  declarations: [
    AppComponent,
    HyperlinkComponent,
    //HyperlinkChildComponent,
    HomeComponent
  ],
  providers:[
    DataService,
    SharePointApp
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
