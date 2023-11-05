import { Component, Input, OnInit, HostListener} from '@angular/core';
import {
  iServiceBase
} from 'src/app/modules/shared-module/shared-module';
import { NotificationService } from 'src/app/services/SignalR/notification.service';
import * as API from "../../../../services/apiURL";
import { Notification } from '../../models/notification.model';
import {animate, AnimationEvent, style, transition, trigger} from '@angular/animations';
import { BehaviorSubject } from 'rxjs';
import { distinctUntilChanged } from 'rxjs/operators';


@Component({
  selector: 'app-notifications',
  templateUrl: './notifications.component.html',
  styleUrls: ['./notifications.component.scss'],
  animations: [
    trigger('topbarActionPanelAnimation', [
        transition(':enter', [
            style({opacity: 0, transform: 'scaleY(0.8)'}),
            animate('.12s cubic-bezier(0, 0, 0.2, 1)', style({opacity: 1, transform: '*'})),
        ]),
        transition(':leave', [
            animate('.1s linear', style({opacity: 0}))
        ])
    ])
]
})
export class NotificationsComponent implements OnInit {
  @Input() layoutService : any;

  lstNotification: Notification[] = [];

  isNewNotify: boolean = false;

  constructor(
    private iServiceBase: iServiceBase,
    private signalRService: NotificationService
  ){
    this.connectSignalR();
    this.getAllNotification();

    this.lstNotification = [
      {
        id: 1,
        sendBy: "a9whjufe",
        receiver: "efasdf",
        typeId: 1,
        typeName: "fasddf",
        title: "have new order",
        content: "order 11029 request",
        createDate: "11/05/2023",
        isRead: false
      },
      {
        id: 2,
        sendBy: "a9whjufe",
        receiver: "efasdf",
        typeId: 1,
        typeName: "fasddf",
        title: "have new order",
        content: "order 453242329 request",
        createDate: "11/05/2023",
        isRead: false
      },
      {
        id: 3,
        sendBy: "a9whjufe",
        receiver: "efasdf",
        typeId: 1,
        typeName: "fasddf",
        title: "have new order",
        content: "order 11021229 request",
        createDate: "11/05/2023",
        isRead: true
      }
    ]
    // check có tin ch đọc hay ko
    const hasUnreadNotification = this.lstNotification.some(notification => notification.isRead === true);

    // Đặt isNewNotify thành true if has isRead = true
    this.isNewNotify = hasUnreadNotification;

    console.log(this.lstNotification);
  }

  async ngOnInit() {
    
  }

  async connectSignalR() {
    this.lstNotification = [];
    this.signalRService.startConnection();
    const res = await this.signalRService.addTransferDataListener(
      API.PHAN_HE.NOTIFY,
      API.API_NOTIFY.GET_ALL_NOTIFIES
    );
    if (res && res.message === 'Success') {
      this.lstNotification = res.notifies;
    }
  }

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any): void {
    this.signalRService.stopConnection();
  }

  async getAllNotification(){
    this.lstNotification = [];
   
    let response = await this.iServiceBase.getDataAsync(
      API.PHAN_HE.NOTIFY,
      API.API_NOTIFY.GET_ALL_NOTIFIES
    );
    if (response && response.message === 'Success') {
      this.lstNotification = response.notifies;
    }
  }

  notifyChange(notifyId: number){
    console.log("clicked", notifyId);
    //this.updateNotification(notify);

    // move to page read
  }

  async updateNotification(notifyId: number){
    const param = {
      notifyId : notifyId
    }
    // Gọi API để cập nhật thông báo
    const response = await this.iServiceBase.postDataAsync(
      API.PHAN_HE.NOTIFY,
      API.API_NOTIFY.UPDATE_NOTIFY,
      param
    );

    if (response && response.message === 'Success') {
      // Cập nhật element tuong ung trong mảng lstNotification
      const index = this.lstNotification.findIndex(notification => notification.id === notifyId);
      if (index !== -1) {
        this.lstNotification[index].isRead = true;
      }
    }
  }

}
