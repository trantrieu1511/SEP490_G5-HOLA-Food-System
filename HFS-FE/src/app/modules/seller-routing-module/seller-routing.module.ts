import { NgModule } from '@angular/core';
import { CommonModule, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { SharedModule } from '../shared-module/shared-module.module';
import { OrderManagementComponent } from './components/order-management/order-management.component';
import { ComponentModule } from '../components-module/component.modules';
import { SwitchCasesDirective } from './services/switch-case.directive';
import { ShipperComponent } from './components/shipper-management/shipper.component';
import { PostManagementComponent } from './components/post-management/post-management.component';
import { FoodManagementComponent } from './components/food-management/food-management.component';
import { ReactiveFormsModule } from '@angular/forms';
import { InvitationShipperComponent } from './components/invitation-shipper/invitation-shipper.component';
import { VoucherManagementComponent } from './components/voucher-management/voucher-management.component';
import { DashboardSellerComponent } from './components/dashboard-seller/dashboard-seller.component';
// import { ProfileManagementComponent } from '../business-routing-module/components/profile-management/profile-management.component';

const routes: Routes = [
  { path: 'order-management', component: OrderManagementComponent },
  { path: 'shipper-management', component: ShipperComponent },
  { path: 'post-management', component: PostManagementComponent },
  { path: 'invitation-shipper', component: InvitationShipperComponent },
  { path: '', component: FoodManagementComponent },
  { path: 'food-management', component: FoodManagementComponent },
  { path: 'voucher-management', component: VoucherManagementComponent },
  // { path: 'profile-management', component: ProfileManagementComponent },
  { path: 'dashboard', component: DashboardSellerComponent },
]
// food-management
@NgModule({
  declarations: [
    OrderManagementComponent,
    ShipperComponent,
    PostManagementComponent,
    FoodManagementComponent,
    SwitchCasesDirective,
    DashboardSellerComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ComponentModule,
    RouterModule.forChild(routes),
  ],
  exports: [
    OrderManagementComponent,
    PostManagementComponent,
    FoodManagementComponent,
    DashboardSellerComponent
  ],
  providers: [
    { provide: LocationStrategy, useClass: HashLocationStrategy }
  ],
  bootstrap: [AppComponent]
})
export class SellerRoutingModule { }
