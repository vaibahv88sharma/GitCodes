import { PreloadAllModules, RouterModule, Routes} from '@angular/router';
import { NgModule } from '@angular/core';

import { AboutComponent } from './about/about.component';
import { DisclaimerComponent } from './disclaimer/disclaimer.component';

const routes: Routes = [
    {
        path: '',
        //component: PlayerListComponent
        redirectTo: 'home',
        pathMatch: 'full'
    },
    {
        path:'contact',
        loadChildren: './contact/contact.module#ContactModule'
        //loadChildren: './contact/contact.module#ContactModule?sync=true'
    }//,    
/*     {
        path:'teams',
        loadChildren: './teams/teams.module#TeamsModule'
        //loadChildren: './teams/teams.module#TeamsModule?sync=true'
    }, */
/*     {
        path: '**',
        component: DisclaimerComponent
    }  */
];

@NgModule({
    //imports: [RouterModule.forRoot(routes)],
    imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
    exports: [RouterModule]
})

export class AppRoutingModule {

}