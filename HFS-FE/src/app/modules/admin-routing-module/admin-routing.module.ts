import { NgModule } from '@angular/core';
import { CommonModule, HashLocationStrategy, LocationStrategy, PathLocationStrategy } from '@angular/common';
import { SharedModule } from '../shared-module/shared-module.module';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { ComponentModule } from '../components-module/component.modules';
import { ManageCustomerModuleComponent } from './manage-customer-module/manage-customer-module.component';
import { ManagePostmoderatorModuleComponent } from './manage-postmoderator-module/manage-postmoderator-module.component';
import { ManageSellerModuleComponent } from './manage-seller-module/manage-seller-module.component';
import { ManageShipperModuleComponent } from './manage-shipper-module/manage-shipper-module.component';
import { ManageMenumoderatorModuleComponent } from './manage-menumoderator-module/manage-menumoderator-module.component';
//import { ManageCategoryModuleComponent } from './manage-category-module/manage-category-module.component';
import { ManageCategoryModuleComponent } from './manage-category-module/manage-category-module.component';
import { DashboadAdminModuleComponent } from './dashboad-admin-module/dashboad-admin-module.component';
import { SellerReportModuleComponent } from './seller-report-module/seller-report-module.component';
import { ManageAccountantModuleComponent } from './manage-accountant-module/manage-accountant-module.component';


const routes: Routes = [
  {path: "customer-management", component: ManageCustomerModuleComponent},
  {path: "post-moderator", component: ManagePostmoderatorModuleComponent},
  {path: "seller-management", component: ManageSellerModuleComponent},
  {path: "shipper-management", component: ManageShipperModuleComponent},
  {path: "menu-moderator", component: ManageMenumoderatorModuleComponent},
  //{path: "manage-category", component: ManageCategoryModuleComponent},
  {path: "category-management", component: ManageCategoryModuleComponent},
  {path: "dashboard", component: DashboadAdminModuleComponent},
  {path: "report", component: SellerReportModuleComponent},
  {path: "accountant", component: ManageAccountantModuleComponent},
]


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    SharedModule,
    ComponentModule,
    RouterModule.forChild(routes),
  ],
  exports: [

  ],
  providers: [
    {provide: LocationStrategy, useClass: PathLocationStrategy}
  ],
  bootstrap: [AppComponent]
})
export class AdminRoutingModule { }
