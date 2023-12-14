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
import { Table } from 'primeng/table';
import { Shop } from '../../models/shop.model';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from 'src/app/services/data.service';
import { User, AuthService } from 'src/app/services/auth.service';
import { PresenceService } from 'src/app/services/presence.service';
import { DataView } from 'primeng/dataview';
import { AddToCart } from '../../models/addToCart.model';
import { TranslateService } from '@ngx-translate/core';
import { BaseInput } from '../../models/BaseInput.model';
interface PageEvent {
  first?: number;
  rows?: number;
  page?: number;
  pageCount?: number;
}
@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent extends iComponentBase implements OnInit {
  @ViewChild('dt') table: Table;

  first : number = 0;
  row : number = 9;
  loading: boolean;
  lstShop: any[];
  hotfoods : any[]
  sortOptions: SelectItem[];
  sortOrder: number;
  sortField: string;
  searchText : string;
  searchOptions : any = "0"
  lstCategory : any[];
  searchCategory : any[]
  paging : BaseInput = new BaseInput();
  total : number
  constructor(private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private _router: Router,
    private _route: ActivatedRoute,
    private dataService: DataService,
    public presence: PresenceService,
    public translate: TranslateService
  ) {
    super(messageService);
  }

  ngOnInit() {
    this.paging.pageSize = this.row;
    this.paging.pageNum = this.first;
    this.getAllShop();
    this.getHotFoods();
    this.getCategory();
   // this.setCurrentUser();
   this.translate.get('homePageScreen').subscribe( (text: any) => {

    this.sortOptions = [
      { label:  text.OderedHightoLow, value: '!numberOrdered'},
      { label:  text.OderedLowtoHigh, value: 'numberOrdered'},
      { label:  text.StarLowtoHigh, value: '!star'},
      { label:  text.StarHightoLow, value: 'star'} 
    ];

  });
  //  this.sortOptions = [
  //   { label: 'Odered High to Low', value: '!numberOrdered' },
  //   { label: 'Odered Low to High', value: 'numberOrdered' },
  //   { label: 'Star Low to High', value: '!star' },
  //   { label: 'Star High to Low', value: 'star' }]
  }
  // setCurrentUser() {
  //   const user: User = JSON.parse(localStorage.getItem('user'));
  //   const token = sessionStorage.getItem('JWT');
  //  // 
  //   if (user) {

  //     this.presence.createHubConnection(token);
  //   }
  // }

  async getCategory() {
    try {
      this.loading = true;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.HOME, API.API_HOME.GET_CATEGORY, null);
      console.log(response)
      if (response && response.message === "Success") {
        this.lstCategory = response.listCategory;
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  async getAllShop() {
    try {
      debugger
      this.loading = true;
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.HOME, API.API_HOME.DISPLAY_SHOP, this.paging);
      console.log(response)
      if (response && response.message === "Success") {
        this.lstShop = response.listShop;
        this.total = response.total
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  async getHotFoods() {
    try {
      this.loading = true;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.HOME, API.API_HOME.HOT_FOOD, null);
      console.log(response)
      if (response && response.message === "Success") {
        this.hotfoods = response.listFood;
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  onShopDetail(shop: Shop) {
    this._router.navigate(['/shopdetail'], { queryParams: { shopid: shop.userId } });
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

onPageChange(event: PageEvent) {
  debugger
  this.paging.pageNum = event.page;
  this.paging.pageSize = event.rows;
  this.getAllShop();
}

onFoodDetail(foodId : number){
  this._router.navigate(['/fooddetail'], { queryParams: { foodId: foodId } });
}

onSearch(){
  if (this.searchOptions === "1") {
    this.searchCategory = []
  }
    this._router.navigate(['/search'], { queryParams: { key: this.searchText, type : this.searchOptions,category : this.searchCategory } });
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
      this._router.navigate(['/login']);
    } 

    this.loading = false;
  } catch (e) {
      console.log(e);
      this.loading = false;
  }
}

    carouselResponsiveOptions: any[] = [
      {
        breakpoint: '1024px',
        numVisible: 3,
        numScroll: 3
      },
      {
        breakpoint: '768px',
        numVisible: 2,
        numScroll: 2
      },
      {
        breakpoint: '560px',
        numVisible: 1,
        numScroll: 1
      }
    ];

}
