import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import {
  iComponentBase,
  iFunction,
  iServiceBase, mType,
  ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from "../services/apiURL";
import {
  ConfirmationService,
  LazyLoadEvent,
  MenuItem,
  MessageService,
  SelectItem,
  TreeNode
} from "primeng/api";
import { ChangePasswordInputValidation, EditProfileInputValidation, LicenseImage, Profile, ProfileDisplay, ProfileImage, VerifyIdentityInputValidation } from './models/profile';
import { FileRemoveEvent, FileSelectEvent, FileUploadEvent } from 'primeng/fileupload';
import { Router } from '@angular/router';
import { AppCustomerTopBarComponent } from 'src/app/app-systems/app-topbar/customer/app.topbar-cus.component';
import { AuthService } from 'src/app/services/auth.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-manageprofile',
  templateUrl: './manageprofile.component.html',
  styleUrls: ['./manageprofile.component.scss'],
  // providers: [AppCustomerTopBarComponent]
})

export class ManageprofileComponent extends iComponentBase implements OnInit {

  // ------------- Binding variables ------------------
  profile: Profile = new Profile();
  profileDisplay: ProfileDisplay = new ProfileDisplay();
  // minDate: Date = new Date();
  uploadedFiles: File[] = [];
  profileImage: ProfileImage = new ProfileImage();
  profileModal: Profile = new Profile(); // instance dung de bind len modal
  licenseImages: LicenseImage[] = [];

  // ------------- UI component variables ---------------
  isVisibleChangePasswordModal: boolean = false;
  isVisibleVerifyYourselfModal: boolean = false;
  isVisibleOthersEditProfileDialog: boolean = false;
  isVisibleSellerEditProfileDialog: boolean = false;
  isLoggedIn: boolean = false;
  selectedSideBarOption: boolean = true;
  isVisibleVerifyPhoneNumberModal: boolean = false;
  isVisibleVerifyPhoneNumberModal2: boolean = false;
  isVisibleEditLicenseDialog: boolean = false;
  visibleLicenseImageDialog: boolean = false;

  // ------------- Validation variables --------------
  editProfileValidation: EditProfileInputValidation = new EditProfileInputValidation();
  changePasswordValidation: ChangePasswordInputValidation = new ChangePasswordInputValidation();
  verifyIdentityValidation: VerifyIdentityInputValidation = new VerifyIdentityInputValidation();

  // ------- Verify phone number variables ---------
  phoneOtp: string;
  verifyOtp: string;
  role: string;


  //lang
  edtUpBtn: string;
  addUpBtn: string;
  None1: string;
  None2: string;
  None3: string;
  None4: string;
  None5: string;
  None6: string;

  constructor(
    private shareData: ShareData,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    public router: Router,
    public authService: AuthService,
    private el: ElementRef,
    private iFunction: iFunction,
    public translate: TranslateService
    // private appCustomerTopBarComponent: AppCustomerTopBarComponent
  ) {
    super(messageService);
    this.initLabelLang();
    // this.route.queryParams.subscribe(params => {
    //   const myData = params['shopInfor'];
    //   console.log(myData.shopName);
    //   // Sử dụng myData tại đây
    // });

  }

  initLabelLang() {
    this.translate.get('profileScreen').subscribe( (text: any) => {
      this.addUpBtn = text.Addprofileimage;
      this.edtUpBtn = text.Editprofileimage;
      this.None1 = text.None1;
      this.None2 = text.None2;
      this.None3 = text.None3;
      this.None4 = text.None4;
      this.None5 = text.None5;
      this.None6 = text.None6;
      
    });
  }

  // Check whether the user has logged into Hola Food or not
  checkUserLoginState() {
    // if (sessionStorage.getItem("userId") != null) {
    //   this.isLoggedIn = true;
    // }
    if (this.authService.getUserInfor().userId != null) {
      this.isLoggedIn = true;
    }
  }

  ngOnInit(): void {
    this.role = this.authService.getUserInfor().role;
    this.checkUserLoginState();
    this.getProfileInfo();
    // this.minDate.setFullYear(new Date().getFullYear() - 80); // Cho người dùng nhập tối đa đến 80 năm tuổi
    this.getProfileImage();
  }

