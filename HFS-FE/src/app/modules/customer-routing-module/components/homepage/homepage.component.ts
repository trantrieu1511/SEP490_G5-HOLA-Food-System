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
import { User } from 'src/app/services/auth.service';
import { PresenceService } from 'src/app/services/presence.service';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.scss']
})
export class HomepageComponent extends iComponentBase implements OnInit {
  @ViewChild('dt') table: Table;

  loading: boolean;
  lstShop: any[];

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
    this.getAllShop();
    this.setCurrentUser();
  }
  setCurrentUser() {
    const user: User = JSON.parse(localStorage.getItem('user'));
    const token = sessionStorage.getItem('JWT');
    //debugger; dcm debugger an l a`
    if (user) {

      this.presence.createHubConnection(token);
    }
  }
  async getAllShop() {
    try {
      this.loading = true;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.HOME, API.API_HOME.DISPLAY_SHOP, null);
      console.log(response)
      if (response && response.message === "Success") {
        this.lstShop = response.listShop;
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
}
