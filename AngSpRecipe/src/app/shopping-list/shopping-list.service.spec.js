"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var testing_1 = require("@angular/core/testing");
var shopping_list_service_1 = require("./shopping-list.service");
describe('ShoppingListService', function () {
    beforeEach(function () {
        testing_1.TestBed.configureTestingModule({
            providers: [shopping_list_service_1.ShoppingListService]
        });
    });
    it('should be created', testing_1.inject([shopping_list_service_1.ShoppingListService], function (service) {
        expect(service).toBeTruthy();
    }));
});
//# sourceMappingURL=shopping-list.service.spec.js.map