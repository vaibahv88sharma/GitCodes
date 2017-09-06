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

    //private formControlNames = ['name1', 'name2', 'commencementDate', 'directorSurname', 'directorEmail', 'directorPhone' ];
    private formControlNames = { 
                                name1 : {
                                    required: 'Please enter an name1 address',
                                    pattern: 'Please enter a valid name1 address'                                  
                                },
                                name2 : {
                                    required: 'Please enter an name2 address',
                                    pattern: 'Please enter a valid name2 address'                                  
                                },                                
                            };

  constructor(private fb: FormBuilder) { }

//https://stackoverflow.com/questions/40361799/how-to-get-name-of-input-field-from-angular2-formcontrol-object
//https://kfarst.github.io/angular/2016/12/12/subscribing-to-form-value-changes-in-angular-2/

//Reactive Forms 41
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
                directorSurname: ["", Validators.required],
                directorEmail: ['',[Validators.required, Validators.pattern("[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?")]],
                directorPhone: ['', Validators.required]
            })
        });

        //const emailControl = this.crForm.get('directorInfoGroup.directorEmail');
        //emailControl.valueChanges.debounceTime(1000).subscribe(value => {
        //            this.setMessage(emailControl)
        //        }
        //    );
        //emailControl.statusChanges.subscribe(value => {
        //        //console.log(value)
        //    }
        //);

        this.setMessageOnForm(this.crForm); //hecking Group Validation  
                        
    }

    save() {
      // save(crForm: NgForm) {  // Template Driven
      debugger;
        console.log(this.crForm);
        console.log('Saved: ' + JSON.stringify(this.crForm.value));
    }


    setMessageOnForm(c: FormGroup): void {
        //debugger;
        Object.keys(c.controls).forEach((name) =>
        {
            //debugger;
            let formControl = c.controls[name];
            if (formControl instanceof FormGroup) {
                this.setMessageOnForm(formControl);
            } else {                
                //debugger;
                if(name != "companyType"){
                    //debugger;
                    formControl.valueChanges.subscribe(value => 
                        {
                            //debugger;
                            this.setMessageOnControl(formControl, name);
                            console.log(value);
                        }
                    );
                }       
            }
        });
        

    }

    setMessageOnControl(c: AbstractControl, controlName:string): void {
        debugger;
        Object.keys(this.formControlNames).forEach((name) =>
        {
            debugger;
            if (controlName === name)
            {
                debugger;
            }
        });        
        //this.emailMessage = '';
        //if ((c.touched || c.dirty) && c.errors) {
        //    this.emailMessage = Object.keys(c.errors).map(key =>
        //        this.validationMessages[key]).join(' ');
        //}

    }

    setMessage(c: AbstractControl): void {
        this.emailMessage = '';
        if ((c.touched || c.dirty) && c.errors) {
            this.emailMessage = Object.keys(c.errors).map(key =>
                this.validationMessages[key]).join(' ');
        }

        debugger;
        var controlName = null;
        var parent = c["_parent"];

        Object.keys(parent.controls).forEach((name) =>
        {
            // and compare the passed control and 
            // a child control of a parent - with provided name (we iterate them all)
            if (c === parent.controls[name])
            {
                // both are same: control passed to Validator
                //  and this child - are the same references
                controlName = name;
            }
        });

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
