import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AppManageLayoutComponent } from './layout/manage/app.manage.component';
import { CommonModule, DatePipe, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { MenuService } from './app-systems/app-menu/app.menu.service';
import { AppBreadcrumbService } from './app-systems/app-breadcrumb/app.breadcrumb.service';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
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
import { JwtModule } from '@auth0/angular-jwt';
import { AppNotfoundComponent } from './pages/app.notfound.component';
import { AppErrorComponent } from './pages/app.error.component';
import { AppAccessdeniedComponent } from './pages/app.accessdenied.component';
import { JwtService } from './services/app.jwt.service';
import { LoginComponent } from './login/login.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ChatComponent } from './modules/chat/chat.component';
import { RegisterComponent } from './register/register.component';
import { ConfirmemailComponent } from './confirmemail/confirmemail.component';
import { LoadingComponent } from './modules/shared-module/components/loading-component/loading.component';
import { ForgotComponent } from './forgot/forgot.component';
import { LoginNonCustomerComponent } from './login-non-customer/login-non-customer.component';
import { ManageCustomerModuleComponent } from './modules/admin-routing-module/manage-customer-module/manage-customer-module.component';
import { ManagePostmoderatorModuleComponent } from './modules/admin-routing-module/manage-postmoderator-module/manage-postmoderator-module.component';
import { SellerListComponent } from './seller-list/seller-list.component';
import { FoodManagementComponent } from './modules/menumoderator-routing-module/components/food-management/food-management.component';
import { VoucherManagementComponent } from './modules/seller-routing-module/components/voucher-management/voucher-management.component';
import { InvitationShipperComponent } from './modules/seller-routing-module/components/invitation-shipper/invitation-shipper.component';
import { ManageSellerModuleComponent } from './modules/admin-routing-module/manage-seller-module/manage-seller-module.component';
import { ManageShipperModuleComponent } from './modules/admin-routing-module/manage-shipper-module/manage-shipper-module.component';
import { ChatboxComponent } from './chatbox/chatbox.component';
import { ProfileManagementComponent } from './modules/business-routing-module/components/profile-management/profile-management.component';
import { OrderhistoryComponent } from './modules/customer-routing-module/components/orderhistory/orderhistory.component';
import { MenuDataService } from './services/menu-data.service';
import { BusinessRoutingModule } from './modules/business-routing-module/business-routing-module.module';
import { WalletComponent } from './modules/customer-routing-module/components/wallet/wallet.component';
import { PaymentverifyComponent } from './modules/customer-routing-module/components/paymentverify/paymentverify.component';
import { CustomerListByChatComponent } from './customer-list-by-chat/customer-list-by-chat.component';
import { CustomerfoodreportComponent } from './modules/customer-routing-module/components/customerfoodreport/customerfoodreport.component';
import { CustomerpostreportComponent } from './modules/customer-routing-module/components/customerpostreport/customerpostreport.component';

import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { AppNavbarComponent } from './app-systems/app-navbar/app-navbar.component';


@NgModule({
  declarations: [
    AppComponent,
    AppManageLayoutComponent,
    AppBreadcrumbComponent,
    AppTopBarComponent,
    AppFooterComponent,
    AppMenuComponent,
    AppMenuitemComponent,
    AppNotfoundComponent,
    AppErrorComponent,
    AppAccessdeniedComponent,
    LoginComponent,
    ChatComponent,
    RegisterComponent,
    ConfirmemailComponent,
    ForgotComponent,
    LoginNonCustomerComponent,
    ManageCustomerModuleComponent,
    ManagePostmoderatorModuleComponent,
    VoucherManagementComponent,
    InvitationShipperComponent,
    ManageSellerModuleComponent,
    ManageShipperModuleComponent,
    ProfileManagementComponent,
    OrderhistoryComponent,
    WalletComponent,
    PaymentverifyComponent,
    OrderhistoryComponent,
    // CustomerfoodreportComponent,
    // CustomerpostreportComponent
    AppNavbarComponent


    
  ],
  imports: [
    BrowserAnimationsModule,
    AppRoutingModule,
    HttpClientModule,
    ComponentModule,
    SharedModule,
    AutoCompleteModule,
    AppCustomerLayoutModule,
    ReactiveFormsModule,
    FormsModule,
    BusinessRoutingModule,
    JwtModule.forRoot({
      config: {
        tokenGetter:  () => sessionStorage.getItem('token')
      }
    }),
    TranslateModule.forRoot({

      loader: {
 
        provide: TranslateLoader,
 
        useFactory: httpTranslateLoader,
 
        deps: [HttpClient]
 
      }
 
    })

  ],

  providers: [
    JwtService,
    MenuDataService,
    {provide: LocationStrategy, useClass: HashLocationStrategy},
    MenuService, AppBreadcrumbService, DatePipe,
    {provide: HTTP_INTERCEPTORS, useClass: MyHttpInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true},
    ShareData, CacheData,
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
export function httpTranslateLoader(http: HttpClient) {

  return new TranslateHttpLoader(http);
 
 }