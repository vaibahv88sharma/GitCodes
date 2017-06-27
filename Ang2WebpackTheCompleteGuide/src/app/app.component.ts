import { Component } from '@angular/core';

import '../assets/css/styles.css';
import { DatabindingComponent } from './databinding/databinding.component';
import { DataService } from './services/data.service';

@Component({
  selector: 'my-app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  providers: [DataService]
})
export class AppComponent { 

  delete = false;
  test = "Starting Value";
  boundValue = 1000

}
