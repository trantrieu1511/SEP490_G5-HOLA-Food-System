import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InputTextModule } from 'primeng/inputtext';
import { SidebarModule } from 'primeng/sidebar';
import { BadgeModule } from 'primeng/badge';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputSwitchModule } from 'primeng/inputswitch';
import { RippleModule } from 'primeng/ripple';
import { RouterModule } from '@angular/router';
import { AppCustomerTopBarComponent } from 'src/app/app-systems/app-topbar/customer/app.topbar-cus.component';
import { AppCustomerLayoutComponent } from './app.customer.component';
import { AppCustomerFooterComponent } from 'src/app/app-systems/app-footer/customer/app.footer-cus.component';
import { ComponentModule } from 'src/app/modules/components-module/component.modules';
import { SharedModule } from 'src/app/modules/shared-module/shared-module.module';

@NgModule({
    declarations: [
        AppCustomerTopBarComponent,
        AppCustomerFooterComponent,
        AppCustomerLayoutComponent,
    ],
    imports: [
        ComponentModule,
        BrowserModule,
        FormsModule,
        HttpClientModule,
        BrowserAnimationsModule,
        InputTextModule,
        SidebarModule,
        BadgeModule,
        RadioButtonModule,
        InputSwitchModule,
        RippleModule,
        RouterModule,
        SharedModule
    ],
    exports: [AppCustomerLayoutComponent]
})
export class AppCustomerLayoutModule { }
