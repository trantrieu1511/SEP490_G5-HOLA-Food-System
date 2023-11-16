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
  MessageService,
} from 'primeng/api';
import { AppBreadcrumbService } from '../../../../app-systems/app-breadcrumb/app.breadcrumb.service';
import { ActivatedRoute } from '@angular/router';
import { Notification } from 'src/app/modules/shared-module/models/notification.model';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-detail-notification',
  templateUrl: './detail-notification.component.html',
  styleUrls: ['./detail-notification.component.scss']
})
export class DetailNotificationComponent extends iComponentBase implements OnInit{
  notifyId: number;
  notification: Notification;

  constructor(
    public breadcrumbService: AppBreadcrumbService,
    public messageService: MessageService,
    private confirmationService: ConfirmationService,
    private iServiceBase: iServiceBase,
    private iFunction: iFunction,
    private activatedRoute: ActivatedRoute,
    public translate: TranslateService
  ){
    super(messageService, breadcrumbService);

    this.activatedRoute.params.subscribe(params => {
      this.notifyId = params['id']
    });  

    this.breadcrumbService.setItems([
      {label: 'HFSBusiness'},
      {label: 'Notification Management', routerLink: ['/HFSBusiness/notify-management']},
      {label: 'Detail', routerLink: [`/HFSBusiness/notify-management/detail/${this.notifyId}`]}
    ]);
  }

  ngOnInit(): void {
    

    this.getNofication();
  }

  async getNofication(){
    const param = {
      notifyId : this.notifyId,
      lang: this.translate.currentLang
    }
    // Gọi API để cập nhật thông báo
    const response = await this.iServiceBase.getDataWithParamsAsync(
      API.PHAN_HE.NOTIFY,
      API.API_NOTIFY.GET_DETAIL_NOTIFY,
      param
    );
    if (response && response.message === 'Success') {
      this.notification = response.notify;
    }
    else{
      var messageError = this.iServiceBase.formatMessageError(response);
      this.showMessage(mType.error, response.message, messageError, 'notify');
    }
  }
}
