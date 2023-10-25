import {HttpClient, HttpErrorResponse, HttpHeaders, HttpRequest} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable, throwError, firstValueFrom, lastValueFrom} from 'rxjs';
import {catchError, map} from 'rxjs/operators';
import * as API from 'src/app/services/apiURL';
import {LoadingService} from '../shared-data-services/loading.service';


// @ts-ignore
import IPService from '/src/assets/config/IPService.json';

@Injectable()
export class iServiceBase {
    strIP_Service = '';
    strIP_GateWay = '';
    strVersion = '';
    strProjectName = '';

    constructor(public httpClient: HttpClient
        , public loadingService: LoadingService
        ) {
        this.strIP_Service = IPService.APISERVICE;
        this.strIP_GateWay = IPService.APIGATEWAY;
        this.strVersion = IPService.Version;
        this.strProjectName = IPService.PROJECT_NAME;

        // Set IP các service vào localStorage để dùng
        localStorage.setItem('APISERVICE', this.strIP_Service);
        localStorage.setItem('APIGATEWAY', this.strIP_GateWay);
        localStorage.setItem('VERSION', this.strVersion);
        localStorage.setItem('PROJECT_NAME', this.strProjectName);

    }

    // Lấy Ip cấu hình ở file này
    async getServiceList() {
        return IPService;
    }

    // getServiceList() {
    //     return this.httpClient.get<any>('assets/config/IPService.json')
    //         .toPromise()
    //         .then(res => res.data)
    //         .then(data => data);
    // }
    getOptionsRequest(ignoreLoading?: boolean, responseType?: string) {
        const options: any = {};
        if (ignoreLoading != undefined && ignoreLoading) {
            // this.loadingService.setIgnoreLoading();
            options.reportProgress = true;
        }
        if (responseType != undefined && responseType) {
            // this.loadingService.setIgnoreLoading();
            options.responseType = responseType;
        }
        return options;
    }

    async getDataByURLAsync(url : string, api : string, inputData : any, ignoreLoading?: boolean): Promise<any> {
        try {
            url = `${url}${api}`;
            const response = await this.httpClient.post(url, inputData, this.getOptionsRequest(ignoreLoading)).toPromise();
            document.body.style.cursor = 'default';

            return response;
        } catch (error) {
            document.body.style.cursor = 'default';
            console.log(error);
            return null;
        }
    }

    async getDataAsync(service : any, api : string, ignoreLoading?: boolean): Promise<any> {
        // Get IP và URL
        service = await this.getURLService(service);

        if (service == null) {
            return null;
        }

        const url = `${service}${api}`;
        const response = await this.httpClient.get(url, this.getOptionsRequest(ignoreLoading)).toPromise();
        document.body.style.cursor = 'default';


        return response;
    }

    async getDataAsyncByPostRequest(service : any, api : string, inputData : any, ignoreLoading?: boolean): Promise<any> {

        try {
            // Get IP và URL
            service = await this.getURLService(service);

            if (service == null) {
                return null;
            }

            const url = `${service}${api}`;
            const response = await this.httpClient.post(url, inputData, this.getOptionsRequest(ignoreLoading)).toPromise();
            document.body.style.cursor = 'default';

            return response;
        } catch (errorResponse) {
            document.body.style.cursor = 'default';

            //console.log(error);
            return errorResponse.error;
        }
    }

    async getDataWithParamsAsync(service : any, api : string, Params : any, ignoreLoading?: boolean): Promise<any> {

        // Get IP và URL
        service = await this.getURLService(service);

        if (service == null) {
            return null;
        }

        const url = `${service}${api}`;
        const response = await this.httpClient.get(url, {params: Params}).pipe(catchError(this.handleError)).toPromise();
        document.body.style.cursor = 'default';

        return response;
    }

