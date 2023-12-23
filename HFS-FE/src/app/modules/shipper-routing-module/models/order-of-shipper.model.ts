import { da } from "@fullcalendar/core/internal-common";
import { OrderProgress } from "./order-progress-shipper.model";

export class OrderDaoOutputDto{
        orderId:number;
        customerId :number;
        orderDate : string;
        requiredDate : string;
        shippedDate:string;
        shipAddress :string;
        customerPhone:string;
        voucherId :number;
        paymentMethod:string;
        status :boolean;
        total: number;  
        orderDetails:OrderDetailDtoOutput[];  
        orderProgresses:OrderProgressDtoOutput[];  
        voucher: any;
}
export class OrderDetailDtoOutput{
    orderId :number;
    foodId :number;
    foodName :string;
    unitPrice : number;
    quantity : number;
    status :boolean;
    imageBase64:Images;
}

export class Images{
    imageBase64:string;
    name: string;
    size:string;
}

export class OrderProgressDtoOutput{
    imageBase64: Images;
    note: string;
    createDate: string;
    status: boolean;
}