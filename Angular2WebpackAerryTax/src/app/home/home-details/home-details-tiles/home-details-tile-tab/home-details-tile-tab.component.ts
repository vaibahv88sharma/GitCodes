import { Component, OnInit, Input } from '@angular/core';
import { HomeDetailsTilesComponent } from '../home-details-tiles.component';

@Component({
  selector: 'home-details-tile-tab',
  templateUrl: './home-details-tile-tab.component.html',
  styleUrls: ['./home-details-tile-tab.component.css']
})
export class HomeDetailsTileTabComponent implements OnInit {

   //@Input() tabTitle: string;
  @Input() active: boolean; 

   @Input('tabTitle') title: string;
  //@Input() active = false; 

  constructor(tabs: HomeDetailsTilesComponent) {
    tabs.addTab(this);
  }

  ngOnInit() {
  }

}
