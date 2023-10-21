import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared-module/shared-module.module';
import {HashLocationStrategy, LocationStrategy} from '@angular/common';
import {AppComponent} from '../../app.component';
import { ComponentModule } from '../components-module/component.modules';
import { HomepageComponent } from './components/homepage/homepage.component';
import { FooddetailComponent } from './components/fooddetail/fooddetail.component';
import { CartdetailComponent } from './components/cartdetail/cartdetail.component';
import { ShopdetailComponent } from './components/shopdetail/shopdetail.component';
import { CheckoutComponent } from './components/checkout/checkout.component';
import { ManageprofileComponent } from './components/manageprofile/manageprofile.component';


const routes: Routes = [
  {path: "", component: HomepageComponent},
  {path: "shopdetail", component: ShopdetailComponent},
  {path: "fooddetail", component: FooddetailComponent},
  {path: "cartdetail", component: CartdetailComponent},
  {path: "checkout", component : CheckoutComponent}
]

@NgModule({
  declarations: [
    HomepageComponent,
    FooddetailComponent,
    CartdetailComponent,
    ShopdetailComponent,
    CheckoutComponent,
    ManageprofileComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ComponentModule,
    RouterModule.forChild(routes),
  ],
  exports: [
    ManageprofileComponent
  ],
  providers: [
    {provide: LocationStrategy, useClass: HashLocationStrategy}
  ],
  bootstrap: [AppComponent]
})
export class CustomerRoutingModule { }
