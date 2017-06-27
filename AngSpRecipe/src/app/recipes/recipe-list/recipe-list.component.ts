import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Recipe } from '../recipe';
import { RecipeService } from "../recipe.service";

@Component({
  selector: 'rb-recipe-list',
  templateUrl: './recipe-list.component.html',
  styleUrls: ['./recipe-list.component.css']
})
export class RecipeListComponent implements OnInit {

    recipes: Recipe[] = [];
    //recipes: Recipe[] = [
    //    new Recipe('Biryani', 'Very Spicy', '//staffportal.myselfserve.com.au/sites/dev/Style%20Library/App/Vegetable-biriyani.jpg',[]),
    //    new Recipe('Cauliflower Potato', 'Tasty', '//staffportal.myselfserve.com.au/sites/dev/Style%20Library/App/Cauliflower-Potato.jpg',[])
    //];

    //@Output() recipeSelected = new EventEmitter<Recipe>();

    //recipe = new Recipe('Dummy', 'Dummy', '//staffportal.myselfserve.com.au/sites/dev/Style%20Library/App/dummy.jpg');
  constructor(private recipeService: RecipeService) { }

  ngOnInit() {
      this.recipes = this.recipeService.getRecipes();
  }

  //onSelected(recipe: Recipe) {
  //  this.recipeSelected.emit(recipe);
  //}

}
