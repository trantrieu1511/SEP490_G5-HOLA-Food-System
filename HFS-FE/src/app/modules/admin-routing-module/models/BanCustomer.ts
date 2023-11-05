export class BanCustomer {
  customerId: number;
  reason:string;
  isBanned: boolean;
}
export class HistoryBanCustomer {
  banCustomerId: number;
  reason:string;
  createDate:Date;
}
