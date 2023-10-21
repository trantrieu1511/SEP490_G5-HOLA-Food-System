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
error$: Observable<string> = this.errorSubject.asObservable();
  private path = environment.apiUrl
  constructor(private httpClient: HttpClient) { }

  login(model: any){
    return this.httpClient.post(this.path+'home/login', model).pipe(
      map((res:Tokens)=>{
        const token = res;
        debugger;
        if(token.success){
          this.setCurrentUser(token);

        }else{
          this.errorSubject.next('Email hoặc mật khẩu không chính xác');

        }
      })
    )
  }

  logingoogle(credentials: string): Observable<any>{
    debugger;
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

    return this.httpClient.post(this.path+'home/register', model).pipe(

      map((res:Register)=>{
        const register = res;
        debugger;
        if(register.success){
         console.log("tao tài khoản thành công");

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
  setCurrentUser(token: Tokens){
    debugger;
    if(token){
      const data = this.getDecodedToken(token.token);//copy token to jwt.io see .role
      this.user = {
        email: data['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'],
        role: +data['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'],
        name: data['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name'],
        userId: data['userId'],
        exp: +data['exp']
      };
     console.log(this.user)
      // Save user data to localStorage
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
  role: number;
  name: string;
  userId: string;
  exp: number;
}
