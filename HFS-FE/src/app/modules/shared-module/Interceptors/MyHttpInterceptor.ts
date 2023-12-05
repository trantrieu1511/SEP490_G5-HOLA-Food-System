import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Injectable, Injector} from '@angular/core';
import {BehaviorSubject, Observable, of, throwError} from 'rxjs';
import { catchError, filter, last, map, switchMap, take, finalize } from 'rxjs/operators';
import { Router } from "@angular/router";
import { AuthService } from 'src/app/services/auth.service';
import { AuthenticatedResponse } from '../models/authenticated-response.model';
import { iFunction, iServiceBase } from '../shared-module';
import * as API from '../../../services/apiURL';
import { JwtService } from 'src/app/services/app.jwt.service';


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
  private timesRefresh = 0;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);
  private refreshHandle = this.refreshTokenSubject.asObservable();

  constructor(private router: Router,
    private inject: Injector,
    private iServiceBase: iServiceBase,
    private jwtService: JwtService,
    private iFunction: iFunction,
    private authService: AuthService
    ) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // const token = sessionStorage.getItem('JWT');

    // if (!!token) {
    //   const timetoken = Number(sessionStorage.getItem('timetoken'));
    //   const currentTime = Date.now() / 1000;

      // if (timetoken < currentTime) {
      //   let service = this.inject.get(AuthService);
      //   this.router.navigate(['/login']);
      //   return throwError('Token expired');
      // }
      this.refreshHandle.subscribe(res => {
        if(res != null){
          next.handle(this.addTokenHeader(request, res.token));
        }
      })

      const token = this.iFunction.getCookie("token");
      if(token && token != ""){
        request = request.clone({
          setHeaders: {
            Authorization: `Bearer ${token}`
          },
        });
      }

    //}

    return next.handle(request).pipe(
      catchError(err => {
        
        if (err instanceof HttpErrorResponse && (err.status === 401 || err.status === 0)) {
          // Xử lý token hết hạn sau khi gửi request
          return this.handleAuthError(request, next, err);
        }
        return throwError(err);

      })
    );
  }

  private handleAuthError(request: HttpRequest<any>, next: HttpHandler, err: any){

    //if(!request.url.includes('auths/refresh')){
    if(this.timesRefresh != 1){
      this.timesRefresh++;
      this.refreshTokenSubject.next(null);

      if(this.iFunction.getCookie("token")){
        return of(err.message);
      }

      const refreshToken: string = this.iFunction.getCookie("refreshToken");
      if (!refreshToken) {
        
        localStorage.removeItem('user');
        this.jwtService.removeToken();
        this.router.navigate(['/login']);
        window.location.reload();
        
        return of(err.message);

      }

      const credentials = {
        refreshToken: refreshToken
      };
      this.authService.refreshToken(credentials).subscribe({
        next: (x : any) => {

          this.jwtService.saveTokenResponse(x);

          this.refreshTokenSubject.next(x.token);

          return next.handle(this.addTokenHeader(request, x.token))
        },
        error: (err: any) => {
          this.iServiceBase.postData(
            API.PHAN_HE.AUTH,
            API.API_AUTH.REVOKE_TOKEN,
            credentials,
            false
          ).subscribe({
            next: (a: any) => {
              
              localStorage.removeItem('user');
              this.jwtService.removeToken();
              this.router.navigate(['/login']);
              //window.location.reload();
              return of(err.message);
            },
            error: (err: any) => {
              localStorage.removeItem('user');
              this.jwtService.removeToken();
              this.router.navigate(['/login']);
              // Handle error here
              return of(err.message);
            }
          
          })
        }
      })
      //return of("Refresh token loading..")
    }
    else{
      this.timesRefresh = 0;
      return throwError(() => new Error('Non Authentications Error'));
    }

    return this.refreshTokenSubject.pipe(
      filter(token => token !== null),
      take(1),
      switchMap((token) => next.handle(this.addTokenHeader(request, token)))
    );
  }

  private addTokenHeader(request: HttpRequest<any>, token: string){

    return request.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      },
    });
  }
}
