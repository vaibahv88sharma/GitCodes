import { Component, OnInit, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators, AbstractControl, FormArray } from '@angular/forms';  //Reactive Forms

@Component({
  selector: 'director-array',
  templateUrl: './company-register-nested-form.component.html',
  styleUrls: ['./company-register-nested-form.component.css']
})
export class CompanyRegisterNestedFormComponent implements OnInit {

    @Input('group')
    public directorForm: FormGroup;

  constructor() { }

  ngOnInit() {
  }

}
