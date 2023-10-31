import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { environment } from 'src/environments/environment';
import { Tokens } from '../login/models/token';
import { map } from 'rxjs';
import { Register } from '../register/models/register';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  loginErrorMessage: string = '';
user:User;
private userSubject: BehaviorSubject<User> = new BehaviorSubject<User>(null);
user$: Observable<User> = this.userSubject.asObservable();
private errorSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);
private errorregisterSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);
private showSubject: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(null);
private showforgotSubject: BehaviorSubject<number> = new BehaviorSubject<number>(null);
error$: Observable<string> = this.errorSubject.asObservable();
errorregister$: Observable<string> = this.errorregisterSubject.asObservable();
showregister$: Observable<boolean> = this.showSubject.asObservable();
showforgot$: Observable<number> = this.showforgotSubject.asObservable();
  private path = environment.apiUrl
  constructor(private httpClient: HttpClient) { }

  login(model: any){
    return this.httpClient.post(this.path+'home/logincustomer', model).pipe(
      map((res:Tokens)=>{
        const token = res;
        if(token.success){
          this.setCurrentUser(token);

        }else{
          this.errorSubject.next('Email hoặc mật khẩu không chính xác');

        }
      })
    )
  }
  loginnotcus(model: any){
    debugger
    return this.httpClient.post(this.path+'home/loginnotcustomer', model).pipe(
      map((res:Tokens)=>{
        const token = res;
        if(token.success){
          this.setCurrentUser(token);

        }else{
          this.errorSubject.next('Email hoặc mật khẩu không chính xác');

        }
      })
    )
  }
  logingoogle(credentials: string): Observable<any>{

    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.post(this.path+'home/logingoogle', JSON.stringify(credentials),{ headers: header, withCredentials: true }).pipe(

      map((res:Tokens)=>{
        const token = res;
        debugger;
        if(token){
          this.setCurrentUser(token);

        }
      })
    )
  }
  register(model: any){
    debugger;

    return this.httpClient.post(this.path+'home/registercustomer', model).pipe(

      map((res:Register)=>{
        const register = res;
        debugger;
        if(register.success){
         console.log("tao tài khoản thành công");

         this.showSubject.next(false);
        }else{
          this.errorregisterSubject.next(register.message.toString());
          this.showSubject.next(true);
        }
      })
    )
  }
  registerseller(model: any){
    debugger;

    return this.httpClient.post(this.path+'home/registerseller', model).pipe(

      map((res:Register)=>{
        const register = res;
        debugger;
        if(register.success){
         console.log("tao tài khoản thành công");

         this.showSubject.next(false);
        }else{
          this.errorregisterSubject.next(register.message.toString());
          this.showSubject.next(true);
        }
      })
    )
  }
  registershipper(model: any){
    debugger;

    return this.httpClient.post(this.path+'home/registershipper', model).pipe(

      map((res:Register)=>{
        const register = res;
        debugger;
        if(register.success){
         console.log("tao tài khoản thành công");

         this.showSubject.next(false);
        }else{
          this.errorregisterSubject.next(register.message.toString());
          this.showSubject.next(true);
        }
      })
    )
  }
  loginfacebook(tokenconfirm: string): Observable<any>{
    debugger;
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.post("https://localhost:7016/api/Ok/" + "LoginWithFacebook", JSON.stringify(tokenconfirm),{ headers: header, withCredentials: true }).pipe(

      map((res:Tokens)=>{
        const token = res;
        debugger;
        if(token){
          this.setCurrentUser(token);

        }
      })
    )
  }

  confirm(tokenconfirm: string): Observable<any>{
    debugger;
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.post("https://localhost:7016/api/Role/" + "confirmation", JSON.stringify(tokenconfirm),{ headers: header, withCredentials: true }).pipe(

      map((res:Tokens)=>{
        const token = res;
        debugger;
        if(token){
          console.log("ok");

        }
      })
    )
  }

  sendforgot(model: any){
    debugger;
   // https://localhost:7016/home/sendforgot
    return this.httpClient.post(this.path+ "home/sendforgot",model ).pipe(
      map((res:Tokens)=>{
        const token = res;
        debugger;
        if(token.success){
          this.showforgotSubject.next(0);

        }else{
          this.errorSubject.next('Email này không có trong hệ thống');
          this.showforgotSubject.next(1);
        }
      })
    )
  }
  confirmforgot(model: any){
    debugger;
   // https://localhost:7016/home/confirmforgot
    return this.httpClient.post("https://localhost:7016/home/confirmforgot",model ).pipe(
      map((res:Register)=>{
        const ok = res;
        debugger;
        if(ok.success){
          this.showforgotSubject.next(2);

        }else{
          this.errorSubject.next('Email này không có trong hệ thống');
          this.showforgotSubject.next(1);
        }
      })
    )
  }
  changeforgot(model: any){
    debugger;
   // https://localhost:7016/home/confirmforgot
    return this.httpClient.post(this.path+"home/changepassword",model ).pipe(
      map((res:Register)=>{
        const ok = res;
        debugger;
        if(ok.success){
          this.showforgotSubject.next(3);

        }else{
          this.errorSubject.next('Email này không có trong hệ thống');
          this.showforgotSubject.next(1);
        }
      })
    )
  }
  setCurrentUser(token: Tokens){
    debugger;
    if(token){
      const data = this.getDecodedToken(token.token);//copy token to jwt.io see .role
      this.user = {
        email: data['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
        role: data['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
        name: data['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
        userId: data['userId'],
        exp: +data['exp']
      };
     console.log(this.user)

      this.userSubject.next(this.user);

      sessionStorage.setItem("JWT",token.token);
      sessionStorage.setItem("role",this.user.role.toString());
      sessionStorage.setItem("timetoken",this.user.exp.toString());
      sessionStorage.setItem("userId",this.user.userId.toString());
    }
  }
  getDecodedToken(token: string) {
    return JSON.parse(atob(token.split('.')[1]));
  }


}

export interface User {
  email: string;
  role: string;
  name: string;
  userId: string;
  exp: number;
}

