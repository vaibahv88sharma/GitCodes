"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var UserDetailGuard = (function () {
    function UserDetailGuard() {
    }
    UserDetailGuard.prototype.canActivate = function (route, state) {
        return confirm('Are you sure?');
    };
    return UserDetailGuard;
}());
exports.UserDetailGuard = UserDetailGuard;
//# sourceMappingURL=user-detail.guard.js.map