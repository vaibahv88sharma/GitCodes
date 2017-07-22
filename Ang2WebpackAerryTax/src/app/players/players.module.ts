import { NgModule } from '@angular/core';
//import { BrowserModule }  from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule }  from '@angular/forms';

import { PlayerListComponent } from './player-list/player-list.component';
import { PlayersRoutingModule } from './players.routing';
import { CommonnModule } from '../common/common.module';

@NgModule({
  imports: [
    //BrowserModule,
    FormsModule,
    CommonModule,
    CommonnModule,
    PlayersRoutingModule
  ],
  declarations: [
    PlayerListComponent
  ]
})
export class PlayersModule { }
