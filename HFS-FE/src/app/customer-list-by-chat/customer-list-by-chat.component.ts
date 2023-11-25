import { Component, OnInit } from '@angular/core';
import { CustomerChatBox } from './models/CustomerChatBox';
import { PresenceService } from '../services/presence.service';

@Component({
  selector: 'app-customer-list-by-chat',
  templateUrl: './customer-list-by-chat.component.html',
  styleUrls: ['./customer-list-by-chat.component.scss']
})
export class CustomerListByChatComponent implements OnInit {
  usersOnline: CustomerChatBox[] = [];
  miniUser:CustomerChatBox[] = [];

  dataUser = [
    {userName: 'hoainam10th', displayName:'Nguyen Hoai Nam'},
    {userName: 'ubuntu', displayName:'Tran Hoai Nam'},
    {userName: 'lisa', displayName:'Nguyen Minh Bac'},
    {userName: 'tony', displayName:'Nguyen Hoai Phong'},
    {userName: 'agutech', displayName:'Technology'},
    {userName: 'android', displayName:'Android 11'}
  ]

  constructor(public presence: PresenceService) { }

  ngOnInit(): void {
    const userChatBox: CustomerChatBox[] = JSON.parse(localStorage.getItem('chatboxusers'));
    if (userChatBox) {
      this.usersOnline = userChatBox;
    } else {
      this.usersOnline = [];
    }
  }

  selectUser(user: any) {
    //dcm dubber casi lon;
    switch ((this.usersOnline.length+1) % 2) {

      case 1: {
        var u = this.usersOnline.find(x => x.customer.email === user.email);
        if (u) {
          this.usersOnline = this.usersOnline.filter(x => x.customer.email !== user.email);
          this.usersOnline.push(u);
        } else {
          this.usersOnline.push(new CustomerChatBox(user, 250));
        }
        localStorage.setItem('chatboxusers', JSON.stringify(this.usersOnline));
        break;
      }
      case 0: {
        var u = this.usersOnline.find(x => x.customer.email === user.email);
        if (u) {
          this.usersOnline = this.usersOnline.filter(x => x.customer.email !== user.email);
          this.usersOnline.push(u);
        } else {
          this.usersOnline.push(new CustomerChatBox(user, 250 + 325));
        }
        localStorage.setItem('chatboxusers', JSON.stringify(this.usersOnline));
        break;
      }
      default: {
        console.log("No");
        break;
      }
    }
  }

  removeChatBox(event: string) {
    this.usersOnline = this.usersOnline.filter(x => x.customer.email !== event);
    localStorage.setItem('chatboxusers', JSON.stringify(this.usersOnline));
  }

  miniChatBox(user: any){
    this.miniUser.push(new CustomerChatBox(user, 250));
  }

  restoreUser(user: CustomerChatBox){
    this.miniUser = this.miniUser.filter(x => x.customer.email !== user.customer.email);
    this.selectUser(user.customer);
  }

  removeMiniUser(user: any){
    this.miniUser = this.miniUser.filter(x => x.customer.email !== user.user.email);
  }
}
