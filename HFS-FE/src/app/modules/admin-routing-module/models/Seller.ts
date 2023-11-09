export class Seller {
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
