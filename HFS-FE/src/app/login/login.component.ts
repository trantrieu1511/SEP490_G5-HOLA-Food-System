import { Component, NgZone, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CredentialResponse } from 'google-one-tap';
import { environment } from '../../environments/environment';
import { AuthService } from '../services/auth.service';

declare const FB: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements  OnInit{
 form:FormGroup;
private client_Id=environment.clientId;
 constructor(private router: Router,
  private _ngZone: NgZone,
  private service: AuthService
){

 }

 FormFirst(){
  this.form = new FormGroup({
    userName: new FormControl('', Validators.email),
    password: new FormControl('', [Validators.required, Validators.minLength(6), Validators.maxLength(12)])
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
  await this.service.LoginWithGoogle(response.credential).subscribe(
    (x:any) => {
      this._ngZone.run(() => {
        this.router.navigate(['/logout']);
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
    console.log("ok");
    } catch (err) {

    }
  } else {
    //this.formSubmitAttempt = true;
  }
}


async login() {
  FB.login(async (result:any) => {
    debugger;
      await this.service.LoginWithFacebook(result.authResponse.accessToken).subscribe(
        (x:any) => {
          this._ngZone.run(() => {
            this.router.navigate(['/logout']);
          })},
        (error:any) => {
            console.log(error);
          }
        );
  }, { scope: 'email' });

}
}
