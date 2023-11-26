import { Component, OnInit } from '@angular/core';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData,
  iFunction
} from 'src/app/modules/shared-module/shared-module';
import { Router } from '@angular/router';
import * as API from "../../../../services/apiURL";
import {ConfirmationService, MessageService } from 'primeng/api';
import { PostModerator } from 'src/app/modules/admin-routing-module/models/PostModerator';
import { Customer } from 'src/app/modules/admin-routing-module/models/Customer';
import { AuthService, User } from 'src/app/services/auth.service';
import { Invition, Shipper } from '../../models/shipper.model';

@Component({
  selector: 'app-invitation-shipper',
  templateUrl: './invitation-shipper.component.html',
  styleUrls: ['./invitation-shipper.component.scss']
})
export class InvitationShipperComponent extends iComponentBase implements OnInit {
  lstShipper: Shipper[] = [];
  listinvitation:Invition[]=[];
  user:User;
  displayDialogAdd: boolean = false;
  headerDialog: string = '';
  addShipper:string;
  postM:PostModerator=new PostModerator();
  constructor( private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private _router: Router,
    private authSerivce: AuthService
  ){
    super(messageService);

  }

  ngOnInit(): void {
    this.user = this.authSerivce.getUserInfor();
    this.getAllShipper();
    debugger
   // this.user = this.authSerivce.getUserInfor();
  }


  async getAllShipper() {
    this.lstShipper = [];
    debugger
  const param = {
   // "manageBy": sessionStorage.getItem('userId'),
    "manageBy": this.user.userId
  }
  debugger
    try {


        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_USER.GET_SHIPPERS_BYSELLER,param);

        if (response && response.message === "Success") {
            this.lstShipper = response.shippers;

        }
       ;
    } catch (e) {
        console.log(e);

    }
}
 Delete(user:Shipper,event){
  this.confirmationService.confirm({
    target: event.target,
    message: `Are you sure to Kick  id: ${user.shipperId} ?`,
    icon: 'pi pi-exclamation-triangle',
    accept: () => {
      //confirm action
      this.DeleteS(user);
    },
    reject: () => {
      //reject action
    },
  });
}
  async DeleteS(user:Shipper){
    const param= {
      //"sellerId": sessionStorage.getItem('userId'),
      "sellerId": this.user.userId,
      "shipperId": user.shipperId
    }
    debugger
    try {


      let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_USER.KICK_SHIPPERS,param);

      if (response && response.message === "Success") {
       this.getAllShipper();
          this.showMessage(mType.success, "Notification", "Kick successfully", 'notify');
      }else{

       this.showMessage(mType.error, "Notification", response.message, 'notify');
      }

  } catch (e) {
      console.log(e);
      this.showMessage(mType.error, "Notification", "Invitation  failure", 'notify');

  }
  }
  onInvitation(){
    this.headerDialog = 'List Invitation';
    this.displayDialogAdd = true;
    debugger
    this.getInvitation();
  }
  async getInvitation() {
    debugger
    this.listinvitation = [];
    const param= {
      //"manageBy": sessionStorage.getItem('userId'),
      "manageBy": this.user.userId
    }
    try {

;
        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_USER.GET_SHIPPERS_INVITATION,param);

        if (response && response.message === "Success") {
            this.listinvitation = response.data;

        }
       ;
    } catch (e) {
        console.log(e);

    }
}
  async AddShipper(){
  console.log(this.addShipper)
  const param= {
    "email":this.addShipper,
    //"sellerId": sessionStorage.getItem('userId'),
    "sellerId": this.user.userId
  }
     try {

      ;

         let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_USER.ADD_SHIPPERS_INVITATION,param);

         if (response && response.message === "Success") {
          this.getInvitation();
             this.showMessage(mType.success, "Notification", "Invitation successfully", 'notify');
         }else{

          this.showMessage(mType.error, "Notification", response.message, 'notify');
         }

     } catch (e) {
         console.log(e);
         this.showMessage(mType.error, "Notification", "Invitation failure", 'notify');

     }
}
}
