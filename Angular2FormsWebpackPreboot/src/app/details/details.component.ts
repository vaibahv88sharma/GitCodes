import { Component, OnInit } from '@angular/core';
import { NgForm } from "@angular/forms";
//import { Subscription } from "rxjs/Rx";

import { Employee } from '../models/employee.model';
import { FormPosterService } from '../services/form-poster.service';

@Component({
  selector: 'my-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {

  constructor(private fps: FormPosterService) {
    //this.q="&$select=firstName,lastName,isFullTime,paymentType,primaryLanguage";
    //this.q="&$select=Id,primaryLanguage";
    this.fps.getLanguages('primaryLanguage', "").subscribe(
      data => this.languages = data,
      //data => console.log('data: ', data),
      err => console.log('get error: ', err)
    );
  }

  employee: Employee[] = [];
  private body: string;

  languages = [];

  // model = new Employee('Vaibhav', 'Sharma', true, 'w2', 'English');
  model = new Employee('', '', true, 'w2', 'default');
  hasPrimaryLanguageError = false;
  //private q: string;

  startDate: Date;
  minDate = new Date('June 26 2017');
  startTime = new Date('June 26 2017 3:00 PM');
  onOffSwitch = "Off";
  taxType = "W2";
  postRating = 5;

  hover(value){
    console.log("hover: "+value);
  }
  leave(value){
    console.log("leave: "+value);
  }

  validatePrimaryLanguage(value: any){
    if(value === 'default')
      this.hasPrimaryLanguageError = true;
    else
      this.hasPrimaryLanguageError = false;
  }

  submitForm(form: NgForm){

    this.validatePrimaryLanguage(this.model.primaryLanguage);
    if (this.hasPrimaryLanguageError){
      return;
    }  
            var obj = {
              '__metadata': {
                  'type': this.fps.getItemTypeForListName('employee')
              },
              'firstName': this.model.firstName,
              'lastName': this.model.lastName,
              'isFullTime': this.model.isFullTime,
              'paymentType': this.model.paymentType,
              'primaryLanguage': this.model.primaryLanguage,

          };
          this.body = JSON.stringify(obj);

          this.fps.postEmployees(
              //this.recipeService.recipes[i],
              this.body,
              'employee'
                  ).subscribe(
              (data: any) => console.log('success: ',data),
                      (error: any) => console.error('error: ', error)
          );  
    //this.fps.postEmployees(this.model);
    console.log(form.value);
  }


/*  firstNameToUppercase(value: string){
    if (value.length > 0)
      this.model.firstName = value.charAt(0).toUpperCase() + value.slice(1);
    else
      this.model.firstName = value;
  }*/

  ngOnInit() {
    console.log('Hello About');
  }

}
