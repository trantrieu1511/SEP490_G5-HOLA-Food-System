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



@Component({
  selector: 'app-manage-notification',
  templateUrl: './manage-notification.component.html',
  styleUrls: ['./manage-notification.component.scss']
})
export class ManageNotificationComponent extends iComponentBase implements OnInit{
  lstNotification: Notification[] = [];

  constructor(
    public breadcrumbService: AppBreadcrumbService,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private signalRService: DataRealTimeService
  ){
    super(messageService, breadcrumbService);
  }

  ngOnInit(){
    this.getAllNotification();

    this.lstNotification = [
      {
        id: 1,
        sendBy: "2",
        receiver: "2",
        typeId: 1,
        typeName: "string",
        title: "string",
        content: "string1",
        createDate: "string",
        isRead: true,
      },
      {
        id: 2,
        sendBy: "2212",
        receiver: "2wqw",
        typeId: 1,
        typeName: "string",
        title: "string",
        content: "string2",
        createDate: "string",
        isRead: true,
      },
      {
        id: 3,
        sendBy: "2212",
        receiver: "2wqw",
        typeId: 1,
        typeName: "string",
        title: "string",
        content: "string3",
        createDate: "string",
        isRead: true,
      },
      {
        id: 4,
        sendBy: "2212",
        receiver: "2wqw",
        typeId: 1,
        typeName: "string",
        title: "string",
        content: "string4",
        createDate: "string",
        isRead: true,
      },
      {
        id: 5,
        sendBy: "2212",
        receiver: "2wqw",
        typeId: 1,
        typeName: "string",
        title: "string",
        content: "string5",
        createDate: "string",
        isRead: true,
      },
      {
        id: 6,
        sendBy: "2212",
        receiver: "2wqw",
        typeId: 1,
        typeName: "string",
        title: "string",
        content: "string6",
        createDate: "string",
        isRead: true,
      },
      {
        id: 7,
        sendBy: "2212",
        receiver: "2wqw",
        typeId: 1,
        typeName: "string",
        title: "string",
        content: "string7",
        createDate: "string",
        isRead: true,
      },
      {
        id: 8,
        sendBy: "2212",
        receiver: "2wqw",
        typeId: 1,
        typeName: "string",
        title: "string",
        content: "string8",
        createDate: "string",
        isRead: true,
      },
      {
        id: 9,
        sendBy: "2212",
        receiver: "2wqw",
        typeId: 1,
        typeName: "string",
        title: "string",
        content: "string9",
        createDate: "string",
        isRead: true,
      },
      {
        id: 10,
        sendBy: "2212",
        receiver: "2wqw",
        typeId: 1,
        typeName: "string",
        title: "string",
        content: "string10",
        createDate: "string",
        isRead: true,
      },
      {
        id: 11,
        sendBy: "2212",
        receiver: "2wqw",
        typeId: 1,
        typeName: "string",
        title: "string",
        content: "string11",
        createDate: "string",
        isRead: true,
      },

    ];

  }
  
  async getAllNotification(){
    const param = {
      skipNum: 0
    };
    let response = await this.iServiceBase.getDataWithParamsAsync(
      API.PHAN_HE.NOTIFY,
      API.API_NOTIFY.GET_ALL_NOTIFIES,
      param
    );
    if (response && response.message === 'Success') {
      this.lstNotification = response.notifies;
    }
  }

  onMarkAllRead(){
    this.markAllNotificationRead();
  }

  async markAllNotificationRead(){
    let response = await this.iServiceBase.postDataAsync(
      API.PHAN_HE.NOTIFY,
      API.API_NOTIFY.MARK_ALL_NOTIFY_READ,
      null
    );

    if (response && response.message === 'Success') {
      await this.getAllNotification();
    }
  }
}
