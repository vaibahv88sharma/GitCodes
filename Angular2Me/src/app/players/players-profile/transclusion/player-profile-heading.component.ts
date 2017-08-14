import { Component, AfterContentInit, AfterContentChecked } from '@angular/core';

@Component({
  selector: 'profile-heading',
  templateUrl: './player-profile-heading.component.html',
  styleUrls: ['./player-profile-heading.component.css']
})
export class PlayerProfileHeadingComponent implements AfterContentInit, AfterContentChecked {

  constructor() { }

  ngAfterContentChecked(){
    console.log("PlayerProfileHeadingComponent ngAfterContentChecked called");
  }

  ngAfterContentInit(){
    console.log("PlayerProfileHeadingComponent ngAfterContentInit called");
  }

}
