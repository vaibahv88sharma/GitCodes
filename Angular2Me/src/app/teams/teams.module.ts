import { NgModule } from '@angular/core';
//import { BrowserModule }  from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
//import { FormsModule }  from '@angular/forms';
import { ReactiveFormsModule }  from '@angular/forms';

import { TeamListComponent } from './team-list/team-list.component';
import { TeamsRoutingModule } from './teams.routing';
import { CommonnModule } from '../shared/common.module';
import { TeamEditComponent } from './team-edit/team-edit.component';

@NgModule({
  imports: [
    //BrowserModule,
    ReactiveFormsModule,
    CommonnModule,
    CommonModule,
    TeamsRoutingModule
  ],
  declarations: [
    TeamListComponent,
    TeamEditComponent
  ]
})
export class TeamsModule { }
