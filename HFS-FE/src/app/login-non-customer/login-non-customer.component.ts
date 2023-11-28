import { AfterViewInit, Component, NgZone, OnInit, Renderer2 } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CredentialResponse } from 'google-one-tap';
import { environment } from '../../environments/environment';
import { AuthService, User } from '../services/auth.service';

import { Subscription } from 'rxjs';
import { PasswordLengthValidator, PasswordNumberValidator, PasswordUpperValidator } from '../login/Restricted-login.directive';

import {
  iComponentBase,
  iServiceBase, mType,
  ShareData,
  iFunction
} from 'src/app/modules/shared-module/shared-module';
import { MessageService } from 'primeng/api';
@Component({
  selector: 'app-login-non-customer',
  templateUrl: './login-non-customer.component.html',
  styleUrls: ['./login-non-customer.component.scss']
})
export class LoginNonCustomerComponent extends iComponentBase  implements OnInit, AfterViewInit {
  user: User;
  form: FormGroup;
  error: string;
  isCaptchaTouched = false;
  isPasswordTouched=false;
  unsuccessfulLoginAttempts: number = 0;
  captchaImage: string = '';
  captchaText:string;
  showLoginForm: boolean = true;
  captchacheck:string;
  private client_Id = environment.clientId;
  constructor(private router: Router,
    public renderer: Renderer2,
    private _ngZone: NgZone,
     public messageService: MessageService,
    private service: AuthService,
    // private cdr: ChangeDetectorRef
  ) {
    super(messageService);
  }
  ngAfterViewInit(): void {
    // this.loadGoogleLibrary();
    // this.cdr.detectChanges();
    const script1 = this.renderer.createElement('script');
    script1.src = `https://accounts.google.com/gsi/client`;
    script1.async = `true`;
    script1.defer = `true`;
    this.renderer.appendChild(document.body, script1);
  }

