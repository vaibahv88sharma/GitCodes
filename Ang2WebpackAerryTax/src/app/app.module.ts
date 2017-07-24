import { NgModule } from '@angular/core';
import { BrowserModule }  from '@angular/platform-browser';
import { RouterModule }  from '@angular/router';
import { FormsModule }  from '@angular/forms';

import { PlayersModule } from './players/players.module';
//import { TeamsModule } from './teams/teams.module';
import { AppComponent } from './app.component';
import { PlayerListComponent } from './players/player-list/player-list.component';
import { NotFoundComponent } from './not-found.component';
import { AppRoutingModule } from './app.routing';
import { CommonnModule } from './shared/common.module';
import { MainNavigationComponent } from './main-navigation/main-navigation.component';

@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    PlayersModule,
    //TeamsModule,
    AppRoutingModule,
    CommonnModule
  ],
  declarations: [
    AppComponent,
    //TeamListComponent,
    //PlayerListComponent,
    NotFoundComponent,
    MainNavigationComponent
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule { }
