import { NgModule } from '@angular/core';
//import { BrowserModule }  from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule }  from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { PlayerListComponent } from './player-list/player-list.component';
import { PlayersRoutingModule } from './players.routing';
import { CommonnModule } from '../shared/common.module';
import { PlayersProfileComponent } from './players-profile/players-profile.component';
import { RegistrationComponent } from './registration/registration.component';
import { PlayerProfileTransclusionComponent } from './players-profile/transclusion/player-profile-transclusion.component';
import { PlayerProfileHeadingComponent } from './players-profile/transclusion/player-profile-heading.component';
import { PlayerProfileDetailsComponent } from './players-profile/transclusion/player-profile-details.component';
import { PlayerProfileDetailEntryComponent } from './players-profile/transclusion/player-profile-detail-entry.component';

@NgModule({
  imports: [
    //BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    CommonnModule,    
    PlayersRoutingModule,
  ],
  declarations: [
    PlayerListComponent,
    PlayersProfileComponent,
    RegistrationComponent,
    PlayerProfileTransclusionComponent,
    PlayerProfileHeadingComponent,
    PlayerProfileDetailsComponent,
    PlayerProfileDetailEntryComponent
  ]
})
export class PlayersModule { }
