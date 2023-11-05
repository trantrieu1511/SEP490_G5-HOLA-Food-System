import { Component, OnInit } from '@angular/core';
import { ConfirmationService, MessageService } from 'primeng/api';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { iComponentBase } from 'src/app/modules/shared-module/shared-module';
import { DataRealTimeService } from 'src/app/services/SignalR/data-real-time.service';
import * as API from "../../../../services/apiURL";
import {
  iServiceBase,
  mType,
  ShareData,
  iFunction,
} from 'src/app/modules/shared-module/shared-module';
import { Voucher, VoucherCreate } from '../../models/voucher.model';
@Component({
  selector: 'app-voucher-management',
  templateUrl: './voucher-management.component.html',
  styleUrls: ['./voucher-management.component.scss']
})
export class VoucherManagementComponent extends iComponentBase implements OnInit {

  lstVoucher: Voucher[] = [];
  loading: boolean;
  userId : string;
  headerDialog: string = '';
  voucherModel: VoucherCreate = new VoucherCreate();
  voucherModel1: Voucher = new Voucher();
  check :boolean = false;
  displayDialogEditAddVoucher: boolean = false;

  constructor(
    public breadcrumbService: AppBreadcrumbService,
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private signalRService: DataRealTimeService
  ) {
    super(messageService, breadcrumbService);
  }
  ngOnInit(): void {
    this.GetAllVoucher();
  }
  
  async GetAllVoucher(){
    this.lstVoucher = [];

    try {
      this.loading = true;
      this.userId = sessionStorage.getItem('userId');  

        const param = {
          "sellerId":this.userId
      };
      this.loading = true;

      let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.VOUCHER, API.API_VOUCHER.GET_ALL_VOUCHER,param);

      if (response && response.message === "Success") {
        this.lstVoucher = response.listVoucher;
        console.log(this.lstVoucher);
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }
  onHideDialogEditAdd() {
    
  }
  onCreateVoucher(){
    this.headerDialog = 'Add New Voucher';

    this.voucherModel = new Voucher();
this.check=true;
    this.displayDialogEditAddVoucher = true;
  }
  async onSaveVoucher(){

if(this.check ==true){
  this.voucherModel.sellerId = this.userId;
  console.log(this.voucherModel)
  debugger;
  try{
  let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.VOUCHER, API.API_VOUCHER.CREATE_VOUCHER,this.voucherModel);

  if (response && response.message === "Success") {
    this.GetAllVoucher();
    this.showMessage(mType.success, "Notification", "Create successfully", 'notify');
    this.displayDialogEditAddVoucher = false;
  }else{
    this.showMessage(mType.success, "Notification", "Create failure", 'notify');
  }
  }catch (e) {
    console.log(e);
    this.showMessage(mType.error, "Notification", "Create failure", 'notify');
    }
}else{
  //xử lý update voucher
  try{

    let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.VOUCHER, API.API_VOUCHER.EDIT_VOUCHER,this.voucherModel1);
  
    if (response && response.message === "Success") {
      this.GetAllVoucher();
      this.showMessage(mType.success, "Notification", "Update successfully", 'notify');
      this.displayDialogEditAddVoucher = false;
    }else{
      this.showMessage(mType.success, "Notification", "Update failure", 'notify');
    }
    }catch (e) {
      console.log(e);
      this.showMessage(mType.error, "Notification", "Update failure", 'notify');
      }
}
    
  }
  onCancelVoucher(){
    this.displayDialogEditAddVoucher = false;
  }
 

  onUpdateVoucher(voucher: Voucher) {
    this.check=false;
    this.headerDialog = `Edit Voucher: ${voucher.code}`;
    this.displayDialogEditAddVoucher=true;
    this.voucherModel=voucher;
    console.log(this.voucherModel);

  }
}