import {Component, OnInit, ViewChild} from '@angular/core';
import {Table} from "primeng/table";
import {AppBreadcrumbService} from "../../../../app-systems/app-breadcrumb/app.breadcrumb.service";
import {Post, PostImage} from "../../models/post.model";
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


  @ViewChild('dt') table: Table;

  constructor(public breadcrumbService: AppBreadcrumbService,
              private shareData: ShareData,
              public messageService: MessageService,
              private confirmationService: ConfirmationService,
              private iServiceBase: iServiceBase,) {
      super(messageService, breadcrumbService);
  }

  ngOnInit() {
    this.getAllPost();
    
  }

  async getAllPost() {
      this.lstPost = [];
      this.uploadedFiles = [];
      // try {
      //     this.loading = true;

      //     let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.QTHT, API.API_QTHT.GET_ALL_ROLE);

      //     if (response && response.length) {
      //         this.lstAppRole = response;
      //     }
      //     this.loading = false;
      // } catch (e) {
      //     console.log(e);
      //     this.loading = false;
      // }

      this.lstPost = [
        {
          postId: 1,
          postContent: "abcaisefdojueiwf",
          createdDate: "12/12/2293",
          status: "Display",
          images: []
        },
        {
          postId: 2,
          postContent: "12412432131231231231231231",
          createdDate: "12/12/2293",
          status: "Display",
          images: []
        },
        {
          postId: 3,
          postContent: "124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsd124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdafaf",
          createdDate: "12/12/2203",
          status: "Hide",
          images: [
            new PostImage(1,3,"bamboo-watch.jpg")
          ]
        },
      ];


  }

  bindingDataPostModel(): Post {
      let result = new Post();
      // if (this.shareData && this.shareData.userInfo) {
        
      //     if (this.roleModel.id && this.roleModel.id > 0) {
      //         //Update
      //         result.id = this.roleModel.id;
      //         result.roleId = this.roleModel.roleId;
      //         result.roleKey = this.roleModel.roleKey;
      //         result.roleName = this.roleModel.roleName;
      //         result.roleDescribe = this.roleModel.roleDescribe;
      //         result.active = this.roleModel.active;
      //         result.lastModifiedBy = this.shareData.userInfo.userName;
      //         result.lastModifiedDate = new Date();
      //         result.createdBy = this.roleModel.createdBy;
      //         result.createdDate = this.roleModel.createdDate;

      //     } else {
      //         //Insert
      //         result.roleId = this.roleModel.roleId;
      //         result.roleKey = this.roleModel.roleKey;
      //         result.roleName = this.roleModel.roleName;
      //         result.roleDescribe = this.roleModel.roleDescribe;
      //         result.active = this.roleModel.active;
      //         result.createdBy = this.shareData.userInfo.userName;
      //         result.createdDate = new Date();
      //     }
      // }

      return result;
  }

  onCreatePost() {
      this.headerDialog = 'Add New Post';

      this.uploadedFiles = [];
      
      this.postModel = new Post();

      this.displayDialogEditAddPost = true;
  }

  onUpdatePost(post: Post) {
      this.headerDialog = `Edit Post ID: ${post.postId}`;

      this.postModel = post;

      //this.uploadedFiles = post.images;

      this.displayDialogEditAddPost = true;
  }

  onHidePost(post: Post, event) {
      this.confirmationService.confirm({
          target: event.target,
          message: `Are you sure to Hide this post id: ${post.postId} ?`,
          icon: 'pi pi-exclamation-triangle',
          accept: () => {
              //confirm action
              this.deletePost(post);
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
            this.deletePost(post);
        },
        reject: () => {
            //reject action
        }
    });
  }

  onSavePost() {
    console.log(this.uploadedFiles);
    let postEnity = this.bindingDataPostModel();

    if (this.validatePostModel()) {
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
      // if (!this.roleModel.roleKey || this.roleModel.roleKey == '') {
      //     this.showMessage(mType.warn, "Thông báo", "Mã phân quyền không được để trống. Vui lòng nhập!", 'notify');
      //     return false;
      // }

      // if (!this.roleModel.roleName || this.roleModel.roleName == '') {
      //     this.showMessage(mType.warn, "Thông báo", "Tên phân quyền không được để trống. Vui lòng nhập!", 'notify');
      //     return false;
      // }

      // if (!this.roleModel.roleId || this.roleModel.roleId == '') {
      //     this.showMessage(mType.warn, "Thông báo", "Mã nhóm phân quyền không được để trống. Vui lòng chọn!", 'notify');
      //     return false;
      // }

      return true;
  }

  async createPost(postEnity: Post) {
      try {
          let param = postEnity;

          // let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.QTHT, API.API_QTHT.INSERT_APP_ROLE, param, true);

          // if (response && response.success) {
          //     this.showMessage(mType.success, "Thông báo", "Thêm mới phân quyền thành công!", 'notify');

          //     this.displayDialogCreateRole = false;

          //     //lấy lại danh sách All Role
          //     this.getAllRole();

          //     //Clear Role model đã tạo
          //     this.roleModel = new AppRole();
          // } else {
          //     this.showMessage(mType.error, "Thông báo", "Thêm mới phân quyền không thành công. Vui lòng xem lại!", 'notify');
          // }
      } catch (e) {
          console.log(e);
      }
  }

  async updatePost(postEnity: Post) {
      try {
          let param = postEnity;

          // let response = await this.iServiceBase.putDataAsync(API.PHAN_HE.QTHT, API.API_QTHT.UPDATE_APP_ROLE, param, true);

          // if (response && response.success) {
          //     this.showMessage(mType.success, "Thông báo", "Cập nhật phân quyền thành công!", 'notify');

          //     this.displayDialogCreateRole = false;

          //     //lấy lại danh sách All Role
          //     this.getAllRole();

          //     //Clear Role model đã tạo
          //     this.roleModel = new AppRole();
          // } else {
          //     this.showMessage(mType.error, "Thông báo", "Cập nhật phân quyền không thành công. Vui lòng xem lại!", 'notify');
          // }
      } catch (e) {
          console.log(e);
      }
  }

  async deletePost(postEnity: Post) {
      try {
          let param = postEnity.postId;

          // let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.QTHT, API.API_QTHT.DELETE_APP_ROLE, param, true);

          // if (response && response.success) {
          //     this.showMessage(mType.success, "Thông báo", "Xóa phân quyền thành công!", 'notify');

          //     //lấy lại danh sách All Role
          //     this.getAllRole();

          // } else {
          //     this.showMessage(mType.error, "Thông báo", "Xóa phân quyền không thành công. Vui lòng xem lại!", 'notify');
          // }
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
          case 'Display':
            return 'success';
          case 'Hide':
            return 'warning';
          case 'Ban':
            return 'danger';
          default:
            return 'error';
      }
  }

  handleFileSelection(event :  FileSelectEvent) {
    console.log("select", event);
    
    this.uploadedFiles = event.currentFiles;
    console.log("uploadFiles", this.uploadedFiles);
  }

  handleFileRemoval(event :  FileRemoveEvent){
    console.log("remove", event.file.name);

    this.uploadedFiles = this.uploadedFiles.filter(f => f.name !== event.file.name);
    console.log("uploadFiles", this.uploadedFiles);
  }

  handleAllFilesClear(event :  Event){
    console.log("clear", event);

    this.uploadedFiles = [];
    console.log("uploadFiles", this.uploadedFiles);
  }

  onDisplayImagesDialog(post: Post, event: any){
    this.postImageDialog = post;
    this.visibleImageDialog = true;
  }

}
