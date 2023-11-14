import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable, of, throwError} from 'rxjs';
import {catchError} from 'rxjs/operators';
import { Router } from "@angular/router";

@Injectable()
export class MyHttpInterceptor implements HttpInterceptor {

	constructor(private router: Router) {
    }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const jwt = sessionStorage.getItem('JWT');

        if (!!jwt) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${jwt}`
                },
            });
        }

        return next.handle(request).pipe(
            catchError((err: any) => {
                    if (err.status === 401) {
                        //UnAuthorized
						this.router.navigateByUrl(`/login`);
						window.location.reload();
                        return of(err.message);
                    } else {
                        //console.log("interceptor", err.error);
                        //const error = err.error.message || err.statusText;
                        return throwError(() => err);
                    }
                },
            ),
        );
    }

}
