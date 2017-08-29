import { Component, OnInit, ContentChildren, QueryList, AfterContentInit } from '@angular/core';
import { HomeDetailsTileTabComponent } from './home-details-tile-tab/home-details-tile-tab.component';

@Component({
  selector: 'home-details-tiles',
  templateUrl: './home-details-tiles.component.html',
  styleUrls: ['./home-details-tiles.component.css']
})

//export class HomeDetailsTilesComponent implements OnInit, AfterContentInit {
export class HomeDetailsTilesComponent implements OnInit, AfterContentInit {

  tabs: HomeDetailsTileTabComponent[] = [];

  //@ContentChildren(HomeDetailsTileTabComponent) tabs: QueryList<HomeDetailsTileTabComponent>;

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
    //debugger;
    this.tabs.push(tab);
  } 

  //selectTab(tab: HomeDetailsTileTabComponent){
  //  debugger;
  //  // deactivate all tabs
  //  this.tabs.toArray().forEach(tab => tab.active = false);
  //  
  //  // activate the tab the user has clicked on.
  //  tab.active = true;
  //}
  
  ngAfterContentInit() {
  //  debugger;
  //    // get all active tabs
  //  let activeTabs = this.tabs.filter((tab)=>tab.active);
  //  
  //  // if there is no active tab set, activate the first
  //  if(activeTabs.length === 0) {
  //    this.selectTab(this.tabs.first);
  //  }
  }

  ngOnInit() {
  }

}
