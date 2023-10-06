import { Component, ElementRef, ViewChild } from '@angular/core';
import { CustomerLayoutService } from 'src/app/layout/service/app.layout-cus.service';


@Component({
    selector: 'app-customer-topbar',
    templateUrl: './app.topbar-cus.component.html'
})
export class AppCustomerTopBarComponent {

    @ViewChild('topbarmenubutton') topbarMenuButton!: ElementRef;

    @ViewChild('topbarmenu') menu!: ElementRef;

    constructor(public layoutService: CustomerLayoutService) { }
}
