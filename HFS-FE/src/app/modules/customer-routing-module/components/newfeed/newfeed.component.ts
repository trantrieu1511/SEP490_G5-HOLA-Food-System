import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { DataService } from 'src/app/services/data.service';
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../../../../services/apiURL";
import { Post } from '../../models/post.model';

@Component({
  selector: 'app-newfeed',
  templateUrl: './newfeed.component.html',
  styleUrls: ['./newfeed.component.scss']
})
export class NewfeedComponent extends iComponentBase implements OnInit{

  loading: boolean;
  listPost:Post []=[];

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

}
