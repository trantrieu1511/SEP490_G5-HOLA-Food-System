import { Component } from '@angular/core';
interface Message {
  sender: string;
  content: string;
}

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
  users: string[] = ['User 1', 'User 2', 'User 3']; // Danh sách người dùng
  selectedUser: string = ''; // Người dùng được chọn
  messages: Message[] = [
    { sender: 'User', content: 'Hello' },
    { sender: 'Bot', content: 'Hi, how can I help you?' }
  ];
  newMessage: string = '';

  selectUser(user: string): void {
    this.selectedUser = user;
  }

  sendMessage(): void {
    if (this.newMessage.trim() !== '') {
      const message: Message = {
        sender: 'User',
        content: this.newMessage
      };
      this.messages.push(message);
      this.newMessage = '';
    }
  }
}
