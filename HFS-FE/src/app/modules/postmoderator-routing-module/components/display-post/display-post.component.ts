import { Component, OnInit, ViewChild } from '@angular/core';
import { Table } from "primeng/table";
import { AppBreadcrumbService } from "../../../../app-systems/app-breadcrumb/app.breadcrumb.service";
import { BanUnbanInputValidation, Post, PostBanUnbanInputDto, PostInput, PostInputValidation } from "../../models/post.model";
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
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-display-post',
  templateUrl: './display-post.component.html',
  styleUrls: ['./display-post.component.scss']
})
export class DisplayPostComponent extends iComponentBase implements OnInit {

  // -------- Binding variables -----------
  lstPost: Post[] = [];
  // selectedPosts: Post[] = [];
  headerDialog: string = '';
  postModel: Post = new Post();
  banDetail: Post = new Post();
  postImageDialog: Post = new Post();
  uploadedFiles: File[] = [];
  contentDialog: string = '';
  userId: string = '';

  // ---------- UI variables ------------
  visibleContentDialog: boolean = false;
  displayDialogEditAddPost: boolean = false;
  visibleImageDialog: boolean = false;
  visibleBanDetailDialog: boolean = false;
  visibleBanNoteDialog: boolean = false;
  loading: boolean = false;

  // --------- Validation variables ------------
  inputValidation: PostInputValidation = new PostInputValidation();
  banUnbanInputValidation: BanUnbanInputValidation = new BanUnbanInputValidation();


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

  // Not allow the user to access the page if they are not post moderator
  // checkUserAccessPermission() {
  //   let userRoleName = sessionStorage.getItem("userId").substring(0, 2);
  //   if (userRoleName !== "PM") {
  //     this.router.navigateByUrl('/HFSBusiness');
  //     alert('You cannot access this page unless you are a post moderator');
  //   }
  // }

  async ngOnInit() {
    // this.checkUserAccessPermission();
    this.userId = this.authService.getUserInfor().userId;
    this.connectSignalR();
    this.getAllPost();

    // Convert CustomFile[] to File[]
    // this.uploadedFiles = this.fileList.map(customFile => {
    //   const blob = new Blob([], { type: 'application/octet-stream' });
    //   const file = new File([blob], customFile.name, { type: 'image/*' });

    //   Object.defineProperty(file, 'imageBase64', {
    //     value: customFile.imageBase64,
    //     writable: false,
    //     enumerable: true,
    //     configurable: true,
    //   });

    //   return file;
    // });

    console.log(this.uploadedFiles);
  }

  async connectSignalR() {
    this.lstPost = [];
    this.signalRService.startConnection();
    const res = await this.signalRService.addTransferDataListener('postDataRealTime', API.PHAN_HE.POST, API.API_POST.GET_POST);
    if (res && res.message === "Success") {
      this.lstPost = res.posts;
    }
  }

  async getAllPost() {
    this.lstPost = [];
    this.uploadedFiles = [];
    try {
      this.loading = true;

      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.POST, API.API_POST.GET_POST);

      if (response && response.message === "Success") {
        this.lstPost = response.posts;
        this.lstPost.forEach(element => {
          if (element.banDate != undefined)
            element.banDate = this.iServiceBase.formatDatetime(element.banDate)
        });
        console.log(this.lstPost);
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }

  }

  // onBanPost(post: Post, event) {
  //   this.confirmationService.confirm({
  //     target: event.target,
  //     message: 'Are you sure to ban this post id: ' + post.postId + '?',
  //     icon: 'pi pi-exclamation-triangle',
  //     accept: () => {
  //       //confirm action
  //       this.banUnbanPost(post, true);
  //     },
  //     reject: () => {
  //       //reject action
  //     }
  //   });
  // }

