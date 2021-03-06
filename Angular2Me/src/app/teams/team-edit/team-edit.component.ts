import { Component, Input, AfterViewInit, AfterViewChecked, OnChanges } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { Team } from '../../shared/teams/team';
import { FormField } from '../../shared/forms/fields/form-field';
import { FormFieldsService } from '../../shared/forms/fields/form-fields.service';

@Component({
  selector: 'app-team-edit',
  templateUrl: './team-edit.component.html',
  styleUrls: ['./team-edit.component.css']
})
export class TeamEditComponent implements OnChanges, AfterViewInit, AfterViewChecked{
  @Input()
  selectedTeam: Team;
  formFields: FormField<any>[];
  teamEditForm: FormGroup;
  showNotEditing: boolean;

  constructor(private formFieldsService: FormFieldsService) { 
  }

  ngOnChanges() {
    if(this.selectedTeam)
    {
      this.showNotEditing = false;
      this.formFields = this.formFieldsService.getFieldsForForm();

      if(this.formFields && this.selectedTeam) {
        let controls: any = {};
          
        this.formFields.forEach(field => {        
          controls[field.key] = new FormControl(this.selectedTeam[field.key]);
        });        

        this.teamEditForm = new FormGroup(controls);   
      }    
      else {      
        this.teamEditForm = new FormGroup({});
      }      
    }
    else {
      this.teamEditForm = new FormGroup({});
    }
  }

  ngAfterViewInit(){
    console.log("TeamEditComponent ngAfterViewInit called.");
  }
  ngAfterViewChecked(){
    console.log("TeamEditComponent ngAfterViewChecked called.");
  }

  saveTeam() {
    // Save to the bound team so the table updates
    this.selectedTeam.teamName = this.teamEditForm.value.teamName;
    this.selectedTeam.numberOfWins = this.teamEditForm.value.numberOfWins;
    this.selectedTeam.numberOfLosses = this.teamEditForm.value.numberOfLosses;

    this.selectedTeam = null;
    this.showNotEditing = true;
  }
}