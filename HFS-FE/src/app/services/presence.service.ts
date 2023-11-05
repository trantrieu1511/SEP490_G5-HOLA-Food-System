import { Injectable } from "@angular/core";
import { BehaviorSubject, take } from "rxjs";
import { environment } from "src/environments/environment";
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Router } from "@angular/router";
import { Tokens } from "../login/models/token";
import { Profile } from "../modules/customer-routing-module/models/profile";
import { Seller } from "../modules/admin-routing-module/models/Seller";
@Injectable({
  providedIn: 'root'
})
export class PresenceService {

  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;
  private onlineUsersSource = new BehaviorSubject<Seller[]>([]);
  onlineUsers$ = this.onlineUsersSource.asObservable();
  private offlineUsersSource = new BehaviorSubject<Seller[]>([]);
  offlineUsers$ = this.offlineUsersSource.asObservable();
  constructor( private router: Router) { }

  createHubConnection(token: string) {

    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'presence', {
        accessTokenFactory: () => token
      })
      .withAutomaticReconnect()
      .build()
      debugger;
      this.hubConnection
      .start()
      .then(() => {
        console.log('Kết nối SignalR thành công');
        // Thực hiện các thao tác khác sau khi kết nối thành công
      })
      .catch(error => console.log('Lỗi khi kết nối SignalR: ' + error));


      this.hubConnection.on('UserIsOnline', (username: Seller) => {
        debugger;
        this.onlineUsers$.pipe(take(1)).subscribe(usernames => {
          this.onlineUsersSource.next([...usernames, username])
        })
         this.offlineUsers$.pipe(take(1)).subscribe(usernames => {
          this.offlineUsersSource.next([...usernames.filter(x => x.email !== username.email)])

        })
        console.log(username.lastName+ ' has connect')
      })
      this.hubConnection.on('UserIsOffline', (username: Seller) => {
        this.onlineUsers$.pipe(take(1)).subscribe(usernames => {
          this.onlineUsersSource.next([...usernames.filter(x => x.email !== username.email)])

        })
         this.offlineUsers$.pipe(take(1)).subscribe(usernames => {
          this.offlineUsersSource.next([...usernames,username])

        })
        console.log(username.lastName + ' disconnect')
      })
      // this.hubConnection.on('GetOnlineUsers', (usernames: Seller[]) => {
      //   this.onlineUsersSource.next(usernames);
      //  // console.log(usernames )
      // })
      // this.hubConnection.on('GetOfflineUsers', (usernames: Seller[]) => {
      //   this.offlineUsersSource.next(usernames);
      //   console.log(usernames )
      // })
      this.hubConnection.on('GetOnlineAndOfflineUsers', (onlineUsers: Seller[], offlineUsers: Seller[]) => {
        // Xử lý danh sách người dùng trực tuyến (onlineUsers) và người dùng offline (offlineUsers) ở đây
        debugger;
        this.onlineUsersSource.next(onlineUsers);
        this.offlineUsersSource.next(offlineUsers);
      });
}

}
