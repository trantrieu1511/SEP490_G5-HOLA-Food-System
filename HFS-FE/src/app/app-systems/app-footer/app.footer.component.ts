import {Component} from '@angular/core';
import {AppComponent} from '../../app.component';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

@Component({
    selector: 'app-footer',
    template: `
        <div class="layout-footer flex align-items-center p-4 shadow-2">
            <button pButton pRipple type="button" icon="pi pi-github fs-large" class="p-button-rounded p-button-text p-button-plain" [ngClass]="{'ml-auto mr-2': !layoutService.app.isRTL, 'ml-2 mr-auto': layoutService.app.isRTL}"></button>
            <button pButton pRipple type="button" icon="pi pi-facebook fs-large" class="p-button-rounded p-button-text p-button-plain" [ngClass]="{'mr-2': !layoutService.app.isRTL, 'ml-2': layoutService.app.isRTL}"></button>
            <button pButton pRipple type="button" icon="pi pi-twitter fs-large" class="p-button-rounded p-button-text p-button-plain" [ngClass]="{'mr-2': !layoutService.app.isRTL, 'ml-2': layoutService.app.isRTL}"></button>
        </div>
    `
})
export class AppFooterComponent {
    constructor(public layoutService: LayoutService) {}
}
