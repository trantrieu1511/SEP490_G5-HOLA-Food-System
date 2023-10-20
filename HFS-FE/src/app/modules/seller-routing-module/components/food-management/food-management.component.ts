import {Component, OnInit, ViewChild} from '@angular/core';
import {Table} from "primeng/table";
import {AppBreadcrumbService} from "../../../../app-systems/app-breadcrumb/app.breadcrumb.service";
import {Food, Category, FoodInput, FoodDisplayHideInputDto} from "../../models/food.model";
import {
    iComponentBase,
    iServiceBase, mType,
    ShareData,
    iFunction
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
import { FileRemoveEvent, FileSelectEvent, FileUpload } from 'primeng/fileupload';

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
  //@ViewChild('fileUploader') f_upload: FileUpload;

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
    this.getAllFood();
    this.getAllCategory();
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

  async getAllFood() {
    this.lstFood = [];
    this.uploadedFiles = [];
    try {
        this.loading = true;

        let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.FOOD, API.API_FOOD.GET_FOOD_SELLER);

        if (response && response.message === "Success") {
            this.lstFood = response.foods;
        }
        this.loading = false;
    } catch (e) {
        console.log(e);
        this.loading = false;
    }


  }

  bindingDataFoodModel(): FoodInput {
      let result = new FoodInput();
      
      if (this.foodModel.foodId && this.foodModel.foodId > 0) {
        // //Update
        result.foodId = this.foodModel.foodId;
        result.name = this.foodModel.name;
        result.unitPrice = this.foodModel.unitPrice;
        result.description = this.foodModel.description;
        result.categoryId = this.foodModel.categoryId;

      } else {
        //Insert
        result.name = this.foodModel.name;
        result.unitPrice = this.foodModel.unitPrice;
        result.description = this.foodModel.description;
        result.categoryId = this.foodModel.categoryId;
      }
    
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
    //this.f_upload.files = [];
    this.uploadedFiles = [];
    this.foodModel = food;

    this.selectedCategory = new Category();
    this.selectedCategory.categoryId = food.categoryId;
    this.selectedCategory.name = food.categoryName;

    food.imagesBase64.forEach(image => {
      var fileimage = this.iFunction.convertImageBase64toFile(image.imageBase64, image.name);
      this.uploadedFiles.push(fileimage);
      //this.f_upload.files.push(fileimage);
    });

    //console.log('prime',this.f_upload);

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
        this.deleteFood(food, false);
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
        this.deleteFood(food, true);
      },
      reject: () => {
        //reject action
      }
    });
  }

  onSaveFood() {
    //console.log(this.foodModel);
    this.foodModel.categoryId = this.selectedCategory.categoryId;
    document.body.style.cursor = 'wait';
    let foodEntity = this.bindingDataFoodModel();

    if (this.validateFoodModel()) {
      document.body.style.cursor = 'wait';
      if (foodEntity && foodEntity.foodId && foodEntity.foodId > 0) {
          this.updateFood(foodEntity);
      } else {
          this.createFood(foodEntity);
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

  async createFood(foodEnity: FoodInput) {
      try {
        const param = new FormData();

        this.uploadedFiles.forEach(file => {
          param.append('images', file, file.name);
        });

        Object.keys(foodEnity).forEach(function(key) {
          param.append(key, foodEnity[key]);
        });
        //console.log(foodEnity);
        //console.log(param);

        const response = await this.iServiceBase
          .postDataAsync(API.PHAN_HE.FOOD, API.API_FOOD.ADD_FOOD, param, true);
      
        if(response && response.message === "Success"){
          this.showMessage(mType.success, "Notification", "New food added successfully", 'notify');

          this.displayDialogEditAddFood = false;

          //lấy lại danh sách All 
          this.getAllFood();

          //Clear model đã tạo
          this.foodModel = new Food();
          //clear file upload too =))
          this.uploadedFiles = [];
        } 
      } catch (e) {
          console.log(e);
      }
  }

  async updateFood(foodEnity) {
      try {
        const param = new FormData();

        this.uploadedFiles.forEach(file => {
          param.append('images', file, file.name);
        });

        Object.keys(foodEnity).forEach(function(key) {
          param.append(key, foodEnity[key]);
        });

        let response = await this.iServiceBase
            .putDataAsync(API.PHAN_HE.FOOD, API.API_FOOD.UPDATE_FOOD, param, true);

        if (response && response.success) {
          this.showMessage(mType.success, "Notification"
            , "New food updated successfully", 'notify');

          this.displayDialogEditAddFood = false;

          //lấy lại danh sách All 
          this.getAllFood();

          //Clear model đã tạo
          this.foodModel = new Food();
          //clear file upload too =))
          this.uploadedFiles = [];
        } else {
          this.showMessage(mType.error, "Notification"
            , "New food updated failed", 'notify');
        }
      } catch (e) {
          console.log(e);
      }
  }

  async deleteFood(foodEnity: Food, type: boolean) {
    // type = true => Display
    // false => Hide

    const message = type ? "Displayed" : "Hidden";
    try {
      let param = new FoodDisplayHideInputDto();
      param.foodId = foodEnity.foodId;
      param.type = type;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.FOOD, API.API_FOOD.ENABLE_DISABLE, param, true);

      if (response && response.success) {
        this.showMessage(mType.success, "Notification", `${message} foodId: ${foodEnity.foodId} successfully`, 'notify');

        //lấy lại danh sách All Role
        this.getAllFood();

      } else {
        this.showMessage(mType.warn, "Notification", response.message, 'notify');
      }
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

    //console.log('primeSelect',this.f_upload);
    //console.log("uploadFiles", this.uploadedFiles);
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

  onDisplayImagesDialog(food: Food, event: any){
    this.foodImageDialog = food;
    this.visibleImageDialog = true;
  }

  onHideDialogEditAdd(){
    this.uploadedFiles = [];
  }
}
