export class Order{
  orderId: number;
  customerId: string;
  customerName: string;
  orderDate: string;
  shippedDate: string;
  shipAddress: string;
  shipperId: string;
  shipperName: string;
  voucherId: string;
  discountAmount: number;
  paymentMethod: string;
  totalPrice: number;
  orderProgresses: DetailProgress[];
  orderDetails: OrderDetailFoodDto[];
}

export class DetailProgress{
  imageBase64: ImageOrderProgress;
  note: string;
  createDate: string;
}

export class ImageOrderProgress{
  imageBase64: string;
  name: string;
  size: string;
}

export class OrderDetailFoodDto{
  orderId: number;
  foodId: number;
  foodName: string;
  unitPrice: number;
  quantity: number;
  imageBase64: ImageFoodOutputDto;
  categoryName: string;
}

export class ImageFoodOutputDto{
  imageId: number;
  imageBase64: string;
  name: string;
  size: string;
}

export class OrderStatusInput{
  status: number;
  dateFrom: string;
  dateEnd: string;
}

export class OrderCancelInput{
  orderId: number;
  status: number;
  note: string;
}

export class OrderCancelInputValidation{
  isNoteValid: boolean = true;
  noteMessage: string;
}

export class OrderAcceptInput{
  constructor(orderId: number, status: number){
    this.orderId = orderId;
    this.status = status;
  }

  orderId: number;
  status: number;
}

export class OrderInternalInput{
  constructor(orderId: number, shipperId: number){
    this.orderId = orderId;
    this.shipperId = shipperId;
  }

  orderId: number;
  shipperId: number;
}

export class OrderExternalInput{
  constructor(orderId: number){
    this.orderId = orderId;
  }

  orderId: number;
}

