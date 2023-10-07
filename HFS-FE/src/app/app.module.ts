import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppManageLayoutComponent } from './layout/manage/app.manage.component';
import { DatePipe, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { MenuService } from './app-systems/app-menu/app.menu.service';
import { AppBreadcrumbService } from './app-systems/app-breadcrumb/app.breadcrumb.service';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { MyHttpInterceptor } from './modules/shared-module/Interceptors/MyHttpInterceptor';
import { LoadingInterceptor } from './modules/shared-module/Interceptors/LoadingInterceptor';
import { ShareData } from './modules/shared-module/shared-module';
import { CacheData } from './modules/shared-module/shared-data-services/cachedata.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ComponentModule } from './modules/components-module/component.modules';
import {
  SharedModule
} from './modules/shared-module/shared-module.module';
import {AutoCompleteModule} from 'primeng/autocomplete';
import { AppBreadcrumbComponent } from './app-systems/app-breadcrumb/app.breadcrumb.component';
import { AppTopBarComponent } from './app-systems/app-topbar/app.topbar.component';
import { AppFooterComponent } from './app-systems/app-footer/app.footer.component';
import { AppMenuComponent } from './app-systems/app-menu/app-menu.component';
import { AppMenuitemComponent } from './app-systems/app-menuitem/app.menuitem.component';
import { AppCustomerLayoutModule } from './layout/customer/app.customer.module';
import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA } from '@angular/compiler';

@NgModule({
  declarations: [
    AppComponent,
    AppManageLayoutComponent,
    AppBreadcrumbComponent,
    AppTopBarComponent,
    AppFooterComponent,
    AppMenuComponent,
    AppMenuitemComponent,
    
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    ComponentModule,
    SharedModule,
    AutoCompleteModule,
    AppCustomerLayoutModule,
  ],
  providers: [
    {provide: LocationStrategy, useClass: HashLocationStrategy},
        MenuService, AppBreadcrumbService, DatePipe,
        {provide: HTTP_INTERCEPTORS, useClass: MyHttpInterceptor, multi: true},
        {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
        ShareData, CacheData,
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
