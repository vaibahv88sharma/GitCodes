import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { FormsModule }  from '@angular/forms';
import { HttpModule }  from '@angular/http';
import { RouterModule }  from '@angular/router';


import { AppComponent } from './app.component';

import { FirstFeatureComponent } from './first-feature/first-feature.component';
import { SecondFeatureComponent } from './second-feature/second-feature.component';
import { ChildComponent } from './child/child.component';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    HttpModule,
    RouterModule.forRoot([
      {
        "path": "second-feature",
        "component": SecondFeatureComponent 
      },
      {
        //"path": "first-feature",
        "path": "**",
        "component": FirstFeatureComponent         
      }
    ])
  ],
  declarations: [
    AppComponent,
    FirstFeatureComponent,
    SecondFeatureComponent,
    ChildComponent
  ],
  providers: [],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
