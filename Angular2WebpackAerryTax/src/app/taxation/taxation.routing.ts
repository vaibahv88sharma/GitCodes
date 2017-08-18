import {RouterModule, Routes} from '@angular/router';
import { NgModule } from '@angular/core';
import { TaxationComponent } from './taxation.component';

const routes: Routes = [
    {
        path: 'taxation',
        component: TaxationComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class TaxationRoutingModule {

}