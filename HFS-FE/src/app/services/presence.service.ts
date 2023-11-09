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
  private onlineUsersSourcecus = new BehaviorSubject<Seller[]>([]);
  onlineUserscus$ = this.onlineUsersSourcecus.asObservable();
  private offlineUsersSourcecus = new BehaviorSubject<Seller[]>([]);
  offlineUserscus$ = this.offlineUsersSourcecus.asObservable();
  constructor( private router: Router) { }

  createHubConnection(token: string) {

    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'presence', {
        accessTokenFactory: () => token
      })
      .withAutomaticReconnect()
      .build()

      this.hubConnection
      .start()
      .then(() => {
        console.log('Kết nối SignalR thành công');
        // Thực hiện các thao tác khác sau khi kết nối thành công
      })
      .catch(error => console.log('Lỗi khi kết nối SignalR: ' + error));


      this.hubConnection.on('UserIsOnline', (username: Seller) => {


         this.offlineUserscus$.pipe(take(1)).subscribe(usernames => {
          this.offlineUsersSourcecus.next([...usernames.filter(x => x.email !== username.email)])

        })
        this.onlineUserscus$.pipe(take(1)).subscribe(usernames => {
          this.onlineUsersSourcecus.next([...usernames, username])
        })
        console.log(username.lastName+ ' has connect')
      })
      this.hubConnection.on('UserIsOffline', (username: Seller) => {
        this.onlineUserscus$.pipe(take(1)).subscribe(usernames => {
          this.onlineUsersSourcecus.next([...usernames.filter(x => x.email !== username.email)])

        })
         this.offlineUserscus$.pipe(take(1)).subscribe(usernames => {
          this.offlineUsersSourcecus.next([...usernames,username])

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

        this.onlineUsersSource.next(onlineUsers);
        this.offlineUsersSource.next(offlineUsers);
      });
      this.hubConnection.on('GetOnlineAndOfflineUsersCUS', (onlineUsers: Seller[], offlineUsers: Seller[]) => {
        // Xử lý danh sách người dùng trực tuyến (onlineUsers) và người dùng offline (offlineUsers) ở đây

        this.onlineUsersSourcecus.next(onlineUsers);
        this.offlineUsersSourcecus.next(offlineUsers);
      });
}

}
