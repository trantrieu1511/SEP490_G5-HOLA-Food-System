import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ComponentModule} from '../components-module/component.modules';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { StyleClassModule } from 'primeng/styleclass';
import {iComponentBase} from './functions/iComponentBase.component';
import {iServiceBase} from './functions/iServiceBase';
import {LoadingComponent} from './components/loading-component/loading.component';
import {OpenPagePopupComponent} from './components/open-page-popup/open-page-popup.component';
import { iFunction } from './shared-module';
import { DataRealTimeService } from 'src/app/services/SignalR/data-real-time.service';
import { NotificationsComponent } from 'src/app/modules/shared-module/components/notifications/notifications.component';
import { NotificationItemComponent } from './components/notification-item/notification-item.component';
import { SellerListComponent } from 'src/app/seller-list/seller-list.component';
import { ChatboxComponent } from 'src/app/chatbox/chatbox.component';
import { CustomerListByChatComponent } from 'src/app/customer-list-by-chat/customer-list-by-chat.component';
import { TranslateModule } from '@ngx-translate/core';
import { AppBreadcrumbComponent } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.component';
import { AppTopBarComponent } from 'src/app/app-systems/app-topbar/app.topbar.component';
import { AppFooterComponent } from 'src/app/app-systems/app-footer/app.footer.component';
import { AppMenuComponent } from 'src/app/app-systems/app-menu/app-menu.component';
import { AppMenuitemComponent } from 'src/app/app-systems/app-menuitem/app.menuitem.component';
import { CustomerRoutingModule } from '../customer-routing-module/customer-routing.module';


@NgModule({
  imports: [
    ComponentModule,
    FormsModule,
    CommonModule,
    ReactiveFormsModule,
    StyleClassModule,
    TranslateModule,

  ],
  providers: [
      iServiceBase,
      iFunction,
      DataRealTimeService,

  ],
  declarations: [
      iComponentBase,
      LoadingComponent,
      OpenPagePopupComponent,
      NotificationsComponent,
      NotificationItemComponent,
      SellerListComponent,
      ChatboxComponent,
      CustomerListByChatComponent,
      

  ],
  exports: [
      iComponentBase,
      LoadingComponent,
      OpenPagePopupComponent,
      NotificationsComponent,
      SellerListComponent,
      CustomerListByChatComponent,
      TranslateModule,
      // AppBreadcrumbComponent,
      // AppTopBarComponent,
      // AppFooterComponent,
      // AppMenuComponent,
      // AppMenuitemComponent,

  ],
})

export class SharedModule { }
