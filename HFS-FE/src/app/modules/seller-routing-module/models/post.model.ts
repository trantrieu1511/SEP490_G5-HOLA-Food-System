export class Post {
    postId: number;
    postContent: string;
    createdDate: string;
    status: string;
    images: PostImage[];
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