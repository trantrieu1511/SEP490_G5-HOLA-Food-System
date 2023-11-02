import {Component, OnInit, ViewChild} from '@angular/core';
import {
    iComponentBase,
    iServiceBase, mType,
    ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../../services/apiURL";
import {
    ConfirmationService,
    MessageService,
    SelectItem,
} from "primeng/api";
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from 'src/app/services/data.service';
import { MenuInput } from '../../models/menuInput.model';
import { AddToCart } from '../../models/addToCart.model';
import { DataView } from 'primeng/dataview';
import { GetShopInforInputDto } from '../../models/GetShopInforInputDto.model';


@Component({
  selector: 'app-shopdetail',
  templateUrl: './shopdetail.component.html',
  styleUrls: ['./shopdetail.component.scss']
})
export class ShopdetailComponent extends iComponentBase implements OnInit {
  foods: any[];
  sortOptions: SelectItem[];
  sortOrder: number;
  loading: boolean;
  sortField: string;
	menuInput = new MenuInput();
  shopid : string;
  shopInfor : any
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
    this.route.queryParams.subscribe(params => {
      this.shopid = params['shopid'];
      // console.log(id);
      // Sử dụng giá trị id tại đây
	  this.menuInput.shopId = this.shopid
	  console.log(this.menuInput)
		this.getMenu(this.menuInput)
    this.getShopInfor()
    });

    this.sortOptions = [
      { label: 'Price High to Low', value: '!unitPrice' },
      { label: 'Price Low to High', value: 'unitPrice' }
  ];

  }

  onSortChange(event: any) {
    const value = event.value;

    if (value.indexOf('!') === 0) {
        this.sortOrder = -1;
        this.sortField = value.substring(1, value.length);
    } else {
        this.sortOrder = 1;
        this.sortField = value;
    }
}

  async getMenu(menuInput : MenuInput){
    try {
        this.loading = true;

        let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.SHOP_DETAIL, API.API_SHOP_DETAIL.DISPLAY_MENU, menuInput);
        if (response && response.success == true) {
            this.foods = response.listFood;            
        }

		console.log(this.foods);
        this.loading = false;
    } catch (e) {
        console.log(e);
        this.loading = false;
    }
  }

  async getShopInfor(){
    try {
        this.loading = true;
        let shopInforInput = new GetShopInforInputDto();
        shopInforInput.ShopId = this.shopid
        let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.SHOP_DETAIL, API.API_SHOP_DETAIL.DISPLAY_INFOR, shopInforInput);
        if (response && response.success === true) {
            this.shopInfor = response;            
        }

		console.log(this.foods);
        this.loading = false;
    } catch (e) {
        console.log(e);
        this.loading = false;
    }
  }

  onFilter(dv: DataView, event: Event) {
    dv.filter((event.target as HTMLInputElement).value);
}

  async onAddToCart(foodId : number){
    try {
      this.loading = true;
      let cartItem = new AddToCart();
      cartItem.foodId = foodId
      cartItem.amount = 1
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.CART, API.API_CART.ADDTOCART, cartItem);
      if (response && response.message === "Success") {
        console.log(response)
          this.showMessage(mType.success, "", "Add to cart success!", 'notify');      
      }
      else{
        this.showMessage(mType.warn, "", "You are not logged as customer!", 'notify');
        this.router.navigate(['/login']);
      } 

      this.loading = false;
  } catch (e) {
      console.log(e);
      this.loading = false;
  }
  }
}
