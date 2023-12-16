import {Component, ElementRef, EventEmitter, OnInit, Output, ViewChild} from '@angular/core';
import {animate, AnimationEvent, style, transition, trigger} from '@angular/animations';
import {MegaMenuItem, MessageService} from 'primeng/api';
import {AppComponent} from '../../app.component';
import {NavigationEnd, Router} from "@angular/router";
import {
    iComponentBase,
    iFunction,
    iServiceBase,
    ShareData
} from 'src/app/modules/shared-module/shared-module';
import * as API from 'src/app/services/apiURL';
import { LayoutService } from 'src/app/layout/service/app.layout.service';
import { PresenceService } from 'src/app/services/presence.service';
import { ProfileImage } from 'src/app/profile/models/profile';
import { AuthService, User } from 'src/app/services/auth.service';
import { RoleNames } from 'src/app/utils/roleName';
import { ManageprofileComponent } from 'src/app/profile/manageprofile.component';
import { TranslateService } from '@ngx-translate/core';

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
    ],
    providers: [ManageprofileComponent]
})
export class AppTopBarComponent extends iComponentBase implements OnInit {

    @Output() toggleListEvent = new EventEmitter<void>();

    topBarProfileImg: ProfileImage = new ProfileImage();
    isLoggedInState = false;
    isCustomer: boolean = false;
    isSeller: boolean = false;
    user: any;

    constructor(public layoutService: LayoutService,
        public app: AppComponent,
        private router: Router,
        private iServiceBase: iServiceBase,
        private shareData: ShareData,
        public messageService: MessageService,
        public presence: PresenceService,
        public profileService: ManageprofileComponent,
        private authService: AuthService,
        private iFunction: iFunction,
        public translate: TranslateService
    ) {
        super(messageService);
        if(this.checkLink()){
            this.isCustomer = true;
            this.isSeller = false;
        }else{
            if(RoleNames[this.authService.getUserInfor().role] == 'Seller'){
                this.isSeller = true;
            }
        }
        this.user = this.authService.getUserInfor();
    }

    checkRoleCus() {
        //
        // console.log(this.authService.getRole());
        if(!this.user)
            return true;
        const role = this.user.role
        return role == null || RoleNames[role] == 'Customer'
    }
    setCurrentUser() {
      const user: User = JSON.parse(localStorage.getItem('user'));
    //  const token = sessionStorage.getItem('JWT');
     //
      //const token = sessionStorage.getItem('JWT');
      const token = this.iFunction.getCookie('token');
     //
      if (user) {

        this.presence.createHubConnection(token);
      }
    }

    @ViewChild('searchInput') searchInputViewChild!: ElementRef;

    toggleCustomerList() {
        this.toggleListEvent.emit();
        localStorage.removeItem('chatboxusers')
    }

    toggleSellerList() {
        this.toggleListEvent.emit();
        localStorage.removeItem('chatboxusers')
    }

    onSearchAnimationEnd(event: AnimationEvent) {
        switch (event.toState) {
            case 'visible':
                this.searchInputViewChild.nativeElement.focus();
                break;
        }
    }

    logOut(event: Event) {
        localStorage.removeItem('user');
        //Xóa hết đi các thứ linh tinh chỉ gán lại các thứ cấn thiết trong localstorage
        this.clearLocalStorage();

        event.preventDefault();
        // ;
        let urlLogin = this.checkRoleCus() ? '/login' : '/login-2';
        //Logout thì xóa đi
        sessionStorage.clear();
        this.iFunction.deleteCookieAllToken();

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
        let urlProfile = this.checkRoleCus() ? 'profile' : 'HFSBusiness/profile';
        this.router.navigateByUrl(urlProfile);
    }

    checkLoggedInState() {
        // if (sessionStorage.getItem("userId") != null) {
        //     this.isLoggedInState = true;
        // }

        const user = this.authService.getUserInfor();
        if(user != null)
            this.isLoggedInState = true;

        // if (this.authService.getUserInfor().userId != null) {
        //     this.isLoggedInState = true;
        // }
    }

    async ngOnInit() {
        // ;
        this.setCurrentUser();
        this.checkLoggedInState();
        await this.profileService.getProfileImage();
        this.topBarProfileImg = this.profileService.profileImage;
        // console.log("Top bar profile img: ");
        // console.log(this.topBarProfileImg);
        const userChatBox: boolean = JSON.parse(localStorage.getItem('chatboxusers'));
        if (userChatBox) {
          this.toggleListEvent.emit();
        } else {

        }
    }

    goToLoginPage() {
        let urlLogin = this.checkRoleCus() ? '/login' : '/login-2';
        this.router.navigateByUrl(urlLogin);
    }

    onCartDetail() {
        this.router.navigate(['/cartdetail']);
    }

    goToNewsFeedPage() {
        this.router.navigateByUrl('/newfeed');
    }

    onClickLogo(event: any){
        event.preventDefault();
        let urlHome = this.checkLink() ? '/homepage' : '/HFSBusiness';
        this.router.navigateByUrl(urlHome);
    }

    checkLink(){
        // ;
        return this.router.url.indexOf("/HFSBusiness") == 0 ? false : true;
    }

    goToCartDetail(){

    }

}
