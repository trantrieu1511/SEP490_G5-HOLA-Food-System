import { Component, Input, OnInit, HostListener} from '@angular/core';
import {
  iComponentBase,
  iServiceBase,
  mType
} from 'src/app/modules/shared-module/shared-module';
import { NotificationService } from 'src/app/services/SignalR/notification.service';
import * as API from "../../../../services/apiURL";
import { Notification } from '../../models/notification.model';
import {animate, AnimationEvent, style, transition, trigger} from '@angular/animations';
import { BehaviorSubject } from 'rxjs';
import { distinctUntilChanged } from 'rxjs/operators';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { RoleNames } from '../../../../utils/roleName';


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
export class NotificationsComponent extends iComponentBase implements OnInit {
  @Input() layoutService : any;

  lstNotification: Notification[] = [];

  isNewNotify: boolean = false;

  constructor(
    private iServiceBase: iServiceBase,
    private signalRService: NotificationService,
    public authService: AuthService,
    private _route: Router,
    public messageService: MessageService,
    
  ){
    super(messageService);
  }

  async ngOnInit() {
    if(sessionStorage.getItem('JWT')){
      this.connectSignalR();
      this.getAllNotification();
      // check có tin ch đọc hay ko
      const hasUnreadNotification = this.lstNotification.some(notification => notification.isRead === false);

      // Đặt isNewNotify thành true if has isRead = true
      this.isNewNotify = hasUnreadNotification;
    }
  }

  async connectSignalR() {
    
    const param = {
      takeNum: 5
    };
    this.signalRService.startConnection();
    const res = await this.signalRService.addTransferDataListener(
      API.PHAN_HE.NOTIFY,
      API.API_NOTIFY.GET_ALL_NOTIFIES,
      param
    );
    if (res && res.message === 'Success') {
      this.lstNotification = res.notifies;

      // check có tin ch đọc hay ko
      const hasUnreadNotification = this.lstNotification.some(notification => notification.isRead === false);
      // Đặt isNewNotify thành true if has isRead = true
      this.isNewNotify = hasUnreadNotification;
    }
  }

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any): void {
    this.signalRService.stopConnection();
  }


  async getAllNotification(){
    this.lstNotification = [];
    const param = {
      takeNum: 5
    };
    let response = await this.iServiceBase.getDataWithParamsAsync(
      API.PHAN_HE.NOTIFY,
      API.API_NOTIFY.GET_ALL_NOTIFIES,
      param
    );
    if (response && response.message === 'Success') {
      this.lstNotification = response.notifies;
      // check có tin ch đọc hay ko
      const hasUnreadNotification = this.lstNotification.some(notification => notification.isRead === false);
      // Đặt isNewNotify thành true if has isRead = true
      this.isNewNotify = hasUnreadNotification;
    }
  }

  notifyChange(notifyId: number){
    console.log("clicked", notifyId);
    //this.updateNotification(notify);

    // move to page read
    this._route.navigate([`HFSBusiness/notify-management/detail/${notifyId}`]);
  }

  async updateNotificationRead(notifyId: number){
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

  onMarkAllRead(){
    this.markAllNotificationRead();
  }

  async markAllNotificationRead(){
    // check have new notify -> mark all read
    if(this.isNewNotify){
      let response = await this.iServiceBase.postDataAsync(
        API.PHAN_HE.NOTIFY,
        API.API_NOTIFY.MARK_ALL_NOTIFY_READ,
        null
      );
  
      if (response && response.message === 'Success') {
        await this.getAllNotification();
      }else{
        var messageError = this.iServiceBase.formatMessageError(response);
        this.showMessage(mType.error, response.message, messageError, 'notify');
      }
    }
  }

  onViewAll(event: any){
    event.preventDefault();
    this._route.navigate([`HFSBusiness/notify-management`]);
  }

}
