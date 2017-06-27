"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
//@Injectable()
var LogService = (function () {
    function LogService() {
    }
    LogService.prototype.writeToLog = function (logMessage) {
        console.log(logMessage);
    };
    return LogService;
}());
exports.LogService = LogService;
//# sourceMappingURL=log.service.js.map