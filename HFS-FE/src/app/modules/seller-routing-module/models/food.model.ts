export class Food{
  foodId: number = null;
  name: string = null;
  unitPrice: string = null;
  description: string = null;
  categoryName: string = null;
  categoryId: number = null;
  status: string = null;
  rating: number = null;
  imagesBase64: FoodImageBase64[] = null;
}

export class Category{
  categoryId: number = null;
  name: string = null;
}

export interface FoodImageBase64 {
  imageId: number;
  imageBase64: string;
  name: string;
  size: string;
}

export class FoodInput{
  foodId: number = null;
  name: string = null;
  unitPrice: string = null;
  description: string = null;
  categoryId: number = null;
}

export class FoodDisplayHideInputDto{
  foodId: number;
  type: boolean;
}