import { Component, Input, OnInit, HostListener, OnDestroy} from '@angular/core';
import {
  iComponentBase,
  iFunction,
  iServiceBase,
  mType
} from 'src/app/modules/shared-module/shared-module';
import { NotificationService } from 'src/app/services/SignalR/notification.service';
import * as API from "../../../../services/apiURL";
import { Notification } from '../../models/notification.model';
import {animate, AnimationEvent, style, transition, trigger} from '@angular/animations';
import { BehaviorSubject, Subscription } from 'rxjs';
import { distinctUntilChanged } from 'rxjs/operators';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { RoleNames } from '../../../../utils/roleName';
import { TranslateService } from '@ngx-translate/core';
import { NotificationBellService } from 'src/app/services/notification-bell.service';


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
export class NotificationsComponent extends iComponentBase implements OnInit, OnDestroy {
  @Input() layoutService : any;

  lstNotification: Notification[] | undefined;

  isNewNotify: boolean = false;

  lstNotificationSubscription: Subscription;

  isNewNotifySubscription: Subscription;

  readNotifySubscription: Subscription;

  markNotifySubscription: Subscription;

  role: string;

  constructor(
    private iServiceBase: iServiceBase,
    private signalRService: NotificationService,
    public authService: AuthService,
    private _route: Router,
    public messageService: MessageService,
    public translate: TranslateService,
    public notifyService: NotificationBellService,
    private iFunction: iFunction
    
  ){
    super(messageService);
    if(this.iFunction.getCookie("token")){
      this.connectSignalR();
      this.getAllNotification();

      this.isNewNotifySubscription = notifyService.isNewNotifyHandle.subscribe(res => {
        this.isNewNotify = res;
      })

      this.readNotifySubscription = notifyService.readNotifyHandle.subscribe(res => {
        // Cập nhật element tuong ung trong mảng lstNotification
        const index = this.lstNotification.findIndex(notification => notification.id === res.id);
        if (index !== -1) {
          this.lstNotification[index].isRead = true;
        }
      })

      this.markNotifySubscription = notifyService.markAllNotifyHandle.subscribe(res => {
        this.lstNotification.forEach(notification => {
          notification.isRead = true;
        });
        this.isNewNotify = false;
      })
    }
  }

  async ngOnInit() {
    // console.log("lang")
    // console.log(this.translate)
    const user = this.authService.getUserInfor();
    if(user != null)
      this.role = user.role;
  }

  ngOnDestroy() {
    if(this.lstNotificationSubscription){
      this.lstNotificationSubscription.unsubscribe();
    }

    if(this.isNewNotifySubscription){
      this.isNewNotifySubscription.unsubscribe();
    }
  }

  async connectSignalR() {
    
    const param = {
      takeNum: 5,
      lang: this.translate.currentLang
    };
    this.signalRService.startConnection();
    this.signalRService.addTransferDataListener(
      API.PHAN_HE.NOTIFY,
      API.API_NOTIFY.GET_ALL_NOTIFIES,
      param
    );

    this.lstNotificationSubscription = this.signalRService.notifiesHandler.subscribe(res => {
      this.lstNotification = res;

      this.checkNewNotify();
    });
  }

  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any): void {
    this.signalRService.stopConnection();
  }


  async getAllNotification(){
    this.lstNotification = [];
    const param = {
      takeNum: 5,
      lang: this.translate.currentLang
    };
    let response = await this.iServiceBase.getDataWithParamsAsync(
      API.PHAN_HE.NOTIFY,
      API.API_NOTIFY.GET_ALL_NOTIFIES,
      param
    );
    if (response && response.message === 'Success') {
      this.lstNotification = response.notifies;
      this.checkNewNotify();
    }
  }

  notifyChange(notifyId: number){
    console.log("clicked", notifyId);
    //this.updateNotification(notify);
  
    var url = `/notify-management/detail/${notifyId}`;
    if(RoleNames[this.role] != 'Customer'){
      url = 'HFSBusiness' + url;
    }

    // const index = this.lstNotification.findIndex(notification => notification.id === notifyId);
    //   if (index !== -1) {
    //     this.lstNotification[index].isRead = true;
    //   }

    // move to page read
    this._route.navigate([url]);
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
    var url = `/notify-management`;
    if(RoleNames[this.role] != 'Customer'){
      url = 'HFSBusiness' + url;
    }

    this._route.navigate([url]);
  }

  checkNewNotify(){
    // check có tin ch đọc hay ko
    const hasUnreadNotification = this.lstNotification.some(notification => notification.isRead === false);
    // Đặt isNewNotify thành true if has isRead = true
    this.isNewNotify = hasUnreadNotification;
  }

}
