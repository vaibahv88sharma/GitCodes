import { PreloadAllModules, RouterModule, Routes} from '@angular/router';
import { NgModule } from '@angular/core';

import { PlayerListComponent } from './players/player-list/player-list.component';
import { NotFoundComponent } from './not-found.component';

const routes: Routes = [
/*     {
        path:'teams',
        //loadChildren: 'app/teams/teams.module#TeamsModule'
         loadChildren: () => new Promise(resolve => {
            (require as any).ensure([], (require: any) => {
                resolve(require('./teams/teams.module').TeamsModule);
            })
        })
    }, */
    {
        path: '',
        component: PlayerListComponent
    },
    {
        path: '**',
        component: NotFoundComponent
    }  
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    //imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
    exports: [RouterModule]
})

export class AppRoutingModule {

}