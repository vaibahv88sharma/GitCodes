import { Component, OnInit, Input } from '@angular/core';

import { Player } from '../../common/players/player';

@Component({
  selector: 'app-players-profile',
  templateUrl: './players-profile.component.html',
  styleUrls: ['./players-profile.component.css']
})
export class PlayersProfileComponent implements OnInit {

  @Input()
  selectedPlayer: Player

/*   constructor() {
    this.selectedPlayer = new Player("1212","121","212","21","12");
   } */

  ngOnInit() {
  }

}
