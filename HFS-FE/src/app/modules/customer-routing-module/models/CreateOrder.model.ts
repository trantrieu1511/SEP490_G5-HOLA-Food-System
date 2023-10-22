import { AddToCart } from "./addToCart.model"

export class CreateOrder{
    public CreateOrder(){

    }
    shipAddress : string
    voucherId : string
    phone: string
    note: string
    paymentMethod: string
    listShop : ListShop[]
}

export class ListShop{
    public ListShop(){

    }
    shopId : number
    cartItems : AddToCart[]
}