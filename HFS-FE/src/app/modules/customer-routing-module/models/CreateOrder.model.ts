import { AddToCart } from "./addToCart.model"

export class CreateOrder{
    public CreateOrder(){

    }
    voucher : string
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