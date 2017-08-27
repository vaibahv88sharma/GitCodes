import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HomeDataService } from '../shared/services/home-data.service';

@Component({
  selector: 'app-main-navigation',
  templateUrl: './main-navigation.component.html',
  styleUrls: ['./main-navigation.component.css']
})
export class MainNavigationComponent implements OnInit {

  dropdownMenu: Array<any> = [];

  constructor(private _router: Router, hds: HomeDataService) { 
    hds.getNavDropDown('src/app/shared/jsonFiles/navDropdown.json').subscribe(
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
