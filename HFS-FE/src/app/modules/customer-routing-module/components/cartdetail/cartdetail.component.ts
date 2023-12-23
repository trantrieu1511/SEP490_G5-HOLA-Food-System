import { Component, OnInit, ViewChild } from '@angular/core';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../../services/apiURL";
import {
  ConfirmationService,
  LazyLoadEvent,
  MenuItem,
  MessageService,
  SelectItem,
  TreeNode
} from "primeng/api";
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from 'src/app/services/data.service';
import { CartItem } from '../../models/CartItem.model';
import { CheckboxModule } from 'primeng/checkbox';
import { AddToCart } from '../../models/addToCart.model';
import { async } from '@angular/core/testing';
import { TranslateService } from '@ngx-translate/core';
import { ShoppingCartPopupService } from 'src/app/services/shopping-cart-popup.service';

@Component({
  selector: 'app-cartdetail',
  templateUrl: './cartdetail.component.html',
  styleUrls: ['./cartdetail.component.scss']
})

export class CartdetailComponent extends iComponentBase implements OnInit {

  loading: boolean;
  items: CartItem[]
  isSelectAll: boolean = false
  totalprice: number = 0
  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private route: ActivatedRoute,
    private router: Router,
    private dataService: DataService,
    public translate: TranslateService,
    private shoppingCartService: ShoppingCartPopupService
  ) {
    super(messageService);
  }

  ngOnInit() {
    this.getCartItem();
  }

  // get cart item
  async getCartItem() {
    try {
      this.loading = true;
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.CART, API.API_CART.CART_DETAIL, null);
      if (response && response.message === "Success") {
        this.items = response.listItem
        this.calculate();
      }
      else {
        this.router.navigate(['/login']);
      }

      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  // tính total price
  calculate() {
    this.totalprice = 0;
    this.items.forEach(element => {
      element.totalPrice = element.unitPrice * element.amount
      this.totalprice += element.selected ? element.totalPrice : 0
    });
  }

  // thêm food vào danh sách check out
  onSelectFood(foodId: number, amount: number) {
    let item = this.items.filter(item => item.foodId === foodId)
    if (item.length > 0) {
      item.forEach(x => {
        x.selected = !x.selected
      })
    }
    this.isSelectAll = false

    this.calculate();
  }

  onCheckOut() {
    if (this.totalprice != 0) {
      this.dataService.setData(this.items.filter(x => x.selected === true))
      this.router.navigate(['/checkout']);
    }
    else {
      this.showMessage(mType.warn, "", "Please select an item!", 'notify');
    }
  }

  async onChangeAmount(foodId: number, amount: number) {
    try {
      this.loading = true;
      let updateItem = new AddToCart();
      updateItem.foodId = foodId;
      updateItem.amount = amount;

      let item = this.items.filter(x => x.foodId === foodId)
      if (item.length > 0) {
        item.forEach(x => {
          x.amount = amount;
        })
      }

      this.calculate();

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.CART, API.API_CART.UPDATE_AMOUNT, updateItem);
      if (response && response.success === true) {
        this.shoppingCartService.onCheckOut();
      }
      else {
        // this.router.navigate(['/login']);
      }

      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  onSelectAll(event: any) {
    this.isSelectAll = event.target.checked;
    console.log(this.isSelectAll)
    if (this.isSelectAll === true) {
      this.items.forEach(x => {
        x.selected = true
      })
    }
    else {
      this.items.forEach(x => {
        x.selected = false
      })
    }

    this.calculate()
  }

  async onDeleteCartItem(foodId: number) {
    try {
      this.loading = true;
      let deleteitem = new AddToCart();
      deleteitem.foodId = foodId;
      const index = this.items.findIndex(item => item.foodId === foodId);
      if (index !== -1) {
        this.items.splice(index, 1);
      }
      this.calculate()

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.CART, API.API_CART.DELETE_ITEM, deleteitem);
      if (response && response.message === "Success") {
      }
      else {
        this.router.navigate(['/login']);
      }

      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  onFoodDetail(foodId : number){
    this.router.navigate(['/fooddetail'], { queryParams: { foodId: foodId } });
  }

  onShopDetail(shop: string) {
    this.router.navigate(['/shopdetail'], { queryParams: { shopid: shop } });
  }
}
