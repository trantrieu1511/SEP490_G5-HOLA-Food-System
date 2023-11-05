import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Notification } from '../../models/notification.model';

@Component({
  selector: 'notification-item',
  templateUrl: './notification-item.component.html',
  styleUrls: ['./notification-item.component.scss']
})
export class NotificationItemComponent implements OnInit {
  @Input() notify: Notification = new Notification();
  @Input() layoutService : any;
  @Output() notifyChange = new EventEmitter<number>();

  constructor(){

  }

  ngOnInit(){
    //console.log(this.notify);
  }

  readNotify(){
    this.notifyChange.emit(this.notify.id);
  }
}
