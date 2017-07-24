import { Component, OnInit } from '@angular/core';

import { RegistrationViewModel } from './registrationViewModel';
import { TeamLookup } from '../../shared/lookup/team-lookup';
import { InjuryLookupService } from '../../shared/lookup/injury-lookup.service';
import { TeamLookupService } from '../../shared/lookup/team-lookup.service';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  registrationViewModel: RegistrationViewModel
  teamLookupList: TeamLookup[];
  injuryLookupList: string[];
  injuryFieldList: string[];

  constructor(
    private injuryLookupService: InjuryLookupService,
    private teamLookupService: TeamLookupService    
  ) {
    this.registrationViewModel = new RegistrationViewModel();    
   }

/* 
  addOrRemoveInjury(value: string) {
    if(this.confirmInjuryNotAlreadyChosen(value))
    {
      this.registrationViewModel.injuries.push(value);
    }
    else {
      var index = this.registrationViewModel.injuries.indexOf(value);
      this.registrationViewModel.injuries.splice(index);
    }
  }

  private confirmInjuryNotAlreadyChosen(value: string): boolean {
    return this.registrationViewModel.injuries.find(inj => inj.toLowerCase() === value.toLowerCase()) == null;
  } */
  addOrRemoveInjury(value: string) {
    var indexOfEntry = this.registrationViewModel.injuries.indexOf(value);
    
    if(indexOfEntry < 0) {
      this.registrationViewModel.injuries.push(value);
    }
    else {
      this.registrationViewModel.injuries.splice(indexOfEntry, 1);
    }
  }
  
  ngOnInit() {
    this.teamLookupList = this.teamLookupService.getTeamsLookup();
    this.injuryLookupList = this.injuryLookupService.getInjuriesLookup();
    this.initialiseInjuryFieldList();
  }

  private initialiseInjuryFieldList() {
    this.injuryFieldList = [];

    for(let injury of this.injuryLookupList) {
      this.injuryFieldList.push(injury.trim().replace(" ", "").toLowerCase());
    }
  }

}
