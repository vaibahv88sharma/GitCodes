import { Component, OnInit, AfterContentInit, AfterContentChecked, ContentChild } from '@angular/core';

import { PlayerProfileHeadingComponent } from './player-profile-heading.component';


@Component({
  selector: 'player-profile',
  templateUrl: './player-profile-transclusion.component.html',
  styleUrls: ['./player-profile-transclusion.component.css']
})
export class PlayerProfileTransclusionComponent implements AfterContentInit, AfterContentChecked {

  @ContentChild(PlayerProfileHeadingComponent) playerProfileHeadingComponent: PlayerProfileHeadingComponent;


  constructor() { }

  ngAfterContentChecked(){
    console.log("PlayerProfileTransclusionComponent ngAfterContentChecked called");
    console.log(this.playerProfileHeadingComponent);
  }

  ngAfterContentInit(){
    console.log("PlayerProfileTransclusionComponent ngAfterContentInit called");
  }

}
