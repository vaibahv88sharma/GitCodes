import { Component, OnInit } from '@angular/core';
import { DropdownDirective } from './dropdown.directive';
import { RecipeService } from "./recipes/recipe.service";
import { Recipe } from "./recipes/recipe";

@Component({
  selector: 'rb-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

    private recipes: Recipe[];
    private recipesPosted: Recipe[] = [];    
    private body: string;

  constructor(private recipeService: RecipeService) { }

  ngOnInit() {
  }

    

  onStore() {

      for (var i = 0; i < this.recipeService.recipes.length; i++) {

          //this.recipesPosted = [];

          var obj = {
              '__metadata': {
                  'type': this.recipeService.getItemTypeForListName('recipe')
              },
              'recipe': this.recipeService.recipes[i]['name'],
              'image': this.recipeService.recipes[i]['imagePath'],
              'description': this.recipeService.recipes[i]['description']
          };
          this.body = JSON.stringify(obj);

          this.recipeService.storeData(
              //this.recipeService.recipes[i],
              this.body,
              'recipe'
                  ).subscribe(
              (data: any) => this.createIngredientsLookup(data),
                      (error: any) => console.error(error)
          );
      }
  }

  private createIngredientsLookup(data: any) {
      console.log(data);

      for (var i = 0; i < this.recipeService.recipes.length; i++) {
          if (this.recipeService.recipes[i]['name'] === data.d.recipe ){
              for (var j = 0; j < this.recipeService.recipes[i]["ingredients"].length; j++) {
                  var obj = {
                      '__metadata': {
                          'type': this.recipeService.getItemTypeForListName('ingredient')
                      },
                      'name': this.recipeService.recipes[i]["ingredients"][j]["name"],
                      'amount': this.recipeService.recipes[i]["ingredients"][j]["amount"]
                      ,'recipeId': data.d.Id,
                  };
                  this.body = JSON.stringify(obj);

                  this.recipeService.storeData(
                      //this.recipeService.recipes[i],
                      this.body,
                      'ingredient'
                  ).subscribe(
                      (data: any) => console.log(data),
                      (error: any) => console.error(error)
                      );
              }
          }
      }

      //this.recipesPosted.push(
      //    new Recipe(data.d.recipe, "", "", data.d.Id, [])
      //);
      
      
      
  }

  onFetch() {
      this.recipeService.fetchData('ingredient');
  }
}
