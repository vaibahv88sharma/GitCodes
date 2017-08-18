import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }  from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { TaxationRoutingModule } from './taxation.routing';
import { TaxationComponent } from './taxation.component';


@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    TaxationRoutingModule
  ],
  declarations: [
    TaxationComponent    
  ]
})
export class TaxationModule { }
