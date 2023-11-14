import { Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { animate, AnimationEvent, style, transition, trigger } from '@angular/animations';
import { MegaMenuItem, MessageService } from 'primeng/api';
import { Router } from "@angular/router";
import {
    iComponentBase,
    iServiceBase,
    ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from 'src/app/services/apiURL';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { CustomerLayoutService } from 'src/app/layout/service/app.layout-cus.service';
import { AppComponent } from 'src/app/app.component';


@Component({
    selector: 'app-customer-topbar',
    templateUrl: './app.topbar-cus.component.html',
    animations: [
        trigger('topbarActionPanelAnimation', [
            transition(':enter', [
                style({ opacity: 0, transform: 'scaleY(0.8)' }),
                animate('.12s cubic-bezier(0, 0, 0.2, 1)', style({ opacity: 1, transform: '*' })),
            ]),
            transition(':leave', [
                animate('.1s linear', style({ opacity: 0 }))
            ])
        ])
    ],
    styleUrls: ['./app.topbar-cus.component.scss'],
})
export class AppCustomerTopBarComponent extends iComponentBase implements OnInit {
  @Output() toggleSellerListEvent = new EventEmitter<void>();
    isLoggedIn: boolean = false;

    constructor(public layoutService: CustomerLayoutService,
        public app: AppComponent,
        private router: Router,
        private iServiceBase: iServiceBase,
        private shareData: ShareData,
        public messageService: MessageService
    ) {
        super(messageService);
    }

    @ViewChild('topbarmenubutton') topbarMenuButton!: ElementRef;

    @ViewChild('topbarmenu') menu!: ElementRef;
    toggleSellerList() {
      this.toggleSellerListEvent.emit();
    }
    goToNewsFeedPage() {
        this.router.navigateByUrl('/newsfeed');
    }

    goToLoginPage() {
        this.router.navigateByUrl('/login');
    }

    //check whether the user has logged in or not to display button login for them to login
    checkUserLoggedInState() {
        if (sessionStorage.getItem('userId') != null) {
            this.isLoggedIn = true;
        }
    }

    ngOnInit() {
        this.checkUserLoggedInState();
    }

    logOut(event: Event) {
        //Logout thì xóa đi
        sessionStorage.clear();
        localStorage.removeItem('user');
        //Xóa hết đi các thứ linh tinh chỉ gán lại các thứ cấn thiết trong localstorage
        this.clearLocalStorage();

        event.preventDefault();

        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigate(['/login']);
            window.location.reload();
        });
            this.router.navigate(['/login']);
    }

    clearLocalStorage() {
        //get ra các biến không cần xóa
        let IP_API_SERVICE = localStorage.getItem('APISERVICE');
        let IP_API_GATEWAY = localStorage.getItem('APIGATEWAY');
        let VERSION = localStorage.getItem('VERSION');
        let PROJECT_NAME = localStorage.getItem('PROJECT_NAME');

        //clear
        localStorage.clear();

        //Set lại các thứ cần thiết
        if (IP_API_SERVICE && IP_API_GATEWAY && VERSION && PROJECT_NAME) {
            localStorage.setItem("APISERVICE", IP_API_SERVICE);
            localStorage.setItem("APIGATEWAY", IP_API_GATEWAY);
            localStorage.setItem("VERSION", VERSION);
            localStorage.setItem("PROJECT_NAME", PROJECT_NAME);
        }
    }

    onCartDetail() {
        this.router.navigate(['/cartdetail']);
    }
    viewProfile() {
        this.router.navigate(['/profile']);
    }
}
