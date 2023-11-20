import { Injectable } from "@angular/core";
import { BehaviorSubject, ReplaySubject, take } from "rxjs";
import { environment } from "src/app/environments/environment";
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
  private listcusbysellerMessSource = new BehaviorSubject<CustomerMessage[]>([]);
  listcusbysellerMess$ = this.listcusbysellerSource.asObservable();
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
      ////dcm dubber casi lon; de cl ma` bug
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
        console.log(username + ' disconnect')
      })

      this.hubConnection.on('ListCus', (cus: CustomerMessage[]) => {
        this.listcusbysellerSource.next(cus);
        console.log(cus)

        })
        this.hubConnection.on('notIsReadCustomer', (countmess: number,emailcus:string) => {
          // this.messageUsernameSource.next(countmess)
          // debugger;
          // this.emailcustomermessageSource.next(emailcus)
          const targetEmail =emailcus;
          const currentUsers = this.onlineUsersSourcecus.getValue();
         const targetUserIndex = currentUsers.findIndex(user => user.email === targetEmail);

             if (targetUserIndex !== -1) {
             const targetUser = currentUsers[targetUserIndex];
               targetUser.countMessageNotIsRead=countmess;


                 const updatedUsers = [...currentUsers];
                   updatedUsers[targetUserIndex] = targetUser;


                this.onlineUsersSourcecus.next(updatedUsers);

            } else {
              console.log('User not found');
            }
          if(countmess!=0){
            this.soundService.playAudioMessage();
          }


        })
        this.hubConnection.on('notIsReadSeller', (countmess: number,emailseller:string) => {
          const targetEmail = emailseller;
          const currentUsers = this.listcusbysellerSource.getValue();
          const targetUser = currentUsers.find(user => user.email === targetEmail);

          if (targetUser) {
            // Kiểm tra kiểu dữ liệu và giá trị của countmess trước khi cập nhật
            if (typeof countmess === 'number' && !isNaN(countmess)) {
              targetUser.countMessageNotIsRead = countmess;
            } else {
              console.log('Invalid countmess:', countmess);
            }

            this.listcusbysellerSource.next([...currentUsers]);

            if (countmess !== 0) {
              this.soundService.playAudioMessage();
            }
          } else {
            console.log('User not found');
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
