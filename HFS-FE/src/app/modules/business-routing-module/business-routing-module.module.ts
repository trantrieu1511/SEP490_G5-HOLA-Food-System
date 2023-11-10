import { NgModule } from '@angular/core';
import { CommonModule, LocationStrategy, HashLocationStrategy } from '@angular/common';
import { SellerRoutingModule } from '../seller-routing-module/seller-routing.module';
import { PostmoderatorRoutingModule } from '../postmoderator-routing-module/postmoderator-routing.module';
import { ShipperRoutingModule } from '../shipper-routing-module/shipper-routing.module';
import { MenumoderatorRoutingModule } from '../menumoderator-routing-module/menumoderator-routing.module';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { ManagePostComponent } from './components/manage-post/manage-post.component';
import { SharedModule } from '../shared-module/shared-module.module';
import { ComponentModule } from '../components-module/component.modules';
import { ManageNotificationComponent } from './components/manage-notification/manage-notification.component';
import { DetailNotificationComponent } from './components/detail-notification/detail-notification.component';

// const routes: Routes = [
//   {path: 'post-management', component: ManagePostComponent},
//   {path: 'post-management/detail', component: DetailNotificationComponent},
// ]

@NgModule({
  declarations: [
    ManagePostComponent,
    ManageNotificationComponent,
    DetailNotificationComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ComponentModule,
    //RouterModule.forChild(routes),
    SellerRoutingModule,
    PostmoderatorRoutingModule,
    //MenumoderatorRoutingModule,
    //ShipperRoutingModule,

  ],
  exports: [
    ManagePostComponent,
    ManageNotificationComponent,
    DetailNotificationComponent
  ],
  providers: [
    {provide: LocationStrategy, useClass: HashLocationStrategy}
  ],
  bootstrap: [AppComponent]
})
export class BusinessRoutingModule { }
