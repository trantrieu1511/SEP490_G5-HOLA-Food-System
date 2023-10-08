import { NgModule } from '@angular/core';
import { CommonModule, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { SharedModule } from '../shared-module/shared-module.module';
import { OrderManagementComponent } from './components/order-management/order-management.component';
import { ComponentModule } from '../components-module/component.modules';
import { SwitchCasesDirective } from './services/switch-case.directive';

const routes: Routes = [
  {path: 'order-management', component: OrderManagementComponent},
]

@NgModule({
  declarations: [
    OrderManagementComponent,
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
