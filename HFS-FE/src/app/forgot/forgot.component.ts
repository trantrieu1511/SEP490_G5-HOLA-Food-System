import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PasswordLengthValidator, PasswordMatch, PasswordNumberValidator, PasswordUpperValidator } from '../login/Restricted-login.directive';
import * as API from "../services/apiURL";
import {
  iComponentBase,
  iServiceBase, mType,
  ShareData,
  iFunction
} from 'src/app/modules/shared-module/shared-module';
import { MessageService } from 'primeng/api';
import { environment } from 'src/environments/environment';
import { RouterTestingHarness } from '@angular/router/testing';
@Component({
  selector: 'app-forgot',
  templateUrl: './forgot.component.html',
  styleUrls: ['./forgot.component.scss']
})
export class ForgotComponent extends iComponentBase implements  OnInit {
  formforgot:FormGroup;
  formpassword:FormGroup;
  formCofirmforgot:FormGroup;
  showForgotForm: number = 1;
code:string

  constructor(private router: Router,
    private acroute: ActivatedRoute,
    public messageService: MessageService,
    private service: AuthService,
    private iServiceBase: iServiceBase,
   // private cdr: ChangeDetectorRef
  ){
    super(messageService);
  }
  ngOnInit(): void {
    this.acroute.queryParams.subscribe(params => {
      const userId = params['userId'];
      const code = params['code'];

      // Sử dụng userId và code theo nhu cầu của bạn
      console.log('userId:', userId);
      console.log('code:', code);


      this.code=code;
      if (code === undefined || code === null) {
        console.log('Biến code là undefined hoặc null');
      } else {
        console.log('Biến code không phải là undefined hoặc null');
           this.formcheck();
      }
  })
   this.FormFirst();
   this.FormPassword();
  //  this.formCofirmforgot=new FormGroup({
  //   confirm: new FormControl(this.code),
  //   userId: new FormControl("1")
  // })
  // this.service.confirmforgot(this.formCofirmforgot.value).subscribe(res=>{
  //   this.service.showforgot$.subscribe(showforgot => {
  //     this.showForgotForm = showforgot;})

  // }, error=>{
  //   console.log(error);

  // })
  }
  FormFirst(){
    this.formforgot = new FormGroup({
      email: new FormControl('', Validators.email)

    })
  }
  FormPassword(){
    const passwordControl = new FormControl('', [
      Validators.required,
      PasswordLengthValidator(),
      PasswordUpperValidator(),
      PasswordNumberValidator()
    ]);
    this.formpassword = new FormGroup({
      confirm: new FormControl(this.code),
      password: passwordControl,
      confirmPassword: new FormControl('',[
        Validators.required,
        PasswordMatch(passwordControl)
      ])
    })
  }
  async onSubmit() {
    //this.formSubmitAttempt = false;

    if (this.formforgot.valid) {
     this.sendForgot();
    }
    }

    async Submitpassword() {
      if (this.formpassword.valid) {
        try{
          const param={
            "confirm":  this.code,
         "password":this.formpassword.value.password,
         "confirmPassword":this.formpassword.value.confirmPassword
           }
          let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.HOME, API.API_USER.CHANGEFORGOT, param);
          if (response && response.success === true) {
           this.showForgotForm=3;
            this.showMessage(mType.success, "Notification", `Change password successfully`, 'app-forgot');
            console.log(response);

          } else {
            this.showMessage(mType.error, "Notification", response.message, 'app-forgot');
            this.showForgotForm=1;
          }
        } catch (err) {
          this.showMessage(mType.error, "Notification", err, 'app-forgot');
        }
      }
    }
    async formcheck(){
     const param={
      "confirm":  this.code,
      "userId":"1"
     }
       try{
      let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.HOME, API.API_USER.FORGOT, param);
      if (response && response.success === true) {
        this.showForgotForm=2;
        this.showMessage(mType.success, "Notification", `Check successfully`, 'app-forgot');
        console.log(response);

      } else {
        this.showMessage(mType.error, "Notification", response.message, 'app-forgot');
        this.showForgotForm=1;
      }
    } catch (err) {
      this.showMessage(mType.error, "Notification", err, 'app-forgot');
    }

    }
    async sendForgot(){
      
      const param={
        "email":  this.formforgot.value.email,

       }
        try{
       let response = await this.iServiceBase.getDataAsyncByPostRequest(API.PHAN_HE.HOME, API.API_USER.SENDFORGOT, param);
       if (response && response.success === true) {
        this.showForgotForm=0;
         this.showMessage(mType.success, "Notification", `Send successfully`, 'app-forgot');
         console.log(response);

       } else {
         this.showMessage(mType.error, "Notification", response.message, 'app-forgot');
         this.showForgotForm=1;
       }
     } catch (err) {
       this.showMessage(mType.error, "Notification", err, 'app-forgot');
     }

     }
  }
