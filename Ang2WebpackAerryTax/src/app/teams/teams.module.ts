import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { FormsModule }  from '@angular/forms';

import { TeamListComponent } from './team-list/team-list.component';
import { TeamsRoutingModule } from './teams.routing';
import { CommonModule } from '../common/common.module';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    CommonModule,
    TeamsRoutingModule
  ],
  declarations: [
    TeamListComponent
  ]
})
export class TeamsModule { }
