import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { Table } from "primeng/table";
import { AppBreadcrumbService } from "../../../../app-systems/app-breadcrumb/app.breadcrumb.service";
import { Food, Category, FoodInput, FoodDisplayHideInputDto, FoodInputValidation, FoodBanUnbanInputDto } from "../../models/food.model";
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
import { FormBuilder, FormGroup, Validators } from '@angular/forms'
import { Router } from '@angular/router';
import { DataRealTimeService } from 'src/app/services/SignalR/data-real-time.service';
import { AuthService } from 'src/app/services/auth.service';

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

  foodForm: FormGroup;

  foodValidation: FoodInputValidation = new FoodInputValidation();

  constructor(public breadcrumbService: AppBreadcrumbService,
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private fb: FormBuilder,
    private router: Router,
    private signalRService: DataRealTimeService,
    private authService: AuthService
  ) {
    super(messageService, breadcrumbService);
    this.foodForm = this.fb.group({
      foodId: ['', Validators.required],
      name: ['', Validators.required],
      unitPrice: ['', Validators.required],
      description: ['', Validators.required],
      categoryId: ['', Validators.required]
    });
  }

  // Not allow the user to access the page if they are not menu moderator
  checkUserAccessPermission() {
    //let userRoleName = sessionStorage.getItem("userId").substring(0, 2);
    let userRoleName = this.authService.getUserInfor().userId.substring(0, 2);
    if (userRoleName !== "MM") {
      this.router.navigateByUrl('/HFSBusiness');
      alert('You cannot access this page unless you are a menu moderator');
    }
  }

  async ngOnInit() {
    this.checkUserAccessPermission();
    this.getAllFood();
    this.getAllCategory();
    this.connectSignalR();
    //console.log(this.foodValidation);
  }

  async connectSignalR() {
    this.lstFood = [];
    this.signalRService.startConnection();
    const res = await this.signalRService.addTransferDataListener(
      'foodDataRealTime',
      API.PHAN_HE.FOOD, 
      API.API_FOOD.GET_FOOD
    );
    if (res && res.message === 'Success') {
      this.lstFood = res.foods;
    }
  }

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any): void {
    this.signalRService.stopConnection();
  }

  async getAllCategory() {
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
      { name: 'Food', categoryId: 1 },
      { name: 'Drink', categoryId: 2 }
    ];
  }

  async getAllFood() {
    this.lstFood = [];
    this.uploadedFiles = [];
    try {
      this.loading = true;

      // let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.FOOD, API.API_FOOD.GET_FOOD_SELLER);
      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.FOOD, API.API_FOOD.GET_FOOD);

      if (response && response.message === "Success") {
        this.lstFood = response.foods;
        console.log(this.lstFood);
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

    this.selectedCategory = null;

    this.displayDialogEditAddFood = true;
  }

  onUpdateFood(food: Food) {
    this.headerDialog = `Edit Food ID: ${food.foodId}`;
    //this.f_upload.files = [];
    this.uploadedFiles = [];
    this.foodModel = Object.assign({}, food);

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

  onBanFood(food: Food, event){
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure to Ban this food id: ${food.foodId}?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        //confirm action
        this.banUnbanFood(food, true);
      },
      reject: () => {
        //reject action
      }
    });
  }

  onUnbanFood(food: Food, event){
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure to Unban this food id: ${food.foodId}?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        //confirm action
        this.banUnbanFood(food, false);
      },
      reject: () => {
        //reject action
      }
    });
  }

  onDisplayFood(food: Food, event) { // Approve the food for display
    this.confirmationService.confirm({
      target: event.target,
      message: `Are you sure to Display this food id: ${food.foodId} to the guests and customers?`,
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        //confirm action
        this.enableDisableFood(food, true);
      },
      reject: () => {
        //reject action
      }
    });
  }

  onSaveFood() {
    //console.log(this.foodModel);
    this.foodModel.categoryId = this.selectedCategory != null ? this.selectedCategory.categoryId : null;

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
    var check = true;
    this.foodValidation = new FoodInputValidation();
    if (!this.foodModel.name || this.foodModel.name == '') {
      this.foodValidation.isNameValid = false;
      this.foodValidation.nameMessage = "Food name can not empty";
      check = false;
    }

    if (!this.foodModel.unitPrice || this.foodModel.unitPrice == '') {
      this.foodValidation.isUnitPriceValid = false;
      this.foodValidation.unitPriceMessage = "UnitPrice can not empty";
      check = false;
    }

    if (parseInt(this.foodModel.unitPrice) < 0) {
      this.foodValidation.isUnitPriceValid = false;
      this.foodValidation.unitPriceMessage = "UnitPrice must be >= 0";
      check = false;
    }

    if (!this.foodModel.description || this.foodModel.description == '') {
      this.foodValidation.isDescriptionValid = false;
      this.foodValidation.descriptionMessage = "Description can not empty";
      check = false;
    }

    if (!this.foodModel.categoryId || this.foodModel.categoryId < 1) {
      this.foodValidation.isCategoryIdValid = false;
      this.foodValidation.categoryIdMessage = "Category must be choose";
      check = false;
    }


    return check;
  }

  async createFood(foodEnity: FoodInput) {
    const param = new FormData();

    this.uploadedFiles.forEach(file => {
      param.append('images', file, file.name);
    });

    Object.keys(foodEnity).forEach(function (key) {
      param.append(key, foodEnity[key]);
    });
    const response = await this.iServiceBase
      .postDataAsync(API.PHAN_HE.FOOD, API.API_FOOD.ADD_FOOD, param, true);
    //console.log(response);
    if (response && response.message === "Success") {
      this.showMessage(mType.success, "Notification", "New food added successfully", 'notify');

      this.displayDialogEditAddFood = false;

      //lấy lại danh sách All 
      this.getAllFood();

      //Clear model đã tạo
      this.foodModel = new Food();
      //clear file upload too =))
      this.uploadedFiles = [];
    } else {
      var messageError = this.iServiceBase.formatMessageError(response);
      console.log(messageError);
      this.showMessage(mType.error, response.message, messageError, 'notify');
    }
  }

  async updateFood(foodEnity) {
    try {
      const param = new FormData();

      this.uploadedFiles.forEach(file => {
        param.append('images', file, file.name);
      });

      Object.keys(foodEnity).forEach(function (key) {
        param.append(key, foodEnity[key]);
      });

      let response = await this.iServiceBase
        .putDataAsync(API.PHAN_HE.FOOD, API.API_FOOD.UPDATE_FOOD, param, true);

      if (response && response.message === "Success") {
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
        var messageError = this.iServiceBase.formatMessageError(response)
        this.showMessage(mType.error, response.message, messageError, 'notify');
      }
    } catch (e) {
      console.log(e);
    }
  }

  async enableDisableFood(foodEnity: Food, type: boolean) {
    // type = true => Display
    // false => Hide

    const message = type ? "Displayed" : "Hidden";
    try {
      let param = new FoodDisplayHideInputDto();
      param.foodId = foodEnity.foodId;
      param.type = type;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.FOOD, API.API_FOOD.ENABLE_DISABLE, param, true);

      if (response && response.message === "Success") {
        this.showMessage(mType.success, "Notification", `Successfully approved foodId: ${foodEnity.foodId} to be displayed to public`, 'notify');
        console.log(`Successfully approved foodId: ${foodEnity.foodId}`);
        //lấy lại danh sách All Role
        this.getAllFood();

      } else {
        var messageError = this.iServiceBase.formatMessageError(response);
        console.log(messageError);
        this.showMessage(mType.error, response.message, messageError, 'notify');
      }
    } catch (e) {
      console.log(e);
    }
  }
  
  async banUnbanFood(foodEnity: Food, isBanned: boolean) {
    // type = true => Display
    // false => Hide

    const message = isBanned ? "Banned" : "Unbanned";
    try {
      let param = new FoodBanUnbanInputDto();
      param.foodId = foodEnity.foodId;
      param.isBanned = isBanned;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.FOOD, API.API_FOOD.BAN_UNBAN, param, true);

      if (response && response.message === "Success") {
        this.showMessage(mType.success, "Notification", `${message} foodId: ${foodEnity.foodId} successfully`, 'notify');
        console.log(`${message} foodId: ${foodEnity.foodId} successfully`);
        //lấy lại danh sách All Role
        this.getAllFood();

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

  viewContentDetail(food: Food) {
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

  handleFileSelection(event: FileSelectEvent) {
    //console.log("select", event);

    this.uploadedFiles = event.currentFiles;

    //console.log('primeSelect',this.f_upload);
    //console.log("uploadFiles", this.uploadedFiles);
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

  onDisplayImagesDialog(food: Food, event: any) {
    this.foodImageDialog = food;
    this.visibleImageDialog = true;
  }

  onHideDialogEditAdd() {
    this.uploadedFiles = [];
  }

}
