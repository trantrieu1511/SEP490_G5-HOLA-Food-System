import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {
    iComponentBase,
    iServiceBase, mType,
    ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../../services/apiURL";
import {
    ConfirmationService,
    MessageService,
    SelectItem,
} from "primeng/api";
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from 'src/app/services/data.service';
import { MenuInput } from '../../models/menuInput.model';
import { AddToCart } from '../../models/addToCart.model';
import { DataView } from 'primeng/dataview';
import { GetShopInforInputDto } from '../../models/GetShopInforInputDto.model';
import { PostReport } from '../../models/postreport.model';
import { AuthService } from 'src/app/services/auth.service';
import { FileRemoveEvent, FileSelectEvent } from 'primeng/fileupload';
import { SellerReport } from '../../models/sellerReport.model';
import { take } from 'rxjs';
import { PresenceService } from 'src/app/services/presence.service';


@Component({
  selector: 'app-shopdetail',
  templateUrl: './shopdetail.component.html',
  styleUrls: ['./shopdetail.component.scss']
})
export class ShopdetailComponent extends iComponentBase implements OnInit, AfterViewInit {
  foods: any[];
   sortOptions: SelectItem[];
  sortOrder: number;
  loading: boolean;
  sortField: string;
  isLoggedIn:boolean =false;
	menuInput = new MenuInput();
  shopid : string;
  userId: string;
  shopInfor : any
  isVisiblePostReportModal = false; // Bien phuc vu cho viec bat tat modal post report
  sellerReport: SellerReport = new SellerReport();
  isDisabledPostReportBtnSubmit: boolean = true; // Trạng thái disable của nút submit của modal post report
  isDisabledPostReportTextArea: boolean = true; // Trạng thái disable của text area của modal post report
  uploadedFiles: File[] = [];
  vouchers : any;
  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private route: ActivatedRoute,
    private router: Router,
    public presence: PresenceService,
    private dataService: DataService,
    private authService: AuthService
  ){
    super(messageService);
  }
  ngAfterViewInit(): void {

  }
  checkLoggedIn() {
    // if (sessionStorage.getItem('userId') != null) {
    //   this.isLoggedIn = true;
    // }
    const user = this.authService.getUserInfor();
    if(!user){
      this.isLoggedIn = false;
      return;
    }

    this.userId = user.userId;
    if (this.userId != null) {
      this.isLoggedIn = true;
    }
  }

  async ngOnInit(){
    this.checkLoggedIn();
    this.route.queryParams
    .subscribe(params => {
      this.shopid = params['shopid'];
      // console.log(id);
      // Sử dụng giá trị id tại đây
      this.menuInput.shopId = this.shopid
      console.log(this.menuInput)
      this.getMenu(this.menuInput)
      this.getShopInfor()
    });

    this.sortOptions = [
      { label: 'Price High to Low', value: '!unitPrice' },
      { label: 'Price Low to High', value: 'unitPrice' }
  ];

  }

  onSortChange(event: any) {
    const value = event.value;

    if (value.indexOf('!') === 0) {
        this.sortOrder = -1;
        this.sortField = value.substring(1, value.length);
    } else {
        this.sortOrder = 1;
        this.sortField = value;
    }
}

  async getMenu(menuInput : MenuInput){
    try {
        this.loading = true;

        let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.SHOP_DETAIL, API.API_SHOP_DETAIL.DISPLAY_MENU, menuInput);
        if (response && response.success == true) {
            this.foods = response.listFood;
        }

		    console.log(this.foods);
        this.loading = false;
    } catch (e) {
        console.log(e);
        this.loading = false;
    }
  }

  async getVoucher(){
    try {
        this.loading = true;

        const param = {
          "sellerId":this.userId
      };
        let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.VOUCHER, API.API_VOUCHER.GET_ALL_VOUCHER, param);
        if (response && response.success == true) {
            this.vouchers = response.listVoucher;
        }
        this.loading = false;
    } catch (e) {
        console.log(e);
        this.loading = false;
    }
  }

  async getShopInfor(){
    try {
        this.loading = true;
        let shopInforInput = new GetShopInforInputDto();
        shopInforInput.ShopId = this.shopid
        let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.SHOP_DETAIL, API.API_SHOP_DETAIL.DISPLAY_INFOR, shopInforInput);
        if (response && response.success === true) {
            this.shopInfor = response;
        }

		console.log(this.foods);
        this.loading = false;
    } catch (e) {
        console.log(e);
        this.loading = false;
    }
  }

  onFilter(dv: DataView, event: Event) {
    dv.filter((event.target as HTMLInputElement).value);
}

  async onAddToCart(foodId : number){
    try {
      this.loading = true;
      let cartItem = new AddToCart();
      cartItem.foodId = foodId
      cartItem.amount = 1
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.CART, API.API_CART.ADDTOCART, cartItem);
      if (response && response.message === "Success") {
        console.log(response)
          this.showMessage(mType.success, "", "Add to cart success!", 'notify');
      }
      else{
        this.showMessage(mType.warn, "", "You are not logged as customer!", 'notify');
        this.router.navigate(['/login']);
      }

      this.loading = false;
  } catch (e) {
      console.log(e);
      this.loading = false;
  }
  }

  onFoodDetail(foodId : number){
    this.router.navigate(['/fooddetail'], { queryParams: { foodId: foodId } });
  }


  enableDisableReportTextArea($event: any) {
    if ($event.checked == 'Other') {
      this.isDisabledPostReportTextArea = false;
    } else {
      this.isDisabledPostReportTextArea = true;
    }
  }

  enableDisableReportButtonSubmit() {
    // ;
    //console.log(this.postReport.reportContents);
    //console.log("rpContents length: " + this.postReport.reportContents.length);
    if (this.sellerReport.reportContents.length < 1) {
      this.isDisabledPostReportBtnSubmit = true;
    } else {
      this.isDisabledPostReportBtnSubmit = false;
    }
  }

  addValueToReportContentList() {
    console.log(this.sellerReport.reportContent);
    if (this.sellerReport.reportContent == '') {
      this.sellerReport.reportContents.pop();
      console.log("poped!");
    } else {
      this.sellerReport.reportContents.push(this.sellerReport.reportContent);
      console.log("pushed!");
    }
    this.enableDisableReportButtonSubmit();
  }
  async submitReport() {
    //------------ Lay cac ly do report duoc input boi nguoi dung -------------
    let rpContent: string = "";
    let i = 0;
    // ;
    console.log(this.sellerReport.reportContents);
    this.sellerReport.reportContents.forEach(element => {
      i++;
      console.log("element" + i + ": " + element);
      if (element == this.sellerReport.reportContents[this.sellerReport.reportContents.length - 1]) { //the last element in the array
        rpContent += element;
      } else {
        rpContent += element + ", ";
      }
    });
    // rpContent += ", " + this.postReport.reportContent;
   //
    this.sellerReport.reportContent = rpContent;
    console.log("Full rp content: " + this.sellerReport.reportContent);
   //
    // ------------------ Commit vao db --------------------
    try {
     this.sellerReport.images=this.uploadedFiles;
    const param22 = new FormData();
this.loading = true;
param22.append('sellerId',   this.sellerReport.sellerId);
param22.append('reportContent',   this.sellerReport.reportContent);
this.uploadedFiles.forEach(file => {
  param22.append('images', file, file.name);
});

// Object.keys(this.sellerReport).forEach(function (key) {
//   param.append(key, this.sellerReport[key]);
// });

   //
      let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_USER.REPORT_SELLER, param22);
      if (response && response.success === true) {
        this.showMessage(mType.success, "Notification", `Report the food successfully`, 'notify');
        console.log(response);
        console.log('Create new food report successfully');
      }
      else {
        // this.showMessage(mType.warn, "Error", this.iServiceBase.formatMessageError(response.message), 'notify');
        this.showMessage(mType.warn, "Error", response.message, 'notify');
        // console.log(response);
        // console.log('Internal server error, please contact for admin help!');
      }
      this.loading = false;
      //this.checkUsersReportPostCapability();
    } catch (e) {
      console.log(e);
      this.loading = false;
     // this.checkUsersReportPostCapability();
    }

    // Làm mới model để không bị ảnh hường bởi two way binding
    this.sellerReport = new SellerReport();
    // Tat modal
    this.isVisiblePostReportModal = false;
    // Refresh nut submit
    this.enableDisableReportButtonSubmit();
  }
  openPostReportDialog(sellerId: string) {
    this.sellerReport = new SellerReport();
    this.enableDisableReportButtonSubmit(); // reset nut submit

    this.sellerReport.sellerId = sellerId;
    this.isVisiblePostReportModal = true;
    // event.preventDefault();
  }
  handleFileSelection(event: FileSelectEvent) {
    //console.log("select", event);

    this.uploadedFiles = event.currentFiles;

    //console.log('primeSelect',this.f_upload);
    console.log("uploadFiles", this.uploadedFiles);
  }

  handleFileRemoval(event: FileRemoveEvent) {
    console.log("remove", event.file.name);

    this.uploadedFiles = this.uploadedFiles.filter(f => f.name !== event.file.name);
    console.log("uploadFiles", this.uploadedFiles);
  }

  handleAllFilesClear(event: Event) {
    //console.log("clear", event);

    this.uploadedFiles = [];
    console.log("uploadFiles", this.uploadedFiles);
  }

}
