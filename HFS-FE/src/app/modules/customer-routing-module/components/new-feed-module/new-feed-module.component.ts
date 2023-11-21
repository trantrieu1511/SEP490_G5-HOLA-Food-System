import { Component, OnInit } from '@angular/core';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../../services/apiURL";
import { Post } from '../../models/post.model';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from 'src/app/services/data.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { CommentNewFeed, InputComment } from '../../models/comment.model';
import { PostReport } from '../../models/postreport.model';
@Component({
  selector: 'app-new-feed-module',
  templateUrl: './new-feed-module.component.html',
  styleUrls: ['./new-feed-module.component.scss']
})

export class NewFeedModuleComponent extends iComponentBase implements OnInit {
  loading: boolean;
  listPost: Post[] = [];
  lstComment: CommentNewFeed[] = [];
  commentModel: CommentNewFeed = new CommentNewFeed();
  userId: string;
  postId: number;

  //----------- Post report variables -----------
  isVisiblePostReportModal = false; // Bien phuc vu cho viec bat tat modal post report
  postReport: PostReport = new PostReport();
  isDisabledPostReportBtnSubmit: boolean = true; // Trạng thái disable của nút submit của modal post report
  isDisabledPostReportTextArea: boolean = true; // Trạng thái disable của text area của modal post report
  // hasAlreadyReported: boolean = false;
  isLoggedIn: boolean = false;
  listPostReport: PostReport[] = [];
  reportedPostIds: number[] = []; // List luu tru id cua nhung post nao da bi report boi khach hang
  isReported: boolean = false;

  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private route: ActivatedRoute,
    private router: Router,
    private dataService: DataService
  ) {
    super(messageService);
  }

  checkLoggedIn() {
    if (sessionStorage.getItem('userId') != null) {
      this.isLoggedIn = true;
    }
  }

  async ngOnInit(): Promise<void> {
    this.checkLoggedIn();

    await this.getAllPost();

    await this.checkUsersReportPostCapability();
    this.enableDisablePostReportButtonSubmit(); // reset nut submit 

  }

  async getAllPost() {
    this.listPost = [];
    try {
      const param = {
        "status": 1
      }
      this.loading = true;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_NEWFEED.GETALLPOST, param);
      console.log(response)
      if (response && response.message === "Success") {
        this.listPost = response.posts;
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  OnCommnent(item: Post) {
    this.getAllComment(item.postId);
    this.postId = item.postId;
  }

  bindingDataCommentModel(): InputComment {
    let comment = new InputComment();
    if (this.commentModel.commentId && this.commentModel.commentId > 0) {
      //Update
      comment.commentId = this.commentModel.commentId;
      comment.commentContent = this.commentModel.commentContent;
      comment.customerId = this.commentModel.customerId;
      comment.postId = this.commentModel.postId
    } else {
      comment.commentContent = this.commentModel.commentContent;
      comment.customerId = this.commentModel.customerId;
      comment.postId = this.commentModel.postId
    }
    return comment;
  }
  OnSaveCommnent() {
    let comEntity = this.bindingDataCommentModel();
    console.log(comEntity);
    if (comEntity && comEntity.commentId && comEntity.commentId > 0) {
      //this.updateVoucher(voucherEntity);
    } else {
      this.createComment(comEntity);
    }
    this.commentModel = new CommentNewFeed();
  }

  async createComment(comEntity: InputComment) {
    try {
      //
      this.userId = sessionStorage.getItem('userId'); 
      const param = {
        'postId': this.postId,
        'customerId': this.userId,
        'commentContent': comEntity.commentContent
      }
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_NEWFEED.CREATECOMMENT, param);
      if (response && response.message === "Success") {
        this.showMessage(mType.success, "Notification", "Create successfully", 'notify');
        this.getAllComment(this.postId);
      } else {
        this.showMessage(mType.success, "Notification", "Create failure", 'notify');
      }
    } catch (e) {
      console.log(e);
      this.showMessage(mType.error, "Notification", "Create failure", 'notify');
    }
  }

  async getAllComment(item: number) {
    this.lstComment = [];
    try {
      const param = {
        "postId": item
      }
      this.loading = true;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_NEWFEED.GetALLCOMMENT, param);
      console.log(response)
      if (response && response.message === "Success") {
        this.lstComment = response.listComment;
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  // Hàm này kiểm tra khả năng tố cáo bằng cách xem customer đã report cái post với id: cụ thể nào đó chưa. Nếu rồi thì sẽ không sử dụng lại tính năng tố cáo đối với cái post đó nữa.
  async checkUsersReportPostCapability() {
    // debugger;
    try {
      this.loading = true;
      this.reportedPostIds = [];

      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.POSTREPORT, API.API_POSTREPORT.GET_ALL_POSTREPORT);

      if (response && response.message === "Success") {
        this.listPostReport = response.postReports;
        this.listPostReport.forEach(postrp => {
          this.listPost.forEach((post, index) => {
            if (postrp.postId == post.postId) { // Da report
              // this.reportedPostIds.push(post.postId);
              // this.isReported = true;
              this.listPost[index].isReported = true;
            }
          });
        });
      }
      console.log(this.reportedPostIds);
      console.log(this.isReported);
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  openPostReportDialog(postId: number) {
    this.postReport = new PostReport();
    this.enableDisablePostReportButtonSubmit(); // reset nut submit 

    this.postReport.postId = postId;
    this.isVisiblePostReportModal = true;
    // event.preventDefault();
  }

  async submitReport() {
    //------------ Lay cac ly do report duoc input boi nguoi dung -------------
    let rpContent: string = "";
    let i = 0;
    // debugger;
    console.log(this.postReport.reportContents);
    this.postReport.reportContents.forEach(element => {
      i++;
      console.log("element" + i + ": " + element);
      if (element == this.postReport.reportContents[this.postReport.reportContents.length - 1]) { //the last element in the array
        rpContent += element;
      } else {
        rpContent += element + ", ";
      }
    });
    // rpContent += ", " + this.postReport.reportContent;

    this.postReport.reportContent = rpContent;
    console.log("Full rp content: " + this.postReport.reportContent);

    // ------------------ Commit vao db --------------------
    try {
      this.loading = true;
      let param = {
        postId: this.postReport.postId,
        reportContent: this.postReport.reportContent
      }
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.POSTREPORT, API.API_POSTREPORT.CREATE_NEW_POSTREPORT, param);
      if (response && response.success === true) {
        this.showMessage(mType.success, "Notification", `Report the food successfully`, 'notify');
        console.log(response);
        console.log('Create new food report successfully');
      }
      else {
        // this.showMessage(mType.warn, "Error", this.iServiceBase.formatMessageError(response.message), 'notify');
        this.showMessage(mType.warn, "Error", "Internal server error, please contact for admin help!", 'notify');
        console.log(response);
        console.log('Internal server error, please contact for admin help!');
      }
      this.loading = false;
      this.checkUsersReportPostCapability();
    } catch (e) {
      console.log(e);
      this.loading = false;
      this.checkUsersReportPostCapability();
    }

    // Làm mới model để không bị ảnh hường bởi two way binding
    this.postReport = new PostReport();
    // Tat modal
    this.isVisiblePostReportModal = false;
    // Refresh nut submit
    this.enableDisablePostReportButtonSubmit();
  }

  // Phuc vu cho viec an hien nut submit o modal post report
  addValueToReportContentList() {
    console.log(this.postReport.reportContent);
    if (this.postReport.reportContent == '') {
      this.postReport.reportContents.pop();
      console.log("poped!");
    } else {
      this.postReport.reportContents.push(this.postReport.reportContent);
      console.log("pushed!");
    }
    this.enableDisablePostReportButtonSubmit();
  }

  enableDisablePostReportTextArea($event: any) {
    if ($event.checked == 'Other') {
      this.isDisabledPostReportTextArea = false;
    } else {
      this.isDisabledPostReportTextArea = true;
    }
  }

  enableDisablePostReportButtonSubmit() {
    // debugger;
    console.log(this.postReport.reportContents);
    console.log("rpContents length: " + this.postReport.reportContents.length);
    if (this.postReport.reportContents.length < 1) {
      this.isDisabledPostReportBtnSubmit = true;
    } else {
      this.isDisabledPostReportBtnSubmit = false;
    }
  }
}
