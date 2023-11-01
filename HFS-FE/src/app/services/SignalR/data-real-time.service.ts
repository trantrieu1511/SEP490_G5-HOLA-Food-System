import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { iServiceBase } from 'src/app/modules/shared-module/shared-module';
import * as API from "../apiURL";

@Injectable({
  providedIn: 'root'
})
export class DataRealTimeService {

  constructor(private iServiceBase: iServiceBase
    ){}

  private hubConnection: signalR.HubConnection

  public startConnection = () => {
    const service = this.iServiceBase.getURLService(API.PHAN_HE.HUB);
       
    const url = `${service}${API.API_HUB.DATA_REALTIME}`;

    this.hubConnection = new signalR.HubConnectionBuilder()
                            .withUrl(url)
                            .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }
  
  public addTransferDataListener = (service: any, api: string, inputData?: any, method: boolean = true, ignoreLoading?: boolean): Promise<any> => {
    return new Promise((resolve, reject) => {
      this.hubConnection.on('dataRealTime', async () => {
        try {
          const result = await this.loadProdData(service, api, inputData, ignoreLoading, method);
          resolve(result);
        } catch (error) {
          reject(error);
        }
      });
    });
    // this.hubConnection.on('dataRealTime', () => {
    //   console.log(1);
    // });
  }

  async loadProdData(service: any, api: string, inputData?: any, ignoreLoading?: boolean, method?: boolean){
    if(method){
      return await this.iServiceBase.getDataWithParamsAsync(service, api, inputData, ignoreLoading);
    }

    return await this.iServiceBase.postDataAsync(service, api, inputData, ignoreLoading);
  }
}
