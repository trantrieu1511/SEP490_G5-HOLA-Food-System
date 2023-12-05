export class reportSeller {
  sellerReportId: number;
  sellerName: string;
  shopName: string;
  reportContent: string;
  createDate: string;
  updateDate:Date;
  status: number;
  note: string;
  imagesBase64: ReportSellerImageBase64[] = null;
}


export class reportSellerDetail {
  sellerReportId: number;
  sellerId: string;
  sellerName: string;
  reportBy: string;
  reportByName: string;
  reportContent: string;
  createDate: Date;
  updateDate:string;
  updateBy:Date;
  status: number;
  note: string;

}
export interface ReportSellerImageBase64 {
  imageId: number;
  imageBase64: string;
  name: string;
  size: string;
}
export class ReportSellerInput{
  sellerReportId: number;
  status: number;
  note: string;
}
