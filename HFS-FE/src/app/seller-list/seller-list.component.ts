import { Component } from '@angular/core';
import { SellerChatBox } from './models/seller-chatbox';
import { PresenceService } from '../services/presence.service';

@Component({
  selector: 'app-seller-list',
  templateUrl: './seller-list.component.html',
  styleUrls: ['./seller-list.component.scss']
})
export class SellerListComponent {
  usersOnline: SellerChatBox[] = [];
  miniUser:SellerChatBox[] = [];

  dataUser = [
    {userName: 'hoainam10th', displayName:'Nguyen Hoai Nam'},
    {userName: 'ubuntu', displayName:'Tran Hoai Nam'},
    {userName: 'lisa', displayName:'Nguyen Minh Bac'},
    {userName: 'tony', displayName:'Nguyen Hoai Phong'},
    {userName: 'agutech', displayName:'Technology'},
    {userName: 'android', displayName:'Android 11'}
  ]
  isSellerListVisible: boolean = false;

  toggleSellerList() {
    this.isSellerListVisible = !this.isSellerListVisible;
    console.log(this.isSellerListVisible);
  }
  constructor(public presence: PresenceService) { }

  ngOnInit(): void {

  }

  selectUser(user: any) {
    switch ((this.usersOnline.length+1) % 2) {
      case 1: {
        debugger;
        var u = this.usersOnline.find(x => x.seller.userName === user.userName);
        if (u) {
          this.usersOnline = this.usersOnline.filter(x => x.seller.userName !== user.userName);
          this.usersOnline.push(u);
        } else {
          this.usersOnline.push(new SellerChatBox(user, 250));
        }
        break;
      }
      case 0: {
        var u = this.usersOnline.find(x => x.seller.userName === user.userName);
        if (u) {
          this.usersOnline = this.usersOnline.filter(x => x.seller.userName !== user.userName);
          this.usersOnline.push(u);
        } else {
          this.usersOnline.push(new SellerChatBox(user, 250 + 325));
        }
        break;
      }
      default: {
        console.log("No");
        break;
      }
    }
  }

  removeChatBox(event: string) {
    this.usersOnline = this.usersOnline.filter(x => x.seller.userName !== event);
  }

  miniChatBox(user: any){
    this.miniUser.push(new SellerChatBox(user, 250));
  }

  restoreUser(user: SellerChatBox){
    this.miniUser = this.miniUser.filter(x => x.seller.userName !== user.seller.userName);
    this.selectUser(user.seller);
  }

  removeMiniUser(user: any){
    this.miniUser = this.miniUser.filter(x => x.seller.userName !== user.user.userName);
  }
}
