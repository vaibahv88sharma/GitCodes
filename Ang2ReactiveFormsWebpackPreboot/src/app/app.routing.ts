import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { AboutComponent } from './about/about.component';
import { DetailsComponent } from './details/details.component';

import { ProductListComponent } from './products/product-list.component';

const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full' },  
  { path: 'home', component: HomeComponent },
  // { path: '', component: HomeComponent },
  { path: 'about', component: AboutComponent},
  { path: 'details', component: DetailsComponent} , 
  { path: 'products', component: ProductListComponent },
];

export const routing = RouterModule.forRoot(routes);