    public getData(service : any, api : string, ignoreLoading?: boolean): Observable<any> {
        // try {

        //     // Get IP và URL
        //     service = this.getURLService(service);

        //     const url = `${service}${api}`;
        //     document.body.style.cursor = 'default';

        //     return this.httpClient.get(url, this.getOptionsRequest(ignoreLoading)).pipe(catchError(this.handleError));
        // } catch (error) {
        //     document.body.style.cursor = 'default';

        //     console.log(error);
        //     return null;
        // }

        service = this.getURLService(service);
        const url = `${service}${api}`;
        // Return the observable from the HTTP request
        const request = this.httpClient.get(url, this.getOptionsRequest(ignoreLoading)).pipe(
            catchError((error) => {
                console.error(error);
                // Instead of returning null, emit an error using throwError
                return throwError(error);
            })
        );

        request.subscribe(
            () => {
                // Request completed successfully, set cursor to 'default'
                document.body.style.cursor = 'default';
            },
            (error) => {
                // Request failed, handle error and set cursor to 'default'
                console.error(error);
                document.body.style.cursor = 'default';
            }
        );

        return request;
    }

    public downloadFilePDF(service : any, api : string, ignoreLoading?: boolean): Observable<Blob> {
        // try {

        //     // Get IP và URL
        //     service = this.getURLService(service);

        //     const url = `${service}${api}`;
        //     let headers = new HttpHeaders();
        //     headers = headers.set('Accept', 'application/pdf');


        //     return this.httpClient.get(url, {headers, responseType: 'blob'});
        // } catch (error) {
        //     document.body.style.cursor = 'default';

        //     console.log(error);
        //     return null;
        // }

        // Get IP và URL
        service = this.getURLService(service);
        const url = `${service}${api}`;

        // Set headers for PDF download
        const headers = new HttpHeaders().set('Accept', 'application/pdf');

        // Return the observable for PDF download
        const request = this.httpClient.get(url, { headers, responseType: 'blob' }).pipe(
            catchError((error) => {
                console.error(error);
                // Instead of returning null, emit an error using throwError
                return throwError(error);
            })
        );

        request.subscribe(
            () => {
                // Request completed successfully, set cursor to 'default'
                document.body.style.cursor = 'default';
            },
            (error) => {
                // Request failed, handle error and set cursor to 'default'
                console.error(error);
                document.body.style.cursor = 'default';
            }
        );

        return request;
    }

    public downloadFilePDFPost(service: any, api : string, param : any, ignoreLoading?: boolean): Observable<Blob> {
        // try {

        //     // Get IP và URL
        //     service = this.getURLService(service);

        //     const url = `${service}${api}`;
        //     let headers = new HttpHeaders();
        //     headers = headers.set('Accept', 'application/pdf');


        //     return this.httpClient.post(url, param, {headers, responseType: 'blob'});
        // } catch (error) {
        //     document.body.style.cursor = 'default';

        //     console.log(error);
        //     return null;
        // }

        // Get IP và URL
        service = this.getURLService(service);
        const url = `${service}${api}`;

        // Set headers for PDF download
        const headers = new HttpHeaders().set('Accept', 'application/pdf');

        // Return the observable for PDF download using POST
        const request = this.httpClient.post(url, param, { headers, responseType: 'blob' }).pipe(
            catchError((error) => {
                console.error(error);
                // Instead of returning null, emit an error using throwError
                return throwError(error);
            })
        );

        request.subscribe(
            () => {
                // Request completed successfully, set cursor to 'default'
                document.body.style.cursor = 'default';
            },
            (error) => {
                // Request failed, handle error and set cursor to 'default'
                console.error(error);
                document.body.style.cursor = 'default';
            }
        );

        return request;
    }

