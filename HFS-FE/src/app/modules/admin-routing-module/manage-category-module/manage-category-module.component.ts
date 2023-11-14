// import { Component, OnInit } from '@angular/core';
// import { iComponentBase } from '../../shared-module/shared-module';
// import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
// import { ConfirmationService, MessageService } from 'primeng/api';
// import { DataRealTimeService } from 'src/app/services/SignalR/data-real-time.service';

// import {
//   iServiceBase,
//   mType,
//   ShareData,
//   iFunction,
// } from 'src/app/modules/shared-module/shared-module';
// import * as API from "../../../services/apiURL";
// import { Category } from '../../seller-routing-module/models/food.model';

// @Component({
//   selector: 'app-manage-category-module',
//   templateUrl: './manage-category-module.component.html',
//   styleUrls: ['./manage-category-module.component.scss']
// })
// export class ManageCategoryModuleComponent extends iComponentBase implements OnInit{

//   lstCategory: Category[] = [];
//   loading: boolean;
//   userId : string;
//   headerDialog: string = '';
//   cateModel: Category = new Category();
//   check :boolean = false;
//   displayDialogEditAddVoucher: boolean = false;

//   constructor(
//     public breadcrumbService: AppBreadcrumbService,
//     private shareData: ShareData,
//     public messageService: MessageService,
//     private confirmationService: ConfirmationService,
//     private iServiceBase: iServiceBase,
//     private iFunction: iFunction,
//     private signalRService: DataRealTimeService
//   ) {
//     super(messageService, breadcrumbService);
//   }
//   ngOnInit(): void {
//     this.GetAllVoucher();
//   }
  

//   async GetAllVoucher(){
//     this.lstCategory = [];

//     try {
//       this.loading = true;



//       let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_MANAGE.LIST_CATEGORY,null);

//       if (response && response.message === "Success") {
//         this.lstCategory = response.listCategory;
//         console.log(this.lstCategory);
//       }
//       this.loading = false;
//     } catch (e) {
//       console.log(e);
//       this.loading = false;
//     }
//   }
//   onHideDialogEditAdd() {
    
//   }

//   onHideVoucher(cate: Category, event) {
//     this.confirmationService.confirm({
//       target: event.target,
//       message: `Are you sure to Hide this Voucher : ${cate.code} ?`,
//       icon: 'pi pi-exclamation-triangle',
//       accept: () => {
//         //confirm action
//         this.EDVoucher(cate, false);
//       },
//       reject: () => {
//         //reject action
//       },
//     });
//   }

//   onDisplayVoucher(cate: Category, event) {
//     this.confirmationService.confirm({
//       target: event.target,
//       message: `Are you sure to Display this post id: ${cate.code} ?`,
//       icon: 'pi pi-exclamation-triangle',
//       accept: () => {
//         //confirm action
//         this.EDVoucher(cate, true);
//       },
//       reject: () => {
//         //reject action
//       },
//     });
//   }

  

//   bindingDataVoucherModel(): CateInput {
//     let vouInput = new VoucherInput();

//     if (this.voucherModel.voucherId && this.voucherModel.voucherId > 0) {
//       //Update
//       vouInput.voucherId = this.voucherModel.voucherId;
//       vouInput.discountAmount = this.voucherModel.discountAmount;
//       vouInput.effectiveDate = this.voucherModel.effectiveDate;
//       vouInput.expireDate = this.voucherModel.expireDate;
//       vouInput.minimumOrderValue = this.voucherModel.minimumOrderValue;
//       vouInput.status = this.voucherModel.status;
//     } else {
//       //Insert
//       vouInput.discountAmount = this.voucherModel.discountAmount;
//       vouInput.effectiveDate = this.voucherModel.effectiveDate;
//       vouInput.expireDate = this.voucherModel.expireDate;
//       vouInput.minimumOrderValue = this.voucherModel.minimumOrderValue;
//       vouInput.status = this.voucherModel.status;
//     }

//     return vouInput;
//   }

//   onCreateVoucher(){
//     this.headerDialog = 'Add New Voucher';

//     this.voucherModel = new Voucher();

//     this.displayDialogEditAddVoucher = true;
//   }

//   onUpdateVoucher(voucher: Voucher) {
//     this.headerDialog = `Edit Voucher: ${voucher.code}`;

//     this.displayDialogEditAddVoucher=true;

//     this.voucherModel = Object.assign({}, voucher);

//   }

//   onSaveVoucher() {
//     //console.log(this.uploadedFiles);
//     let voucherEntity = this.bindingDataVoucherModel();

//       if (voucherEntity && voucherEntity.voucherId && voucherEntity.voucherId > 0) {
//         this.updateVoucher(voucherEntity);
//       } else {
//         this.createVoucher(voucherEntity);
//       }
    
//   }

//   async updateVoucher(voucherEntity : VoucherInput){
//     try {
//       voucherEntity.sellerId = this.userId;

//       let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.VOUCHER, API.API_VOUCHER.EDIT_VOUCHER,voucherEntity);
//       if (response && response.message === "Success") {
//         this.GetAllVoucher();
//         this.showMessage(mType.success, "Notification", "Update successfully", 'notify');
//         this.displayDialogEditAddVoucher = false;
//       }else{
//         this.showMessage(mType.success, "Notification", "Update failure", 'notify');
//       }
//     } catch (e) {
//       console.log(e);
//         this.showMessage(mType.error, "Notification", "Update failure", 'notify');
//     }
//   }
//   async createVoucher(voucherEntity : VoucherInput){
//     try {
      
//       const param = new FormData();  
//       this.userId = sessionStorage.getItem('userId');  
//       param.append('sellerId',this.userId);    
//       param.append('discountAmount',voucherEntity.discountAmount.toString());
//       param.append('minimumOrderValue',voucherEntity.minimumOrderValue.toString());
//       param.append('effectiveDate',voucherEntity.effectiveDate.toString());
//       param.append('expireDate',voucherEntity.expireDate.toString());

//       let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.VOUCHER, API.API_VOUCHER.CREATE_VOUCHER,param,true);
//       if (response && response.message === "Success") {
//         this.GetAllVoucher();
//         this.showMessage(mType.success, "Notification", "Create successfully", 'notify');
//         this.displayDialogEditAddVoucher = false;
//       }else{
//         this.showMessage(mType.success, "Notification", "Create failure", 'notify');
//       }
//     } catch (e) {
//       console.log(e);
//         this.showMessage(mType.error, "Notification", "Create failure", 'notify');
//     }
//   }
  
  
//   onCancelVoucher(){
//     this.displayDialogEditAddVoucher = false;
//   }
// }
