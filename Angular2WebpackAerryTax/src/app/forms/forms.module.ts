import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }  from '@angular/forms'; // Template Driven Forms
import { ReactiveFormsModule }  from '@angular/forms'; // Reactive Forms

import { FormsRoutingModule } from './forms.routing';
import { CompanyRegisterComponent } from './company-register/company-register.component';
import { CompanyRegisterNestedFormComponent } from './company-register/company-register-nested-form/company-register-nested-form.component';


@NgModule({
  imports: [
    FormsModule, // Template Driven Forms
    ReactiveFormsModule, // Reactive Forms
    CommonModule,
    FormsRoutingModule
  ],
  declarations: [
    CompanyRegisterComponent,
    CompanyRegisterNestedFormComponent   
  ]
})
export class FormsDataModule { }
