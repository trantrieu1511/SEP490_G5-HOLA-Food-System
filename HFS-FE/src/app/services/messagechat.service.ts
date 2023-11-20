import { Injectable } from "@angular/core";
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, take } from "rxjs";
import { environment } from "src/app/environments/environment";
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { Message } from "../chatbox/models/Message";

@Injectable({
  providedIn: 'root'
})
export class MessageChatService {

  baseUrl = environment.apiUrl;
  hubUrl = environment.hubUrl;
  private hubConnection: HubConnection;
    private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();
  constructor(private http: HttpClient) { }
 createHubConnection(token:string, otherUsername: string){
    //dcm dubber casi lon;
    this.hubConnection = new HubConnectionBuilder()
    .withUrl(this.hubUrl+ 'message?user=' + otherUsername, {
      accessTokenFactory: ()=> token
    }).withAutomaticReconnect().build()

    this.hubConnection.start().catch(err => console.log(err));
      this.hubConnection.on('ReceiveMessageThread', messages => {
    this.messageThreadSource.next(messages);
    console.log(messages);
    })
    this.hubConnection.on('NewMessage', message => {
      this.messageThread$.pipe(take(1)).subscribe(messages => {
        this.messageThreadSource.next([...messages, message])
        console.log(message)
      })
    })
  }


  stopHubConnection(){
    if(this.hubConnection){
      this.hubConnection.stop().catch(error => console.log(error));
    }
  }
  async sendMessage(useremail: string, content: string){
    return this.hubConnection.invoke('SendMessage', {RecipientEmail: useremail, content})
      .catch(error => console.log(error));
  }
}
