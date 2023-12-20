import { AfterViewInit, Component, OnInit, ViewChildren, QueryList, ElementRef } from '@angular/core';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../../services/apiURL";
import { ImageBase64, Post, PostVote } from '../../models/post.model';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from 'src/app/services/data.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { CommentNewFeed, InputComment } from '../../models/comment.model';
import { PostReport } from '../../models/postreport.model';
import { LayoutService } from '../../../../layout/service/app.layout.service';
import { AuthService } from 'src/app/services/auth.service';
import { ClipboardModule, ClipboardService } from 'ngx-clipboard';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-new-feed-module',
  templateUrl: './new-feed-module.component.html',
  styleUrls: ['./new-feed-module.component.scss']
})

export class NewFeedModuleComponent extends iComponentBase implements OnInit, AfterViewInit {
  loading: boolean;
  listPost: Post[] = [];
  lstComment: CommentNewFeed[] = [];
  commentModel: CommentNewFeed = new CommentNewFeed();
  userId: string;
  postId: number;
  isSaveButtonDisabled :boolean=true;
  first = 0;
  rows = 10;
  totalRecords = 0;
  //----------- Post report variables -----------
  isVisiblePostReportModal = false; // Bien phuc vu cho viec bat tat modal post report
  postReport: PostReport = new PostReport();
  isDisabledPostReportBtnSubmit: boolean = true; // Trạng thái disable của nút submit của modal post report
  isDisabledPostReportTextArea: boolean = true; // Trạng thái disable của text area của modal post report
  // hasAlreadyReported: boolean = false;
  isLoggedIn: boolean = false;
  listPostReport: PostReport[] = [];
  reportedPostIds: number[] = []; // List luu tru id cua nhung post nao da bi report boi khach hang
  // isReported: boolean = false;

  displayBasic: boolean | undefined;
  activeIndex: number = 0;
  imagesLst: ImageBase64[] = [];
  responsiveOptions: any[] = [
    {
      breakpoint: '1500px',
      numVisible: 5
    },
    {
      breakpoint: '1024px',
      numVisible: 3
    },
    {
      breakpoint: '768px',
      numVisible: 2
    },
    {
      breakpoint: '560px',
      numVisible: 1
    }
  ];

  visibleCommentDialog: boolean = false;
  postViewComment: Post = new Post();