  onUnbanPost(post: Post, event) {
    this.confirmationService.confirm({
      target: event.target,
      message: 'Are you sure to unban this post id: ' + post.postId + '?',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        //confirm action
        this.banUnbanPost(post, false);
      },
      reject: () => {
        //reject action
      }
    });
  }

  onOpenBanNoteDialog(post) {
    this.postModel = Object.assign({}, post);
    this.banUnbanInputValidation = new BanUnbanInputValidation();
    this.visibleBanNoteDialog = true;
  }

  onOpenBanDetailDialog(post) {
    this.banDetail = Object.assign({}, post);
    this.visibleBanDetailDialog = true;
  }


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

  validateBanUnbanInput() {
    this.banUnbanInputValidation = new BanUnbanInputValidation();
    var check = true;
    if (!this.postModel.banNote || this.postModel.banNote == '') {
      this.banUnbanInputValidation.isBanNoteValid = false;
      this.banUnbanInputValidation.banNoteMessage = "Please enter a ban reason";
      check = false;
    }

    console.log(this.banUnbanInputValidation);
    return check;
  }

  async banUnbanPost(post: Post, isBanned: boolean) {
    if (isBanned) {
      if (this.validateBanUnbanInput()) {
        // type = true => Unban
        // false => Ban
        // 
        const message = isBanned ? "Banned" : "Unbanned";

        try {
          let param: PostBanUnbanInputDto = new PostBanUnbanInputDto();
          param.postId = post.postId;
          param.isBanned = isBanned;
          param.banNote = post.banNote;

          let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.POST, API.API_POST.BAN_UNBAN, param, true);

          if (response && response.message === "Success") {
            console.log(message + ' postId: ' + post.postId + ' successfully');
            this.showMessage(mType.success, "Notification", `${message} postId: ${post.postId} successfully`, 'notify');
            //lấy lại danh sách All 
            this.getAllPost();

            // Đóng dialog
            this.visibleBanNoteDialog = false;

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
      }
    } else {
      // type = true => Unban
      // false => Ban
      // 
      const message = isBanned ? "Banned" : "Unbanned";

      try {
        let param: PostBanUnbanInputDto = new PostBanUnbanInputDto();
        param.postId = post.postId;
        param.isBanned = isBanned;
        // param.banNote = post.banNote;

        let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.POST, API.API_POST.BAN_UNBAN, param, true);

        if (response && response.message === "Success") {
          console.log(message + ' postId: ' + post.postId + ' successfully');
          this.showMessage(mType.success, "Notification", `${message} postId: ${post.postId} successfully`, 'notify');
          //lấy lại danh sách All 
          this.getAllPost();

          // Đóng dialog
          this.visibleBanNoteDialog = false;

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
    }
  }

  viewContentDetail(post: Post) {
    this.contentDialog = post.postContent;
    this.visibleContentDialog = true;
  }

  getSeverity(status: string) {
    switch (status) {
      case 'NotApproved':
        return 'warning';
      case 'Display':
        return 'success';
      case 'Hidden':
        return 'secondary';
      case 'Ban':
        return 'danger';
      default:
        return 'error';
    }
  }

  handleFileSelection(event: FileSelectEvent) {
    //console.log("select", event);

    this.uploadedFiles = event.currentFiles;


    // const files = event.currentFiles as File[];

    // // Process each file
    // for (const file of files) {
    //   const customFile: CustomFile = {
    //     imageBase64: '', // Placeholder for base64 data
    //     name: file.name,
    //     size: this.formatSize(file.size)
    //   };

    //   this.fileList.push(customFile);

    //   // Read file as base64
    //   const reader = new FileReader();
    //   reader.onloadend = () => {
    //     customFile.imageBase64 = reader.result as string;
    //   };
    //   reader.readAsDataURL(file);
    // }


    // console.log("currentfile", event.currentFiles);
    // console.log("customfile", this.fileList);
  }

  formatSize(size: number): string {
    // Format file size as needed
    // Example: Convert bytes to KB
    return (size / 1024).toFixed(2) + ' KB';
  }

  handleFileRemoval(event: FileRemoveEvent) {
    //console.log("remove", event.file.name);

    this.uploadedFiles = this.uploadedFiles.filter(f => f.name !== event.file.name);
    //console.log("uploadFiles", this.uploadedFiles);
  }

  handleAllFilesClear(event: Event) {
    //console.log("clear", event);

    this.uploadedFiles = [];
    //console.log("uploadFiles", this.uploadedFiles);
  }

  onDisplayImagesDialog(post: Post, event: any) {
    this.postImageDialog = post;
    this.visibleImageDialog = true;
  }

  onHideDialogEditAdd() {
    this.uploadedFiles = [];
  }
}
