import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
//import { FormsModule }  from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { ContactRoutingModule } from './contact.routing';
import { ContactComponent } from './contact.component';
import { ContactSubmitComponent } from './contact-submit/contact-submit.component';

@NgModule({
  imports: [
    //FormsModule,
    ReactiveFormsModule,
    CommonModule,
    ContactRoutingModule
  ],
  declarations: [
    ContactComponent,
    ContactSubmitComponent
  ]
})
export class ContactModule { }
