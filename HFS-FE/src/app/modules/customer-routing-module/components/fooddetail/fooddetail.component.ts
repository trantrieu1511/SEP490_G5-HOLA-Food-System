import { Component, OnInit, ViewChild } from '@angular/core';
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
import { Table } from 'primeng/table';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from 'src/app/services/data.service';
import { PresenceService } from 'src/app/services/presence.service';
import { GetFoodDetail } from '../../models/getFoodDetail.model';
import { AddToCart } from '../../models/addToCart.model';
import { GetFoodByCategoryInpt } from '../../models/GetFoodByCategoryInput.model';
import { VoteFeedBackInputDto } from '../../models/VoteFeedBackInputDto.model';
import { TabView, TabViewChangeEvent } from 'primeng/tabview';
import { FoodReport } from 'src/app/modules/menumoderator-routing-module/models/foodreport.model';
import { CheckboxChangeEvent } from 'primeng/checkbox';
interface PageEvent {
  first?: number;
  rows?: number;
  page?: number;
  pageCount?: number;
}
@Component({
  selector: 'app-fooddetail',
  templateUrl: './fooddetail.component.html',
  styleUrls: ['./fooddetail.component.scss']
})
export class FooddetailComponent extends iComponentBase implements OnInit {

  constructor(private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private _router: Router,
    private _route: ActivatedRoute,
    private dataService: DataService,
    public presence: PresenceService
  ) {
    super(messageService);
  }
  foodId : number
  loading: boolean;
  fooddetail: any;
  foodImages: any[]
  amount: number = 1
  similarFood: any[]
  feedbacks: any[]
  displayFeedback: any[]
  category: number
  first: number = 0;
  rows: number = 10;

  // ----------- Food report --------------
  isVisibleFoodReportModal = false; // Bien phuc vu cho viec bat tat modal
  foodReport: FoodReport = new FoodReport();
  isDisabledFoodReportBtnSubmit: boolean = true; // Trạng thái disable của nút submit của modal food report
  isDisabledFoodReportTextArea: boolean = true; // Trạng thái disable của text area của modal food report
  isLoggedIn: boolean = false; // Trang thai da login cua nguoi dung
  listFoodReport: any[] = [];
  hasAlreadyReported: boolean = false; // Bien de check xem food nay da duoc report chua

  checkLoggedIn() {
    if (sessionStorage.getItem('userId') != null) {
      this.isLoggedIn = true;
    }
  }

  async ngOnInit() {
    this._route.queryParams.subscribe(params => {
      this.foodId = params['foodId'];})
    await this.getFoodDetail();
    await this.onGetSimilarFood();
    await this.getFeedBack();

    this.checkLoggedIn();
    this.checkUsersReportFoodCapability();
    if(!this.hasAlreadyReported){ // Neu food nay chua bi report thi moi reset nut submit (Tiet kiem tai nguyen may tinh)
      this.enableDisableFoodReportButtonSubmit();
    }
  }

  onPageChange(event: PageEvent, star: number) {
    this.first = event.first;
    this.rows = event.rows;
    this.displayFeedback = this.feedbacks.filter(x => x.star === star).slice(event.page, event.rows);
    console.log(this.displayFeedback)
  }

  onTabChange(event: TabViewChangeEvent) {
    let index = event.index
    switch (index) {
      case 0:
        this.displayFeedback = this.feedbacks.filter(x => x.star === 5);
        break;
      case 1:
        this.displayFeedback = this.feedbacks.filter(x => x.star === 4);
        break;
      case 2:
        this.displayFeedback = this.feedbacks.filter(x => x.star === 3);
        break;
      case 3:
        this.displayFeedback = this.feedbacks.filter(x => x.star === 2);
        break;
      case 4:
        this.displayFeedback = this.feedbacks.filter(x => x.star === 1);
        break;
      default:
        this.displayFeedback = this.feedbacks.filter(x => x.star === 5);
        break;
    }
  }



  async onReply(feedbackId: number) {
    const replyForm = document.getElementById(`${feedbackId}`);
    if (replyForm) {
      replyForm.classList.toggle('d-none');
    }
  }

