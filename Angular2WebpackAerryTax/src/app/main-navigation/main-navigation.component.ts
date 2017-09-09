import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HomeDataService } from '../shared/services/home-data.service';
import { AppSettings } from "../shared/app.settings";

@Component({
  selector: 'app-main-navigation',
  templateUrl: './main-navigation.component.html',
  styleUrls: ['./main-navigation.component.css']
})
export class MainNavigationComponent implements OnInit {

  dropdownMenu: Array<any> = [];
  show:boolean = false;
  
  toggleCollapse() {
    this.show = !this.show
  }
  constructor(private _router: Router, hds: HomeDataService) { 
    hds.getTaxation(AppSettings.SERVICES_PROVIDED).subscribe( //'src/app/shared/jsonFiles/navDropdown.json'
      data => {
        this.dropdownMenu = data.navDropdown;
      },
      err => console.log('get error: ', err)
    );    
  }

  isActive(url: string): boolean {
    return url == this._router.url;
  }

  ngOnInit() {
  }

}
