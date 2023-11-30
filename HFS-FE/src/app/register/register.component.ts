import { AfterViewInit, Component, NgZone, OnInit, Renderer2 } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CredentialResponse } from 'google-one-tap';
import { DividerModule } from 'primeng/divider';
import { environment } from 'src/environments/environment';
import { AuthService, User } from '../services/auth.service';
import { Router } from '@angular/router';
import { DateOfBirthValidator, PasswordLengthValidator, PasswordMatch, PasswordNumberValidator, PasswordUpperValidator } from '../login/Restricted-login.directive';
import { RowToggler } from 'primeng/table';
import * as API from "../services/apiURL";
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData,
  iFunction
} from 'src/app/modules/shared-module/shared-module';
import { Register } from './models/register';
import { MessageService } from 'primeng/api';
import { FileRemoveEvent, FileSelectEvent } from 'primeng/fileupload';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent extends iComponentBase  implements OnInit,AfterViewInit {
  provinces: any[] = [];
  districts: any[] = [];
  wards: any[] = [];
  selectedResult: string = '';
  uploadedFiles:File[]=[];
  isFirstnameTouched = false;
  isLastnameTouched = false;
    isphoneNumberTouched = false;
  isShopnameTouched = false;
  isShopAddressTouched = false;
  user:User;
  formregister:FormGroup;
  date;
  roles: Role[];
  showForm: boolean = true;
  errorregister: string;
  selectedRole: number = 3;
  selectedRoleId: string = '';
private client_Id=environment.clientId;
 constructor(private router: Router,
  public renderer: Renderer2,
  private _ngZone: NgZone,
  public messageService: MessageService,
  private service: AuthService,
  private iServiceBase: iServiceBase,
 // private cdr: ChangeDetectorRef
){
   super(messageService);
 }
  ngAfterViewInit(): void {
    // this.loadGoogleLibrary();
    // this.cdr.detectChanges();

  }

 FormFirst(){
  const passwordControl = new FormControl('', [
    Validators.required,
    PasswordLengthValidator(),
    PasswordUpperValidator(),
    PasswordNumberValidator()
  ]);
  this.formregister = new FormGroup({
    firstName: this.selectedRole!=2? new FormControl('',[Validators.required]):new FormControl(''),
    lastName:this.selectedRole!=2? new FormControl('',[Validators.required]):new FormControl(''),
    email: new FormControl('', Validators.email),
    password:passwordControl,
    confirmPassword: new FormControl('', [
      Validators.required,
      PasswordMatch(passwordControl)
    ]),
    gender: this.selectedRole!=2? new FormControl('male', Validators.required):new FormControl(''),
    birthDate:this.selectedRole!=2? new FormControl('',DateOfBirthValidator()):new FormControl(''),
    roleId:new FormControl('',[Validators.required]),
    phoneNumber:new FormControl('',[Validators.required]),
    shopName:  this.selectedRole==2? new FormControl('',[Validators.required]):new FormControl(''),
    shopAddress:  this.selectedRole==2? new FormControl('',[Validators.required]):new FormControl('')
  });
}

7
// Gán giá trị cho biến registerData
registerData: Register;


 ngOnInit(): void {
  this.callAPI("", 'province');
  this.FormFirst();
  this.service.errorregister$.subscribe(errorregister => {
   // this.errorregister = errorregister;
   this.showMessage(mType.error, "Notification", errorregister, 'app-register');

  })

  this.roles = [
    {name: 'Customer', id: 3},
    {name: 'Seller', id: 2},
    {name: 'Shipper', id: 4}

  ];

  const cssFilePaths = ['assets/theme/indigo/theme-light.css',
        'assets/layout/css/layout-light.css'];

  // Xóa các liên kết CSS hiện có trong document.head
  const existingLinks = document.head.querySelectorAll('link[rel="stylesheet"]');
  existingLinks.forEach((link: HTMLLinkElement) => {
      document.head.removeChild(link);
  });

  cssFilePaths.forEach(link => {
    const cssLink = this.renderer.createElement('link');
    this.renderer.setAttribute(cssLink, 'rel', 'stylesheet');
    this.renderer.setAttribute(cssLink, 'type', 'text/css');
    this.renderer.setAttribute(cssLink, 'href', link);
    this.renderer.appendChild(document.head, cssLink);
  });
}
showFirstnameError() {
  const firstnameControl = this.formregister.get('firstname');
  if (firstnameControl.value === '' && firstnameControl.touched) {
    console.log('Firstname is required!');
  }
}
showLastnameError() {
  const lastnameControl = this.formregister.get('lastname');
  if (lastnameControl.value === '' && lastnameControl.touched) {
    console.log('Lastname is required!');
  }
}
showCaptchaError() {
  const captchanameControl = this.formregister.get('captcha');
  if (captchanameControl.value === '' && captchanameControl.touched) {
    console.log('Captcha is required!');
  }
}
showPhoneNumberError() {
  const captchanameControl = this.formregister.get('phonenumber');
  if (captchanameControl.value === '' && captchanameControl.touched) {
    console.log('Phone is required!');
  }
}
showShopNameError() {
  const captchanameControl = this.formregister.get('shopname');
  if (captchanameControl.value === '' && captchanameControl.touched) {
    console.log('Captcha is required!');
  }
}
showShopAddressError() {
  const captchanameControl = this.formregister.get('shopaddress');
  if (captchanameControl.value === '' && captchanameControl.touched) {
    console.log('Captcha is required!');
  }
}
onRoleChange(event: any) {
  this.selectedRoleId = event.target.value;

  const roleIdControl = this.formregister.get('roleId');
  const shopNameControl = this.formregister.get('shopName');
  const shopAddressControl = this.formregister.get('shopAddress');
  const firstNameControl = this.formregister.get('firstName');
  const lastNameControl = this.formregister.get('lastName');
  const genderControl = this.formregister.get('gender');
  const birthDateControl = this.formregister.get('birthDate');

  if (roleIdControl.value === '2') {
    // Nếu roleId là 2, enable và đặt validators cho shopName và shopAddress
    shopNameControl.enable();
    shopNameControl.setValidators([Validators.required]);
    shopAddressControl.enable();
    shopAddressControl.setValidators([Validators.required]);

    // Tắt và xóa validators cho các trường firstName, lastName, gender, birthDate
    firstNameControl.disable();
    firstNameControl.clearValidators();
    lastNameControl.disable();
    lastNameControl.clearValidators();
    genderControl.disable();
    genderControl.clearValidators();
    birthDateControl.disable();
    birthDateControl.clearValidators();
  } else {
    // Nếu roleId không phải là 2, tắt và xóa validators cho shopName và shopAddress
    shopNameControl.disable();
    shopNameControl.clearValidators();
    shopAddressControl.disable();
    shopAddressControl.clearValidators();

    // Enable validators cho các trường firstName, lastName, gender, birthDate
    firstNameControl.enable();
    firstNameControl.setValidators([Validators.required]);
    lastNameControl.enable();
    lastNameControl.setValidators([Validators.required]);
    genderControl.enable();
    genderControl.setValidators([Validators.required]);
    birthDateControl.enable();
    birthDateControl.setValidators([Validators.required]);
  }

  // Cập nhật validators và trạng thái của các trường
  shopNameControl.updateValueAndValidity();
  shopAddressControl.updateValueAndValidity();
  firstNameControl.updateValueAndValidity();
  lastNameControl.updateValueAndValidity();
  genderControl.updateValueAndValidity();
  birthDateControl.updateValueAndValidity();
}
async onSubmit() {
  console.log(this.formregister);
  ;
  if (this.formregister.valid) {

    switch(this.formregister.value.roleId.toString()){
      case "3":
        try {


          this.service.register(this.formregister.value).subscribe(res=>{
            this.service.showregister$.subscribe(showregister => {
              this.showForm = showregister;})
          })
        }catch (err) {

        }
        break;
        case "2":
          try {
            const selectedProvinceText = (document.getElementById('province') as HTMLSelectElement).options[
              (document.getElementById('province') as HTMLSelectElement).selectedIndex
            ].text;

            const selectedDistrictText = (document.getElementById('district') as HTMLSelectElement).options[
              (document.getElementById('district') as HTMLSelectElement).selectedIndex
            ].text;

            const selectedWardText = (document.getElementById('ward') as HTMLSelectElement).options[
              (document.getElementById('ward') as HTMLSelectElement).selectedIndex
            ].text;

            if (selectedDistrictText === 'Select district' && selectedProvinceText === 'Select Province' && selectedWardText === 'Select ward') {
              this.showMessage(mType.error, "Notification", `Vui Lòng chọn địa chỉ quán ăn`, 'app-register');
            }
            if((selectedDistrictText === 'Select district' || selectedProvinceText === 'Select Province' || selectedWardText === 'Select ward')&&this.uploadedFiles.length<2){
              this.showMessage(mType.error, "Notification", `Vui Lòng chọn địa chỉ quán ăn và up ít nhất 2 ảnh vào trường Business License`, 'app-register');
             return;
            }
            if(selectedDistrictText === 'Select district'){
              this.showMessage(mType.error, "Notification", `Vui Lòng chọn quận`, 'app-register');
             return;
            }
            if(selectedDistrictText === 'Select Province'){
              this.showMessage(mType.error, "Notification", `Vui Lòng chọn tỉnh`, 'app-register');
             return;
            }
            if(selectedDistrictText === 'Select ward'){
              this.showMessage(mType.error, "Notification", `Vui Lòng chọn phường`, 'app-register');
             return;
            }
            if(this.uploadedFiles.length<2){
              this.showMessage(mType.error, "Notification", `Please upload at least 2 photos in the Business License field`, 'app-register');
             return;
            }
            const param = new FormData();
            const shopNameControl = this.formregister.get('shopName');
            const shopAddressControl = this.formregister.get('shopAddress').value+"-"+this.selectedResult;
            const email = this.formregister.get('email');
            const phone = this.formregister.get('phoneNumber');
            const password = this.formregister.get('password');
            const confirm = this.formregister.get('confirmPassword');
            param.append('shopName',   shopNameControl.value);
            param.append('shopAddress',   shopAddressControl);
            param.append('email',   email.value);
            param.append('phoneNumber',   phone.value);
            param.append('password',   password.value);
            param.append('confirmPassword',   confirm.value);
            this.uploadedFiles.forEach(file => {
              param.append('images', file, file.name);
            });

            let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.HOME, API.API_USER.REGISTER_SELLER, param);
            if (response && response.success === true) {
             this.showForm=false;
                 this.showMessage(mType.success, "Notification", `Register successfully`, 'app-register');
              console.log(response);

           }
  else {
    // this.showMessage(mType.warn, "Error", this.iServiceBase.formatMessageError(response.message), 'notify');
    this.showMessage(mType.warn, "Error", response.message, 'app-register');
    // console.log(response);
    // console.log('Internal server error, please contact for admin help!');
 }

  //this.checkUsersReportPostCapability();

            // this.service.registerseller(param).subscribe(res=>{
            //   this.service.showregister$.subscribe(showregister => {
            //     this.showForm = showregister;})
            // })
          }catch (err) {

          }
          break;
          case "4":
            try {


              this.service.registershipper(this.formregister.value).subscribe(res=>{
                this.service.showregister$.subscribe(showregister => {
                  this.showForm = showregister;})
              })
            }catch (err) {
              this.showMessage(mType.error, "Notification", err, 'app-register');
            }
          break;
    }
    console.log(this.formregister.value.roleId);

}else{
  this.showMessage(mType.error, "Notification", "If you have missing information, please fill in again", 'app-register');

}
}

