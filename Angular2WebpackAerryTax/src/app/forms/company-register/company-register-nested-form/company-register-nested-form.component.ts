import { Component, OnInit, Input,OnChanges } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, FormArray } from '@angular/forms';  //Reactive Forms

@Component({
  selector: 'director-array',
  templateUrl: './company-register-nested-form.component.html',
  styleUrls: ['./company-register-nested-form.component.css']
})
export class CompanyRegisterNestedFormComponent implements OnInit,OnChanges {

    @Input('group')
    public directorForm: FormGroup;

    @Input('directorPositionFG')
    public directorPositionChildArray: FormGroup;

  constructor() { }

  ngOnInit() {
    //debugger;
   // console.log(this.directorPositionChildArray);
  }
  ngOnChanges() {
    //debugger;
    //console.log(this.directorPositionChildArray);    
  }

}
