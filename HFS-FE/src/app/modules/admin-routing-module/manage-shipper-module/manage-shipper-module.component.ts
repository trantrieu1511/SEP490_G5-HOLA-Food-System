import { Component, OnInit } from '@angular/core';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData,
  iFunction
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../services/apiURL";
import { ActiveShipper, BanShipper, HistoryBanShipper, Shipper } from '../models/Shipper';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { TranslateService } from '@ngx-translate/core';
@Component({
  selector: 'app-manage-shipper-module',
  templateUrl: './manage-shipper-module.component.html',
  styleUrls: ['./manage-shipper-module.component.scss']
})
export class ManageShipperModuleComponent extends iComponentBase implements OnInit {
  listShipper:Shipper []=[];
  isSaveButtonDisabled:boolean= true;
  displayDialogNote: boolean = false;
  visibleContentDialog:boolean=false;
  visibleImageDialog:boolean=false;
  visibleImageDialogId:boolean=false;
  visiblebanHistoryDialog:boolean=false;
  shipperImg:Shipper =new Shipper();
  shipperDetail:Shipper =new Shipper();;
  headerDialog: string = '';
  listhistoryBan:HistoryBanShipper []=[];
  banshipper:BanShipper= new BanShipper();
  activeshipper:ActiveShipper =new ActiveShipper();
  constructor( private shareData: ShareData,
    public messageService: MessageService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private _router: Router,
    public breadcrumbService: AppBreadcrumbService,
    public translate: TranslateService
  ){
    super(messageService, breadcrumbService);

    this.breadcrumbService.setItems([
      {label: 'HFSBusiness'},
      {label: 'Shipper Management', routerLink: ['/HFSBusiness/admin/shipper-management']}
    ]);

  }
  checkSaveButtonStatus() {
    // Example condition: enable the button if both note and status are filled

    this.isSaveButtonDisabled = !this.activeshipper.note;
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
    this.displayDialogNote = true;
    this.banshipper.shipperId=user.shipperId;
    this.banshipper.isBanned=!user.isBanned;
console.log(this.banshipper);
  }

  async ActiveShipper(user:Shipper,so:number){
    this.headerDialog="Verify ID: "+user.shipperId ;
    this.displayDialogNote = true;
 this.activeshipper.note=null;
    this.activeshipper.shipperId=user.shipperId;
    this.activeshipper.status=so;
  }
  async RejectShipper(user:Shipper,so:number){
    this.headerDialog="Reject ID: "+user.shipperId ;
    this.activeshipper.note=null;
    this.displayDialogNote = true;
    this.activeshipper.status=so;
  }
  async ActiveShipperCancel(){
    this.displayDialogNote = false;
    this.activeshipper=new ActiveShipper();
  }
  async ActiveShipper1(){
    // const param={
    //   "shipperId": user.shipperId,
    //   "status": user.status
    // }
    try{
    let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.ACTVIVE_SHIPPER, this.activeshipper);

    if (response && response.message === "Success") {
     this.getAllShipper();
     this.showMessage(mType.success, "Notification", "Update "+this.activeshipper.shipperId+" successfully", 'notify');
     this.banshipper = new BanShipper();
     this.displayDialogNote = false;
    }
   ;
  } catch (e) {
    console.log(e);
    this.showMessage(mType.error, "Notification", "Update "+this.activeshipper.shipperId+" failure", 'notify');
    }
   }
   async onSaveBan(){

    try{
    let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.BAN_SHIPPER,this.banshipper);

    if (response && response.message === "Success") {
     this.getAllShipper();
     this.showMessage(mType.success, "Notification", "Ban "+this.banshipper.shipperId+" successfully", 'notify');
     this.banshipper = new BanShipper();
     this.displayDialogNote = false;
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
    this.displayDialogNote = false;
  }
  onDisplayImagesDialog(s: Shipper, event: any) {
    this.headerDialog="Image: "+s.shipperId;
    this.shipperImg = s;
    this.visibleImageDialog = true;
  }
  onDisplayImagesDialogId(s: Shipper, event: any) {
    this.headerDialog="Image Id Card: "+s.shipperId;
    this.shipperImg = s;
    this.visibleImageDialogId = true;
  }
  Detail(user:Shipper){
this.visibleContentDialog=true;
this.shipperDetail=user;
  }

}
