export class SellerReport {
  public SellerReport(){

  }
  sellerId: string;
  //reportBy: string;
  reportContent: string;
  reportContents: string[] = [];
 // createDate: string;
 // updateDate: string;
//updateBy: string;
//status: string;
//note: string;
  images: File[];
}
// public string SellerId { get; set; } = null!;
// public string ReportBy { get; set; } = null!;
// public string ReportContent { get; set; } = null!;
// public DateTime CreateDate { get; set; }
// public DateTime? UpdateDate { get; set; }
// public string? UpdateBy { get; set; }
// public byte Status { get; set; }
// public string? Note { get; set; }
export class PostInputValidation {
  isPostContentValid: boolean = true;
  postContentMessage: string;
}
export class SellerReportInput{
  public SellerReportInput(){

  }
  foodId : number
  feedBackMessage : string = "";
  star : number;
  images: File[];
}
