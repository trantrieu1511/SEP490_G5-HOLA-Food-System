import { Component, OnInit } from '@angular/core';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../../services/apiURL";
import { Post } from '../../models/post.model';
import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from 'src/app/services/data.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { CommentNewFeed, InputComment } from '../../models/comment.model';
@Component({
  selector: 'app-new-feed-module',
  templateUrl: './new-feed-module.component.html',
  styleUrls: ['./new-feed-module.component.scss']
})

export class NewFeedModuleComponent extends iComponentBase implements OnInit{
  loading: boolean;
  listPost:Post []=[];
  lstComment:CommentNewFeed[] = [];
  commentModel: CommentNewFeed = new CommentNewFeed();
  userId:string ;
  postId:number;
  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private route: ActivatedRoute,
    private router: Router,
    private dataService: DataService
  ){
    super(messageService);
  }

  ngOnInit(): void {
    this.getAllPost();
  }

  async getAllPost(){
    this.listPost = [];
    try {
      const param={
        "status":0
      }
      this.loading = true;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_NEWFEED.GETALLPOST, param);
      console.log(response)
      if (response && response.message === "Success") {
        this.listPost = response.posts;
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

  OnCommnent(item : Post){
    this.getAllComment(item.postId);
    this.postId = item.postId;
  }

  bindingDataCommentModel():InputComment{
    let comment = new InputComment();
    if (this.commentModel.commentId && this.commentModel.commentId > 0) {
      //Update
      comment.commentId = this.commentModel.commentId;
      comment.commentContent = this.commentModel.commentContent;
      comment.customerId = this.commentModel.customerId;
      comment.postId = this.commentModel.postId
    }else{      
      comment.commentContent = this.commentModel.commentContent;
      comment.customerId = this.commentModel.customerId;
      comment.postId = this.commentModel.postId
    }
    return comment;
  }
  OnSaveCommnent(){
    let comEntity = this.bindingDataCommentModel();
    console.log(comEntity);
    if (comEntity && comEntity.commentId && comEntity.commentId > 0) {
      //this.updateVoucher(voucherEntity);
    } else {
      this.createComment(comEntity);
    }
    this.commentModel = new CommentNewFeed();
  }

  async createComment(comEntity : InputComment){
    try {
      //
      this.userId = sessionStorage.getItem('userId'); 
      const param = {
        'postId':this.postId,
        'customerId':this.userId,
        'commentContent':comEntity.commentContent
      }  
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_NEWFEED.CREATECOMMENT,param);
      if (response && response.message === "Success") {
        this.showMessage(mType.success, "Notification", "Create successfully", 'notify');
        this.getAllComment(this.postId);
      }else{
        this.showMessage(mType.success, "Notification", "Create failure", 'notify');
      }
    } catch (e) {
      console.log(e);
        this.showMessage(mType.error, "Notification", "Create failure", 'notify');
    }
  }

  async getAllComment(item: number){
    this.lstComment= [];
    try {
      const param={
        "postId":item
      }
      this.loading = true;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_NEWFEED.GetALLCOMMENT, param);
      console.log(response)
      if (response && response.message === "Success") {
        this.lstComment = response.listComment;
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }
}
