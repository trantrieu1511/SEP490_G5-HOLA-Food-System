import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService, MessageService, SelectItem } from 'primeng/api';
import { ShareData, iComponentBase, iServiceBase, mType } from 'src/app/modules/shared-module/shared-module';
import { DataService } from 'src/app/services/data.service';
import { PresenceService } from 'src/app/services/presence.service';
import { SearchInput } from '../../models/SearchInput.model';
import * as API from "../../../../services/apiURL";
import { DataView } from 'primeng/dataview';
import { AddToCart } from '../../models/addToCart.model';
import { Shop } from '../../models/shop.model';
import { ShoppingCartPopupService } from 'src/app/services/shopping-cart-popup.service';
import { TranslateService } from '@ngx-translate/core';
import { AuthService } from 'src/app/services/auth.service';
interface PageEvent {
  first?: number;
  rows?: number;
  page?: number;
  pageCount?: number;
}
@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent extends iComponentBase implements OnInit {

  loading: boolean;
  searchInput: SearchInput = new SearchInput();
  searchData: any;
  sortOptions: SelectItem[];
  sortOrder: number;
  sortField: string;
  searchText: string;
  type: string
  searchOptions: any = "0"
  lstCategory: any[];
  searchCategory: number[] = []
  list : any
  total : number
  constructor(private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private _router: Router,
    private _route: ActivatedRoute,
    private dataService: DataService,
    private authService: AuthService,
    public presence: PresenceService,
    private shoppingCartService: ShoppingCartPopupService,
    public translate: TranslateService
  ) {
    super(messageService);
    
  }

  async ngOnInit() {
      this._route.queryParams.subscribe(params => {
      this.searchInput.searchKey = params['key'];
      this.searchText = params['key'];
      this.list = params['category']; // "1" [""]
      if (this.list instanceof Array){
        this.list.forEach(x => {
          this.searchCategory.push(parseInt(x));
        })
      }
      else{
        if (this.list){
          this.searchCategory.push(parseInt(this.list));
        }       
      }
      this.type = params['type'];
      this.searchOptions = params['type'];
      this.searchInput.type = params['type']
      this.searchInput.category = this.searchCategory
    })
    this.searchInput.pageNum = 0;
    this.searchInput.pageSize = 2;
    await this.getCategory()
    this.onGetSearchData()
    
    if (this.type === "0") {
      this.translate.get('searchCusScreen').subscribe( (text: any) => {
        this.sortOptions = [
          { label: text.OderedHightoLow, value: '!numberOrdered' },
          { label: text.OderedLowtoHigh, value: 'numberOrdered' },
          { label: text.StarHightoLow, value: '!averageStar' },
          { label: text.StarLowtoHigh, value: 'averageStar' }]
      });
      
    }
    else 
    this.translate.get('searchCusScreen').subscribe( (text: any) => {
      
    this.sortOptions = [
      { label:  text.OderedHightoLow, value: '!numberOrdered' },
      { label: text.OderedLowtoHigh, value: 'numberOrdered' },
      { label: text.StarHightoLow, value: '!star' },
      { label: text.StarLowtoHigh, value: 'star' }]
    });
      console.log(this.searchCategory);
  }

  onPageChange(event: PageEvent) {
    this.searchInput.pageNum = event.page;
    this.searchInput.pageSize = event.rows
    this.onGetSearchData();
  }

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

  onShopDetail(shop: Shop) {
    //console.log(shop);
    this.dataService.setData(shop);
    // this._router.navigate(['/shopdetail']);
    this._router.navigate(['/shopdetail'], { queryParams: { shopid: shop.userId } });
    //this._router.navigate(['/shopdetail'], { queryParams: { shopInfor: shop} });
    //this._router.navigate(['/shopdetail/'+ shop ]);
  }

  async onGetSearchData() {
    this.searchData = []
    try {
      this.loading = true;
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.HOME, API.API_HOME.SEARCH, this.searchInput);
      console.log(response)
      if (response && response.message === "Success") {
        if (this.type === "0") {
          this.searchData = response.listFood;
        }
        else this.searchData = response.listShop;
        this.total = response.total
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
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
  onFilter(dv: DataView, event: Event) {
    dv.filter((event.target as HTMLInputElement).value);
  }

  onFoodDetail(foodId: number) {
    this._router.navigate(['/fooddetail'], { queryParams: { foodId: foodId } });
  }

  async onAddToCart(foodId: number) {
    const user = this.authService.getUserInfor();
    if(user == null){
      this._router.navigate(['/login']);
      return
    }
    try {
      this.loading = true;
      let cartItem = new AddToCart();
      cartItem.foodId = foodId
      cartItem.amount = 1
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.CART, API.API_CART.ADDTOCART, cartItem);
      if (response && response.message === "Success") {
        console.log(response)
        this.shoppingCartService.onAddToCart()
        this.showMessage(mType.success, "", "Add to cart success!", 'notify');
      }
      else {
        this.showMessage(mType.warn, "", "You are not logged as customer!", 'notify');
        this._router.navigate(['/login']);
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  onSearch() {
    if (this.searchOptions === "1") {
      this.searchCategory = []
    }
    this._router.routeReuseStrategy.shouldReuseRoute = function () {
      return false;
    }
    this._router.onSameUrlNavigation = 'reload';
    this._router.navigate(['/search'], { queryParams: { key: this.searchText, type: this.searchOptions,category : this.searchCategory } });
    this.type = this.searchOptions
    this.onGetSearchData();
  }
}