  async getProfileImage() {
    try {
      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.PROFILEIMAGE, API.API_PROFILEIMAGE.GET_PROFILEIMAGE);

      if (response && response.message === 'Success') {
        //console.log(response);
        //console.log(response.profileImage);
        this.profileImage = response.profileImage;
      }
    } catch (e) {
      console.log(e);
      this.showMessage(mType.error, 'Error'
        , 'Internal server error, please contact admin for help.', 'notify');
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

  handleLicenseFileSelection($event: FileSelectEvent) {
    this.uploadedFiles = $event.currentFiles;
    // this.importProfileImage();
  }

  handleFileRemoval(event: FileRemoveEvent) {
    //console.log("remove", event.file.name);

    this.uploadedFiles = this.uploadedFiles.filter(f => f.name !== event.file.name);
    //console.log("uploadFiles", this.uploadedFiles);
  }

  handleAllFilesClear(event: Event) {
    //console.log("clear", event);

    this.uploadedFiles = [];
    //console.log("uploadFiles", this.uploadedFiles);
  }

  // onUploadProfileImg($event: FileUploadEvent) {
  //   //

  //   console.log($event.files);
  //   console.log($event.originalEvent);
  //   this.uploadedFiles = $event.files;
  // }

  async getProfileInfo() {
    try {
      // debugger
      //
      // const params = {
      //   userId: sessionStorage.getItem("userId")
      //   // userId: 1
      // }
      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.USER, API.API_USER.GETPROFILE);

      if (response && response.message === 'Success') {
        this.profile = response.data;
        this.profile.birthDate = (response.data.birthDate != undefined ? response.data.birthDate.toString().split('T')[0] : '');
        this.profileDisplay = Object.assign({}, response.data); //Copy profile sang mot entity moi phuc vu cho viec display (Entity do khac dia chi so voi profile nen khong bi anh huong boi two way data binding)
        this.profileModal = response.data;

        // format date sang dd/mm/yyyy
        const date = new Date(this.profileDisplay.birthDate);
        const yyyy = date.getFullYear();
        let mm = date.getMonth() + 1; // Months start at 0!
        let dd = date.getDate();
        let ddstr: string = '';
        let mmstr: string = '';

        if (dd < 10) ddstr = '0' + dd;
        if (mm < 10) mmstr = '0' + mm;

        const formattedDate = (ddstr != '' ? ddstr : dd) + '/' + (mmstr != '' ? mmstr : mm) + '/' + yyyy;
        this.profileDisplay.birthDate = (formattedDate == "NaN/NaN/NaN" ? '' : formattedDate); // Dang ky bang gg se bi gap truong hop NaN/NaN/NaN nhu vay

        // console.log(this.profile);
        // console.log(this.profileDisplay);

      } else {
        this.showMessage(mType.error, 'Error', response.message, 'notify');
        console.log(response);
      }
    } catch (e) {
      // this.showMessage(mType.error, 'Error', e.message, 'notify');
      console.log(e);
      this.showMessage(mType.error, 'Error'
        , 'Internal server error, please contact admin for help.', 'notify');
    }
  }

  validateEditProfileInput(): boolean {
    console.log(this.profile.birthDate);
    var check = true;
    this.editProfileValidation = new EditProfileInputValidation();
    if (this.authService.getUserInfor().role != 'SE') {
      if (!this.profile.lastName || this.profile.lastName == '') {
        this.editProfileValidation.isValidLastName = false;
        this.editProfileValidation.LastNameValidationMessage = "Last name can not empty";
        check = false;
      } else {
        if (this.profile.lastName.length > 50) {
          this.editProfileValidation.isValidLastName = false;
          this.editProfileValidation.LastNameValidationMessage = "Last name must be less than 50 characters";
          check = false;
        }
      }

      if (!this.profile.firstName || this.profile.firstName == '') {
        this.editProfileValidation.isValidFirstName = false;
        this.editProfileValidation.FirstNameValidationMessage = "First name can not empty";
        check = false;
      } else {
        if (this.profile.firstName.length > 50) {
          this.editProfileValidation.isValidFirstName = false;
          this.editProfileValidation.FirstNameValidationMessage = "First name must be less than 50 characters";
          check = false;
        }
      }

      if (this.profile.birthDate != '') {
        var birthdate = new Date(this.profile.birthDate);
        var currentDate = new Date();
        var maxAgeDate = new Date();
        maxAgeDate.setFullYear(maxAgeDate.getFullYear() - 100);
        if (birthdate > currentDate) {
          this.editProfileValidation.isValidBirthDate = false;
          this.editProfileValidation.BirthDateValidationMessage = "Your birth date cannot be in the future";
          check = false;
        }

        if (birthdate <= maxAgeDate) {
          this.editProfileValidation.isValidBirthDate = false;
          this.editProfileValidation.BirthDateValidationMessage = "Birth date is less than 100 year olds";
          check = false;
        }
      }
    }

    if (!this.profile.phoneNumber || this.profile.phoneNumber == '') {
      this.editProfileValidation.isValidPhoneNumber = false;
      this.editProfileValidation.PhoneNumberValidationMessage = "Phone number can not empty";
      check = false;
    } else {
      const regexPhoneNumber = /(84|0[3|5|7|8|9])+([0-9]{8})\b/g;
      if (!this.profile.phoneNumber.match(regexPhoneNumber)) {
        this.editProfileValidation.isValidPhoneNumber = false;
        this.editProfileValidation.PhoneNumberValidationMessage = "Invalid phone number. Please enter a valid Vietnamese phonenumber.";
        check = false;
      }
    }

    if (this.authService.getUserInfor().role == 'SE') {

      if (!this.profile.shopName || this.profile.shopName == '') {
        this.editProfileValidation.isValidShopName = false;
        this.editProfileValidation.ShopNameValidationMessage = "Shop name can not empty";
        check = false;
      } else {
        if (this.profile.shopName.length > 50) {
          this.editProfileValidation.isValidShopName = false;
          this.editProfileValidation.ShopNameValidationMessage = "Shop name must be less than 50 characters";
          check = false;
        }
      }

      if (!this.profile.shopAddress || this.profile.shopAddress == '') {
        this.editProfileValidation.isValidShopAddress = false;
        this.editProfileValidation.ShopAddressValidationMessage = "Shop address can not empty";
        check = false;
      }

      // if (this.profile.shopAddress.length > 200) {
      //   this.editProfileValidation.isValidShopAddress = false;
      //   this.editProfileValidation.ShopAddressValidationMessage = "Shop address must be less than 200 characters";
      //   check = false;
      // }
    }

    console.log(this.editProfileValidation);
    return check;
  }

