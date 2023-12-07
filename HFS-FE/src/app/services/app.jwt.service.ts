import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { iFunction, iServiceBase } from '../modules/shared-module/shared-module';
import * as API from '../services/apiURL';
import { AuthService, LoginDto } from 'src/app/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class JwtService {
  private jwtHelper: JwtHelperService;

  constructor(
    private iFunction: iFunction,
    private iServiceBase: iServiceBase,
    ) {

      this.jwtHelper = new JwtHelperService();
    }
    
    getToken(): string | null {
      return this.iFunction.getCookie("token")
    }
  
    getRefreshToken(): string | null {
      return this.iFunction.getCookie("refreshToken")
    }
  
    decodeToken(token: string): any {
      return this.jwtHelper.decodeToken(token);
    }
  
    getExpirationDate(token: string): Date | null {
      const expirationDate = this.jwtHelper.getTokenExpirationDate(token);
      return expirationDate;
    }
  
    isTokenExpired(): boolean{
      return this.jwtHelper.isTokenExpired(this.getToken());
    }
  
    saveTokenResponse(res: LoginDto){
      const exp = this.getExpirationDate(res.token);
      console.log(exp + ':' + new Date(res.refreshToken.expired))
      this.iFunction.setCookie('token', res.token, exp)
      this.iFunction.setCookie('refreshToken', res.refreshToken.token, new Date(res.refreshToken.expired))
    }

    removeToken(){
      this.iFunction.deleteCookie('token');
      this.iFunction.deleteCookie('refreshToken');
    }
  
    async tryRefreshingTokens() {
      //const refreshToken: string = localStorage.getItem("refreshToken");
      const refreshToken: string = this.iFunction.getCookie("refreshToken");
      if (!refreshToken) { 
        return false;
      }
      
      const credentials = { 
        refreshToken: refreshToken 
      };
      let isRefreshSuccess: boolean;
  
      const response = await this.iServiceBase.postDataAsync(
        API.PHAN_HE.AUTH,
        API.API_AUTH.REFRESH_TOKEN,
        credentials,
        true
      );
  
      if (response && response.message === 'Success') {
        // localStorage.setItem("jwt", response.token);
        // localStorage.setItem("refreshToken", response.refreshToken);
        this.saveTokenResponse(response);
  
        isRefreshSuccess = true;
      } else {
        isRefreshSuccess = false;
      }
      return isRefreshSuccess;
    }
  
}