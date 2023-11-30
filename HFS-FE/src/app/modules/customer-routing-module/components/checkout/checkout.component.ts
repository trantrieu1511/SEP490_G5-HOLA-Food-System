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
import { CartItemCheckout, CreateOrder, FoodCheckout, ListShop, ListShopCheckout } from '../../models/CreateOrder.model';
import { AddToCart } from '../../models/addToCart.model';
import { FailedToNegotiateWithServerError } from '@microsoft/signalr/dist/esm/Errors';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent extends iComponentBase implements OnInit{
  loading: boolean;
  items : CartItemCheckout[]
  selectedOption: string = 'default';
  customAddress: string = '';
  address : any[]
  defaultAddress: string = '';
  paymentOptions: string = 'cod'
  totalPrice : number
  note : string
  phone: string
  voucher : string = ""
  displayConfirmOrder : boolean = true
  listShop: any[];

  haveVoucher: boolean = false;
  totalPriceVoucher: number;

  isDifferentAddress: boolean = false;

  timeOutVoucherInput: any = null;

  provinceSelected: any;
  districtSelected: any;
  wardSelected: any;
  addressDetailSelected: any;

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

  async ngOnInit(){
    await this.getAddress();
    if (this.dataService.getData() != null){
      this.items = this.dataService.getData();
      this.splitOrderBySeller();
      this.calculate();
    }
    else{
      this.router.navigate(['/cartdetail']);
    }
    this.defaultAddress = this.address[0].addressInfo;
    console.log(this.defaultAddress)
    

    
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
      if (!this.phone || this.phone.trim().length === 0){
        this.showMessage(mType.warn, "", "Phone is required!", 'notify');
        return;
      }    
      let checkoutInfor = new CreateOrder();
      checkoutInfor.shipAddress = !this.isDifferentAddress ? 
        this.defaultAddress : 
        `${this.addressDetailSelected}, ${this.wardSelected}, ${this.districtSelected}, ${this.provinceSelected}`
      
      
      if (!checkoutInfor.shipAddress || checkoutInfor.shipAddress.trim().length === 0){
        this.showMessage(mType.warn, "", "Address is required!", 'notify');
        return;
      }

      if(this.isDifferentAddress){
        if(!this.provinceSelected){
          this.showMessage(mType.warn, "", "Province is required!", 'notify');
          return;
        }

        if(!this.districtSelected){
          this.showMessage(mType.warn, "", "District is required!", 'notify');
          return;
        }

        if(!this.wardSelected){
          this.showMessage(mType.warn, "", "Ward is required!", 'notify');
          return;
        }

        if(!this.addressDetailSelected){
          this.showMessage(mType.warn, "", "Address detail is required!", 'notify');
          return;
        }
      }

      checkoutInfor.note = this.note
      checkoutInfor.paymentMethod = this.paymentOptions
      checkoutInfor.phone = this.phone
      checkoutInfor.voucher = this.voucher.trim();
      checkoutInfor.listShop = this.listShop;
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
      const totalPriceLast = this.totalPrice - (this.haveVoucher ? this.totalPriceVoucher : 0);
      // if(wallet < totalPriceLast){
      //   this.showMessage(mType.warn, "", "Wallet not enough", 'notify');
      //   return
      // }

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

  splitOrderBySeller(){
    console.log(this.items)
    this.listShop = [];

    this.items.forEach(x =>{
      if(this.listShop.filter(e => e.shopId == x.shopId).length == 0){
        let shop = new ListShopCheckout()
        shop.shopId = x.shopId
        shop.shopName = x.shopName
        shop.foodCheckouts = []
        let data = this.items.filter(o => o.shopId == x.shopId);
        
        let totalPriceShop = 0;
        data.forEach(x => {
          let cartitem = new FoodCheckout()
          cartitem.foodId = x.foodId
          cartitem.amount = x.amount
          cartitem.foodImage = x.foodImages
          cartitem.foodName = x.name
          cartitem.unitPrice = x.unitPrice
          cartitem.totalPrice = x.amount * x.unitPrice
          shop.foodCheckouts.push(cartitem)
          totalPriceShop += cartitem.totalPrice
        })

        shop.totalPrice = totalPriceShop
        
        this.listShop.push(shop)
      }
    })

    console.log(this.listShop);
  }

  checkVoucher(event: any, shopId: number){
    
    clearTimeout(this.timeOutVoucherInput);

    this.timeOutVoucherInput = setTimeout(() => {
      console.log('goi check voucher')
      this.listShop = this.listShop.map(shop => {
        if (shop.shopId === shopId) {
           return { ...shop, voucher: event.target.value };
        }
        return shop;
      });

      console.log(this.listShop)
     //check api thành công thì 
          //1. set haveVoucher = true (hiển thị số tiền trừ ở voucher tổng)
          //2. set voucherPrice trả về từ BE vào listShop để hiển thị số tiền vouvher trừ (voucherPrice !=0 sẽ hiển thị )     
          //3. tính toán lại cho totalPrice order trong listShop by shopId param
          //4. và tính lại total của cả Order (biến totalPrice)
    }, 1000); //TH nếu vẫn input thì sẽ ch gọi api check voucher vội

   
  }

  proviceOutput(province: string){
    this.provinceSelected = province
    console.log(this.provinceSelected)
  }

  districtOutput(district: string){
    this.districtSelected = district
    console.log(this.districtSelected)
  }

  wardOutput(ward: string){
    this.wardSelected = ward
    console.log(this.wardSelected)
  }

  addressOutput(address: string){
    this.addressDetailSelected = address
    console.log(this.addressDetailSelected)
  }

  onClickWallet(){
    //request api get wallet
  }

}
