export class Customer {
  customerId: number;
  firstName: string;
  lastName: string;
  gender: string;
  birthDate: Date;
  email: string;
  phoneNumber: string;
  avatar: string;
  isOnline: boolean;
  confirmedEmail:boolean
  isPhoneVerified:boolean;
  numberOfViolations:number;
  imagesBase64: CustomerImageBase64[] = null;
  orders: OrderInCom[] = null;
}


export interface OrderInCom {
  orderId: number;
  orderDate: string;
  shipAddress: string;
  shipperId: string;
  note: string;
}
export interface CustomerImageBase64 {
  imageId: number;
  imageBase64: string;
  name: string;
  size: string;
}
export class CustomerMessage {
  customerId: number;
  firstName: string;
  lastName: string;
  gender: string;
  birthDate: Date;
  email: string;
  phoneNumber: string;
  avatar: string;
  isOnline: boolean;
  isBanned: boolean;
  countMessageNotIsRead:number;
  image:string;
}
export class CustomerMessageOnline {
 email: string;
  countMessageNotIsRead:number;

}
