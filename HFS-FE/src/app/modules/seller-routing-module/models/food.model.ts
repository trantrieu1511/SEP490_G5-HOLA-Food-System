export class Food{
  foodId: number;
  name: string;
  unitPrice: string;
  description: string;
  categoryName: string;
  categoryId: number;
  status: string;
  rating: number;
  imagesBase64: FoodImageBase64[];
}

export class Category{
  categoryId: number;
  name: string;
}

export interface FoodImageBase64 {
  imageId: number;
  imageBase64: string;
  name: string;
  size: string;
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