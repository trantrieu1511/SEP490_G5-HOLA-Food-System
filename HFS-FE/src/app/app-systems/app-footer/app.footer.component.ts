import {Component} from '@angular/core';
import {AppComponent} from '../../app.component';
import { LayoutService } from 'src/app/layout/service/app.layout.service';

@Component({
    selector: 'app-footer',
    templateUrl: './app-footer.component.html',
    styleUrls: ['./app-footer.component.scss']
})
export class AppFooterComponent {
    phone: string = '0912345678'
    mail: string = 'holafoodfpt@gmail.com'
    address: string = 'Tầng 72, Toà Alpha, FPT University Hà Nội'
    currentYear: number = new Date().getFullYear();
    noCopyright: string;

    constructor(public layoutService: LayoutService) {
        this.noCopyright = '© ' + this.currentYear + ' - ' + 'Bản quyền thuộc về anh em Nhóm 5-490 Đồ án Kỹ thuật phần mềm'
    }
}

/*
<div class="layout-footer flex flex-column align-items-center p-4 shadow-2">
            <button pButton pRipple type="button" icon="pi pi-github fs-large" class="p-button-rounded p-button-text p-button-plain" [ngClass]="{'ml-auto mr-2': !layoutService.app.isRTL, 'ml-2 mr-auto': layoutService.app.isRTL}"></button>
            <button pButton pRipple type="button" icon="pi pi-facebook fs-large" class="p-button-rounded p-button-text p-button-plain" [ngClass]="{'mr-2': !layoutService.app.isRTL, 'ml-2': layoutService.app.isRTL}"></button>
            <button pButton pRipple type="button" icon="pi pi-twitter fs-large" class="p-button-rounded p-button-text p-button-plain" [ngClass]="{'mr-2': !layoutService.app.isRTL, 'ml-2': layoutService.app.isRTL}"></button>
        </div>
*/
