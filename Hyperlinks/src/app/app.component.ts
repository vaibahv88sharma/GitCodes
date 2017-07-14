import { Component } from '@angular/core';

import '../assets/css/styles.css';

import { SharePointApp } from './shared/sharepoint';

@Component({
  selector: 'my-app',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent { 

  constructor(private spApp: SharePointApp) {
    this.spApp.init();
  }

}
