import { Routes } from "@angular/router"
import { UserEditComponent } from "./user-edit.component";
import { UserDetailComponent } from "./user-detail.component";
import { UserDetailGuard } from "./user-detail.guard";
import { UserEditGuard } from "./user-edit.guard";

export const USER_ROUTES: Routes = [
    { path: 'edit', component: UserEditComponent, canDeactivate: [UserEditGuard] },
    { path: 'detail', component: UserDetailComponent, canActivate: [UserDetailGuard] },
];