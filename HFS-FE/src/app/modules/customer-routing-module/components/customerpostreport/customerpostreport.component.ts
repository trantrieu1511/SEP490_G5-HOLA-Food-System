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
import { PostReport } from '../../models/postreport.model';

@Component({
  selector: 'app-customerpostreport',
  templateUrl: './customerpostreport.component.html',
  styleUrls: ['./customerpostreport.component.scss']
})
export class CustomerpostreportComponent extends iComponentBase implements OnInit{
  lstPostReport: PostReport[] = [];
  contentDialog: PostReport = new PostReport(); // entity used in view report detail
  postReport: PostReport = new PostReport(); // entity used in function cancel report
  visibleContentDialog: boolean = false;
  loading: boolean = false;
  isVisibleCancelPostReportModal: boolean;
  isDisabledCancelPostReportTextArea: boolean;

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
    await this.getAllPostReport();
  }

  async getAllPostReport() {
    this.lstPostReport = [];
    try {
      this.loading = true;

      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.POSTREPORT, API.API_POSTREPORT.GET_ALL_POSTREPORT);

      if (response && response.message === "Success") {
        this.lstPostReport = response.postReports;
        console.log(this.lstPostReport);
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }

  }

  viewContentDetail(postrp: PostReport) {
    // debugger;
    // format date to dd/mm/yyyy
    const date = new Date(postrp.createDate);
    const yyyy = date.getFullYear();
    let mm = date.getMonth() + 1; // Months start at 0!
    let dd = date.getDate();
    let ddstr = '';
    let mmstr = '';

    if (dd < 10) ddstr = '0' + dd;
    if (mm < 10) mmstr = '0' + mm;

    const formattedDate = dd + '/' + mm + '/' + yyyy;
    const time = postrp.createDate.split('T')[1];
    this.contentDialog = Object.assign({}, postrp); // Để dòng lệnh bên dưới dòng này không bind ngược lại vào postrp
    this.contentDialog.createDate = formattedDate + ' ' + time;
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
