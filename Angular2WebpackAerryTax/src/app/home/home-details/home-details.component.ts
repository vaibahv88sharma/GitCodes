import { Component, OnInit } from '@angular/core';
import { HomeDataService } from '../../shared/services/home-data.service';
import { AppSettings } from "../../shared/app.settings";

@Component({
  selector: 'home-details',
  templateUrl: './home-details.component.html',
  styleUrls: ['./home-details.component.css']
})
export class HomeDetailsComponent implements OnInit {

  tabsThumbnails: Array<any> = [];

  constructor(hds: HomeDataService) { 
    hds.getTaxation(AppSettings.SERVICES_PROVIDED).subscribe( //'src/app/shared/jsonFiles/navDropdown.json'
      data => {
        //debugger;;
        this.tabsThumbnails = data.tabs;
      },
      err => console.log('get error: ', err)
    );    
  }

  ngOnInit() {
  }

}
