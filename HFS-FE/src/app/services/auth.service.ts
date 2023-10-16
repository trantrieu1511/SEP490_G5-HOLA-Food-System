import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { environment } from 'src/environments/environment';
import { Tokens } from '../login/models/token';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
user:User;
  private path = environment.apiUrl
  constructor(private httpClient: HttpClient) { }

  login(model: any){
    return this.httpClient.post(this.path+'home/login', model).pipe(
      map((res:Tokens)=>{
        const token = res;
        debugger;
        if(token){
          this.setCurrentUser(token);

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
      localStorage.setItem('user', JSON.stringify(this.user));
      sessionStorage.setItem("JWT",token.token);
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
