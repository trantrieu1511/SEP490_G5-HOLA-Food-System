import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { AppBreadcrumbService } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.service';
import { iComponentBase } from 'src/app/modules/shared-module/shared-module';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-manage-post',
  template: `
      <post-seller *ngIf="authService.getRole() == 'SE'; else postModerator"></post-seller>
      <ng-template #postModerator>
        <app-display-post></app-display-post>
      </ng-template> 
    `
})
export class ManagePostComponent extends iComponentBase{
  constructor(
    public authService: AuthService,
    public breadcrumbService: AppBreadcrumbService,
    public messageService: MessageService,
  ){
    super(messageService, breadcrumbService);

    this.breadcrumbService.setItems([
      {label: 'HFSBusiness'},
      {label: 'Post Management', routerLink: ['/HFSBusiness/post-management']}
    ]);
  }
/*<ng-template *ngIf="authService.getRole() === 'SE'; else postModerator">
      <app-post-management></app-post-management>
    </ng-template>
    <ng-template #postModerator>
      <app-display-post></app-display-post>
    </ng-template> */
}
