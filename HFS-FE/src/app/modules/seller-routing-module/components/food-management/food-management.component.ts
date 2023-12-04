import { Component, OnInit, ViewChild } from '@angular/core';
import { Table } from "primeng/table";
import { AppBreadcrumbService } from "../../../../app-systems/app-breadcrumb/app.breadcrumb.service";
import { Food, FoodInput, FoodDisplayHideInputDto, FoodInputValidation } from "../../models/food.model";
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
import {FormBuilder, FormGroup, Validators} from '@angular/forms'
import { User } from 'src/app/services/auth.service';
import { PresenceService } from 'src/app/services/presence.service';
import { Category } from 'src/app/modules/admin-routing-module/models/Category';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'food-seller',
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
              public presence: PresenceService,
              public translate: TranslateService
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


  ngOnInit() {
    this.getAllFood();
    this.getAllCategory();
    //console.log(this.foodValidation);
   // this.setCurrentUser();
  }
  // setCurrentUser(){
  //   const user: User = JSON.parse(localStorage.getItem('user'));
  //   const token=sessionStorage.getItem('JWT');
  //   if(user){

  //     this.presence.createHubConnection(token);
  //   }
  // }

  async getAllCategory() {
    this.lstCategory = [];

    try {
        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_MANAGE.LIST_CATEGORY,"");

        if (response && response.message === "Success") {
            this.lstCategory = response.listCategory;

        }
       ;
    } catch (e) {
        console.log(e);

    }

    // this.lstCategory = [
    //   { name: 'Food', categoryId: 1 },
    //   { name: 'Drink', categoryId: 2 }
    // ];
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
    this.foodModel.categoryId = this.selectedCategory != null ?  this.selectedCategory.categoryId : null;

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

      if(!this.foodModel.description || this.foodModel.description == ''){
        this.foodValidation.isDescriptionValid = false;
        this.foodValidation.descriptionMessage = "Description can not empty";
        check = false;
      }

      if(!this.foodModel.categoryId || this.foodModel.categoryId < 1 ){
        this.foodValidation.isCategoryIdValid = false;
        this.foodValidation.categoryIdMessage = "Category must be choose";
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

  async deleteFood(foodEnity: Food, type: boolean) {
    // type = true => Display
    // false => Hide

    const message = type ? "Displayed" : "Hidden";
    try {
      let param = new FoodDisplayHideInputDto();
      param.foodId = foodEnity.foodId;
      param.type = type;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.FOOD, API.API_FOOD.ENABLE_DISABLE, param, true);

      if (response && response.message === "Success") {
        this.showMessage(mType.success, "Notification", `${message} foodId: ${foodEnity.foodId} successfully`, 'notify');

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
