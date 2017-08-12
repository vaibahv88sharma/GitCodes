import { Component, OnInit } from '@angular/core';
import { BasketballPlayer } from '../../shared/players/basketball-player';
import { RestApiService } from '../../shared/restapi.service';

@Component({
  selector: 'app-player-list',
  templateUrl: './player-list.component.html',
  styleUrls: ['./player-list.component.css']
})
export class PlayerListComponent implements OnInit {

  constructor(private apiService: RestApiService) { }
  listOfPlayers: BasketballPlayer[];
  selectedPlayer: BasketballPlayer;

  ngOnInit() {
    this.listOfPlayers = this.apiService.getListOfPlayers();
  }

  setSelectedPlayer(selectedPlayer: BasketballPlayer) {
    this.selectedPlayer = selectedPlayer;
  }

}
