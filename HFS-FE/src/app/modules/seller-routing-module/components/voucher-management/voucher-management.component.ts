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
import { Voucher, VoucherInput} from '../../models/voucher.model';
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
  voucherModel: Voucher = new Voucher();
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

  bindingDataVoucherModel(): VoucherInput {
    let vouInput = new VoucherInput();

    if (this.voucherModel.voucherId && this.voucherModel.voucherId > 0) {
      //Update
      vouInput.voucherId = this.voucherModel.voucherId;
      vouInput.discountAmount = this.voucherModel.discountAmount;
      vouInput.effectiveDate = this.voucherModel.effectiveDate;
      vouInput.expireDate = this.voucherModel.expireDate;
      vouInput.minimumOrderValue = this.voucherModel.minimumOrderValue;
      vouInput.status = this.voucherModel.status;
    } else {
      //Insert
      vouInput.discountAmount = this.voucherModel.discountAmount;
      vouInput.effectiveDate = this.voucherModel.effectiveDate;
      vouInput.expireDate = this.voucherModel.expireDate;
      vouInput.minimumOrderValue = this.voucherModel.minimumOrderValue;
      vouInput.status = this.voucherModel.status;
    }

    return vouInput;
  }

  onCreateVoucher(){
    this.headerDialog = 'Add New Voucher';

    this.voucherModel = new Voucher();

    this.displayDialogEditAddVoucher = true;
  }

  onUpdateVoucher(voucher: Voucher) {
    this.headerDialog = `Edit Voucher: ${voucher.code}`;

    this.displayDialogEditAddVoucher=true;

    this.voucherModel = Object.assign({}, voucher);

  }

  onSaveVoucher() {
    //console.log(this.uploadedFiles);
    let voucherEntity = this.bindingDataVoucherModel();

      if (voucherEntity && voucherEntity.voucherId && voucherEntity.voucherId > 0) {
        this.updateVoucher(voucherEntity);
      } else {
        this.createVoucher(voucherEntity);
      }
    
  }

  async updateVoucher(voucherEntity : VoucherInput){
    try {
      voucherEntity.sellerId = this.userId;

      let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.VOUCHER, API.API_VOUCHER.EDIT_VOUCHER,voucherEntity);
      if (response && response.message === "Success") {
        this.GetAllVoucher();
        this.showMessage(mType.success, "Notification", "Update successfully", 'notify');
        this.displayDialogEditAddVoucher = false;
      }else{
        this.showMessage(mType.success, "Notification", "Update failure", 'notify');
      }
    } catch (e) {
      console.log(e);
        this.showMessage(mType.error, "Notification", "Update failure", 'notify');
    }
  }
  async createVoucher(voucherEntity : VoucherInput){
    try {
      
      const param = new FormData();  
      this.userId = sessionStorage.getItem('userId');  
      param.append('sellerId',this.userId);    
      param.append('discountAmount',voucherEntity.discountAmount.toString());
      param.append('minimumOrderValue',voucherEntity.minimumOrderValue.toString());
      param.append('status',voucherEntity.status.toString());
      param.append('effectiveDate',voucherEntity.effectiveDate.toString());
      param.append('expireDate',voucherEntity.expireDate.toString());

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.VOUCHER, API.API_VOUCHER.CREATE_VOUCHER,param,true);
      if (response && response.message === "Success") {
        this.GetAllVoucher();
        this.showMessage(mType.success, "Notification", "Create successfully", 'notify');
        this.displayDialogEditAddVoucher = false;
      }else{
        this.showMessage(mType.success, "Notification", "Create failure", 'notify');
      }
    } catch (e) {
      console.log(e);
        this.showMessage(mType.error, "Notification", "Create failure", 'notify');
    }
  }
  
  
  onCancelVoucher(){
    this.displayDialogEditAddVoucher = false;
  }

}