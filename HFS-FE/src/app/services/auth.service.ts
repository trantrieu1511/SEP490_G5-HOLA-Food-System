import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';

import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private loginStatus = new BehaviorSubject<boolean>(this.loggedIn())
  private username = new BehaviorSubject<string>(localStorage.getItem('username')!)
  private path = environment.apiUrl
  constructor(private httpClient: HttpClient) { }

  public signOutExternal = () => {
      localStorage.removeItem("token");
      console.log("token deleted")
  }

  LoginWithGoogle(credentials: string): Observable<any> {
    debugger;
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.post(this.path + "LoginWithGoogle", JSON.stringify(credentials), { headers: header, withCredentials: true });
  }
  LoginWithFacebook(credentials: string): Observable<any> {
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.post("https://localhost:7016/api/Ok/" + "LoginWithFacebook", JSON.stringify(credentials), { headers: header, withCredentials: true });
  }
  login(loginModel:any): Observable<any> {
    debugger;
    const header = new HttpHeaders().set('Content-type', 'application/json');
console.log(header);
    return this.httpClient.post(this.path + 'Login', JSON.stringify(loginModel), { headers: header, withCredentials: true })

  }

  getClient(): Observable<any> {
    const header = new HttpHeaders().set('Content-type', 'application/json');
    return this.httpClient.get(this.path + "GetColorList", { headers: header, withCredentials: true });
  }



  saveUsername(username:string) {
    localStorage.setItem('username', username)
  }

  loggedIn(): boolean {
    if (localStorage.getItem('token')) {
      return true;
    }
    return false;
  }

  setLoginStatus(val:any) {
    this.loginStatus.next(val)
  }

  setUsername(val:any) {
    this.username.next(val)
  }

}
