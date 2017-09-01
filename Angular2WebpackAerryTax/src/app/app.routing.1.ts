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
        path: 'contact',
        loadChildren: './contact/contact.module#ContactModule'
        //path: 'contact', loadChildren: () => new Promise(resolve => {
        //    (require as any).ensure([], (require : any) => {
        //        resolve(require('./contact/contact.module').ContactModule);
        //    })
        //})
    },  
    {
        path:'taxation',
        loadChildren: './taxation/taxation.module#TaxationModule'
        //loadChildren: './teams/teams.module#TeamsModule?sync=true'
    },
    {
        path:'about',
        loadChildren: './about/about.module#AboutModule'
    },
    {
        path:'disclaimer',
        loadChildren: './disclaimer/disclaimer.module#DisclaimerModule'
    },
    {
        path:'forms',
        loadChildren: './forms/forms.module#FormsDataModule'
    },      
    {
        path: '**',
        redirectTo: 'home'
    }          
/*     {
        path: '**',
        component: DisclaimerComponent
    } */
];

@NgModule({
    //imports: [RouterModule.forRoot(routes)],
    imports: [RouterModule.forRoot(routes, { preloadingStrategy: PreloadAllModules })],
    exports: [RouterModule]
})

export class AppRoutingModule {

}