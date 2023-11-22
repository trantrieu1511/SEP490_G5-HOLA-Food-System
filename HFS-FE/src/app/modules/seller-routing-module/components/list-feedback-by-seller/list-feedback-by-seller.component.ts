import { Component, OnInit } from '@angular/core';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData,
  iFunction
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../../services/apiURL";
import { User } from 'src/app/services/auth.service';
import { MessageService } from 'primeng/api';
import { FeedBackSeller, ReplySeller } from '../../models/feedbackseller.model';
import { Router } from '@angular/router';
@Component({
  selector: 'app-list-feedback-by-seller',
  templateUrl: './list-feedback-by-seller.component.html',
  styleUrls: ['./list-feedback-by-seller.component.scss']
})
export class ListFeedbackBySellerComponent extends iComponentBase implements OnInit {
  lstFeed: FeedBackSeller[] = [];

  user:User;
  displayDialogAdd: boolean = false;
  headerDialog: string = '';
  contentDialog:string ='';
  addShipper:string;
  feedImg :FeedBackSeller = new FeedBackSeller();
  feedreply:ReplySeller = new ReplySeller();
  visibleImageDialog:boolean=false;
  displayDialogReply:boolean=false;
  constructor( private shareData: ShareData,
    public messageService: MessageService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private _router: Router,
  ){
    super(messageService);

  }

  ngOnInit(): void {
this.getAllFeedback();
  }


  async getAllFeedback() {
    this.lstFeed = [];
// const param= {
//   "manageBsey": sessionStorage.getItem('userId'),
// }
    try {


        let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_USER.FEEDBACK_SELLER,"");

        if (response && response.message === "Success") {
            this.lstFeed = response.feedBacks;

        }
       ;
    } catch (e) {
        console.log(e);

    }
}
   Reply(feed:FeedBackSeller){
    this.displayDialogReply = true;
    this.contentDialog="Reply "+feed.feedbackId
    this.feedreply.customerId=feed.customerId;
    this.feedreply.sellerId=sessionStorage.getItem('userId');
    this.feedreply.feedbackId=feed.feedbackId;
}
  async onSave(){
  try {


    let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, API.API_USER.REPLY_SELLER,this.feedreply);

    if (response && response.message === "Success") {
      this.feedreply=new ReplySeller();
      this.displayDialogReply = false;
      this.showMessage(mType.success, "Notification", "New Reply successfully", 'notify');
    }
  else{
   this.showMessage(mType.error, "Notification", "Reply Failed", 'notify');
   }


} catch (e) {
    console.log(e);

}
}
onCancel(){}
onDisplayImagesDialog(cus: FeedBackSeller, event: any) {
  this.feedImg = cus;
  this.visibleImageDialog = true;
}
}
