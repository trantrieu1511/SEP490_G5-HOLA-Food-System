export class Post{
    postId :number;
    sellerId :string;
    postContent : string;
    createdDate : Date;
    settatus :string;
    imagesBase64: ImageBase64[] = null;
}


export interface ImageBase64 {
  imageId: number;
  imageBase64: string;
  name: string;
  size: string;
}  