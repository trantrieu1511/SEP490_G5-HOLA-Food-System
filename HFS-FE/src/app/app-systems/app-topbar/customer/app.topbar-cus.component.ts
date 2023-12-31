import {Component, ElementRef, OnInit, ViewChild} from '@angular/core';
import {animate, AnimationEvent, style, transition, trigger} from '@angular/animations';
import {MegaMenuItem, MessageService} from 'primeng/api';
import {Router} from "@angular/router";
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
                style({opacity: 0, transform: 'scaleY(0.8)'}),
                animate('.12s cubic-bezier(0, 0, 0.2, 1)', style({opacity: 1, transform: '*'})),
            ]),
            transition(':leave', [
                animate('.1s linear', style({opacity: 0}))
            ])
        ])
    ],
    styleUrls: ['./app.topbar-cus.component.scss'],
})
export class AppCustomerTopBarComponent extends iComponentBase implements OnInit {  
    
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

    ngOnInit() {

    }

    onCartDetail(){
        this.router.navigate(['/cartdetail']);
    }
    viewProfile(){
        this.router.navigate(['/profile']);
    }
}
