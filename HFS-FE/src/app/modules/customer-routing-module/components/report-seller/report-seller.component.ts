import { Component, OnInit } from '@angular/core';
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
import { FoodReport } from '../../models/foodreport.model';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { reportSeller } from '../../models/reportSeller2';
@Component({
  selector: 'app-report-seller',
  templateUrl: './report-seller.component.html',
  styleUrls: ['./report-seller.component.scss']
})
export class ReportSellerComponent extends iComponentBase implements OnInit {
  lstFoodReport: FoodReport[] = [];
  lstReport: reportSeller[] = [];
  contentDialog: reportSeller = new reportSeller(); // entity used in view report detail
  foodReport: FoodReport = new FoodReport(); // entity used in function cancel report
  visibleContentDialog: boolean = false;
  reportImg: reportSeller = new reportSeller();
  loading: boolean = false;
  isVisibleCancelFoodReportModal: boolean;
  isDisabledCancelFoodReportTextArea: boolean;
  visibleImageDialog:boolean = false;
  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    public router: Router,
    public authService: AuthService
    // private appCustomerTopBarComponent: AppCustomerTopBarComponent
  ) {
    super(messageService);
  }

  async ngOnInit(): Promise<void> {
    await this.getAllFoodReport();
  }

  async getAllFoodReport() {
    ;
    this.lstReport = [];
    try {
      this.loading = true;

      let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_USER.REPORT_SELLERCUS,"");

      if (response && response.message === "Success") {
        this.lstReport = response.data;
        console.log(this.lstReport);
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }

  }

  viewContentDetail(foodrp: reportSeller) {
    //
    // format date to dd/mm/yyyy
    const date = new Date(foodrp.createDate);
    const yyyy = date.getFullYear();
    let mm = date.getMonth() + 1; // Months start at 0!
    let dd = date.getDate();
    let hours = date.getHours();
    let minutes = date.getMinutes();
    let seconds = date.getSeconds();

    let ddstr = '';
    let mmstr = '';
    let hourstr = '';
    let minutestr = '';
    let secondstr = '';

    if (dd < 10) ddstr = '0' + dd;
    if (mm < 10) mmstr = '0' + mm;
    if (hours < 10) hourstr = '0' + hours;
    if (minutes < 10) minutestr = '0' + minutes;
    if (seconds < 10) secondstr = '0' + seconds;


    const formattedDate = (ddstr != '' ? ddstr : dd) + '/' + (mmstr != '' ? mmstr : mm) + '/' + yyyy;
    const time = (hourstr != '' ? hourstr : hours) + ':' + (minutestr != '' ? minutestr : minutes) + ':' + (secondstr != '' ? secondstr : seconds);
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
  //   //
  //   console.log(this.foodReport.reportContents);
  //   console.log("rpContents length: " + this.foodReport.reportContents.length);
  //   if (this.foodReport.reportContents.length < 1) {
  //     this.isDisabledCancelFoodReportTextArea = true;
  //   } else {
  //     this.isDisabledCancelFoodReportTextArea = false;
  //   }
  // }
  onDisplayImagesDialog(cus: reportSeller, event: any) {
    this.reportImg = cus;
    this.visibleImageDialog = true;
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