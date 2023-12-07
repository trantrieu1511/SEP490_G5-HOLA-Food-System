export class PostReport {
  postId: number;
  sellerName: string;
  shopName: string;
  postContent: string;
  reportBy: string;
  reportContent: string;
  reportContents: string[] = [];
  createDate: string;
  updateDate: string;
  updateBy: string;
  status: string;
  note: string;
}

export class PostInputValidation {
  isPostContentValid: boolean = true;
  postContentMessage: string;
}