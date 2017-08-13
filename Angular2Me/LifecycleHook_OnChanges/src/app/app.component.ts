import { Component } from '@angular/core';

import '../assets/css/styles.css';

@Component({
  selector: 'my-app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent { 

  numberToChange = 90;
  objectToChange = { numberToChange: this.numberToChange };

  resetToDefaultValue(){
    this.objectToChange.numberToChange = this.numberToChange;
  }

  resetToNewReference(){
    this.objectToChange = {numberToChange: this.numberToChange};
  }

  keepChildValue(childValue: number){
    this.numberToChange = childValue;
  }

}
