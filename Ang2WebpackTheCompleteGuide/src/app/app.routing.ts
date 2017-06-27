//import { RouterModule, Routes, providerRoutes, RouterConfig } from '@angular/router';


import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from "./user/user.component";
import { HomeComponent } from "./home-component.component";
import { USER_ROUTES } from "./user/user.routes";
import { DataDrivenComponent } from "./11_Forms/data-driven/data-driven.component";
import { TemplateDrivenComponent } from "./11_Forms/template-driven/template-driven.component";


const APP_ROUTES: Routes = [
    { path: 'user', redirectTo: '/user/1', pathMatch: 'full' },
    { path: 'user/:id', component: UserComponent },
    { path: 'user/:id', component: UserComponent, children: USER_ROUTES },
    //{ path: '', component: HomeComponent }
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },

    //11 Forms
    { path: 'formsData', component: DataDrivenComponent },
    { path: 'formsTemplate', component: TemplateDrivenComponent },

    { path: '**', redirectTo: '/user/2', pathMatch: 'full' }

];


export const routing = RouterModule.forRoot(APP_ROUTES);
//export const APP_ROUTES_PROVIDER = [
//    provideRoutes(APP_ROUTES)
//];