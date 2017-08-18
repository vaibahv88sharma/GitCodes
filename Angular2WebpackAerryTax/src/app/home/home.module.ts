import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule }  from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

import { HomeRoutingModule } from './home.routing';
import { HomeDetailsComponent } from './home-details/home-details.component';
import { HomeDetailsTilesComponent } from './home-details/home-details-tiles/home-details-tiles.component';

@NgModule({
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    HomeRoutingModule
  ],
  declarations: [
    HomeDetailsComponent,
    HomeDetailsTilesComponent
    
  ]
})
export class HomeModule { }
