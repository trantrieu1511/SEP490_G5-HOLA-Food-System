import {Component, OnInit, ViewChild} from '@angular/core';
import {Table} from "primeng/table";
import {AppBreadcrumbService} from "../../../../app-systems/app-breadcrumb/app.breadcrumb.service";
import {Post, PostDisplayHideInputDto, PostInput, PostInputValidation} from "../../models/post.model";
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

@Component({
  selector: 'app-post-management',
  templateUrl: './post-management.component.html',
  styleUrls: ['./post-management.component.scss']
})
export class PostManagementComponent extends iComponentBase implements OnInit {
  
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

  inputValidation : PostInputValidation = new PostInputValidation();


  @ViewChild('dt') table: Table;

  constructor(public breadcrumbService: AppBreadcrumbService,
              private shareData: ShareData,
              public messageService: MessageService,
              private confirmationService: ConfirmationService,
              private iServiceBase: iServiceBase,
              private iFunction: iFunction
              ) {
      super(messageService, breadcrumbService);
  }

  ngOnInit() {
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

  async getAllPost() {
      this.lstPost = [];
      this.uploadedFiles = [];
      try {
          this.loading = true;

          let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.POST, API.API_POST.GET_POST_SELLER);

          if (response && response.message === "Success") {
            this.lstPost = response.posts;
            //console.log(this.lstPost);
          }
          this.loading = false;
      } catch (e) {
          console.log(e);
          this.loading = false;
      }

  }

  bindingDataPostModel(): PostInput {

    let postInput = new PostInput();

    if (this.postModel.postId && this.postModel.postId > 0) {
      //Update
      postInput.postId = this.postModel.postId;
      postInput.postContent = this.postModel.postContent;
      postInput.images = this.uploadedFiles;
    } else {
        //Insert
        postInput.postContent = this.postModel.postContent;
        postInput.images = this.uploadedFiles;
    }

    return postInput;
  }

  onCreatePost() {
      this.headerDialog = 'Add New Post';

      this.uploadedFiles = [];
      
      this.postModel = new Post();

      this.uploadedFiles = [];

      this.displayDialogEditAddPost = true;
  }

  onUpdatePost(post: Post) {
      this.headerDialog = `Edit Post ID: ${post.postId}`;

      this.postModel = Object.assign({}, post);

      //this.uploadedFiles = post.images;
      this.uploadedFiles = [];

      post.imagesBase64.forEach(image => {
        var fileimage = this.iFunction.convertImageBase64toFile(image.imageBase64, image.name);
        this.uploadedFiles.push(fileimage);
        //this.f_upload.files.push(fileimage);
      });

      this.displayDialogEditAddPost = true;
  }

