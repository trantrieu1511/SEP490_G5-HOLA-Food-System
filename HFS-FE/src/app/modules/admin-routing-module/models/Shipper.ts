export class Shipper {
  shipperId: string;
  shipperName: string;
  gender: string;
  birthDate: Date;
  email: string;
  phoneNumber: string;
  avatar: string;
  confirmedEmail:boolean;
  isBanned: boolean;
  isVerified:boolean;
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
export class ActiveShipper {
  shipperId: string;
  isVerified:boolean;
}
