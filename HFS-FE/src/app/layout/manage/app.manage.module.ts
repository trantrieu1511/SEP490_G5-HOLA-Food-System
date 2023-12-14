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
import { ComponentModule } from 'src/app/modules/components-module/component.modules';
import { SharedModule } from 'src/app/modules/shared-module/shared-module.module';
import { CommonModule } from '@angular/common';
import { AppBreadcrumbComponent } from 'src/app/app-systems/app-breadcrumb/app.breadcrumb.component';
import { AppTopBarComponent } from 'src/app/app-systems/app-topbar/app.topbar.component';
import { AppFooterComponent } from 'src/app/app-systems/app-footer/app.footer.component';
import { AppMenuComponent } from 'src/app/app-systems/app-menu/app-menu.component';
import { AppMenuitemComponent } from 'src/app/app-systems/app-menuitem/app.menuitem.component';
import { AppManageLayoutComponent } from './app.manage.component';
import { CustomerRoutingModule } from 'src/app/modules/customer-routing-module/customer-routing.module';

@NgModule({
    declarations: [
      AppBreadcrumbComponent,
      AppTopBarComponent,
      AppFooterComponent,
      AppMenuComponent,
      AppMenuitemComponent,
      AppManageLayoutComponent
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
        SharedModule,
        CustomerRoutingModule
        // CommonModule
    ],
    exports: [
      AppManageLayoutComponent,
      AppBreadcrumbComponent,
      AppTopBarComponent,
      AppFooterComponent,
      AppMenuComponent,
      AppMenuitemComponent,
    ]
})
export class AppManageLayoutModule { }
