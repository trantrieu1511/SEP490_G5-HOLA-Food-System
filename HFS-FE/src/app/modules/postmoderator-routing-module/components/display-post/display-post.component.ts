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

@Component({
  selector: 'app-display-post',
  templateUrl: './display-post.component.html',
  styleUrls: ['./display-post.component.scss']
})
export class DisplayPostComponent extends iComponentBase implements OnInit {
  // loading: boolean;
  // listPosts: Post[];


  // constructor(public breadcrumbService: AppBreadcrumbService,
  //   private shareData: ShareData,
  //   public messageService: MessageService,
  //   private confirmationService: ConfirmationService,
  //   private iServiceBase: iServiceBase,) {
  //   super(messageService, breadcrumbService);
  // }

  // ngOnInit() {
  //   this.getAllPosts();

  // }

  // async getAllPosts() {
  //   this.listPosts = [];
  //   try {
  //     this.loading = true;

  //     let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.POST, API.API_POST.GET_POST);

  //     if (response && response.message === 'Success') {
  //       this.listPosts = response.posts;
  //     }
  //     this.loading = false;
  //   } catch (e) {
  //     console.log(e);
  //     this.loading = false;
  //   }
  // }

  // onCreatePost(){
  //   console.log(1);
  // }
  lstPost: Post[] = [];
  // selectedPosts: Post[] = [];
  displayDialogEditAddPost: boolean = false;
  headerDialog: string = '';
  postModel: Post = new Post();
  loading: boolean;

  uploadedFiles: File[] = [];

  contentDialog: string;
  visibleContentDialog: boolean = false;

  postImageDialog: Post = new Post();
  visibleImageDialog: boolean = false;

  inputValidation: PostInputValidation = new PostInputValidation();


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

  // Not allow the user to access the page if they are not post moderator
  checkUserAccessPermission() {
    let userRoleName = sessionStorage.getItem("userId").substring(0, 2);
    if (userRoleName !== "PM") {
      this.router.navigateByUrl('/HFSBusiness');
      alert('You cannot access this page unless you are a post moderator');
    }
  }

  async ngOnInit() {
    this.checkUserAccessPermission();
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
    const res = await this.signalRService.addTransferDataListener('dataRealTime' ,API.PHAN_HE.POST, API.API_POST.GET_POST);
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
        console.log(this.lstPost);
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }

  }

  onBanPost(post: Post, event) {
    this.confirmationService.confirm({
      target: event.target,
      message: 'Are you sure to ban this post id: ' + post.postId + '?',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        //confirm action
        this.banUnbanPost(post, true);
      },
      reject: () => {
        //reject action
      }
    });
  }

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

  async banUnbanPost(post: Post, isBanned: boolean) {
    // type = true => Unban
    // false => Ban
    // debugger;
    const message = isBanned ? "Banned" : "Unbanned";

    try {
      let param: PostBanUnbanInputDto = new PostBanUnbanInputDto();
      param.postId = post.postId;
      param.isBanned = isBanned;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.POST, API.API_POST.BAN_UNBAN, param, true);

      if (response && response.message === "Success") {
        this.showMessage(mType.success, "Notification", `${message} postId: ${post.postId} successfully`, 'notify');
        console.log(message + ' postId: ' + post.postId + ' successfully');
        //lấy lại danh sách All 
        this.getAllPost();

      } else {
        var messageError = this.iServiceBase.formatMessageError(response);
        console.log(messageError);
        this.showMessage(mType.error, response.message, messageError, 'notify');
      }
    } catch (e) {
      console.log(e);
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
