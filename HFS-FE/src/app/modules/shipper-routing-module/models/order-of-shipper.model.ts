export class OrderDaoOutputDto{
        orderId:number;
        customerId :number;
        orderDate : string;
        requiredDate : string;
        shippedDate:string;
        shipAddress :string;
        voucherId :number;
        status :boolean;
        total: number;  
        orderDetails:OrderDetailDtoOutput[];    
}
export class OrderDetailDtoOutput{
    orderId :number;
    foodId :number;
    foodName :string;
    unitPrice : number;
    quantity : number;
    status :boolean;
}