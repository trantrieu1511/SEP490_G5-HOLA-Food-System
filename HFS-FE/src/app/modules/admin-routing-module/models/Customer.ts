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
  isBanned: boolean;
  imagesBase64: CustomerImageBase64[] = null;
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
}
