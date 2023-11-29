import { CartItem } from "./CartItem.model"
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

export class CartItemCheckout{
   
    shopId : number
    shopName : string
    foodId : number
    amount :number
    unitPrice : number
    totalPrice: number
    name : string
    selected: boolean = false
    foodImages : string
}

export class ListShopCheckout{
    public ListShop(){

    }
    shopId : number
    shopName: string
    totalPrice: number
    voucherPrice: number = 0
    foodCheckouts : FoodCheckout[]
}

export class FoodCheckout{
    foodId : number
    amount: number
    foodName: string
    foodImage: string
    unitPrice: number
    totalPrice: number;
}

export class ListShop{
    public ListShop(){

    }
    shopId : number
    shopName: string
    cartItems : AddToCart[]
}

