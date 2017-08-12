import { Component, OnInit, Input } from '@angular/core';

import { BasketballPlayer } from '../../shared/players/basketball-player';

@Component({
  selector: 'app-players-profile',
  templateUrl: './players-profile.component.html',
  styleUrls: ['./players-profile.component.css']
})
export class PlayersProfileComponent implements OnInit {

  @Input()
  selectedPlayer: BasketballPlayer

/*   constructor() {
    this.selectedPlayer = new Player("1212","121","212","21","12");
   } */

  ngOnInit() {
  }

}