  async editProfile() {
    if (this.validateEditProfileInput()) { // Validate before continuing to edit
      try {
        let isNewPhonenumber = true; // Nguoi dung edit thong tin su dung so dien thoai moi
        if (this.profile.phoneNumber == this.profileDisplay.phoneNumber) { // Nguoi dung van giu lai so cu
          isNewPhonenumber = false;
        }
        const inputData = {
          // userId: sessionStorage.getItem("userId"),
          firstName: this.profile.firstName,
          lastName: this.profile.lastName,
          gender: this.profile.gender,
          birthDate: this.profile.birthDate == '' ? null : this.profile.birthDate,
          phoneNumber: this.profile.phoneNumber,
          shopName: this.profile.shopName,
          shopAddress: this.profile.shopAddress,
          isNewPhonenumber: isNewPhonenumber
        }
        // console.log(inputData);
        let response = await this.iServiceBase.putDataAsync(API.PHAN_HE.USER, API.API_USER.EDITPROFILE, inputData);

        if (response && response.message === 'Success') {
          console.log(response);
          console.log(response.message);
          this.showMessage(mType.success, 'Notification'
            , 'Profile information update successfully.', 'notify');

          this.isVisibleOthersEditProfileDialog = false;
          this.isVisibleSellerEditProfileDialog = false;

          //Tải lại profile info
          this.getProfileInfo();

          //Clear model đã tạo
          // this.profile = new Profile();
          // console.log(this.profile);

        } else {
          console.log(response);
          // console.log(response.message);
          if(response.errors){
            this.showMessage(mType.error, response.message
            , response.errors.systemErrors, 'notify');  
            return;
          }
          this.showMessage(mType.error, 'Notification'
            , 'Profile information update failed.', 'notify');
        }
      } catch (e) {
        console.log(e);
        this.showMessage(mType.error, 'Error'
          , 'Internal server error, please contact admin for help.', 'notify');
      }
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
        console.log("Import new profile image successfully");
        this.showMessage(
          mType.success,
          'Notification',
          'Import new profile image successfully',
          'notify'
        );

        //refresh profile images
        // this.getProfileImage();
        window.location.reload();

        //clear file upload
        // this.uploadedFiles = [];

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
      this.showMessage(mType.error, 'Error'
        , 'Internal server error, please contact admin for help.', 'notify');
    }
  }

  openOthersEditProfileDiaglog() {
    this.profile = Object.assign({}, this.profileModal);

    this.editProfileValidation = new EditProfileInputValidation();

    this.isVisibleOthersEditProfileDialog = true;
  }

  openSellerEditProfileDiaglog() {
    this.profile = Object.assign({}, this.profileModal);

    this.editProfileValidation = new EditProfileInputValidation();

    this.isVisibleSellerEditProfileDialog = true;
  }

  // hideDialog() {
  //   this.isVisibleEditProfileDialog = false;
  // }

