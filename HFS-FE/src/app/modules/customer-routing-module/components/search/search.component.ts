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
  type : string
  searchOptions : any = "0"
  constructor(private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private _router: Router,
    private _route: ActivatedRoute,
    private dataService: DataService,
    public presence: PresenceService
  ) {
    super(messageService);
    
  }

  ngOnInit() {
    this._route.queryParams.subscribe(params => {
      this.searchInput.searchKey = params['key'];
      this.searchText = params['key'];
      this.type = params['type'];
      this.searchOptions = params['type'];
      this.searchInput.type = params['type']
    })
    this.onGetSearchData()
    

    if (this.type === "0"){
      this.sortOptions = [
        { label: 'Odered High to Low', value: '!numberOrdered' },
        { label: 'Odered Low to High', value: 'numberOrdered' },
        { label: 'Star Low to High', value: '!averageStar' },
        { label: 'Star Low to High', value: 'averageStar' }]
    }
    else this.sortOptions = [
      { label: 'Odered High to Low', value: '!numberOrdered' },
      { label: 'Odered Low to High', value: 'numberOrdered' },
      { label: 'Star Low to High', value: '!star' },
      { label: 'Star Low to High', value: 'star' }]
    
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
          if (this.type === "0"){
            this.searchData = response.listFood;
          }
          else this.searchData = response.listShop;
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

  onFoodDetail(foodId : number){
    this._router.navigate(['/fooddetail'], { queryParams: { foodId: foodId } });
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

  onSearch(){
    if (this.searchText.length > 0){
      this._router.routeReuseStrategy.shouldReuseRoute = function () {
        return false;
      }
      this._router.onSameUrlNavigation = 'reload';
      this._router.navigate(['/search'], { queryParams: { key: this.searchText, type : this.searchOptions } });
      this.type = this.searchOptions
      this.onGetSearchData();
    }
  }
}
