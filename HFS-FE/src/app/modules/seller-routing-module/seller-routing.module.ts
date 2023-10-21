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

const routes: Routes = [
  {path: 'order-management', component: OrderManagementComponent},
  {path: 'shipper-management', component: ShipperComponent},
  {path: 'post-management', component: PostManagementComponent},
  {path: 'food-management', component: FoodManagementComponent},
]

@NgModule({
  declarations: [
    OrderManagementComponent,
    ShipperComponent,
    PostManagementComponent,
    FoodManagementComponent,
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
export class SellerRoutingModule { }
