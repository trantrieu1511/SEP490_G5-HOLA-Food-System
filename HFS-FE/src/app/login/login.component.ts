import { Component, NgZone, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CredentialResponse } from 'google-one-tap';
import { environment } from '../../environments/environment';
import { AuthService, User } from '../services/auth.service';

declare const FB: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements  OnInit{
  user:User;
 form:FormGroup;
private client_Id=environment.clientId;
 constructor(private router: Router,
  private _ngZone: NgZone,
  private service: AuthService
){

 }

 FormFirst(){
  this.form = new FormGroup({
    email: new FormControl('', Validators.email),
    password: new FormControl('', [Validators.required, Validators.minLength(3), Validators.maxLength(12)])
  })
}
 ngOnInit(): void {
  this.FormFirst();

  // @ts-ignore
  window.onGoogleLibraryLoad = () => {
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
  // await this.service.LoginWithGoogle(response.credential).subscribe(
  //   (x:any) => {
  //     this._ngZone.run(() => {
  //       this.router.navigate(['/logout']);
  //     })},
  //   (error:any) => {
  //       console.log(error);
  //     }
  //   );
}



async onSubmit() {
  //this.formSubmitAttempt = false;
  if (this.form.valid) {

    try {
      debugger;
      this.service.login(this.form.value).subscribe(res=>{
        //this.toastr.success('Login success');
        const userData = localStorage.getItem('user');


        this.user = JSON.parse(userData);
        switch(this.user.role){
                  case 1:
                    this.router.navigateByUrl('/HFSBusiness/admin');
                    break;
                    case 2:
                      this.router.navigateByUrl('/HFSBusiness/seller');
                      break;
                      case 3:
                      this.router.navigateByUrl('/');
                      break;

                      case 4:
                        this.router.navigateByUrl('/HFSBusiness/shipper');
                        break;
                        case 5:
                          this.router.navigateByUrl('/HFSBusiness/PostModerator');
                          break;

                          case 6:
                          this.router.navigateByUrl('/HFSBusiness/MenuModerator');
                          break;
                          default:this.router.navigateByUrl('/');
        }


      }, error=>{
        console.log(error);

      })

    } catch (err) {

    }
  } else {
    //this.formSubmitAttempt = true;
  }
}


// async login() {
//   FB.login(async (result:any) => {
//     debugger;
//       await this.service.LoginWithFacebook(result.authResponse.accessToken).subscribe(
//         (x:any) => {
//           this._ngZone.run(() => {
//             this.router.navigate(['/logout']);
//           })},
//         (error:any) => {
//             console.log(error);
//           }
//         );
//   }, { scope: 'email' });

// }
}
