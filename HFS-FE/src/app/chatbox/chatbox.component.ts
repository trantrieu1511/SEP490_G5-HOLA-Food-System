import { AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-chatbox',
  templateUrl: './chatbox.component.html',
  styleUrls: ['./chatbox.component.scss']
})
export class ChatboxComponent implements OnInit, AfterViewInit {

  @Input() user: any;//information of chat box
  messageContent: string;
  @Input() right: number;
  @Output() removeChatBox = new EventEmitter();
  @Output() miniChatBox = new EventEmitter();
  @ViewChild('messageForm') messageForm: NgForm;

  constructor() { }

  ngOnInit(): void {
  }

  ngAfterViewInit() {
    var chatBox = document.getElementById(this.user.userName);
    chatBox.style.right = this.right + "px";
  }

  sendMessage() {
  }

  closeBoxChat() {
    this.removeChatBox.emit(this.user.userName);
  }

  minimumBoxChat() {
    this.closeBoxChat()
    this.miniChatBox.emit(this.user);
  }

  onFocusEvent(event: any) {
    //console.log(event.target.value);
  }

  activedChatBox() {
    //this.activedChatBoxEvent.emit(this.user.userName)
  }

}
