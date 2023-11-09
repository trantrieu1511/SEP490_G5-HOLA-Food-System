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

  async ngOnInit() {
    await this.getFoodDetail();
    await this.onGetSimilarFood();
    await this.getFeedBack();
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
      getfood.foodId = 1
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.FOODETAIL, API.API_FOODDETAIL.GET_FEEDBACK, getfood);
      if (response && response.success === true) {
        this.feedbacks = response.feedBacks
        this.displayFeedback = this.feedbacks.slice(this.first, this.rows);
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
      getfood.foodId = 1
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.FOODETAIL, API.API_FOODDETAIL.GET_FOOD, getfood);
      debugger
      if (response && response.success === true) {
        this.fooddetail = response;
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
