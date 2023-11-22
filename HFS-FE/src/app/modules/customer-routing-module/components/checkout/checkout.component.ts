import {Component, OnInit, ViewChild} from '@angular/core';
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
import { CreateOrder, ListShop } from '../../models/CreateOrder.model';
import { AddToCart } from '../../models/addToCart.model';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent extends iComponentBase implements OnInit{
  loading: boolean;
  items : CartItem[]
  selectedOption: string = 'default';
  customAddress: string = '';
  defaultAddress: string = '';
  paymentOptions: string = 'cod'
  totalPrice : number
  note : string
  phone: string
  address : any[]
  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private route: ActivatedRoute,
    private router: Router,
    private dataService: DataService
  ){
    super(messageService);	
  }

  ngOnInit(){
    this.getAddress();
    if (this.dataService.getData() != null){
      this.items = this.dataService.getData();
    }
    else{
      this.router.navigate(['/cartdetail']);
    }
    console.log(this.items)
    this.calculate();
  }

  calculate(){
    this.totalPrice = 0;
    this.items.forEach(x => {
      this.totalPrice += x.amount * x.unitPrice
    })
  }

  async onCreateOrder(){
    try {
      this.loading = true;
      let checkoutInfor = new CreateOrder();
      checkoutInfor.shipAddress = this.selectedOption == 'default' ? this.defaultAddress : this.customAddress
      checkoutInfor.note = this.note
      checkoutInfor.paymentMethod = this.paymentOptions
      checkoutInfor.phone = this.phone
      checkoutInfor.listShop = []
      this.items.forEach(x =>{
        if(checkoutInfor.listShop.filter(e => e.shopId == x.shopId).length == 0){
          let shop = new ListShop()
          shop.shopId = x.shopId
          shop.cartItems = []
          let data = this.items.filter(o => o.shopId == x.shopId);
          data.forEach(x => {
            let cartitem = new AddToCart()
            cartitem.foodId = x.foodId
            cartitem.amount = x.amount
            shop.cartItems.push(cartitem)
          })
          
          checkoutInfor.listShop.push(shop)
        }
      })

      console.log(checkoutInfor)


      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.CHECKOUT, API.API_CHECKOUT.CREATE_ORDER, checkoutInfor);
      console.log(response)
      if (response) {
        if (response.message != "Success"){
          this.showMessage(mType.warn, "", response.message, 'notify');
        }
        else if (response.message === "Balance not enough!"){
          this.showMessage(mType.warn, "", "Balance not enough!", 'notify');
          window.open('https://www.google.com/', '_blank');
        }
        else{
          this.showMessage(mType.success, "", "Create Order success!", 'notify');
          this.router.navigate(['/cartdetail'])
        }         
      }
      else{
        this.showMessage(mType.warn, "", "You are not logged as customer!", 'notify');
      }

      this.loading = false;
  } catch (e) {
      console.log(e);
      this.loading = false;
  }
  }

  async getAddress(){
    try {
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.CHECKOUT, API.API_CHECKOUT.GET_ADDRESS, null);
      if (response && response.message === "Success") {
        this.address = response.listAddress
        console.log(this.address)
      }
      else{
        //this.router.navigate(['/login']);
      }

      this.loading = false;
  } catch (e) {
      console.log(e);
      this.loading = false;
  }
  }
}
