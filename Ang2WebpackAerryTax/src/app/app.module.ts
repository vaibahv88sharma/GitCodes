import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { RouterModule }  from '@angular/router';
import { FormsModule }  from '@angular/forms';

import { PlayersModule } from './players/players.module';
import { TeamsModule } from './teams/teams.module';
import { AppComponent } from './app.component';
import { PlayerListComponent } from './players/player-list/player-list.component';
import { NotFoundComponent } from './not-found.component';
import { AppRoutingModule } from './app.routing';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    PlayersModule,
    TeamsModule,
    AppRoutingModule
  ],
  declarations: [
    AppComponent,
    //TeamListComponent,
    //PlayerListComponent,
    NotFoundComponent
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
