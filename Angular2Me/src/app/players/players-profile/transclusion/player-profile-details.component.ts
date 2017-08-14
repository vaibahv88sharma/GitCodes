import { Component, AfterContentInit, AfterContentChecked, ContentChildren,QueryList } from '@angular/core';

import { PlayerProfileDetailEntryComponent } from './player-profile-detail-entry.component';

@Component({
  selector: 'profile-details',
  templateUrl: './player-profile-details.component.html',
  styleUrls: ['./player-profile-details.component.css']
})
export class PlayerProfileDetailsComponent implements AfterContentInit, AfterContentChecked {

  @ContentChildren(PlayerProfileDetailEntryComponent) contentChildren: QueryList<PlayerProfileDetailEntryComponent>;

  constructor() { }

  ngAfterContentChecked(){
    console.log("PlayerProfileDetailsComponent ngAfterContentChecked called");
    console.log(this.contentChildren);
  }

  ngAfterContentInit(){
    console.log("PlayerProfileDetailsComponent ngAfterContentInit called");
  }


}
