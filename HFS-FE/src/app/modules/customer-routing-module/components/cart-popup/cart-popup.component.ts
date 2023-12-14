import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { MessageService } from 'primeng/api';
import { Food } from 'src/app/modules/seller-routing-module/models/food.model';
import { iComponentBase, iFunction, iServiceBase } from 'src/app/modules/shared-module/shared-module';
import { AuthService } from 'src/app/services/auth.service';
import * as API from "../../../../services/apiURL";
import { ShoppingCartPopupService } from 'src/app/services/shopping-cart-popup.service';
import { Subscription } from 'rxjs';
import { FoodPopUp } from '../../models/food-popup.model';


@Component({
  selector: 'app-cart-popup',
  templateUrl: './cart-popup.component.html',
  styleUrls: ['./cart-popup.component.scss']
})
export class CartPopupComponent extends iComponentBase implements OnInit, OnDestroy{
  @Input() layoutService : any;

  lstFoodCart: any[] = []
  role: any
  amountCart: string = ''

  isNewAddCart: boolean = false;

  newFoodCartSubscription: Subscription

  checkOutSubscription: Subscription

  constructor(
    private iServiceBase: iServiceBase,
    public authService: AuthService,
    private _route: Router,
    public messageService: MessageService,
    public translate: TranslateService,
    private iFunction: iFunction,
    private shoppingCartService: ShoppingCartPopupService
    
  ){
    super(messageService);

    const user = this.authService.getUserInfor()
    if(user != null)
      this.role = user.role

    if(this.role == 'CU'){
      this.getAllFoodCart()

      this.newFoodCartSubscription = this.shoppingCartService.newFoodCartHandle.subscribe((res: FoodPopUp) => {
        this.isNewAddCart = true;
        this.getAllFoodCart();
        //this.mapCart(res);

        // call api cart
      })

      this.checkOutSubscription = this.shoppingCartService.checkOutHandle.subscribe(res => {
        this.getAllFoodCart();
      })
    }
    this.isNewAddCart = false;
  }

  ngOnInit() {
    

    
  }

  ngOnDestroy() {
    if(this.newFoodCartSubscription){
      this.newFoodCartSubscription.unsubscribe();
    }

    if(this.checkOutSubscription){
      this.checkOutSubscription.unsubscribe();
    }

  }

  async getAllFoodCart(){

    //api need return amountCart
    this.lstFoodCart = []
    try {
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.CART, API.API_CART.CART_ITEM_POPUP, null);
      if (response && response.message === "Success") {
        this.lstFoodCart = response.listItem

        if(this.lstFoodCart && response.total >= 100){
          this.amountCart = '99+'
          return
        }
        
        this.amountCart = !this.lstFoodCart || response.total == 0 ? '0' : response.total
      }
    } catch (e) {
      console.log(e);
    }
  }

  onViewFood(foodId: number){
    console.log("clicked", foodId);
    // redirect here
  
  }

  onViewCart(event: any){
    event.preventDefault();
    // redirect here
  }

  mapCart(food: FoodPopUp){

    let isDuplicate = false;
    this.lstFoodCart = this.lstFoodCart.map(element => {
      if(element.foodId == food.foodId){
        element.amount++
        isDuplicate = true
      }
      return element
    })

    if(!isDuplicate ){
      if(this.lstFoodCart.length >= 4)
        this.lstFoodCart.pop()
      this.lstFoodCart.unshift(food)
      this.amountCart = this.amountCart == '99+' ? '99+' : (Number.parseInt(this.amountCart) + 1).toString()
    }
  }

  onViewCartPopup(){
    this.isNewAddCart = false;
  }
}
