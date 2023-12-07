
export class FeedBackSeller {
feedbackId :number;
  foodId :number;
  foodName:string;
  customerId :string;
customerName :string;
 feedbackMessage :string;
star :number;
displayDate :Date;
 likeCount :number;
 disLikeCount:number;
 checkReply :boolean;
 imagesBase64: FeedBackSellerImageBase64[] =null;
}


export interface FeedBackSellerImageBase64 {
  imageId: number;
  imageBase64: string;
  name: string;
  size: string;
}

export class ReplySeller {
  feedbackId :number;
    customerId :string;
    sellerId:string;
    replyMessage:string;

  }
