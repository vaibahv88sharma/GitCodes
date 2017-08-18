import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }  from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { DisclaimerRoutingModule } from './disclaimer.routing';
import { DisclaimerComponent } from './disclaimer.component';


@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    DisclaimerRoutingModule
  ],
  declarations: [
    DisclaimerComponent    
  ]
})
export class DisclaimerModule { }
