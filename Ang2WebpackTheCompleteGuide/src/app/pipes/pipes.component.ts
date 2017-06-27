import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-pipes',
  templateUrl: './pipes.component.html',
  styleUrls: ['./pipes.component.css']
})
export class PipesComponent implements OnInit {

    myValue = 'lowecase';
    myDate = new Date(2017, 6, 17);
    values = ['Milk', 'Bread', 'Beans'];
    asyncValue = new Promise((resolve, reject) => {
        setTimeout(() => resolve('Data is here!'),2000);
    });

  constructor() { }

  ngOnInit() {
  }

}