  async confirm(email: string) {
    this.hideElement();
    const param = {
      "email": email
    }
    try {


      let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.HOME, API.API_HOME.SEND_CONFIRM_EMAIL, param);

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
      this.showMessage(mType.error, 'Error'
        , 'Internal server error, please contact admin for help.', 'notify');

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
  openVerifyIdentityModal() {
    this.profile.oldPassword = '';

    this.verifyIdentityValidation = new VerifyIdentityInputValidation();

    this.isVisibleVerifyYourselfModal = true;
  }

  async verifyIdentity() {
    if (this.validateVerifyIdentityInput()) {
      try {
        let param = { password: this.profile.oldPassword };
        let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_USER.VERIFYUSERIDENTITY, param);

        if (response && response.message === 'Success') {
          this.showMessage(
            mType.success,
            'Notification',
            'Verify your identity successfully',
            'notify'
          );
          console.log(response);
          this.isVisibleVerifyYourselfModal = false;
          setTimeout(() => {
            this.profile.newPassword = '';
            this.profile.confirmNewPassword = '';
            this.changePasswordValidation = new ChangePasswordInputValidation();
            this.isVisibleChangePasswordModal = true;
          }, 1000);
        } else {
          this.showMessage(
            mType.error,
            'Notification',
            'Wrong password, verify your identity failed. Please try again',
            'notify'
          );
          console.log(response);
          this.isVisibleVerifyYourselfModal = false;
        }
      } catch (e) {
        console.log(e);
        this.showMessage(
          mType.success,
          'Notification',
          'System error',
          'notify'
        );
      }
    }
  }

  validateVerifyIdentityInput() {
    var check = true;
    this.verifyIdentityValidation = new VerifyIdentityInputValidation();
    if (!this.profile.oldPassword || this.profile.oldPassword == '') {
      this.verifyIdentityValidation.isValidPassword = false;
      this.verifyIdentityValidation.PasswordValidationMessage = "Password can not empty";
      check = false;
    } else {
      // const regexPassword = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,30}$\b/g; // / \b /g phuc vu cho cau truc cu phap cua regex
      // if (!this.profile.oldPassword.match(regexPassword)) {
      //   this.verifyIdentityValidation.isValidPassword = false;
      //   this.verifyIdentityValidation.PasswordValidationMessage = "Password must contain at least a uppercase, lower case and number and not contain special characters. Min length: 8. Max length: 30.";
      //   check = false;
      // }
    }
    return check;
  }

