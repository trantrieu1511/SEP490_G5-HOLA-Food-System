import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { PasswordLengthValidator, PasswordMatch, PasswordNumberValidator, PasswordUpperValidator } from '../login/Restricted-login.directive';

@Component({
  selector: 'app-forgot',
  templateUrl: './forgot.component.html',
  styleUrls: ['./forgot.component.scss']
})
export class ForgotComponent implements  OnInit {
  formforgot:FormGroup;
  formpassword:FormGroup;
  formCofirmforgot:FormGroup;
  showForgotForm: number = 1;
code:string

  constructor(private router: Router,
    private acroute: ActivatedRoute,
    private service: AuthService
   // private cdr: ChangeDetectorRef
  ){

   }
  ngOnInit(): void {
    this.acroute.queryParams.subscribe(params => {
      const userId = params['userId'];
      const code = params['code'];

      // Sử dụng userId và code theo nhu cầu của bạn
      console.log('userId:', userId);
      console.log('code:', code);
      debugger;

      this.code=code;
      if (code === undefined || code === null) {
        console.log('Biến code là undefined hoặc null');
      } else {
        console.log('Biến code không phải là undefined hoặc null');

      }
  })
   this.FormFirst();
   this.FormPassword();
   this.formCofirmforgot=new FormGroup({
    confirm: new FormControl(this.code),
    userId: new FormControl("1")
  })
  this.service.confirmforgot(this.formCofirmforgot.value).subscribe(res=>{
    this.service.showforgot$.subscribe(showforgot => {
      this.showForgotForm = showforgot;})

  }, error=>{
    console.log(error);

  })
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

      try {
        debugger;
        this.service.sendforgot(this.formforgot.value).subscribe(res=>{
          this.service.showforgot$.subscribe(showforgot => {
            this.showForgotForm = showforgot;})

        }, error=>{
          console.log(error);

        })

      } catch (err) {

      }
    }
    }

    async Submitpassword() {
      if (this.formpassword.valid) {

        try {
          debugger;
          this.service.changeforgot(this.formpassword.value).subscribe(res=>{
            this.service.showforgot$.subscribe(showforgot => {
              this.showForgotForm = showforgot;})

          }, error=>{
            console.log(error);

          })

        } catch (err) {

        }
      }
    }
  }

