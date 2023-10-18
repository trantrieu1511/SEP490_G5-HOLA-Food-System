export class Food{
  foodId: number;
  name: string;
  description: string;
  categoryName: string;
  categoryId: number;
  images: FoodImage[];
  status: string;
  rating: number;
}

export class Category{
  categoryId: number;
  name: string;
}


export class FoodImage{

  constructor(imageId: number, foodId: number, image: string) {
    this.imageId = imageId;
    this.foodId = foodId;
    this.image = image;
  }

  imageId: number;
  foodId: number;
  image: string;
}