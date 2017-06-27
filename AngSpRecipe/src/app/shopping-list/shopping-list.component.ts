import { Component, OnInit, Input } from '@angular/core';
import { Ingredient } from "../recipes/ingredient";
import { ShoppingListService } from "./shopping-list.service";

@Component({
  selector: 'rb-shopping-list',
  templateUrl: './shopping-list.component.html',
  styleUrls: ['./shopping-list.component.css']
})
export class ShoppingListComponent implements OnInit {

    items: Ingredient[] = [];
    selectedItem: Ingredient = null;

    constructor(private sls: ShoppingListService) { }

    ngOnInit() {
        this.items = this.sls.getItems();
    }
    onSelectedItem(item: Ingredient) {
        this.selectedItem = item;
    }
    onCleared() {
        this.selectedItem = null;
    }
}
