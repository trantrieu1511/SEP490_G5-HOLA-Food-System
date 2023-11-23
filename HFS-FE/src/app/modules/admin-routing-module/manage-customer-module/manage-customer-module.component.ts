import { Component, OnInit } from '@angular/core';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData,
  iFunction
} from 'src/app/modules/shared-module/shared-module';
import { User } from 'src/app/services/auth.service';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import * as API from "../../../services/apiURL";
import { Customer } from '../models/Customer';
import { BanCustomer, HistoryBanCustomer } from '../models/BanCustomer';

@Component({
  selector: 'app-manage-customer-module',
  templateUrl: './manage-customer-module.component.html',
  styleUrls: ['./manage-customer-module.component.scss']
})
export class ManageCustomerModuleComponent extends iComponentBase implements OnInit {
  lstUser: Customer[] = [];
  listhistoryBan: HistoryBanCustomer[] = [];
  user:User;
  bancus:BanCustomer=new BanCustomer();
  cusImg:Customer=new Customer();
  displayDialogBan: boolean = false;
  visiblebanHistoryDialog:boolean=false;
  visibleImageDialog:boolean=false;
  headerDialog: string = '';
  constructor( private shareData: ShareData,
    public messageService: MessageService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private _router: Router,
  ){
    super(messageService);

  }

  ngOnInit(): void {
    //  const userData = localStorage.getItem('user');
    //    this.user = JSON.parse(userData);
    //    const token=sessionStorage.getItem('JWT');
    // this.presence.createHubConnection(token);
    this.getAllCustomer();

  }

  async getAllCustomer() {
    this.lstUser = [];

    try {


        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.LIST_CUS,"");

        if (response && response.message === "Success") {
            this.lstUser = response.customers;

        }
       ;
    } catch (e) {
        console.log(e);

    }
}
// async BanCustomer(user:Customer){
//
//   const param = {
//     "customerId":user.customerId,
//    "Ban":!user.isBanned
//   };
//   console.log(param)
// try {

//
//        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.BAN_CUS,param);

//        if (response && response.message === "Success") {
//         this.getAllCustomer();
//         this.showMessage(mType.success, "Notification", "Update "+user.customerId+" successfully", 'notify');
//        }
//       ;
//    } catch (e) {
//        console.log(e);
//        this.showMessage(mType.error, "Notification", "Update "+user.customerId+" failure", 'notify');
//    }
// }

async BanCustomer1(user:Customer){
  this.displayDialogBan = true;
this.bancus.customerId=user.customerId;
this.bancus.isBanned=!user.isBanned;
}
async onSaveBan(){
  console.log(this.bancus);
  try{

  let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.BAN_CUS,this.bancus);

  if (response && response.message === "Success") {
   this.getAllCustomer();
   this.showMessage(mType.success, "Notification", "Ban "+this.bancus.customerId+" successfully", 'notify');
   this.bancus = new BanCustomer();
   this.displayDialogBan = false;
  }
 ;
} catch (e) {
  console.log(e);
  this.showMessage(mType.error, "Notification", "Ban "+this.bancus.customerId+" failure", 'notify');
}
}
onCancelBan(){
  this.bancus = new BanCustomer();
  this.displayDialogBan = false;
}
  async onHistoryBan(customerId:string){
  this.bancus = new BanCustomer();
  this.visiblebanHistoryDialog = true;
  this.listhistoryBan = [];
const param={
  "customerId":customerId
}
    try {


        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.HIS_CUS,param);

        if (response && response.message === "Success") {
            this.listhistoryBan = response.data;

        }
       ;
    } catch (e) {
        console.log(e);

    }
}
onDisplayImagesDialog(cus: Customer, event: any) {
  this.cusImg = cus;
  this.visibleImageDialog = true;
}
}
