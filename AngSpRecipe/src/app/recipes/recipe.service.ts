import { Injectable } from '@angular/core';
import { Http, Headers, Response } from "@angular/http";
import { Observable } from "rxjs/Rx";

import { Recipe } from "./recipe";
import { Ingredient } from "./ingredient";
//import { REST } from "../shared/REST";

@Injectable()
export class RecipeService {

    public recipes: Recipe[] = [
        new Recipe('Biryani', 'Very Spicy', '//staffportal.myselfserve.com.au/sites/dev/Style%20Library/App/Vegetable-biriyani.jpg','', [
            new Ingredient('Rice', 2),
            new Ingredient('Vegitable', 1),
        ]),
        new Recipe('Cauliflower Potato', 'Tasty', '//staffportal.myselfserve.com.au/sites/dev/Style%20Library/App/Cauliflower-Potato.jpg','', [])
    ];


    // REST
    private hostweburl: string = decodeURIComponent(this.manageQueryStringParameter("SPHostUrl"));
    private appweburl: string = decodeURIComponent(this.manageQueryStringParameter("SPAppWebUrl"));
    public spApiUrl: string;
    private spListName: string = 'recipe';


    constructor(private http: Http) {

        //REST
        // this.spApiUrl = this.appweburl + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('Demo')/items?" +
        //    "@target='" + this.hostweburl + "'";

    }

    getRecipes() {
        return this.recipes;
    }
    getRecipe(id: number) {
        return this.recipes[id];
    }
    deleteRecipe(recipe: Recipe) {
        this.recipes.splice(this.recipes.indexOf(recipe), 1);
    }
    addRecipe(recipe: Recipe) {
        this.recipes.push(recipe);
    }

    editRecipe(oldRecipe: Recipe, newRecipe: Recipe) {
        this.recipes[this.recipes.indexOf(oldRecipe)] = newRecipe;
    }

    //storeData(recipeItems: Recipe, listName: string) {
    storeData(body: string, listName: string) {

        const headers = this.getHeaders();
        return this.http.post(
            this.getURL(listName),
                body,
                { headers: this.getHeaders("POST") })
            .map((data: Response) => data.json())
            .catch(this.handleError);
    }

    fetchData(listName: string) {
        return this.http.get(
            this.getURL(listName) + "&$select=name,amount,recipe/recipe,recipe/image,recipe/description&$expand=recipe/recipe",
            { headers: this.getHeaders() }
        )
            .map((response: Response) => response.json())
            .catch(this.handleError)
            .subscribe(
                (data: Recipe[]) => {
                    this.recipes = data.d.results;
                }
            );
    }


    // REST Error Handling
    private handleError(error: any) {
        console.log(error);
        return Observable.throw(error.json());
    }

    // List URL
    public getURL(listName: string) {
        return this.spApiUrl = this.appweburl + "/_api/SP.AppContextSite(@target)/web/lists/getbytitle('" + listName + "')/items?" +
            "@target='" + this.hostweburl + "'";
    }

    //REST GET HEADERS, this function resolves the headers according to the verb that we are using
    public getHeaders(verb?: string) {
        var headers = new Headers();
        //var digest = document.getElementById('__REQUESTDIGEST').value;
        var digest = (<HTMLInputElement>document.getElementById('__REQUESTDIGEST')).value;

        headers.set('X-RequestDigest', digest);
        headers.set('Accept', 'application/json;odata=verbose');

        switch (verb) {
            case "POST":
                headers.set('Content-type', 'application/json;odata=verbose');
                break;
            case "PUT":
                headers.set('Content-type', 'application/json;odata=verbose');
                headers.set("IF-MATCH", "*");
                headers.set("X-HTTP-Method", "MERGE");
                break;
            case "DELETE":
                headers.set("IF-MATCH", "*");
                headers.set("X-HTTP-Method", "DELETE");
                break;
        }
        return headers;
    }


    // REST URL
    public manageQueryStringParameter(paramToRetrieve: any) {
        var params =
            document.URL.split("?")[1].split("&");
        //var strParams = "";
        for (var i = 0; i < params.length; i = i + 1) {
            var singleParam = params[i].split("=");
            if (singleParam[0] == paramToRetrieve) {
                return singleParam[1];
            }
        }
    }
    // REST SP.Data.List
    public getItemTypeForListName(name: string) {
        return "SP.Data." + name.charAt(0).toUpperCase() + name.split(" ").join("").slice(1) + "ListItem";
    }
}
