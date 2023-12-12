export class Post {
  isLiked : boolean
  postId: number;
  likeCount : number;
  sellerId: string;
  shopName: string;
  postContent: string;
  createdDate: Date;
  settatus: string;
  imagesBase64: ImageBase64[] = null;
  isReported: boolean = false;
}


export interface ImageBase64 {
  imageId: number;
  imageBase64: string;
  name: string;
  size: string;
}

export class PostVote {
  isLike : boolean
  postId: number;
}