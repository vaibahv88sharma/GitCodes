//import { RouterModule, Routes, providerRoutes, RouterConfig } from '@angular/router';


import { RouterModule, Routes } from '@angular/router';
import { HyperlinkComponent } from "./hyperlink/hyperlink.component";
import { HomeComponent } from "./home/home.component";


const APP_ROUTES: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HyperlinkComponent },
    { path: 'hyperlink', component: HyperlinkComponent }//,
    //{ path: '**', redirectTo: '/user/2', pathMatch: 'full' }
];


export const routing = RouterModule.forRoot(APP_ROUTES);
//export const APP_ROUTES_PROVIDER = [
//    provideRoutes(APP_ROUTES)
//];