    public downloadFileByType(service : any, api : string, inputData : any, ignoreLoading?: boolean): Observable<Blob> {
        // try {

        //     // Get IP và URL
        //     service = this.getURLService(service);
        //     const url = `${service}${api}`;
        //     // Set header
        //     let headers = new HttpHeaders();
        //     if (inputData.exportType == 'pdf') {
        //         headers = headers.set('Accept', 'application/pdf');
        //     } else if (inputData.exportType == 'doc') {
        //         // headers = headers.set('Accept', 'application/doc');
        //     } else if (inputData.exportType == 'xls') {
        //         // headers = headers.set('Accept', 'application/xls');
        //     } else if (inputData.exportType == 'docx') {
        //         // headers = headers.set('Accept', 'application/docx');
        //     } else if (inputData.exportType == 'xls-only') {
        //         // headers = headers.set('Accept', 'application/xls');
        //     }

        //     return this.httpClient.post(url, inputData, {headers, responseType: 'blob'});
        // } catch (error) {
        //     document.body.style.cursor = 'default';

        //     console.log(error);
        //     return null;
        // }


        // Get IP và URL
        service = this.getURLService(service);
        const url = `${service}${api}`;

        // Set the appropriate header based on exportType
        let headers = new HttpHeaders();
        if (inputData.exportType === 'pdf') {
            headers = headers.set('Accept', 'application/pdf');
        } else if (inputData.exportType === 'doc') {
            headers = headers.set('Accept', 'application/msword');
        } else if (inputData.exportType === 'xls') {
            headers = headers.set('Accept', 'application/vnd.ms-excel');
        } else if (inputData.exportType === 'docx') {
            headers = headers.set('Accept', 'application/vnd.openxmlformats-officedocument.wordprocessingml.document');
        } else if (inputData.exportType === 'xls-only') {
            headers = headers.set('Accept', 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet');
        }
        // Make the HTTP request and specify responseType as blob
        const request = this.httpClient.post(url, inputData, { headers, responseType: 'blob' }).pipe(
            catchError((error) => {
                console.error(error);
                // Instead of returning null, emit an error using throwError
                return throwError(error);
            })
        );

        request.subscribe(
            () => {
                // Request completed successfully, set cursor to 'default'
                document.body.style.cursor = 'default';
            },
            (error) => {
                // Request failed, handle error and set cursor to 'default'
                console.error(error);
                document.body.style.cursor = 'default';
            }
        );

        return request;
    }

    public getDataByPostRequest(service : any, api : string, inputData : any,
            ignoreLoading?: boolean): Observable<any> {
        // try {

        //     // Get IP và URL
        //     service = this.getURLService(service);

        //     const url = `${service}${api}`;
        //     document.body.style.cursor = 'default';

        //     return this.httpClient.post(url, inputData, this.getOptionsRequest(ignoreLoading)).pipe(catchError(this.handleError));
        // } catch (error) {
        //     document.body.style.cursor = 'default';

        //     console.log(error);
        //     return null;
        // }

        // Get IP và URL
        service = this.getURLService(service);
        const url = `${service}${api}`;

        // Make the HTTP POST request
        const request = this.httpClient
            .post(url, inputData, this.getOptionsRequest(ignoreLoading))
            .pipe(
                catchError((error) => {
                console.error(error);
                // Instead of returning null, emit an error using throwError
                return throwError(error);
                })
        );
                
        request.subscribe(
            () => {
                // Request completed successfully, set cursor to 'default'
                document.body.style.cursor = 'default';
            },
            (error) => {
                // Request failed, handle error and set cursor to 'default'
                console.error(error);
                document.body.style.cursor = 'default';
            }
        );

        return request;
    }

    public getDataWithParams(service : any, api : string, Params : any, ignoreLoading?: boolean): Observable<any> {
        // try {

        //     // Get IP và URL
        //     service = this.getURLService(service);

        //     const url = `${service}${api}`;
        //     document.body.style.cursor = 'default';

        //     return this.httpClient.get(url, {params: Params}).pipe(catchError(this.handleError));
        // } catch (error) {
        //     document.body.style.cursor = 'default';

        //     console.log(error);
        //     return null;
        // }

        // Get IP và URL
        service = this.getURLService(service);
        const url = `${service}${api}`;

        // // Convert Params to HttpParams
        // const httpParams = new HttpParams({ fromObject: Params });

        // Make the HTTP GET request with query parameters
        const request = this.httpClient
            .get(url, { params: Params })
            .pipe(
                catchError((error) => {
                console.error(error);
                // Instead of returning null, emit an error using throwError
                return throwError(error);
                })
            );
        request.subscribe(
            () => {
                // Request completed successfully, set cursor to 'default'
                document.body.style.cursor = 'default';
            },
            (error) => {
                // Request failed, handle error and set cursor to 'default'
                console.error(error);
                document.body.style.cursor = 'default';
            }
        );

        return request;
    }


    async postDataAsync(service : any, api : string, inputData : any,
            ignoreLoading?: boolean, responseType?: string): Promise<any> {
        try {
            // Get IP và URL
            service = await this.getURLService(service);

            if (service == null) {
                return null;
            }
            const url = `${service}${api}`;
            
            const request = await firstValueFrom(this.httpClient.post(url, inputData, this.getOptionsRequest(ignoreLoading, responseType)))
            document.body.style.cursor = 'default';
            return request;
        } catch (errorResponse) {
            document.body.style.cursor = 'default';
            console.log(errorResponse);
            return errorResponse.error;
        }
    }

    public postData(service : any, api : string, inputData : any, ignoreLoading?: boolean): Observable<any> {
        // try {
        //     // Get IP và URL
        //     service = this.getURLService(service);

        //     const url = `${service}${api}`;
        //     document.body.style.cursor = 'default';
        //     return this.httpClient.post(url, inputData, this.getOptionsRequest(ignoreLoading)).pipe(catchError(this.handleError));
        // } catch (error) {
        //     document.body.style.cursor = 'default';

        //     console.log(error);
        //     return null;
        // }

        // Get IP và URL
        service = this.getURLService(service);
        const url = `${service}${api}`;
        document.body.style.cursor = 'default';

        // Make the HTTP POST request with inputData
        const request = this.httpClient
            .post(url, inputData, this.getOptionsRequest(ignoreLoading))
            .pipe(
                catchError((error) => {
                console.error(error);
                // Instead of returning null, emit an error using throwError
                return throwError(error);
                })
            );
        request.subscribe(
            () => {
                // Request completed successfully, set cursor to 'default'
                document.body.style.cursor = 'default';
            },
            (error) => {
                // Request failed, handle error and set cursor to 'default'
                console.error(error);
                document.body.style.cursor = 'default';
            }
        );

        return request;
    }

    public postFormData(service: any, api: string, inputData: any, ignoreLoading?: boolean): Observable<any> {
        // try {
        //     // Get IP và URL
        //     service = this.getURLService(service);

        //     const url = `${service}${api}`;
        //     document.body.style.cursor = 'default';

        //     return this.httpClient.post(url, inputData, this.getOptionsRequest(ignoreLoading)).pipe(catchError(this.handleError));
        // } catch (error) {
        //     document.body.style.cursor = 'default';

        //     console.log(error);
        //     return null;
        // }

         // Get IP và URL
        service = this.getURLService(service);
        const url = `${service}${api}`;

        // Make the HTTP POST request with inputData
        const request = this.httpClient
            .post(url, inputData, this.getOptionsRequest(ignoreLoading))
            .pipe(
                map(res => {
                    document.body.style.cursor = 'default';
                    return res;
                }),
                catchError((error) => {
                    console.error(error);
                    document.body.style.cursor = 'default';
                    // Instead of returning null, you can choose to re-throw the error or handle it as needed
                    throw error;
                })
            );
        // // Subscribe to the request and set cursor style when completed
        // request.subscribe(
        //     () => {
        //         // Request completed successfully, set cursor to 'default'
        //         document.body.style.cursor = 'default';
        //     },
        //     (error) => {
        //         // Request failed, handle error and set cursor to 'default'
        //         console.error(error);
        //         document.body.style.cursor = 'default';
        //     }
        // );

        return request;
    }

    public postDataURL(service: any, api: string, inputData: any, ignoreLoading?: boolean): Observable<any> {
        // try {

        //     // Get IP và URL
        //     service = this.getURLService(service);

        //     const url = `${service}${api}`;
        //     document.body.style.cursor = 'default';

        //     return this.httpClient.post(url, inputData, this.getOptionsRequest(ignoreLoading)).pipe(catchError(this.handleError));
        // } catch (error) {
        //     document.body.style.cursor = 'default';

        //     console.log(error);
        //     return null;
        // }

        // Get IP và URL
        service = this.getURLService(service);
        const url = `${service}${api}`;

        // Make the HTTP POST request with inputData
        const request = this.httpClient
            .post(url, inputData, this.getOptionsRequest(ignoreLoading))
            .pipe(catchError(this.handleError));
        // Subscribe to the request and set cursor style when completed
        request.subscribe(
            () => {
                // Request completed successfully, set cursor to 'default'
                document.body.style.cursor = 'default';
            },
            (error) => {
                // Request failed, handle error and set cursor to 'default'
                console.error(error);
                document.body.style.cursor = 'default';
            }
        );
    
        return request;
    }

    async putDataAsync(service: any, api: string, inputData: any, ignoreLoading?: boolean): Promise<any> {
        try {

            // Get IP và URL
            service = await this.getURLService(service);

            if (service == null) {
                return null;
            }

            const url = `${service}${api}`;
            const response = await firstValueFrom(this.httpClient.put(url, inputData, this.getOptionsRequest(ignoreLoading)));
            document.body.style.cursor = 'default';
            return response;
        } catch (errorResponse) {
            document.body.style.cursor = 'default';
            //console.log(errorResponse);
            return errorResponse.error;
        }
    }

    public putData(service: any, api: string, inputData: any, ignoreLoading?: boolean): Observable<any> {
        // try {
        //     // Get IP và URL
        //     service = this.getURLService(service);

        //     const url = `${service}${api}`;
        //     document.body.style.cursor = 'default';
        //     return this.httpClient.put(url, inputData, this.getOptionsRequest(ignoreLoading)).pipe(catchError(this.handleError));
        // } catch (error) {
        //     document.body.style.cursor = 'default';
        //     console.log(error);
        //     return null;
        // }

        // Get IP và URL
        service = this.getURLService(service);
        const url = `${service}${api}`;

        // Make the HTTP PUT request with inputData
        const request = this.httpClient
            .put(url, inputData, this.getOptionsRequest(ignoreLoading))
            .pipe(catchError(this.handleError));
        // Subscribe to the request and set cursor style when completed
        request.subscribe(
            () => {
                // Request completed successfully, set cursor to 'default'
                document.body.style.cursor = 'default';
            },
            (error) => {
                // Request failed, handle error and set cursor to 'default'
                console.error(error);
                document.body.style.cursor = 'default';
            }
        );
    
        return request;
    }

    async deleteDataAsync(service: any, api: string, ignoreLoading?: boolean): Promise<any> {
        // Get IP và URL
        try {
            service = await this.getURLService(service);

            if (service == null) {
                return null;
            }

            const url = `${service}${api}`;
            const response = await firstValueFrom(this.httpClient.delete(url));
            document.body.style.cursor = 'default';
            return response;
        } catch (errorResponse) {
            document.body.style.cursor = 'default';
            return errorResponse.error;
        }
    }

    public deleteData(service: any, api: string, ignoreLoading?: boolean): Observable<any> {
        // try {
        //     // Get IP và URL
        //     service = this.getURLService(service);

        //     const url = `${service}${api}`;
        //     document.body.style.cursor = 'default';
        //     return this.httpClient.delete(url).pipe(catchError(this.handleError));
        // } catch (error) {
        //     document.body.style.cursor = 'default';
        //     console.log(error);
        //     return null;
        // }
        
        // Get IP và URL
        service = this.getURLService(service);
       
        const url = `${service}${api}`;

        // Make the HTTP DELETE request
        const request = this.httpClient
            .delete(url, this.getOptionsRequest(ignoreLoading))
            .pipe(catchError(this.handleError))
            ;

        // Subscribe to the request and set cursor style when completed
        request.subscribe(
            () => {
                // Request completed successfully, set cursor to 'default'
                document.body.style.cursor = 'default';
            },
            (error) => {
                // Request failed, handle error and set cursor to 'default'
                console.error(error);
                document.body.style.cursor = 'default';
            }
        );
    
        return request;

    }

    public uploadAsync(service: any, api: string, inputData: any, ignoreLoading?: boolean) {
        // Get IP và URL
        service = this.getURLService(service);

        const url = `${service}${api}`;


        return new HttpRequest('POST', url, inputData);
    }

    // Đoạn này chỉ cần lưu IP của API gateway rồi lần sau lấy ra cộng lại thôi
    getURLService(phanhe: any) {
        try {

            switch (phanhe) {
                case API.PHAN_HE.USER: {
                    return localStorage.getItem('APISERVICE') + '/users/';
                }
                case API.PHAN_HE.ROLE: {
                    return localStorage.getItem('APISERVICE') + '/roles/';
                }
                case API.PHAN_HE.ORDER: {
                    return localStorage.getItem('APISERVICE') + '/orders/';
                }
                case API.PHAN_HE.VOUCHER: {
                    return localStorage.getItem('APISERVICE') + '/vouchers/';
                }
                case API.PHAN_HE.POST: {
                    return localStorage.getItem('APISERVICE') + '/posts/';
                }
                case API.PHAN_HE.TEST: {
                    return localStorage.getItem('APISERVICE') + '/home/';
                }
                case API.PHAN_HE.POSTMODERATORMANAGEPOST: {
                    return localStorage.getItem('APISERVICE') + '/manage/';
                }
                case API.PHAN_HE.SHIPPER: {
                    return localStorage.getItem('APISERVICE') + '/shipper/';
                }
                
                case API.PHAN_HE.HOME: {
                    return localStorage.getItem('APISERVICE') + '/home/';
                }
                case API.PHAN_HE.SHOP_DETAIL: {
                    return localStorage.getItem('APISERVICE') + '/shopdetail/';
                }
                case API.PHAN_HE.CART: {
                    return localStorage.getItem('APISERVICE') + '/cart/';
                }
                case API.PHAN_HE.FOOD: {
                    return localStorage.getItem('APISERVICE') + '/foods/';
                }
                case API.PHAN_HE.CHECKOUT: {
                    return localStorage.getItem('APISERVICE') + '/checkout/';
                }
                default: {
                    return '';
                }
            }
        } catch (error) {
            console.log('Lỗi lấy IP APT Gate way' + error);
            return null;
        }
    }

    protected extractData(res: Response): any {
        const body = res;
        return body || {};
    }

    protected extractDataNoLoading(res: Response): any {
        const body = res;
        return body || {};
    }

    protected extractDataWParams(res: Response): any {
        const body = res;
        return body || {};
    }

    // protected handleError(error: Response | any): any {
    //     let errMsg;
    //     if (error instanceof Response) {
    //         const body = JSON.stringify(error) || '';
    //         const err = JSON.parse(body).error || '';
    //         errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
    //     } else {
    //         errMsg = error.message ? error.message : error.toString();
    //     }
    //     console.log(errMsg);
    //     return Observable.throw(errMsg);
    // }

    protected handleErrorWParams(error: Response | any): any {
        let errMsg;
        if (error instanceof Response) {
            const body = JSON.parse(JSON.stringify(error)) || '';
            const err = body.error;
            errMsg = `${error.status} - ${error.statusText || ''} ${err}`;
        } else {
            errMsg = error.message ? error.message : error.toString();
        }
        console.log(errMsg);
        return throwError(errMsg);
    }

    handleError(error: HttpErrorResponse) {
        let errorMessage = 'Unknown error!';
        if (error.error instanceof ErrorEvent) {
            // Client-side errors
            errorMessage = `Error: ${error.error.message}`;
        } else {
            // Server-side errors
            errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
        }
        window.alert(errorMessage);
        return throwError(errorMessage);
    }

    formatMessageError(response: any): string{
        var message = "";
        if(response.errors.validationErrors && response.errors.validationErrors.length > 0){
            const validationErrors = response.errors.validationErrors;
            validationErrors.forEach(element => {
                message += element.field + "\n";
                element.messages.forEach((mess, index) => {
                    message += "\t" + mess + "\n";
                });
            });
        }

        if(response.errors.systemErrors && response.errors.systemErrors.length > 0){
            const systemErrors = response.errors.systemErrors;
            systemErrors.forEach(mess => {
                message += mess + "\n";
            });
        }
        return message.trim();
    }
}
