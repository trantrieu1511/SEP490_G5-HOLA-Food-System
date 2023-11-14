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

  lstPost: Post[] ;
  loading: boolean;


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
    try {
      this.loading = true;

      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.NEWFEED, API.API_NEWFEED.GETALLPOST, null);
      console.log(response)
      if (response && response.message === "Success") {
        this.lstPost = response.posts;
      }
      this.loading = false;
    } catch (e) {
      console.log(e);
      this.loading = false;
    }
  }

}
