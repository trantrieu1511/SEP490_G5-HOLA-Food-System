import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { environment } from 'src/environments/environment';
import { Tokens } from '../login/models/token';
import { map } from 'rxjs';
import { Register } from '../register/models/register';
import { iFunction } from './../modules/shared-module/functions/iFunction';
import { JwtService } from './app.jwt.service';


@Injectable({
  providedIn: 'root'
})
export class AuthService {
  loginErrorMessage: string = '';
  user: User;
  private userSubject: BehaviorSubject<User> = new BehaviorSubject<User>(null);
  user$: Observable<User> = this.userSubject.asObservable();
  private errorSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);
  private errorregisterSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);
  private showSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(null);
  private showforgotSubject: BehaviorSubject<number> = new BehaviorSubject<number>(null);
  private showconfirmSubject: BehaviorSubject<number> = new BehaviorSubject<number>(null);
  error$: Observable<string> = this.errorSubject.asObservable();
  errorregister$: Observable<string> = this.errorregisterSubject.asObservable();
  showregister$: Observable<boolean> = this.showSubject.asObservable();
  showforgot$: Observable<number> = this.showforgotSubject.asObservable();
  showconfirm$: Observable<number> = this.showconfirmSubject.asObservable();
  private path = environment.apiUrl
  constructor(
    private httpClient: HttpClient,
    private jwtService: JwtService,
    ) { }

  login(model: any) {


    return this.httpClient.post(this.path + 'home/logincustomer', model).pipe(
      map((res: any) => {
        const token = res;
        if (token.success) {
          //this.setCurrentUser(token);
          this.jwtService.saveTokenResponse(res);
          this.setCurrentUser();
          localStorage.removeItem("captcha");
        } else {
          this.errorSubject.next(token.message.toString());
          let captchastring = localStorage.getItem("captcha");
          if (captchastring === null) {
            captchastring = "1";
            localStorage.setItem("captcha",captchastring);

          }else{
             let captcha =parseInt(captchastring,10);
              captcha++;
            localStorage.setItem("captcha",captcha.toString());
          }

        }
      })
    )
  }
  loginnotcus(model: any) {
    debugger
    return this.httpClient.post(this.path + 'home/loginnotcustomer', model).pipe(
      map((res: any) => {
        const token = res;
        //
        if (token.success) {
          this.jwtService.saveTokenResponse(res);
          this.setCurrentUser();
          localStorage.removeItem("captcha");

        } else {
          this.errorSubject.next('Email Or Password Was Invalid');
          let captchastring = localStorage.getItem("captcha");
          if (captchastring === null) {
            captchastring = "1";
            localStorage.setItem("captcha",captchastring);

          }else{
             let captcha =parseInt(captchastring,10);
              captcha++;
            localStorage.setItem("captcha",captcha.toString());
          }

        }
      })
    )
  }
  logingoogle(credentials: string): Observable<any> {

    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.post(this.path + 'home/logingoogle', JSON.stringify(credentials), { headers: header, withCredentials: true }).pipe(

      map((res: any) => {
        //
        if (res.success) {
          this.jwtService.saveTokenResponse(res);
          this.setCurrentUser();

        }else {
          this.errorSubject.next(res.message.toString());
        }
      })
    )
  }
  register(model: any) {
    //

    return this.httpClient.post(this.path + 'home/registercustomer', model).pipe(

      map((res: Register) => {
        const register = res;
        //
        if (register.success) {
          console.log("tao tài khoản thành công");

          this.showSubject.next(false);
        } else {
          this.errorregisterSubject.next(register.message.toString());
          this.showSubject.next(true);
        }
      })
    )
  }
  RefreshToken() {
    //


  }
  registerseller(model: any) {
    //

    return this.httpClient.post(this.path + 'home/registerseller', model).pipe(

      map((res: Register) => {
        const register = res;
        //
        if (register.success) {
          console.log("tao tài khoản thành công");

          this.showSubject.next(false);
        } else {
          this.errorregisterSubject.next(register.message.toString());
          this.showSubject.next(true);
        }
      })
    )
  }
  registershipper(model: any) {
    //

    return this.httpClient.post(this.path + 'home/registershipper', model).pipe(

      map((res: Register) => {
        const register = res;
        //
        if (register.success) {
          console.log("tao tài khoản thành công");

          this.showSubject.next(false);
        } else {
          this.errorregisterSubject.next(register.message.toString());
          this.showSubject.next(true);
        }
      })
    )
  }
  loginfacebook(tokenconfirm: string): Observable<any> {
    //
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.post("https://localhost:7016/api/Ok/" + "LoginWithFacebook", JSON.stringify(tokenconfirm), { headers: header, withCredentials: true }).pipe(

      map((res: Tokens) => {
        const token = res;
        //
        if (token) {
          //this.setCurrentUser(token);

        }
      })
    )
  }

  confirm(tokenconfirm: string): Observable<any> {
    //
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.post("https://localhost:7016/api/Role/" + "confirmation", JSON.stringify(tokenconfirm), { headers: header, withCredentials: true }).pipe(

      map((res: Tokens) => {
        const token = res;
        //
        if (token) {
          console.log("ok");

        }
      })
    )
  }

  sendforgot(model: any) {
    //
    // https://localhost:7016/home/sendforgot
    return this.httpClient.post(this.path + "home/sendforgot", model).pipe(
      map((res: Tokens) => {
        const token = res;
        //
        if (token.success) {
          this.showforgotSubject.next(0);

        } else {
          this.errorSubject.next('Email này không có trong hệ thống');
          this.showforgotSubject.next(1);
        }
      })
    )
  }
  confirmforgot(model: any) {
    //
    // https://localhost:7016/home/confirmforgot
    return this.httpClient.post(this.path+ "home/confirmforgot", model).pipe(
      map((res: Register) => {
        const ok = res;
        //
        if (ok.success) {
          this.showforgotSubject.next(2);

        } else {
          this.errorSubject.next('Email này không có trong hệ thống');
          this.showforgotSubject.next(1);
        }
      })
    )
  }
  changeforgot(model: any) {
    //
    // https://localhost:7016/home/confirmforgot
    return this.httpClient.post(this.path + "home/changepassword", model).pipe(
      map((res: Register) => {
        const ok = res;
        //
        if (ok.success) {
          this.showforgotSubject.next(3);

        } else {
          this.errorSubject.next('Email này không có trong hệ thống');
          this.showforgotSubject.next(1);
        }
      })
    )
  }
  setCurrentUser() { //token: Tokens
    //
    //if (token) {
      const token = this.jwtService.getToken();
      const data = this.getDecodedToken(token);//copy token to jwt.io see .role

      this.user = {
        email: data['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
        role: data['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
        name: data['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
        userId: data['userId'],
        exp: +data['exp'],
      };

      localStorage.setItem("user", JSON.stringify(this.user));
      this.userSubject.next(this.user);



    //}
  }
  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }

  getRole() {
    // let token = sessionStorage.getItem("JWT");
    // if (token) {
    //   const data = this.getDecodedToken(token);
    //   return data['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
    // }
    // return null;
  }

  confirmemail(model: any) {
    //
    // https://localhost:7016/home/confirmforgot
    return this.httpClient.post(this.path +"home/confirm", model).pipe(
      map((res: Register) => {
        const ok = res;
        //
        if (ok.success) {
          this.showconfirmSubject.next(1);
        } else {
          this.errorSubject.next('This email is not in the system');
          this.showconfirmSubject.next(0);
        }
      })
    )
  }



  getUserInfor(): User{
    // const token = this.jwtService.getToken();
    // if(!token)
    //   return null;
    // const data = this.getDecodedToken(token);



    const user = localStorage.getItem("user");

    if(!user)
      return null;
    return  JSON.parse(localStorage.getItem("user"));
  }

  refreshToken(param: any): Observable<any>{
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.post(this.path + 'auths/refresh', param, {headers: header, withCredentials: true})
  }

}

export interface User {
  email: string;
  role: string;
  name: string;
  userId: string;
  exp: number;
}

export interface LoginDto{
  token: string;
  refreshToken: RefreshToken;
}

export interface RefreshToken{
  token: string;
  expired: Date;
}