  async onCancel(feedbackId: number) {
    const replyForm = document.getElementById(`${feedbackId}`);
    if (replyForm && !replyForm.classList.contains('d-none')) {
      replyForm.classList.add('d-none');
    }
  }

  async onLikeReview(feedbackId: number) {
    try {
      debugger
      this.loading = true;
      let vote = new VoteFeedBackInputDto();
      vote.FeedBackId = feedbackId
      let voteStatus = this.feedbacks.find(x => x.feedbackId === feedbackId)
      if (voteStatus) {
        vote.IsLike = !voteStatus.isLiked;
      } else {
        this.showMessage(mType.warn, "", "There was some problem please try againg or contact for admin help!", 'notify');
      }

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.FOODETAIL, API.API_FOODDETAIL.VOTE_FEEDBACK, vote);
      if (response && response.success === true) {

        this.getFeedBack();
      }
      this.loading = false;
    } catch (e) {
      this.showMessage(mType.warn, "", "There was some problem please try againg or contact for admin help!", 'notify');
      this.loading = false;
    }
  }

  async getFeedBack() {
    try {
      this.loading = true;
      let getfood = new GetFoodDetail();
      getfood.foodId = this.foodId
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_FOODDETAIL.GET_FEEDBACK_IMAGE, getfood);
      if (response && response.success === true) {
        this.feedbacks = response.feedBacks
        debugger
        this.displayFeedback = this.feedbacks.filter(x => x.star === 5).slice(this.first, this.rows);
      }
      this.loading = false;
    } catch (e) {
      this.showMessage(mType.warn, "", "There was some problem please try againg or contact for admin help!", 'notify');
      this.loading = false;
    }
  }

  async getFoodDetail() {
    try {
      this.loading = true;
      let getfood = new GetFoodDetail();
      getfood.foodId = this.foodId
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.FOODETAIL, API.API_FOODDETAIL.GET_FOOD, getfood);
      debugger
      if (response && response.success === true) {
        this.fooddetail = response;
        console.log(response);
        console.log(this.fooddetail);
        this.foodReport.foodId = response.foodId;
        this.foodImages = response.foodImages
        this.category = response.categoryId
      }
      this.loading = false;
    } catch (e) {
      this.showMessage(mType.warn, "", "There was some problem please try againg or contact for admin help!", 'notify');
      this.loading = false;
    }
  }

  async onAddToCart(foodId: number) {
    try {
      this.loading = true;
      let cartItem = new AddToCart();
      cartItem.foodId = foodId
      cartItem.amount = this.amount
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.CART, API.API_CART.ADDTOCART, cartItem);
      if (response && response.message === "Success") {
        console.log(response)
        this.showMessage(mType.success, "", "Add to cart success!", 'notify');
      }
      else {
        this.showMessage(mType.warn, "", "You are not logged as customer!", 'notify');
        this._router.navigate(['/login']);
      }

      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  async onGetSimilarFood() {
    try {
      this.loading = true;
      let getSimilarFood = new GetFoodByCategoryInpt()
      console.log(this.fooddetail)
      getSimilarFood.categoryId = this.category
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.FOODETAIL, API.API_FOODDETAIL.GET_SIMILARFOOD, getSimilarFood);
      if (response && response.success === true) {
        console.log(response)
        this.similarFood = response.listFood
      }
      else {
        this.showMessage(mType.warn, "", "There was some problem please try againg or contact for admin help!", 'notify');
      }

      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  openFoodReportDialog() {
    this.isVisibleFoodReportModal = true;
    event.preventDefault();
  }

  async submitReport() {
    //------------ Lay cac ly do report duoc input boi nguoi dung -------------
    let rpContent: string = "";
    let i = 0;
    // 
    console.log(this.foodReport.reportContents);
    this.foodReport.reportContents.forEach(element => {
      i++;
      console.log("element" + i + ": " + element);
      if (element == this.foodReport.reportContents[this.foodReport.reportContents.length - 1]) { //the last element in the array
        rpContent += element;
      } else {
        rpContent += element + ", ";
      }
    });
    // rpContent += ", " + this.foodReport.reportContent;

    this.foodReport.reportContent = rpContent;
    console.log("Full rp content: " + this.foodReport.reportContent);

    // ------------------ Commit vao db --------------------
    try {
      this.loading = true;
      let param = {
        foodId: this.foodReport.foodId,
        reportContent: this.foodReport.reportContent
      }
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.FOODREPORT, API.API_FOODREPORT.CREATE_NEW_FOODREPORT, param);
      if (response && response.success === true) {
        this.showMessage(mType.success, "Notification", `Report the food successfully`, 'notify');
        console.log(response);
        console.log('Create new food report successfully');
        this.checkUsersReportFoodCapability();
      }
      else {
        this.showMessage(mType.warn, "error", "Internal server error, please contact for admin help!", 'notify');
        console.log(response);
        console.log('Internal server error, please contact for admin help!');
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }

    // Làm mới model để không bị ảnh hường bởi two way binding
    this.foodReport = new FoodReport();
    // Tat modal
    this.isVisibleFoodReportModal = false;
    // Refresh nut submit
    this.enableDisableFoodReportButtonSubmit();
  }

  // Phuc vu cho viec an hien nut submit
  addValueToReportContentList() {
    console.log(this.foodReport.reportContent);
    if (this.foodReport.reportContent == '') {
      this.foodReport.reportContents.pop();
      console.log("poped!");
    } else {
      this.foodReport.reportContents.push(this.foodReport.reportContent);
      console.log("pushed!");
    }
    this.enableDisableFoodReportButtonSubmit();
  }

  enableDisableFoodReportTextArea($event: any) {
    if ($event.checked == 'Other') {
      this.isDisabledFoodReportTextArea = false;
    } else {
      this.isDisabledFoodReportTextArea = true;
    }
  }

  // Validate to make this submit button enable whenever the user input some report content
  enableDisableFoodReportButtonSubmit() {
    // 
    // console.log($event.checked);
    // this.foodReport.reportContents.forEach(element => {
    //   console.log(element);
    //   if ($event.checked == element) {
    //     this.isDisabledFoodReportBtnSubmit = false;
    //   } else {
    //     this.isDisabledFoodReportBtnSubmit = true;
    //   }
    // });
    console.log(this.foodReport.reportContents);
    console.log("rpContents length: " + this.foodReport.reportContents.length);
    if (this.foodReport.reportContents.length < 1) {
      this.isDisabledFoodReportBtnSubmit = true;
    } else {
      this.isDisabledFoodReportBtnSubmit = false;
    }
  }

  // Hàm này kiểm tra khả năng tố cáo bằng cách xem customer đã report cái food với id: cụ thể nào đó chưa. Nếu rồi thì sẽ không sử dụng lại tính năng tố cáo đối với cái food đó nữa.
  async checkUsersReportFoodCapability() {
    // 
    try {
      this.loading = true;

      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.FOODREPORT, API.API_FOODREPORT.GET_ALL_FOODREPORT);

      if (response && response.message === "Success") {
        this.listFoodReport = response.foodReports;
        // console.log("Danh sách các đồ ăn đã tố cáo của khách hàng: " + this.listFoodReport);
        // console.log(response.foodReports);
        this.listFoodReport.forEach(element => {
          if(element.foodId == this.fooddetail.foodId){ // Da report
            this.hasAlreadyReported = true;
          }
        });
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  galleriaResponsiveOptions: any[] = [
    {
      breakpoint: '1024px',
      numVisible: 5
    },
    {
      breakpoint: '960px',
      numVisible: 4
    },
    {
      breakpoint: '768px',
      numVisible: 3
    },
    {
      breakpoint: '560px',
      numVisible: 1
    }
  ];

  carouselResponsiveOptions: any[] = [
    {
      breakpoint: '1024px',
      numVisible: 3,
      numScroll: 3
    },
    {
      breakpoint: '768px',
      numVisible: 2,
      numScroll: 2
    },
    {
      breakpoint: '560px',
      numVisible: 1,
      numScroll: 1
    }
  ];
}
