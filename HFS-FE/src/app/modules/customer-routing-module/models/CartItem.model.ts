import { FoodImage } from "./foodimage.model"

export class CartItem{
    public CartItem(){

    }
    shopId : number
    shopName : string
    foodId : number
    amount :number
    unitPrice : number
    totalPrice: number
    name : string
    foodImages : FoodImage[]
}