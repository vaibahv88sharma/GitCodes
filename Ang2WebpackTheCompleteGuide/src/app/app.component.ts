import { Component } from '@angular/core';

import '../assets/css/styles.css';
import { DatabindingComponent } from './databinding/databinding.component';

@Component({
  selector: 'my-app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent { 

  delete = false;
  test = "Starting Value";
  boundValue = 1000

}
