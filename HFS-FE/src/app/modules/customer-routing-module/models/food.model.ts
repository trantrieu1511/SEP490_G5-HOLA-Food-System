import { FoodImage } from "./foodimage.model"

export class Food
    {
        foodId : number
        name : string
        description : string
        categoryId : number
        unitPrice : number
        categoryName : string
        foodImages : FoodImage[]
        status : boolean
    }