export class Seller {
  sellerId: string;
  gender: string;
  birthDate: Date;
  email: string;
  phoneNumber: string;
  isOnline:boolean;
  walletBalance:number;
  avatar: string;
  shopName:string;
  shopAddress:string;
  confirmedEmail:boolean;
  isBanned: boolean;
  isVerified:boolean;
  businessCode:string;
  imagesBase64: SellerImageBase64[] = null;
  imagesBase64L: SellerImageBase64[] = null;
}
export interface SellerImageBase64 {
  imageId: number;
  imageBase64: string;
  name: string;
  size: string;
}
export class SellerMessage {
  sellerId: string;
  firstName: string;
  lastName: string;
  gender: string;
  birthDate: Date;
  email: string;
  phoneNumber: string;
  isOnline:boolean;
  walletBalance:number;
  avatar: string;
  shopName:string;
  shopAdress:string;
  confirmedEmail:boolean;
  isBanned: boolean;
  isVerified:boolean;
  countMessageNotIsRead:number;
  image:string;
}
export class  HistoryBanSeller{
  banSellerId: number;
  reason:string;
  createDate:Date;
}
export class BanSeller {
  sellerId: string;
  reason:string;
  isBanned: boolean;
}
export class ActiveSeller {
  sellerId: string;
  isVerified:boolean;
}
