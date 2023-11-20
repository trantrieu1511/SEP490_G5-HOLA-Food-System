import { Component, OnInit, ViewChild } from '@angular/core';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData
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
import { FileSelectEvent, FileUploadEvent } from 'primeng/fileupload';
import { Router } from '@angular/router';
import { FoodReport } from '../../models/foodreport.model';


@Component({
  selector: 'app-customerfoodreport',
  templateUrl: './customerfoodreport.component.html',
  styleUrls: ['./customerfoodreport.component.scss']
})
export class CustomerfoodreportComponent extends iComponentBase implements OnInit {

  lstFoodReport: FoodReport[] = [];
  contentDialog: FoodReport = new FoodReport(); // entity used in view report detail
  foodReport: FoodReport = new FoodReport(); // entity used in function cancel report
  visibleContentDialog: boolean = false;
  loading: boolean = false;
  isVisibleCancelFoodReportModal: boolean;
  isDisabledCancelFoodReportTextArea: boolean;

  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    public router: Router,
    // private appCustomerTopBarComponent: AppCustomerTopBarComponent
  ) {
    super(messageService);
  }

  async ngOnInit(): Promise<void> {
    await this.getAllFoodReport();
  }

  async getAllFoodReport() {
    this.lstFoodReport = [];
    try {
      this.loading = true;

      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.FOODREPORT, API.API_FOODREPORT.GET_ALL_FOODREPORT);

      if (response && response.message === "Success") {
        this.lstFoodReport = response.foodReports;
        console.log(this.lstFoodReport);
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }

  }

  viewContentDetail(foodrp: FoodReport) {
    // debugger;
    // format date to dd/mm/yyyy
    const date = new Date(foodrp.createDate);
    const yyyy = date.getFullYear();
    let mm = date.getMonth() + 1; // Months start at 0!
    let dd = date.getDate();
    let ddstr = '';
    let mmstr = '';

    if (dd < 10) ddstr = '0' + dd;
    if (mm < 10) mmstr = '0' + mm;

    const formattedDate = dd + '/' + mm + '/' + yyyy;
    const time = foodrp.createDate.split('T')[1];
    // this.contentDialog = foodrp; 
    this.contentDialog = Object.assign({}, foodrp); // Để dòng lệnh bên dưới dòng này không bind ngược lại vào foodrp
    this.contentDialog.createDate = formattedDate + ' ' + time;
    this.visibleContentDialog = true;
    // console.log("contentDialog create date: " + this.contentDialog.createDate);
    // console.log("foodrp create date: " + foodrp.createDate);
    // console.log(foodrp);
  }

  // openCancelModal(foodrp: FoodReport) {
  //   this.foodReport = foodrp;
  // }

  // // Phuc vu cho viec an hien nut submit
  // addValueToReportContentList() {
  //   console.log(this.foodReport.reportContent);
  //   if (this.foodReport.reportContent == '') {
  //     this.foodReport.reportContents.pop();
  //     console.log("poped!");
  //   } else {
  //     this.foodReport.reportContents.push(this.foodReport.reportContent);
  //     console.log("pushed!");
  //   }
  //   this.enableDisableCancelFoodReportButtonSubmit();
  // }

  // enableDisableCancelFoodReportTextArea($event: any) {
  //   if ($event.checked == 'Other') {
  //     this.isDisabledCancelFoodReportTextArea = false;
  //   } else {
  //     this.isDisabledCancelFoodReportTextArea = true;
  //   }
  // }

  // // Validate to make this submit button enable whenever the user input some report content
  // enableDisableCancelFoodReportButtonSubmit() {
  //   // debugger;
  //   console.log(this.foodReport.reportContents);
  //   console.log("rpContents length: " + this.foodReport.reportContents.length);
  //   if (this.foodReport.reportContents.length < 1) {
  //     this.isDisabledCancelFoodReportTextArea = true;
  //   } else {
  //     this.isDisabledCancelFoodReportTextArea = false;
  //   }
  // }

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