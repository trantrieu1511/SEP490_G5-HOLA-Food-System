import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { iComponentBase } from 'src/app/modules/shared-module/shared-module';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-manage-order',
  template: `
      <order-seller *ngIf="authService.getRole() == 'SE'; else orderShipper"></order-seller>
      <ng-template #orderShipper>
        <app-shipper></app-shipper>
      </ng-template> 
    `
})
export class ManageOrderComponent extends iComponentBase{
  constructor(
    public authService: AuthService,
    public breadcrumbService: AppBreadcrumbService,
    public messageService: MessageService,
    )
  {
    super(messageService, breadcrumbService);

    this.breadcrumbService.setItems([
      {label: 'HFSBusiness'},
      {label: 'Order Management', routerLink: ['/HFSBusiness/order-management']}
    ]);

  }

}
