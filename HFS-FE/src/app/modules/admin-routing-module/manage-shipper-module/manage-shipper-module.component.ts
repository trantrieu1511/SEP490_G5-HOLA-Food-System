import { Component, OnInit } from '@angular/core';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData,
  iFunction
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../services/apiURL";
import { BanShipper, HistoryBanShipper, Shipper } from '../models/Shipper';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
@Component({
  selector: 'app-manage-shipper-module',
  templateUrl: './manage-shipper-module.component.html',
  styleUrls: ['./manage-shipper-module.component.scss']
})
export class ManageShipperModuleComponent extends iComponentBase implements OnInit {
  listShipper:Shipper []=[];
  displayDialogBan: boolean = false;
  visibleImageDialog:boolean=false;
  visiblebanHistoryDialog:boolean=false;
  shipperImg:Shipper =new Shipper();
  headerDialog: string = '';
  listhistoryBan:HistoryBanShipper []=[];
  banshipper:BanShipper= new BanShipper();
  constructor( private shareData: ShareData,
    public messageService: MessageService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private _router: Router,
    public breadcrumbService: AppBreadcrumbService,
  ){
    super(messageService, breadcrumbService);

    this.breadcrumbService.setItems([
      {label: 'HFSBusiness'},
      {label: 'Shipper Management', routerLink: ['/HFSBusiness/admin/shipper-management']}
    ]);

  }
  ngOnInit(): void {
this.getAllShipper();
  }

  async getAllShipper() {
    this.listShipper = [];

    try {


        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.LIST_SHIPPER,"");

        if (response && response.message === "Success") {
            this.listShipper = response.shippers;

        }
       ;
    } catch (e) {
        console.log(e);

    }
}
  async onHistoryBan(Id:string){

    this.visiblebanHistoryDialog = true;
    this.listhistoryBan = [];
  const param={
    "shipperId":Id
  }
      try {


          let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.HIS_SHIPPER,param);

          if (response && response.message === "Success") {
              this.listhistoryBan = response.data;

          }
         ;
      } catch (e) {
          console.log(e);

      }
  }
  BanShipper(user:Shipper){
    this.displayDialogBan = true;
    this.banshipper.shipperId=user.shipperId;
    this.banshipper.isBanned=!user.isBanned;
console.log(this.banshipper);
  }
  async ActiveShipper(user:Shipper){
    const param={
      "shipperId": user.shipperId,
      "isVerified": !user.isVerified
    }
    try{
    let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.ACTVIVE_SHIPPER,param);

    if (response && response.message === "Success") {
     this.getAllShipper();
     this.showMessage(mType.success, "Notification", "Verification "+user.shipperId+" successfully", 'notify');
     this.banshipper = new BanShipper();
     this.displayDialogBan = false;
    }
   ;
  } catch (e) {
    console.log(e);
    this.showMessage(mType.error, "Notification", "Verificationn "+user.shipperId+" failure", 'notify');
    }
   }
   async onSaveBan(){

    try{
    let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.BAN_SHIPPER,this.banshipper);

    if (response && response.message === "Success") {
     this.getAllShipper();
     this.showMessage(mType.success, "Notification", "Ban "+this.banshipper.shipperId+" successfully", 'notify');
     this.banshipper = new BanShipper();
     this.displayDialogBan = false;
    }else{
      this.showMessage(mType.error, "Notification",response.message, 'notify');
    }
   ;
  } catch (e) {
    console.log(e);
    this.showMessage(mType.error, "Notification", "Ban "+this.banshipper.shipperId+" failure", 'notify');
  }
  }
  onCancelBan(){
    this.banshipper = new BanShipper();
    this.displayDialogBan = false;
  }
  onDisplayImagesDialog(s: Shipper, event: any) {
    this.shipperImg = s;
    this.visibleImageDialog = true;
  }
}
