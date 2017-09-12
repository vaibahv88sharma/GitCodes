import { Component, OnInit } from '@angular/core';
//import { NgForm } from '@angular/forms';  // Template Forms
import { FormGroup, FormBuilder, Validators, AbstractControl, FormArray } from '@angular/forms';  //Reactive Forms
import 'rxjs/add/operator/debounceTime';

import { CompanyRegister } from './company-register';
import { AppSettings } from "../../shared/app.settings";
import { HomeDataService } from "../../shared/services/home-data.service";

@Component({
  selector: 'app-company-register',
  templateUrl: './company-register.component.html',
  styleUrls: ['./company-register.component.css']
})
export class CompanyRegisterComponent implements OnInit {

  crForm :FormGroup;
  public cr: CompanyRegister = new CompanyRegister();
  private crRef : CompanyRegister;

    get getShareHoldersArray(): FormArray{
        return <FormArray>this.crForm.get('shareHoldersArray');
    }
    get getDirectorArray(): FormArray{
        /* debugger;
        console.log(this.crForm.get('directorPositionArray'));
        console.log(this.crForm.get('directorInfoGroupArray'));
        console.log(this.crForm.get('directorInfoGroupArray.directorPositionArray'));   */      
        return <FormArray>this.crForm.get('directorInfoGroupArray');
    }    
    get getDirectorPosition(): FormArray{
        return <FormArray>this.crForm.get('directorPositionArray');
    } 

  emailMessage: string;

    private validationMessages = {
        required: 'Please enter an email address',
        pattern: 'Please enter a valid email address',
    };
    // {{getShareHoldersArray.get('0.sharesNo')?.value}}
    // {{shareHoldersArray.get('0.sharesNo')?.value}}

    //private formControlNames = ['name1', 'name2', 'commencementDate', 'directorSurname', 'directorEmail', 'directorPhone' ];
    private formValidation : Array<any> = [];
    private formControlNames = { 
                                name1 : {
                                    required: 'Please enter an name1 address',
                                    minlength: 'Minimum Length Should be 2'                                  
                                },
                                name2 : {
                                    required: 'Please enter an name2 address',
                                    minlength: 'Minimum Length Should be 2'                                 
                                },
                                commencementDate:{
                                    required: "Please enter the Commencement Date."
                                },
                                directorEmail : {
                                    required: 'Please enter an email address',
                                    pattern: 'Please enter a valid email address',                               
                                },
                                directorSurname : {
                                    required: "Please enter Surname."
                                },
                                directorPhone : {
                                    required: "Please enter phone number."
                                }                                
                            };

  constructor(private fb: FormBuilder, private appSettings: AppSettings, private hds: HomeDataService) { 
      //cr = new CompanyRegister();
  }

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
            liability: 'shares',
            directorInfoGroupArray: this.fb.array([ this.buildDirector() ]),
            shareHoldersArray: this.fb.array([ this.buildShareholder() ]),
            //declarationName: ""
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

        //this.hds.getTaxation("src/app/shared/jsonFiles/navDropdown.json").subscribe( //AppSettings.SERVICES_PROVIDED
        this.hds.getTaxation(AppSettings.SERVICES_PROVIDED).subscribe(
            data => {
                //debugger;
                this.formValidation = data.formValidation;
            },
            err => {debugger; console.log('get error: ', err)}
        ); 

    }

    save() {
      // save(crForm: NgForm) {  // Template Driven
      debugger;
        console.log(this.crForm);
        console.log('Saved: ' + JSON.stringify(this.crForm.value));
    }

    addDirector(): void{
        this.getDirectorArray.push(this.buildDirector());
    }   
    removeDirector(i: number) {
        this.getDirectorArray.controls.splice(i,1);
    }

    buildDirector(): FormGroup{
        return this.fb.group({
                directorFirstname: "",
                directorSurname: "",
                directorTfn: "",
                directorPhone: "",  
                directorEmail: "",                              
                directorDl: "",
                directorDob: "",
                directorBirthPlace: "",
                directorAddress: "",
                //directorPositionArray: this.fb.array([ this.buildDirectorPosition() ])//""          
               directorPositionArray: this.fb.group({
                                               posDirector: "",
                                               posSecretary: "",
                                               posPublicOfficer: "",      
                                       })
        })
    }

    buildDirectorPosition(): FormGroup{
        return this.fb.group({
                posDirector: "",
                posSecretary: "",
                posPublicOfficer: "",      
        })
    }    

    addShareholder(): void{
        this.getShareHoldersArray.push(this.buildShareholder());
    }
    buildShareholder(): FormGroup{
        return this.fb.group({
            sharesNo: "",
            //sharesNo: ["", Validators.required],
            shareHolderName: "",
            shareHolderAddress: ""            
        })
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
                if (formControl instanceof FormArray) {
                    //debugger;
                    //console.log(formControl.controls[0]);
                    this.setMessageOnForm(<FormGroup>formControl.controls[0]);
                }
                //debugger;
                //if(name != "companyType"){
                    //debugger;
                    formControl.valueChanges.debounceTime(1000).subscribe(value => 
                        {
                            //debugger;
                            this.setMessageOnControl(formControl, name);
                            //console.log(value);
                        }
                    );
                //}       
            }
        });
        

    }

    setMessageOnControl(c: AbstractControl, controlName:string): void {
        //debugger;
        
        Object.keys(this.formValidation).forEach((name) => //formControlNames
        {
            //debugger;
            if (controlName === name)
            {
               // debugger;
                this.cr[name] = "";
                if ((c.touched || c.dirty) && c.errors) {
                    //debugger;
                    this.cr[name] = Object.keys(c.errors).map(key =>
                        this.formValidation[name][key]).join(' '); //formControlNames
                }
                //console.log(this.formValidation[name]);
                //console.log(this.cr[name]);              
            }
        });
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

    saveForm(formSubmitted :FormGroup): void{
        debugger;
        console.log(formSubmitted);
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
