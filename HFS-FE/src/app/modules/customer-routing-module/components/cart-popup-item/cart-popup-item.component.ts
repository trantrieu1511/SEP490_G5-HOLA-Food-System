import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'app-cart-popup-item',
  templateUrl: './cart-popup-item.component.html',
  styleUrls: ['./cart-popup-item.component.scss']
})
export class CartPopupItemComponent {
  @Input() food: any = {};
  @Input() layoutService : any;
  @Output() onViewFood = new EventEmitter<number>();


  onViewFoodClick(){
    this.onViewFood.emit(this.food.foodId)
  }
}
