import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { FoodPopUp } from '../modules/customer-routing-module/models/food-popup.model';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartPopupService {
  private newFoodCart = new BehaviorSubject<any>(null);

  newFoodCartHandle = this.newFoodCart.asObservable();

  private checkOut = new BehaviorSubject<any>(null);

  checkOutHandle = this.checkOut.asObservable();

  constructor() { }

  onAddToCart(){
    this.newFoodCart.next(null)
  }

  onCheckOut(){
    this.checkOut.next(null);
  }


}
