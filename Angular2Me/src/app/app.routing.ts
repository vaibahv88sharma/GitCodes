import { PreloadAllModules, RouterModule, Routes} from '@angular/router';
import { NgModule } from '@angular/core';

import { PlayerListComponent } from './players/player-list/player-list.component';
import { NotFoundComponent } from './not-found.component';

const routes: Routes = [
    {
        path: '',
        //component: PlayerListComponent
        redirectTo: 'players',
        pathMatch: 'full'
    },  
    {
        path:'teams',
        loadChildren: './teams/teams.module#TeamsModule'
        //loadChildren: './teams/teams.module#TeamsModule?sync=true'
    },
    {
        path: '**',
        component: NotFoundComponent
    }      
];

@NgModule({
    //imports: [RouterModule.forRoot(routes)],
    imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
    exports: [RouterModule]
})

export class AppRoutingModule {

}