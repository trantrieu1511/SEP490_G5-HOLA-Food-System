import { Injectable } from "@angular/core";
import { BehaviorSubject, ReplaySubject, take } from "rxjs";
import { environment } from "src/environments/environment";
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Router } from "@angular/router";
import { Tokens } from "../login/models/token";
import { Profile } from "../profile/models/profile";
import { Seller, SellerMessage } from "../modules/admin-routing-module/models/Seller";
import { Customer, CustomerMessage } from "../modules/admin-routing-module/models/Customer";
import { SoundService } from "./sound.service";
@Injectable({
  providedIn: 'root'
})
export class PresenceService {

  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;
  private onlineUsersSource = new BehaviorSubject<SellerMessage[]>([]);
  onlineUsers$ = this.onlineUsersSource.asObservable();
  private offlineUsersSource = new BehaviorSubject<SellerMessage[]>([]);
  offlineUsers$ = this.offlineUsersSource.asObservable();
  private onlineUsersSourcecus = new BehaviorSubject<SellerMessage[]>([]);
  onlineUserscus$ = this.onlineUsersSourcecus.asObservable();
  private offlineUsersSourcecus = new BehaviorSubject<SellerMessage[]>([]);
  offlineUserscus$ = this.offlineUsersSourcecus.asObservable();
  private listcusbysellerSource = new BehaviorSubject<CustomerMessage[]>([]);
  listcusbyseller$ = this.listcusbysellerSource.asObservable();

  private messageUsernameSource = new ReplaySubject<number>(1);
  messageUsername$ = this.messageUsernameSource.asObservable();
    private messageUsernameSellerSource = new ReplaySubject<number>(1);
    messageUsernameSeller$ = this.messageUsernameSellerSource.asObservable();
    private emailcustomermessageSource  = new ReplaySubject<string>(1);
    emailcustomermessage$ = this.emailcustomermessageSource.asObservable();
    private emaisellermessageSource = new ReplaySubject<string>(1);
    emaisellermessage$ = this.emaisellermessageSource.asObservable();
  constructor( private router: Router,private soundService:SoundService) { }

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


      this.hubConnection.on('UserIsOnline', (username: SellerMessage) => {

         this.offlineUserscus$.pipe(take(1)).subscribe(usernames => {
          this.offlineUsersSourcecus.next([...usernames.filter(x => x.email !== username.email)])

        })
        this.onlineUserscus$.pipe(take(1)).subscribe(usernames => {
          this.onlineUsersSourcecus.next([...usernames, username])
        })
        console.log(username.lastName+ ' has connect')
      })
      this.hubConnection.on('UserIsOffline', (username: SellerMessage) => {
        this.onlineUserscus$.pipe(take(1)).subscribe(usernames => {
          this.onlineUsersSourcecus.next([...usernames.filter(x => x.email !== username.email)])

        })
         this.offlineUserscus$.pipe(take(1)).subscribe(usernames => {
          this.offlineUsersSourcecus.next([...usernames,username])

        })
        console.log(username.lastName + ' disconnect')
      })

      this.hubConnection.on('ListCus', (cus: CustomerMessage[]) => {
        this.listcusbysellerSource.next(cus);

        })
        this.hubConnection.on('notIsRead', (username: number,emailcus:string) => {
          this.messageUsernameSource.next(username)
          debugger;
          this.emailcustomermessageSource.next(emailcus)
          if(username!=0){
            this.soundService.playAudioMessage();
          }


        })
        this.hubConnection.on('notIsReadSeller', (username: number,emailseller:string) => {
          this.messageUsernameSellerSource.next(username)
          debugger;
          this.emaisellermessageSource.next(emailseller)
          if(username!=0){
            this.soundService.playAudioMessage();
          }
        })


      // this.hubConnection.on('GetOnlineUsers', (usernames: Seller[]) => {
      //   this.onlineUsersSource.next(usernames);
      //  // console.log(usernames )
      // })
      // this.hubConnection.on('GetOfflineUsers', (usernames: Seller[]) => {
      //   this.offlineUsersSource.next(usernames);
      //   console.log(usernames )
      // })
      this.hubConnection.on('GetOnlineAndOfflineUsers', (onlineUsers: SellerMessage[], offlineUsers: SellerMessage[]) => {
        // Xử lý danh sách người dùng trực tuyến (onlineUsers) và người dùng offline (offlineUsers) ở đây

        this.onlineUsersSource.next(onlineUsers);
        this.offlineUsersSource.next(offlineUsers);
        console.log(offlineUsers)
      });
      this.hubConnection.on('GetOnlineAndOfflineUsersCUS', (onlineUsers: SellerMessage[], offlineUsers: SellerMessage[]) => {
        // Xử lý danh sách người dùng trực tuyến (onlineUsers) và người dùng offline (offlineUsers) ở đây
        this.offlineUsersSourcecus.next(offlineUsers);
        this.onlineUsersSourcecus.next(onlineUsers);
        console.log(offlineUsers);
      });
}

}
