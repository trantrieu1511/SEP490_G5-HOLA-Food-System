import { Component, OnDestroy } from '@angular/core';
import { AppBreadcrumbService } from './app.breadcrumb.service';
import { Subscription } from 'rxjs';
import { MenuItem } from 'primeng/api';
import { AuthService } from 'src/app/services/auth.service';

@Component({
    selector: 'app-breadcrumb',
    templateUrl: './app.breadcrumb.component.html'
})
export class AppBreadcrumbComponent implements OnDestroy {

    subscription: Subscription;

    items: MenuItem[] | undefined;

    home: MenuItem;

    constructor(public breadcrumbService: AppBreadcrumbService, private authService: AuthService) {
        this.subscription = breadcrumbService.itemsHandler.subscribe(response => {
            this.items = response;
        });
        const user = authService.getUserInfor()

        if(user == null || user.role == 'CU'){
            this.home = { icon: 'pi pi-home', routerLink: '/' };
        }else{
            this.home = { icon: 'pi pi-home', routerLink: '/HFSBusiness/' };
        }
            
    }

    ngOnDestroy() {
        if (this.subscription) {
            this.subscription.unsubscribe();
        }
    }
}
