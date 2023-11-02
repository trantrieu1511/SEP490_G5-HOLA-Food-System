import { NgModule } from '@angular/core';
import { CommonModule, HashLocationStrategy, LocationStrategy } from '@angular/common';
import { SharedModule } from '../shared-module/shared-module.module';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { ComponentModule } from '../components-module/component.modules';
import { ManageCustomerModuleComponent } from './manage-customer-module/manage-customer-module.component';
import { ManagePostmoderatorModuleComponent } from './manage-postmoderator-module/manage-postmoderator-module.component';

const routes: Routes = [
  {path: "", component: ManageCustomerModuleComponent},
  {path: "post-moderator", component: ManagePostmoderatorModuleComponent},
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
    {provide: LocationStrategy, useClass: HashLocationStrategy}
  ],
  bootstrap: [AppComponent]
})
export class AdminRoutingModule { }
