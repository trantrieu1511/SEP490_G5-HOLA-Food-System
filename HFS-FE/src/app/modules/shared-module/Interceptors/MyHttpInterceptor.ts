import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Injectable, Injector} from '@angular/core';
import {Observable, of, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import { Router } from "@angular/router";
import { AuthService } from 'src/app/services/auth.service';
import { AuthenticatedResponse } from '../models/authenticated-response.model';
import { iServiceBase } from '../shared-module';
import * as API from '../../../services/apiURL';
import { JwtHelperService } from '@auth0/angular-jwt';


@Injectable()
export class MyHttpInterceptor implements HttpInterceptor {

	// constructor(private router: Router) {
  //   }

  //   intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

  //       const jwt = sessionStorage.getItem('JWT');

  //       if (!!jwt) {
  //           request = request.clone({
  //               setHeaders: {
  //                   Authorization: `Bearer ${jwt}`
  //               },
  //           });
  //       }

  //       return next.handle(request).pipe(
  //           catchError((err: any) => {
  //                   if (err.status === 401) {
  //                     
  //                       //UnAuthorized
	// 					this.router.navigateByUrl(`/login`);
	// 					window.location.reload();
  //                       return of(err.message);
  //                   } else {
  //                       //console.log("interceptor", err.error);
  //                       //const error = err.error.message || err.statusText;
  //                       return throwError(() => err);
  //                   }
  //               },
  //           ),
  //       );
  //   }
  constructor(private router: Router,
    private inject: Injector,
    private iServiceBase: iServiceBase,
    private jwtHelper: JwtHelperService
    ) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const token = sessionStorage.getItem('JWT');

    if (!!token) {
      const timetoken = Number(sessionStorage.getItem('timetoken'));
      const currentTime = Date.now() / 1000;

      if (timetoken < currentTime) {
        let service = this.inject.get(AuthService);
        this.router.navigate(['/login']);
        return throwError('Token expired');
      }

      // const token = localStorage.getItem("jwt");
      // if (token && this.jwtHelper.isTokenExpired(token)){
      //   const isRefreshSuccess = this.tryRefreshingTokens(token); 
      //   if (!isRefreshSuccess) { 
      //     this.router.navigate(["login"]); 
      //   }
      // }
      
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        },
      });
    }

    return next.handle(request).pipe(
      catchError((error) => {
        if (error.status === 401 && error.error === 'Token expired') {
          // Xử lý token hết hạn sau khi gửi request
          this.router.navigate(['/login']);
        }
        return throwError(error);
      })
    );
  }

  private async tryRefreshingTokens(token: string) {
    const refreshToken: string = localStorage.getItem("refreshToken");
    if (!token || !refreshToken) { 
      return false;
    }
    
    const credentials = { 
      accessToken: token, 
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
      localStorage.setItem("jwt", response.token);
      localStorage.setItem("refreshToken", response.refreshToken);
      isRefreshSuccess = true;
    } else {
      isRefreshSuccess = false;
    }
    return isRefreshSuccess;
  }
}
