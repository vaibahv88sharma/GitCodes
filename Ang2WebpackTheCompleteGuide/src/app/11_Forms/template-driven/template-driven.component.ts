import { Component, OnInit } from '@angular/core';
import { NgForm } from "@angular/forms";

@Component({
  selector: 'app-template-driven',
  templateUrl: './template-driven.component.html',
  styleUrls: ['./template-driven.component.css']
})
export class TemplateDrivenComponent implements OnInit {


    user = {
        username: 'test',
        email: 'test@gmail.com',
        password: '1234',
        gender: 'male'
    }

    genders = [
        'male',
        'female'
    ];

    onSubmit(form: NgForm) {
        console.log(form.value);
        console.log(this.user);
    }
  constructor() { }

  ngOnInit() {
  }

}