  onHidePost(post: Post, event) {
      this.confirmationService.confirm({
          target: event.target,
          message: `Are you sure to Hide this post id: ${post.postId} ?`,
          icon: 'pi pi-exclamation-triangle',
          accept: () => {
              //confirm action
              this.deletePost(post, false);
          },
          reject: () => {
              //reject action
          }
      });
  }

  
  onDisplayPost(post: Post, event) {
    this.confirmationService.confirm({
        target: event.target,
        message: `Are you sure to Display this post id: ${post.postId} ?`,
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
            //confirm action
            this.deletePost(post, true);
        },
        reject: () => {
            //reject action
        }
    });
  }

  onSavePost() {
    //console.log(this.uploadedFiles);
    let postEnity = this.bindingDataPostModel();

    if (this.validatePostModel()) {
      document.body.style.cursor = 'wait';

      if (postEnity && postEnity.postId && postEnity.postId > 0) {
          this.updatePost(postEnity);
      } else {
          this.createPost(postEnity);
      }
    }
  }

  onCancelPost() {
      this.postModel = new Post();

      this.displayDialogEditAddPost = false;
  }

  validatePostModel(): boolean {
    this.inputValidation = new PostInputValidation();
    var check = true;
      // if (!this.roleModel.roleKey || this.roleModel.roleKey == '') {
      //     this.showMessage(mType.warn, "Thông báo", "Mã phân quyền không được để trống. Vui lòng nhập!", 'notify');
      //     return false;
      // }
    if(!this.postModel.postContent || this.postModel.postContent == ''){
      this.inputValidation.isPostContentValid = false;
      this.inputValidation.postContentMessage = "Please enter a post content";
      check = false;
    }

    return true;
  }

  async createPost(postEnity: PostInput) {
    try {
      //let param = postEnity;
      const param = new FormData();
      param.append('postContent', postEnity.postContent);

      this.uploadedFiles.forEach(file => {
        param.append('images', file, file.name);
      });

      //console.log(param);
      const response = await this.iServiceBase
        .postDataAsync(API.PHAN_HE.POST, API.API_POST.ADD_POST_SELLER, param, true);
      
      if(response && response.message === "Success"){
        this.showMessage(mType.success, "Notification", "New post added successfully", 'notify');

        this.displayDialogEditAddPost = false;

        //lấy lại danh sách All 
        this.getAllPost();

        //Clear model đã tạo
        this.postModel = new Post();
        //clear file upload too =))
        this.uploadedFiles = [];
      } 
    } catch (e) {
      console.log(e);
      this.showMessage(mType.error, "Notification", "Adding new post failed", 'notify');
    }
  }

  async updatePost(postEnity: PostInput) {
    try {
      const param = new FormData();
      param.append('postContent', postEnity.postContent);
      param.append('postId', postEnity.postId.toString());

      this.uploadedFiles.forEach(file => {
        param.append('images', file, file.name);
      });

      let response = await this.iServiceBase
        .putDataAsync(API.PHAN_HE.POST, API.API_POST.UPDATE_POST, param, true);

      if (response && response.success) {
        this.showMessage(mType.success, "Notification", "New post updated successfully", 'notify');

        this.displayDialogEditAddPost = false;

        //lấy lại danh sách All 
        this.getAllPost();

        //Clear model đã tạo
        this.postModel = new Post();
        //clear file upload too =))
        this.uploadedFiles = [];
      } else {
        this.showMessage(mType.warn, "Notification", "New post updated failed", 'notify');
      }
    } catch (e) {
        console.log(e);
    }
  }

  async deletePost(postEnity: Post, type: boolean) {
    // type = true => Display
    // false => Hide

    const message = type ? "Displayed" : "Hidden";

    try {
      let param : PostDisplayHideInputDto = new PostDisplayHideInputDto();
      param.postId = postEnity.postId;
      param.type = type;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.POST, API.API_POST.ENABLE_DISABLE_SELLER, param, true);

      if (response && response.success) {
        this.showMessage(mType.success, "Notification",  `${message} postId: ${postEnity.postId} successfully`, 'notify');

        //lấy lại danh sách All 
        this.getAllPost();

      } else {
        console.log(response);
        this.showMessage(mType.warn, "Notification", response.message, 'notify');
      }
    } catch (e) {
        console.log(e);
    }
  }

  viewContentDetail(post: Post){
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

  handleFileSelection(event :  FileSelectEvent) {
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

  handleFileRemoval(event :  FileRemoveEvent){
    //console.log("remove", event.file.name);

    this.uploadedFiles = this.uploadedFiles.filter(f => f.name !== event.file.name);
    //console.log("uploadFiles", this.uploadedFiles);
  }

  handleAllFilesClear(event :  Event){
    //console.log("clear", event);

    this.uploadedFiles = [];
    //console.log("uploadFiles", this.uploadedFiles);
  }

  onDisplayImagesDialog(post: Post, event: any){
    this.postImageDialog = post;
    this.visibleImageDialog = true;
  }

  onHideDialogEditAdd(){
    this.uploadedFiles = [];
  }
}
