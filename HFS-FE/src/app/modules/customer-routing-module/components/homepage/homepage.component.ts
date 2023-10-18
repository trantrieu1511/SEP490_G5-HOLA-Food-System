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
import { Table } from 'primeng/table';
import { Shop } from '../../models/shop.model';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from 'src/app/services/data.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent  extends iComponentBase implements OnInit {
  @ViewChild('dt') table: Table;

  loading: boolean;
  lstShop: Shop[];

  constructor(private shareData: ShareData,
              public messageService: MessageService,
              private confirmationService: ConfirmationService,
              private iServiceBase: iServiceBase,
              private _router: Router,
              private _route: ActivatedRoute,
              private dataService: DataService
              ) {
      super(messageService);
  }

  ngOnInit() {
    this.getAllShop();
    
  }

  async getAllShop(){
    try {
        this.loading = true;

        let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.HOME, API.API_HOME.DISPLAY_SHOP, null);
        if (response && response.message === "Success") {
            this.lstShop = response.listShop;
            
        }
        this.loading = false;
    } catch (e) {
        console.log(e);
        this.loading = false;
    }
  }

  onShopDetail(shop: Shop){
    //console.log(shop);
    this.dataService.setData(shop);
    // this._router.navigate(['/shopdetail']);
    this._router.navigate(['/shopdetail'], { queryParams: {shopid : shop.userId} });
    //this._router.navigate(['/shopdetail'], { queryParams: { shopInfor: shop} });
    //this._router.navigate(['/shopdetail/'+ shop ]);
  }
}
