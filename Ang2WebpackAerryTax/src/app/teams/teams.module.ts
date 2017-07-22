import { NgModule } from '@angular/core';
//import { BrowserModule }  from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule }  from '@angular/forms';

import { TeamListComponent } from './team-list/team-list.component';
import { TeamsRoutingModule } from './teams.routing';
import { CommonnModule } from '../common/common.module';

@NgModule({
  imports: [
    //BrowserModule,
    FormsModule,
    CommonnModule,
    CommonModule,
    TeamsRoutingModule
  ],
  declarations: [
    TeamListComponent
  ]
})
export class TeamsModule { }
