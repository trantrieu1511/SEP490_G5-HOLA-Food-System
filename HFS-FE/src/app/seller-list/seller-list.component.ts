import { Component, EventEmitter, Output } from '@angular/core';
import { SellerChatBox } from './models/seller-chatbox';
import { PresenceService } from '../services/presence.service';
import { MessageChatService } from '../services/messagechat.service';
import { SoundService } from '../services/sound.service';

@Component({
  selector: 'app-seller-list',
  templateUrl: './seller-list.component.html',
  styleUrls: ['./seller-list.component.scss'],

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
  @Output() closeSellerList = new EventEmitter<void>();

  // Phương thức để gửi sự kiện khi nút tắt được nhấn
  onCloseSellerList() {
    this.closeSellerList.emit();
    localStorage.removeItem('chatboxusers');
  }
  toggleSellerList() {
    this.isSellerListVisible = !this.isSellerListVisible;

    console.log(this.isSellerListVisible);

  }
  constructor(public presence: PresenceService,
    public messageChatService: MessageChatService,
    public soundService:SoundService) { }

  ngOnInit(): void {
    const userChatBox: SellerChatBox[] = JSON.parse(localStorage.getItem('chatboxusers'));
    if (userChatBox) {
      this.usersOnline = userChatBox;
    } else {
      this.usersOnline = [];
    }
  }

  selectUser(user: any) {
    debugger
    switch ((this.usersOnline.length+1) % 2) {
      case 1: {
        debugger;
        var u = this.usersOnline.find(x => x.seller.email === user.email);
        if (u) {
          this.usersOnline = this.usersOnline.filter(x => x.seller.email !== user.email);
          this.usersOnline.push(u);

        } else {
          this.usersOnline.push(new SellerChatBox(user, 350));
        }
        localStorage.setItem('chatboxusers', JSON.stringify(this.usersOnline));
//this.soundService.playAudioMessage();
        break;
      }
      case 0: {
        var u = this.usersOnline.find(x => x.seller.email === user.email);
        if (u) {
          this.usersOnline = this.usersOnline.filter(x => x.seller.email !== user.email);
          this.usersOnline.push(u);
        } else {
          this.usersOnline.push(new SellerChatBox(user, 350 + 325));
        }
        localStorage.setItem('chatboxusers', JSON.stringify(this.usersOnline));
      //  this.soundService.playAudioMessage();
        break;
      }
      default: {
        console.log("No");
        break;
      }
    }
  }

  removeChatBox(event: string) {
    this.usersOnline = this.usersOnline.filter(x => x.seller.email !== event);
    localStorage.setItem('chatboxusers', JSON.stringify(this.usersOnline));
  }

  miniChatBox(user: any){
    this.miniUser.push(new SellerChatBox(user, 350));
  }

  restoreUser(user: SellerChatBox){
    this.miniUser = this.miniUser.filter(x => x.seller.userName !== user.seller.userName);
    this.selectUser(user.seller);
  }

  removeMiniUser(user: any){
    this.miniUser = this.miniUser.filter(x => x.seller.userName !== user.user.userName);
  }
}
