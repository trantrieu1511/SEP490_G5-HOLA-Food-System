import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData,
  iFunction
} from 'src/app/modules/shared-module/shared-module';
import { BanSeller, HistoryBanSeller, RejectSeller, Seller } from '../models/Seller';
import * as API from "../../../services/apiURL";
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { TranslateService } from '@ngx-translate/core';
@Component({
  selector: 'app-manage-seller-module',
  templateUrl: './manage-seller-module.component.html',
  styleUrls: ['./manage-seller-module.component.scss']
})
export class ManageSellerModuleComponent extends iComponentBase implements OnInit {
   listSeller:Seller []=[];
   visibleContentDialog:boolean=false;
  displayDialogBan: boolean = false;
  displayDialogReject: boolean = false;
  visiblebanHistoryDialog:boolean=false;
  visibleImageDialog:boolean=false;
  visibleImageDialog2:boolean=false;
  sellerImg:Seller=new Seller();
  sellerDetail:Seller=new Seller();
  headerDialog: string = '';
  listhistoryBan:HistoryBanSeller []=[];
  banseller:BanSeller= new BanSeller();
  activeseller:RejectSeller= new RejectSeller();

  isSaveButtonDisabled: boolean = true;
  constructor( private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private _router: Router,
    public breadcrumbService: AppBreadcrumbService,
    public translate: TranslateService
  ){
    super(messageService, breadcrumbService);

    this.breadcrumbService.setItems([
      {label: 'HFSBusiness'},
      {label: 'Seller Management', routerLink: ['/HFSBusiness/admin/seller-management']}
    ]);

  }
  ngOnInit(): void {
this.getAllSeller();
  }

  checkSaveButtonStatus() {
    // Example condition: enable the button if both note and status are filled
    this.isSaveButtonDisabled = !this.activeseller.note;
}
  async getAllSeller() {
    this.listSeller = [];

    try {
        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.LIST_SELLER,"");

        if (response && response.message === "Success") {
            this.listSeller = response.sellers;

        }
       ;
    } catch (e) {
        console.log(e);

    }
}
  async onHistoryBan(sellerId:string){
    this.banseller = new BanSeller();
    this.visiblebanHistoryDialog = true;
    this.listhistoryBan = [];
  const param={
    "sellerId":sellerId
  }
      try {


          let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.HIS_SELLER,param);

          if (response && response.message === "Success") {
              this.listhistoryBan = response.data;

          }
         ;
      } catch (e) {
          console.log(e);

      }
  }
  BanSeller(user:Seller){
    this.headerDialog="Ban Id: "+user.sellerId;
    this.displayDialogBan = true;
    this.banseller.sellerId=user.sellerId;
    this.banseller.isBanned=!user.isBanned;
console.log(this.banseller);
  }
  UnBanSeller(user:Seller){
    this.headerDialog="Unban Id: "+user.sellerId;
    this.displayDialogBan = true;
    this.banseller.sellerId=user.sellerId;
    this.banseller.isBanned=!user.isBanned;
console.log(this.banseller);
  }
  async onSaveBan(){
    console.log(this.banseller);
    try{

    let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.BAN_SELLER,this.banseller);

    if (response && response.message === "Success") {
     this.getAllSeller();
     this.showMessage(mType.success, "Notification", "Ban "+this.banseller.sellerId+" successfully", 'notify');
     this.banseller = new BanSeller();
     this.displayDialogBan = false;
    }else{
      this.showMessage(mType.error,"Notification", response.message, 'notify');
    }
   ;
  } catch (e) {
    console.log(e);
    this.showMessage(mType.error, "Notification", "Ban "+this.banseller.sellerId+" failure", 'notify');
  }
  }
  onCancelBan(){
    this.banseller = new BanSeller();
    this.displayDialogBan = false;
  }
  async ActiveSeller(user:Seller,event){
    // this.confirmationService.confirm({
    //   target: event.target,
    //   message: `Are you sure to Verify  id: ${user.sellerId} ?`,
    //   icon: 'pi pi-exclamation-triangle',
    //   accept: () => {
    //     //confirm action
    //     this.ActiveSeller1(user);
    //   },
    //   reject: () => {
    //     //reject action
    //   },
  //  });
  this.activeseller.note="";
  this.headerDialog="Verify Id: "+user.sellerId;
  this.displayDialogReject = true;
  this.activeseller.status=1;
  this.activeseller.sellerId=user.sellerId;

  }
  async ActiveSeller1(){
    // const param={
    //   "sellerId": user.sellerId,
    //   "status": 1
    // }
    try{
      
    let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.ACTIVE_SELLER,this.activeseller);

    if (response && response.message === "Success") {

     this.getAllSeller();
     this.showMessage(mType.success, "Notification", "Verification "+this.activeseller.sellerId+" successfully", 'notify');
     this.activeseller = new RejectSeller();
     this.displayDialogReject = false;
    }
   ;
  } catch (e) {
    console.log(e);
    this.showMessage(mType.error, "Notification", "Verificationn "+this.activeseller.sellerId+" failure", 'notify');
  }


  }
  RejectSeller(user:Seller){
    this.headerDialog="Reject Id: "+user.sellerId;
    this.activeseller.note="";
    this.displayDialogReject = true;
    this.activeseller.status=2;
    this.activeseller.sellerId=user.sellerId;
  }
  onCancelReject(){
    this.activeseller = new RejectSeller();
    this.displayDialogReject = false;
  }


  async onSave(){
     if(this.activeseller.status===2){
      this.onSaveReject();
     }
     if(this.activeseller.status===1){
      this.ActiveSeller1();
     }
  }

  async onSaveReject(){

    try{
      
    let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.REJECT_SELLER,this.activeseller);

    if (response && response.message === "Success") {

     this.getAllSeller();
     this.showMessage(mType.success, "Notification", "Reject "+this.activeseller.sellerId+" successfully", 'notify');
     this.activeseller = new RejectSeller();
     this.displayDialogReject = false;
    }
   ;
  } catch (e) {
    console.log(e);
    this.showMessage(mType.error, "Notification", "Reject "+this.activeseller.sellerId+" failure", 'notify');
  }


  }
  onDisplayImagesDialog(s: Seller, event: any) {
    this.sellerImg = s;
    this.visibleImageDialog = true;
  }
  onDisplayImagesDialog2(s: Seller, event: any) {
    this.sellerImg = s;
    this.visibleImageDialog2 = true;
  }
  Detail(s:Seller,event){
    this.visibleContentDialog=true;
    this.sellerDetail=s;

    //console.log(this.sellerDetail.imagesBase64)
  }
}
