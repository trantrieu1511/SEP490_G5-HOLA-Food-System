import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared-module/shared-module.module';
import { HashLocationStrategy, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { SwitchCasesDirective } from './services/switch-case.directive';
import { AppComponent } from '../../app.component';
import { ComponentModule } from '../components-module/component.modules';
import { HomepageComponent } from './components/homepage/homepage.component';
import { FooddetailComponent } from './components/fooddetail/fooddetail.component';
import { CartdetailComponent } from './components/cartdetail/cartdetail.component';
import { ShopdetailComponent } from './components/shopdetail/shopdetail.component';
import { CheckoutComponent } from './components/checkout/checkout.component';
// import { ManageprofileComponent } from '../../profile/manageprofile.component';
import { OrderhistoryComponent } from './components/orderhistory/orderhistory.component';
import { WalletComponent } from './components/wallet/wallet.component';
import { PaymentverifyComponent } from './components/paymentverify/paymentverify.component';
import { CustomerfoodreportComponent } from './components/customerfoodreport/customerfoodreport.component';
import { CustomerpostreportComponent } from './components/customerpostreport/customerpostreport.component';
import { ManageshipaddressComponent } from './components/shipaddress/manageshipaddress.component';
import { NewFeedModuleComponent } from './components/new-feed-module/new-feed-module.component';
import { SearchComponent } from './components/search/search.component';
import { AppMenuMyaccountComponent } from 'src/app/app-systems/app-menu/myaccount/app-menu-myaccount.component';
import { ManageprofileComponent } from 'src/app/profile/manageprofile.component';
import { SellerReportModuleComponent } from '../admin-routing-module/seller-report-module/seller-report-module.component';
import { ReportSellerComponent } from './components/report-seller/report-seller.component';
import { AddressSelectorComponent } from './address-selector/address-selector.component';

const routes: Routes = [
  { path: "cartdetail", component: CartdetailComponent },
  { path: "checkout", component: CheckoutComponent },
  { path: "orderhistory", component: OrderhistoryComponent },
  { path: "wallet", component: WalletComponent },
  { path: "paymentverify", component: PaymentverifyComponent },
  { path: "newfeed", component: NewFeedModuleComponent },
  // -------------- My account page related routes -----------------
  { path: "profile", component: ManageprofileComponent },
  { path: "menureport", component: CustomerfoodreportComponent },
  { path: "postreport", component: CustomerpostreportComponent },
  { path: "shipaddress", component: ManageshipaddressComponent },
  { path: "sellerreport", component: ReportSellerComponent },
  { path: "address", component: AddressSelectorComponent },
]


@NgModule({
  declarations: [
    HomepageComponent,
    FooddetailComponent,
    CartdetailComponent,
    ShopdetailComponent,
    CheckoutComponent,
    SwitchCasesDirective,
    OrderhistoryComponent,
    WalletComponent,
    ManageprofileComponent,
    CustomerfoodreportComponent,
    CustomerpostreportComponent,
    ManageshipaddressComponent,
    AppMenuMyaccountComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ComponentModule,
    RouterModule.forChild(routes),
  ],
  exports: [
    // AppMenuMyaccountComponent
  ],
  providers: [
    { provide: LocationStrategy, useClass: PathLocationStrategy }
  ],
  bootstrap: [AppComponent]
})
export class CustomerRoutingModule { }
