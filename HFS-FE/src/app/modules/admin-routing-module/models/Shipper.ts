export class Shipper {
  shipperId: string;
  shipperName: string;
  gender: string;
  birthDate: Date;
  email: string;
  phoneNumber: string;
  avatar: string;
  isPhoneVerified:boolean;
  confirmedEmail:boolean;
  isBanned: boolean;
  manageBy:string
  status:number;
  note:string;
  createDate:Date;
  idcardNumber:string;
  imageBase64Id1:string;
  imageBase64Id2:string;
  imagesBase64: ShipperImageBase64[] = null;
}
export interface ShipperImageBase64 {
  imageId: number;
  imageBase64: string;
  name: string;
  size: string;
}
export class  HistoryBanShipper{
  banShipperId: number;
  reason:string;
  createDate:Date;
}
export class BanShipper {
  shipperId: string;
  reason:string;
  isBanned: boolean;
}
export class ActiveShipper1 {
  shipperId: string;
  isVerified:boolean;
}
export class ActiveShipper {
  shipperId: string;
  note:string;
  status: number;
}
