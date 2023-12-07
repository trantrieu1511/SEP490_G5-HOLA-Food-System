import { Component, OnInit } from '@angular/core';
import * as API from "../../../services/apiURL";
import {
  iServiceBase,
  mType,
  ShareData,
  iFunction,
  iComponentBase,
} from 'src/app/modules/shared-module/shared-module';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DataRealTimeService } from 'src/app/services/SignalR/data-real-time.service';
import { CateDisplayHideInputDto, Category, CategoryInput } from '../models/Category';
import { C } from '@fullcalendar/core/internal-common';
import { TranslateService } from '@ngx-translate/core';
@Component({
  selector: 'app-manage-category-module',
  templateUrl: './manage-category-module.component.html',
  styleUrls: ['./manage-category-module.component.scss']
})
export class ManageCategoryModuleComponent extends iComponentBase implements OnInit  {
  lstCate: Category[] = [];
  loading: boolean;
  displayDialogEditAddCate: boolean = false;
  headerDialog: string = '';
  cateModel: Category = new Category();

  constructor(
    public breadcrumbService: AppBreadcrumbService,
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    public translate: TranslateService,
    private signalRService: DataRealTimeService
  ) {
    super(messageService, breadcrumbService);
  }
  ngOnInit(): void {
    this.getAllCate();
  }
  async getAllCate() {
    this.lstCate = [];

    try {
        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.LIST_CATEGORY,"");

        if (response && response.message === "Success") {
            this.lstCate = response.listCategory;

        }
       ;
    } catch (e) {
        console.log(e);

    }
}
  onCreateCate(){
    this.headerDialog = 'Add New Category';

    this.cateModel = new Category();

    this.displayDialogEditAddCate = true;
  }

  bindingDataCategoryModel(): CategoryInput{
    let cateInput = new CategoryInput();
    if(this.cateModel.categoryId && this.cateModel.categoryId > 0){
      //update
      cateInput.categoryId =  this.cateModel.categoryId;
      cateInput.name = this.cateModel.name;
      cateInput.status = this.cateModel.status;
    }
    else{
      //Insert
      cateInput.name = this.cateModel.name;
      cateInput.status = this.cateModel.status;
    }
    return cateInput;
  }

  onSaveCate(){
    let cataEntity = this.bindingDataCategoryModel();
    if (cataEntity && cataEntity.categoryId && cataEntity.categoryId > 0) {
      this.updateCate(cataEntity);
    } else {
      this.createCate(cataEntity);
    }
  }

  async updateCate(cateEntity : CategoryInput){
    try {
      
      let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.EDIT_CATE,cateEntity);
      if (response && response.message === "Success") {
        this.getAllCate();
        this.showMessage(mType.success, "Notification", "Update successfully", 'notify');
        this.displayDialogEditAddCate = false;
      }else{
        this.showMessage(mType.success, "Notification", "Update failure", 'notify');
      }
    } catch (e) {
      console.log(e);
        this.showMessage(mType.error, "Notification", "Update failure", 'notify');
    }
  }

  async createCate(cateEntity : CategoryInput){
    try {
      const param = {
        "name": cateEntity.name
      }     
               
      let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.ADD_CATE,param);
      if (response && response.message === "Success") {
        this.getAllCate();
        this.showMessage(mType.success, "Notification", "Update successfully", 'notify');
        this.displayDialogEditAddCate = false;
      }else{
        this.showMessage(mType.success, "Notification", "Update failure", 'notify');
      }
    } catch (e) {
      console.log(e);
        this.showMessage(mType.error, "Notification", "Update failure", 'notify');
    }
  }
  onUpdateCate(cate:Category) {
    this.headerDialog = `Edit Category: ${cate.categoryId}`;

    this.displayDialogEditAddCate=true;

    this.cateModel = Object.assign({}, cate);
  }

  onHideCate(cate:Category,event) {
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure to Hide this Category : ${cate.categoryId} ?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        //confirm action
        this.EDCate(cate, false);
      },
      reject: () => {
        //reject action
      },
    });
  }
  onDisplayCate(cate:Category,event){
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure to Display this post id: ${cate.categoryId} ?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        //confirm action
        this.EDCate(cate, true);
      },
      reject: () => {
        //reject action
      },
    });
  }

  async EDCate(cateEnity: Category, type: boolean) {
    // type = true => Display
    // false => Hide

    const message = type ? 'Displayed' : 'Hidden';

    try {
      let param: CateDisplayHideInputDto = new CateDisplayHideInputDto();
      param.categoryId = cateEnity.categoryId;
      param.type = type;

      let response = await this.iServiceBase.postDataAsync(
        API.PHAN_HE.USER,
        API.API_MANAGE.ENABLE_DISABLE_CATE,
        param,
        true
      );

      if (response && response.message === 'Success') {
        this.showMessage(
          mType.success,
          'Notification',
          `${message} Voucher: ${cateEnity.categoryId} successfully`,
          'notify'
        );

        //lấy lại danh sách All
        this.getAllCate();
      } else {
        var messageError = this.iServiceBase.formatMessageError(response);
        console.log(messageError);
        this.showMessage(mType.error, response.message, messageError, 'notify');
      }
    } catch (e) {
      console.log(e);
    }
  }

  onHideDialogEditAdd(){}
  onCancelCate(){
    this.displayDialogEditAddCate = false;
  }
  
}
