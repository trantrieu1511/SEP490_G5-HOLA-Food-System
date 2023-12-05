export class Food {
  foodId: number = null;
  sellerId: string = null;
  name: string = null;
  unitPrice: string = null;
  description: string = null;
  categoryName: string = null;
  categoryId: number = null;
  status: string = null;
  reportedTimes: number = null;
  banBy: string = null;
  banDate: string = null;
  banNote: string = null;
  rating: number = null;
  imagesBase64: FoodImageBase64[] = null;
}

export class Category {
  categoryId: number = null;
  name: string = null;
}

export interface FoodImageBase64 {
  imageId: number;
  imageBase64: string;
  name: string;
  size: string;
}

export class FoodInput {
  foodId: number = null;
  name: string = null;
  unitPrice: string = null;
  description: string = null;
  categoryId: number = null;
}

export class FoodInputValidation {
  isFoodIdValid: boolean = true;
  foodIdMessage: string = null;
  isNameValid: boolean = true;
  nameMessage: string = null;
  isUnitPriceValid: boolean = true;
  unitPriceMessage: string = null;
  isDescriptionValid: boolean = true;
  descriptionMessage: string = null;
  isCategoryIdValid: boolean = true;
  categoryIdMessage: string = null;
}

export class BanUnbanInputValidation {
  isBanNoteValid: boolean = true;
  banNoteMessage: string = null;
}

export class FoodDisplayHideInputDto {
  foodId: number;
  type: boolean;
}

export class FoodBanUnbanInputDto {
  foodId: number;
  isBanned: boolean;
  banNote: string;
}