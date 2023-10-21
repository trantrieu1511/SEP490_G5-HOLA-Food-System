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
import { Profile } from '../../models/profile';

@Component({
  selector: 'app-manageprofile',
  templateUrl: './manageprofile.component.html',
  styleUrls: ['./manageprofile.component.scss']
})

export class ManageprofileComponent extends iComponentBase implements OnInit {
  profile: Profile;

  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
  ) {
    super(messageService);
    // this.route.queryParams.subscribe(params => {
    //   const myData = params['shopInfor'];
    //   console.log(myData.shopName);
    //   // Sử dụng myData tại đây
    // });

  }

  ngOnInit(): void {
    this.GetProfileInfo();
  }

  async GetProfileInfo() {
    try {
      const params = {
        // userId: sessionStorage.getItem("userId")
        userId: 1
      }
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_USER.GETPROFILE, params);

      if (response && response.message === 'Success') {
        this.profile = response.data;
        
        console.log(this.profile);

      }
    } catch (e) {
      console.log(e);
    }
  }

}
