import { Component, OnInit } from '@angular/core';
//import { NgForm } from '@angular/forms';  // Template Forms
import { FormGroup, FormBuilder, Validators, AbstractControl } from '@angular/forms';  //Reactive Forms
import 'rxjs/add/operator/debounceTime';

import { CompanyRegister } from './company-register';

@Component({
  selector: 'app-company-register',
  templateUrl: './company-register.component.html',
  styleUrls: ['./company-register.component.css']
})
export class CompanyRegisterComponent implements OnInit {

  crForm :FormGroup;
  cr: CompanyRegister = new CompanyRegister();
  emailMessage: string;

    private validationMessages = {
        required: 'Please enter an email address',
        pattern: 'Please enter a valid email address',
    };

  constructor(private fb: FormBuilder) { }

    ngOnInit(): void {
        this.crForm = this.fb.group({
            nameGroup : this.fb.group({
            name1:['',[Validators.required, Validators.minLength(2)]],
            name2:['',[Validators.required, Validators.minLength(2)]],
            //name3:['',[Validators.required, Validators.minLength(2)]],
            }),
            commencementDate: ['', Validators.required],
            companyType: 'public',
            directorInfoGroup: this.fb.group({
                surname: ["", Validators.required],
                directorEmail: ['',[Validators.required, Validators.pattern("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")]],
                directorPhone: ['', Validators.required]
            })
        });

        const emailControl = this.crForm.get('directorInfoGroup.directorEmail');
        emailControl.valueChanges.debounceTime(1000).subscribe(value => {
                this.setMessage(emailControl)}
            );
        emailControl.statusChanges.subscribe(value => {
                //console.log(value)
            }
        );                
    }

    save() {
      // save(crForm: NgForm) {  // Template Driven
      debugger;
        console.log(this.crForm);
        console.log('Saved: ' + JSON.stringify(this.crForm.value));
    }

    setMessage(c: AbstractControl): void {
        this.emailMessage = '';
        if ((c.touched || c.dirty) && c.errors) {
            this.emailMessage = Object.keys(c.errors).map(key =>
                this.validationMessages[key]).join(' ');
        }
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
