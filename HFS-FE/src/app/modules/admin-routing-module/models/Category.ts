export class Category {
    categoryId :number ;
    name :string ;
    status :string ;
  }

  export class CategoryInput {
    categoryId :number ;
    name :string ;
    status :string ;
  }

  export class CateDisplayHideInputDto{
    categoryId: number;
    type: boolean;
  }