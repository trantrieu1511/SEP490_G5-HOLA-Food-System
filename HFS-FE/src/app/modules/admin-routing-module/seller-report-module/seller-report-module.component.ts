import { Component, OnInit } from '@angular/core';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData,
  iFunction
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../services/apiURL";
import {  BanCustomer, HistoryBanCustomer } from '../models/BanCustomer';
import { Customer } from '../models/Customer';
import { MessageService } from 'primeng/api';
import { Router } from '@angular/router';
import { User } from 'src/app/services/auth.service';
import { ReportSellerInput, reportSeller } from '../models/reportSeller';
@Component({
  selector: 'app-seller-report-module',
  templateUrl: './seller-report-module.component.html',
  styleUrls: ['./seller-report-module.component.scss']
})
export class SellerReportModuleComponent extends iComponentBase implements OnInit {
  lstReport: reportSeller[] = [];
  listhistoryBan: HistoryBanCustomer[] = [];
  user:User;
  bancus:BanCustomer=new BanCustomer();
  cusImg:Customer=new Customer();
  status2 :any[];
  replySellerDialog:string="";
  displayDialogBan: boolean = false;
  visiblebanHistoryDialog:boolean=false;
  visibleImageDialog:boolean=false;
  headerDialog: string = '';
  inputreply:ReportSellerInput =new ReportSellerInput();
  detailreport:reportSeller =new reportSeller();
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
    this.getAllReport();
    this.status2 = [
      {name: 'Solved', id: 1},
      {name: 'Reject', id: 2}

    ];
  }

  async getAllReport() {
    this.lstReport = [];

    try {


        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.LIST_REPORT,"");

        if (response && response.message === "Success") {
            this.lstReport = response.data;

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

async ReplySellerReport1(user:reportSeller){
  this.displayDialogBan = true;
  this.replySellerDialog="Reply ID: " + user.sellerReportId
// this.bancus.customerId=user.customerId;
// this.bancus.isBanned=!user.isBanned;
this.inputreply.sellerReportId=user.sellerReportId;
}
async onSave(){
  console.log(this.inputreply);
  try{

  let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.REPLY_REPORT,this.inputreply);

  if (response && response.message === "Success") {
   this.getAllReport();
   this.showMessage(mType.success, "Notification", "Reply "+this.inputreply.sellerReportId+" successfully", 'notify');
   this.bancus = new BanCustomer();
   this.displayDialogBan = false;
  }
 ;
} catch (e) {
  console.log(e);
  this.showMessage(mType.error, "Notification",  "Reply "+this.inputreply.sellerReportId+" successfully", 'notify');
}
}
onCancel(){
  this.bancus = new BanCustomer();
  this.displayDialogBan = false;
}
  async Detail(user:reportSeller,event: any){
    this.visiblebanHistoryDialog = true;
    this.detailreport=user;
    console.log(this.detailreport);
}
onDisplayImagesDialog(cus: Customer, event: any) {
  this.cusImg = cus;
  this.visibleImageDialog = true;
}
}