handleFileSelection(event: FileSelectEvent) {
  //console.log("select", event);

  this.uploadedFiles = event.currentFiles;

  //console.log('primeSelect',this.f_upload);
  console.log("uploadFiles", this.uploadedFiles);
}

handleFileRemoval(event: FileRemoveEvent) {
  console.log("remove", event.file.name);

  this.uploadedFiles = this.uploadedFiles.filter(f => f.name !== event.file.name);
  console.log("uploadFiles", this.uploadedFiles);
}

handleAllFilesClear(event: Event) {
  //console.log("clear", event);

  this.uploadedFiles = [];
  console.log("uploadFiles", this.uploadedFiles);
}
async callAPI(api: string, target: string) {
  
  const param={
    "url" :api
  }
  if(target!=='province'){
    const response = await  this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, target,param);
   console.log(response);
      this.renderData(response, target);
  }else{
    const response = await  this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.USER, target,"");
    console.log(response);
       this.renderData(response, target);
}



}

renderData(data: any, target: string) {
  switch (target) {
    case 'province':
      this.provinces = data;
      break;
    case 'district':
      this.districts = data['districts'];
      break;
    case 'ward':
      this.wards = data['wards'];
      break;
    default:
      break;
  }
}

onProvinceChange() {
;
  const selectedProvinceCode = (document.getElementById('province') as HTMLSelectElement).value;

  this.callAPI('p/' + selectedProvinceCode + '?depth=2', 'district');
  this.printResult();
}

onDistrictChange() {
  const selectedDistrictCode = (document.getElementById('district') as HTMLSelectElement).value;
  this.callAPI('d/' + selectedDistrictCode + '?depth=2', 'ward');
  this.printResult();
}

onWardChange() {
  this.printResult();
}

printResult() {
  const selectedProvinceText = (document.getElementById('province') as HTMLSelectElement).options[
    (document.getElementById('province') as HTMLSelectElement).selectedIndex
  ].text;

  const selectedDistrictText = (document.getElementById('district') as HTMLSelectElement).options[
    (document.getElementById('district') as HTMLSelectElement).selectedIndex
  ].text;

  const selectedWardText = (document.getElementById('ward') as HTMLSelectElement).options[
    (document.getElementById('ward') as HTMLSelectElement).selectedIndex
  ].text;

  if (selectedDistrictText !== '' && selectedProvinceText !== '' && selectedWardText !== '') {
    this.selectedResult = `${selectedWardText} - ${selectedDistrictText} - ${selectedProvinceText}`;

  }
}
}
interface Role {
  name: string,
  id: number
}

