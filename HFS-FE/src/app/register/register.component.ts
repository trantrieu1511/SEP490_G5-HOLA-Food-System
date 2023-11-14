import { AfterViewInit, Component, NgZone, OnInit, Renderer2 } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { CredentialResponse } from 'google-one-tap';
import { DividerModule } from 'primeng/divider';
import { environment } from 'src/environments/environment';
import { AuthService, User } from '../services/auth.service';
import { Router } from '@angular/router';
import { DateOfBirthValidator, PasswordLengthValidator, PasswordMatch, PasswordNumberValidator, PasswordUpperValidator } from '../login/Restricted-login.directive';
import { RowToggler } from 'primeng/table';

import {
  iComponentBase,
  iServiceBase, mType,
  ShareData,
  iFunction
} from 'src/app/modules/shared-module/shared-module';
import { Register } from './models/register';
import { MessageService } from 'primeng/api';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent extends iComponentBase  implements OnInit,AfterViewInit {
  isFirstnameTouched = false;
  isLastnameTouched = false;
  user:User;
  formregister:FormGroup;
  date;
  roles: Role[];
  showForm: boolean = true;
  errorregister: string;
  selectedRole: number = 3;

private client_Id=environment.clientId;
 constructor(private router: Router,
  public renderer: Renderer2,
  private _ngZone: NgZone,
  public messageService: MessageService,
  private service: AuthService,
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
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    email: new FormControl('', Validators.email),
    password:passwordControl,
    confirmPassword: new FormControl('', [
      Validators.required,
      PasswordMatch(passwordControl)
    ]),
    gender: new FormControl('male', Validators.required),
    birthDate:new FormControl('',DateOfBirthValidator()),
    roleId:new FormControl('',[Validators.required])
  });
}

7
// Gán giá trị cho biến registerData
registerData: Register;


 ngOnInit(): void {
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
async onSubmit() {
  if (this.formregister.valid) {
debugger;
    switch(this.formregister.value.roleId.toString()){
      case "3":
        try {
          debugger;

          this.service.register(this.formregister.value).subscribe(res=>{
            this.service.showregister$.subscribe(showregister => {
              this.showForm = showregister;})
          })
        }catch (err) {

        }
        break;
        case "2":
          try {
            debugger;

            this.service.registerseller(this.formregister.value).subscribe(res=>{
              this.service.showregister$.subscribe(showregister => {
                this.showForm = showregister;})
            })
          }catch (err) {

          }
          break;
          case "4":
            try {
              debugger;

              this.service.registershipper(this.formregister.value).subscribe(res=>{
                this.service.showregister$.subscribe(showregister => {
                  this.showForm = showregister;})
              })
            }catch (err) {

            }
          break;
    }
    console.log(this.formregister.value.roleId);

}
}



}
interface Role {
  name: string,
  id: number
}

