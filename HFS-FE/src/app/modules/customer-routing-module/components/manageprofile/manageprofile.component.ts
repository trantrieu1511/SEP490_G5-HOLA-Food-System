import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
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
import { Profile, ProfileDisplay, ProfileImage } from '../../models/profile';
import { FileSelectEvent, FileUploadEvent } from 'primeng/fileupload';
import { Router } from '@angular/router';
import { AppCustomerTopBarComponent } from 'src/app/app-systems/app-topbar/customer/app.topbar-cus.component';

@Component({
  selector: 'app-manageprofile',
  templateUrl: './manageprofile.component.html',
  styleUrls: ['./manageprofile.component.scss'],
  // providers: [AppCustomerTopBarComponent]
})

export class ManageprofileComponent extends iComponentBase implements OnInit {
editPhoneNumber() {
// throw new Error('Method not implemented.');
}
editEmail() {
// throw new Error('Method not implemented.');
}

  profile: Profile = new Profile();
  profileDisplay: ProfileDisplay = new ProfileDisplay();
  // visible = false;
  isVisibleEditProfileDialog: boolean = false;
  // minDate: Date = new Date();
  uploadedFiles: File[] = [];
  profileImage: ProfileImage = new ProfileImage();
  isLoggedIn: boolean = false;
  selectedSideBarOption: boolean = true;

  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private router: Router,
    private el: ElementRef
    // private appCustomerTopBarComponent: AppCustomerTopBarComponent
  ) {
    super(messageService);
    // this.route.queryParams.subscribe(params => {
    //   const myData = params['shopInfor'];
    //   console.log(myData.shopName);
    //   // Sử dụng myData tại đây
    // });

  }

  // Check whether the user has logged into Hola Food or not
  checkUserLoginState() {
    if (sessionStorage.getItem("userId") != null) {
      this.isLoggedIn = true;
    }
  }

  ngOnInit(): void {
    this.checkUserLoginState();
    this.getProfileInfo();
    // this.minDate.setFullYear(new Date().getFullYear() - 80); // Cho người dùng nhập tối đa đến 80 năm tuổi
    this.getProfileImage();
  }

  async getProfileImage() {
    try {
      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.PROFILEIMAGE, API.API_PROFILEIMAGE.GET_PROFILEIMAGE);

      if (response && response.message === 'Success') {
        console.log(response);
        console.log(response.profileImage);
        this.profileImage = response.profileImage;
      }
    } catch (e) {
      console.log(e);
    }
  }

  // showDialog() {
  //   this.visible = true;
  // }

  showUploadedFiles() {
    console.log(this.uploadedFiles);
  }

  handleFileSelection($event: FileSelectEvent) {
    this.uploadedFiles = $event.currentFiles;
    this.importProfileImage();
  }

  // onUploadProfileImg($event: FileUploadEvent) {
  //   // debugger;

  //   console.log($event.files);
  //   console.log($event.originalEvent);
  //   this.uploadedFiles = $event.files;
  // }

  async getProfileInfo() {
    try {
      // debugger;
      // const params = {
      //   userId: sessionStorage.getItem("userId")
      //   // userId: 1
      // }
      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.USER, API.API_USER.GETPROFILE);

      if (response && response.message === 'Success') {
        this.profile = response.data;
        this.profile.birthDate = this.profile.birthDate.toString().split('T')[0];
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
        // userId: sessionStorage.getItem("userId"),
        firstName: this.profile.firstName,
        lastName: this.profile.lastName,
        gender: this.profile.gender,
        birthDate: this.profile.birthDate
      }
      let response = await this.iServiceBase.putDataAsync(API.PHAN_HE.USER, API.API_USER.EDITPROFILE, inputData);

      if (response && response.message === 'Success') {
        console.log(response);
        console.log(response.message);
        this.showMessage(mType.success, 'Notification'
          , 'Profile information updated successfully.', 'notify');

        this.isVisibleEditProfileDialog = false;

        //Tải lại profile info
        this.getProfileInfo();

        //Clear model đã tạo
        // this.profile = new Profile();
        // console.log(this.profile);

      } else {
        console.log(response);
        console.log(response.message);
        this.showMessage(mType.error, 'Notification'
          , 'Internal server error, please contact admin for help.', 'notify');
      }
    } catch (e) {
      console.log(e);
    }
  }

  async importProfileImage() {
    try {
      //let param = postEnity;
      const param = new FormData();

      this.uploadedFiles.forEach((file) => {
        param.append('profileImage', file, file.name);
      });

      //console.log(param);
      const response = await this.iServiceBase.postDataAsync(
        API.PHAN_HE.PROFILEIMAGE,
        API.API_PROFILEIMAGE.IMPORT_PROFILEIMAGE,
        param,
        true
      );

      if (response && response.message === 'Success') {
        this.showMessage(
          mType.success,
          'Notification',
          'Import new profile image successfully',
          'notify'
        );
        console.log("Import new profile image successfully");

        //refresh profile image
        this.getProfileImage();

        //clear file upload
        this.uploadedFiles = [];

        //refresh customer app top bar
        // this.router.navigate(['/'], { skipLocationChange: true }).then(() => {
        //   this.router.navigate(['/profile']);
        // });
        // this.appCustomerTopBarComponent.reloadPage();

      } else {
        this.showMessage(
          mType.error,
          'Notification',
          'Import new profile image failed',
          'notify'
        );
        console.log("Import new profile image failed");
      }
    } catch (e) {
      console.log(e);
    }
  }

  openEditProfileDiaglog() {
    this.isVisibleEditProfileDialog = true;
  }

  hideDialog() {
    this.isVisibleEditProfileDialog = false;
  }

  doSomething() {
    // throw new Error('Method not implemented.');
  }
  async confirm(email:string){
     this.hideElement();
    const param= {
      "email": email
    }
    try {

      debugger;
           let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.HOME, API.API_HOME.SEND_CONFIRM_EMAIL,param);

           if (response && response.message === "Success") {
            this.showMessage(
              mType.success,
              'Notification',
              'Send to Email successfully',
              'notify'
            );
            this.hideElement();
           }
          ;
       } catch (e) {
           console.log(e);

       }
       this.hideElement();
  }
  hideElement() {
    // Sử dụng nativeElement để có thể thao tác trực tiếp với phần tử DOM
    const element = this.el.nativeElement.querySelector('.verify-email-link');

    // Kiểm tra xem phần tử có tồn tại không trước khi thực hiện thay đổi
    if (element) {
        element.style.display = 'none';
    }
}
  // getSeverity(status: string) {
  //   switch (status) {
  //     case 'INSTOCK':
  //       return 'success';
  //     case 'LOWSTOCK':
  //       return 'warning';
  //     case 'OUTOFSTOCK':
  //       return 'danger';
  //     default:
  //       return null;
  //   }
  // }

}
