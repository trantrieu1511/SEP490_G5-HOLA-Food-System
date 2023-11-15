import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared-module/shared-module.module';
import {HashLocationStrategy, LocationStrategy} from '@angular/common';
import { SwitchCasesDirective } from './services/switch-case.directive';
import {AppComponent} from '../../app.component';
import { ComponentModule } from '../components-module/component.modules';
import { HomepageComponent } from './components/homepage/homepage.component';
import { FooddetailComponent } from './components/fooddetail/fooddetail.component';
import { CartdetailComponent } from './components/cartdetail/cartdetail.component';
import { ShopdetailComponent } from './components/shopdetail/shopdetail.component';
import { CheckoutComponent } from './components/checkout/checkout.component';
import { ManageprofileComponent } from './components/manageprofile/manageprofile.component';
import { OrderhistoryComponent } from './components/orderhistory/orderhistory.component';
import { WalletComponent } from './components/wallet/wallet.component';
import { PaymentverifyComponent } from './components/paymentverify/paymentverify.component';
import { NewfeedComponent } from './components/newfeed/newfeed.component';
import { CustomerfoodreportComponent } from './components/customerfoodreport/customerfoodreport.component';
import { CustomerpostreportComponent } from './components/customerpostreport/customerpostreport.component';



const routes: Routes = [
  {path: "", component: HomepageComponent},
  {path: "shopdetail", component: ShopdetailComponent},
  {path: "fooddetail", component: FooddetailComponent},
  {path: "cartdetail", component: CartdetailComponent},
  {path: "checkout", component : CheckoutComponent},
  {path: "orderhistory", component : OrderhistoryComponent},
  {path: "wallet", component : WalletComponent},
  {path: "paymentverify", component : PaymentverifyComponent},
  {path: "newfeed", component : NewfeedComponent},
  // -------------- My account page related routes -----------------
  {path: "profile", component : ManageprofileComponent},
  {path: "menureport", component : CustomerfoodreportComponent},
  {path: "postreport", component : CustomerpostreportComponent},
]


@NgModule({
  declarations: [
    HomepageComponent,
    FooddetailComponent,
    CartdetailComponent,
    ShopdetailComponent,
    CheckoutComponent,
    ManageprofileComponent,
    SwitchCasesDirective
  ],
  imports: [
    CommonModule,
    SharedModule,
    ComponentModule,
    RouterModule.forChild(routes),
  ],
  exports: [
  ],
  providers: [
    {provide: LocationStrategy, useClass: HashLocationStrategy}
  ],
  bootstrap: [AppComponent]
})
export class CustomerRoutingModule { }
