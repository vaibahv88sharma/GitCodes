import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }  from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { AboutRoutingModule } from './about.routing';
import { AboutComponent } from './about.component';

@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    AboutRoutingModule
  ],
  declarations: [
    AboutComponent    
  ]
})
export class AboutModule { }
