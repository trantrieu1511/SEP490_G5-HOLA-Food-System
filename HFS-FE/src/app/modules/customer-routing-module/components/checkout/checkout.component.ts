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
import { GetVoucherInput } from '../../models/GetVoucherInput.model';
import { UndoIcon } from 'primeng/icons/undo';
import { TranslateService } from '@ngx-translate/core';

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
  defaultAddress: any;
  paymentOptions: string = 'cod'
  totalPrice : number
  note : string
  phone: string
  voucher : string = ""
  displaySuccess : boolean = false
  listShop: any[];
  listShop2: any[];
  haveVoucher: boolean = false;
  totalPriceVoucher: number;
  balance : number;

  isDifferentAddress: boolean = false;

  timeOutVoucherInput: any = null;

  provinceSelected: any;
  districtSelected: any;
  wardSelected: any;
  addressDetailSelected: any;
  voucherDetail : any;

  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private route: ActivatedRoute,
    private router: Router,
    private dataService: DataService,
    public translate: TranslateService 
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

    if (!this.haveVoucher && this.listShop != null){
      this.listShop.forEach(x => {
        if (x.voucherPrice != 0) this.haveVoucher = true;
      })
    }

    if (this.haveVoucher){
      this.totalPriceVoucher = 0;
      this.listShop.forEach(x => {
        this.totalPriceVoucher += x.voucherPrice
      })
    }
  }

  async onCreateOrder(){
    try {
      this.loading = true;
      if (!this.phone || this.phone.trim().length === 0){
        this.showMessage(mType.warn, "", "Phone is required!", 'notify');
        return;
      }    
      let checkoutInfor = new CreateOrder();
      if (this.address.length == 0 && !this.isDifferentAddress){
        this.showMessage(mType.warn, "", "You don't have any address inprofile, please choose different address!", 'notify');
        return;
      }

      checkoutInfor.shipAddress = !this.isDifferentAddress ? 
        this.defaultAddress.addressInfo : 
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
      if (this.paymentOptions === "Wallet" && this.balance < (this.totalPrice - this.totalPriceVoucher)){
        this.showMessage(mType.warn, "", "Wallet not enough", 'notify');
        return;
      }
      checkoutInfor.phone = this.phone
      //checkoutInfor.voucher = this.voucher.trim();
      checkoutInfor.listShop = [];
      this.items.forEach(x =>{
        if(checkoutInfor.listShop.filter(e => e.shopId == x.shopId).length == 0){
          let shop = new ListShop()
          shop.shopId = x.shopId
          shop.cartItems = []
          let data1 = this.listShop.filter(o => o.shopId == x.shopId);
          data1.forEach(x => {
            shop.voucher = x.voucher
          })

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
          this.displaySuccess = true;
          this.showMessage(mType.success, "", "Create Order success!", 'notify');
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
        this.phone = response.phone
        this.balance = response.balance
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

  checkVoucher(event: any, shopId: string){
    this.listShop.forEach(x => {
      if (x.shopId === shopId){
        x.voucher = "";
        x.voucherPrice = 0;        
      }
    })
    this.haveVoucher = false;
    this.calculate();
    let voucherCd = event.target.value;
    if (voucherCd.length === 0) return;

    clearTimeout(this.timeOutVoucherInput);
    this.timeOutVoucherInput = setTimeout(async () => {
      await this.onGetVoucher(voucherCd);
      
      if (this.voucherDetail === undefined){
        return;
      }
      let voucher = this.voucherDetail
      console.log(voucher);
      if (voucher.sellerId != shopId){
        this.showMessage(mType.warn, "", "Voucher " + voucherCd + " can't use for this shop!", 'notify');
        return;
      }

      if (!voucher.isEffective){
        this.showMessage(mType.warn, "", "Voucher " + voucherCd + " can't use now!", 'notify');
        return;
      }

      if (voucher.isExpired){
        this.showMessage(mType.warn, "", "Voucher " + voucherCd + " can't use now!", 'notify');
        return;
      }

      if (voucher.isUsed){
        this.showMessage(mType.warn, "", "Voucher " + voucherCd + " have been used!", 'notify');
        return;
      }

      this.listShop.forEach(x => {
        if (x.shopId === shopId){
          if (x.totalPrice >= voucher.minValue){
            this.haveVoucher = true;
            x.voucher = voucherCd;
            x.voucherPrice = voucher.discount;
          }
        }
      })


      this.listShop = this.listShop.map(shop => {
        if (shop.shopId === shopId) {
           return { ...shop, voucher: event.target.value };
        }
        return shop;
      });

      console.log(this.listShop)
      this.calculate();
     //check api thành công thì 
          //1. set haveVoucher = true (hiển thị số tiền trừ ở voucher tổng)
          //2. set voucherPrice trả về từ BE vào listShop để hiển thị số tiền vouvher trừ (voucherPrice !=0 sẽ hiển thị )     
          //3. tính toán lại cho totalPrice order trong listShop by shopId param
          //4. và tính lại total của cả Order (biến totalPrice)
    }, 1000); //TH nếu vẫn input thì sẽ ch gọi api check voucher vội
  }

  async onGetVoucher(voucher : string){
    try {
      const voucherInput = {voucher : voucher}
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.CHECKOUT, API.API_CHECKOUT.GET_VOUCHER, voucherInput);
      if (response && response.message === "Success") {
        this.voucherDetail = response
        console.log(this.voucherDetail)
      }
      else{
        this.showMessage(mType.warn, "", "Voucher Invalid, you will not have discount!", 'notify');
        this.voucherDetail = undefined;
      }

      this.loading = false;
  } catch (e) {
      console.log(e);
      this.loading = false;
  }
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

  onClose(){
    this.displaySuccess = false;
    this.router.navigate(['/orderhistory'])
  }

}
