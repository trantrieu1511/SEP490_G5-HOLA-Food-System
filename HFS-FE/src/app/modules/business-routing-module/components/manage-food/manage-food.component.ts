import { Component } from '@angular/core';
import { MessageService } from 'primeng/api';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { iComponentBase } from 'src/app/modules/shared-module/shared-module';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-manage-food',
  template: `
      <food-seller *ngIf="authService.getRole() == 'SE'; else foodModerator"></food-seller>
      <ng-template #foodModerator>
        <app-food-management></app-food-management>
      </ng-template> 
    `
})
export class ManageFoodComponent extends iComponentBase{
  constructor(
    public authService: AuthService,
    public breadcrumbService: AppBreadcrumbService,
    public messageService: MessageService,
    )
  {
    super(messageService, breadcrumbService);

    this.breadcrumbService.setItems([
      {label: 'HFSBusiness'},
      {label: 'Food Management', routerLink: ['/HFSBusiness/food-management']}
    ]);
  }
}
