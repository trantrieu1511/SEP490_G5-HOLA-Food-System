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
import { Profile, ProfileDisplay } from '../../models/profile';

@Component({
  selector: 'app-manageprofile',
  templateUrl: './manageprofile.component.html',
  styleUrls: ['./manageprofile.component.scss']
})

export class ManageprofileComponent extends iComponentBase implements OnInit {

  profile: Profile;
  profileDisplay: ProfileDisplay;
  visible = false;

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
    this.getProfileInfo();
  }

  showDialog() {
    this.visible = true;
  }

  async getProfileInfo() {
    try {
      const params = {
        userId: sessionStorage.getItem("userId")
        // userId: 1
      }
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_USER.GETPROFILE, params);

      if (response && response.message === 'Success') {
        this.profile = response.data;
        this.profileDisplay = Object.assign({}, response.data); //Copy profile sang mot entity moi phuc vu cho viec display (Entity do khac dia chi so voi profile nen khong bi anh huong boi two way data binding)

        console.log(this.profile);
        console.log(this.profileDisplay);

      }
    } catch (e) {
      console.log(e);
    }
  }

  async editProfile() {
    try {
      const inputData = {
        userId: sessionStorage.getItem("userId"),
        firstName: this.profile.firstName,
        lastName: this.profile.lastName,
        gender: this.profile.gender,
        birthDate: this.profile.birthDate
      }
      let response = await this.iServiceBase.putDataAsync(API.PHAN_HE.USER, API.API_USER.EDITPROFILE, inputData);

      if (response && response.message === 'Success') {
        
        this.showMessage(mType.success, "Notification"
          , "profile updated successfully", 'notify');

        this.visible = false;

        //lấy lại danh sách All 
        this.getProfileInfo();

        //Clear model đã tạo
        this.profile = new Profile();
        // console.log(this.profile);

      }
    } catch (e) {
      console.log(e);
    }
  }

}
