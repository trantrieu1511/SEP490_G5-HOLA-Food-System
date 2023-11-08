import { Component, OnInit } from '@angular/core';
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
export class ManagePostComponent{
  constructor(public authService: AuthService){}
/*<ng-template *ngIf="authService.getRole() === 'SE'; else postModerator">
      <app-post-management></app-post-management>
    </ng-template>
    <ng-template #postModerator>
      <app-display-post></app-display-post>
    </ng-template> */
}
