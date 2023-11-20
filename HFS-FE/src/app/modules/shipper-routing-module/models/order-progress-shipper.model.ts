export class OrderProgress{
    orderProgressId:number;
    note : string;
    createDate : string;
    imageBase64:Images;
    orderId : number;
    status : number;
    userId : number;
}

export class Images{
    imageBase64:string;
    name: string;
    size:string;
}