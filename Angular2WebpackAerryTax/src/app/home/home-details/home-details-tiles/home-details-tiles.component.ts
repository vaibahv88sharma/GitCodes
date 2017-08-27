import { Component, OnInit } from '@angular/core';
import { HomeDetailsTileTabComponent } from './home-details-tile-tab/home-details-tile-tab.component';

@Component({
  selector: 'home-details-tiles',
  templateUrl: './home-details-tiles.component.html',
  styleUrls: ['./home-details-tiles.component.css']
})
export class HomeDetailsTilesComponent implements OnInit {

  tabs: HomeDetailsTileTabComponent[] = [];

  constructor() { }

   selectTab(tab: HomeDetailsTileTabComponent){
     //debugger;
    this.tabs.forEach((tab) => {
      tab.active = false;
    });
    tab.active = true;
  }

  addTab(tab: HomeDetailsTileTabComponent){
    if(this.tabs.length === 0){
      tab.active = true;
    }
    this.tabs.push(tab);
  } 

  ngOnInit() {
  }

}
