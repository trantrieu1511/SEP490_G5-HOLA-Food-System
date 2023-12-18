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
import { Voucher, VoucherDisplayHideInputDto, VoucherInput} from '../../models/voucher.model';
import { AuthService } from 'src/app/services/auth.service';
import { TranslateService } from '@ngx-translate/core';

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
  check2: boolean = true;
  header1:string;
  header2:string;

  constructor(
    public breadcrumbService: AppBreadcrumbService,
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private signalRService: DataRealTimeService,
    private authService: AuthService,
    public translate: TranslateService

  ) {
    super(messageService, breadcrumbService);

    this.breadcrumbService.setItems([
      {label: 'HFSBusiness'},
      {label: 'Voucher Management', routerLink: ['/HFSBusiness/seller/voucher-management']}
    ]);

    this.translate.get('voucherScreen').subscribe( (text: any) => {
      this.header1 = text.AddNewVoucher;  
      this.header2 = text.EditVoucher;
    });
  }
  ngOnInit(): void {
    this.userId = this.authService.getUserInfor().userId;

    this.GetAllVoucher();
    
  }
  

  async GetAllVoucher(){
    this.lstVoucher = [];

    try {
      this.loading = true;
      //this.userId = sessionStorage.getItem('userId');  

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

  onHideVoucher(voucher: Voucher, event) {
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure to Hide this Voucher : ${voucher.code} ?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        //confirm action
        this.EDVoucher(voucher, false);
      },
      reject: () => {
        //reject action
      },
    });
  }

  onDisplayVoucher(voucher: Voucher, event) {
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure to Display this post id: ${voucher.code} ?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        //confirm action
        this.EDVoucher(voucher, true);
      },
      reject: () => {
        //reject action
      },
    });
  }

  async EDVoucher(voucherEnity: Voucher, type: boolean) {
    // type = true => Display
    // false => Hide

    const message = type ? 'Displayed' : 'Hidden';

    try {
      let param: VoucherDisplayHideInputDto = new VoucherDisplayHideInputDto();
      param.voucherId = voucherEnity.voucherId;
      param.type = type;

      let response = await this.iServiceBase.postDataAsync(
        API.PHAN_HE.VOUCHER,
        API.API_VOUCHER.ENABLE_DISABLE_VOUCHER,
        param,
        true
      );

      if (response && response.message === 'Success') {
        this.showMessage(
          mType.success,
          'Notification',
          `${message} Voucher: ${voucherEnity.code} successfully`,
          'notify'
        );

        //lấy lại danh sách All
        this.GetAllVoucher();
      } else {
        var messageError = this.iServiceBase.formatMessageError(response);
        console.log(messageError);
        this.showMessage(mType.error, response.message, messageError, 'notify');
      }
    } catch (e) {
      console.log(e);
    }
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
    this.headerDialog = this.header1;

    this.voucherModel = new Voucher();

    this.displayDialogEditAddVoucher = true;
  }

  onUpdateVoucher(voucher: Voucher) {
    this.headerDialog = this.header2 + voucher.code;

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
        this.showMessage(mType.error, "Notification", response.message, 'notify');
      }
    } catch (e) {
      console.log(e);
        this.showMessage(mType.error, "Notification", "Update failure", 'notify');
    }
  }
  async createVoucher(voucherEntity : VoucherInput){
    try {
      
      const param = new FormData();  
      //this.userId = sessionStorage.getItem('userId');  
      param.append('sellerId',this.userId);    
      param.append('discountAmount',voucherEntity.discountAmount.toString());
      param.append('minimumOrderValue',voucherEntity.minimumOrderValue.toString());
      param.append('effectiveDate',voucherEntity.effectiveDate.toString());
      param.append('expireDate',voucherEntity.expireDate.toString());

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.VOUCHER, API.API_VOUCHER.CREATE_VOUCHER,param,true);
      if (response && response.message === "Success") {
        this.GetAllVoucher();
        this.showMessage(mType.success, "Notification", "Create successfully", 'notify');
        this.displayDialogEditAddVoucher = false;
      }else{
        var messageError = this.iServiceBase.formatMessageError(response);
        console.log(messageError);
        this.showMessage(mType.error, response.message, messageError, 'notify');
        //his.showMessage(mType.error, "Notification", response.message, 'notify');
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