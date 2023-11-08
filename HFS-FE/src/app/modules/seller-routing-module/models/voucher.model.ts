export class Voucher{
    voucherId:number;
    sellerId:string;
    code:string;
    discountAmount:number;
    minimumOrderValue:number;
    effectiveDate:Date;
    expireDate:Date;
    status:number;
}
export class VoucherCreate{
    sellerId:string;
    discountAmount:number;
    minimumOrderValue:number;
    effectiveDate:Date;
    expireDate:Date;
    status:number;
}

export class VoucherInput{
    voucherId:number;
    sellerId:string;
    discountAmount:number;
    minimumOrderValue:number;
    effectiveDate:Date;
    expireDate:Date;
    status:number;
}