  @ViewChildren('postContent') postContentRefs!: QueryList<ElementRef>;

  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private route: ActivatedRoute,
    private router: Router,
    private dataService: DataService,
    public layoutService: LayoutService,
    private authService: AuthService,
    private clipboard: ClipboardService,
    public translate: TranslateService
  ) {
    super(messageService);

    this.checkLoggedIn();
  }

  checkLoggedIn() {
    // if (sessionStorage.getItem('userId') != null) {
    //   this.isLoggedIn = true;
    // }
    var user = this.authService.getUserInfor();
    if (user != null) {
      this.userId = user.userId;
      this.isLoggedIn = true;

    }
  }

  copyToClipboard() {
      const isCopied: boolean = this.clipboard.copyFromContent("");

      if (isCopied) {
        console.log('URL copied to clipboard');
      } else {
        console.error('Error copying URL to clipboard');
      }
  }
  checkSaveButtonStatus() {
    // Example condition: enable the button if both note and status are filled
    this.isSaveButtonDisabled = !this.commentModel.commentContent.trim;
}
  async ngOnInit(): Promise<void> {
    await this.getAllPost();


    if (this.isLoggedIn) {
      await this.checkUsersReportPostCapability();
      this.enableDisablePostReportButtonSubmit(); // reset nut submit
    }

  }

  async getAllPost() {
    ;
    this.listPost = [];
    try {
      const param = {
        "status": 1,
        "page": this.first / this.rows + 1,
        "pageSize": this.rows
      }
      this.loading = true;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_NEWFEED.GETALLPOST, param, false);
      if (response && response.message === "Success") {
        this.listPost = response.posts;
        this.totalRecords = response.totalPosts;
        this.checkLoggedIn();
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }
  // product-list.component.ts

  onPageChange(event: any): void {
    this.first = event.first;
    this.rows = event.rows;
    this.getAllPost();
  }

  OnCommnent(event: any, item: Post) {
    event.preventDefault();
    this.postViewComment = item;
    this.visibleCommentDialog = true;
    this.getAllComment(item.postId);
    this.postId = item.postId;

  }

  onShopDetail(shop: string) {
    this.router.navigate(['/shopdetail'], { queryParams: { shopid: shop } });
  }

  async onVote(item: Post) {
    try {
      this.loading = true;
      let vote = new PostVote();
      vote.postId = item.postId
      vote.isLike = !item.isLiked
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.POST, API.API_NEWFEED.VOTE_POST, vote);
      if (response && response.success === true) {
        this.listPost
        .filter(x => x.postId === item.postId)
        .forEach(x => {
          if (!x.isLiked) x.likeCount++;
          if (x.isLiked) x.likeCount--;
          x.isLiked = !x.isLiked;
        })
      }
      else{
        this.showMessage(mType.warn, "Notification", response.message, 'notify');
      }
      this.loading = false;
    } catch (e) {
      this.showMessage(mType.warn, "", "There was some problem please try againg or contact for admin help!", 'notify');
      this.loading = false;
    }
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
    ;
    return comment;
  }
  OnSaveCommnent(postId: number) {
    let comEntity = this.bindingDataCommentModel();
    comEntity.postId = postId;
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
      //this.userId = sessionStorage.getItem('userId');
      const param = {
        'postId': comEntity.postId,
        'customerId': this.userId,
        'commentContent': comEntity.commentContent
      }
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_NEWFEED.CREATECOMMENT, param);
      if (response && response.message === "Success") {
        this.showMessage(mType.success, "Notification", "Create successfully", 'notify');
        this.getAllComment(this.postId);
        this.commentModel= new CommentNewFeed();
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
    // ;
    try {
      this.loading = true;
      this.reportedPostIds = [];

      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.POSTREPORT, API.API_POSTREPORT.GET_ALL_POSTREPORT);

      if (response && response.message === "Success") {
        this.listPostReport = response.postReports;
        this.listPost.forEach((post, index) => {
          if (this.listPostReport.length < 1) {
            this.listPost[index].isReported = false;
            return;
          }

          this.listPostReport.forEach(postrp => {
            if (postrp.postId == post.postId) { // Da report
              // this.reportedPostIds.push(post.postId);
              // this.isReported = true;
              post.isReported = true;
              // console.log(this.listPost[index].isReported);
            }
            // else{
            // this.listPost[index].isReported = false;
            // console.log(this.listPost[index].isReported);
            // }
          });
        });
      }
      //console.log(this.listPost);
      // console.log(this.isReported);
      // console.log(this.reportedPostIds);
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
    // ;
    console.log(this.postReport.reportContents);
    this.postReport.reportContents.forEach(element => {
      i++;
      console.log("element" + i + ": " + element);
      if (element == this.postReport.reportContents[this.postReport.reportContents.length - 1]) { //the last element in the array
        rpContent += element; // Phan tu cuoi khong can cong them dau ", "
      } else {
        rpContent += element + ", "; // Cong them dau ", "
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
        this.showMessage(mType.success, "Notification", `Report the post successfully`, 'notify');
        console.log(response);
        console.log('Create new post report successfully');
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
    // ;
    console.log(this.postReport.reportContents);
    console.log("rpContents length: " + this.postReport.reportContents.length);
    if (this.postReport.reportContents.length < 1) {
      this.isDisabledPostReportBtnSubmit = true;
    } else {
      this.isDisabledPostReportBtnSubmit = false;
    }
  }

  ngAfterViewInit() {

    this.postContentRefs.changes.subscribe(() => {
      this.checkContentOverflow();
    });
  }

  checkContentOverflow() {
    if (this.postContentRefs && this.postContentRefs.length > 0) {
      this.postContentRefs.forEach(ref => {
        if (ref.nativeElement.scrollHeight > ref.nativeElement.clientHeight) {
          //console.log(ref.nativeElement.scrollHeight + ': ' + ref.nativeElement.clientHeight)
          ref.nativeElement.children[1].setAttribute("style", "display: block")
        }
      }
      )
    }
  }

  getRemainingImageCount(item) {
    if (!this.layoutService.isMobile()) {
      return item.imagesBase64.length - 4;
    }
    return item.imagesBase64.length - 1;
  }

  onViewImage(indexPost: number, indexImage: number) {
    this.imagesLst = this.listPost[indexPost].imagesBase64;
    this.activeIndex = indexImage;
    this.displayBasic = true;
  }

  getImageSrc(item: ImageBase64) {
    return "data:image/gif;base64," + item.imageBase64;
  }

  onViewMore(index: any) {
    if (this.postContentRefs && this.postContentRefs.length > 0) {
      // reset max-height of parent
      this.postContentRefs.get(index)
        .nativeElement.children[0].parentElement
        .setAttribute("style", "max-height: 100%; ")
      // change display this p content
      this.postContentRefs.get(index)
        .nativeElement.children[0]
        .setAttribute("style", "display: block; ")
        ;
      // hide view more button
      this.postContentRefs.get(index)
        .nativeElement.children[1].setAttribute("style", "display: none")
      console.log(this.postContentRefs.get(index).nativeElement.children)
      // display view less button
      this.postContentRefs.get(index)
        .nativeElement.children[2].setAttribute("style", "display: block")
    }
  }

  onViewLess(index: any) {
    if (this.postContentRefs && this.postContentRefs.length > 0) {
      // reset max-height of parent
      this.postContentRefs.get(index)
        .nativeElement.children[0].parentElement
        .setAttribute("style", "max-height: 240px; ")
      // change display this p content
      this.postContentRefs.get(index)
        .nativeElement.children[0]
        .setAttribute("style", "display: -webkit-box; ")
        ;
      // display view more button
      this.postContentRefs.get(index)
        .nativeElement.children[1].setAttribute("style", "display: block")

      // hide view less button
      this.postContentRefs.get(index)
        .nativeElement.children[2].setAttribute("style", "display: none")
    }
  }
}