  FormFirst() {
    this.form = new FormGroup({
      email: new FormControl('', Validators.email),
      password: new FormControl('', [Validators.required]),
      captcha: new FormControl('')
    })
    this.refreshCaptcha();
  }
  ngOnInit(): void {
    this.checkLoggedIn();
    this.captchacheck = localStorage.getItem("captcha");
    this.unsuccessfulLoginAttempts=parseInt(this.captchacheck);
    // localStorage.removeItem('user');
     localStorage.removeItem('chatboxusers')
    //localStorage.removeItem('user');
    sessionStorage.clear();
    this.service.error$.subscribe(error => {
      this.error = error;
    //this.showMessage(mType.error, "Notification", error, 'app-non-login');
    })
    this.FormFirst();
    this.loadGoogleLibrary();
    this.service.user$.subscribe(user => {
      this.user = user;


    });
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

  loadGoogleLibrary() {
    // @ts-ignore
    window.onGoogleLibraryLoad = () => {
      console.log(document.getElementById("buttonDiv"));
      // @ts-ignore
      google.accounts.id.initialize({
        client_id: this.client_Id,
        callback: this.handleCredentialResponse.bind(this),
        auto_select: false,
        cancel_on_tap_outside: true
      });
      // @ts-ignore

      google.accounts.id.renderButton(
        // @ts-ignore
        document.getElementById("buttonDiv"),
        { theme: "outline", size: "large", width: 100 }
      );
      // @ts-ignore
      google.accounts.id.prompt((notification: PromptMomentNotification) => { });
    };
  }
  async handleCredentialResponse(response: CredentialResponse) {
    //
    await this.service.logingoogle(response.credential).subscribe(
      (x: any) => {
        if(x===undefined){
          this.showMessage(mType.error, "Notification", "You have been banned due to violations, please contact us to resolve", 'app-non-login');
        }
        this._ngZone.run(() => {
          switch (this.user.role) {
            case "CU":
              this.router.navigateByUrl('/homepage');
              break;
            case "AD":
            case "SE":
            case "SH":
            case "PM":
            case "MM":
              this.router.navigateByUrl('/HFSBusiness');
              break;
            default: this.router.navigateByUrl('/login-2');
          }
        })
      },
      (error: any) => {
        console.log(error);
      }
    );
  }


  refreshCaptcha() {
    // Generate a random alphanumeric string for the CAPTCHA
    this.captchaText = this.generateRandomString(6); // Change the length as needed

    // Create a canvas element and draw the text on it
    const canvas = document.createElement('canvas');
    const ctx = canvas.getContext('2d')!;
    canvas.width = 150;
    canvas.height = 50;

    // Customize the appearance of the CAPTCHA text
    ctx.fillStyle = '#000';
    ctx.font = '30px Arial';
    ctx.fillText(this.captchaText, 10, 40);

    // Convert the canvas to a data URL and set it as the image source
    this.captchaImage = canvas.toDataURL();
  }

  generateRandomString(length: number): string {
    const characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
    let result = '';
    for (let i = 0; i < length; i++) {
      result += characters.charAt(Math.floor(Math.random() * characters.length));
    }
    return result;
  }
  async onSubmit() {
    //this.formSubmitAttempt = false;
    this.captchacheck = localStorage.getItem("captcha");
    this.unsuccessfulLoginAttempts=parseInt(this.captchacheck);
    if (this.form.valid) {
      if (this.unsuccessfulLoginAttempts>2&& this.captchaText !== this.form.value.captcha) {

        this.refreshCaptcha();
        this.showMessage(mType.error, "Notification", "CAPTCHA verification failed. Please try again.", 'app-non-login');
        return;
      }
      try {
        //
        this.service.loginnotcus(this.form.value).subscribe(res => {
          if(res===undefined&&this.user===null){
            this.showMessage(mType.error, "Notification", this.error, 'app-non-login');
          }else{
          //this.toastr.success('Login success');
          // const userData = localStorage.getItem('user');
          // this.user = JSON.parse(userData);
            switch (this.user.role) {
              case "CU":
                this.router.navigateByUrl('/homepage');
                break;
              case "AD":
              case "SE":
              case "SH":
              case "PM":
              case "MM":
                this.router.navigateByUrl('/HFSBusiness');
                break;
              default: this.router.navigateByUrl('/login-2');
            }
          }


        }, error => {
          console.log(error);
          if (error.status === 401) {
            //this.error = 'Email hoặc mật khẩu không chính xác.';
          } else {
           // this.error = 'Đã xảy ra lỗi khi đăng nhập. Vui lòng thử lại sau.';
            this.showMessage(mType.error, "Notification", "An error occurred while logging in. Please try again later.", 'app-non-login');
          }
        })

      } catch (err) {

      }
    } else {
      this.refreshCaptcha();
    //  this.showMessage(mType.error, "Notification", "CAPTCHA verification failed. Please try again", 'app-non-login');
    }
  }

  showCaptchaError() {
    const captchanameControl = this.form.get('captcha');
    if (captchanameControl.value === '' && captchanameControl.touched) {
      console.log('Captcha is required!');
    }
  }
  showPasswordError() {
    const passwordControl = this.form.get('password');
    if (passwordControl.value === '' && passwordControl.touched) {
      console.log('Password is required!');
    }
  }
  //  async loginfb() {
  //    FB.login(async (result:any) => {
  //      //
  //        await this.service.loginfacebook(result.authResponse.accessToken).subscribe(
  //          (x:any) => {
  //            this._ngZone.run(() => {
  //              this.router.navigate(['/logout']);
  //            })},
  //          (error:any) => {
  //              console.log(error);
  //            }
  //          );
  //    }, { scope: 'email' });

  //  }

  checkLoggedIn(){
    const user = this.service.getUserInfor();
    console.log(user)
    if(!user)
      return;
    const role = this.service.getUserInfor().role;

    this.redirectToPageByRole(role);
  }

  redirectToPageByRole(role: string){
    switch (role) {
      case 'CU':
        this.router.navigate(['/homepage']);
        break;
      case 'AD':
      case 'SE':
      case 'SH':
      case 'PM':
      case 'MM':
        this.router.navigateByUrl('/HFSBusiness');
        break;

      default:
        window.location.reload();

    }
  }
}