  async changePassword() {
    if (this.validateChangePasswordInput()) {
      try {
        let param = {
          newPassword: this.profile.newPassword,
          confirmPassword: this.profile.confirmNewPassword
        };
        let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_USER.CHANGEACCOUNTPASSWORD, param);

        if (response && response.message === 'Success') {
          this.showMessage(
            mType.success,
            'Notification',
            'Change password successfully',
            'notify'
          );
          console.log(response);
          this.isVisibleChangePasswordModal = false;
          // setTimeout(() => {
          //   this.isVisibleChangePasswordModal = true;
          // }, 1000);
        } else {
          this.showMessage(
            mType.error,
            'Notification',
            'Change password failed',
            'notify'
          );
          console.log(response);
          this.isVisibleChangePasswordModal = false;
        }
      } catch (e) {
        console.log(e);
        this.showMessage(
          mType.success,
          'Notification',
          'System error',
          'notify'
        );
      }
    }
  }
  validateChangePasswordInput(): boolean {
    var check = true;
    this.changePasswordValidation = new ChangePasswordInputValidation();
    if (!this.profile.newPassword || this.profile.newPassword == '') {
      this.changePasswordValidation.isValidPassword = false;
      this.changePasswordValidation.PasswordValidationMessage = "New password can not empty";
      check = false;
    } else {
      const regexPassword = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,30}$\b/g; // / \b /g phuc vu cho cau truc cu phap cua regex
      if (!this.profile.newPassword.match(regexPassword)) {
        this.changePasswordValidation.isValidPassword = false;
        this.changePasswordValidation.PasswordValidationMessage = "Password must contain at least a uppercase, lower case and number and not contain special characters. Min length: 8. Max length: 30.";
        check = false;
      }
      if (this.profile.newPassword == this.profile.oldPassword) {
        this.changePasswordValidation.isValidPassword = false;
        this.changePasswordValidation.PasswordValidationMessage = "New password cannot be the same as the old one. Please enter a different one.";
        check = false;
      }
      if (!this.profile.confirmNewPassword || this.profile.confirmNewPassword == '') {
        this.changePasswordValidation.isValidConfirmPassword = false;
        this.changePasswordValidation.ConfirmPasswordValidationMessage = "Confirm password can not empty";
        check = false;
      } else {
        if (this.profile.confirmNewPassword != this.profile.newPassword) {
          this.changePasswordValidation.isValidConfirmPassword = false;
          this.changePasswordValidation.ConfirmPasswordValidationMessage = "Confirm password is not match with new password";
          check = false;
        }
      }
    }
    return check;
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


  async openVerifyPhonenumberModal(phone: string) {
    this.isVisibleVerifyPhoneNumberModal = true;
    this.phoneOtp = phone;
    const param = {
      "phoneNumber": phone
    }
    try {
      // let response ;
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_USER.SEND_OTP, param);

      if (response && response.message === 'Success') {
        this.isVisibleVerifyPhoneNumberModal = true;
        this.showMessage(
          mType.success,
          'Notification',
          'Send OTP successfully',
          'notify'
        );
      } else {
        this.showMessage(
          mType.error,
          'Notification',
          'Send OTP fail',
          'notify'
        );
      }
    } catch (e) {
      console.log(e);
      this.showMessage(
        mType.error,
        'Notification',
        'System error',
        'notify'
      );
    }

    // this.isVisibleVerifyPhoneNumberModal = true;
  }
  isValidOTP(): boolean {
    return this.verifyOtp.length === 4;
  }
  async verifyPhonenumber() {
    const param = {
      "phoneNumber": this.phoneOtp,
      "otp": this.verifyOtp
    }
    try {
      let response = await this.iServiceBase.postDataAsync(API.PHAN_HE.USER, API.API_USER.CHECK_OTP, param);

      if (response && response.message === 'Success') {
        this.isVisibleVerifyPhoneNumberModal = false;
        this.showMessage(
          mType.success,
          'Notification',
          'Verify OTP successfully',
          'notify'
        );
      } else {
        this.showMessage(
          mType.error,
          'Notification',
          'Verify OTP fail',
          'notify'
        );
      }
    } catch (e) {
      console.log(e);
      this.showMessage(
        mType.error,
        'Notification',
        'System error',
        'notify'
      );
    }

  }

  async openEditLicenseDialog() {
    this.uploadedFiles = [];
    await this.getLicenseImage();
    if (this.licenseImages) {
      this.licenseImages.forEach(image => {
        var fileimage = this.iFunction.convertImageBase64toFile(image.imageBase64, image.name);
        this.uploadedFiles.push(fileimage);
        //this.f_upload.files.push(fileimage);
      });
    }
    this.isVisibleEditLicenseDialog = true;
  }

  async getLicenseImage() {
    try {
      let response = await this.iServiceBase.getDataAsync(API.PHAN_HE.USER, API.API_USER.GETLICENSEIMAGE);

      if (response && response.message === 'Success') {
        this.licenseImages = response.licenseImages;
        console.log(this.licenseImages);

      } else {
        if (response.message == 'The seller has no license images.') {
          return;
        }
        this.showMessage(mType.error, 'Error', response.message, 'notify');
        console.log(response);
      }
    } catch (e) {
      // this.showMessage(mType.error, 'Error', e.message, 'notify');
      console.log(e);
      this.showMessage(mType.error, 'Error'
        , 'System error, please contact admin for help.', 'notify');
    }
  }

  async editLicense() {
    // alert('Nut save da hoat dong');
    try {
      const param = new FormData();

      this.uploadedFiles.forEach(file => {
        param.append('images', file, file.name);
      });

      let response = await this.iServiceBase
        .putDataAsync(API.PHAN_HE.USER, API.API_USER.EDITLICENSEIMAGE, param, true);

      if (response && response.message === "Success") {
        this.showMessage(mType.success, "Notification"
          , "License image updated successfully", 'notify');

        // this.isVisibleEditLicenseDialog = false;

        //lấy lại danh sách All
        await this.getLicenseImage();

        //clear file upload
        this.uploadedFiles = [];

        //reload file upload
        if (this.licenseImages) {
          this.licenseImages.forEach(image => {
            var fileimage = this.iFunction.convertImageBase64toFile(image.imageBase64, image.name);
            this.uploadedFiles.push(fileimage);
            //this.f_upload.files.push(fileimage);
          });
        }
      } else {
        // var messageError = this.iServiceBase.formatMessageError(response)
        this.showMessage(mType.error, 'Error', response.message, 'notify');
        console.log(response.message);
      }
    } catch (e) {
      console.log(e);
      this.showMessage(mType.error, 'Error', 'System error. Please contact admin for further help.', 'notify');
    }
  }

}
