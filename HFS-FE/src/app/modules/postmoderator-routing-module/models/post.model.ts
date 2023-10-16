export class Post {
    postId: number;
    userId: number;
    userFirstName: string;
    postContent: string;
    createdDate: Date;
    status: boolean;
    postImages: [];
}

export class PostImage {
    imageId: number;
    postId: number;
    path: string;
}