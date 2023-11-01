import { AfterViewInit, Component, NgZone, OnInit, Renderer2 } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CredentialResponse } from 'google-one-tap';
import { environment } from '../../environments/environment';
import { AuthService, User } from '../services/auth.service';

import { Subscription } from 'rxjs';
import { PasswordLengthValidator, PasswordNumberValidator, PasswordUpperValidator } from '../login/Restricted-login.directive';


@Component({
  selector: 'app-login-non-customer',
  templateUrl: './login-non-customer.component.html',
  styleUrls: ['./login-non-customer.component.scss']
})
export class LoginNonCustomerComponent implements  OnInit,AfterViewInit{
  user:User;
  form:FormGroup;
  error: string;
  showLoginForm: boolean = true;

 private client_Id=environment.clientId;
  constructor(private router: Router,
   public renderer: Renderer2,
   private _ngZone: NgZone,
   private service: AuthService,
  // private cdr: ChangeDetectorRef
 ){

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

  FormFirst(){
   this.form = new FormGroup({
     email: new FormControl('', Validators.email),
     password: new FormControl('', [Validators.required, Validators.minLength(3), PasswordLengthValidator(),
      PasswordUpperValidator(),PasswordNumberValidator()])
   })
 }
  ngOnInit(): void {
    localStorage.clear();
  sessionStorage.clear();
   this.service.error$.subscribe(error => {
     this.error = error;})
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
     google.accounts.id.prompt((notification: PromptMomentNotification) => {});
   };
 }
 async handleCredentialResponse(response: CredentialResponse) {
   debugger;
   await this.service.logingoogle(response.credential).subscribe(
     (x:any) => {
       this._ngZone.run(() => {
         switch(this.user.role){
           case "AD":
             this.router.navigateByUrl('/HFSBusiness/admin');
             break;
             case "SE":
               this.router.navigateByUrl('/HFSBusiness/seller');
               break;
               case "CU":
               this.router.navigateByUrl('/');
               break;

               case "SH":
                 this.router.navigateByUrl('/HFSBusiness/shipper');
                 break;
                 case "PM":
                   this.router.navigateByUrl('/HFSBusiness/PostModerator');
                   break;

                   case "MM":
                   this.router.navigateByUrl('/HFSBusiness/MenuModerator');
                   break;

                   default: this.router.navigateByUrl('/login');
 }
       })},
     (error:any) => {
         console.log(error);
       }
     );
 }



 async onSubmit() {
   //this.formSubmitAttempt = false;
   if (this.form.valid) {

     try {
       debugger;
       this.service.loginnotcus(this.form.value).subscribe(res=>{
         //this.toastr.success('Login success');
         // const userData = localStorage.getItem('user');
         // this.user = JSON.parse(userData);
         switch(this.user.role){
          case "AD":
            this.router.navigateByUrl('/HFSBusiness/admin');
            break;
            case "SE":
              this.router.navigateByUrl('/HFSBusiness/seller');
              break;
              case "CU":
              this.router.navigateByUrl('/');
              break;

              case "SH":
                this.router.navigateByUrl('/HFSBusiness/shipper');
                break;
                case "PM":
                  this.router.navigateByUrl('/HFSBusiness/postmoderator');
                  break;

                  case "MM":
                  this.router.navigateByUrl('/HFSBusiness/postmoderator');
                  break;

                  default: this.router.navigateByUrl('/login');
         }


       }, error=>{
         console.log(error);
         if (error.status === 401) {
           this.error = 'Email hoặc mật khẩu không chính xác.';
         } else {
           this.error= 'Đã xảy ra lỗi khi đăng nhập. Vui lòng thử lại sau.';
         }
       })

     } catch (err) {

     }
   } else {
     //this.formSubmitAttempt = true;
   }
 }


//  async loginfb() {
//    FB.login(async (result:any) => {
//      debugger;
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
 }
