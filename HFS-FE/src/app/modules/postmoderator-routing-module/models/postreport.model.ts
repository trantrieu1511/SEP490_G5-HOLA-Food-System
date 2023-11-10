export class PostReport {
  postId: number;
  reportBy: string
  reportContent: string;
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