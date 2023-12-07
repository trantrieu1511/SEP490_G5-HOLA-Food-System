import { AfterViewInit, ChangeDetectionStrategy, Component, EventEmitter, HostListener, Input, OnDestroy, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

import { AuthService, User } from '../services/auth.service';
import { MessageChatService } from '../services/messagechat.service';
import { iFunction } from '../modules/shared-module/shared-module';


@Component({
  changeDetection: ChangeDetectionStrategy.OnPush,
  selector: 'app-chatbox',
  templateUrl: './chatbox.component.html',
  styleUrls: ['./chatbox.component.scss'],
  providers: [MessageChatService]
})
export class ChatboxComponent implements OnInit, AfterViewInit,OnDestroy {

  @Input() user: any;//information of chat box
  messageContent: string;
  @Input() right: number;
  @Output() removeChatBox = new EventEmitter();
  @Output() miniChatBox = new EventEmitter();
  @Output() activedChatBoxEvent = new EventEmitter();
  @ViewChild('messageForm') messageForm: NgForm;
// Inside your component class
isCollapsed = false;
isAnimated = true;
role:string;
  customer: User;
  constructor(
    public messageChatService: MessageChatService, 
    private authService: AuthService,
    private iFunction: iFunction
    ) { }

  ngOnInit(): void {
    //this.role=sessionStorage.getItem('role');
    this.role = this.authService.getUserInfor().role;
    //this.customer=JSON.parse(localStorage.getItem('user'));
    this.customer= this.authService.getUserInfor();
    //const user: User = JSON.parse(localStorage.getItem('user'));
    //const token = sessionStorage.getItem('JWT');
    const token = this.iFunction.getCookie('token');
    this.messageChatService.createHubConnection(token, this.user.email);
  }
  ngOnDestroy(): void {
    this.messageChatService.stopHubConnection();
  }

  ngAfterViewInit() {
    var chatBox = document.getElementById(this.user.email);
    chatBox.style.right = this.right + "px";
  }
  @HostListener("scroll", ["$event"])
  onScroll(event) {
    let pos = event.target.scrollTop + event.target.offsetHeight;
    let max = event.target.scrollHeight;
    //pos/max will give you the distance between scroll bottom and and bottom of screen in percentage.

  }
  sendMessage() {
    this.messageChatService.sendMessage(this.user.email, this.messageContent).then(() => {
      this.messageForm.reset();
    })
  }

  closeBoxChat() {
    this.removeChatBox.emit(this.user.email);
  }

  minimumBoxChat() {
    this.closeBoxChat()
    this.miniChatBox.emit(this.user);
  }

  onFocusEvent(event: any) {
    //console.log(event.target.value);
    this.activedChatBox();
  }

  activedChatBox() {
    this.activedChatBoxEvent.emit(this.user.email)
  }

}
