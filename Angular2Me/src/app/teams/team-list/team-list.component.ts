import { Component, OnInit, AfterViewInit, AfterViewChecked, ViewChild, ViewChildren, QueryList, ElementRef } from '@angular/core';

import { Team } from '../../shared/teams/team';
import { RestApiService } from '../../shared/restapi.service';
import { TeamEditComponent } from '../team-edit/team-edit.component';

@Component({
  selector: 'app-team-list',
  templateUrl: './team-list.component.html',
  styleUrls: ['./team-list.component.css']
})
export class TeamListComponent implements OnInit, AfterViewInit, AfterViewChecked {
  listOfTeams: Team[];
  selectedTeam: Team;
  @ViewChild(TeamEditComponent) teamEditComponent: TeamEditComponent;
  @ViewChildren("wins") winCells: QueryList<ElementRef>;

  constructor(private apiService: RestApiService) {}

  ngOnInit() {
    this.listOfTeams = this.apiService.getListOfTeams();
  }

  ngAfterViewInit(){
    this.teamEditComponent.showNotEditing = true;
    console.log("TeamListComponent ngAfterViewInit called.");
  }

  ngAfterViewChecked(){
    console.log("TeamListComponent ngAfterViewChecked called.");
    this.highlightMostWinningTeam();
  }

  selectTeam(selectedTeam: Team) {
    this.selectedTeam = selectedTeam;
  }

  private highlightMostWinningTeam() {
    var highlightIndex = 0;
    var currentIndex = 0;
    var highestWinValue = 0;
    
    this.winCells.forEach(wc => {
      wc.nativeElement.parentNode.style.backgroundColor = '';
      wc.nativeElement.parentNode.style.color = '';

      var currentWins = Number(wc.nativeElement.innerText);
      
      if(currentWins > highestWinValue)
      {
        highlightIndex = currentIndex;
        highestWinValue = currentWins;
      }

      currentIndex++;
    });

    this.winCells.find((item, index) => index == highlightIndex).nativeElement.parentNode.style.backgroundColor = 'red';
    this.winCells.find((item, index) => index == highlightIndex).nativeElement.parentNode.style.color = 'white';
  }

}