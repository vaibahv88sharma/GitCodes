"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var user_edit_component_1 = require("./user-edit.component");
var user_detail_component_1 = require("./user-detail.component");
var user_detail_guard_1 = require("./user-detail.guard");
exports.USER_ROUTES = [
    { path: 'edit', component: user_edit_component_1.UserEditComponent, canDeactivate: [ComponentCanDeactivate] },
    { path: 'detail', component: user_detail_component_1.UserDetailComponent, canActivate: [user_detail_guard_1.UserDetailGuard] },
];
//# sourceMappingURL=user.routes.js.map