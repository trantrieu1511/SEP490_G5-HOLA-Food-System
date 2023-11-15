import {Component, ElementRef, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {animate, AnimationEvent, style, transition, trigger} from '@angular/animations';
import {MegaMenuItem, MessageService} from 'primeng/api';
import {AppComponent} from '../../app.component';
import {Router} from "@angular/router";
import {
    iComponentBase,
    iServiceBase,
    ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from 'src/app/services/apiURL';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { PresenceService } from 'src/app/services/presence.service';
import { AuthService } from 'src/app/services/auth.service';
import { RoleNames } from 'src/app/utils/roleName';

@Component({
    selector: 'app-topbar',
    templateUrl: './app.topbar.component.html',
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
    ]
})
export class AppTopBarComponent extends iComponentBase implements OnInit {

  @Output() toggleListEvent = new EventEmitter<void>();

    isLoggedInState = false;
    isCustomer: boolean = false;

    constructor(public layoutService: LayoutService,
        public app: AppComponent,
        private router: Router,
        private iServiceBase: iServiceBase,
        private shareData: ShareData,
        public messageService: MessageService,
        public presence: PresenceService,
        private authService: AuthService
    ) {
        super(messageService);
        if(this.checkRoleCus){
            this.isCustomer = true;
        }
    }

    checkRoleCus(){
        return this.authService.getRole() == null || RoleNames[this.authService.getRole()] == 'Customer'
    }


    @ViewChild('searchInput') searchInputViewChild!: ElementRef;

    toggleCustomerList() {
      this.toggleListEvent.emit();
    }

    toggleSellerList() {
        this.toggleListEvent.emit();
    }

    onSearchAnimationEnd(event: AnimationEvent) {
        switch (event.toState) {
            case 'visible':
                this.searchInputViewChild.nativeElement.focus();
                break;
        }
    }

    logOut(event: Event) {
        //Logout thì xóa đi
        sessionStorage.clear();
        localStorage.removeItem('user');
        //Xóa hết đi các thứ linh tinh chỉ gán lại các thứ cấn thiết trong localstorage
        this.clearLocalStorage();

        event.preventDefault();
        let urlLogin = this.checkRoleCus ? '/login' : '/login-2';

        //this.router.navigate(['/login']);
        this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
            this.router.navigate([urlLogin]);
            window.location.reload();
        });
           this.router.navigate([urlLogin]);
    }

    clearLocalStorage() {
        //get ra các biến không cần xóa
        let IP_API_SERVICE = localStorage.getItem('APISERVICE');
        let IP_API_GATEWAY = localStorage.getItem('APIGATEWAY');
        let VERSION = localStorage.getItem('VERSION');
        let PROJECT_NAME = localStorage.getItem('PROJECT_NAME');
        let LANG = localStorage.getItem("LANG");

        //clear
        localStorage.clear();

        //Set lại các thứ cần thiết
        if (IP_API_SERVICE && IP_API_GATEWAY && VERSION && PROJECT_NAME) {
            localStorage.setItem("APISERVICE", IP_API_SERVICE);
            localStorage.setItem("APIGATEWAY", IP_API_GATEWAY);
            localStorage.setItem("VERSION", VERSION);
            localStorage.setItem("PROJECT_NAME", PROJECT_NAME);
            localStorage.setItem("LANG", LANG);
        }
    }

    viewProfile() {
        let urlProfile = this.checkRoleCus ? 'profile' : 'HFSBusiness';
        this.router.navigateByUrl(urlProfile);
    }

    ngOnInit() {
        this.checkLoggedInState();

    }

    checkLoggedInState() {
        if (sessionStorage.getItem("userId") != null) {
            this.isLoggedInState = true;
        }
    }

    goToLoginPage() {
        let urlLogin = this.checkRoleCus ? '/login' : '/login-2';
        this.router.navigateByUrl(urlLogin);
    }

    onCartDetail() {
        this.router.navigate(['/cartdetail']);
    }

    goToNewsFeedPage() {
        this.router.navigateByUrl('/newsfeed');
    }

}
