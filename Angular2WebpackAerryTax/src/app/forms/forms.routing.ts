import {RouterModule, Routes} from '@angular/router';
import { NgModule } from '@angular/core';
import { CompanyRegisterComponent } from './company-register/company-register.component';

const routes: Routes = [
    {
        path: '',
        component: CompanyRegisterComponent
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})

export class FormsRoutingModule {

}