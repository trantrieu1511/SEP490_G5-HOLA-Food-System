import {Component, OnInit, ViewChild} from '@angular/core';
import {Table} from "primeng/table";
import {AppBreadcrumbService} from "../../../../app-systems/app-breadcrumb/app.breadcrumb.service";
import {Food, FoodImage, Category} from "../../models/food.model";
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
  selector: 'app-food-management',
  templateUrl: './food-management.component.html',
  styleUrls: ['./food-management.component.scss']
})
export class FoodManagementComponent extends iComponentBase implements OnInit{
  
  lstFood: Food[] = [];
  // selectedPosts: Post[] = [];
  displayDialogEditAddFood: boolean = false;
  headerDialog: string = '';
  foodModel: Food = new Food();
  loading: boolean;
  uploadedFiles: File[] = [];

  contentDialog: string;
  visibleDescriptionDialog: boolean = false;

  foodImageDialog: Food = new Food();
  visibleImageDialog: boolean = false;

  lstCategory: Category[] = [];

  selectedCategory: Category = new Category();


  @ViewChild('dt') table: Table;

  constructor(public breadcrumbService: AppBreadcrumbService,
              private shareData: ShareData,
              public messageService: MessageService,
              private confirmationService: ConfirmationService,
              private iServiceBase: iServiceBase,) {
      super(messageService, breadcrumbService);
      this.getAllCategory();
  }

  ngOnInit() {
    this.getAllPost();
    
  }

  async getAllCategory(){
    // try {
    //     this.loading = true;

    //     let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.QTHT, API.API_QTHT.GET_ALL_ROLE);

    //     if (response && response.length) {
    //         this.lstCategory = response;
    //     }
    //     this.loading = false;
    // } catch (e) {
    //     console.log(e);
    //     this.loading = false;
    // }

    this.lstCategory = [
        {name: 'Food', categoryId: 1},
        {name: 'Drink', categoryId: 2}
    ];
  }

  async getAllPost() {
      this.lstFood = [];
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

      this.lstFood = [
        {
          foodId: 1,
          name: "gà",
          description: "abcaisefdojueiwf",
          categoryId: 1,
          categoryName: "Food",
          rating: 4,
          status: "Display",
          images: []
        },
        {
          foodId: 2,
          name: "gà chiên",
          description: "abcaisefdojueiwf",
          categoryId: 1,
          categoryName: "Food",
          rating: 5,
          status: "Display",
          images: []
        },
        {
          foodId: 3,
          name: "coca",
          description: "124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsd124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdaf124124fwfscsdefwefewcsdceds32131231231231231231fasefsfsfasfesfasf32rf2w3f23f23f23f23qf23qfw23f23wfwefsdafaf",
          categoryId: 2,
          categoryName: "Drink",
          rating: 0,
          status: "Hide",
          images: [
            new FoodImage(1,3,"bamboo-watch.jpg")
          ]
        },
      ];


  }

  bindingDataFoodModel(): Food {
      let result = new Food();
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

  onCreateFood() {
      this.headerDialog = 'Add New Food';

      this.uploadedFiles = [];
      
      this.foodModel = new Food();

      this.selectedCategory = new Category();

      this.displayDialogEditAddFood = true;
  }

  onUpdateFood(food: Food) {
      this.headerDialog = `Edit Food ID: ${food.foodId}`;

      this.foodModel = food;

      this.selectedCategory = new Category();
      this.selectedCategory.categoryId = food.categoryId;
      this.selectedCategory.name = food.categoryName;

      //this.uploadedFiles = post.images;

      this.displayDialogEditAddFood = true;
  }

  onHideFood(food: Food, event) {
      this.confirmationService.confirm({
          target: event.target,
          message: `Are you sure to Hide this food id: ${food.foodId} ?`,
          icon: 'pi pi-exclamation-triangle',
          accept: () => {
              //confirm action
              this.deleteFood(food);
          },
          reject: () => {
              //reject action
          }
      });
  }

  
  onDisplayFood(food: Food, event) {
    this.confirmationService.confirm({
        target: event.target,
        message: `Are you sure to Display this food id: ${food.foodId} ?`,
        icon: 'pi pi-exclamation-triangle',
        accept: () => {
            //confirm action
            this.deleteFood(food);
        },
        reject: () => {
            //reject action
        }
    });
  }

  onSaveFood() {
    console.log(this.foodModel);
    this.foodModel.categoryId = this.selectedCategory.categoryId;
    this.foodModel.name = this.selectedCategory.name;

    let foodEnity = this.bindingDataFoodModel();

    if (this.validateFoodModel()) {
        if (foodEnity && foodEnity.foodId && foodEnity.foodId > 0) {
            this.updateFood(foodEnity);
        } else {
            this.createFood(foodEnity);
        }
    }
  }

  onCancelFood() {
      this.foodModel = new Food();

      this.displayDialogEditAddFood = false;
  }

  validateFoodModel(): boolean {
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

  async createFood(foodEnity) {
      try {
          let param = foodEnity;

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

  async updateFood(foodEnity) {
      try {
          let param = foodEnity;

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

  async deleteFood(foodEnity: Food) {
      try {
          let param = foodEnity.foodId;

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

  viewContentDetail(food: Food){
    this.contentDialog = food.description;
    this.visibleDescriptionDialog = true;
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

  onDisplayImagesDialog(food: Food, event: any){
    this.foodImageDialog = food;
    this.visibleImageDialog = true;
  }
}
