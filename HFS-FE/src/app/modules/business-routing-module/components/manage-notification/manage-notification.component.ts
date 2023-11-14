import { Component, OnInit} from '@angular/core';
import {
  iComponentBase,
  iServiceBase,
  mType,
  iFunction,
} from 'src/app/modules/shared-module/shared-module';
import * as API from '../../../../services/apiURL';
import {
  ConfirmationService,
  LazyLoadEvent,
  MenuItem,
  MessageService,
  SelectItem,
  TreeNode,
} from 'primeng/api';
import { AppBreadcrumbService } from '../../../../app-systems/app-breadcrumb/app.breadcrumb.service';
import { DataRealTimeService } from 'src/app/services/SignalR/data-real-time.service';
import { Notification } from 'src/app/modules/shared-module/models/notification.model';
import { ScrollerLazyLoadEvent } from 'primeng/scroller';
import { Router } from '@angular/router';



@Component({
  selector: 'app-manage-notification',
  templateUrl: './manage-notification.component.html',
  styleUrls: ['./manage-notification.component.scss']
})
export class ManageNotificationComponent extends iComponentBase implements OnInit{
  lstNotification: Notification[] = [];
  isNewNotify: boolean = false;

  constructor(
    public breadcrumbService: AppBreadcrumbService,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private signalRService: DataRealTimeService,
    private _route: Router
  ){
    super(messageService, breadcrumbService);

    this.breadcrumbService.setItems([
      {label: 'HFSBusiness'},
      {label: 'Notification Management', routerLink: ['/HFSBusiness/notify-management']}
    ]);
  }

  ngOnInit(){
    this.getAllNotification();
  }
  
  async getAllNotification(){
    const param = {
      takeNum: 0
    };
    let response = await this.iServiceBase.getDataWithParamsAsync(
      API.PHAN_HE.NOTIFY,
      API.API_NOTIFY.GET_ALL_NOTIFIES,
      param
    );
    if (response && response.message === 'Success') {
      this.lstNotification = response.notifies;

      const hasUnreadNotification = this.lstNotification.some(notification => notification.isRead === false);
      // Đặt isNewNotify thành true if has isRead = true
      this.isNewNotify = hasUnreadNotification;
    }else{
      var messageError = this.iServiceBase.formatMessageError(response);
      this.showMessage(mType.error, response.message, messageError, 'notify');
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

  onViewDetaialNotify(notifyId: number){

    // move to page read
    this._route.navigate([`HFSBusiness/notify-management/detail/${notifyId}`]);
  }
}
