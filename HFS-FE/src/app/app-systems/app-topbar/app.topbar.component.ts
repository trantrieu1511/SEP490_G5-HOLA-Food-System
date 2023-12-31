import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
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

@Component({
    selector: 'app-topbar',
    templateUrl: './app.topbar.component.html',
    animations: [
        trigger('topbarActionPanelAnimation', [
            transition(':enter', [
                style({opacity: 0, transform: 'scaleY(0.8)'}),
                animate('.12s cubic-bezier(0, 0, 0.2, 1)', style({opacity: 1, transform: '*'})),
            ]),
            transition(':leave', [
                animate('.1s linear', style({opacity: 0}))
            ])
        ])
    ]
})
export class AppTopBarComponent extends iComponentBase implements OnInit {

    constructor(public layoutService: LayoutService,
                public app: AppComponent,
                private router: Router,
                private iServiceBase: iServiceBase,
                private shareData: ShareData,
                public messageService: MessageService
    ) {
        super(messageService);
    }


    @ViewChild('searchInput') searchInputViewChild!: ElementRef;

    onSearchAnimationEnd(event: AnimationEvent) {
        switch (event.toState) {
            case 'visible':
                this.searchInputViewChild.nativeElement.focus();
                break;
        }
    }

    logOut(event : Event) {
        //Logout thì xóa đi
        sessionStorage.clear();

        //Xóa hết đi các thứ linh tinh chỉ gán lại các thứ cấn thiết trong localstorage
        this.clearLocalStorage();

        event.preventDefault();

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
        if(IP_API_SERVICE && IP_API_GATEWAY && VERSION && PROJECT_NAME){
            localStorage.setItem("APISERVICE", IP_API_SERVICE);
            localStorage.setItem("APIGATEWAY", IP_API_GATEWAY);
            localStorage.setItem("VERSION", VERSION);
            localStorage.setItem("PROJECT_NAME", PROJECT_NAME);
        }
    }



    ngOnInit() {

    }
}
