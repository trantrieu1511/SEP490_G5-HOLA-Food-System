export class Food{
  foodId: number;
  name: string;
  unitPrice: string;
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



export interface FoodImageBase64 {
  imageId: number;
  imageBase64: string;
  name: string;
}

export class FoodInput{
  foodId: number;
  name: string;
  unitPrice: string;
  description: string;
  categoryId: number;
  imagesBase64: FoodImageBase64[];
}

export class FoodDisplayHideInputDto{
  foodId: number;
  type: boolean;
}