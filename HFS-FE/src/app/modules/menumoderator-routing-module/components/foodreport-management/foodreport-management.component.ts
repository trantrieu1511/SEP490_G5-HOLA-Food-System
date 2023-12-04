import { Component, OnInit, ViewChild } from '@angular/core';
import { Table } from "primeng/table";
import { AppBreadcrumbService } from "../../../../app-systems/app-breadcrumb/app.breadcrumb.service";
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData, iFunction
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../../services/apiURL";
import {
  ConfirmationService,
  LazyLoadEvent,
  MenuItem,
  MessageService,
  SelectItem,
  TreeNode
} from "primeng/api";
import { FileRemoveEvent, FileSelectEvent } from 'primeng/fileupload';
import { DataRealTimeService } from 'src/app/services/SignalR/data-real-time.service';
import { Router } from '@angular/router';
import { FoodReport } from '../../models/foodreport.model';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-foodreport-management',
  templateUrl: './foodreport-management.component.html',
  styleUrls: ['./foodreport-management.component.scss']
})
export class FoodreportManagementComponent extends iComponentBase implements OnInit {
  lstFoodReport: FoodReport[] = [];

  foodReport: FoodReport = new FoodReport();

  loading: boolean;

  uploadedFiles: File[] = [];

  contentDialog: string;

  visibleContentDialog: boolean = false;

  isVisibleApproveNotApproveModal: boolean = false;

  userId: string = '';


  @ViewChild('dt') table: Table;

  constructor(public breadcrumbService: AppBreadcrumbService,
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private signalRService: DataRealTimeService,
    private router: Router,
    private authService: AuthService
  ) {
    super(messageService, breadcrumbService);
  }

  async ngOnInit() {
    this.userId = this.authService.getUserInfor().userId;
    this.connectSignalR();
    this.getAllFoodReport();
    console.log(this.uploadedFiles);
  }

  async connectSignalR() {
    this.lstFoodReport = [];
    this.signalRService.startConnection();
    const res = await this.signalRService.addTransferDataListener('dataRealTime', API.PHAN_HE.FOODREPORT, API.API_FOODREPORT.GET_ALL_FOODREPORT);
    if (res && res.message === "Success") {
      this.lstFoodReport = res.foodReports;
    }
  }

  async getAllFoodReport() {
    this.lstFoodReport = [];
    this.uploadedFiles = [];
    try {
      this.loading = true;

      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.FOODREPORT, API.API_FOODREPORT.GET_ALL_FOODREPORT);

      if (response && response.message === "Success") {
        this.lstFoodReport = response.foodReports;
        this.lstFoodReport.forEach(foodrp => {
          // format date to dd/mm/yyyy
          if(foodrp.createDate == '' || foodrp.createDate != undefined){
            foodrp.createDate = this.iServiceBase.formatDatetime(foodrp.createDate);
          }
          if(foodrp.updateDate == '' || foodrp.updateDate != undefined){
            foodrp.updateDate = this.iServiceBase.formatDatetime(foodrp.updateDate);
          }
        });
        console.log(this.lstFoodReport);
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }

  }

  onOpenApproveNotApproveModal(status, foodrp) {
    this.foodReport = Object.assign({}, foodrp);
    this.foodReport.status = status;
    this.isVisibleApproveNotApproveModal = true;
  }

  async approveNotApproveFood(foodrp) {
    // type = true => Unban
    // false => Ban
    // 
    let isApproved = foodrp.status == 'Approved' ? true : false;
    const message = isApproved ? "Approved" : "Not approved";

    try {
      let param = {
        foodId: foodrp.foodId,
        reportBy: foodrp.reportBy,
        isApproved: isApproved,
        note: foodrp.note
      };

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.FOODREPORT, API.API_FOODREPORT.APPROVE_NOTAPPROVE_FOODREPORT, param, true);

      if (response && response.message === "Success") {
        this.showMessage(mType.success, "Notification", `${message} food report that has foodId: ${foodrp.foodId} of user: ${foodrp.reportBy} successfully`, 'notify');
        console.log(`${message} food report that has foodId: ${foodrp.foodId} of user: ${foodrp.reportBy} successfully`);
        //lấy lại danh sách All 
        this.getAllFoodReport();

      } else {
        // var messageError = this.iServiceBase.formatMessageError(response);
        console.log(response);
        console.log(response.message);
        this.showMessage(mType.error, "Error", response.message, 'notify');
      }
    } catch (e) {
      console.log(e);
      this.showMessage(mType.error, "Error", "BE error, please contact admin for further help.", 'notify');
    }
    //hide modal
    this.isVisibleApproveNotApproveModal = false;
  }

  viewContentDetail(detail: string) {
    this.contentDialog = detail;
    this.visibleContentDialog = true;
  }

  getSeverity(status: string) {
    switch (status) {
      case 'Pending':
        return 'secondary';
      case 'Approved':
        return 'success';
      case 'NotApproved':
        return 'warning';
      case 'Ban':
        return 'danger';
      default:
        return 'error';
    }
  }
}
