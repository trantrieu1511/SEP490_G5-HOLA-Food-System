// export class Post {
//     postId: number;
//     userId: number;
//     userFirstName: string;
//     postContent: string;
//     createdDate: Date;
//     status: boolean;
//     postImages: [];
// }

// export class PostImage {
//     imageId: number;
//     postId: number;
//     path: string;
// }

export class Post {
    postId: number;
    postContent: string;
    createdDate: string;
    status: string;
    imagesBase64: PostImageBase64[];
  }
  
  export class PostImage{
  
    constructor(imageId: number, postId: number, image: string) {
      this.imageId = imageId;
      this.postId = postId;
      this.image = image;
    }
  
    imageId: number;
    postId: number;
    image: string;
      
  }
  
  export interface PostImageBase64 {
    imageId: number;
    imageBase64: string;
    name: string;
    size: string;
  }
  
  export class PostInput{
    postId: number;
    postContent: string;
    images: File[];
  }
  
  export class PostInputValidation{
    isPostContentValid: boolean = true;
    postContentMessage: string;
  }
  
  export class PostBanUnbanInputDto{
    postId: number;
    isBanned: boolean;
  }