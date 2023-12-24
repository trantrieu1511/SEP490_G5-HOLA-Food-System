import { NgModule } from '@angular/core';
import { CommonModule, HashLocationStrategy, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared-module/shared-module.module';
import { AppComponent } from 'src/app/app.component';
import { ComponentModule } from '../components-module/component.modules';
import { ShipperComponent } from './components/shipper-order-management/shipper.component';
import { ShipperHistoryManagementComponent } from './components/shipper-history-management/shipper-history-management.component';
import { DashboadShipperModuleComponent } from './components/dashboad-shipper-module/dashboad-shipper-module.component';


const routes: Routes = [
  {path: 'order', component: ShipperComponent},
  {path: 'history', component: ShipperHistoryManagementComponent},
  {path: 'dashboard', component: DashboadShipperModuleComponent},
]

@NgModule({
  declarations: [
    ShipperComponent,
    ShipperHistoryManagementComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ComponentModule,
    RouterModule.forChild(routes),
  ],
  exports: [
    ShipperComponent
  ],
  providers: [
    {provide: LocationStrategy, useClass: PathLocationStrategy}
  ],
  bootstrap: [AppComponent]
})
export class ShipperRoutingModule { }
