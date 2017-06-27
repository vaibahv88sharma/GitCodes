import { Ingredient } from "./ingredient";

export class Recipe {
    constructor(
        public name: string,
        public description: string,
        public imagePath: string,
        public recipeId: string,
        public ingredients: Ingredient[]
    ) { }
}