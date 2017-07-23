import { NgModule } from '@angular/core';
//import { BrowserModule }  from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule }  from '@angular/forms';

import { PlayerListComponent } from './player-list/player-list.component';
import { PlayersRoutingModule } from './players.routing';
import { CommonnModule } from '../common/common.module';
import { PlayersProfileComponent } from './players-profile/players-profile.component';

@NgModule({
  imports: [
    //BrowserModule,
    FormsModule,
    CommonModule,
    CommonnModule,
    PlayersRoutingModule,
  ],
  declarations: [
    PlayerListComponent,
    PlayersProfileComponent
  ]
})
export class PlayersModule { }
