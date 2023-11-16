import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Injectable, Injector} from '@angular/core';
import {Observable, of, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import { Router } from "@angular/router";
import { AuthService } from 'src/app/services/auth.service';

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
  //                     debugger;
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
  constructor(private router: Router,private inject: Injector) {}

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
}
