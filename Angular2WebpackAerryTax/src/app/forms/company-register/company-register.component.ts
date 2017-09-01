import { Component, OnInit } from '@angular/core';
//import { NgForm } from '@angular/forms';  // Template Forms
import { FormGroup, FormBuilder, Validators } from '@angular/forms';  //Reactive Forms

import { CompanyRegister } from './company-register';

@Component({
  selector: 'app-company-register',
  templateUrl: './company-register.component.html',
  styleUrls: ['./company-register.component.css']
})
export class CompanyRegisterComponent implements OnInit {

  crForm :FormGroup;
  cr: CompanyRegister = new CompanyRegister();

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.crForm = this.fb.group({
      name1:['',[Validators.required, Validators.minLength(3)]],
      email: ['',[Validators.required, Validators.pattern("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")]],
      phone: '',
      notification: 'email',
    });
  }

    save() {
      // save(crForm: NgForm) {  // Template Driven
      debugger;
        console.log(this.crForm);
        console.log('Saved: ' + JSON.stringify(this.crForm.value));
    }

    setNotification(notifyVia: string): void {
        const phoneControl = this.crForm.get('phone');
        if (notifyVia === 'text') {
            phoneControl.setValidators(Validators.required);
        } else {
            phoneControl.clearValidators();
        }
        phoneControl.updateValueAndValidity();
    }

/*  // .patchValue   --  Partial Array value 
    // .setValue  --  Full Array Value
    populateTestData(): void {
        this.customerForm.patchValue({
            firstName: 'Jack',
            lastName: 'Harkness',
            sendCatalog: false
        });
    } */

}
