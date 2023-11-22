import { Component, OnInit, ViewChild } from '@angular/core';
import { Table } from "primeng/table";
import { AppBreadcrumbService } from "../../../../app-systems/app-breadcrumb/app.breadcrumb.service";
import { Post, PostBanUnbanInputDto, PostInput, PostInputValidation } from "../../models/post.model";
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
import { PostReport } from '../../models/postreport.model';

// interface ApproveNotApproveOption {
//   optionName: string;
// }

@Component({
  selector: 'app-postreport-management',
  templateUrl: './postreport-management.component.html',
  styleUrls: ['./postreport-management.component.scss']
})
export class PostreportManagementComponent extends iComponentBase implements OnInit {
  lstPostReport: PostReport[] = [];

  // selectedPosts: Post[] = [];

  displayDialogEditAddPost: boolean = false;

  headerDialog: string = '';

  postReport: PostReport = new PostReport();

  postModel: Post = new Post();

  loading: boolean;

  uploadedFiles: File[] = [];

  contentDialog: string;

  visibleContentDialog: boolean = false;

  isVisibleApproveNotApproveModal: boolean = false;

  postImageDialog: Post = new Post();

  visibleImageDialog: boolean = false;

  inputValidation: PostInputValidation = new PostInputValidation();

  // listApproveNotApproveOption: ApproveNotApproveOption[] | undefined;

  // approveNotApproveOption: ApproveNotApproveOption | undefined;


  @ViewChild('dt') table: Table;

  constructor(public breadcrumbService: AppBreadcrumbService,
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private signalRService: DataRealTimeService,
    private router: Router
  ) {
    super(messageService, breadcrumbService);
  }

  async ngOnInit() {
    this.connectSignalR();
    this.getAllPostReport();

    // this.listApproveNotApproveOption = [
    //   { optionName: 'Approved' },
    //   { optionName: 'NotApproved' }
    // ];

    console.log(this.uploadedFiles);
    // console.log(this.listApproveNotApproveOption);
  }

  async connectSignalR() {
    this.lstPostReport = [];
    this.signalRService.startConnection();
    const res = await this.signalRService.addTransferDataListener('dataRealTime', API.PHAN_HE.POSTREPORT, API.API_POSTREPORT.GET_ALL_POSTREPORT);
    if (res && res.message === "Success") {
      this.lstPostReport = res.postReports;
    }
  }

  async getAllPostReport() {
    this.lstPostReport = [];
    this.uploadedFiles = [];
    try {
      this.loading = true;

      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.POSTREPORT, API.API_POSTREPORT.GET_ALL_POSTREPORT);

      if (response && response.message === "Success") {
        this.lstPostReport = response.postReports;
        this.lstPostReport.forEach(postrp => {
          // format date to dd/mm/yyyy
          const date = new Date(postrp.createDate);
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
          postrp.createDate = formattedDate + ' ' + time;
        });
        console.log(this.lstPostReport);
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }

  }

  onOpenApproveNotApproveModal(postrp) {
    this.postReport = Object.assign({}, postrp);
    // 
    // this.approveNotApproveOption = { optionName: '' };
    // this.approveNotApproveOption.optionName = this.postReport.status;
    // this.postReport.status = this.approveNotApproveOption.optionName;
    // console.log(postrp);
    // console.log(this.postReport);
    this.isVisibleApproveNotApproveModal = true;
  }

  // onApprove(postrp, event) {
  //   this.confirmationService.confirm({
  //     target: event.target,
  //     message: 'Are you sure to approve this post report id: ' + postrp.postId + '? of user: ' + postrp.reportBy,
  //     icon: 'pi pi-exclamation-triangle',
  //     accept: () => {
  //       //confirm action
  //       this.approveNotApprovePost(postrp, true);
  //     },
  //     reject: () => {
  //       //reject action
  //     }
  //   });
  // }

  // onNotApprove(postrp, event) {
  //   this.confirmationService.confirm({
  //     target: event.target,
  //     message: 'Are you sure to not approve this post id: ' + postrp.postId + '? of user: ' + postrp.reportBy,
  //     icon: 'pi pi-exclamation-triangle',
  //     accept: () => {
  //       //confirm action
  //       this.approveNotApprovePost(postrp, false);
  //     },
  //     reject: () => {
  //       //reject action
  //     }
  //   });
  // }

  validatePostModel(): boolean {
    this.inputValidation = new PostInputValidation();
    var check = true;
    // if (!this.roleModel.roleKey || this.roleModel.roleKey == '') {
    //     this.showMessage(mType.warn, "Thông báo", "Mã phân quyền không được để trống. Vui lòng nhập!", 'notify');
    //     return false;
    // }
    if (!this.postModel.postContent || this.postModel.postContent == '') {
      this.inputValidation.isPostContentValid = false;
      this.inputValidation.postContentMessage = "Please enter a post content";
      check = false;
    }

    return check;
  }

  async approveNotApprovePost(postrp) {
    // type = true => Unban
    // false => Ban
    // 
    let isApproved = postrp.status == 'Approved' ? true : false;
    const message = isApproved ? "Approved" : "Not approved";

    try {
      let param = {
        postId: postrp.postId,
        reportBy: postrp.reportBy,
        isApproved: isApproved,
        note: postrp.note
      };

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.POSTREPORT, API.API_POSTREPORT.APPROVE_NOTAPPROVE_POSTREPORT, param, true);

      if (response && response.message === "Success") {
        this.showMessage(mType.success, "Notification", `${message} post report that has postId: ${postrp.postId} of user: ${postrp.reportBy} successfully`, 'notify');
        console.log(`${message} post report that has postId: ${postrp.postId} of user: ${postrp.reportBy} successfully`);
        //lấy lại danh sách All 
        this.getAllPostReport();

      } else {
        // var messageError = this.iServiceBase.formatMessageError(response);
        // console.log(messageError);
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

  viewContentDetail(postReport: PostReport) {
    this.contentDialog = postReport.reportContent;
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